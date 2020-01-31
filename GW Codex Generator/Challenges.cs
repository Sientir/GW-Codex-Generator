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
		
            - Build theme: Fragility, Hexway, high pressure, orders, etc...
            - Rule: Skills must be on exactly two skill bars. So if you bring Glimmering Light on one character, it must be on exactly on other character's skill bar, as an example.

            - Add rules for the other challenges to the rules list? Or maybe make a separate thing for it. Oooh, it could be a checkbox!

            --- Moist Midget Suggestions ---
            Here some ideas for rules for your programm:
O- no Elite skills allowed
X- Elite have to be from secondary class
O- You have to take one hero less with you than the maximum is in the area
O- 50% of the Team have to do physical damage
O- Atleast one hero have to wield x weapon (bow, daggers, axe,...)
X- The Team can only take skills, that have 5 energy or less
X- 50% of the skills in each build have to be from the secondary class
O- Your Team have to distribute atleast 5 conditions

		*/


        static public List<Skill> MandatoryElites(int charactersCount, int rating_min, int rating_max, bool excludePrimaryAttributes)
        {
            List<Skill> pool = new List<Skill>();

            // If we're excluding primary attributes, do it here:
            if (excludePrimaryAttributes)
            {
                foreach (Skill skill in SkillDatabase.Data)
                {
                    if (skill.IsElite && skill.Rating >= rating_min && skill.Rating <= rating_max && skill.Attribute != Skill.Attributes.PvE_Only && Skill.PrimaryAttributes.Contains(skill.Attribute) == false)
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

        /// <summary>
        /// Elite level 0 = no elites | Elite level 1 = elites allowed | Elite level 2 = only elites
        /// </summary>
        /// <param name="eliteLevel"></param>
        /// <returns></returns>
        static public Skill RequiredSkill(int eliteLevel)
        {
            List<Skill> pool = new List<Skill>();
            Skill.Attributes[] ExcludedAttributes =
            {
                // These are a combination of primary attributes and weapon attributes. Beast Mastery is also included as a pseudo weapon attribute.
                Skill.Attributes.Axe_Mastery,
                Skill.Attributes.Beast_Mastery,
                Skill.Attributes.Critical_Strikes,
                Skill.Attributes.Dagger_Mastery,
                Skill.Attributes.Divine_Favor,
                Skill.Attributes.Energy_Storage,
                Skill.Attributes.Expertise,
                Skill.Attributes.Fast_Casting,
                Skill.Attributes.Hammer_Mastery,
                Skill.Attributes.Leadership,
                Skill.Attributes.Marksmanship,
                Skill.Attributes.Mysticism,
                Skill.Attributes.PvE_Only,
                Skill.Attributes.Scythe_Mastery,
                Skill.Attributes.Soul_Reaping,
                Skill.Attributes.Spawning_Power,
                Skill.Attributes.Spear_Mastery,
                Skill.Attributes.Strength,
                Skill.Attributes.Swordsmanship,
            };

            for (int i = 0; i < SkillDatabase.Data.Count; ++i)
            {
                // Skills that aren't added because they don't work well for this.
                // This includes: Attack skills outside of attack attributes and spirits, as just a couple of examples.
                bool skipSkill = false;
                switch (i)
                {
                    default: break;
                    #region Skipped Skills by ID
                    case 889: // Barbed Arrows
                        skipSkill = true;
                        break;
                    case 413: // Choking Gas
                        skipSkill = true;
                        break;
                    case 614: // Brambles
                        skipSkill = true;
                        break;
                    case 445: // Conflagration
                        skipSkill = true;
                        break;
                    case 759: // Equinox
                        skipSkill = true;
                        break;
                    case 654: // Famine
                        skipSkill = true;
                        break;
                    case 450: // Frozen Soil
                        skipSkill = true;
                        break;
                    case 444: // Greater Conflagration
                        skipSkill = true;
                        break;
                    case 410: // Ignite Arrows
                        skipSkill = true;
                        break;
                    case 407: // Incendiary Arrows
                        skipSkill = true;
                        break;
                    case 412: // Kindle Arrows
                        skipSkill = true;
                        break;
                    case 408: // Melandru's Arrows
                        skipSkill = true;
                        break;
                    case 455: // Muddy Terrain
                        skipSkill = true;
                        break;
                    case 454: // Nature's Renewal
                        skipSkill = true;
                        break;
                    case 551: // Pestilence
                        skipSkill = true;
                        break;
                    case 392: // Poison Arrow
                        skipSkill = true;
                        break;
                    case 453: // Quickening Zephyr
                        skipSkill = true;
                        break;
                    case 892: // Quicksand
                        skipSkill = true;
                        break;
                    case 1077: // Roaring Winds
                        skipSkill = true;
                        break;
                    case 760: // Tranquility
                        skipSkill = true;
                        break;
                    case 442: // Winnowing
                        skipSkill = true;
                        break;
                    case 441: // Winter
                        skipSkill = true;
                        break;
                    case 391: // Called Shot
                        skipSkill = true;
                        break;
                    case 385: // Dual Shot
                        skipSkill = true;
                        break;
                    case 1074: // Forked Arrow
                        skipSkill = true;
                        break;
                    case 1078: // Magebane Shot
                        skipSkill = true;
                        break;
                    case 386: // Quick Shot
                        skipSkill = true;
                        break;
                    case 377: // Deadly Riposte
                        skipSkill = true;
                        break;
                    case 313: // Desperation Blow
                        skipSkill = true;
                        break;
                    case 734: // Drunken Blow
                        skipSkill = true;
                        break;
                    case 376: // Riposte
                        skipSkill = true;
                        break;
                    case 367: // Shield Stance
                        skipSkill = true;
                        break;
                    case 314: // Thrill of Victory
                        skipSkill = true;
                        break;
                    case 315: // Distracting Blow
                        skipSkill = true;
                        break;
                    case 1249: // Distracting Strike
                        skipSkill = true;
                        break;
                    case 319: // Skull Crack
                        skipSkill = true;
                        break;
                    case 1250: // Symbolic Strike
                        skipSkill = true;
                        break;
                    case 311: // Wild Blow
                        skipSkill = true;
                        break;
                    case 670: // Impale
                        skipSkill = true;
                        break;
                    case 458: // Deadly Paradox
                        skipSkill = true;
                        break;
                    case 476: // Entangling Asp
                        skipSkill = true;
                        break;
                    case 632: // Mantis Touch
                        skipSkill = true;
                        break;
                    case 456: // Mark of Insecurity
                        skipSkill = true;
                        break;
                    case 1147: // Vampiric Assault
                        skipSkill = true;
                        break;
                    case 644: // Way of the Empty Palm
                        skipSkill = true;
                        break;
                    case 631: // Blinding Powder
                        skipSkill = true;
                        break;
                    case 1028: // Hidden Caltrops
                        skipSkill = true;
                        break;
                    case 635: // Way of the Lotus
                        skipSkill = true;
                        break;
                    case 1029: // Assault Enchantments
                        skipSkill = true;
                        break;
                    case 636: // Mark of Instability
                        skipSkill = true;
                        break;
                    case 594: // Recall
                        skipSkill = true;
                        break;
                    case 1030: // Wastrel's Collapse
                        skipSkill = true;
                        break;
                    case 861: // Healer's Covenant
                        skipSkill = true;
                        break;
                    case 724: // Healing Burst
                        skipSkill = true;
                        break;
                    case 303: // Healing Touch
                        skipSkill = true;
                        break;
                    case 296: // Rebirth
                        skipSkill = true;
                        break;
                    case 298: // Succor
                        skipSkill = true;
                        break;
                    case 102: // Awaken the Blood
                        skipSkill = true;
                        break;
                    case 495: // Cultist's Fervor
                        skipSkill = true;
                        break;
                    case 129: // Dark Bond
                        skipSkill = true;
                        break;
                    case 545: // Order of Apostasy
                        skipSkill = true;
                        break;
                    case 111: // Blood of the Master
                        skipSkill = true;
                        break;
                    case 107: // Dark Aura
                        skipSkill = true;
                        break;
                    case 830: // Feast for the Dead
                        skipSkill = true;
                        break;
                    case 130: // Infuse Condition
                        skipSkill = true;
                        break;
                    case 831: // Jagged Bones
                        skipSkill = true;
                        break;
                    case 828: // Order of Undeath
                        skipSkill = true;
                        break;
                    case 829: // Putrid Flesh
                        skipSkill = true;
                        break;
                    case 143: // Taste of Death
                        skipSkill = true;
                        break;
                    case 79: // Verata's Aura
                        skipSkill = true;
                        break;
                    case 81: // Verata's Sacrifice
                        skipSkill = true;
                        break;
                    case 28: // Illusionary Weaponry
                        skipSkill = true;
                        break;
                    case 36: // Ether Lord
                        skipSkill = true;
                        break;
                    case 11: // Mantra of Inscriptions
                        skipSkill = true;
                        break;
                    case 10: // Mantra of Persistence
                        skipSkill = true;
                        break;
                    case 14: // Mantra of Signets
                        skipSkill = true;
                        break;
                    case 56: // Signet of Humility
                        skipSkill = true;
                        break;
                    case 66: // Arcane Echo
                        skipSkill = true;
                        break;
                    case 215: // Air Attunement
                        skipSkill = true;
                        break;
                    case 528: // Arc Lightning
                        skipSkill = true;
                        break;
                    case 211: // Conjure Lightning
                        skipSkill = true;
                        break;
                    case 1187: // Shell Shock
                        skipSkill = true;
                        break;
                    case 705: // Shock Arrow
                        skipSkill = true;
                        break;
                    case 159: // Earth Attunement
                        skipSkill = true;
                        break;
                    case 1047: // Glowstone
                        skipSkill = true;
                        break;
                    case 206: // Iron Mist
                        skipSkill = true;
                        break;
                    case 208: // Obsidian Flesh
                        skipSkill = true;
                        break;
                    case 172: // Conjure Flame
                        skipSkill = true;
                        break;
                    case 1049: // Elemental Flame
                        skipSkill = true;
                        break;
                    case 174: // Fire Attunement
                        skipSkill = true;
                        break;
                    case 853: // Glowing Gaze
                        skipSkill = true;
                        break;
                    case 197: // Conjure Frost
                        skipSkill = true;
                        break;
                    case 1247: // Glowing Ice
                        skipSkill = true;
                        break;
                    case 198: // Water Attunement
                        skipSkill = true;
                        break;
                    case 188: // Glyph of Elemental Power
                        skipSkill = true;
                        break;
                    case 191: // Glyph of Concentration
                        skipSkill = true;
                        break;
                    case 716: // Glyph of Essence
                        skipSkill = true;
                        break;
                    case 192: // Glyph of Sacrifice
                        skipSkill = true;
                        break;
                    case 1260: // Agony
                        skipSkill = true;
                        break;
                    case 794: // Bloodsong
                        skipSkill = true;
                        break;
                    case 591: // Destruction
                        skipSkill = true;
                        break;
                    case 1086: // Gaze of Fury
                        skipSkill = true;
                        break;
                    case 784: // Signet of Spirits
                        skipSkill = true;
                        break;
                    case 773: // Spirit Siphon
                        skipSkill = true;
                        break;
                    case 1201: // Weapon of Aggression
                        skipSkill = true;
                        break;
                    case 1096: // Anguish
                        skipSkill = true;
                        break;
                    case 777: // Armor of Unfeeling
                        skipSkill = true;
                        break;
                    case 593: // Disenchantment
                        skipSkill = true;
                        break;
                    case 790: // Displacement
                        skipSkill = true;
                        break;
                    case 592: // Dissonance
                        skipSkill = true;
                        break;
                    case 793: // Earthbind
                        skipSkill = true;
                        break;
                    case 789: // Pain
                        skipSkill = true;
                        break;
                    case 466: // Mighty Was Vorizun
                        skipSkill = true;
                        break;
                    case 629: // Restoration
                        skipSkill = true;
                        break;
                    case 552: // Shadowsong
                        skipSkill = true;
                        break;
                    case 640: // Shelter
                        skipSkill = true;
                        break;
                    case 1093: // Signet of Ghostly Might
                        skipSkill = true;
                        break;
                    case 805: // Soothing
                        skipSkill = true;
                        break;
                    case 583: // Union
                        skipSkill = true;
                        break;
                    case 795: // Wanderlust
                        skipSkill = true;
                        break;
                    case 792: // Life
                        skipSkill = true;
                        break;
                    case 791: // Preservation
                        skipSkill = true;
                        break;
                    case 1098: // Recovery
                        skipSkill = true;
                        break;
                    case 639: // Recuperation
                        skipSkill = true;
                        break;
                    case 1259: // Rejuvenation
                        skipSkill = true;
                        break;
                    case 1258: // Spiritleech Aura
                        skipSkill = true;
                        break;
                    case 584: // Tranquil Was Tanasen
                        skipSkill = true;
                        break;
                    case 1083: // Vocal Was Sogolon
                        skipSkill = true;
                        break;
                    case 769: // Draw Spirit
                        skipSkill = true;
                        break;
                    case 1126: // Signet of Aggression
                        skipSkill = true;
                        break;
                    case 1110: // Ebon Dust Aura
                        skipSkill = true;
                        break;
                    case 925: // Sand Shards
                        skipSkill = true;
                        break;
                    case 1109: // Vow of Strength
                        skipSkill = true;
                        break;
                    case 1173: // Grenth's Aura
                        skipSkill = true;
                        break;
                    case 1106: // Grenth's Grasp
                        skipSkill = true;
                        break;
                    case 1255: // Signet of Mystic Speed
                        skipSkill = true;
                        break;
                    case 947: // Winds of Disenchantment
                        skipSkill = true;
                        break;
                        #endregion
                }

                if (skipSkill) continue;

                // Handle the elite marker:
                if (eliteLevel == 0 && SkillDatabase.Data[i].IsElite) continue; // No elites allowed!
                if (eliteLevel == 2 && SkillDatabase.Data[i].IsElite == false) continue; // Elites required!
                // Obviously, eliteLevel 1, which is elites allowed but not required, doesn't need a check against the elite status of the skill.

                // Next, exclude certain attributes...
                if (ExcludedAttributes.Contains(SkillDatabase.Data[i].Attribute))
                {
                    continue;
                }

                pool.Add(SkillDatabase.Data[i]);
            }

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

        static public string[][] Restrictions =
            {
                new string[]{"• No profession may be duplicated in primaries/secondaries. (You may have a W/Mo and a Mo/W, but not a W/Mo and W/R or Mo/W and R/W.)" },
                new string[]{"• No skill duplication. (If a skill is used on one character, it may not be used on another character.)", "• No skill duplication. (If a skill is used on one character, it may not be used on another character.)", "• Skills must be used on exactly two characters." },
                new string[]{"• Attribute Lines must be unique. (If one character has at least 1 rank in an attribute line, no other character may put any ranks into that attribute line.)" },
                new string[]{"• Pokemon—Skill Bars are limited to 4 skills." },
                new string[]{"• Elite Skills must be from primary profession.", "• Elite Skills must be from secondary profession.", "• Elite Skills must be from secondary profession.", "• Elite Skills must be from secondary profession.", "• You're not allowed to use elite skills.", "• You're not allowed to use elite skills." },
                new string[]{"• Every party member must bring an interrupt."},
                new string[]{"• Every party member must bring a healing skill."},
                new string[]{"• Party size -1. (If the area has a party size of 8, treat it as 7, etc.)"},
                new string[]{"• Your party must be capable of inflicting at least 5 different conditions."},
                new string[]{"• Your party must consist of at least 50% physical attackers."},
                new string[]{"• Your party must be capable of applying at least 5 different hexes that each last at least 10 seconds."},
                new string[]{"• Characters in your party must leave at least half of their attribute points unused. (100 unused attribute points for a character with maximum attribute points.)"},
                new string[]{"• At least one character must wield a spear. (If this is a repeat, then at least two characters must use this weapon.)", "• At least one character must wield daggers. (If this is a repeat, then at least two characters must use this weapon.)", "• At least one character must wield a sword. (If this is a repeat, then at least two characters must use this weapon.)", "• At least one character must wield a hammer. (If this is a repeat, then at least two characters must use this weapon.)", "• At least one character must wield a axe. (If this is a repeat, then at least two characters must use this weapon.)", "• At least one character must wield a scythe. (If this is a repeat, then at least two characters must use this weapon.)", "• At least one character must wield a bow. (If this is a repeat, then at least two characters must use this weapon.)"},
                new string[]{"• At least one character must wield a spear. (If this is a repeat, then at least two characters must use this weapon.)", "• At least one character must wield daggers. (If this is a repeat, then at least two characters must use this weapon.)", "• At least one character must wield a sword. (If this is a repeat, then at least two characters must use this weapon.)", "• At least one character must wield a hammer. (If this is a repeat, then at least two characters must use this weapon.)", "• At least one character must wield a axe. (If this is a repeat, then at least two characters must use this weapon.)", "• At least one character must wield a scythe. (If this is a repeat, then at least two characters must use this weapon.)", "• At least one character must wield a bow. (If this is a repeat, then at least two characters must use this weapon.)"},
                new string[] {"• Your party may not use skills that cost more than 5 energy. (Unless necessary for another part of a challenge, obviously!)"},
                new string[] {"• Bring at least three traps."},
                new string[] {"• Every character must bring at least one Adrenaline skill."},
                new string[] {"• Your party must bring at least one pet."},
                // Skill split between primary and secondary profession...
                new string[] {
                    "• Your party's skills must be split 1 primary and 7 secondary (1p-3s if Pokemon rule is present).",
                    "• Your party's skills must be split 2 primary and 6 secondary (1p-3s if Pokemon rule is present).",
                    "• Your party's skills must be split 3 primary and 5 secondary (2p-2s if Pokemon rule is present).",
                    "• Your party's skills must be split 3 primary and 5 secondary (2p-2s if Pokemon rule is present).",
                    "• Your party's skills must be split 4 primary and 4 secondary (2p-2s if Pokemon rule is present).",
                    "• Your party's skills must be split 4 primary and 4 secondary (2p-2s if Pokemon rule is present).",
                    "• Your party's skills must be split 4 primary and 4 secondary (2p-2s if Pokemon rule is present).",
                    "• Your party's skills must be split 5 primary and 3 secondary (2p-2s if Pokemon rule is present).",
                    "• Your party's skills must be split 5 primary and 3 secondary (2p-2s if Pokemon rule is present).",
                    "• Your party's skills must be split 6 primary and 2 secondary (3p-1s if Pokemon rule is present).",
                    "• Your party's skills must be split 7 primary and 1 secondary (3p-1s if Pokemon rule is present)."
                },
                new string[] {"• Your party must include at least one henchman.", "• Your party must include at least one henchman.", "• Your party must include at least two henchmen."},
                new string[] { "• Any given character cannot use more than 3 skills from an attribute line.", "• Any given character cannot use more than 3 skills from an attribute line.", "• For any given attribute line (no attribute counts as an attribute for this), all characters can combined use up to six skills. (One character could use 2 and another use 4, but a third could not then use even one skill from that attribute line.)"}
            };
        static public string[] BooleanRestrictions(int count)
        {
            if (count <= 0) return new string[0];

            // Well, we want all of them, so get adding!
            if (count >= Restrictions.Length)
            {
                string[] result = new string[Restrictions.Length];
                for (int i = 0; i < result.Length; ++i)
                {
                    result[i] = Restrictions[i].GetRandom();
                }
                return result;
            }

            // Create and fill the bag:
            List<int> bag = new List<int>(Restrictions.Length);
            for (int i = 0; i < Restrictions.Length; ++i) { bag.Add(i); }

            // Make an array to hold this stuff:
            string[] output = new string[count];
            for (int i = 0; i < count; ++i)
            {
                // This is a grab bag, but it isn't a skill grab bag, so I'm just writing it out:
                int index = SkillDatabase.RNG.Next(0, bag.Count);
                int id = bag[index];
                bag.RemoveAt(index);

                // Get our specified restriction!
                output[i] = Restrictions[id].GetRandom();
            }

            return output;
        }

        #region Unused primary and secondary skills split code...
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
        #endregion

        static internal string[] RandomBuildTemplates(int count, bool allowDuplicates)
        {
            List<string> templates = SkillDatabase.TemplatesDatabase;
            string[] result = new string[count];
            if (allowDuplicates)
            {
                for (int i = 0; i < count; ++i)
                {
                    result[i] = templates[SkillDatabase.RNG.Next(0, templates.Count)];
                }
            }
            else
            {
                List<int> indices = new List<int>(templates.Count);
                for (int i = 0; i < templates.Count; ++i) indices.Add(i);

                if (indices.Count < count) count = indices.Count; // Limiter to prevent crashing.

                for (int i = 0; i < count; ++i)
                {
                    int index = SkillDatabase.RNG.Next(0, indices.Count);
                    result[i] = templates[indices[index]];
                    indices.RemoveAt(index); // Grab-bag style!
                }
            }

            return result;
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

        static public T GetRandom<T>(this T[] array)
        {
            if (array.Length == 1) return array[0];
            return array[SkillDatabase.RNG.Next(0, array.Length)];
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
