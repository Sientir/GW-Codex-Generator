using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

namespace GW_Codex_Generator
{
    class Skill
    {
        public enum Professions
        {
            Warrior,
            Mesmer,
            Ranger,
            Monk,
            Necromancer,
            Elementalist,
            Assassin,
            Ritualist,
            Dervish,
            Paragon,
            None
        };

        /// <summary>
        /// Attributes for all professions.
        /// </summary>
        public enum Attributes
        {
            Axe_Mastery,
            Hammer_Mastery,
            Strength,
            Swordsmanship,
            Tactics,
            Domination_Magic,
            Fast_Casting,
            Illusion_Magic,
            Inspiration_Magic,
            Earth_Prayers,
            Mysticism,
            Scythe_Mastery,
            Wind_Prayers,
            Command,
            Leadership,
            Motivation,
            Spear_Mastery,
            Beast_Mastery,
            Expertise,
            Marksmanship,
            Wilderness_Survival,
            Critical_Strikes,
            Dagger_Mastery,
            Deadly_Arts,
            Shadow_Arts,
            Divine_Favor,
            Healing_Prayers,
            Protection_Prayers,
            Smiting_Prayers,
            Blood_Magic,
            Curses,
            Death_Magic,
            Soul_Reaping,
            Air_Magic,
            Earth_Magic,
            Energy_Storage,
            Fire_Magic,
            Water_Magic,
            Channeling_Magic,
            Communing,
            Restoration_Magic,
            Spawning_Power,
            PvE_Only,
            None
        };

        /// <summary>
        /// An array of all of the primary attributes.
        /// </summary>
        public static readonly Attributes[] PrimaryAttributes =
        {
            Attributes.Strength,
            Attributes.Fast_Casting,
            Attributes.Divine_Favor,
            Attributes.Expertise,
            Attributes.Energy_Storage,
            Attributes.Soul_Reaping,
            Attributes.Critical_Strikes,
            Attributes.Spawning_Power,
            Attributes.Leadership,
            Attributes.Mysticism
        };

        public enum Campaigns
        {
            Core,
            Prophecies,
            Factions,
            Nightfall,
            Eye_of_the_North
        }

        static Font QuantityFont = new Font("Arial", 16);
        static SolidBrush QuantityBrush = new SolidBrush(Color.White);
        static SolidBrush QuantityBackgroundBrush = new SolidBrush(Color.FromArgb(192, 0, 0, 0));

        static public string[] AttributeNames =
        {
            "Axe Mastery",
            "Hammer Mastery",
            "Strength",
            "Swordsmanship",
            "Tactics",
            "Domination Magic",
            "Fast Casting",
            "Illusion Magic",
            "Inspiration Magic",
            "Earth Prayers",
            "Mysticism",
            "Scythe Mastery",
            "Wind Prayers",
            "Command",
            "Leadership",
            "Motivation",
            "Spear Mastery",
            "Beast Mastery",
            "Expertise",
            "Marksmanship",
            "Wilderness Survival",
            "Critical Strikes",
            "Dagger Mastery",
            "Deadly Arts",
            "Shadow Arts",
            "Divine Favor",
            "Healing Prayers",
            "Protection Prayers",
            "Smiting Prayers",
            "Blood Magic",
            "Curses",
            "Death Magic",
            "Soul Reaping",
            "Air Magic",
            "Earth Magic",
            "Energy Storage",
            "Fire Magic",
            "Water Magic",
            "Channeling Magic",
            "Communing",
            "Restoration Magic",
            "Spawning Power",
            "PvE-Only",
            "No Profession"
        };

        string _Name = "Not a skill";
        Image _Icon = null;
        int _Rating = 0;
        Professions _Profession = Professions.None;
        Attributes _Attribute = Attributes.None;
        bool Elite = false;
        string _Description = "I am a skill.";
        public int Rarity = 0;
        public Campaigns Campaign = Campaigns.Core;

        #region Costs
        public string Cost_Energy { get; private set; }
        public string Cost_Adrenaline { get; private set; }
        public string Cost_CastTime { get; private set; }
        public string Cost_Recharge { get; private set; }
        public string Cost_Maintainence { get; private set; }
        public string Cost_Sacrifice { get; private set; }
        public string Cost_Overcast { get; private set; }
        #endregion

        public string AttributeName { get { return AttributeNames[(int)_Attribute]; } }

        public int ID { get; private set; }

        public Skill(string name, Image icon, Attributes attribute, Professions profession, int rating, string description, bool elite = false)
        {
            Name = name;
            Icon = icon;
            Attribute = attribute;
            Profession = profession;
            Elite = elite;
            Rating = rating;

            string[] descsplit = description.Split(new string[] { " - " }, StringSplitOptions.RemoveEmptyEntries);
            string[] costs = descsplit[0].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            Description = descsplit[1];
            ProcessCosts(costs);
        }

        private void ProcessCosts(string[] costs)
        {
            // Set these up to be empty at first:
            Cost_Energy = "";
            Cost_Adrenaline = "";
            Cost_CastTime = "";
            Cost_Recharge = "";
            Cost_Maintainence = "";
            Cost_Sacrifice = "";
            Cost_Overcast = "";

            // Fill in any found costs:
            for (int i = 0; i < costs.Length; ++i)
            {
                if (costs[i].EndsWith("C")) // Cast time
                {
                    Cost_CastTime = costs[i].Substring(0, costs[i].Length - 1).ReplaceFractions();
                }
                else if (costs[i].EndsWith("E")) // Energy
                {
                    Cost_Energy = costs[i].Substring(0, costs[i].Length - 1);
                }
                else if (costs[i].EndsWith("A")) // Adrenaline
                {
                    Cost_Adrenaline = costs[i].Substring(0, costs[i].Length - 1);
                }
                else if (costs[i].EndsWith("O")) // Overcast
                {
                    Cost_Overcast = costs[i].Substring(0, costs[i].Length - 1);
                }
                else if (costs[i].EndsWith("HP")) // HP sacrifice
                {
                    Cost_Sacrifice = costs[i].Substring(0, costs[i].Length - 2);
                }
                else if (costs[i].EndsWith("ER")) // Upkeep
                {
                    Cost_Maintainence = costs[i].Substring(0, costs[i].Length - 2);
                }
                else if (costs[i].EndsWith("R")) // Recharge...HAS to come after upkeep so I don't get a false match!
                {
                    Cost_Recharge = costs[i].Substring(0, costs[i].Length - 1);
                }
            }
        }

        public void Draw(Graphics g, int x, int y)
        {
            g.DrawImage(Icon, x, y, Icon.Width, Icon.Height);
        }

        public void Draw(Graphics g, int x, int y, int quantity)
        {
            Draw(g, x, y); // Just use this for consistency...
            string quantity_string = quantity.ToString();
            SizeF dimensions = g.MeasureString(quantity_string, QuantityFont);
            g.FillRectangle(QuantityBackgroundBrush, x, y, dimensions.Width + 2, dimensions.Height);
            g.DrawString(quantity.ToString(), QuantityFont, QuantityBrush, x + 1, y + 1);
        }

        public void AddToBag(GrabBag bag)
        {
            bag.Skills.Add(this);
        }

        public string WikiLink()
        {
            return "https://wiki.guildwars.com/wiki/" + Name.Replace(' ', '_');
        }

        public string AHref()
        {
            return "<a href=\"" + WikiLink() + "\">" + Name + "</a>";
        }

        public string AHref(string iconpath, bool justIcon = true)
        {
            if (justIcon)
            {
                return "<a href=\"" + WikiLink() + "\">" + Name + "</a>";
            }
            else
            {
                return "<a href=\"" + WikiLink() + "\">" + Name + "</a>";
            }
        }

        public void SetID(int value)
        {
            ID = value;
        }

        public string Name { get { return _Name; } private set { _Name = value; } }
        public string Description { get { return _Description; } private set { _Description = value; } }
        public int Rating { get { return _Rating; } private set { _Rating = value; } }
        public Professions Profession { get { return _Profession; } private set { _Profession = value; } }
        public Attributes Attribute { get { return _Attribute; } private set { _Attribute = value; } }
        public bool IsElite { get { return Elite; } }
        public Image Icon { get { return _Icon; } private set { _Icon = value; } }

        public int Compare(Skill y)
        {
            return (int)(Profession == y.Profession ? (Attribute == y.Attribute ? (Name.CompareTo(y.Name)) : Attribute - y.Attribute) : Profession - y.Profession);
        }

        public void UpdateRating(int newRating)
        {
            _Rating = newRating;
        }

        public string GetRarityLabel()
        {
            if (Rarity < 0 || Rarity >= SkillDatabase.RarityLabels.Length) return "Unknown Rarity";
            else return SkillDatabase.RarityLabels[Rarity];
        }
    }

    static class EXTENSIONS
    {
        static public bool IsPrimary(this Skill.Attributes att)
        {
            return Skill.PrimaryAttributes.Contains(att);
        }

        /// <summary>
        /// Sorts skills by profession -> attribute -> alphabetical
        /// </summary>
        /// <param name="list">The list to sort.</param>
        static public void SkillSort(this List<Skill> list)
        {
            // Hideous nested conditional clauses to do the sort as above.
            list.Sort((x, y) => (int)(x.Profession == y.Profession ? (x.Attribute == y.Attribute ? (x.Name.CompareTo(y.Name)) : x.Attribute - y.Attribute) : x.Profession - y.Profession));
        }

        static public string ReplaceFractions(this string cost)
        {
            return cost.Replace("1/4", "¼").Replace("1/2", "½").Replace("3/4", "¾").Replace("3/2", "1½");
        }
    }
}
