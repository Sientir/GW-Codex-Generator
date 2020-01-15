using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GW_Codex_Generator
{
    static class Challenges
    {
        /*
        Build Restrictions: (These would be a separate thing that could be rolled to add some constraints to making a team build.)
		O- Require certain numbers of skills from primary and secondary profession. (For example, 4/4 split, 3/5, etc.)
		bO- No profession duplication among primaries and secondaries, separately.That is, if you have a W/Mo, you can't have another primary warrior or secondary monk, but you can have a primary monk and a secondary warrior.
		bO- Skills must be unique.That is, no duplicates across the team.If one character is using, say, Healing Burst, no other character can use it.
		bO- Attribute lines must be unique across the team.So, for example, if a character is using Healing Prayers, no other character may use healing prayers.
		O- Limited numbers of skills allowed to be used from an attribute line.So, perhaps a character can't use more than 3 skills from healing prayers, or perhaps the entire team can't have more than 6 skills from Restoration Magic.
		O- Required skills. That is, a set of skills that have to be used.Could be one per character, or possibly split however I want across characters. Could include elites or not, PvE only skills, etc.I think 8 required elites would probably be pretty nasty to have to work with, but also potentially interesting!
		O- A skill that all characters are required to bring.For example, everyone has to bring Healing Whisper or Tease. Obviously, this is way more restrictive with an elite.
		bO- Elite must be from (Primary/Secondary) profession.I think from Secondary is a better, more interesting restriction.
		- Pool is list of skills I can't use (instead of pool of skills I can only use).
		bO- Pokemon (skill bars are limited to 4 skills).
		O- Elites that have to be used equal to the number of open character slots. Probably should be able to specify rating range for these skills!
		
		*/


        static public List<Skill> MandatoryElites(int charactersCount, int rating_min, int rating_max, bool excludePrimaryAttributes)
        {
            List<Skill> pool = new List<Skill>();

            // If we're excluding primary attributes, do it here:
            if (excludePrimaryAttributes)
            {
                foreach (Skill skill in SkillDatabase.Data)
                {
                    if (skill.IsElite && skill.Rating >= rating_min && skill.Rating <= rating_max && skill.Attribute != Skill.Attributes.PvE_Only && Skill.PrimaryAttributes.Contains(skill.Attribute)==false)
                    {
                        pool.Add(skill);
                    }
                }
            }
            else // Includes primary attributes. Separating these for efficiency (I guess?) and also to simplify the if statements.
            {
                foreach (Skill skill in SkillDatabase.Data)
                {
                    if (skill.IsElite && skill.Rating >= rating_min && skill.Rating <= rating_max && skill.Attribute != Skill.Attributes.PvE_Only)
                    {
                        pool.Add(skill);
                    }
                }
            }

            return new GrabBag(pool).PullXFromBag(charactersCount);
        }

        static public Skill RequiredSkill(List<Skill> pool)
        {
            return new GrabBag(pool).PullFromBag();
        }
        static public List<Skill> RequiredSkills(List<Skill> pool, int count)
        {
            GrabBag bag = new GrabBag(pool);

            return bag.PullXFromBag(count);
        }
        static public int MaxSkillsPerAttribute(int min, int max)
        {
            // Note: May use as limited number per character or per party.
            return RandomInclusive(min, max);
        }

        static public string[] BooleanRestrictions(int count)
        {
            if (count <= 0) return new string[0];

            string[] Restrictions =
            {
                "• No profession may be duplicated in primaries/secondaries. (You may have a W/Mo and a Mo/W, but not a W/Mo and W/R or Mo/W and R/W.)",
                "• No skill duplication. (If a skill is used on one character, it may not be used on another character.)",
                "• Attribute Lines must be unique. (If one character has at least 1 rank in an attribute line, no other character may put any ranks into that attribute line.)",
                "• Pokemon—Skill Bars are limited to 4 skills.",
                "• Elite Restriction..." // index 4
			};

            if (count >= 5)
            {
                bool eliteFromPrimary = RandomBoolean();
                return new string[] { Restrictions[0], Restrictions[1], Restrictions[2], Restrictions[3], eliteFromPrimary ? "Elite Skills must be from primary profession." : "Elite Skills must be from secondary profession." };
            }

            List<int> bag = new List<int>(new int[] { 0, 1, 2, 3, 4 });

            string[] output = new string[count];
            for (int i = 0; i < count; ++i)
            {
                // This is a grab bag, but it isn't a skill grab bag, so I'm just writing it out:
                int index = SkillDatabase.RNG.Next(0, bag.Count);
                int id = bag[index];
                bag.RemoveAt(index);

                if (id == 4) // Index 4 is the elite restrictions, which are mutually exclusive...
                {
                    if (RandomBoolean())
                    {
                        output[i] = "• Elite Skills must be from primary profession.";
                    }
                    else
                    {
                        output[i] = "• Elite Skills must be from secondary profession.";
                    }
                }
                else // The other restrictions...
                {
                    output[i] = Restrictions[id];
                }
            }

            return output;
        }

        static internal Pair<int, int> RequiredSkillCountsPrimarySecondary(int primary_min, int primary_max, int secondary_min, int secondary_max)
        {
            // Get my the requirements!
            Pair<int, int> result = new Pair<int, int>(RandomInclusive(primary_min, primary_max), RandomInclusive(secondary_min, secondary_max));

            // If, for some reason, the total exceeds 8, make it 8. Shave off of the secondary first, though. Gotta pick something!
            while (result.second + result.first > 8)
            {
                if (result.second > 0) result.second--;
                else result.first--;
            }

            // Return our result:
            return result;
        }

        static internal Pair<int, int> RequiredSkillCountsSplit()
        {
            // Must have at least 1 skill from primary and one from secondary, hence the range:
            int primary = RandomInclusive(1, 7);
            int secondary = 8 - primary;

            return new Pair<int, int>(primary, secondary);
        }





        /// <summary>
        /// Gets a random number in the inclusive interval.
        /// </summary>
        /// <param name="min">Minimum: Inclusive</param>
        /// <param name="max">Maximum: Inclusive</param>
        /// <returns></returns>
        static private int RandomInclusive(int min, int max)
        {
            return SkillDatabase.RNG.Next(min, max + 1);
        }

        static private bool RandomBoolean()
        {
            return SkillDatabase.RNG.Next(0, 2) == 0;
        }

        public delegate void RefreshHTMLFunction();
    }

    class Pair<F, S>
    {
        public Pair() { }
        public Pair(F f, S s) { first = f; second = s; }
        public F first;
        public S second;
    }
}
