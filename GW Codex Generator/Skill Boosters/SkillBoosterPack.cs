using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GW_Codex_Generator.Skill_Boosters
{
    class SkillBoosterPack
    {
        static public List<SkillBoosterPack> GenerateBoosterBox(int set, int pack_count)
        {
            List<SkillBoosterPack> packs = new List<SkillBoosterPack>();

            List<int>[] skills_per_rarity = GenerateSkillsPerRarityFromSet(set);

            for (int i = 0; i < pack_count; ++i)
            {
                packs.Add(GeneratePackFromSetPool(set, skills_per_rarity));
            }

            return packs;
        }

        static public SkillBoosterPack GenerateBoosterPack(int set_index)
        {
            return GeneratePackFromSetPool(set_index, GenerateSkillsPerRarityFromSet(set_index));
        }

        static private List<int>[] GenerateSkillsPerRarityFromSet(int set_index)
        {
            // Create the lists:
            List<int>[] skills_per_rarity = new List<int>[SkillDatabase.RarityLabels.Length];
            for (int i = 0; i < skills_per_rarity.Length; ++i)
            {
                skills_per_rarity[i] = new List<int>();
            }

            SkillBoosterSet set = SkillBoosterSet.Sets[set_index]; // Gotta grab this!

            // Do some stuff!
            foreach (int i in set.SetSkills)
            {
                // But, like, actually do the thing!
                // At this point, skills have been excluded based on what campaigns are allowed and whether or not PvE only skills are included.
                // That leaves the ratings filter.
                skills_per_rarity[SkillDatabase.Data[i].Rarity].Add(i);
            }

            return skills_per_rarity;
        }

        static private SkillBoosterPack GeneratePackFromSetPool(int setId, List<int>[] skills_per_rarity)
        {
            // figure out what the pool looks like...
            // First, I need to generate a copy of the pool so I can draw from it properly:
            List<int>[] clone_pool = new List<int>[skills_per_rarity.Length];
            for (int i = 0; i < clone_pool.Length; ++i)
            {
                clone_pool[i] = new List<int>(skills_per_rarity[i]);
            }

            // Let's set up the pack and retrieve the set:
            SkillBoosterPack pack = new SkillBoosterPack();
            SkillBoosterSet set = SkillBoosterSet.Sets[setId];

            // Iterate over each rarity, generating a number of cards equal to the required number. I meant skills, not cards, by the way.
            for (int rarity = 0; rarity < set.PackRarityContents.Length; ++rarity)
            {
                for (int i = 0; i < set.PackRarityContents[rarity]; ++i)
                {
                    // Grab a random item and add it to the pack, then remove it from the pool so I can't get duplicates:
                    int index = SkillDatabase.RNG.Next(clone_pool[rarity].Count);
                    pack.Contents.Add(clone_pool[rarity][index]);
                    clone_pool[rarity].RemoveAt(index);
                }
            }

            // Now that I've filled up the pack, I can turn it in!
            return pack;
        }

        public List<int> Contents = new List<int>();
        public int Count { get { return Contents.Count; } }
        public Skill this[int index] { get { return SkillDatabase.Data[Contents[index]]; } }
    }

    class SkillBoosterSet
    {
        public static List<SkillBoosterSet> Sets = new List<SkillBoosterSet>();
        public int[] PackRarityContents = { 11, 3, 1 }; // Just defaulting to standard MTG booster pack ratios.
        public string Name = "Standard";
        public List<int> SetSkills = new List<int>();
        // Need to define allowed sets, if PvE only are included, and permitted ratings.
        //public int AllowedCampaigns = 0x1F; // Should be all five campaigns if I've got my bit math correct...
        //public bool AllowPvE_OnlySkills = false; // Disable PvE only skills by default.
        //public List<int> PermittedRatings = new List<int>(new int[] { 1, 2, 3, 4, 5 }); // Default to all ratings.

        public List<Skill> GetPool()
        {
            List<Skill> pool = new List<Skill>();
            foreach (int id in SetSkills)
            {
                pool.Add(SkillDatabase.Data[id]);
            }

            return pool;
        }

        /*
         Maybe consider adding finer controls, such as allowing the specification of certain attributes/professions, or even
         listing a select set of skills included!
             */
        static public void LoadSets()
        {
            // Make the default set.
            SkillBoosterSet defaultset = new SkillBoosterSet();
            foreach (Skill skill in SkillDatabase.Data)
            {
                if (skill.Attribute != Skill.Attributes.PvE_Only)
                {
                    defaultset.SetSkills.Add(skill.ID);
                }
            }
            Sets.Add(defaultset);

            // Load any other sets:
            if (System.IO.Directory.Exists(Environment.CurrentDirectory + "\\GW Codex Skill Sets"))
            {
                string[] setFiles = System.IO.Directory.GetFiles(Environment.CurrentDirectory + "\\GW Codex Skill Sets", "*.gwset");
                foreach (string file in setFiles)
                {
                    SkillBoosterSet set = Read(file);
                    if (set != null)
                    {
                        Sets.Add(set);
                    }
                }
            }
        }

        public void Save(string filename)
        {
            System.IO.StreamWriter output = new System.IO.StreamWriter(filename);

            output.WriteLine("File format: Instruct line (this line), set name, quantities per rarity (lowest rarity|middle|...|highest), then remaining lines are internal IDs of skills included in the set.");
            output.WriteLine(Name);
            // The rarities quantities!
            if (PackRarityContents.Length > 0) output.Write(PackRarityContents[0].ToString());
            for (int i = 1; i < PackRarityContents.Length; ++i) output.Write("|" + PackRarityContents[i].ToString());
            output.Write(Environment.NewLine);
            // The included skills!
            foreach (int id in SetSkills) output.WriteLine(id.ToString());

            output.Close();
            System.Windows.Forms.MessageBox.Show("Skill Booster Set saved to " + filename + "!", "Save Complete", System.Windows.Forms.MessageBoxButtons.OK);
        }

        static public SkillBoosterSet Read(string filename)
        {
            System.IO.StreamReader input = new System.IO.StreamReader(filename);
            SkillBoosterSet set = new SkillBoosterSet();

            input.ReadLine(); // Instruction line, undeeded.

            // Read name:
            set.Name = input.ReadLine();

            // Read rarity counts:
            string linecopy = input.ReadLine();
            string[] raritycontents = linecopy.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            List<int> translated_raritycontents = new List<int>();
            foreach (string str in raritycontents)
            {
                try
                {
                    translated_raritycontents.Add(Convert.ToInt32(str));
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show("Warning: Malformed number \"" + str + "\" in line \"" + linecopy + "\" in Skill Set file: " + filename + Environment.NewLine + Environment.NewLine + "Error message: " + e.Message, "Malformed Line", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                }
            }

            // Read included skills:
            while (input.EndOfStream == false)
            {
                string line = input.ReadLine();
                int id = -1;
                try
                {
                    id = Convert.ToInt32(line);
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show("Warning: Malformed line \"" + line + "\" in Skill Set file: " + filename + Environment.NewLine + Environment.NewLine + "Error message: " + e.Message, "Malformed Line", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                }

                if (id < 0 || id >= SkillDatabase.Data.Count)
                {
                    System.Windows.Forms.MessageBox.Show("Warning: Internal skill ID \"" + id.ToString() + "\" is out of bounds in Skill Set file: " + filename);
                }
                else
                {
                    set.SetSkills.Add(id);
                }
            }

            input.Close();

            return set;
        }
    }

    class BoosterLeaguePool
    {
        List<Pair<int, int>> Pool = new List<Pair<int, int>>();

        public int Count { get { return Pool.Count; } }
        public int QuantityAt(int i) { return Pool[i].second; }
        public Skill SkillAt(int i) { return SkillDatabase.Data[Pool[i].first]; }

        public void Clear()
        {
            Pool.Clear();
        }

        public void AddPack(SkillBoosterPack pack)
        {
            foreach (int skillId in pack.Contents)
            {
                AddSkill(skillId);
            }
        }

        public void AddSkill(int skillId)
        {
            // Look to see if the skill id is already in the pool:
            int index = Pool.FindIndex((x) => x.first == skillId);
            // If it isn't, then add a single copy:
            if (index < 0) Pool.Add(new Pair<int, int>(skillId, 1));
            // If the skill is in the pool, then increment our count for the number of copies in the pool.
            else Pool[index].second++;
        }

        public void AddSkill(Skill skill)
        {
            AddSkill(skill.ID);
        }

        public void AddBox(List<SkillBoosterPack> box)
        {
            foreach (SkillBoosterPack pack in box) AddPack(pack);
        }

        public void SortByProfessionAttribute()
        {
            Pool.Sort((x, y) => SkillDatabase.Data[x.first].Compare(SkillDatabase.Data[y.first]));
        }

        public void SortByRarity()
        {
            Pool.Sort((x, y) => SkillDatabase.Data[x.first].Rarity - SkillDatabase.Data[y.first].Rarity);
        }

        public void SortByRating()
        {
            Pool.Sort((x, y) => SkillDatabase.Data[y.first].Rating - SkillDatabase.Data[x.first].Rating);
        }

        public void SavePool(string filename)
        {
            System.IO.StreamWriter output = new System.IO.StreamWriter(filename);

            output.WriteLine("This file contains a list of internal skill ids and quantities separated by |. If you modify this file, do not erase this line.");
            foreach (Pair<int, int> item in Pool)
            {
                output.WriteLine(item.first.ToString() + "|" + item.second.ToString());
            }

            output.Close();
            System.Windows.Forms.MessageBox.Show("Booster League Pool saved!", "Save Complete", System.Windows.Forms.MessageBoxButtons.OK);
        }

        public static BoosterLeaguePool ReadFromFile(string filename)
        {
            System.IO.StreamReader input = new System.IO.StreamReader(filename);

            input.ReadLine(); // Toss; instruction line.
            List<Pair<int, int>> pool = new List<Pair<int, int>>();

            // Read the pool using a now extremely familiar format:
            while (input.EndOfStream == false)
            {
                string linecopy = input.ReadLine();
                string[] line = linecopy.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                if (line.Length < 2)
                {
                    System.Windows.Forms.MessageBox.Show("Warning: Malformed line \"" + linecopy + "\".", "Malformed Line", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                }

                int id = -1;
                int quantity = -1;
                try
                {
                    id = Convert.ToInt32(line[0]);
                    quantity = Convert.ToInt32(line[1]);
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show("Warning: Malformed line \"" + linecopy + "\"" + Environment.NewLine + Environment.NewLine + "Error message: " + e.Message, "Malformed Line", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                    continue;
                }

                pool.Add(new Pair<int, int>(id, quantity));
            }

            input.Close();

            // Make a booster league and assign the pool!
            BoosterLeaguePool league = new BoosterLeaguePool();
            league.Pool = pool;
            return league;
        }
    }
}
