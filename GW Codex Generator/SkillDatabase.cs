using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GW_Codex_Generator
{
    static class SkillDatabase
    {
        /*
		Some ideas:
		- Randomized builds (team and character). These would be full builds, i.e. attribute spread, professions, and skill bar. Should be intelligent (so picking the spread carefully, that sort of thing). Should maybe be able to balance among several different types of restrictions, such as no skills below a rating or no skills above, or otherwise using build restriction ideas listed below.
		
		Build Restrictions: (These would be a separate thing that could be rolled to add some constraints to making a team build.)
		- Require certain numbers of skills from primary and secondary profession. (For example, 4/4 split, 3/5, etc.)
		- No profession duplication among primaries and secondaries, separately. That is, if you have a W/Mo, you can't have another primary warrior or secondary monk, but you can have a primary monk and a secondary warrior.
		- Skills must be unique. That is, no duplicates across the team. If one character is using, say, Healing Burst, no other character can use it.
		- Attribute lines must be unique across the team. So, for example, if a character is using Healing Prayers, no other character may use healing prayers.
		- Limited numbers of skills allowed to be used from an attribute line. So, perhaps a character can't use more than 3 skills from healing prayers, or perhaps the entire team can't have more than 6 skills from Restoration Magic.
		- Required skills. That is, a set of skills that have to be used. Could be one per character, or possibly split however I want across characters. Could include elites or not, PvE only skills, etc. I think 8 required elites would probably be pretty nasty to have to work with, but also potentially interesting!
		- A skill that all characters are required to bring. For example, everyone has to bring Healing Whisper or Tease. Obviously, this is way more restrictive with an elite.
		- Elite must be from (Primary/Secondary) profession. I think from Secondary is a better, more interesting restriction.
		- Pool is list of skills I can't use (instead of pool of skills I can only use).
		- Pokemon (skill bars are limited to 4 skills).
		- Elites that have to be used equal to the number of open character slots. Probably should be able to specify rating range for these skills!
		
		*/

        public static List<Skill> Data = new List<Skill>();
        public static Random RNG = new Random();

        public static string TemplateDeckName = "None";
        public static List<string> TemplateDeck = new List<string>();

        public static string[] RarityLabels = { "Common", "Uncommon", "Rare" };

        private static List<Skill>[][] SkillsByProfession;

        private static List<Skill>[][] SkillsByCampaign;

        public const int WithPvEOnly = 0;
        public const int WithoutPvEOnly = 1;

        public const int Core = 1;
        public const int Prophecies = 2;
        public const int Factions = 4;
        public const int Nightfall = 8;
        public const int EyeOfTheNorth = 16;

        // Utility lists:
        static public List<Skill> Standard = new List<Skill>();
        static public List<Skill> LeadAttacks = new List<Skill>();
        static public List<Skill> OffHandAttacks = new List<Skill>();
        static public List<Skill> DualAttacks = new List<Skill>();

        #region SkillNamesByCampaign
        static public string[] CoreSkillNames = {
            "\"Charge!\"", "\"For Great Justice!\"", "\"Shields Up!\"", "\"Watch Yourself!\"",
            "Axe Rake", "Balanced Stance", "Battle Rage",
            "Berserker Stance",
            "Bull's Strike",
            "Cleave",
            "Counter Blow",
            "Cyclone Axe",
            "Defensive Stance",
            "Devastating Hammer",
            "Dismember",
            "Disrupting Chop",
            "Distracting Blow",
            "Endure Pain",
            "Executioner's Strike",
            "Final Thrust",
            "Frenzy",
            "Gash",
            "Hammer Bash",
            "Hamstring",
            "Healing Signet",
            "Heavy Blow",
            "Hundred Blades",
            "Irresistible Blow",
            "Mighty Blow",
            "Power Attack",
            "Savage Slash",
            "Seeking Blade",
            "Sever Artery",
            "Shield Bash",
            "Sprint",
            "Staggering Blow",
            "Swift Chop",
            "Thrill of Victory",
            "Warrior's Cunning",
            "Wild Blow",
            "Antidote Signet",
            "Apply Poison",
            "Barbed Trap",
            "Barrage",
            "Call of Haste",
            "Called Shot",
            "Charm Animal",
            "Choking Gas",
            "Comfort Animal",
            "Concussion Shot",
            "Debilitating Shot",
            "Determined Shot",
            "Disrupting Lunge",
            "Distracting Shot",
            "Dual Shot",
            "Energizing Wind",
            "Escape",
            "Favorable Winds",
            "Ferocious Strike",
            "Flame Trap",
            "Hunter's Shot",
            "Kindle Arrows",
            "Lightning Reflexes",
            "Maiming Strike",
            "Nature's Renewal",
            "Pin Down",
            "Power Shot",
            "Precision Shot",
            "Primal Echoes",
            "Quick Shot",
            "Quickening Zephyr",
            "Read the Wind",
            "Savage Shot",
            "Scavenger Strike",
            "Spike Trap",
            "Storm Chaser",
            "Throw Dirt",
            "Troll Unguent",
            "Whirling Defense",
            "Winnowing",
            "Aegis",
            "Balthazar's Aura",
            "Bane Signet",
            "Banish",
            "Blessed Signet",
            "Convert Hexes",
            "Divine Boon",
            "Divine Intervention",
            "Draw Conditions",
            "Guardian",
            "Heal Party",
            "Healing Breeze",
            "Healing Seed",
            "Healing Touch",
            "Holy Veil",
            "Infuse Health",
            "Judge's Insight",
            "Life Bond",
            "Martyr",
            "Mend Ailment",
            "Mending",
            "Orison of Healing",
            "Protective Spirit",
            "Purge Conditions",
            "Purge Signet",
            "Rebirth",
            "Remove Hex",
            "Resurrect",
            "Retribution",
            "Reversal of Fortune",
            "Scourge Healing",
            "Shield of Regeneration",
            "Shielding Hands",
            "Signet of Devotion",
            "Signet of Judgment",
            "Smite Hex",
            "Spell Breaker",
            "Strength of Honor",
            "Word of Healing",
            "Zealot's Fire",
            "Animate Bone Fiend",
            "Animate Bone Minions",
            "Barbed Signet",
            "Barbs",
            "Blood is Power",
            "Blood of the Master",
            "Chilblains",
            "Dark Aura",
            "Dark Bond",
            "Dark Pact",
            "Death Nova",
            "Deathly Swarm",
            "Defile Flesh",
            "Demonic Flesh",
            "Enfeebling Blood",
            "Faintheartedness",
            "Grenth's Balance",
            "Life Siphon",
            "Lingering Curse",
            "Mark of Pain",
            "Order of Pain",
            "Parasitic Bond",
            "Plague Signet",
            "Plague Touch",
            "Putrid Explosion",
            "Rend Enchantments",
            "Rigor Mortis",
            "Rotting Flesh",
            "Signet of Agony",
            "Soul Feast",
            "Strip Enchantment",
            "Suffering",
            "Tainted Flesh",
            "Taste of Death",
            "Unholy Feast",
            "Vampiric Gaze",
            "Vile Touch",
            "Weaken Armor",
            "Well of Blood",
            "Well of the Profane",
            "Arcane Conundrum",
            "Arcane Echo",
            "Arcane Mimicry",
            "Backfire",
            "Clumsiness",
            "Conjure Phantasm",
            "Crippling Anguish",
            "Cry of Frustration",
            "Diversion",
            "Drain Enchantment",
            "Echo",
            "Empathy",
            "Energy Burn",
            "Energy Drain",
            "Energy Surge",
            "Energy Tap",
            "Epidemic",
            "Ether Feast",
            "Fragility",
            "Hex Breaker",
            "Ignorance",
            "Illusion of Haste",
            "Imagined Burden",
            "Leech Signet",
            "Mantra of Earth",
            "Mantra of Flame",
            "Mantra of Frost",
            "Mantra of Inscriptions",
            "Mantra of Lightning",
            "Mantra of Persistence",
            "Mantra of Recovery",
            "Mantra of Resolve",
            "Mind Wrack",
            "Power Drain",
            "Power Spike",
            "Shatter Enchantment",
            "Shatter Hex",
            "Signet of Humility",
            "Soothing Images",
            "Spirit Shackles",
            "Aftershock",
            "Air Attunement",
            "Armor of Earth",
            "Aura of Restoration",
            "Blinding Flash",
            "Blurred Vision", "Conjure Flame", "Conjure Frost", "Conjure Lightning", "Deep Freeze", "Earth Attunement", "Elemental Attunement",
            "Enervating Charge", "Fire Attunement", "Fire Storm", "Fireball", "Flare", "Gale", "Glyph of Elemental Power", "Glyph of Lesser Energy", "Ice Spear",
            "Ice Spikes", "Immolate", "Inferno", "Lightning Orb", "Lightning Strike", "Lightning Surge", "Lightning Touch", "Maelstrom", "Meteor", "Mind Burn",
            "Mind Freeze", "Obsidian Flame", "Obsidian Flesh",
            "Rust", "Stone Daggers", "Stoning", "Ward Against Foes", "Ward Against Melee", "Water Attunement", "Resurrection Signet"
        };
        static public string[] PropheciesSkillNames =
        {
            "\"Fear Me!\"", "\"I Will Avenge You!\"", "\"I Will Survive!\"", "\"To the Limit!\"", "\"Victory Is Mine!\"", "Axe Twist", "Backbreaker",
            "Belly Smash", "Bonetti's Defense", "Bull's Charge", "Crude Swing", "Crushing Blow", "Deadly Riposte", "Deflect Arrows", "Defy Pain",
            "Desperation Blow", "Disciplined Stance", "Dolyak Signet", "Dwarven Battle Stance", "Earth Shaker", "Eviscerate", "Flourish", "Flurry",
            "Galrath Slash", "Gladiator's Defense", "Griffon's Sweep", "Penetrating Blow", "Protector's Strike", "Pure Strike", "Riposte", "Rush", "Shield Stance", "Skull Crack", "Warrior's Endurance",
            "Wary Stance", "Bestial Pounce", "Brutal Strike", "Call of Protection", "Crippling Shot", "Dodge", "Dryder's Defenses", "Dust Trap",
            "Edge of Extinction", "Feral Lunge", "Fertile Season", "Frozen Soil", "Greater Conflagration", "Healing Spring", "Ignite Arrows", "Incendiary Arrows",
            "Marksman's Wager", "Melandru's Arrows", "Melandru's Assault", "Melandru's Resilience", "Muddy Terrain", "Oath Shot", "Otyugh's Cry", "Penetrating Attack",
            "Point Blank Shot", "Poison Arrow", "Practiced Stance", "Predator's Pounce", "Predatory Season", "Punishing Shot", "Revive Animal", "Serpent's Quickness", "Symbiosis",
            "Symbiotic Bond", "Tiger's Fury", "Winter", "Amity", "Aura of Faith", "Balthazar's Spirit", "Blessed Aura", "Contemplation of Purity", "Divine Healing",
            "Divine Spirit", "Dwayna's Kiss", "Essence Bond", "Heal Area", "Heal Other", "Healing Hands", "Holy Strike", "Holy Wrath", "Life Attunement", "Life Barrier",
            "Light of Dwayna", "Live Vicariously", "Mark of Protection", "Mend Condition", "Pacifism", "Peace and Harmony", "Protective Bond", "Restore Condition", "Restore Life",
            "Scourge Sacrifice", "Shield of Deflection", "Shield of Judgment", "Smite", "Succor", "Symbol of Wrath", "Unyielding Aura", "Vengeance", "Vigorous Spirit", "Vital Blessing",
            "Watchful Spirit", "Animate Bone Horror", "Aura of the Lich", "Awaken the Blood", "Blood Renewal", "Blood Ritual", "Consume Corpse", "Dark Fury", "Deathly Chill",
            "Desecrate Enchantments", "Enfeeble", "Feast of Corruption", "Infuse Condition", "Insidious Parasite", "Life Transfer", "Malaise", "Malign Intervention", "Mark of Subversion",
            "Necrotic Traversal", "Offering of Blood", "Order of the Vampire", "Plague Sending", "Price of Failure", "Shadow Strike", "Shadow of Fear", "Soul Barbs", "Soul Leech",
            "Spinal Shivers", "Spiteful Spirit", "Touch of Agony", "Vampiric Touch", "Verata's Aura", "Verata's Gaze", "Verata's Sacrifice", "Virulence", "Well of Power", "Well of Suffering", "Wither",
            "Arcane Thievery", "Blackout", "Channeling", "Chaos Storm", "Distortion", "Elemental Resistance", "Ether Lord", "Ethereal Burden", "Fevered Dreams", "Guilt", "Illusion of Weakness",
            "Illusionary Weaponry", "Ineptitude", "Inspired Enchantment", "Inspired Hex", "Keystone Signet", "Mantra of Concentration", "Mantra of Recall", "Mantra of Signets", "Migraine",
            "Panic", "Phantom Pain", "Physical Resistance", "Power Block", "Power Leak", "Shame", "Shatter Delusions", "Signet of Midnight", "Signet of Weariness", "Spirit of Failure", "Sympathetic Visage",
            "Wastrel's Worry", "Armor of Frost", "Armor of Mist", "Chain Lightning", "Crystal Wave", "Earthquake", "Eruption", "Ether Prodigy", "Ether Renewal", "Flame Burst",
            "Frozen Burst", "Glimmering Mark", "Glyph of Concentration", "Glyph of Energy", "Glyph of Renewal", "Glyph of Sacrifice", "Grasping Earth", "Ice Prison", "Incendiary Bonds", "Iron Mist",
            "Kinetic Armor", "Lava Font", "Lightning Javelin", "Magnetic Aura", "Mark of Rodgort", "Meteor Shower", "Mind Shock", "Mist Form", "Phoenix", "Rodgort's Invocation",
            "Searing Heat", "Shard Storm", "Shock", "Swirling Aura", "Thunderclap", "Ward Against Elements", "Ward Against Harm", "Water Trident", "Whirlwind", "Windborne Speed"
        };
        static public string[] FactionsSkillNames =
        {
            "\"Coward!\"", "\"None Shall Pass!\"", "\"On Your Knees!\"", "\"Retreat!\"", "\"Save Yourselves!\" (Luxon)", "\"Save Yourselves!\" (Kurzick)", "\"You Will Die!\"", "Auspicious Blow", "Auspicious Parry", "Dragon Slash",
            "Drunken Blow", "Enraged Smash", "Fierce Blow", "Forceful Blow", "Furious Axe", "Jaizhenju Strike", "Lacerating Chop", "Leviathan's Sweep", "Penetrating Chop", "Primal Rage",
            "Protector's Defense", "Quivering Blade", "Renewing Smash", "Shove", "Signet of Strength", "Silverwing Slash", "Standing Slash", "Sun and Moon Slash", "Tiger Stance", "Triple Chop",
            "Whirling Axe", "Yeti Smash", "Archer's Signet", "Bestial Fury", "Bestial Mauling", "Brambles", "Broad Head Arrow", "Conflagration", "Enraged Lunge", "Equinox", "Famine", "Focused Shot",
            "Glass Arrows", "Heal as One", "Lacerate", "Marauder's Shot", "Melandru's Shot", "Needling Shot", "Poisonous Bite", "Pounce", "Predatory Bond", "Run as One", "Savage Pounce",
            "Seeking Arrows", "Snare", "Splinter Shot", "Sundering Attack", "Tranquility", "Trapper's Focus", "Triple Shot (Luxon)", "Triple Shot (Kurzick)", "Viper's Nest", "Zojun's Haste", "Zojun's Shot", "Air of Enchantment",
            "Blessed Light", "Boon Signet", "Deny Hexes", "Dwayna's Sorrow", "Empathic Removal", "Ethereal Light", "Extinguish", "Gift of Health", "Healing Burst", "Healing Light", "Healing Whisper",
            "Heaven's Delight", "Jamei's Gaze", "Karei's Healing Circle", "Kirin's Wrath", "Life Sheath", "Ray of Judgment", "Release Enchantments", "Resurrection Chant", "Reverse Hex",
            "Selfless Spirit (Luxon)", "Selfless Spirit (Kurzick)","Shield Guardian", "Signet of Rage", "Signet of Rejuvenation", "Spear of Light", "Spell Shield", "Spirit Bond", "Stonesoul Strike", "Withdraw Hexes", "Word of Censure",
            "Animate Flesh Golem", "Animate Vampiric Horror", "Bitter Chill", "Blood Bond", "Blood Drinker", "Cultist's Fervor", "Defile Enchantments", "Discord", "Enfeebling Touch",
            "Fetid Ground", "Gaze of Contempt", "Icy Veins", "Jaundiced Gaze", "Lifebane Strike", "Oppressive Gaze", "Order of Apostasy", "Reckless Haste", "Rising Bile", "Shivers of Dread",
            "Signet of Corruption (Luxon)", "Signet of Corruption (Kurzick)","Soul Bind", "Spoil Victor", "Taste of Pain", "Vampiric Bite", "Vampiric Spirit", "Vampiric Swarm", "Vile Miasma", "Wail of Doom", "Wallow's Bite",
            "Weaken Knees", "Well of Weariness", "Accumulated Pain", "Ancestor's Visage", "Arcane Languor", "Arcane Larceny", "Auspicious Incantation", "Complicate", "Conjure Nightmare",
            "Ether Nightmare (Luxon)", "Ether Nightmare (Kurzick)","Ether Signet", "Expel Hexes", "Feedback", "Hex Eater Signet", "Illusion of Pain", "Images of Remorse", "Kitah's Burden", "Lyssa's Aura", "Lyssa's Balance", "Overload",
            "Power Leech", "Power Return", "Psychic Distraction", "Psychic Instability", "Recurring Insecurity", "Revealed Enchantment", "Revealed Hex", "Shared Burden", "Shatter Storm",
            "Signet of Disenchantment", "Signet of Disruption", "Stolen Speed", "Unnatural Signet", "Arc Lightning", "Ash Blast", "Bed of Coals", "Breath of Fire", "Burning Speed", "Churning Earth",
            "Double Dragon", "Dragon's Stomp", "Elemental Lord (Luxon)", "Elemental Lord (Kurzick)", "Energy Boon", "Glyph of Essence", "Gust", "Icy Prism", "Lava Arrows", "Lightning Hammer", "Mirror of Ice", "Ride the Lightning",
            "Second Wind", "Shatterstone", "Shock Arrow", "Shockwave", "Sliver Armor", "Smoldering Embers", "Star Burst", "Teinai's Crystals", "Teinai's Heat", "Teinai's Prison", "Teinai's Wind",
            "Unsteady Ground", "Vapor Blade", "Ward of Stability", "Assassin's Promise", "Aura of Displacement", "Beguiling Haze", "Black Lotus Strike", "Black Mantis Thrust", "Blades of Steel",
            "Blinding Powder", "Caltrops", "Crippling Dagger", "Critical Defenses", "Critical Eye", "Critical Strike", "Dancing Daggers", "Dark Apostasy", "Dark Escape", "Dark Prison", "Dash",
            "Death Blossom", "Death's Charge", "Desperate Strike", "Disrupting Stab", "Enduring Toxin", "Entangling Asp", "Exhausting Assault", "Expose Defenses", "Expunge Enchantments",
            "Falling Spider", "Flashing Blades", "Fox Fangs", "Golden Lotus Strike", "Golden Phoenix Strike", "Heart of Shadow", "Horns of the Ox", "Impale", "Iron Palm", "Jagged Strike",
            "Jungle Strike", "Leaping Mantis Sting", "Locust's Fury", "Mantis Touch", "Mark of Death", "Mark of Instability", "Mirrored Stance", "Moebius Strike", "Nine Tail Strike", "Palm Strike",
            "Recall", "Repeating Strike", "Return", "Scorpion Wire", "Seeping Wound", "Shadow Form", "Shadow Refuge", "Shadow Sanctuary (Luxon)", "Shadow Sanctuary (Kurzick)", "Shadow Shroud", "Shadow of Haste", "Shadowy Burden",
            "Shameful Fear", "Sharpen Daggers", "Shroud of Distress", "Shroud of Silence", "Signet of Malice", "Signet of Shadows", "Siphon Speed", "Siphon Strength", "Spirit Walk", "Temple Strike",
            "Twisting Fangs", "Unseen Fury", "Unsuspecting Strike", "Viper's Defense", "Way of Perfection", "Way of the Empty Palm", "Way of the Fox", "Way of the Lotus", "Wild Strike",
            "Ancestors' Rage", "Anguished Was Lingwah", "Armor of Unfeeling", "Attuned Was Songkai", "Binding Chains", "Blind Was Mingson", "Bloodsong", "Boon of Creation", "Brutal Weapon",
            "Channeled Strike", "Clamor of Souls", "Consume Soul", "Cruel Was Daoshen", "Defiant Was Xinrae", "Destruction", "Disenchantment", "Displacement", "Dissonance", "Doom", "Draw Spirit",
            "Dulled Weapon", "Earthbind", "Essence Strike", "Explosive Growth", "Feast of Souls", "Flesh of My Flesh", "Gaze from Beyond", "Generous Was Tsungrai", "Ghostly Haste", "Grasping Was Kuurong",
            "Guided Weapon", "Lamentation", "Life", "Lively Was Naomei", "Mend Body and Soul", "Mighty Was Vorizun", "Nightmare Weapon", "Pain", "Painful Bond", "Preservation", "Protective Was Kaolai",
            "Recuperation", "Resilient Was Xiko", "Resilient Weapon", "Restoration", "Ritual Lord", "Rupture Soul", "Shadowsong", "Shelter", "Signet of Creation", "Signet of Spirits", "Soothing",
            "Soothing Memories", "Soul Twisting", "Spirit Boon Strike", "Spirit Burn", "Spirit Channeling", "Spirit Light", "Spirit Light Weapon", "Spirit Rift", "Spirit Siphon", "Spirit Transfer",
            "Spirit to Flesh", "Splinter Weapon", "Summon Spirits (Luxon)", "Summon Spirits (Kurzick)", "Tranquil Was Tanasen", "Union", "Vengeful Was Khanhei", "Vengeful Weapon", "Vital Weapon", "Wailing Weapon", "Wanderlust",
            "Weapon of Quickening", "Weapon of Warding", "Weapon of Shadow", "Wielder's Boon", "Spear of Fury (Luxon)", "Spear of Fury (Kurzick)", "Aura of Holy Might (Luxon)", "Aura of Holy Might (Kurzick)"
        };
        static public string[] NightfallSkillNames =
        {
            "\"You're All Alone!\"", "Agonizing Chop", "Barbarous Slice", "Burst of Aggression", "Charging Strike", "Counterattack", "Crippling Slash", "Critical Chop", "Decapitate",
            "Enraging Charge", "Flail", "Frenzied Defense", "Headbutt", "Lion's Comfort", "Magehunter Strike", "Magehunter's Smash", "Mokele Smash", "Overbearing Smash", "Rage of the Ntouka",
            "Signet of Stamina", "Soldier's Defense", "Soldier's Stance", "Soldier's Strike", "Steady Stance", "Steelfang Slash", "Whirlwind Attack", "Arcing Shot", "Barbed Arrows",
            "Burning Arrow", "Crossfire", "Disrupting Accuracy", "Expert's Dexterity", "Forked Arrow", "Heket's Rampage", "Infuriating Heat", "Keen Arrow", "Magebane Shot", "Natural Stride",
            "Never Rampage Alone", "Pestilence", "Prepared Shot", "Quicksand", "Rampage as One", "Roaring Winds", "Scavenger's Focus", "Screaming Shot", "Smoke Trap", "Storm's Embrace",
            "Strike as One", "Toxicity", "Trapper's Speed", "Tripwire", "Balthazar's Pendulum", "Defender's Zeal", "Dismiss Condition", "Divert Hexes", "Glimmer of Light", "Healer's Boon",
            "Healer's Covenant", "Healing Ring", "Holy Haste", "Judge's Intervention", "Light of Deliverance", "Mending Touch", "Pensive Guardian", "Renew Life", "Restful Breeze",
            "Reversal of Damage", "Scourge Enchantment", "Scribe's Insight", "Seed of Life", "Shield of Absorption", "Signet of Mystic Wrath", "Signet of Removal", "Supportive Spirit",
            "Watchful Healing", "Words of Comfort", "Zealous Benediction", "Animate Shambling Horror", "Blood of the Aggressor", "Contagion", "Corrupt Enchantment", "Depravity",
            "Envenom Enchantments", "Feast for the Dead", "Jagged Bones", "Mark of Fury", "Meekness", "Necrosis", "Order of Undeath", "Pain of Disenchantment", "Poisoned Heart", "Putrid Flesh",
            "Ravenous Gaze", "Reaper's Mark", "Rip Enchantment", "Signet of Lost Souls", "Signet of Sorrow", "Signet of Suffering", "Toxic Chill", "Ulcerous Lungs", "Vocal Minority",
            "Well of Darkness", "Well of Silence", "Air of Disenchantment", "Cry of Pain", "Discharge Enchantment", "Drain Delusions", "Enchanter's Conundrum", "Ether Phantom", "Extend Conditions",
            "Frustration", "Hex Eater Vortex", "Hypochondria", "Mirror of Disenchantment", "Mistrust", "Persistence of Memory", "Power Flux", "Price of Pride", "Signet of Clumsiness",
            "Signet of Illusions", "Simple Thievery", "Spiritual Pain", "Symbolic Celerity", "Symbolic Posture", "Symbols of Inspiration", "Tease", "Visions of Regret", "Wastrel's Demise",
            "Web of Disruption", "Blinding Surge", "Chilling Winds", "Ebon Hawk", "Elemental Flame", "Ether Prism", "Flame Djinn's Haste", "Freezing Gust", "Frigid Armor", "Glowing Gaze", "Glowstone",
            "Glyph of Restoration", "Icy Shackles", "Intensity", "Invoke Lightning", "Lightning Bolt", "Liquid Flame", "Master of Magic", "Mind Blast", "Sandstorm", "Savannah Heat",
            "Searing Flames", "Steam", "Stone Sheath", "Stone Striker", "Stoneflesh Aura", "Storm Djinn's Haste", "Assassin's Remedy", "Assault Enchantments", "Augury of Death", "Black Spider Strike",
            "Critical Agility", "Deadly Haste", "Deadly Paradox", "Death's Retreat", "Disrupting Dagger", "Feigned Neutrality", "Fox's Promise", "Golden Fox Strike", "Golden Skull Strike",
            "Hidden Caltrops", "Lift Enchantment", "Malicious Strike", "Mark of Insecurity", "Shadow Meld", "Shadow Prison", "Shadow Walk", "Shattering Assault", "Signet of Toxic Shock",
            "Signet of Twilight", "Swap", "Wastrel's Collapse", "Way of the Assassin", "Anguish", "Caretaker's Charge", "Death Pact Signet", "Destructive Was Glaive", "Empowerment", "Gaze of Fury",
            "Ghostmirror Light", "Offering of Spirit", "Reclaim Essence", "Recovery", "Renewing Memories", "Renewing Surge", "Sight Beyond Sight", "Signet of Binding", "Signet of Ghostly Might",
            "Spirit's Gift", "Spirit's Strength", "Vampirism", "Vocal Was Sogolon", "Warmonger's Weapon", "Weapon of Fury", "Weapon of Remedy", "Wielder's Remedy", "Wielder's Strike",
            "Wielder's Zeal", "Xinrae's Weapon", "\"Brace Yourself!\"", "\"Can't Touch This!\"", "\"Fall Back!\"", "\"Find Their Weakness!\"", "\"Go for the Eyes!\"", "\"Help Me!\"", "\"Incoming!\"",
            "\"It's Just a Flesh Wound.\"", "\"Lead the Way!\"", "\"Make Haste!\"", "\"Make Your Time!\"", "\"Never Give Up!\"", "\"Never Surrender!\"", "\"Stand Your Ground!\"",
            "\"The Power Is Yours!\"", "\"There's Nothing to Fear!\"", "\"They're on Fire!\"", "\"We Shall Return!\"", "Aggressive Refrain", "Angelic Bond", "Angelic Protection", "Anthem of Envy",
            "Anthem of Flame", "Anthem of Fury", "Anthem of Guidance", "Aria of Restoration", "Aria of Zeal", "Awe", "Ballad of Restoration", "Barbed Spear", "Bladeturn Refrain", "Blazing Finale",
            "Blazing Spear", "Burning Refrain", "Cautery Signet", "Chorus of Restoration", "Crippling Anthem", "Cruel Spear", "Defensive Anthem", "Disrupting Throw", "Enduring Harmony",
            "Energizing Chorus", "Energizing Finale", "Finale of Restoration", "Focused Anger", "Glowing Signet", "Godspeed", "Harrier's Toss", "Hexbreaker Aria", "Leader's Comfort", "Leader's Zeal",
            "Lyric of Purification", "Lyric of Zeal", "Mending Refrain", "Merciless Spear", "Mighty Throw", "Natural Temper", "Purifying Finale", "Remedy Signet", "Signet of Aggression",
            "Signet of Return", "Signet of Synergy", "Slayer's Spear", "Soldier's Fury", "Song of Concentration", "Song of Power", "Song of Purification", "Song of Restoration", "Spear of Lightning",
            "Stunning Strike", "Swift Javelin", "Unblockable Throw", "Vicious Attack", "Wearying Spear", "Wild Throw", "Zealous Anthem", "Arcane Zeal", "Armor of Sanctity", "Attacker's Insight",
            "Aura of Thorns", "Avatar of Balthazar", "Avatar of Dwayna", "Avatar of Grenth", "Avatar of Lyssa", "Avatar of Melandru", "Balthazar's Rage", "Banishing Strike", "Chilling Victory",
            "Conviction", "Crippling Sweep", "Dust Cloak", "Dwayna's Touch", "Ebon Dust Aura", "Enchanted Haste", "Eremite's Attack", "Eremite's Zeal", "Eternal Aura", "Extend Enchantments",
            "Faithful Intervention", "Featherfoot Grace", "Fleeting Stability", "Grenth's Fingers", "Grenth's Grasp", "Guiding Hands", "Harrier's Grasp", "Harrier's Haste", "Heart of Fury",
            "Heart of Holy Flame", "Imbue Health", "Intimidating Aura", "Irresistible Sweep", "Lyssa's Assault", "Lyssa's Haste", "Meditation", "Mirage Cloak", "Mystic Corruption", "Mystic Healing",
            "Mystic Regeneration", "Mystic Sandstorm", "Mystic Sweep", "Mystic Twister", "Mystic Vigor", "Natural Healing", "Onslaught", "Pious Assault", "Pious Concentration", "Pious Haste",
            "Pious Renewal", "Pious Restoration", "Reap Impurities", "Reaper's Sweep", "Rending Aura", "Rending Sweep", "Rending Touch", "Sand Shards", "Signet of Pious Light", "Staggering Force",
            "Test of Faith", "Twin Moon Sweep", "Veil of Thorns", "Victorious Sweep", "Vital Boon", "Vow of Piety", "Vow of Silence", "Vow of Strength", "Watchful Intervention", "Wearying Strike",
            "Whirling Charge", "Winds of Disenchantment", "Wounding Strike", "Zealous Renewal", "Zealous Vow", "Sunspear Rebirth Signet", "Lightbringer Signet", "Lightbringer's Gaze"
        };
        static public string[] EyeOfTheNorthSkillNames =
        {
            "\"I Meant to Do That!\"", "Body Blow", "Disarm", "Distracting Strike", "Grapple", "Keen Chop", "Knee Cutter", "Pulverizing Smash", "Soldier's Speed", "Symbolic Strike",
            "Body Shot", "Companionship", "Disrupting Shot", "Expert Focus", "Feral Aggression", "Piercing Trap", "Poison Tip Signet", "Rapid Fire", "Sloth Hunter's Shot", "Volley",
            "Aura of Stability", "Castigation Signet", "Cure Hex", "Healing Ribbon", "Patient Spirit", "Purifying Veil", "Smite Condition", "Smiter's Boon", "Spotless Mind",
            "Spotless Soul", "Angorodon's Gaze", "Atrophy", "Cacophony", "Defile Defenses", "Foul Feast", "Hexer's Vigor", "Masochism", "Putrid Bile", "Well of Ruin", "Withering Aura",
            "Aneurysm", "Calculated Risk", "Confusing Images", "Power Lock", "Shrinking Armor", "Signet of Distraction", "Signet of Recall", "Sum of All Fears", "Wandering Eye",
            "Waste Not, Want Not", "Earthen Shackles", "Energy Blast", "Glowing Ice", "Glyph of Immolation", "Glyph of Swiftness", "Magnetic Surge", "Shell Shock", "Slippery Ground",
            "Ward of Weakness", "Winter's Embrace", "Falling Lotus Strike", "Golden Fang Strike", "Lotus Strike", "Sadist's Signet", "Shadow Fang", "Signet of Deadly Corruption",
            "Smoke Powder Defense", "Trampling Ox", "Vampiric Assault", "Way of the Master", "Agony", "Energetic Was Lee Sa", "Ghostly Weapon", "Mending Grip", "Pure Was Li Ming",
            "Rejuvenation", "Spiritleech Aura", "Sundering Weapon", "Weapon of Aggression", "Weapon of Renewal", "Anthem of Disruption", "Anthem of Weariness", "Burning Shield",
            "Chest Thumper", "Hasty Refrain", "Holy Spear", "Inspirational Speech", "Maiming Spear", "Spear Swipe", "Spear of Redemption", "Aura Slicer", "Crippling Victory",
            "Farmer's Scythe", "Grenth's Aura", "Pious Fury", "Radiant Scythe", "Shield of Force", "Signet of Mystic Speed", "Signet of Pious Restraint", "Zealous Sweep",
            "\"By Ural's Hammer!\"", "\"Dodge This!\"", "\"Don't Trip!\"", "\"Finish Him!\"", "\"I Am Unstoppable!\"", "\"I Am the Strongest!\"", "\"You Are All Weaklings!\"",
            "\"You Move Like a Dwarf!\"", "A Touch of Guile", "Air of Superiority", "Alkar's Alchemical Acid", "Asuran Scan", "Black Powder Mine", "Brawling Headbutt",
            "Breath of the Great Dwarf", "Club of a Thousand Bears", "Deft Strike", "Drunken Master", "Dwarven Stability", "Ear Bite", "Ebon Battle Standard of Courage",
            "Ebon Battle Standard of Honor", "Ebon Battle Standard of Wisdom", "Ebon Escape", "Ebon Vanguard Assassin Support", "Ebon Vanguard Sniper Support", "Feel No Pain",
            "Great Dwarf Armor", "Great Dwarf Weapon", "Light of Deldrimor", "Low Blow", "Mental Block", "Mindbender", "Pain Inverter", "Radiation Field", "Raven Blessing",
            "Signet of Infection", "Smooth Criminal", "Sneak Attack", "Snow Storm", "Summon Ice Imp", "Summon Mursaat", "Summon Naga Shaman", "Summon Ruby Djinn", "Technobabble",
            "Tryptophan Signet", "Ursan Blessing", "Volfen Blessing", "Weakness Trap", "Winds"
        };
        #endregion

        public static List<Skill> GetSkillsOfAttribute(Skill.Attributes attr)
        {
            List<Skill> skills = new List<Skill>();
            foreach (Skill skill in Data)
            {
                if (skill.Attribute == attr)
                {
                    skills.Add(skill);
                }
            }

            return skills;
        }

        public static GrabBag GetFullGrabBag()
        {
            GrabBag bag = new GrabBag();
            bag.Skills.AddRange(Data);
            return bag;
        }

        private static void PopulateLists()
        {
            // This function fills in specific lists that can be used for various purposes.
            SkillsByProfession = new List<Skill>[][] { new List<Skill>[11], new List<Skill>[11] };
            for (int i = 0; i < 11; ++i)
            {
                SkillsByProfession[0][i] = new List<Skill>();
                SkillsByProfession[1][i] = new List<Skill>();
            }

            foreach (Skill skill in Data)
            {
                // For lists that don't use PvE Only skills:
                if (skill.Attribute != Skill.Attributes.PvE_Only)
                {
                    SkillsByProfession[WithoutPvEOnly][(int)skill.Profession].Add(skill);
                }

                // For lists that do use PvE only skills:
                SkillsByProfession[WithPvEOnly][(int)skill.Profession].Add(skill);
            }

            // Load in some standard skills. This are skills that should always be available. There aren't many of these...
            Standard.Add(Data[1]); // Resurrection Signet
            Standard.Add(Data[398]); // Charm Animal

            // Create a list of standard issue lead attacks:
            LeadAttacks.Add(Data[661]);
            LeadAttacks.Add(Data[615]);
            LeadAttacks.Add(Data[662]);
            LeadAttacks.Add(Data[1023]);
            LeadAttacks.Add(Data[663]);
            LeadAttacks.Add(Data[474]);
            LeadAttacks.Add(Data[660]);

            // Create a list of standard issue off-hand attacks (require a lead):
            OffHandAttacks.Add(Data[472]);
            OffHandAttacks.Add(Data[1149]);
            OffHandAttacks.Add(Data[658]);
            OffHandAttacks.Add(Data[1148]);
            OffHandAttacks.Add(Data[659]);

            // Create a list of standard issue dual-attacks (require an off-hand):
            DualAttacks.Add(Data[657]);
            DualAttacks.Add(Data[467]);
            DualAttacks.Add(Data[469]);
            DualAttacks.Add(Data[643]);
            DualAttacks.Add(Data[1225]);
            DualAttacks.Add(Data[1147]);

            // Prepare lists to hold the skills by campaign:
            SkillsByCampaign = new List<Skill>[][]
            {
                new List<Skill>[]{new List<Skill>(), new List<Skill>(), new List<Skill>(), new List<Skill>(), new List<Skill>() },
                new List<Skill>[]{new List<Skill>(), new List<Skill>(), new List<Skill>(), new List<Skill>(), new List<Skill>() }
            };

            #region Assign skills to campaign lists.

            // Core skills:
            int index = GetCampaignIndex(Core);

            SkillsByCampaign[WithPvEOnly][index].Add(Data[354]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[354]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[333]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[333]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[357]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[357]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[338]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[338]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[324]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[324]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[360]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[360]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[307]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[307]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[359]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[359]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[322]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[322]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[325]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[325]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[347]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[347]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[320]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[320]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[335]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[335]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[345]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[345]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[327]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[327]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[330]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[330]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[315]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[315]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[337]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[337]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[326]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[326]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[374]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[374]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[336]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[336]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[373]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[373]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[321]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[321]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[310]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[310]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[0]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[0]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[349]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[349]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[370]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[370]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[346]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[346]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[341]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[341]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[312]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[312]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[379]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[379]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[375]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[375]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[371]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[371]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[353]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[353]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[339]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[339]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[350]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[350]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[331]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[331]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[314]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[314]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[352]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[352]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[311]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[311]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[406]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[406]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[414]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[414]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[437]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[437]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[384]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[384]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[400]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[400]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[391]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[391]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[398]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[398]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[413]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[413]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[415]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[415]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[396]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[396]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[394]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[394]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[390]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[390]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[424]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[424]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[388]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[388]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[385]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[385]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[452]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[452]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[427]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[427]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[451]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[451]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[421]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[421]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[438]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[438]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[380]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[380]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[412]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[412]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[432]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[432]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[417]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[417]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[454]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[454]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[381]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[381]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[383]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[383]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[389]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[389]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[448]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[448]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[386]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[386]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[453]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[453]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[411]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[411]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[405]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[405]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[419]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[419]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[440]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[440]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[434]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[434]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[403]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[403]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[425]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[425]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[429]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[429]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[442]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[442]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[247]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[247]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[262]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[262]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[286]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[286]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[242]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[242]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[287]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[287]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[293]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[293]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[274]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[274]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[236]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[236]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[301]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[301]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[248]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[248]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[277]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[277]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[278]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[278]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[264]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[264]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[303]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[303]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[299]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[299]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[282]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[282]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[257]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[257]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[231]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[231]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[288]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[288]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[267]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[267]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[280]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[280]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[271]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[271]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[235]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[235]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[268]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[268]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[285]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[285]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[296]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[296]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[291]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[291]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[295]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[295]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[238]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[238]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[297]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[297]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[241]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[241]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[251]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[251]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[289]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[289]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[283]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[283]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[284]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[284]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[292]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[292]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[263]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[263]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[233]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[233]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[272]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[272]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[261]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[261]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[75]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[75]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[76]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[76]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[122]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[122]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[92]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[92]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[110]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[110]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[111]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[111]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[135]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[135]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[107]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[107]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[129]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[129]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[124]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[124]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[95]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[95]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[96]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[96]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[120]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[120]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[121]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[121]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[109]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[109]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[126]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[126]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[77]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[77]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[100]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[100]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[133]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[133]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[141]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[141]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[125]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[125]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[90]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[90]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[123]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[123]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[145]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[145]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[86]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[86]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[132]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[132]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[128]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[128]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[97]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[97]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[136]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[136]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[87]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[87]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[134]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[134]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[99]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[99]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[104]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[104]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[143]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[143]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[101]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[101]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[144]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[144]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[146]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[146]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[150]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[150]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[83]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[83]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[85]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[85]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[31]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[31]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[66]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[66]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[58]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[58]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[23]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[23]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[38]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[38]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[26]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[26]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[49]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[49]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[52]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[52]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[25]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[25]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[61]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[61]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[65]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[65]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[21]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[21]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[37]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[37]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[70]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[70]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[34]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[34]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[71]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[71]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[69]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[69]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[35]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[35]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[15]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[15]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[7]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[7]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[30]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[30]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[32]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[32]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[67]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[67]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[55]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[55]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[3]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[3]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[4]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[4]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[5]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[5]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[11]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[11]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[6]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[6]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[10]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[10]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[9]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[9]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[13]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[13]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[44]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[44]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[20]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[20]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[18]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[18]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[62]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[62]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[60]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[60]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[56]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[56]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[51]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[51]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[59]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[59]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[164]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[164]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[215]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[215]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[155]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[155]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[170]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[170]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[210]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[210]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[225]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[225]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[172]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[172]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[197]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[197]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[211]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[211]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[224]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[224]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[159]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[159]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[154]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[154]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[214]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[214]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[174]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[174]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[187]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[187]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[176]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[176]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[184]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[184]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[152]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[152]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[188]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[188]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[190]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[190]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[204]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[204]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[201]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[201]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[181]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[181]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[173]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[173]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[219]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[219]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[212]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[212]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[195]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[195]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[222]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[222]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[205]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[205]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[177]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[177]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[175]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[175]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[199]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[199]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[209]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[209]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[208]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[208]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[194]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[194]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[162]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[162]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[161]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[161]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[167]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[167]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[166]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[166]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[198]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[198]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1]);
            // Technically, the PvE-only 15th anniversary elites are core skills:
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1318]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1319]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1320]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1321]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1322]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1323]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1324]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1325]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1326]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1327]);

            // Prophecies skills:
            index = GetCampaignIndex(Prophecies);

            SkillsByCampaign[WithPvEOnly][index].Add(Data[356]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[356]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[323]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[323]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[358]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[358]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[306]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[306]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[355]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[355]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[332]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[332]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[348]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[348]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[340]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[340]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[369]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[369]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[368]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[368]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[343]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[343]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[342]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[342]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[377]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[377]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[362]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[362]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[308]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[308]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[313]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[313]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[365]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[365]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[351]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[351]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[364]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[364]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[344]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[344]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[328]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[328]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[378]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[378]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[334]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[334]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[372]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[372]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[361]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[361]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[317]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[317]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[329]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[329]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[316]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[316]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[318]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[318]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[376]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[376]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[309]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[309]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[367]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[367]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[319]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[319]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[363]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[363]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[366]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[366]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[416]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[416]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[423]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[423]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[399]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[399]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[382]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[382]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[404]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[404]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[431]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[431]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[436]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[436]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[443]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[443]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[418]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[418]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[446]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[446]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[450]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[450]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[444]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[444]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[439]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[439]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[410]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[410]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[407]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[407]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[409]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[409]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[408]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[408]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[420]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[420]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[430]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[430]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[455]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[455]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[393]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[393]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[426]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[426]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[387]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[387]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[395]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[395]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[392]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[392]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[428]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[428]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[422]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[422]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[449]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[449]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[397]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[397]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[401]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[401]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[435]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[435]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[447]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[447]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[402]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[402]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[433]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[433]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[441]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[441]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[255]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[255]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[250]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[250]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[232]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[232]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[246]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[246]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[290]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[290]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[269]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[269]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[300]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[300]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[273]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[273]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[240]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[240]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[270]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[270]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[276]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[276]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[275]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[275]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[302]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[302]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[239]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[239]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[234]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[234]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[260]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[260]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[294]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[294]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[281]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[281]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[259]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[259]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[265]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[265]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[254]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[254]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[256]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[256]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[253]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[253]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[266]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[266]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[304]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[304]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[243]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[243]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[249]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[249]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[252]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[252]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[230]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[230]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[298]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[298]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[237]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[237]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[258]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[258]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[305]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[305]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[244]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[244]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[279]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[279]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[245]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[245]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[74]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[74]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[105]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[105]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[102]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[102]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[106]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[106]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[148]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[148]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[89]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[89]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[138]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[138]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[80]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[80]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[103]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[103]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[108]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[108]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[142]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[142]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[130]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[130]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[114]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[114]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[117]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[117]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[131]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[131]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[113]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[113]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[118]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[118]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[88]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[88]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[137]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[137]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[139]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[139]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[140]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[140]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[94]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[94]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[93]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[93]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[127]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[127]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[91]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[91]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[119]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[119]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[115]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[115]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[112]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[112]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[149]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[149]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[147]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[147]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[79]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[79]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[78]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[78]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[81]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[81]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[98]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[98]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[82]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[82]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[84]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[84]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[116]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[116]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[72]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[72]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[24]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[24]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[33]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[33]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[68]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[68]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[8]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[8]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[63]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[63]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[36]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[36]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[40]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[40]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[50]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[50]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[41]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[41]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[27]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[27]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[28]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[28]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[42]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[42]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[16]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[16]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[17]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[17]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[57]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[57]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[12]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[12]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[73]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[73]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[14]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[14]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[48]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[48]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[47]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[47]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[39]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[39]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[64]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[64]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[2]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[2]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[19]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[19]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[46]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[46]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[22]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[22]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[53]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[53]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[54]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[54]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[43]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[43]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[29]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[29]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[45]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[45]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[196]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[196]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[228]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[228]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[213]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[213]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[207]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[207]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[160]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[160]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[157]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[157]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[168]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[168]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[171]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[171]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[178]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[178]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[202]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[202]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[217]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[217]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[191]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[191]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[189]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[189]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[193]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[193]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[192]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[192]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[163]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[163]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[200]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[200]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[169]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[169]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[206]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[206]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[156]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[156]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[185]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[185]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[220]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[220]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[158]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[158]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[180]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[180]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[182]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[182]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[216]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[216]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[226]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[226]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[183]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[183]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[179]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[179]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[186]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[186]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[203]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[203]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[221]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[221]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[223]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[223]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[218]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[218]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[165]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[165]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[229]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[229]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[227]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[227]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[153]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[153]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[151]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[151]);
            // Factions skills:
            index = GetCampaignIndex(Factions);

            SkillsByCampaign[WithPvEOnly][index].Add(Data[550]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[550]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[485]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[485]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[567]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[567]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[578]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[578]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[525]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[525]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1144]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1210]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[739]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[739]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[577]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[577]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[740]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[740]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[579]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[579]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[734]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[734]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[650]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[650]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[536]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[536]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[566]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[566]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[576]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[576]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[736]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[736]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[535]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[535]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[735]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[735]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[737]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[737]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[518]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[518]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[498]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[498]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[568]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[568]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[651]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[651]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[742]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[742]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[612]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[612]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[741]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[741]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[653]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[653]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[537]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[537]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[652]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[652]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[649]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[649]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[565]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[565]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[738]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[738]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[751]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[751]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[757]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[757]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[754]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[754]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[614]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[614]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[749]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[749]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[445]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[445]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[753]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[753]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[759]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[759]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[654]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[654]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[581]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[581]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[750]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[750]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[746]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[746]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[627]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[627]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[580]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[580]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[539]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[539]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[748]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[748]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[755]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[755]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[756]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[756]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[745]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[745]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[499]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[499]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[752]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[752]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[569]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[569]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[540]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[540]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[538]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[538]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[743]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[743]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[760]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[760]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[613]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[613]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1143]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1209]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[758]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[758]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[747]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[747]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[744]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[744]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[722]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[722]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[609]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[609]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[533]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[533]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[648]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[648]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[524]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[524]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[729]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[729]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[625]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[625]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[611]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[611]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[727]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[727]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[724]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[724]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[549]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[549]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[624]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[624]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[723]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[723]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[726]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[726]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[725]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[725]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[720]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[720]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[728]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[728]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[517]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[517]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[626]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[626]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[730]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[730]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[534]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[534]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1142]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1208]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[562]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[562]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[808]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[808]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[564]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[564]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[732]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[732]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[623]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[623]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[721]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[721]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[733]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[733]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[610]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[610]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[731]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[731]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[519]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[519]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[494]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[494]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[695]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[695]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[521]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[521]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[700]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[700]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[495]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[495]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[697]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[697]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[505]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[505]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[703]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[703]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[527]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[527]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[461]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[461]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[509]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[509]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[459]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[459]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[694]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[694]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[546]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[546]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[545]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[545]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[520]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[520]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[604]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[604]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[698]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[698]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1140]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1206]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[573]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[573]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[693]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[693]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[696]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[696]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[701]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[701]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[507]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[507]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[699]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[699]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[516]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[516]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[460]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[460]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[702]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[702]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[510]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[510]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[506]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[506]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[684]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[684]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[686]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[686]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[493]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[493]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[692]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[692]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[599]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[599]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[601]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[601]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[542]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[542]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1139]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1205]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[558]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[558]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[621]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[621]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[691]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[691]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[690]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[690]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[556]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[556]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[571]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[571]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[688]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[688]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[501]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[501]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[554]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[554]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[570]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[570]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[492]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[492]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[600]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[600]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[685]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[685]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[689]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[689]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[687]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[687]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[682]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[682]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[683]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[683]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[572]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[572]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[602]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[602]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[559]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[559]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[543]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[543]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[557]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[557]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[603]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[603]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[528]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[528]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[708]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[708]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[513]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[513]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[714]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[714]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[511]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[511]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[530]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[530]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[712]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[712]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[709]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[709]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1141]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1207]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[523]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[523]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[716]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[716]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[529]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[529]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[575]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[575]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[512]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[512]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[547]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[547]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[718]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[718]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[522]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[522]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[710]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[710]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[497]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[497]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[705]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[705]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[606]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[606]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[707]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[707]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[711]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[711]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[715]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[715]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[719]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[719]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[713]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[713]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[717]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[717]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[704]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[704]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[706]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[706]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[548]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[548]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[607]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[607]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[672]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[672]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[464]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[464]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[488]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[488]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[471]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[471]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[661]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[661]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[657]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[657]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[631]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[631]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[642]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[642]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[675]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[675]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[664]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[664]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[655]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[655]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[656]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[656]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[541]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[541]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[666]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[666]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[674]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[674]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[680]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[680]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[679]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[679]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[467]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[467]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[619]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[619]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[615]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[615]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[662]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[662]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[489]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[489]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[476]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[476]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[633]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[633]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[491]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[491]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[647]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[647]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[470]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[470]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[678]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[678]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[472]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[472]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[663]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[663]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[646]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[646]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[669]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[669]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[469]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[469]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[670]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[670]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[478]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[478]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[474]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[474]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[658]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[658]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[660]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[660]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[667]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[667]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[632]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[632]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[477]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[477]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[636]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[636]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[504]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[504]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[473]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[473]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[643]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[643]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[681]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[681]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[594]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[594]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[634]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[634]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[463]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[463]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[503]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[503]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[671]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[671]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[514]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[514]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[502]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[502]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1138]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1204]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[597]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[597]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[598]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[598]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[617]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[617]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[596]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[596]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[595]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[595]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[668]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[668]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[490]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[490]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[673]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[673]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[553]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[553]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[618]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[618]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[515]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[515]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[676]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[676]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[645]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[645]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[468]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[468]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[677]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[677]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[475]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[475]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[462]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[462]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[665]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[665]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[644]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[644]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[616]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[616]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[635]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[635]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[659]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[659]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[788]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[788]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[768]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[768]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[777]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[777]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[765]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[765]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[781]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[781]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[480]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[480]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[794]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[794]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[775]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[775]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[797]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[797]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[770]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[770]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[761]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[761]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[585]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[585]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[763]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[763]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[500]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[500]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[591]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[591]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[593]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[593]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[790]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[790]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[592]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[592]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[803]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[803]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[769]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[769]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[780]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[780]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[793]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[793]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[772]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[772]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[774]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[774]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[638]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[638]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[483]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[483]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[787]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[787]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[465]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[465]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[786]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[786]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[481]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[481]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[798]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[798]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[587]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[587]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[792]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[792]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[767]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[767]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[779]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[779]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[466]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[466]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[487]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[487]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[789]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[789]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[782]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[782]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[791]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[791]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[764]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[764]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[639]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[639]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[766]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[766]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[479]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[479]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[629]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[629]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[762]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[762]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[588]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[588]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[552]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[552]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[640]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[640]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[783]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[783]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[784]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[784]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[805]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[805]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[778]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[778]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[785]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[785]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[771]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[771]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[590]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[590]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[776]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[776]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[586]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[586]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[796]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[796]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[582]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[582]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[773]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[773]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[628]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[628]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[589]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[589]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[484]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[484]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1179]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1213]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[584]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[584]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[583]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[583]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[482]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[482]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[630]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[630]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[806]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[806]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[486]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[486]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[795]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[795]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[807]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[807]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[641]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[641]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[804]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[804]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1146]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1212]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1145]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1211]);
            // Nightfall skills:
            index = GetCampaignIndex(Nightfall);

            SkillsByCampaign[WithPvEOnly][index].Add(Data[879]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[879]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[870]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[870]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[883]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[883]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[880]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[880]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[872]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[872]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1061]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1061]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[882]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[882]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[869]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[869]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1064]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1064]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[881]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[881]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[871]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[871]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1068]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1068]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[873]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[873]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[874]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[874]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1062]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1062]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1065]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1065]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[876]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[876]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[877]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[877]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[875]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[875]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[878]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[878]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1067]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1067]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1066]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1066]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1063]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1063]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1069]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1069]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1070]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1070]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1219]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[886]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[886]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[889]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[889]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[885]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[885]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[888]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[888]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1075]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1075]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1076]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1076]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1074]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1074]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1080]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1080]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1082]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1082]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1072]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1072]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1078]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1078]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1079]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1079]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1220]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[551]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[551]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[884]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[884]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[892]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[892]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1073]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1073]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1077]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1077]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[890]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[890]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1071]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1071]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1081]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1081]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[893]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[893]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[887]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[887]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[891]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[891]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[894]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[894]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[895]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[895]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[862]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[862]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1056]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1056]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1059]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1059]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1060]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1060]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1054]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1054]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[860]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[860]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[861]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[861]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[801]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[801]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1053]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1053]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[857]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[857]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[864]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[864]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[868]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[868]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1051]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1051]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[802]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[802]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[563]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[563]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[867]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[867]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[865]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[865]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1052]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1052]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1218]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[866]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[866]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1057]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1057]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1058]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1058]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[858]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[858]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[859]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[859]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[863]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[863]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1055]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1055]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[827]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[827]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[574]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[574]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[832]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[832]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[836]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[836]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[508]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[508]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[605]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[605]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[830]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[830]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[831]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[831]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[835]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[835]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[799]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[799]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1216]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[828]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[828]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[834]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[834]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[526]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[526]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[829]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[829]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[544]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[544]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[496]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[496]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[622]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[622]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[839]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[839]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[837]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[837]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[838]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[838]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1045]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1045]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[833]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[833]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[560]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[560]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[840]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[840]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1046]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1046]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1042]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1042]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1215]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[823]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[823]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[813]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[813]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[821]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[821]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[819]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[819]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[809]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[809]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[817]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[817]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[824]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[824]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[810]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[810]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[825]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[825]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[637]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[637]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[814]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[814]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[620]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[620]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1041]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1041]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1043]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1043]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[822]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[822]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[826]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[826]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[812]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[812]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[816]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[816]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1044]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1044]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[815]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[815]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[818]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[818]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[555]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[555]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[811]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[811]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[820]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[820]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[841]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[841]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[842]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[842]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[848]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[848]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1049]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1049]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[851]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[851]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[855]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[855]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[856]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[856]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[800]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[800]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[853]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[853]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1047]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1047]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[850]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[850]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[608]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[608]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1217]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1050]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1050]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[843]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[843]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[531]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[531]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[852]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[852]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1048]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1048]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[846]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[846]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[854]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[854]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[561]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[561]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[532]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[532]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[847]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[847]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[845]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[845]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[849]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[849]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[844]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[844]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1025]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1025]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1029]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1029]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1032]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1032]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1022]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1022]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1214]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1024]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1024]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[458]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[458]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1037]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1037]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[457]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[457]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1027]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1027]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1026]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1026]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1023]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1023]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1021]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1021]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1028]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1028]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1031]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1031]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1019]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1019]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[456]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[456]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1040]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1040]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1038]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1038]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1036]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1036]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1020]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1020]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1033]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1033]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1034]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1034]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1039]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1039]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1030]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1030]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1035]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1035]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1096]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1096]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1095]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1095]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[899]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[899]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1084]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1084]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1097]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1097]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1086]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1086]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1092]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1092]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[897]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[897]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[900]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[900]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1098]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1098]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1090]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1090]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[896]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[896]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1089]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1089]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1094]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1094]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1093]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1093]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[898]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[898]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1087]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1087]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1222]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1083]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1083]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1101]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1101]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1099]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1099]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1102]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1102]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1091]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1091]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1085]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1085]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1088]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1088]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1100]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1100]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[986]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[986]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1130]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1130]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1008]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1008]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1131]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1131]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[972]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[972]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1007]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1007]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1009]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1009]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1012]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1012]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1003]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1003]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1004]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1004]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1129]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1129]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1006]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1006]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1011]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1011]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1002]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1002]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1132]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1132]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1223]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1010]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1010]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1005]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1005]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1124]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1124]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1000]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1000]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[999]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[999]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[973]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[973]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[971]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[971]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[967]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[967]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[982]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[982]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[980]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[980]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[976]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[976]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[987]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[987]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[978]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[978]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1013]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1013]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[994]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[994]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[989]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[989]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[960]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[960]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[990]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[990]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1001]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1001]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[979]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[979]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[968]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[968]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[962]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[962]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[969]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[969]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1017]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1017]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[988]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[988]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[983]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[983]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1125]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1125]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[991]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[991]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1119]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1119]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[995]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[995]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[970]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[970]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[963]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[963]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[985]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[985]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[997]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[997]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[996]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[996]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1122]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1122]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[977]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[977]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[992]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[992]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1016]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1016]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[961]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[961]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1120]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1120]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[993]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[993]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1127]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1127]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1126]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1126]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1128]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1128]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[998]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[998]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1133]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1133]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1123]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1123]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[981]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[981]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[974]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[974]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[984]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[984]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1121]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1121]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[965]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[965]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1015]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1015]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1134]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1134]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[964]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[964]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1014]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1014]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[966]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[966]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1018]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1018]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[975]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[975]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[917]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[917]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[929]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[929]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1114]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1114]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[911]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[911]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[932]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[932]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[933]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[933]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[934]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[934]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[935]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[935]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[936]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[936]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[912]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[912]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[901]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[901]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[953]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[953]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[954]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[954]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[949]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[949]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[913]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[913]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[942]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[942]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1110]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1110]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[955]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[955]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[903]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[903]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[938]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[938]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1221]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[923]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[923]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[924]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[924]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1116]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1116]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[928]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[928]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[910]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[910]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1106]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1106]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[927]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[927]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1108]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1108]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1118]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1118]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1112]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1112]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[922]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[922]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[940]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[940]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[945]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[945]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[907]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[907]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[952]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[952]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[926]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[926]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[937]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[937]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[916]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[916]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1105]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1105]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[941]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[941]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[930]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[930]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[946]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[946]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[902]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[902]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[909]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[909]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[918]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[918]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[939]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[939]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1104]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1104]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[908]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[908]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[956]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[956]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[957]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[957]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[915]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[915]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[943]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[943]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[904]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[904]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1117]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1117]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1115]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1115]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1103]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1103]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[948]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[948]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[925]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[925]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[944]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[944]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[914]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[914]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[959]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[959]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[905]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[905]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1107]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1107]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[906]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[906]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[921]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[921]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[920]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[920]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[931]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[931]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1109]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1109]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[919]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[919]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[951]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[951]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[958]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[958]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[947]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[947]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[950]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[950]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1113]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1113]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1111]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1111]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1137]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1136]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1135]);
            // Eye of the North skills:
            index = GetCampaignIndex(EyeOfTheNorth);

            SkillsByCampaign[WithPvEOnly][index].Add(Data[1195]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1195]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1252]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1252]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1194]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1194]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1249]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1249]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1171]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1171]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1169]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1169]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1170]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1170]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1168]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1168]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1251]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1251]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1250]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1250]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1253]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1253]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1231]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1231]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1233]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1233]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1235]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1235]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1232]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1232]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1230]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1230]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1254]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1254]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1196]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1196]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1197]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1197]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1234]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1234]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1191]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1191]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1166]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1166]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1163]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1163]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1190]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1190]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1189]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1189]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1167]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1167]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1164]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1164]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1165]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1165]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1192]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1192]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1193]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1193]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1244]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1244]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1292]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1292]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1158]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1158]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1243]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1243]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1185]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1185]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1228]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1228]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1229]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1229]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1186]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1186]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1291]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1291]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1157]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1157]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1183]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1183]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1181]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1181]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1227]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1227]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1154]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1154]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1182]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1182]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1152]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1152]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1153]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1153]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1156]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1156]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1184]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1184]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1155]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1155]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1160]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1160]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1248]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1248]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1247]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1247]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1188]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1188]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1162]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1162]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1245]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1245]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1187]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1187]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1246]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1246]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1161]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1161]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1159]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1159]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1150]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1150]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1149]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1149]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1148]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1148]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1151]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1151]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1180]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1180]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1241]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1241]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1226]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1226]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1225]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1225]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1147]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1147]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1242]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1242]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1260]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1260]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1176]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1176]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1261]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1261]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1257]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1257]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1200]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1200]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1259]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1259]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1258]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1258]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1238]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1238]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1201]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1201]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1239]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1239]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1178]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1178]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1177]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1177]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1263]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1263]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1202]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1202]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1203]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1203]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1264]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1264]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1262]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1262]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1240]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1240]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1265]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1265]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1293]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1293]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1198]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1198]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1237]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1237]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1175]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1175]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1173]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1173]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1236]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1236]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1172]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1172]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1256]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1256]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1255]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1255]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1174]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1174]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1199]);
            SkillsByCampaign[WithoutPvEOnly][index].Add(Data[1199]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1272]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1295]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1271]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1294]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1297]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1296]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1300]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1299]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1298]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1311]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1266]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1310]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1278]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1270]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1276]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1302]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1283]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1273]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1317]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1268]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1286]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1288]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1287]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1314]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1290]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1289]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1301]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1275]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1274]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1267]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1269]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1312]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1306]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1313]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1309]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1305]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1284]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1307]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1224]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1277]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1281]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1279]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1282]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1280]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1308]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1285]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1303]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1304]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1315]);
            SkillsByCampaign[WithPvEOnly][index].Add(Data[1316]);

            #endregion

            // Assign skills their campaign! -- Skipping Core, since that is the default assingment, and I can save an iteration, so why not?
            foreach(Skill skill in GetSkillsByCampaign(Prophecies, true))
            {
                skill.Campaign = Skill.Campaigns.Prophecies;
            }
            foreach (Skill skill in GetSkillsByCampaign(Factions, true))
            {
                skill.Campaign = Skill.Campaigns.Factions;
            }
            foreach (Skill skill in GetSkillsByCampaign(Nightfall, true))
            {
                skill.Campaign = Skill.Campaigns.Nightfall;
            }
            foreach (Skill skill in GetSkillsByCampaign(EyeOfTheNorth, true))
            {
                skill.Campaign = Skill.Campaigns.Eye_of_the_North;
            }
        }

        static private int GetCampaignIndex(int campaignID)
        {
            switch (campaignID)
            {
                case Core: return 0;
                case Prophecies: return 1;
                case Factions: return 2;
                case Nightfall: return 3;
                case EyeOfTheNorth: return 4;
                default: return -1;
            }
        }

        static public List<Skill> GetSkillsByCampaign(int campaigns, bool includePvEOnly)
        {
            int metaindex = includePvEOnly ? WithPvEOnly : WithoutPvEOnly;

            List<Skill> pool = new List<Skill>();
            if ((campaigns & Core) != 0)
            {
                pool.AddRange(SkillsByCampaign[metaindex][GetCampaignIndex(Core)]);
            }
            if ((campaigns & Prophecies) != 0)
            {
                pool.AddRange(SkillsByCampaign[metaindex][GetCampaignIndex(Prophecies)]);
            }
            if ((campaigns & Factions) != 0)
            {
                pool.AddRange(SkillsByCampaign[metaindex][GetCampaignIndex(Factions)]);
            }
            if ((campaigns & Nightfall) != 0)
            {
                pool.AddRange(SkillsByCampaign[metaindex][GetCampaignIndex(Nightfall)]);
            }
            if ((campaigns & EyeOfTheNorth) != 0)
            {
                pool.AddRange(SkillsByCampaign[metaindex][GetCampaignIndex(EyeOfTheNorth)]);
            }

            return pool;
        }

        public static List<Skill> GetComplexList(Skill.Professions[] allowableProfessions, Skill.Attributes[] allowableAttributes, bool[] allowableEliteStatus)
        {
            List<Skill> codex = new List<Skill>();

            foreach (Skill skill in Data)
            {
                if (allowableProfessions.Contains(skill.Profession) && allowableAttributes.Contains(skill.Attribute) && allowableEliteStatus.Contains(skill.IsElite))
                {
                    codex.Add(skill);
                }
            }

            return codex;
        }

        public static List<Skill> GetSkillsByProfession(Skill.Professions prof, int withPvEOnly)
        {
            return SkillsByProfession[withPvEOnly][(int)prof];
        }

        public static int GetSkillIndexByName(string name)
        {
            return Data.FindIndex((x) => string.Compare(x.Name, name, true) == 0);
        }

        /// <summary>
        /// Loads all of the information about skills into the giant list. Hurray!
        /// </summary>
        public static void LoadSkillInformation()
        {
            if (Data.Count() > 0) return; // I'm going to assume that if data has data, then we've done this already and don't need to do it again.

            /*
             new Skill(name, icon, attribute, profession, rating, description, elite [=false])
             
             */

            Data.Add(new Skill("Healing Signet", Properties.Resources.Healing_Signet, Skill.Attributes.Tactics, Skill.Professions.Warrior, 2, "2C 4R - Signet. You gain 82...154...172 Health. You have -40 armor while using this skill."));
            Data.Add(new Skill("Resurrection Signet", Properties.Resources.Resurrection_Signet, Skill.Attributes.None, Skill.Professions.None, 2, "3C - Signet. Resurrects target party member (100% Health, 25% Energy). This signet only recharges when you gain a morale boost."));
            //Data.Add(new Skill("Signet of Capture", Properties.Resources.Signet_of_Capture, Skill.Attributes.PvE_Only, Skill.Professions.None)); <-- Look, capsig really shouldn't be in this list...
            Data.Add(new Skill("Power Block", Properties.Resources.Power_Block, Skill.Attributes.Domination_Magic, Skill.Professions.Mesmer, 1, "15E 1/4C 20R - Elite Spell. If target foe is casting a spell or chant, that skill and all skills of the same attribute are disabled (1...10...12 seconds) and that skill is interrupted.", true));
            Data.Add(new Skill("Mantra of Earth", Properties.Resources.Mantra_of_Earth, Skill.Attributes.Inspiration_Magic, Skill.Professions.Mesmer, 2, "10E 20R - Stance. (30...78...90 seconds.) Reduces earth damage you take by 26...45...50%. You gain 2 Energy when you take earth damage."));
            Data.Add(new Skill("Mantra of Flame", Properties.Resources.Mantra_of_Flame, Skill.Attributes.Inspiration_Magic, Skill.Professions.Mesmer, 2, "10E 20R - Stance. (30...78...90 seconds.) Reduces fire damage you take by 26...45...50%. You gain 2 Energy when you take fire damage."));
            Data.Add(new Skill("Mantra of Frost", Properties.Resources.Mantra_of_Frost, Skill.Attributes.Inspiration_Magic, Skill.Professions.Mesmer, 2, "10E 20R - Stance. (30...78...90 seconds.) Reduces cold damage you take by 26...45...50%. You gain 2 Energy when you take cold damage."));
            Data.Add(new Skill("Mantra of Lightning", Properties.Resources.Mantra_of_Lightning, Skill.Attributes.Inspiration_Magic, Skill.Professions.Mesmer, 2, "10E 20R - Stance. (30...78...90 seconds.) Reduces lightning damage you take by 26...45...50%. You gain 2 Energy when you take lightning damage."));
            Data.Add(new Skill("Hex Breaker", Properties.Resources.Hex_Breaker, Skill.Attributes.Domination_Magic, Skill.Professions.Mesmer, 1, "5E 15R - Stance. (5...65...80 seconds.) The next hex against you fails and the caster takes 10...39...46 damage."));
            Data.Add(new Skill("Distortion", Properties.Resources.Distortion, Skill.Attributes.Illusion_Magic, Skill.Professions.Mesmer, 2, "5E 8R - Stance. (1...4...5 second[s].) You have 75% chance to block. Block cost: you lose 2 Energy or Distortion ends."));
            Data.Add(new Skill("Mantra of Recovery", Properties.Resources.Mantra_of_Recovery, Skill.Attributes.Fast_Casting, Skill.Professions.Mesmer, 2, "5E 15R - Elite Stance. (5...17...20 seconds.) Your spells recharge 33% faster.", true));
            Data.Add(new Skill("Mantra of Persistence", Properties.Resources.Mantra_of_Persistence, Skill.Attributes.Inspiration_Magic, Skill.Professions.Mesmer, 1, "5E 15R - Stance. (5...21...25 seconds.) Illusion hexes you cast last 10...34...40% longer."));
            Data.Add(new Skill("Mantra of Inscriptions", Properties.Resources.Mantra_of_Inscriptions, Skill.Attributes.Inspiration_Magic, Skill.Professions.Mesmer, 2, "10E 20R - Stance. (5...21...25 seconds.) Signets you successfully activate while in this stance recharge 10...34...40% faster."));
            Data.Add(new Skill("Mantra of Concentration", Properties.Resources.Mantra_of_Concentration, Skill.Attributes.Inspiration_Magic, Skill.Professions.Mesmer, 1, "5E 30R - Stance. (1...31...38 seconds.) The next time you would be interrupted, you are not interrupted."));
            Data.Add(new Skill("Mantra of Resolve", Properties.Resources.Mantra_of_Resolve, Skill.Attributes.Inspiration_Magic, Skill.Professions.Mesmer, 2, "10E 20R - Stance. (30...78...90 seconds.) Prevents interrupts against you. Prevention cost: lose 10...5...4 Energy or Mantra of Resolve ends."));
            Data.Add(new Skill("Mantra of Signets", Properties.Resources.Mantra_of_Signets, Skill.Attributes.Inspiration_Magic, Skill.Professions.Mesmer, 1, "5E 20R - Stance. (10...34...40 seconds.) You have +3 armor for each signet. You gain 5...49...60 health each time you use a signet."));
            Data.Add(new Skill("Fragility", Properties.Resources.Fragility, Skill.Attributes.Illusion_Magic, Skill.Professions.Mesmer, 2, "5E 1C 5R - Hex Spell. Also hexes foes adjacent to target (8...18...20 seconds). These foes take 5...17...20 damage each time they gain or lose a condition."));
            Data.Add(new Skill("Inspired Enchantment", Properties.Resources.Inspired_Enchantment, Skill.Attributes.Inspiration_Magic, Skill.Professions.Mesmer, 2, "10E 1C - Spell. Removes an enchantment from target foe. Removal effects: you gain 3...13...15 Energy; this spell is replaced with that enchantment (20 seconds)."));
            Data.Add(new Skill("Inspired Hex", Properties.Resources.Inspired_Hex, Skill.Attributes.Inspiration_Magic, Skill.Professions.Mesmer, 3, "5E 1C - Spell. Removes a hex from target ally. Removal effects: you gain 4...9...10 Energy; this spell is replaced with that hex (20 seconds)."));
            Data.Add(new Skill("Power Spike", Properties.Resources.Power_Spike, Skill.Attributes.Domination_Magic, Skill.Professions.Mesmer, 3, "5E 1/4C 12R - Spell. Interrupts a spell or chant. Interruption effect: deals 30...102...120 damage."));
            Data.Add(new Skill("Power Leak", Properties.Resources.Power_Leak, Skill.Attributes.Domination_Magic, Skill.Professions.Mesmer, 2, "10E 1/4C 20R - Spell. Interrupts a spell or chant. Interruption effect: causes 3...14...17 Energy loss."));
            Data.Add(new Skill("Power Drain", Properties.Resources.Power_Drain, Skill.Attributes.Inspiration_Magic, Skill.Professions.Mesmer, 4, "5E 1/4C 20R - Spell. Interrupts a spell or chant. Interruption effect: you gain 1...25...31 Energy."));
            Data.Add(new Skill("Empathy", Properties.Resources.Empathy, Skill.Attributes.Domination_Magic, Skill.Professions.Mesmer, 5, "10E 2C 10R - Hex Spell. (5...13...15 seconds.) Target foe's attacks deal 1...12...15 less damage, and that foe takes 10...46...55 damage with each attack."));
            Data.Add(new Skill("Shatter Delusions", Properties.Resources.Shatter_Delusions, Skill.Attributes.Domination_Magic, Skill.Professions.Mesmer, 2, "5E 1/4C 6R - Spell. Removes a Mesmer hex from target foe. Removal effect: 15...63...75 damage to target and all adjacent foes."));
            Data.Add(new Skill("Backfire", Properties.Resources.Backfire, Skill.Attributes.Domination_Magic, Skill.Professions.Mesmer, 3, "15E 3C 20R - Hex Spell. (10 seconds.) Target foe takes 35...119...140 damage whenever it casts a spell."));
            Data.Add(new Skill("Blackout", Properties.Resources.Blackout, Skill.Attributes.Domination_Magic, Skill.Professions.Mesmer, 1, "10E 1C 12R - Touch Skill. (2...5...6 seconds.) Disables skills. Your skills are disabled (5 seconds)."));
            Data.Add(new Skill("Diversion", Properties.Resources.Diversion, Skill.Attributes.Domination_Magic, Skill.Professions.Mesmer, 3, "10E 3C 12R - Hex Spell. (6 seconds.) Target foe's next skill takes +10...47...56 seconds to recharge."));
            Data.Add(new Skill("Conjure Phantasm", Properties.Resources.Conjure_Phantasm, Skill.Attributes.Illusion_Magic, Skill.Professions.Mesmer, 3, "10E 1C 5R - Hex Spell. (2...13...16 seconds.) Causes -5 Health degeneration."));
            Data.Add(new Skill("Illusion of Weakness", Properties.Resources.Illusion_of_Weakness, Skill.Attributes.Illusion_Magic, Skill.Professions.Mesmer, 2, "10E 2C 30R - Enchantment Spell. Lose 50...202...240 Health. End effect: you gain 50...202...240 Health. Ends if damage drops your Health below 25% of your maximum."));
            Data.Add(new Skill("Illusionary Weaponry", Properties.Resources.Illusionary_Weaponry, Skill.Attributes.Illusion_Magic, Skill.Professions.Mesmer, 2, "5E 1C 25R - Elite Enchantment Spell. (30 seconds.) Deals 8...34...40 damage to foes in place of other damage or effects from melee attacks. You have +5 armor for each equipped Illusion Magic skill. Your melee attacks neither hit nor fail to hit.", true));
            Data.Add(new Skill("Sympathetic Visage", Properties.Resources.Sympathetic_Visage, Skill.Attributes.Illusion_Magic, Skill.Professions.Mesmer, 1, "10E 1C 20R - Enchantment Spell. (4...9...10 seconds.) All adjacent foes lose all adrenaline and 3 Energy whenever a melee attack hits target ally."));
            Data.Add(new Skill("Ignorance", Properties.Resources.Ignorance, Skill.Attributes.Domination_Magic, Skill.Professions.Mesmer, 1, "15E 1C 10R - Hex Spell. (8...18...20 seconds.) Target foe cannot use signets."));
            Data.Add(new Skill("Arcane Conundrum", Properties.Resources.Arcane_Conundrum, Skill.Attributes.Illusion_Magic, Skill.Professions.Mesmer, 3, "10E 2C 20R - Hex Spell. Also hexes foes adjacent to target (5...13...15 seconds). Doubles spell casting time. End effect: you gain 1...6...7 energy."));
            Data.Add(new Skill("Illusion of Haste", Properties.Resources.Illusion_of_Haste, Skill.Attributes.Illusion_Magic, Skill.Professions.Mesmer, 1, "5E 1C 5R - Enchantment Spell. (5...10...11 seconds.) You move 33% faster. Initial effect: removes Crippled condition. End effect: you are Crippled (3 seconds)."));
            Data.Add(new Skill("Channeling", Properties.Resources.Channeling, Skill.Attributes.Inspiration_Magic, Skill.Professions.Mesmer, 2, "5E 1C 15R - Enchantment Spell. (8...46...56 seconds.) You gain 1 Energy for each foe in the area whenever you cast a spell."));
            Data.Add(new Skill("Energy Surge", Properties.Resources.Energy_Surge, Skill.Attributes.Domination_Magic, Skill.Professions.Mesmer, 5, "5E 2C 15R - Elite Spell. Causes 1...8...10 Energy loss. Deals 9 damage to target and nearby foes for each point of Energy lost.", true));
            Data.Add(new Skill("Ether Feast", Properties.Resources.Ether_Feast, Skill.Attributes.Inspiration_Magic, Skill.Professions.Mesmer, 2, "5E 1C 8R - Spell. Cause 3 Energy loss. You gain 20...56...65 Health for each point of Energy lost."));
            Data.Add(new Skill("Ether Lord", Properties.Resources.Ether_Lord, Skill.Attributes.Inspiration_Magic, Skill.Professions.Mesmer, 1, "5E 2C 20R - Hex Spell. You lose all Energy. Target foe has -1...3...3 Energy degeneration and you have +1...3...3 Energy regeneration (5...9...10 seconds)."));
            Data.Add(new Skill("Energy Burn", Properties.Resources.Energy_Burn, Skill.Attributes.Domination_Magic, Skill.Professions.Mesmer, 4, "10E 2C 20R - Spell. Causes 1...8...10 Energy loss. Deals 9 damage for each point of Energy lost."));
            Data.Add(new Skill("Clumsiness", Properties.Resources.Clumsiness, Skill.Attributes.Illusion_Magic, Skill.Professions.Mesmer, 5, "10E 2C 8R - Hex Spell. (4 seconds.) Also hexes adjacent foes. Interrupts next attack. Interruption effect: deals 10...76...92 damage."));
            Data.Add(new Skill("Phantom Pain", Properties.Resources.Phantom_Pain, Skill.Attributes.Illusion_Magic, Skill.Professions.Mesmer, 2, "5E 2C 15R - Hex Spell. (10 seconds.) Causes -1...3...4 Health degeneration. End effect: inflicts Deep Wound condition (5...17...20 seconds)."));
            Data.Add(new Skill("Ethereal Burden", Properties.Resources.Ethereal_Burden, Skill.Attributes.Illusion_Magic, Skill.Professions.Mesmer, 2, "15E 3/2C 30R - Hex Spell. (10 seconds.) Target foe moves 50% slower. End effect: you gain 10...16...18 Energy."));
            Data.Add(new Skill("Guilt", Properties.Resources.Guilt, Skill.Attributes.Domination_Magic, Skill.Professions.Mesmer, 2, "5E 2C 25R - Hex Spell. (6 seconds.) Target foe's next spell fails and you steal 5...12...14 Energy. No effect unless this foe's spell targets one of your allies."));
            Data.Add(new Skill("Ineptitude", Properties.Resources.Ineptitude, Skill.Attributes.Illusion_Magic, Skill.Professions.Mesmer, 4, "10E 1C 15R - Elite Hex Spell. (4 seconds.) Also hexes foes adjacent to target. Deals 30...114...135 damage. Inflicts Blindness condition (10 seconds). No effect unless hexed foe attacks.", true));
            Data.Add(new Skill("Spirit of Failure", Properties.Resources.Spirit_of_Failure, Skill.Attributes.Inspiration_Magic, Skill.Professions.Mesmer, 2, "15E 3C 20R - Hex Spell. (30 seconds.) Target foe has 25% chance to miss. You gain 1...3...3 Energy whenever this foe misses."));
            Data.Add(new Skill("Mind Wrack", Properties.Resources.Mind_Wrack, Skill.Attributes.Domination_Magic, Skill.Professions.Mesmer, 2, "5E 1C 7R - Hex Spell. (5...33...40 seconds.) Causes 1 Energy loss each time foe is the target of your non-hex Mesmer skills. Deals 5...21...25 damage per point of Energy lost. If target foe's Energy drops to 0, it takes 15...83...100 damage and Mind Wrack ends."));
            Data.Add(new Skill("Wastrel's Worry", Properties.Resources.Wastrel_s_Worry, Skill.Attributes.Domination_Magic, Skill.Professions.Mesmer, 2, "5E 1/4C 1R - Hex Spell. (3 seconds). End effect: causes 20...84...100 damage to target and adjacent foes. No effect and ends early if target foe uses a skill."));
            Data.Add(new Skill("Shame", Properties.Resources.Shame, Skill.Attributes.Domination_Magic, Skill.Professions.Mesmer, 3, "10E 2C 30R - Hex Spell. (6 seconds.) Target foe's next spell fails and you steal 5...12...14 Energy. No effect unless this foe's spell targeted one of its allies."));
            Data.Add(new Skill("Panic", Properties.Resources.Panic, Skill.Attributes.Domination_Magic, Skill.Professions.Mesmer, 5, "10E 1C 15R - Elite Hex Spell. Also hexes foes near your target (1...8...10 second[s]). Interrupts all other nearby foes whenever a hexed foe successfully activates a skill.", true));
            Data.Add(new Skill("Migraine", Properties.Resources.Migraine, Skill.Attributes.Illusion_Magic, Skill.Professions.Mesmer, 3, "10E 2C 12R - Elite Hex Spell. (5...17...20 seconds.) Causes -1...7...8 Health degeneration and doubles skill activation time.", true));
            Data.Add(new Skill("Crippling Anguish", Properties.Resources.Crippling_Anguish, Skill.Attributes.Illusion_Magic, Skill.Professions.Mesmer, 4, "5E 1C 12R - Elite Hex Spell. (5...17...20 seconds.) Target foe moves and attacks 50% slower and has -1...7...8 Health degeneration.", true));
            Data.Add(new Skill("Fevered Dreams", Properties.Resources.Fevered_Dreams, Skill.Attributes.Illusion_Magic, Skill.Professions.Mesmer, 2, "10E 2C 10R - Elite Hex Spell. (10...22...25 seconds.) Foes in the area also have any new conditions that target foe acquires. Inflicts Dazed on target foe (1...3...3 second[s]) if that foe has two or more conditions.", true));
            Data.Add(new Skill("Soothing Images", Properties.Resources.Soothing_Images, Skill.Attributes.Illusion_Magic, Skill.Professions.Mesmer, 2, "15E 2C 8R - Hex Spell. Also hexes foe adjacent to target. (8...18...20 seconds). These foes cannot gain adrenaline."));
            Data.Add(new Skill("Cry of Frustration", Properties.Resources.Cry_of_Frustration, Skill.Attributes.Domination_Magic, Skill.Professions.Mesmer, 5, "10E 1/4C 15R - Spell. If target foe is using a skill, that foe and all foes in the area are interrupted and take 15...63...75 damage."));
            Data.Add(new Skill("Signet of Midnight", Properties.Resources.Signet_of_Midnight, Skill.Attributes.None, Skill.Professions.Mesmer, 2, "2C 10R - Elite Touch Signet. (15 seconds.) Inflicts Blindness condition. You suffer from Blindness (15 seconds).", true));
            Data.Add(new Skill("Signet of Weariness", Properties.Resources.Signet_of_Weariness, Skill.Attributes.Domination_Magic, Skill.Professions.Mesmer, 3, "2C 30R - Signet. Also affects foes near your target. Causes 3...7...8 Energy loss and inflicts Weakness (1...10...12 second[s])."));
            Data.Add(new Skill("Leech Signet", Properties.Resources.Leech_Signet, Skill.Attributes.Inspiration_Magic, Skill.Professions.Mesmer, 3, "1/4C 30R - Signet. Interrupts an action. Interruption effect: you gain 3...13...15 Energy if the action was a spell."));
            Data.Add(new Skill("Signet of Humility", Properties.Resources.Signet_of_Humility, Skill.Attributes.Inspiration_Magic, Skill.Professions.Mesmer, 2, "3C 20R - Signet. Disables elite skill (1...13...16 second[s]). Disables your non-Mesmer skills (10 seconds)."));
            Data.Add(new Skill("Keystone Signet", Properties.Resources.Keystone_Signet, Skill.Attributes.Fast_Casting, Skill.Professions.Mesmer, 2, "1C 15R - Elite Signet. (20 seconds.) Your next 0...5...6 signet[s] interrupt and deal 15...51...60 damage to other foes adjacent to your target. Initial Effect: recharges all of your other signets.", true));
            Data.Add(new Skill("Arcane Mimicry", Properties.Resources.Arcane_Mimicry, Skill.Attributes.None, Skill.Professions.Mesmer, 2, "15E 2C 60R - Spell. This skill becomes target ally's elite skill (20 seconds). Cannot self-target. No effect if target's elite skill is a form."));
            Data.Add(new Skill("Spirit Shackles", Properties.Resources.Spirit_Shackles, Skill.Attributes.Inspiration_Magic, Skill.Professions.Mesmer, 2, "10E 3C 8R - Hex Spell. (5...17...20). Target foe loses 5 Energy whenever it attacks."));
            Data.Add(new Skill("Shatter Hex", Properties.Resources.Shatter_Hex, Skill.Attributes.Domination_Magic, Skill.Professions.Mesmer, 3, "10E 1C 10R - Spell. Removes a hex from target ally. Removal effect: deals 30...102...120 damage to foes near this ally."));
            Data.Add(new Skill("Drain Enchantment", Properties.Resources.Drain_Enchantment, Skill.Attributes.Inspiration_Magic, Skill.Professions.Mesmer, 4, "5E 2C 20R - Spell. Removes an enchantment from target foe. Removal effect: you gain 8...15...17 Energy and 40...104...120 Health."));
            Data.Add(new Skill("Shatter Enchantment", Properties.Resources.Shatter_Enchantment, Skill.Attributes.Domination_Magic, Skill.Professions.Mesmer, 4, "10E 1C 20R - Spell. Removes an enchantment from target foe. Removal effect: deals 14...83...100 damage."));
            Data.Add(new Skill("Elemental Resistance", Properties.Resources.Elemental_Resistance, Skill.Attributes.Inspiration_Magic, Skill.Professions.Mesmer, 2, "10E 20R - Stance. (30...78...90 seconds.) You have +40 armor against elemental damage. You have -24...14...12 armor against physical damage."));
            Data.Add(new Skill("Physical Resistance", Properties.Resources.Physical_Resistance, Skill.Attributes.Inspiration_Magic, Skill.Professions.Mesmer, 1, "10E 20R - Stance. (30...78...90 seconds.) You have +40 armor against physical damage. You have -24...14...12 armor against elemental damage."));
            Data.Add(new Skill("Echo", Properties.Resources.Echo, Skill.Attributes.None, Skill.Professions.Mesmer, 2, "5E 1C 10R - Elite Enchantment Spell. (30 seconds.) Echo becomes the next skill you use (30 seconds).", true));
            Data.Add(new Skill("Arcane Echo", Properties.Resources.Arcane_Echo, Skill.Attributes.None, Skill.Professions.Mesmer, 2, "15E 2C 20R - Enchantment Spell. (20 seconds.) Arcane Echo becomes the next spell you use (20 seconds). This enchantment ends if you use any skill that is not a spell."));
            Data.Add(new Skill("Imagined Burden", Properties.Resources.Imagined_Burden, Skill.Attributes.Illusion_Magic, Skill.Professions.Mesmer, 1, "15E 1C 30R - Hex Spell. (8...18...20 seconds.) Target foe moves 50% slower."));
            Data.Add(new Skill("Chaos Storm", Properties.Resources.Chaos_Storm, Skill.Attributes.Domination_Magic, Skill.Professions.Mesmer, 5, "5E 2C 30R - Spell. Deals 5...21...25 damage and causes 0...2...2 Energy loss each second (10 seconds). Hits foes adjacent to target's initial location."));
            Data.Add(new Skill("Epidemic", Properties.Resources.Epidemic, Skill.Attributes.None, Skill.Professions.Mesmer, 4, "5E 1/4C 5R - Spell. Conditions on target foe transfer to adjacent foes."));
            Data.Add(new Skill("Energy Drain", Properties.Resources.Energy_Drain, Skill.Attributes.Inspiration_Magic, Skill.Professions.Mesmer, 3, "5E 1C 30R - Elite Spell. Causes 2...8...9 Energy loss. You gain 3 Energy for each point of Energy lost.", true));
            Data.Add(new Skill("Energy Tap", Properties.Resources.Energy_Tap, Skill.Attributes.Inspiration_Magic, Skill.Professions.Mesmer, 2, "5E 2C 30R - Spell. Causes 4...6...7 Energy loss. You gain 2 Energy for each point of Energy lost."));
            Data.Add(new Skill("Arcane Thievery", Properties.Resources.Arcane_Thievery, Skill.Attributes.Domination_Magic, Skill.Professions.Mesmer, 2, "10E 1C - Spell. (5...29...35 seconds.) Disables one random spell. This skill becomes that spell."));
            Data.Add(new Skill("Mantra of Recall", Properties.Resources.Mantra_of_Recall, Skill.Attributes.Inspiration_Magic, Skill.Professions.Mesmer, 2, "10E 1C 30R - Elite Enchantment Spell. (20 seconds.) End effect: you gain 10...22...25 Energy.", true));

            Data.Add(new Skill("Animate Bone Horror", Properties.Resources.Animate_Bone_Horror, Skill.Attributes.Death_Magic, Skill.Professions.Necromancer, 4, "10E 3C 5R - Spell. Creates a level 1...14...17 bone horror. Exploits a fresh corpse."));
            Data.Add(new Skill("Animate Bone Fiend", Properties.Resources.Animate_Bone_Fiend, Skill.Attributes.Death_Magic, Skill.Professions.Necromancer, 4, "25E 3C 5R - Spell. Creates a level 1...14...17 bone fiend that can attack at range. Exploits a fresh corpse."));
            Data.Add(new Skill("Animate Bone Minions", Properties.Resources.Animate_Bone_Minions, Skill.Attributes.Death_Magic, Skill.Professions.Necromancer, 3, "15E 3C 5R - Spell. Creates two level 0...10...12 bone minions. Exploits a fresh corpse."));
            Data.Add(new Skill("Grenth's Balance", Properties.Resources.Grenth_s_Balance, Skill.Attributes.None, Skill.Professions.Necromancer, 1, "10E 1/4C 10R - Elite Spell. You gain Health equal to half the difference between you and target, and this foe loses an equal amount. If this foe has less Health than you, you lose Health equal to half the difference.", true));
            Data.Add(new Skill("Verata's Gaze", Properties.Resources.Verata_s_Gaze, Skill.Attributes.Death_Magic, Skill.Professions.Necromancer, 2, "5E 1C 5R - Spell. Make target undead servant masterless. If it is already masterless, you become its master. 50% failure chance unless Death Magic 5 or more."));
            Data.Add(new Skill("Verata's Aura", Properties.Resources.Verata_s_Aura, Skill.Attributes.Death_Magic, Skill.Professions.Necromancer, 2, "33%HP 15E 3/4C 30R - Enchantment Spell. (120...264...300 seconds.) Become master of all hostile undead servants in the area. End effect: your undead servants become masterless. 50% failure chance unless Death Magic 5 or more."));
            Data.Add(new Skill("Deathly Chill", Properties.Resources.Deathly_Chill, Skill.Attributes.Death_Magic, Skill.Professions.Necromancer, 3, "10E 1C 5R - Spell. Deals 5...41...50 cold damage. Deals 5...41...50 more damage if target foe was above 50% Health."));
            Data.Add(new Skill("Verata's Sacrifice", Properties.Resources.Verata_s_Sacrifice, Skill.Attributes.Death_Magic, Skill.Professions.Necromancer, 1, "15%HP 10E 2C 60R - Spell. (5...9...10 seconds.) 10 undead allied servants gain +10 Health regeneration. Instantly recharges if you have control of 3 or fewer servants. Transfers all conditions from those servants to you."));
            Data.Add(new Skill("Well of Power", Properties.Resources.Well_of_Power, Skill.Attributes.Blood_Magic, Skill.Professions.Necromancer, 3, "5E 1C 15R - Elite Well Spell. (8...18...20 seconds.) Allies in this well have +1...5...6 Health regeneration and +2 Energy regeneration. Exploits a fresh corpse.", true));
            Data.Add(new Skill("Well of Blood", Properties.Resources.Well_of_Blood, Skill.Attributes.Blood_Magic, Skill.Professions.Necromancer, 4, "10E 1C 2R - Well Spell. (8...18...20 seconds.) Allies in this well have +1...5...6 Health regeneration. Exploits a fresh corpse."));
            Data.Add(new Skill("Well of Suffering", Properties.Resources.Well_of_Suffering, Skill.Attributes.Death_Magic, Skill.Professions.Necromancer, 2, "10E 1C 10R - Well Spell. (10...26...30 seconds.) Foes in this well have -1...5...6 Health degeneration. Exploits a fresh corpse."));
            Data.Add(new Skill("Well of the Profane", Properties.Resources.Well_of_the_Profane, Skill.Attributes.Death_Magic, Skill.Professions.Necromancer, 2, "25E 3C 10R - Well Spell. (8...18...20 seconds.) Foes in this well lose all enchantments and cannot be the target of further enchantments. Exploits a fresh corpse. 50% failure chance unless Death Magic 5 or more."));
            Data.Add(new Skill("Putrid Explosion", Properties.Resources.Putrid_Explosion, Skill.Attributes.Death_Magic, Skill.Professions.Necromancer, 3, "10E 1C 5R - Spell. Explodes a corpse, dealing 24...101...120 damage to foes near it. Exploits a fresh corpse."));
            Data.Add(new Skill("Soul Feast", Properties.Resources.Soul_Feast, Skill.Attributes.Death_Magic, Skill.Professions.Necromancer, 2, "10E 1C - Spell. You gain 50...234...280 Health. Exploits a fresh corpse."));
            Data.Add(new Skill("Necrotic Traversal", Properties.Resources.Necrotic_Traversal, Skill.Attributes.Death_Magic, Skill.Professions.Necromancer, 2, "5E 3/4C - Spell. Teleport to a corpse's location. Inflicts Poisoned condition (5...17...20 seconds). Affects all nearby foes. Exploits a fresh corpse."));
            Data.Add(new Skill("Consume Corpse", Properties.Resources.Consume_Corpse, Skill.Attributes.Death_Magic, Skill.Professions.Necromancer, 3, "10E 1C - Spell. Teleport to a corpse's location. You gain 25...85...100 Health and 5...17...20 Energy. Exploits a fresh corpse."));
            Data.Add(new Skill("Parasitic Bond", Properties.Resources.Parasitic_Bond, Skill.Attributes.Curses, Skill.Professions.Necromancer, 2, "5E 1C 2R - Hex Spell. (20 seconds.) Causes -1 Health degeneration. End effect: you are healed for 30...102...120 Health."));
            Data.Add(new Skill("Soul Barbs", Properties.Resources.Soul_Barbs, Skill.Attributes.Curses, Skill.Professions.Necromancer, 2, "10E 2C 20R - Hex Spell. (30 seconds.) Deals 15...27...30 damage when an enchantment or hex is cast on target foe."));
            Data.Add(new Skill("Barbs", Properties.Resources.Barbs, Skill.Attributes.Curses, Skill.Professions.Necromancer, 4, "10E 2C 5R - Hex Spell. (30 seconds.) Target foe takes 1...12...15 damage whenever it takes physical damage."));
            Data.Add(new Skill("Shadow Strike", Properties.Resources.Shadow_Strike, Skill.Attributes.Blood_Magic, Skill.Professions.Necromancer, 2, "10E 2C 15R - Spell. Deals 12...41...48 damage. Steal up to 12...41...48 Health if this foe was above 50% Health."));
            Data.Add(new Skill("Price of Failure", Properties.Resources.Price_of_Failure, Skill.Attributes.Curses, Skill.Professions.Necromancer, 3, "15E 2C 20R - Hex Spell. (30 seconds.) 25% chance to miss. Target foe takes 1...37...46 damage whenever it fails to hit."));
            Data.Add(new Skill("Death Nova", Properties.Resources.Death_Nova, Skill.Attributes.Death_Magic, Skill.Professions.Necromancer, 3, "5E 2C - Enchantment Spell. (30 seconds.) Deals 26...85...100 damage and inflicts Poisoned condition (15 seconds) on adjacent foes if target ally dies."));
            Data.Add(new Skill("Deathly Swarm", Properties.Resources.Deathly_Swarm, Skill.Attributes.Death_Magic, Skill.Professions.Necromancer, 3, "10E 2C 6R - Spell. Deathly Swarm flies out slowly and deals 30...78...90 cold damage. Hits two additional foes in the area."));
            Data.Add(new Skill("Rotting Flesh", Properties.Resources.Rotting_Flesh, Skill.Attributes.Death_Magic, Skill.Professions.Necromancer, 3, "15E 3C 3R - Spell. Inflicts Diseased condition (10...22...25 seconds)."));
            Data.Add(new Skill("Virulence", Properties.Resources.Virulence, Skill.Attributes.Death_Magic, Skill.Professions.Necromancer, 2, "5E 1C 15R - Elite Spell. Inflicts Disease, Poison, and Weakness conditions (3...13...15 seconds). No effect unless this foe already had a condition.", true));
            Data.Add(new Skill("Suffering", Properties.Resources.Suffering, Skill.Attributes.Curses, Skill.Professions.Necromancer, 3, "15E 1C 10R - Hex Spell. Also hexes foes near target (6...25...30 seconds). These foes have -0...2...3 Health degeneration."));
            Data.Add(new Skill("Life Siphon", Properties.Resources.Life_Siphon, Skill.Attributes.Blood_Magic, Skill.Professions.Necromancer, 3, "10E 1C 5R - Hex Spell. (12...22...24 seconds.) Target foe has -1...3...3 Health degeneration. You have +1...3...3 Health regeneration."));
            Data.Add(new Skill("Unholy Feast", Properties.Resources.Unholy_Feast, Skill.Attributes.Blood_Magic, Skill.Professions.Necromancer, 2, "15E 3/4C 8R - Spell. Steals 10...54...65 Health from 1...3...4 foe[s] in the area around you."));
            Data.Add(new Skill("Awaken the Blood", Properties.Resources.Awaken_the_Blood, Skill.Attributes.Blood_Magic, Skill.Professions.Necromancer, 3, "10E 1C 45R - Enchantment Spell. (20...39...44 seconds.) You have +2 Blood Magic and Curses. Sacrifice 50% more Health than normal."));
            Data.Add(new Skill("Desecrate Enchantments", Properties.Resources.Desecrate_Enchantments, Skill.Attributes.Curses, Skill.Professions.Necromancer, 3, "10E 2C 15R - Spell. Deals 6...49...60 damage to target and nearby foes. Deals 4...17...20 more damage for each enchantment on them."));
            Data.Add(new Skill("Tainted Flesh", Properties.Resources.Tainted_Flesh, Skill.Attributes.Death_Magic, Skill.Professions.Necromancer, 2, "5E 1C - Elite Enchantment Spell. (20...39...44 seconds.) Foes who hit target ally in melee become Diseased (3...13...15 seconds); this ally is immune to Disease.", true));
            Data.Add(new Skill("Aura of the Lich", Properties.Resources.Aura_of_the_Lich, Skill.Attributes.Death_Magic, Skill.Professions.Necromancer, 4, "15E 2C 45R - Elite Enchantment Spell. Exploit all corpses in earshot. Animates a level 1...14...17 bone horror, plus one for each exploited corpse. You have +1 Death Magic (5...37...45 seconds).", true));
            Data.Add(new Skill("Blood Renewal", Properties.Resources.Blood_Renewal, Skill.Attributes.Blood_Magic, Skill.Professions.Necromancer, 2, "15%HP 1E 1C 7R - Enchantment Spell. (7 seconds.) You have +3...5...6 Health regeneration. End effect: heals you for 40...160...190."));
            Data.Add(new Skill("Dark Aura", Properties.Resources.Dark_Aura, Skill.Attributes.Death_Magic, Skill.Professions.Necromancer, 2, "10E 1C 10R - Enchantment Spell. (30 seconds). Deals 5...41...50 damage to adjacent foes whenever target ally sacrifices Health. Damage cost: you lose 5...17...20 Health."));
            Data.Add(new Skill("Enfeeble", Properties.Resources.Enfeeble, Skill.Attributes.Curses, Skill.Professions.Necromancer, 3, "5E 1/4C 5R - Spell. Inflicts Weakness condition (10...26...30 seconds)."));
            Data.Add(new Skill("Enfeebling Blood", Properties.Resources.Enfeebling_Blood, Skill.Attributes.Curses, Skill.Professions.Necromancer, 5, "10%HP 1E 1C 8R - Spell. Inflicts Weakness condition (5...17...20 seconds) on this foe and nearby foes."));
            Data.Add(new Skill("Blood is Power", Properties.Resources.Blood_is_Power, Skill.Attributes.Blood_Magic, Skill.Professions.Necromancer, 3, "33%HP 1E 1/4C - Elite Enchantment Spell. (10 seconds.) +3...5...6 Energy regeneration. Cannot self-target.", true));
            Data.Add(new Skill("Blood of the Master", Properties.Resources.Blood_of_the_Master, Skill.Attributes.Death_Magic, Skill.Professions.Necromancer, 5, "5%+HP 5E 1C 2R - Spell. Heals your undead servants for 30...99...116. Healing cost: +2% Health sacrifice per servant healed."));
            Data.Add(new Skill("Spiteful Spirit", Properties.Resources.Spiteful_Spirit, Skill.Attributes.Curses, Skill.Professions.Necromancer, 5, "15E 2C 10R - Elite Hex Spell. (8...18...20 seconds.) Deals 5...29...35 damage to target and adjacent foes whenever this foe attacks or uses a skill.", true));
            Data.Add(new Skill("Malign Intervention", Properties.Resources.Malign_Intervention, Skill.Attributes.Death_Magic, Skill.Professions.Necromancer, 2, "10E 1C 5R - Hex Spell. (5...17...20 seconds.) Target foe receives 20% less from healing. If this foe dies while suffering from this hex, a level 1...14...17 masterless bone horror is summoned."));
            Data.Add(new Skill("Insidious Parasite", Properties.Resources.Insidious_Parasite, Skill.Attributes.Curses, Skill.Professions.Necromancer, 2, "15E 1C 12R - Hex Spell. (5...13...15 seconds.) Steal 15...39...45 Health whenever target foe hits with an attack."));
            Data.Add(new Skill("Spinal Shivers", Properties.Resources.Spinal_Shivers, Skill.Attributes.Curses, Skill.Professions.Necromancer, 2, "10E 2C 15R - Hex Spell. (10...34...40 seconds.) Cold damage interrupts target foe's skills. Interruption cost: you lose 10...6...5 Energy or Spinal Shivers ends."));
            Data.Add(new Skill("Wither", Properties.Resources.Wither, Skill.Attributes.Curses, Skill.Professions.Necromancer, 2, "10E 2C 10R - Elite Hex Spell. (5...29...35 seconds.) Causes -2...4...4 Health degeneration and -1 Energy degeneration. Deals 15...63...75 damage if target foe's Energy drops to 0. Ends if this foe's Energy drops to 0.", true));
            Data.Add(new Skill("Life Transfer", Properties.Resources.Life_Transfer, Skill.Attributes.Blood_Magic, Skill.Professions.Necromancer, 3, "5E 1C 20R - Elite Hex Spell. Also hexes foes adjacent to target (6...11...12 second). Causes -3...7...8 Health degeneration. You have +3...7...8 Health regeneration.", true));
            Data.Add(new Skill("Mark of Subversion", Properties.Resources.Mark_of_Subversion, Skill.Attributes.Blood_Magic, Skill.Professions.Necromancer, 2, "10E 2C 30R - Hex Spell. (6 seconds.) Target foe's next spell fails and you steal 10...76...92 Health. No effect unless this foe's spell targeted one of its allies."));
            Data.Add(new Skill("Soul Leech", Properties.Resources.Soul_Leech, Skill.Attributes.Blood_Magic, Skill.Professions.Necromancer, 3, "10E 2C 15R - Elite Hex Spell. (10 seconds.) Steal 16...67...80 Health whenever target foe casts a spell.", true));
            Data.Add(new Skill("Defile Flesh", Properties.Resources.Defile_Flesh, Skill.Attributes.Curses, Skill.Professions.Necromancer, 1, "10%HP 10E 1C 10R - Hex Spell. (5...29...35 seconds.) Reduces healing target foe receives by 33%. Only skills with the word \"heal\" in the description are affected."));
            Data.Add(new Skill("Demonic Flesh", Properties.Resources.Demonic_Flesh, Skill.Attributes.Blood_Magic, Skill.Professions.Necromancer, 2, "5E 1C 30R - Enchantment Spell. (30...54...60 seconds.) When using a skill on a foe, deal 5...17...20 shadow damage to all other foes adjacent to you."));
            Data.Add(new Skill("Barbed Signet", Properties.Resources.Barbed_Signet, Skill.Attributes.Blood_Magic, Skill.Professions.Necromancer, 2, "8%HP 1C 10R - Signet. Target and adjacent foes suffer from Bleeding (3...13...15 seconds)."));
            Data.Add(new Skill("Plague Signet", Properties.Resources.Plague_Signet, Skill.Attributes.Curses, Skill.Professions.Necromancer, 1, "1C 4R - Elite Signet. Transfers all conditions with 100...180...200% of their remaining durations from yourself to target foe. 50% failure chance unless Curses 5 or more.", true));
            Data.Add(new Skill("Dark Pact", Properties.Resources.Dark_Pact, Skill.Attributes.Blood_Magic, Skill.Professions.Necromancer, 3, "10%HP 1E 1C 2R - Spell. Deals 10...40...48 damage."));
            Data.Add(new Skill("Order of Pain", Properties.Resources.Order_of_Pain, Skill.Attributes.Blood_Magic, Skill.Professions.Necromancer, 2, "17%HP 10E 2C - Enchantment Spell. Enchants all party members (5 seconds). 3...13...16 more damage whenever these party members hit with physical damage."));
            Data.Add(new Skill("Faintheartedness", Properties.Resources.Faintheartedness, Skill.Attributes.Curses, Skill.Professions.Necromancer, 3, "10E 1C 8R - Hex Spell. (3...13...16 seconds.) Target foe attacks 50% slower and has -0...2...3 Health degeneration."));
            Data.Add(new Skill("Shadow of Fear", Properties.Resources.Shadow_of_Fear, Skill.Attributes.Curses, Skill.Professions.Necromancer, 4, "10E 2C 5R - Hex Spell. Also hexes foes adjacent to target (5...25...30 seconds). They attack 50% slower."));
            Data.Add(new Skill("Rigor Mortis", Properties.Resources.Rigor_Mortis, Skill.Attributes.Curses, Skill.Professions.Necromancer, 2, "10E 1C 20R - Hex Spell. (8...18...20 seconds.) Target foe cannot block."));
            Data.Add(new Skill("Dark Bond", Properties.Resources.Dark_Bond, Skill.Attributes.Blood_Magic, Skill.Professions.Necromancer, 2, "5E 2C 20R - Enchantment Spell. (30...54...60 seconds.) Transfers 75% of incoming damage from you to your nearest servant."));
            Data.Add(new Skill("Infuse Condition", Properties.Resources.Infuse_Condition, Skill.Attributes.Death_Magic, Skill.Professions.Necromancer, 2, "5E 1C 20R - Enchantment Spell. (15...51...60 seconds.) Whenever you receive a condition, it transfers from you to your closest undead servant."));
            Data.Add(new Skill("Malaise", Properties.Resources.Malaise, Skill.Attributes.Curses, Skill.Professions.Necromancer, 2, "5E 2C 2R - Hex Spell. (5...29...35 seconds.) Causes -1 Energy degeneration. Deals 5...41...50 damage if target foe's Energy drops to 0. You have -1 Health degeneration. Ends if this foe's Energy drops to 0."));
            Data.Add(new Skill("Rend Enchantments", Properties.Resources.Rend_Enchantments, Skill.Attributes.Curses, Skill.Professions.Necromancer, 2, "5E 2C 20R - Spell. Removes 5...8...9 enchantments from target foe. Removal cost: you lose 55...31...25 Health for each Monk enchantment removed."));
            Data.Add(new Skill("Lingering Curse", Properties.Resources.Lingering_Curse, Skill.Attributes.Curses, Skill.Professions.Necromancer, 2, "15E 1C 10R - Elite Hex Spell. (6...25...30 seconds.) Target and nearby foes have -0...2...3 Health degeneration and receive 20% less benefit from healing.", true));
            Data.Add(new Skill("Strip Enchantment", Properties.Resources.Strip_Enchantment, Skill.Attributes.Blood_Magic, Skill.Professions.Necromancer, 2, "10E 1C 20R - Spell. Removes 0...2...2 enchantment[s] from target foe. Removal effect: you steal 5...53...65 Health."));
            Data.Add(new Skill("Chilblains", Properties.Resources.Chilblains, Skill.Attributes.Curses, Skill.Professions.Necromancer, 2, "25E 2C 8R - Spell. Deals 10...37...44 cold damage to foes in the area around your target; removes 1...2...2 enchantment[s] from these foes. You are Poisoned (10 seconds)."));
            Data.Add(new Skill("Signet of Agony", Properties.Resources.Signet_of_Agony, Skill.Attributes.Blood_Magic, Skill.Professions.Necromancer, 2, "10%HP 3/4C 8R - Signet. Deals 10...58...70 damage to foes near you. You begin Bleeding (25 seconds)."));
            Data.Add(new Skill("Offering of Blood", Properties.Resources.Offering_of_Blood, Skill.Attributes.Blood_Magic, Skill.Professions.Necromancer, 2, "20%HP 1E 1/4C 15R - Elite Spell. You gain 8...18...20 Energy.", true));
            Data.Add(new Skill("Dark Fury", Properties.Resources.Dark_Fury, Skill.Attributes.Blood_Magic, Skill.Professions.Necromancer, 2, "17%HP 10E 3/4C 5R - Enchantment Spell. Enchants party members (5 seconds). These party members gain one strike of adrenaline each time they hit with an attack. 50% failure chance unless Blood Magic 5 or more."));
            Data.Add(new Skill("Order of the Vampire", Properties.Resources.Order_of_the_Vampire, Skill.Attributes.Blood_Magic, Skill.Professions.Necromancer, 2, "17%HP 5E 2C 5R - Elite Enchantment Spell. Enchants all party members (5 seconds.) These party members steal 3...13...16 Health with each physical damage attack. Party members under another Necromancer enchantment are not affected.", true));
            Data.Add(new Skill("Plague Sending", Properties.Resources.Plague_Sending, Skill.Attributes.Curses, Skill.Professions.Necromancer, 2, "10%HP 1E 1C 5R - Spell. Transfer 1...3...3 condition[s] and [its/their] remaining duration[s] from yourself to target foe and all adjacent foes."));
            Data.Add(new Skill("Mark of Pain", Properties.Resources.Mark_of_Pain, Skill.Attributes.Curses, Skill.Professions.Necromancer, 2, "10E 1C 20R - Hex Spell. (30 seconds.) Deals 10...34...40 damage to adjacent foes whenever target foe takes physical damage."));
            Data.Add(new Skill("Feast of Corruption", Properties.Resources.Feast_of_Corruption, Skill.Attributes.Curses, Skill.Professions.Necromancer, 3, "15E 2C 20R - Elite Spell. Deals 16...67...80 damage to target and adjacent foes. Steal 8...34...40 Health from each hexed foe.", true));
            Data.Add(new Skill("Taste of Death", Properties.Resources.Taste_of_Death, Skill.Attributes.Death_Magic, Skill.Professions.Necromancer, 1, "5E 1/4C - Spell. Steals 100...340...400 Health from allied undead servant."));
            Data.Add(new Skill("Vampiric Gaze", Properties.Resources.Vampiric_Gaze, Skill.Attributes.Blood_Magic, Skill.Professions.Necromancer, 2, "10E 1C 8R - Spell. Steals 18...52...60 Health."));
            Data.Add(new Skill("Plague Touch", Properties.Resources.Plague_Touch, Skill.Attributes.Curses, Skill.Professions.Necromancer, 2, "5E 3/4C - Touch Skill. Transfers 1...3...3 condition[s] from yourself to target foe."));
            Data.Add(new Skill("Vile Touch", Properties.Resources.Vile_Touch, Skill.Attributes.Death_Magic, Skill.Professions.Necromancer, 1, "10E 3/4C 2R - Touch Skill. Deals 20...56...65 damage."));
            Data.Add(new Skill("Vampiric Touch", Properties.Resources.Vampiric_Touch, Skill.Attributes.Blood_Magic, Skill.Professions.Necromancer, 2, "15E 3/4C 2R - Touch Skill. Steals 29...65...74 Health."));
            Data.Add(new Skill("Blood Ritual", Properties.Resources.Blood_Ritual, Skill.Attributes.Blood_Magic, Skill.Professions.Necromancer, 5, "17%HP 5E 2C 2R - Enchantment Spell. (8...13...14 seconds.) +3 Energy regeneration. Cannot self-target."));
            Data.Add(new Skill("Touch of Agony", Properties.Resources.Touch_of_Agony, Skill.Attributes.Blood_Magic, Skill.Professions.Necromancer, 2, "10%HP 1E 3/4C 3R - Touch Skill. Deals 20...50...58 damage."));
            Data.Add(new Skill("Weaken Armor", Properties.Resources.Weaken_Armor, Skill.Attributes.Curses, Skill.Professions.Necromancer, 4, "5E 1C 5R - Spell. Also affects adjacent foes. Inflicts Cracked Armor condition (5...17...20 seconds)."));

            Data.Add(new Skill("Windborne Speed", Properties.Resources.Windborne_Speed, Skill.Attributes.Air_Magic, Skill.Professions.Elementalist, 2, "10E 3/4C 5R - Enchantment Spell. (5...11...13 seconds.) Target ally moves 33% faster."));
            Data.Add(new Skill("Gale", Properties.Resources.Gale, Skill.Attributes.Air_Magic, Skill.Professions.Elementalist, 3, "10O 10E 1C 5R - Spell. Causes knock-down. 50% failure chance unless Air Magic 5 or more."));
            Data.Add(new Skill("Whirlwind", Properties.Resources.Whirlwind, Skill.Attributes.Air_Magic, Skill.Professions.Elementalist, 2, "5E 3/4C 8R - Spell. Hits foes adjacent to you. Deals 15...63...75 cold damage. Causes knock-down to attacking foes. Strikes nearby instead of adjacent if Overcast."));
            Data.Add(new Skill("Elemental Attunement", Properties.Resources.Elemental_Attunement, Skill.Attributes.Energy_Storage, Skill.Professions.Elementalist, 4, "10E 1C 20R - Elite Enchantment Spell. (25...53...60 seconds.) Your elemental attributes are increased by +1...2...2. You gain 50% of the Energy cost of any Air, Earth, Fire, and Water Magic skills you use.", true));
            Data.Add(new Skill("Armor of Earth", Properties.Resources.Armor_of_Earth, Skill.Attributes.Earth_Magic, Skill.Professions.Elementalist, 2, "10E 3/4C 15R - Enchantment Spell. (30 seconds.) You have +24...53...60 armor. You move 50...21...14% slower."));
            Data.Add(new Skill("Kinetic Armor", Properties.Resources.Kinetic_Armor, Skill.Attributes.Earth_Magic, Skill.Professions.Elementalist, 2, "15E 3C 60R - Enchantment Spell. (8 seconds.) You have +20...68...80 armor. Renewal bonus: cast a spell."));
            Data.Add(new Skill("Eruption", Properties.Resources.Eruption, Skill.Attributes.Earth_Magic, Skill.Professions.Elementalist, 3, "25E 2C 30R - Spell. Deals 10...34...40 earth damage each second (5 seconds). Hits foes near target's initial location. Inflicts Blindness condition (10 seconds)."));
            Data.Add(new Skill("Magnetic Aura", Properties.Resources.Magnetic_Aura, Skill.Attributes.Earth_Magic, Skill.Professions.Elementalist, 4, "10E 1C 12R - Enchantment Spell. (1...4...5 second[s].) Block the next attack against you and reflect 10...42...50 damage to the attacker. If you are Overcast, enchant party members in earshot."));
            Data.Add(new Skill("Earth Attunement", Properties.Resources.Earth_Attunement, Skill.Attributes.Earth_Magic, Skill.Professions.Elementalist, 5, "10E 1C 30R - Enchantment Spell. (36...55...60 seconds.) You gain 1 Energy plus 30% of the Energy cost when you use an Earth Magic skill."));
            Data.Add(new Skill("Earthquake", Properties.Resources.Earthquake, Skill.Attributes.Earth_Magic, Skill.Professions.Elementalist, 3, "10O 25E 3C 15R - Spell. Deals 26...85...100 earth damage. Also hits foes near target. Causes knock-down."));
            Data.Add(new Skill("Stoning", Properties.Resources.Stoning, Skill.Attributes.Earth_Magic, Skill.Professions.Elementalist, 3, "15E 1C 5R - Spell. Projectile: deals 45...93...105 earth damage. Causes knock-down if target foe is Weakened."));
            Data.Add(new Skill("Stone Daggers", Properties.Resources.Stone_Daggers, Skill.Attributes.Earth_Magic, Skill.Professions.Elementalist, 2, "5E 1C - Spell. Two projectiles: each deals 8...28...33 earth damage. If Overcast, cause Bleeding for 1...4...5 second[s]."));
            Data.Add(new Skill("Grasping Earth", Properties.Resources.Grasping_Earth, Skill.Attributes.Earth_Magic, Skill.Professions.Elementalist, 2, "10E 3/4C 12R - Hex Spell. Hexes foes near you for (5...17...20 seconds). These foes move 50% slower."));
            Data.Add(new Skill("Aftershock", Properties.Resources.Aftershock, Skill.Attributes.Earth_Magic, Skill.Professions.Elementalist, 2, "10E 3/4C 10R - Spell. Deals 26...85...100 earth damage to nearby foes. Deals 10...56...68 more earth damage to knocked down foes."));
            Data.Add(new Skill("Ward Against Elements", Properties.Resources.Ward_Against_Elements, Skill.Attributes.Earth_Magic, Skill.Professions.Elementalist, 4, "15E 1C 20R - Ward Spell. (8...18...20 seconds.) Allies in this ward have +24 armor against elemental damage. Spirits are unaffected."));
            Data.Add(new Skill("Ward Against Melee", Properties.Resources.Ward_Against_Melee, Skill.Attributes.Earth_Magic, Skill.Professions.Elementalist, 5, "15E 1C 30R - Ward Spell. (5...17...20 seconds.) Allies in this ward have a 50% chance to block melee attacks. Allied spirits are not affected."));
            Data.Add(new Skill("Ward Against Foes", Properties.Resources.Ward_Against_Foes, Skill.Attributes.Earth_Magic, Skill.Professions.Elementalist, 4, "15E 1C 20R - Ward Spell. (8...18...20 seconds.) Foes in this ward move 50% slower."));
            Data.Add(new Skill("Ether Prodigy", Properties.Resources.Ether_Prodigy, Skill.Attributes.Energy_Storage, Skill.Professions.Elementalist, 3, "10O 5E 1C 5R - Elite Enchantment Spell. (8...18...20 seconds.) You have +6 Energy regeneration. End effect: lose 2 Health for each point of Energy you have. Lose all enchantments.", true));
            Data.Add(new Skill("Incendiary Bonds", Properties.Resources.Incendiary_Bonds, Skill.Attributes.Fire_Magic, Skill.Professions.Elementalist, 3, "5O 10E 1C 7R - Hex Spell. (3 seconds.) End effect: deals 20...68...80 fire damage and inflicts Burning condition (1...3...3 second[s]) to foes near your target. Also triggers if foe dies."));
            Data.Add(new Skill("Aura of Restoration", Properties.Resources.Aura_of_Restoration, Skill.Attributes.Energy_Storage, Skill.Professions.Elementalist, 2, "5E 1/4C 20R - Enchantment Spell. (60 seconds.) You gain 0...1...1 Energy and are healed for 200...440...500% of the Energy cost each time you cast a spell."));
            Data.Add(new Skill("Ether Renewal", Properties.Resources.Ether_Renewal, Skill.Attributes.Energy_Storage, Skill.Professions.Elementalist, 2, "10E 1C 30R - Elite Enchantment Spell. (5...17...20 seconds.) Each time you cast a spell, you gain 1...3...4 Energy and 5...17...20 Health for each enchantment on you.", true));
            Data.Add(new Skill("Conjure Flame", Properties.Resources.Conjure_Flame, Skill.Attributes.Fire_Magic, Skill.Professions.Elementalist, 4, "10E 1C 45R - Enchantment Spell. (60 seconds.) Your attacks hit for +5...17...20 fire damage. No effect unless your weapon deals fire damage."));
            Data.Add(new Skill("Inferno", Properties.Resources.Inferno, Skill.Attributes.Fire_Magic, Skill.Professions.Elementalist, 4, "10E 3/4C 10R - Spell. Deals 30...114...135 fire damage to foes adjacent to you."));
            Data.Add(new Skill("Fire Attunement", Properties.Resources.Fire_Attunement, Skill.Attributes.Fire_Magic, Skill.Professions.Elementalist, 5, "10E 1C 30R - Enchantment Spell. (36...55...60 seconds.) You gain 1 Energy plus 30% of the Energy cost when you use a Fire Magic skill."));
            Data.Add(new Skill("Mind Burn", Properties.Resources.Mind_Burn, Skill.Attributes.Fire_Magic, Skill.Professions.Elementalist, 4, "5O 5E 1C 5R - Elite Spell. Deals 15...51...60 fire damage. If you have more energy than target foe, deals 15...51...60 more fire damage and inflicts Burning (1...8...10 second[s]). Also hits adjacent foes.", true));
            Data.Add(new Skill("Fireball", Properties.Resources.Fireball, Skill.Attributes.Fire_Magic, Skill.Professions.Elementalist, 4, "10E 3/2C 7R - Spell. Projectile: deals 7...91...112 fire damage to target and adjacent foes."));
            Data.Add(new Skill("Meteor", Properties.Resources.Meteor, Skill.Attributes.Fire_Magic, Skill.Professions.Elementalist, 3, "10O 5E 2C 30R - Spell. Deals 7...91...112 fire damage and causes knock-down. Also hits foes adjacent to target foe."));
            Data.Add(new Skill("Flame Burst", Properties.Resources.Flame_Burst, Skill.Attributes.Fire_Magic, Skill.Professions.Elementalist, 3, "15E 3/4C 5R - Spell. Deals 15...99...120 fire damage to all foes near you."));
            Data.Add(new Skill("Rodgort's Invocation", Properties.Resources.Rodgort_s_Invocation, Skill.Attributes.Fire_Magic, Skill.Professions.Elementalist, 5, "25E 2C 8R - Spell. Deals 15...99...120 fire damage. Also affects foes near your target. Inflicts Burning condition (1...3...3 second[s])."));
            Data.Add(new Skill("Mark of Rodgort", Properties.Resources.Mark_of_Rodgort, Skill.Attributes.Fire_Magic, Skill.Professions.Elementalist, 2, "15E 1C 15R - Hex Spell. Also hexes foes near your target (10...30...35 seconds). Inflicts Burning condition (1...3...4 second[s]) when these foes take fire damage."));
            Data.Add(new Skill("Immolate", Properties.Resources.Immolate, Skill.Attributes.Fire_Magic, Skill.Professions.Elementalist, 3, "10E 1C 5R - Spell. Deals 20...64...75 fire damage. Inflicts Burning condition (1...3...3 second[s])."));
            Data.Add(new Skill("Meteor Shower", Properties.Resources.Meteor_Shower, Skill.Attributes.Fire_Magic, Skill.Professions.Elementalist, 4, "10O 25E 5C 60R - Spell. Deals 7...91...112 fire damage and causes knock-down every 3 seconds (9 seconds). Hits foes adjacent to target's initial location."));
            Data.Add(new Skill("Phoenix", Properties.Resources.Phoenix, Skill.Attributes.Fire_Magic, Skill.Professions.Elementalist, 3, "10E 3/2C 7R - Spell. Projectile: deals 10...50...60 fire damage to target and nearby foes. Heals allies for 20...68...80 if you are Overcast."));
            Data.Add(new Skill("Flare", Properties.Resources.Flare, Skill.Attributes.Fire_Magic, Skill.Professions.Elementalist, 2, "5E 1C - Spell. Projectile: deals 20...56...65 fire damage. If Overcast, strikes adjacent."));
            Data.Add(new Skill("Lava Font", Properties.Resources.Lava_Font, Skill.Attributes.Fire_Magic, Skill.Professions.Elementalist, 4, "10E 3/2C 4R - Spell. Deals 5...41...50 fire damage each second (5 seconds). Hits foes adjacent to your initial location. If Overcast, range increased to nearby."));
            Data.Add(new Skill("Searing Heat", Properties.Resources.Searing_Heat, Skill.Attributes.Fire_Magic, Skill.Professions.Elementalist, 3, "15E 2C 25R - Spell. Deals 10...34...40 fire damage each second (5 seconds). Hits foes near target's initial location. End effect: inflicts Burning condition (3 seconds)."));
            Data.Add(new Skill("Fire Storm", Properties.Resources.Fire_Storm, Skill.Attributes.Fire_Magic, Skill.Professions.Elementalist, 3, "10E 2C 20R - Spell. Deals 5...29...35 fire damage each second (10 seconds). Hits foes adjacent to target's initial location."));
            Data.Add(new Skill("Glyph of Elemental Power", Properties.Resources.Glyph_of_Elemental_Power, Skill.Attributes.None, Skill.Professions.Elementalist, 4, "5E 1C 5R - Glyph. (25 seconds.) Boosts your elemental attributes by +2 for your next 10 spells."));
            Data.Add(new Skill("Glyph of Energy", Properties.Resources.Glyph_of_Energy, Skill.Attributes.Energy_Storage, Skill.Professions.Elementalist, 3, "5E 1C 25R - Elite Glyph. Your next 1...3...3 spell[s] [does/do] not cause Overcast and cost[s] 10...22...25 less Energy. Gain 1...2...2 to all elemental attributes.", true));
            Data.Add(new Skill("Glyph of Lesser Energy", Properties.Resources.Glyph_of_Lesser_Energy, Skill.Attributes.Energy_Storage, Skill.Professions.Elementalist, 4, "5E 1C 30R - Glyph. (15 seconds.) Your next 2 spells cost 10...16...18 less Energy."));
            Data.Add(new Skill("Glyph of Concentration", Properties.Resources.Glyph_of_Concentration, Skill.Attributes.None, Skill.Professions.Elementalist, 1, "5E 1C 10R - Glyph. (15 seconds.) Your next 1 spell cannot be interrupted and is unaffected by the Dazed condition."));
            Data.Add(new Skill("Glyph of Sacrifice", Properties.Resources.Glyph_of_Sacrifice, Skill.Attributes.None, Skill.Professions.Elementalist, 2, "5E 1C 15R - Glyph. (15 seconds.) Your next spell casts instantly but takes an additional 30 seconds to recharge. Ends if you use a non-spell skill."));
            Data.Add(new Skill("Glyph of Renewal", Properties.Resources.Glyph_of_Renewal, Skill.Attributes.None, Skill.Professions.Elementalist, 2, "5E 1C 10R - Elite Glyph. (15 seconds.) Your next spell recharges instantly.", true));
            Data.Add(new Skill("Rust", Properties.Resources.Rust, Skill.Attributes.Water_Magic, Skill.Professions.Elementalist, 2, "10E 1C 8R - Hex Spell. Deals 10...58...70 cold damage to target and adjacent foes. Hexes target and adjacent foes (5...17...20 seconds). Doubles signet activation time. Interrupts and disables signets for 1...8...10 second[s] if you are Overcast."));
            Data.Add(new Skill("Lightning Surge", Properties.Resources.Lightning_Surge, Skill.Attributes.Air_Magic, Skill.Professions.Elementalist, 3, "10E 1C 10R - Elite Hex Spell. (3 seconds.) End effect: deals 14...83...100 lightning damage, causes knock-down, and inflicts Cracked Armor (5...17...20 seconds). 25% armor penetration.", true));
            Data.Add(new Skill("Armor of Frost", Properties.Resources.Armor_of_Frost, Skill.Attributes.Water_Magic, Skill.Professions.Elementalist, 2, "5E 1C 20R - Enchantment Spell. (10...29...34 seconds.) You have +40 armor against physical damage and have +1 Water Magic."));
            Data.Add(new Skill("Conjure Frost", Properties.Resources.Conjure_Frost, Skill.Attributes.Water_Magic, Skill.Professions.Elementalist, 4, "10E 1C 45R - Enchantment Spell. (60 seconds.) Your attacks hit for +5...17...20 cold damage. No effect unless your weapon deals cold damage."));
            Data.Add(new Skill("Water Attunement", Properties.Resources.Water_Attunement, Skill.Attributes.Water_Magic, Skill.Professions.Elementalist, 5, "10E 1C 30R - Enchantment Spell. (36...55...60 seconds.) You gain 1 Energy plus 30% of the Energy cost when you use a Water Magic skill."));
            Data.Add(new Skill("Mind Freeze", Properties.Resources.Mind_Freeze, Skill.Attributes.Water_Magic, Skill.Professions.Elementalist, 3, "5O 5E 1C 5R - Elite Hex Spell. Deals 10...50...60 cold damage. If you have more Energy than target foe, deals +10...50...60 cold damage and causes 90% slower movement (1...4...5 second[s]).", true));
            Data.Add(new Skill("Ice Prison", Properties.Resources.Ice_Prison, Skill.Attributes.Water_Magic, Skill.Professions.Elementalist, 1, "10E 2C 30R - Hex Spell. (8...18...20 seconds.) Target foe moves 66% slower."));
            Data.Add(new Skill("Ice Spikes", Properties.Resources.Ice_Spikes, Skill.Attributes.Water_Magic, Skill.Professions.Elementalist, 4, "15E 3/2C 10R - Hex Spell. Also hexes foes adjacent to target (2...5...6 seconds). These foes move 66% slower. Initial effect: deals 20...68...80 cold damage."));
            Data.Add(new Skill("Frozen Burst", Properties.Resources.Frozen_Burst, Skill.Attributes.Water_Magic, Skill.Professions.Elementalist, 4, "15E 3/4C 8R - Hex Spell. Hexes foes near you. These foes move 66% slower (3...7...8 seconds). Initial effect: deals 10...70...85 cold damage to foes near you."));
            Data.Add(new Skill("Shard Storm", Properties.Resources.Shard_Storm, Skill.Attributes.Water_Magic, Skill.Professions.Elementalist, 2, "10E 1C 10R - Hex Spell. Projectile: deals 10...70...85 cold damage. Target foe moves 66% slower (2...5...6 seconds)."));
            Data.Add(new Skill("Ice Spear", Properties.Resources.Ice_Spear, Skill.Attributes.Water_Magic, Skill.Professions.Elementalist, 3, "5E 1C - Enchantment Spell. Projectile: deals 10...50...60 cold damage. Gain +1...3...4 Health regeneration for 5 seconds if Overcast."));
            Data.Add(new Skill("Maelstrom", Properties.Resources.Maelstrom, Skill.Attributes.Water_Magic, Skill.Professions.Elementalist, 3, "10O 25E 2C 30R - Spell. Deals 10...22...25 cold damage and interrupts spells each second (10 seconds). Hits foes adjacent to target's initial location."));
            Data.Add(new Skill("Iron Mist", Properties.Resources.Iron_Mist, Skill.Attributes.Earth_Magic, Skill.Professions.Elementalist, 1, "10E 1C 20R - Enchantment Spell. (8...14...15 seconds.) Gain +15 armor. Air Magic spells that target a foe activate and recharge 25% faster, but you are Overcast by 3 points."));
            Data.Add(new Skill("Crystal Wave", Properties.Resources.Crystal_Wave, Skill.Attributes.Earth_Magic, Skill.Professions.Elementalist, 2, "15E 3/4C 15R - Spell. Deals 10...58...70 damage to all foes adjacent to you. Those foes lose all conditions and take 5...13...15 damage for each condition removed."));
            Data.Add(new Skill("Obsidian Flesh", Properties.Resources.Obsidian_Flesh, Skill.Attributes.Earth_Magic, Skill.Professions.Elementalist, 1, "25E 1C 30R - Elite Enchantment Spell. (8...18...20 seconds.) You have +20 armor and enemy spells cannot target you. You cannot attack and have -2 energy degeneration.", true));
            Data.Add(new Skill("Obsidian Flame", Properties.Resources.Obsidian_Flame, Skill.Attributes.Earth_Magic, Skill.Professions.Elementalist, 5, "5O 5E 3/2C 5R - Spell. Deals 22...94...112 damage."));
            Data.Add(new Skill("Blinding Flash", Properties.Resources.Blinding_Flash, Skill.Attributes.Air_Magic, Skill.Professions.Elementalist, 3, "10E 3/4C 8R - Spell. Inflicts Blindness condition (3...7...8 seconds)."));
            Data.Add(new Skill("Conjure Lightning", Properties.Resources.Conjure_Lightning, Skill.Attributes.Air_Magic, Skill.Professions.Elementalist, 4, "10E 1C 45R - Enchantment Spell. (60 seconds.) Your attacks hit for +5...17...20 lightning damage. No effect unless your weapon deals lightning damage."));
            Data.Add(new Skill("Lightning Strike", Properties.Resources.Lightning_Strike, Skill.Attributes.Air_Magic, Skill.Professions.Elementalist, 3, "5E 1C 5R - Hex Spell. Deals 5...41...50 lightning damage. 25% armor penetration. Hex for 3 seconds if Overcast. End effect: deals 5...41...50 lightning damage."));
            Data.Add(new Skill("Chain Lightning", Properties.Resources.Chain_Lightning, Skill.Attributes.Air_Magic, Skill.Professions.Elementalist, 5, "5O 10E 2C 6R - Spell. Deals 10...70...85 lightning damage. Also hits two foes near your target. 25% armor penetration."));
            Data.Add(new Skill("Enervating Charge", Properties.Resources.Enervating_Charge, Skill.Attributes.Air_Magic, Skill.Professions.Elementalist, 3, "10E 1C 8R - Spell. Deals 25...45...50 lightning damage. Inflicts Weakness condition (5...17...20 seconds). 25% armor penetration."));
            Data.Add(new Skill("Air Attunement", Properties.Resources.Air_Attunement, Skill.Attributes.Air_Magic, Skill.Professions.Elementalist, 5, "10E 1C 30R - Enchantment Spell. (36...55...60 seconds.) You gain 1 Energy plus 30% of the Energy cost whenever you use an Air Magic skill."));
            Data.Add(new Skill("Mind Shock", Properties.Resources.Mind_Shock, Skill.Attributes.Air_Magic, Skill.Professions.Elementalist, 3, "5O 5E 1C 8R - Elite Spell. Deals 10...42...50 lightning damage. If you have more Energy than target foe, deals +10...42...50 lightning damage and causes knockdown. 25% armor penetration.", true));
            Data.Add(new Skill("Glimmering Mark", Properties.Resources.Glimmering_Mark, Skill.Attributes.Air_Magic, Skill.Professions.Elementalist, 3, "10E 1C 15R - Elite Hex Spell. (10 seconds.) Deals 5...21...25 damage each second to target and adjacent foes. Inflicts Blindness on foes using attack skills. Ends if you use a skill that targets this foe.", true));
            Data.Add(new Skill("Thunderclap", Properties.Resources.Thunderclap, Skill.Attributes.Air_Magic, Skill.Professions.Elementalist, 4, "10E 1C 8R - Elite Spell. Deals 10...42...50 lightning damage. Also strikes adjacent foes. Inflicts Cracked Armor and Weakness (5...17...20 seconds). Causes interrupt. 25% armor penetration.", true));
            Data.Add(new Skill("Lightning Orb", Properties.Resources.Lightning_Orb, Skill.Attributes.Air_Magic, Skill.Professions.Elementalist, 4, "15E 2C 5R - Spell. Projectile: deals 10...82...100 lightning damage. 25% armor penetration."));
            Data.Add(new Skill("Lightning Javelin", Properties.Resources.Lightning_Javelin, Skill.Attributes.Air_Magic, Skill.Professions.Elementalist, 3, "5E 1C 5R - Spell. Projectile: Deals 15...43...50 lightning damage. Interrupts if your target is attacking. 25% armor penetration. Strikes all foes between you and your target."));
            Data.Add(new Skill("Shock", Properties.Resources.Shock, Skill.Attributes.Air_Magic, Skill.Professions.Elementalist, 4, "10O 5E 3/4C 10R - Touch Skill. Deals 10...50...60 lightning damage. Causes knock-down. 25% armor penetration."));
            Data.Add(new Skill("Lightning Touch", Properties.Resources.Lightning_Touch, Skill.Attributes.Air_Magic, Skill.Professions.Elementalist, 4, "5E 3/4C 10R - Touch Skill. Deals 10...50...60 lightning damage. Also hits foes adjacent to target foe. Inflicts Blind (1...3...4 second[s]) and Cracked Armor (1...8...10 second[s]) on all struck foes. 25% armor penetration."));
            Data.Add(new Skill("Swirling Aura", Properties.Resources.Swirling_Aura, Skill.Attributes.Water_Magic, Skill.Professions.Elementalist, 4, "10E 1C 15R - Enchantment Spell. (5 seconds.) Gives 1...5...6 Health regeneration and a 50% chance to block projectiles. If Overcast, also enchants party members in earshot."));
            Data.Add(new Skill("Deep Freeze", Properties.Resources.Deep_Freeze, Skill.Attributes.Water_Magic, Skill.Professions.Elementalist, 5, "25E 2C 15R - Hex Spell. Also hexes foes in the area of your target (10 seconds). These foes move 66% slower. Initial effect: deals 10...70...85 cold damage."));
            Data.Add(new Skill("Blurred Vision", Properties.Resources.Blurred_Vision, Skill.Attributes.Water_Magic, Skill.Professions.Elementalist, 5, "10E 1C 12R - Hex Spell. Also hexes foes adjacent to target (4...9...10 seconds). They have 50% chance to miss."));
            Data.Add(new Skill("Mist Form", Properties.Resources.Mist_Form, Skill.Attributes.Water_Magic, Skill.Professions.Elementalist, 5, "5E 1C 20R - Elite Enchantment Spell. (10...38...45 seconds.) Take 33% less damage from foes hexed with Water Magic. Heals non-spirit allies in earshot for 50...210...250% of the energy cost of your elemental spells.", true));
            Data.Add(new Skill("Water Trident", Properties.Resources.Water_Trident, Skill.Attributes.Water_Magic, Skill.Professions.Elementalist, 4, "5E 1C 3R - Elite Spell. Fast Projectile: deals 10...74...90 cold damage and knocks-down moving foes. Shoots 2 additional projectiles at adjacent foes.", true));
            Data.Add(new Skill("Armor of Mist", Properties.Resources.Armor_of_Mist, Skill.Attributes.Water_Magic, Skill.Professions.Elementalist, 3, "10E 1C 30R - Enchantment Spell. (8...18...20 seconds.) You have +10...34...40 armor and move 33% faster."));
            Data.Add(new Skill("Ward Against Harm", Properties.Resources.Ward_Against_Harm, Skill.Attributes.Water_Magic, Skill.Professions.Elementalist, 4, "15E 1C 20R - Elite Ward Spell. (5...13...15 seconds.) Allies in this ward have +1...3...3 Health regeneration, +12...22...24 armor, and +12...22...24 additional armor against elemental damage. Spirits are unaffected. This spell is disabled for 20 seconds.", true));

            Data.Add(new Skill("Smite", Properties.Resources.Smite, Skill.Attributes.Smiting_Prayers, Skill.Professions.Monk, 2, "10E 1C 10R - Spell. Deals 10...46...55 holy damage. Deals 10...30...35 more holy damage if target is attacking."));
            Data.Add(new Skill("Life Bond", Properties.Resources.Life_Bond, Skill.Attributes.Protection_Prayers, Skill.Professions.Monk, 2, "-1ER 10E 2C - Enchantment Spell. Half of the damage target ally takes from attacks is redirected to you. Redirected damage is reduced by 3...25...30. Cannot self-target."));
            Data.Add(new Skill("Balthazar's Spirit", Properties.Resources.Balthazar_s_Spirit, Skill.Attributes.Smiting_Prayers, Skill.Professions.Monk, 2, "-1ER 10E 2C - Enchantment Spell. Target ally gains adrenaline and 1 Energy when taking damage."));
            Data.Add(new Skill("Strength of Honor", Properties.Resources.Strength_of_Honor, Skill.Attributes.Smiting_Prayers, Skill.Professions.Monk, 4, "-1ER 10E 2C 15R - Enchantment Spell. Target ally deals 5...21...25 more damage in melee."));
            Data.Add(new Skill("Life Attunement", Properties.Resources.Life_Attunement, Skill.Attributes.Protection_Prayers, Skill.Professions.Monk, 1, "-1ER 10E 2C - Enchantment Spell. Target ally gains 14...43...50% more Health when healed. This ally deals 30% less damage with attacks."));
            Data.Add(new Skill("Protective Spirit", Properties.Resources.Protective_Spirit, Skill.Attributes.Protection_Prayers, Skill.Professions.Monk, 4, "10E 1/4C 5R - Enchantment Spell. (5...19...23 seconds.) Incoming damage is reduced to 10% of target ally's maximum Health."));
            Data.Add(new Skill("Divine Intervention", Properties.Resources.Divine_Intervention, Skill.Attributes.Divine_Favor, Skill.Professions.Monk, 1, "5E 1/4C 30R - Enchantment Spell. (10 seconds.) Negates the next fatal damage target ally takes. Negation effect: heals for 26...197...240."));
            Data.Add(new Skill("Symbol of Wrath", Properties.Resources.Symbol_of_Wrath, Skill.Attributes.Smiting_Prayers, Skill.Professions.Monk, 2, "5E 2C 30R - Spell. Deals 8...27...32 holy damage each second (5 seconds). Hits foes adjacent to your initial location."));
            Data.Add(new Skill("Retribution", Properties.Resources.Retribution, Skill.Attributes.Smiting_Prayers, Skill.Professions.Monk, 1, "-1ER 10E 2C - Enchantment Spell. Deals 33% of each attack's damage (maximum 5...17...20) back to the source."));
            Data.Add(new Skill("Holy Wrath", Properties.Resources.Holy_Wrath, Skill.Attributes.Smiting_Prayers, Skill.Professions.Monk, 3, "10E 1C 10R - Enchantment Spell. (10...26...30 seconds). Deals 66% of each attack's damage (maximum 5...41...50) back to source. Ends after dealing damage 1...8...10 time[s]. Cannot self-target."));
            Data.Add(new Skill("Essence Bond", Properties.Resources.Essence_Bond, Skill.Attributes.None, Skill.Professions.Monk, 2, "-1ER 10E 2C - Enchantment Spell. You gain 1 Energy whenever target ally takes physical or elemental damage."));
            Data.Add(new Skill("Scourge Healing", Properties.Resources.Scourge_Healing, Skill.Attributes.Smiting_Prayers, Skill.Professions.Monk, 2, "10E 2C 5R - Hex Spell. (30 seconds.) Whenever target foe is healed, the healer takes 15...67...80 holy damage."));
            Data.Add(new Skill("Banish", Properties.Resources.Banish, Skill.Attributes.Smiting_Prayers, Skill.Professions.Monk, 2, "5E 1C 10R - Spell. Deals 20...49...56 holy damage. Deals double damage to summoned creatures."));
            Data.Add(new Skill("Scourge Sacrifice", Properties.Resources.Scourge_Sacrifice, Skill.Attributes.Smiting_Prayers, Skill.Professions.Monk, 2, "10E 1C 5R - Hex Spell. Also hexes foes adjacent to target (8...18...20 seconds). Doubles Health sacrifice."));
            Data.Add(new Skill("Vigorous Spirit", Properties.Resources.Vigorous_Spirit, Skill.Attributes.Healing_Prayers, Skill.Professions.Monk, 4, "5E 1/4C 4R - Enchantment Spell. (30 seconds.) Heals for 5...17...20 each time target ally attacks or casts a spell."));
            Data.Add(new Skill("Watchful Spirit", Properties.Resources.Watchful_Spirit, Skill.Attributes.Divine_Favor, Skill.Professions.Monk, 2, "-1ER 15E 1C 5R - Enchantment Spell. +2 Health regeneration. End effect: heals for 30...150...180."));
            Data.Add(new Skill("Blessed Aura", Properties.Resources.Blessed_Aura, Skill.Attributes.Divine_Favor, Skill.Professions.Monk, 2, "-1ER 10E 2C 2R - Enchantment Spell. Monk enchantments you cast last 10...30...35% longer."));
            Data.Add(new Skill("Aegis", Properties.Resources.Aegis, Skill.Attributes.Protection_Prayers, Skill.Professions.Monk, 4, "10E 2C 30R - Enchantment Spell. Enchants all party members within earshot (5...10...11 seconds). 50% chance to block."));
            Data.Add(new Skill("Guardian", Properties.Resources.Guardian, Skill.Attributes.Protection_Prayers, Skill.Professions.Monk, 5, "5E 1C 4R - Enchantment Spell. (2...6...7 seconds.) 50% chance to block."));
            Data.Add(new Skill("Shield of Deflection", Properties.Resources.Shield_of_Deflection, Skill.Attributes.Protection_Prayers, Skill.Professions.Monk, 3, "10E 1/4C 10R - Elite Enchantment Spell. (3...9...10 seconds.) 75% chance to block. +15...27...30 armor.", true));
            Data.Add(new Skill("Aura of Faith", Properties.Resources.Aura_of_Faith, Skill.Attributes.Protection_Prayers, Skill.Professions.Monk, 3, "5E 1/4C 8R - Elite Enchantment Spell. (3 seconds.) Target ally gains 50...90...100% more Health when healed and takes 5...41...50% less damage.", true));
            Data.Add(new Skill("Shield of Regeneration", Properties.Resources.Shield_of_Regeneration, Skill.Attributes.Protection_Prayers, Skill.Professions.Monk, 3, "15E 1/4C 8R - Elite Enchantment Spell. (5...11...13 seconds.) +3...9...10 Health regeneration and +40 armor.", true));
            Data.Add(new Skill("Shield of Judgment", Properties.Resources.Shield_of_Judgment, Skill.Attributes.Smiting_Prayers, Skill.Professions.Monk, 3, "15E 1C 45R - Elite Enchantment Spell. (8...18...20 seconds.) Deals 5...41...50 holy damage to foes attacking target ally. Causes knock-down.", true));
            Data.Add(new Skill("Protective Bond", Properties.Resources.Protective_Bond, Skill.Attributes.Protection_Prayers, Skill.Professions.Monk, 1, "-1ER 10E 2C - Enchantment Spell. Target ally cannot lose more than 5% max Health from a single attack or spell. Each time damage is reduced you lose 6...4...3 Energy or this spell ends."));
            Data.Add(new Skill("Pacifism", Properties.Resources.Pacifism, Skill.Attributes.Protection_Prayers, Skill.Professions.Monk, 1, "10E 2C 30R - Hex Spell. (8...18...20 seconds.) Target foe cannot attack. Ends if this foe takes damage."));
            Data.Add(new Skill("Amity", Properties.Resources.Amity, Skill.Attributes.Protection_Prayers, Skill.Professions.Monk, 1, "5E 1/4C 45R - Elite Hex Spell. (8...18...20 seconds.) Foes adjacent to you cannot attack. Ends on any foes that take damage.", true));
            Data.Add(new Skill("Peace and Harmony", Properties.Resources.Peace_and_Harmony, Skill.Attributes.Divine_Favor, Skill.Professions.Monk, 2, "5E 1/4C 10R - Elite Enchantment Spell. Target ally loses 0...7...9 condition[s] and hex[es]. Conditions and hexes expire 90% faster on that ally (1...3...3 second[s]). Disables your Smiting Prayers (20 seconds).", true));
            Data.Add(new Skill("Judge's Insight", Properties.Resources.Judge_s_Insight, Skill.Attributes.Smiting_Prayers, Skill.Professions.Monk, 2, "10E 2C 10R - Enchantment Spell. (8...18...20 seconds.) Converts target ally's attacks to holy damage and adds +20% armor penetration."));
            Data.Add(new Skill("Unyielding Aura", Properties.Resources.Unyielding_Aura, Skill.Attributes.Divine_Favor, Skill.Professions.Monk, 4, "-1ER 5E 1/4C 10R - Elite Enchantment Spell. Your Monk spells heal for +15...51...60%. End effect: a random other party member is resurrected with full Health and Energy and teleported to your location.", true));
            Data.Add(new Skill("Mark of Protection", Properties.Resources.Mark_of_Protection, Skill.Attributes.Protection_Prayers, Skill.Professions.Monk, 2, "10E 1C 45R - Elite Enchantment Spell. (10 seconds.) Converts incoming damage to healing (maximum 6...49...60). All your Protection Prayers are disabled (5 seconds).", true));
            Data.Add(new Skill("Life Barrier", Properties.Resources.Life_Barrier, Skill.Attributes.Protection_Prayers, Skill.Professions.Monk, 2, "-1ER 15E 2C 5R - Elite Enchantment Spell. Reduces damage by 20...44...50%. Cannot self-target. If your Health is below 50% when target takes damage, Life Barrier ends.", true));
            Data.Add(new Skill("Zealot's Fire", Properties.Resources.Zealot_s_Fire, Skill.Attributes.Smiting_Prayers, Skill.Professions.Monk, 4, "10E 1/4C 30R - Enchantment Spell. (60 seconds.) Whenever you use a skill on an ally, all foes adjacent to that ally are hit for 5...29...35 fire damage. Damage cost: you lose 1 Energy."));
            Data.Add(new Skill("Balthazar's Aura", Properties.Resources.Balthazar_s_Aura, Skill.Attributes.Smiting_Prayers, Skill.Professions.Monk, 2, "25E 2C 25R - Enchantment Spell. (8 seconds.) Deals 10...22...25 holy damage each second to foes adjacent to target ally."));
            Data.Add(new Skill("Spell Breaker", Properties.Resources.Spell_Breaker, Skill.Attributes.Divine_Favor, Skill.Professions.Monk, 1, "15E 1C 45R - Elite Enchantment Spell. (5...15...17 seconds.) Target ally cannot be the target of enemy spells.", true));
            Data.Add(new Skill("Healing Seed", Properties.Resources.Healing_Seed, Skill.Attributes.Healing_Prayers, Skill.Professions.Monk, 4, "10E 2C 25R - Enchantment Spell. (10 seconds.) Target and adjacent allies gain 3...25...30 Health whenever this ally takes damage. Cannot self-target."));
            Data.Add(new Skill("Mend Condition", Properties.Resources.Mend_Condition, Skill.Attributes.Protection_Prayers, Skill.Professions.Monk, 3, "5E 3/4C 2R - Spell. Removes one condition. Removal effect: heals for 5...57...70. Cannot self-target."));
            Data.Add(new Skill("Restore Condition", Properties.Resources.Restore_Condition, Skill.Attributes.Protection_Prayers, Skill.Professions.Monk, 2, "5E 3/4C 2R - Elite Spell. Removes all conditions. Removal effect: heals for 10...58...70 for each condition removed. Cannot self-target.", true));
            Data.Add(new Skill("Mend Ailment", Properties.Resources.Mend_Ailment, Skill.Attributes.Protection_Prayers, Skill.Professions.Monk, 2, "5E 3/4C 5R - Spell. Removes a condition. Removal effect: heals for 5...57...70 for each remaining condition."));
            Data.Add(new Skill("Purge Conditions", Properties.Resources.Purge_Conditions, Skill.Attributes.None, Skill.Professions.Monk, 2, "5E 1/4C 20R - Spell. Removes all conditions."));
            Data.Add(new Skill("Divine Healing", Properties.Resources.Divine_Healing, Skill.Attributes.Divine_Favor, Skill.Professions.Monk, 4, "5E 1C 12R - Spell. Heals you and party members within earshot for 15...51...60."));
            Data.Add(new Skill("Heal Area", Properties.Resources.Heal_Area, Skill.Attributes.Healing_Prayers, Skill.Professions.Monk, 1, "10E 1C 5R - Spell. Heals you and adjacent allies and foes for 30...150...180."));
            Data.Add(new Skill("Orison of Healing", Properties.Resources.Orison_of_Healing, Skill.Attributes.Healing_Prayers, Skill.Professions.Monk, 2, "5E 1C 2R - Spell. Heals for 20...60...70."));
            Data.Add(new Skill("Word of Healing", Properties.Resources.Word_of_Healing, Skill.Attributes.Healing_Prayers, Skill.Professions.Monk, 3, "5E 3/4C 3R - Elite Spell. Heals for 5...81...100. Heals for 30...98...115 more if target ally is below 50% Health.", true));
            Data.Add(new Skill("Dwayna's Kiss", Properties.Resources.Dwayna_s_Kiss, Skill.Attributes.Healing_Prayers, Skill.Professions.Monk, 5, "5E 1C 3R - Spell. Heals for 15...51...60. Heals for 10...30...35 more for each enchantment and hex on target ally. Cannot self-target."));
            Data.Add(new Skill("Divine Boon", Properties.Resources.Divine_Boon, Skill.Attributes.Divine_Favor, Skill.Professions.Monk, 2, "-1ER 5E 1/4C 10R - Enchantment Spell. Whenever you cast a Protection Prayers or Divine Favor spell on an ally, that ally is healed for 15...51...60. Heal cost: you lose 1 Energy."));
            Data.Add(new Skill("Healing Hands", Properties.Resources.Healing_Hands, Skill.Attributes.Healing_Prayers, Skill.Professions.Monk, 3, "5E 1/4C 25R - Elite Enchantment Spell. (10 seconds.) Heals for 5...29...35 whenever target takes damage.", true));
            Data.Add(new Skill("Heal Other", Properties.Resources.Heal_Other, Skill.Attributes.Healing_Prayers, Skill.Professions.Monk, 1, "10E 3/4C 3R - Spell. Heals for 35...151...180. Cannot self-target."));
            Data.Add(new Skill("Heal Party", Properties.Resources.Heal_Party, Skill.Attributes.Healing_Prayers, Skill.Professions.Monk, 2, "15E 2C 2R - Spell. Heals entire party for 30...66...75."));
            Data.Add(new Skill("Healing Breeze", Properties.Resources.Healing_Breeze, Skill.Attributes.Healing_Prayers, Skill.Professions.Monk, 2, "10E 1C 5R - Enchantment Spell. (15 seconds.) +4...8...9 Health regeneration."));
            Data.Add(new Skill("Vital Blessing", Properties.Resources.Vital_Blessing, Skill.Attributes.Protection_Prayers, Skill.Professions.Monk, 1, "-1ER 10E 3/4C 2R - Enchantment Spell. +40...168...200 maximum Health."));
            Data.Add(new Skill("Mending", Properties.Resources.Mending, Skill.Attributes.Healing_Prayers, Skill.Professions.Monk, 2, "-1ER 10E 2C - Enchantment Spell. +1...3...4 Health regeneration."));
            Data.Add(new Skill("Live Vicariously", Properties.Resources.Live_Vicariously, Skill.Attributes.Healing_Prayers, Skill.Professions.Monk, 2, "-1ER 5E 2C - Enchantment Spell. You gain 2...14...17 Health whenever target ally hits a foe."));
            Data.Add(new Skill("Infuse Health", Properties.Resources.Infuse_Health, Skill.Attributes.Healing_Prayers, Skill.Professions.Monk, 2, "10E 1/4C - Spell. Heals for 100...129...136% of half your current Health. Lose half your current Health. Cannot self-target."));
            Data.Add(new Skill("Signet of Devotion", Properties.Resources.Signet_of_Devotion, Skill.Attributes.Divine_Favor, Skill.Professions.Monk, 4, "2C 5R - Signet. Heals for 14...83...100."));
            Data.Add(new Skill("Signet of Judgment", Properties.Resources.Signet_of_Judgment, Skill.Attributes.Smiting_Prayers, Skill.Professions.Monk, 3, "1C 20R - Elite Signet. Knocks down target. Deals 15...63...75 holy damage to target and adjacent foes.", true));
            Data.Add(new Skill("Purge Signet", Properties.Resources.Purge_Signet, Skill.Attributes.None, Skill.Professions.Monk, 2, "2C 20R - Signet. Removes all hexes and conditions. Removal cost: 10 Energy each."));
            Data.Add(new Skill("Bane Signet", Properties.Resources.Bane_Signet, Skill.Attributes.Smiting_Prayers, Skill.Professions.Monk, 2, "1C 20R - Signet. Deals 26...50...56 holy damage. Causes knock-down if target foe is attacking."));
            Data.Add(new Skill("Blessed Signet", Properties.Resources.Blessed_Signet, Skill.Attributes.Divine_Favor, Skill.Professions.Monk, 2, "2C 10R - Signet. You gain 3 Energy for each enchantment you are maintaining. You cannot gain more than 3...20...24 Energy in this way."));
            Data.Add(new Skill("Martyr", Properties.Resources.Martyr, Skill.Attributes.None, Skill.Professions.Monk, 2, "5E 1C 10R - Elite Spell. Transfer all conditions from all allies to you.", true));
            Data.Add(new Skill("Shielding Hands", Properties.Resources.Shielding_Hands, Skill.Attributes.Protection_Prayers, Skill.Professions.Monk, 5, "5E 1/4C 15R - Enchantment Spell. (8 seconds.) Reduces incoming damage and life steal by 3...15...18. End effect: heals for 5...41...50"));
            Data.Add(new Skill("Contemplation of Purity", Properties.Resources.Contemplation_of_Purity, Skill.Attributes.Divine_Favor, Skill.Professions.Monk, 2, "5E 1/4C 10R - Skill. You gain 0...64...80 Health and lose one hex and one condition for each enchantment on you (maximum 1...7...8 hexes and conditions). You lose all enchantments."));
            Data.Add(new Skill("Remove Hex", Properties.Resources.Remove_Hex, Skill.Attributes.None, Skill.Professions.Monk, 4, "5E 1C 8R - Spell. Removes a hex from target ally."));
            Data.Add(new Skill("Smite Hex", Properties.Resources.Smite_Hex, Skill.Attributes.Smiting_Prayers, Skill.Professions.Monk, 4, "5E 1C 12R - Spell. Removes a hex from target ally. Removal effect: deals 10...70...85 holy damage to foes in the area of target ally."));
            Data.Add(new Skill("Convert Hexes", Properties.Resources.Convert_Hexes, Skill.Attributes.Protection_Prayers, Skill.Professions.Monk, 2, "15E 1C 12R - Spell. Removes all hexes; +10 armor for each Necromancer hex removed (8...18...20 seconds). Cannot self-target."));
            Data.Add(new Skill("Light of Dwayna", Properties.Resources.Light_of_Dwayna, Skill.Attributes.None, Skill.Professions.Monk, 1, "25E 4C 20R - Spell. Resurrects all dead party members in the area. (25% Health, zero Energy)."));
            Data.Add(new Skill("Resurrect", Properties.Resources.Resurrect, Skill.Attributes.None, Skill.Professions.Monk, 1, "10E 5C 8R - Spell. Resurrects target party member (25% Health, 0 Energy)."));
            Data.Add(new Skill("Rebirth", Properties.Resources.Rebirth, Skill.Attributes.Protection_Prayers, Skill.Professions.Monk, 2, "10E 5C - Spell. Resurrects target party member (25% Health, 0 Energy). Teleports target to you. Disables target's skills (10...4...3 seconds). You lose all Energy."));
            Data.Add(new Skill("Reversal of Fortune", Properties.Resources.Reversal_of_Fortune, Skill.Attributes.Protection_Prayers, Skill.Professions.Monk, 2, "5E 1/4C 2R - Enchantment Spell. (8 seconds.) Converts the next incoming damage or life steal (maximum 15...67...80) to healing."));
            Data.Add(new Skill("Succor", Properties.Resources.Succor, Skill.Attributes.None, Skill.Professions.Monk, 2, "-1ER 5E 1C 10R - Enchantment Spell. +1 Health regeneration and +1 Energy regeneration. Cannot self-target. You lose 1 Energy each time target ally casts a spell."));
            Data.Add(new Skill("Holy Veil", Properties.Resources.Holy_Veil, Skill.Attributes.None, Skill.Professions.Monk, 2, "-1ER 5E 1C 12R - Enchantment Spell. Doubles casting time of hexes cast on target ally. End effect: removes a hex."));
            Data.Add(new Skill("Divine Spirit", Properties.Resources.Divine_Spirit, Skill.Attributes.Divine_Favor, Skill.Professions.Monk, 2, "10E 1/4C 60R - Enchantment Spell. (1...11...14 second[s].) Monk spells cost you 5 less Energy. Minimum cost: 1 Energy."));
            Data.Add(new Skill("Draw Conditions", Properties.Resources.Draw_Conditions, Skill.Attributes.Protection_Prayers, Skill.Professions.Monk, 2, "5E 1/4C 4R - Spell. Transfers all conditions from target ally to yourself. Transfer effect: you gain 6...22...26 Health for each condition. Cannot self-target."));
            Data.Add(new Skill("Holy Strike", Properties.Resources.Holy_Strike, Skill.Attributes.Smiting_Prayers, Skill.Professions.Monk, 2, "5E 3/4C 8R - Touch Skill. Deals 10...46...55 holy damage. Deals 10...46...55 more holy damage if target is knocked down."));
            Data.Add(new Skill("Healing Touch", Properties.Resources.Healing_Touch, Skill.Attributes.Healing_Prayers, Skill.Professions.Monk, 2, "5E 3/4C 5R - Touch spell. Heals for 16...51...60. Double Health gain from Divine Favor for this spell."));
            Data.Add(new Skill("Restore Life", Properties.Resources.Restore_Life, Skill.Attributes.Healing_Prayers, Skill.Professions.Monk, 2, "10E 4C 8R - Touch Spell. Resurrects target party member (20...56...65% Health, 42...80...90% Energy)."));
            Data.Add(new Skill("Vengeance", Properties.Resources.Vengeance, Skill.Attributes.None, Skill.Professions.Monk, 3, "10E 4C 30R - Enchantment Spell. (30 seconds.) Resurrects target party member (full Health, full Energy). This ally deals 25% more damage and does not incur death penalty. End effect: this ally dies."));

            Data.Add(new Skill("\"To the Limit!\"", Properties.Resources._To_the_Limit__, Skill.Attributes.Tactics, Skill.Professions.Warrior, 5, "5E 15R - Shout. (10...18...20 seconds.) You have +10...50...60 maximum Health. Initial effect: you gain one strike of adrenaline for each foe within earshot (maximum 1...5...6)."));
            Data.Add(new Skill("Battle Rage", Properties.Resources.Battle_Rage, Skill.Attributes.Strength, Skill.Professions.Warrior, 2, "4A - Elite Stance. (5...17...20 seconds.) You move 33% faster and gain double adrenaline from your attacks. Ends if you use any non-adrenal skills.", true));
            Data.Add(new Skill("Defy Pain", Properties.Resources.Defy_Pain, Skill.Attributes.Strength, Skill.Professions.Warrior, 2, "5A - Elite Skill. (20 seconds.) You have +90...258...300 maximum Health, +20 armor, and take 1...8...10 less damage.", true));
            Data.Add(new Skill("Rush", Properties.Resources.Rush, Skill.Attributes.Strength, Skill.Professions.Warrior, 4, "4A - Stance. (8...18...20 seconds.) You move 25% faster."));
            Data.Add(new Skill("Hamstring", Properties.Resources.Hamstring, Skill.Attributes.Swordsmanship, Skill.Professions.Warrior, 1, "10E 15R - Sword Attack. Inflicts Crippled condition (3...13...15 seconds)."));
            Data.Add(new Skill("Wild Blow", Properties.Resources.Wild_Blow, Skill.Attributes.None, Skill.Professions.Warrior, 4, "5E 8R - Melee Attack. Always a critical hit. Removes a stance. Unblockable. Lose all adrenaline."));
            Data.Add(new Skill("Power Attack", Properties.Resources.Power_Attack, Skill.Attributes.Strength, Skill.Professions.Warrior, 3, "5E 3R - Melee Attack. Deals +10...34...40 damage."));
            Data.Add(new Skill("Desperation Blow", Properties.Resources.Desperation_Blow, Skill.Attributes.Tactics, Skill.Professions.Warrior, 2, "5E 7R - Melee Attack. Deals +10...34...40 damage. Inflicts one of the following random conditions: Deep Wound (20 seconds), Weakness (20 seconds), Bleeding (25 seconds), or Crippled (15 seconds). You are knocked-down."));
            Data.Add(new Skill("Thrill of Victory", Properties.Resources.Thrill_of_Victory, Skill.Attributes.Tactics, Skill.Professions.Warrior, 5, "5E 8R - Melee Attack. Deals +20...36...40 damage. If you have more Health than your target, you gain 1...2...2 adrenaline."));
            Data.Add(new Skill("Distracting Blow", Properties.Resources.Distracting_Blow, Skill.Attributes.None, Skill.Professions.Warrior, 4, "5E 1/2C 10R - Melee Attack. Also attacks foes adjacent to your target. Interrupts an action. Hits for no damage."));
            Data.Add(new Skill("Protector's Strike", Properties.Resources.Protector_s_Strike, Skill.Attributes.Strength, Skill.Professions.Warrior, 3, "5E 1/2C 3R - Melee Attack. Deals +10...34...40 damage if target is moving."));
            Data.Add(new Skill("Griffon's Sweep", Properties.Resources.Griffon_s_Sweep, Skill.Attributes.Strength, Skill.Professions.Warrior, 3, "5E 8R - Melee Attack. Deals +5...17...20 damage. Causes knock-down and deals 10...29...34 damage if blocked."));
            Data.Add(new Skill("Pure Strike", Properties.Resources.Pure_Strike, Skill.Attributes.Swordsmanship, Skill.Professions.Warrior, 3, "5E 8R - Sword Attack. Deals +1...24...30 damage. Unblockable unless you are in a stance."));
            Data.Add(new Skill("Skull Crack", Properties.Resources.Skull_Crack, Skill.Attributes.None, Skill.Professions.Warrior, 3, "9A 1/2C - Elite Melee Attack. Interrupts an action. Inflicts Dazed condition (10 seconds) if target is casting a spell.", true));
            Data.Add(new Skill("Cyclone Axe", Properties.Resources.Cyclone_Axe, Skill.Attributes.Axe_Mastery, Skill.Professions.Warrior, 5, "5E 4R - Axe Attack. Deals +4...10...12 damage to all foes adjacent to you."));
            Data.Add(new Skill("Hammer Bash", Properties.Resources.Hammer_Bash, Skill.Attributes.Hammer_Mastery, Skill.Professions.Warrior, 1, "6A - Hammer Attack. Causes knock-down. Lose all adrenaline."));
            Data.Add(new Skill("Bull's Strike", Properties.Resources.Bull_s_Strike, Skill.Attributes.Strength, Skill.Professions.Warrior, 3, "5E 10R - Melee Attack. Deals +5...25...30 damage and causes knock-down if target foe is moving."));
            Data.Add(new Skill("\"I Will Avenge You!\"", Properties.Resources._I_Will_Avenge_You__, Skill.Attributes.Strength, Skill.Professions.Warrior, 2, "5E 45R - Shout. You have +3...6...7 Health regeneration and attack 25% faster (10 seconds for each dead ally)."));
            Data.Add(new Skill("Axe Rake", Properties.Resources.Axe_Rake, Skill.Attributes.Axe_Mastery, Skill.Professions.Warrior, 1, "5A - Axe Attack. Deals +1...8...10 damage and inflicts Crippled condition (15 seconds) if target foe has a Deep Wound."));
            Data.Add(new Skill("Cleave", Properties.Resources.Cleave, Skill.Attributes.Axe_Mastery, Skill.Professions.Warrior, 3, "4A - Elite Axe Attack. Deals +10...26...30 damage.", true));
            Data.Add(new Skill("Executioner's Strike", Properties.Resources.Executioner_s_Strike, Skill.Attributes.Axe_Mastery, Skill.Professions.Warrior, 3, "8A - Axe Attack. Deals +10...34...40 damage."));
            Data.Add(new Skill("Dismember", Properties.Resources.Dismember, Skill.Attributes.Axe_Mastery, Skill.Professions.Warrior, 3, "5A - Axe Attack. Inflicts Deep Wound condition (5...17...20 seconds)."));
            Data.Add(new Skill("Eviscerate", Properties.Resources.Eviscerate, Skill.Attributes.Axe_Mastery, Skill.Professions.Warrior, 4, "8A - Elite Axe Attack. Deals +1...25...31 damage. Inflicts Deep Wound condition (5...17...20 seconds).", true));
            Data.Add(new Skill("Penetrating Blow", Properties.Resources.Penetrating_Blow, Skill.Attributes.Axe_Mastery, Skill.Professions.Warrior, 4, "5A - Axe Attack. Deals +5...17...20 damage. 20% armor penetration."));
            Data.Add(new Skill("Disrupting Chop", Properties.Resources.Disrupting_Chop, Skill.Attributes.Axe_Mastery, Skill.Professions.Warrior, 3, "6A - Axe Attack. Interrupts an action. Interruption effect: interrupted skill is disabled for +20 seconds."));
            Data.Add(new Skill("Swift Chop", Properties.Resources.Swift_Chop, Skill.Attributes.Axe_Mastery, Skill.Professions.Warrior, 3, "5E 4R - Axe Attack. Deals +1...16...20 damage. Deals 1...16...20 damage and inflicts Deep Wound condition (20 seconds) if blocked."));
            Data.Add(new Skill("Axe Twist", Properties.Resources.Axe_Twist, Skill.Attributes.Axe_Mastery, Skill.Professions.Warrior, 1, "5A - Axe Attack. Deals +1...16...20 damage and inflicts Weakness condition (20 seconds) if target foe has a Deep Wound."));
            Data.Add(new Skill("\"For Great Justice!\"", Properties.Resources._For_Great_Justice__, Skill.Attributes.None, Skill.Professions.Warrior, 4, "5E 45R - Shout. (20 seconds.) You gain 100% more adrenaline."));
            Data.Add(new Skill("Flurry", Properties.Resources.Flurry, Skill.Attributes.None, Skill.Professions.Warrior, 5, "5E 5R - Stance. (5 seconds). You attack 33% faster. You do 25% less damage."));
            Data.Add(new Skill("Defensive Stance", Properties.Resources.Defensive_Stance, Skill.Attributes.Tactics, Skill.Professions.Warrior, 4, "5A 8R - Stance. (1...4...5 second[s].) You have 75% chance to block. End effect: gain one adrenaline for each melee attack skill you have (maximum 0...3...4)."));
            Data.Add(new Skill("Frenzy", Properties.Resources.Frenzy, Skill.Attributes.None, Skill.Professions.Warrior, 3, "5E 4R - Stance. (8 seconds.) You attack 33% faster. You take double damage."));
            Data.Add(new Skill("Endure Pain", Properties.Resources.Endure_Pain, Skill.Attributes.Strength, Skill.Professions.Warrior, 2, "5E 30R - Skill. (7...16...18 seconds.) You have +90...258...300 maximum Health."));
            Data.Add(new Skill("\"Watch Yourself!\"", Properties.Resources._Watch_Yourself__, Skill.Attributes.Tactics, Skill.Professions.Warrior, 5, "4A 4R - Shout. (10 seconds.) Party members in earshot have +5...21...25 armor. Ends after 10 incoming attacks."));
            Data.Add(new Skill("Sprint", Properties.Resources.Sprint, Skill.Attributes.Strength, Skill.Professions.Warrior, 4, "5E 15R - Stance. (8...13...14 seconds.) You move 25% faster."));
            Data.Add(new Skill("Belly Smash", Properties.Resources.Belly_Smash, Skill.Attributes.Hammer_Mastery, Skill.Professions.Warrior, 2, "5E 1C 10R - Hammer Attack. Inflicts Blindness condition to adjacent foes (3...6...7 seconds) if target foe is knocked down."));
            Data.Add(new Skill("Mighty Blow", Properties.Resources.Mighty_Blow, Skill.Attributes.Hammer_Mastery, Skill.Professions.Warrior, 3, "7A - Hammer Attack. Deals +10...34...40 damage."));
            Data.Add(new Skill("Crushing Blow", Properties.Resources.Crushing_Blow, Skill.Attributes.Hammer_Mastery, Skill.Professions.Warrior, 4, "5E 10R - Hammer Attack. Deals +1...16...20 damage. Inflicts Deep Wound condition if target foe is knocked-down (5...17...20 seconds)."));
            Data.Add(new Skill("Crude Swing", Properties.Resources.Crude_Swing, Skill.Attributes.Hammer_Mastery, Skill.Professions.Warrior, 5, "5E 5R - Hammer Attack. Attack all adjacent foes for +1...16...20 damage."));
            Data.Add(new Skill("Earth Shaker", Properties.Resources.Earth_Shaker, Skill.Attributes.Hammer_Mastery, Skill.Professions.Warrior, 4, "8A - Elite Hammer Attack. Knocks down target and adjacent foes. 50% failure chance unless Hammer Mastery is 5 or more.", true));
            Data.Add(new Skill("Devastating Hammer", Properties.Resources.Devastating_Hammer, Skill.Attributes.Hammer_Mastery, Skill.Professions.Warrior, 4, "7A - Elite Hammer Attack. Causes knock-down. Inflicts Weakness condition (5...17...20 seconds).", true));
            Data.Add(new Skill("Irresistible Blow", Properties.Resources.Irresistible_Blow, Skill.Attributes.Hammer_Mastery, Skill.Professions.Warrior, 3, "5E 6R - Hammer Attack. Deals +5...17...20 damage. Deals 5...17...20 damage and causes knock-down if blocked."));
            Data.Add(new Skill("Counter Blow", Properties.Resources.Counter_Blow, Skill.Attributes.Hammer_Mastery, Skill.Professions.Warrior, 4, "4A - Hammer Attack. Causes knock-down if target foe is attacking."));
            Data.Add(new Skill("Backbreaker", Properties.Resources.Backbreaker, Skill.Attributes.Hammer_Mastery, Skill.Professions.Warrior, 3, "9A - Elite Hammer Attack. Deals +1...16...20 damage. Causes knockdown. Knockdown lasts 4 seconds with Strength 8 or higher.", true));
            Data.Add(new Skill("Heavy Blow", Properties.Resources.Heavy_Blow, Skill.Attributes.Hammer_Mastery, Skill.Professions.Warrior, 2, "5A - Hammer Attack. Deals +1...24...30 damage and causes knock-down if target foe is Weakened. Lose all adrenaline."));
            Data.Add(new Skill("Staggering Blow", Properties.Resources.Staggering_Blow, Skill.Attributes.Hammer_Mastery, Skill.Professions.Warrior, 3, "4A 1C - Hammer Attack. Inflicts Weakness condition (5...17...20 seconds)."));
            Data.Add(new Skill("Dolyak Signet", Properties.Resources.Dolyak_Signet, Skill.Attributes.Strength, Skill.Professions.Warrior, 2, "20R - Signet. (8...18...20 seconds.) You have +10...34...40 armor and cannot be knocked-down. You move 75% slower."));
            Data.Add(new Skill("Warrior's Cunning", Properties.Resources.Warrior_s_Cunning, Skill.Attributes.Strength, Skill.Professions.Warrior, 2, "10E 60R - Skill. (5...10...11 seconds.) Your melee attacks are unblockable."));
            Data.Add(new Skill("Shield Bash", Properties.Resources.Shield_Bash, Skill.Attributes.Strength, Skill.Professions.Warrior, 2, "5E 20R - Skill. (5...10...11 seconds.) You block the next attack skill. Causes knock-down and +15 second recharge if it was a melee skill. No effect unless you are wielding a shield."));
            Data.Add(new Skill("\"Charge!\"", Properties.Resources._Charge__, Skill.Attributes.Tactics, Skill.Professions.Warrior, 4, "5E 20R - Elite Shout. Allies in earshot lose the Crippled condition. For 5...11...13 seconds, these allies move 33% faster.", true));
            Data.Add(new Skill("\"Victory Is Mine!\"", Properties.Resources._Victory_Is_Mine__, Skill.Attributes.Tactics, Skill.Professions.Warrior, 2, "5E 15R - Elite Shout. You gain 10...56...68 Health and 3...6...7 Energy for each condition on target foe.", true));
            Data.Add(new Skill("\"Fear Me!\"", Properties.Resources._Fear_Me__, Skill.Attributes.Tactics, Skill.Professions.Warrior, 4, "4A 6R - Shout. All nearby foes lose 1...3...4 Energy. For 1...12...15 second[s], your melee attacks gain +5...25...30% chance of a critical hit against stationary foes."));
            Data.Add(new Skill("\"Shields Up!\"", Properties.Resources._Shields_Up__, Skill.Attributes.Tactics, Skill.Professions.Warrior, 3, "10E 30R - Shout. (5...10...11 seconds.) Party members in earshot have +60 armor against projectile attacks."));
            Data.Add(new Skill("\"I Will Survive!\"", Properties.Resources._I_Will_Survive__, Skill.Attributes.Strength, Skill.Professions.Warrior, 1, "5E 30R - Shout. (5...10...11 seconds.) You have +3 Health regeneration for each condition on you."));
            Data.Add(new Skill("Berserker Stance", Properties.Resources.Berserker_Stance, Skill.Attributes.Strength, Skill.Professions.Warrior, 1, "5E 20R - Stance. (5...10...11 seconds.) You attack 33% faster and gain 50% more adrenaline. Ends if you use a skill."));
            Data.Add(new Skill("Balanced Stance", Properties.Resources.Balanced_Stance, Skill.Attributes.Tactics, Skill.Professions.Warrior, 2, "5E 30R - Stance. (8...18...20 seconds.) You cannot be knocked-down and do not take extra damage from critical hits."));
            Data.Add(new Skill("Gladiator's Defense", Properties.Resources.Gladiator_s_Defense, Skill.Attributes.Tactics, Skill.Professions.Warrior, 3, "5E 30R - Elite Stance. (5...10...11 seconds.) You have 75% chance to block. Your attacker takes 5...29...35 damage whenever you block a melee attack this way.", true));
            Data.Add(new Skill("Deflect Arrows", Properties.Resources.Deflect_Arrows, Skill.Attributes.Tactics, Skill.Professions.Warrior, 4, "5A 12R - Stance. (1...5...6 second[s].) You have 75% chance to block attacks. Adjacent foes suffer Bleeding (5...13...15 seconds) when you block a projectile attack."));
            Data.Add(new Skill("Warrior's Endurance", Properties.Resources.Warrior_s_Endurance, Skill.Attributes.Strength, Skill.Professions.Warrior, 2, "5E 30R - Elite Skill. (5...29...35 seconds.) You gain 3 Energy each time you hit with a melee attack. No Energy gain if you have more than 10...22...25 Energy.", true));
            Data.Add(new Skill("Dwarven Battle Stance", Properties.Resources.Dwarven_Battle_Stance, Skill.Attributes.Strength, Skill.Professions.Warrior, 2, "5E 20R - Elite Stance. (5...10...11 seconds.) You attack 33% faster, you gain +40 armor, and your attack skills interrupt actions. No effect unless you have a hammer equipped.", true));
            Data.Add(new Skill("Disciplined Stance", Properties.Resources.Disciplined_Stance, Skill.Attributes.Tactics, Skill.Professions.Warrior, 3, "5E 15R - Stance. (1...3...4 second[s].) You have 75% chance to block and +10 armor. Ends if you use an adrenal skill."));
            Data.Add(new Skill("Wary Stance", Properties.Resources.Wary_Stance, Skill.Attributes.Tactics, Skill.Professions.Warrior, 1, "10E 10R - Stance. (1...5...6 second[s]). You block attack skills. Gain adrenaline and 5 Energy for each block. Ends if you use a skill."));
            Data.Add(new Skill("Shield Stance", Properties.Resources.Shield_Stance, Skill.Attributes.Tactics, Skill.Professions.Warrior, 3, "5E 15R - Stance. (1...5...6 second[s].) You have 75% chance to block. Damage is reduced by 2 for each rank of Strength (maximum 15 damage reduction). No effect unless you have a shield equipped."));
            Data.Add(new Skill("Bull's Charge", Properties.Resources.Bull_s_Charge, Skill.Attributes.Strength, Skill.Professions.Warrior, 2, "5E 20R - Elite Stance. (5...10...11 seconds.) You move 33% faster. Causes knock-down if you hit a moving foe in melee. Ends if you use a skill.", true));
            Data.Add(new Skill("Bonetti's Defense", Properties.Resources.Bonetti_s_Defense, Skill.Attributes.Tactics, Skill.Professions.Warrior, 3, "8A - Stance. (5...10...11 seconds.) You have 75% chance to block. Gain 5 Energy for each melee attack blocked. Ends if you use a skill."));
            Data.Add(new Skill("Hundred Blades", Properties.Resources.Hundred_Blades, Skill.Attributes.Swordsmanship, Skill.Professions.Warrior, 4, "5E 25R - Elite Skill. (15 seconds.) Deals 10...22...25 slashing damage to all adjacent foes whenever you attack with a sword.", true));
            Data.Add(new Skill("Sever Artery", Properties.Resources.Sever_Artery, Skill.Attributes.Swordsmanship, Skill.Professions.Warrior, 4, "4A - Sword Attack. Inflicts Bleeding condition (5...21...25 seconds)."));
            Data.Add(new Skill("Galrath Slash", Properties.Resources.Galrath_Slash, Skill.Attributes.Swordsmanship, Skill.Professions.Warrior, 3, "8A - Sword Attack. Deals +1...32...40 damage."));
            Data.Add(new Skill("Gash", Properties.Resources.Gash, Skill.Attributes.Swordsmanship, Skill.Professions.Warrior, 4, "6A - Sword Attack. Deals +5...17...20 damage and inflicts Deep Wound condition (5...17...20 seconds) if your target is Bleeding."));
            Data.Add(new Skill("Final Thrust", Properties.Resources.Final_Thrust, Skill.Attributes.Swordsmanship, Skill.Professions.Warrior, 3, "10A - Sword Attack. Deals +1...32...40 damage. Deals +1...32...40 more damage if target foe is below 50% Health. Lose all adrenaline."));
            Data.Add(new Skill("Seeking Blade", Properties.Resources.Seeking_Blade, Skill.Attributes.Swordsmanship, Skill.Professions.Warrior, 3, "5E 4R - Sword Attack. Deals +1...16...20 damage. Deals 1...16...20 damage and inflicts Bleeding condition (25 seconds) if blocked."));
            Data.Add(new Skill("Riposte", Properties.Resources.Riposte, Skill.Attributes.Tactics, Skill.Professions.Warrior, 4, "4A - Skill. (8 seconds). You block the next melee attack and your attacker takes 20...68...80 damage. No effect unless you have a sword equipped."));
            Data.Add(new Skill("Deadly Riposte", Properties.Resources.Deadly_Riposte, Skill.Attributes.Tactics, Skill.Professions.Warrior, 3, "5E 10R - Skill. (8 seconds). You block the next melee attack and your attacker takes 15...75...90 damage. Inflicts Bleeding condition. (3...21...25 seconds). No effect unless you have a sword equipped."));
            Data.Add(new Skill("Flourish", Properties.Resources.Flourish, Skill.Attributes.Strength, Skill.Professions.Warrior, 2, "5E 1C 8R - Elite Skill. Recharges your attack skills. You gain 2...6...7 Energy for each skill recharged.", true));
            Data.Add(new Skill("Savage Slash", Properties.Resources.Savage_Slash, Skill.Attributes.Swordsmanship, Skill.Professions.Warrior, 3, "5E 1/2C 15R - Sword Attack. Interrupts an action. Interruption effect: deals +1...32...40 damage if action was a spell."));

            Data.Add(new Skill("Hunter's Shot", Properties.Resources.Hunter_s_Shot, Skill.Attributes.Marksmanship, Skill.Professions.Ranger, 2, "5E 1C 10R - Bow Attack. Inflicts Bleeding condition (3...21...25 seconds) if your target is hit."));
            Data.Add(new Skill("Pin Down", Properties.Resources.Pin_Down, Skill.Attributes.Marksmanship, Skill.Professions.Ranger, 3, "15E 8R - Bow Attack. Inflicts Crippled condition (3...13...15 seconds)."));
            Data.Add(new Skill("Crippling Shot", Properties.Resources.Crippling_Shot, Skill.Attributes.Marksmanship, Skill.Professions.Ranger, 2, "10E 4R - Elite Bow Attack. Unblockable. Inflicts Crippled condition (1...9...11 second[s]).", true));
            Data.Add(new Skill("Power Shot", Properties.Resources.Power_Shot, Skill.Attributes.Marksmanship, Skill.Professions.Ranger, 3, "10E 3R - Bow Attack. Target foe takes 25...45...50 damage."));
            Data.Add(new Skill("Barrage", Properties.Resources.Barrage, Skill.Attributes.Marksmanship, Skill.Professions.Ranger, 4, "5E 1R - Elite Bow Attack. Deals +5...17...20 damage. Hits 5 foes adjacent to your target. All your preparations are removed.", true));
            Data.Add(new Skill("Dual Shot", Properties.Resources.Dual_Shot, Skill.Attributes.None, Skill.Professions.Ranger, 2, "10E 10R - Bow Attack. You shoot two arrows simultaneously at target foe. These arrows deal 25% less damage"));
            Data.Add(new Skill("Quick Shot", Properties.Resources.Quick_Shot, Skill.Attributes.None, Skill.Professions.Ranger, 1, "5E 1C 1R - Elite Bow Attack. You shoot an arrow that moves twice as fast.", true));
            Data.Add(new Skill("Penetrating Attack", Properties.Resources.Penetrating_Attack, Skill.Attributes.Marksmanship, Skill.Professions.Ranger, 4, "10E 3/4C 4R - Bow Attack. Deals +5...21...25 damage. 10% armor penetration."));
            Data.Add(new Skill("Distracting Shot", Properties.Resources.Distracting_Shot, Skill.Attributes.Expertise, Skill.Professions.Ranger, 5, "5E 1/2C 10R - Bow Attack. Interrupts an action. Interruption effect: interrupted skill is disabled for +20 seconds. Hits for only 1...13...16 damage."));
            Data.Add(new Skill("Precision Shot", Properties.Resources.Precision_Shot, Skill.Attributes.Marksmanship, Skill.Professions.Ranger, 2, "10E 1C 6R - Bow Attack. Deals +3...9...10 damage. Unblockable. Easily Interrupted."));
            Data.Add(new Skill("Determined Shot", Properties.Resources.Determined_Shot, Skill.Attributes.Marksmanship, Skill.Professions.Ranger, 2, "5E 10R - Bow Attack. Deals +5...17...20 damage. Recharges your attack skills if it fails to hit."));
            Data.Add(new Skill("Called Shot", Properties.Resources.Called_Shot, Skill.Attributes.None, Skill.Professions.Ranger, 1, "5E 3R - Bow Attack. You shoot an arrow that moves 3 times faster. Unblockable."));
            Data.Add(new Skill("Poison Arrow", Properties.Resources.Poison_Arrow, Skill.Attributes.Wilderness_Survival, Skill.Professions.Ranger, 2, "5E 1R - Elite Bow Attack. Inflicts Poisoned condition. (5...17...20)", true));
            Data.Add(new Skill("Oath Shot", Properties.Resources.Oath_Shot, Skill.Attributes.Expertise, Skill.Professions.Ranger, 2, "10E 25R - Elite Bow Attack. Recharges all skills except Oath Shot if it hits. Disables all skills if it misses (10...5...4 seconds). 50% miss chance unless Expertise 8 or higher.", true));
            Data.Add(new Skill("Debilitating Shot", Properties.Resources.Debilitating_Shot, Skill.Attributes.Marksmanship, Skill.Professions.Ranger, 2, "10E 10R - Bow Attack. Causes 1...8...10 Energy loss."));
            Data.Add(new Skill("Point Blank Shot", Properties.Resources.Point_Blank_Shot, Skill.Attributes.Expertise, Skill.Professions.Ranger, 3, "5E 3R - Half Range Bow Attack. Deals +10...34...40 damage."));
            Data.Add(new Skill("Concussion Shot", Properties.Resources.Concussion_Shot, Skill.Attributes.Marksmanship, Skill.Professions.Ranger, 2, "25E 1/2C 5R - Bow Attack. Interrupts a spell. Interruption effect: inflicts Dazed condition (5...17...20 seconds). Hits for only 1...13...16 damage."));
            Data.Add(new Skill("Punishing Shot", Properties.Resources.Punishing_Shot, Skill.Attributes.Marksmanship, Skill.Professions.Ranger, 3, "10E 1/2C 5R - Elite Bow Attack. Deals +10...18...20 damage. Interrupts an action.", true));
            Data.Add(new Skill("Charm Animal", Properties.Resources.Charm_Animal, Skill.Attributes.Beast_Mastery, Skill.Professions.Ranger, 2, "10E 10C - Skill. Charm target animal. Once charmed, your animal companion will travel with you whenever you have Charm Animal equipped. You cannot charm an animal that is more than 4 levels above you."));
            Data.Add(new Skill("Call of Protection", Properties.Resources.Call_of_Protection, Skill.Attributes.Beast_Mastery, Skill.Professions.Ranger, 4, "5E 90R - Shout. (120 seconds.) Your pet has 5...17...20 damage reduction."));
            Data.Add(new Skill("Call of Haste", Properties.Resources.Call_of_Haste, Skill.Attributes.Beast_Mastery, Skill.Professions.Ranger, 3, "10E 25R - Shout. (30 seconds.) Your pet moves and attacks 33% faster."));
            Data.Add(new Skill("Revive Animal", Properties.Resources.Revive_Animal, Skill.Attributes.Beast_Mastery, Skill.Professions.Ranger, 1, "5E 6C 20R - Skill. Resurrects all nearby allied pets (10...77...94% Health)."));
            Data.Add(new Skill("Symbiotic Bond", Properties.Resources.Symbiotic_Bond, Skill.Attributes.Beast_Mastery, Skill.Professions.Ranger, 1, "10E 55R - Shout. (120...264...300 seconds.) Your pet has +1...3...3 Health regeneration. Half of all damage dealt to your pet is redirected to you."));
            Data.Add(new Skill("Throw Dirt", Properties.Resources.Throw_Dirt, Skill.Attributes.Expertise, Skill.Professions.Ranger, 3, "5E 1C 30R - Touch Skill. Inflicts Blindness condition (3...13...15 seconds). Also affects foes adjacent to target foe."));
            Data.Add(new Skill("Dodge", Properties.Resources.Dodge, Skill.Attributes.Expertise, Skill.Professions.Ranger, 1, "5E 30R - Stance. (5...10...11 seconds.) You move 33% faster and have 27...65...75% chance to block projectile attacks. Ends if you attack."));
            Data.Add(new Skill("Savage Shot", Properties.Resources.Savage_Shot, Skill.Attributes.Marksmanship, Skill.Professions.Ranger, 5, "10E 1/2C 5R - Bow Attack. Interrupts an action. Interruption effect: deals +13...25...28 damage if action was a spell."));
            Data.Add(new Skill("Antidote Signet", Properties.Resources.Antidote_Signet, Skill.Attributes.None, Skill.Professions.Ranger, 5, "1C 4R - Signet. Remove Poison, Disease, and Blindness from yourself, and one additional condition."));
            Data.Add(new Skill("Incendiary Arrows", Properties.Resources.Incendiary_Arrows, Skill.Attributes.Wilderness_Survival, Skill.Professions.Ranger, 4, "5E 5R - Elite Bow Attack. Hits 2 foes near your target and inflicts burning (1...3...3 second[s]).", true));
            Data.Add(new Skill("Melandru's Arrows", Properties.Resources.Melandru_s_Arrows, Skill.Attributes.Wilderness_Survival, Skill.Professions.Ranger, 2, "5E 2C 12R - Elite Preparation. (18 seconds.) Your arrows inflict Bleeding condition (3...21...25 seconds) and deal +8...24...28 damage to enchanted foes.", true));
            Data.Add(new Skill("Marksman's Wager", Properties.Resources.Marksman_s_Wager, Skill.Attributes.Expertise, Skill.Professions.Ranger, 2, "5E 2C 24R - Elite Preparation. (18 seconds.) Gain 5...9...10 Energy whenever your arrows hit. Lose 10 Energy whenever your arrows fail to hit.", true));
            Data.Add(new Skill("Ignite Arrows", Properties.Resources.Ignite_Arrows, Skill.Attributes.Wilderness_Survival, Skill.Professions.Ranger, 3, "10E 2C 12R - Preparation. (24 seconds.) Your arrows deal 3...15...18 fire damage to target and foes adjacent to target."));
            Data.Add(new Skill("Read the Wind", Properties.Resources.Read_the_Wind, Skill.Attributes.Marksmanship, Skill.Professions.Ranger, 3, "5E 2C 12R - Preparation. (24 seconds). +3...9...10 damage. Your arrows move twice as fast."));
            Data.Add(new Skill("Kindle Arrows", Properties.Resources.Kindle_Arrows, Skill.Attributes.Wilderness_Survival, Skill.Professions.Ranger, 3, "5E 2C 12R - Preparation. (24 seconds.) +3...20...24 fire damage. Your arrows deal fire damage."));
            Data.Add(new Skill("Choking Gas", Properties.Resources.Choking_Gas, Skill.Attributes.Wilderness_Survival, Skill.Professions.Ranger, 2, "15E 2C 24R - Preparation. (1...10...12 seconds.) +1...7...8 damage. Spreads Choking Gas to foes adjacent to target. Choking Gas interrupts spells."));
            Data.Add(new Skill("Apply Poison", Properties.Resources.Apply_Poison, Skill.Attributes.Wilderness_Survival, Skill.Professions.Ranger, 4, "15E 2C 12R - Preparation. (24 seconds.) Your physical attacks inflict Poisoned condition (3...13...15 seconds)."));
            Data.Add(new Skill("Comfort Animal", Properties.Resources.Comfort_Animal, Skill.Attributes.Beast_Mastery, Skill.Professions.Ranger, 5, "5E 1C 1R - Skill. Your pet gains 20...87...104 Health. Resurrects your pet (10...48...58% Health.) If you have Comfort Animal equipped, your animal companion will travel with you."));
            Data.Add(new Skill("Bestial Pounce", Properties.Resources.Bestial_Pounce, Skill.Attributes.Beast_Mastery, Skill.Professions.Ranger, 2, "5E 10R - Pet Attack. Deals +5...17...20 damage. Causes knock-down if target foe is casting a spell."));
            Data.Add(new Skill("Maiming Strike", Properties.Resources.Maiming_Strike, Skill.Attributes.Beast_Mastery, Skill.Professions.Ranger, 1, "10E 5R - Pet Attack. Deals +5...17...20 damage. Inflicts Crippled condition (3...13...15 seconds) if target foe is moving."));
            Data.Add(new Skill("Feral Lunge", Properties.Resources.Feral_Lunge, Skill.Attributes.Beast_Mastery, Skill.Professions.Ranger, 2, "5E 10R - Pet Attack. Deals +5...29...35 damage. Inflicts Bleeding condition if target foe is attacking (5...21...25 seconds)."));
            Data.Add(new Skill("Scavenger Strike", Properties.Resources.Scavenger_Strike, Skill.Attributes.Beast_Mastery, Skill.Professions.Ranger, 3, "5E 10R - Pet Attack. Deals +10...22...25 damage. You gain 3...13...15 Energy if target foe has a condition."));
            Data.Add(new Skill("Melandru's Assault", Properties.Resources.Melandru_s_Assault, Skill.Attributes.Beast_Mastery, Skill.Professions.Ranger, 4, "10E 5R - Pet Attack. Deals +5...17...20 damage to nearby foes."));
            Data.Add(new Skill("Ferocious Strike", Properties.Resources.Ferocious_Strike, Skill.Attributes.Beast_Mastery, Skill.Professions.Ranger, 4, "5E 8R - Elite Pet Attack. Deals +13...25...28 damage. You gain adrenaline and 3...9...10 energy.", true));
            Data.Add(new Skill("Predator's Pounce", Properties.Resources.Predator_s_Pounce, Skill.Attributes.Beast_Mastery, Skill.Professions.Ranger, 5, "5E 5R - Pet Attack. Deals +5...29...35 damage. Your pet gains 5...41...50 Health if this attack hits."));
            Data.Add(new Skill("Brutal Strike", Properties.Resources.Brutal_Strike, Skill.Attributes.Beast_Mastery, Skill.Professions.Ranger, 3, "10E 5R - Pet Attack. Deals +5...29...35 damage. Deals +5...29...35 more damage if target foe is under 50% health."));
            Data.Add(new Skill("Disrupting Lunge", Properties.Resources.Disrupting_Lunge, Skill.Attributes.Beast_Mastery, Skill.Professions.Ranger, 1, "5E 20R - Pet Attack. Deals +1...10...12 damage. Interrupts a skill. Interruption effect: interrupted skill is disabled for +20 seconds."));
            Data.Add(new Skill("Troll Unguent", Properties.Resources.Troll_Unguent, Skill.Attributes.Wilderness_Survival, Skill.Professions.Ranger, 2, "5E 3C 10R - Skill. (13 seconds.) You have +3...9...10 Health regeneration."));
            Data.Add(new Skill("Otyugh's Cry", Properties.Resources.Otyugh_s_Cry, Skill.Attributes.Beast_Mastery, Skill.Professions.Ranger, 2, "5E 30R - Shout. (10...22...25 seconds.) Your pet has +24 armor and is unblockable."));
            Data.Add(new Skill("Escape", Properties.Resources.Escape, Skill.Attributes.Expertise, Skill.Professions.Ranger, 4, "5E 12R - Elite Stance. (1...7...8 second[s].) You move 33% faster and have 75% chance to block.", true));
            Data.Add(new Skill("Practiced Stance", Properties.Resources.Practiced_Stance, Skill.Attributes.Expertise, Skill.Professions.Ranger, 2, "5E 15R - Elite Stance. (20...32...35 seconds.) Your preparations recharge 50% faster and last 30...126...150% longer.", true));
            Data.Add(new Skill("Whirling Defense", Properties.Resources.Whirling_Defense, Skill.Attributes.Expertise, Skill.Professions.Ranger, 3, "10E 60R - Stance. (8...18...20 seconds.) You have 75% chance to block. Deals 5...10...11 damage to adjacent foes whenever you block a projectile attack."));
            Data.Add(new Skill("Melandru's Resilience", Properties.Resources.Melandru_s_Resilience, Skill.Attributes.Wilderness_Survival, Skill.Professions.Ranger, 1, "5E 15R - Elite Stance. (8...18...20 seconds.) You have +4 Health regeneration and +1 Energy regeneration for each condition and hex on you.", true));
            Data.Add(new Skill("Dryder's Defenses", Properties.Resources.Dryder_s_Defenses, Skill.Attributes.Wilderness_Survival, Skill.Professions.Ranger, 2, "5E 60R - Stance. (5...10...11 seconds.) You have 75% chance to block and +34...55...60 armor against elemental damage."));
            Data.Add(new Skill("Lightning Reflexes", Properties.Resources.Lightning_Reflexes, Skill.Attributes.Expertise, Skill.Professions.Ranger, 3, "10E 30R - Stance. (5...10...11 seconds.) You attack 33% faster and have 75% chance to block."));
            Data.Add(new Skill("Tiger's Fury", Properties.Resources.Tiger_s_Fury, Skill.Attributes.Beast_Mastery, Skill.Professions.Ranger, 1, "10E 10R - Stance. (5...10...11 seconds.) You attack 25% faster. Disables your non-attack skills (5 seconds)."));
            Data.Add(new Skill("Storm Chaser", Properties.Resources.Storm_Chaser, Skill.Attributes.Wilderness_Survival, Skill.Professions.Ranger, 2, "10E 20R - Stance. (8...18...20 seconds.) You move 25% faster and gain 1...4...5 Energy whenever you take elemental damage."));
            Data.Add(new Skill("Serpent's Quickness", Properties.Resources.Serpent_s_Quickness, Skill.Attributes.Wilderness_Survival, Skill.Professions.Ranger, 2, "5E 45R - Stance. (15...27...30 seconds.) Your skills recharge 33% faster. Ends if your Health drops below 50%."));
            Data.Add(new Skill("Dust Trap", Properties.Resources.Dust_Trap, Skill.Attributes.Wilderness_Survival, Skill.Professions.Ranger, 2, "25E 2C 30R - Trap. (90 seconds.) Affects nearby foes. Deals 10...22...25 damage every second (5 seconds). Inflicts Blindness condition (3...7...8 seconds) every second (5 seconds). Easily interrupted."));
            Data.Add(new Skill("Barbed Trap", Properties.Resources.Barbed_Trap, Skill.Attributes.Wilderness_Survival, Skill.Professions.Ranger, 3, "15E 2C 20R - Trap. (90 seconds.) Affects nearby foes. Deals 7...20...23 piercing damage. Inflicts Crippled and Bleeding conditions (3...21...25 seconds). Easily interrupted."));
            Data.Add(new Skill("Flame Trap", Properties.Resources.Flame_Trap, Skill.Attributes.Wilderness_Survival, Skill.Professions.Ranger, 2, "10E 2C 20R - Trap. (90 seconds.) Affects nearby foes. Deals 5...17...20 fire damage every second (3 seconds). Inflicts Burning condition (1...3...3 second[s]). Easily interrupted."));
            Data.Add(new Skill("Healing Spring", Properties.Resources.Healing_Spring, Skill.Attributes.Wilderness_Survival, Skill.Professions.Ranger, 3, "10E 2C 20R - Trap. (10 seconds.) Affects adjacent allies. Heals for 15...51...60 every 2 seconds. Easily interrupted."));
            Data.Add(new Skill("Spike Trap", Properties.Resources.Spike_Trap, Skill.Attributes.Wilderness_Survival, Skill.Professions.Ranger, 3, "10E 2C 20R - Elite Trap. (90 seconds.) Affects nearby foes. Every second, (for 2 seconds), this trap deals 10...34...40 piercing damage, causes knockdown, and inflicts Crippled condition (3...21...25 seconds). Easily interrupted.", true));
            Data.Add(new Skill("Winter", Properties.Resources.Winter, Skill.Attributes.Wilderness_Survival, Skill.Professions.Ranger, 2, "5E 3C 60R - Nature Ritual. Creates a level 1...8...10 spirit (30...126...150 second lifespan). Converts elemental damage to cold damage for creatures in range. Does not affect spirits."));
            Data.Add(new Skill("Winnowing", Properties.Resources.Winnowing, Skill.Attributes.Wilderness_Survival, Skill.Professions.Ranger, 2, "5E 5C 60R - Nature Ritual. Creates a level 1...8...10 spirit (30...126...150 second lifespan). Increases physical damage by +4 for creatures in range. Does not affect spirits."));
            Data.Add(new Skill("Edge of Extinction", Properties.Resources.Edge_of_Extinction, Skill.Attributes.Beast_Mastery, Skill.Professions.Ranger, 2, "5E 5C 60R - Nature Ritual. Creates a level 1...8...10 spirit (30...126...150 second lifespan). Deals 14...43...50 damage to creatures in its range whenever a creature of the same type dies. Does not affect spirits. No damage to creatures above 90% Health."));
            Data.Add(new Skill("Greater Conflagration", Properties.Resources.Greater_Conflagration, Skill.Attributes.Wilderness_Survival, Skill.Professions.Ranger, 2, "5E 3C 15R - Elite Nature Ritual. Creates a level 1...8...10 spirit (30...126...150 second lifespan). Converts physical damage to fire damage for creatures in range. Does not affect spirits.", true));
            Data.Add(new Skill("Conflagration", Properties.Resources.Conflagration, Skill.Attributes.Wilderness_Survival, Skill.Professions.Ranger, 1, "5E 5C 60R - Nature Ritual. Creates a level 1...8...10 spirit (30...126...150 second lifespan). All arrows deal fire damage for creatures in range. Does not affect spirits."));
            Data.Add(new Skill("Fertile Season", Properties.Resources.Fertile_Season, Skill.Attributes.Beast_Mastery, Skill.Professions.Ranger, 1, "15E 5C 60R - Nature Ritual. Creates a level 1...8...10 spirit (15...39...45 second lifespan). Creatures in range have +50...130...150 maximum health and +8 armor. Does not affect spirits."));
            Data.Add(new Skill("Symbiosis", Properties.Resources.Symbiosis, Skill.Attributes.Beast_Mastery, Skill.Professions.Ranger, 2, "5E 5C 60R - Nature Ritual. Creates a level 1...8...10 spirit (30...126...150 second lifespan). Creatures in range have +27...125...150 maximum health for each enchantment on them. Does not affect spirits."));
            Data.Add(new Skill("Primal Echoes", Properties.Resources.Primal_Echoes, Skill.Attributes.Beast_Mastery, Skill.Professions.Ranger, 2, "5E 5C 60R - Nature Ritual. Creates a level 1...8...10 spirit (30...126...150 second lifespan). Signets cost 10 energy for creatures in range. Does not affect spirits."));
            Data.Add(new Skill("Predatory Season", Properties.Resources.Predatory_Season, Skill.Attributes.Beast_Mastery, Skill.Professions.Ranger, 2, "5E 5C 60R - Nature Ritual. Creates a level 1...8...10 spirit (30...126...150 second lifespan). Creatures in range receive 20% less from healing. Creatures gain 5 health each time they hit with an attack. Does not affect spirits."));
            Data.Add(new Skill("Frozen Soil", Properties.Resources.Frozen_Soil, Skill.Attributes.Wilderness_Survival, Skill.Professions.Ranger, 1, "10E 5C 30R - Nature Ritual. Creates a level 1...8...10 spirit. (30...78...90 second lifespan). Creatures in range cannot activate resurrection skills. Does not affect spirits."));
            Data.Add(new Skill("Favorable Winds", Properties.Resources.Favorable_Winds, Skill.Attributes.Marksmanship, Skill.Professions.Ranger, 2, "5E 5C 60R - Nature Ritual. Creates a level 1...8...10 spirit (30...126...150 second lifespan). Arrows move twice as fast and hit for +6 damage for creatures in range. Does not affect spirits."));
            Data.Add(new Skill("Energizing Wind", Properties.Resources.Energizing_Wind, Skill.Attributes.Beast_Mastery, Skill.Professions.Ranger, 1, "15E 5C 60R - Nature Ritual. Creates a level 1...5...6 spirit (1...25...31 second[s] lifespan). Skills cost 15 less energy (minimum cost 10 energy) and recharge 25% slower for creatures in range. Does not affect spirits."));
            Data.Add(new Skill("Quickening Zephyr", Properties.Resources.Quickening_Zephyr, Skill.Attributes.Wilderness_Survival, Skill.Professions.Ranger, 2, "25E 5C 60R - Nature Ritual. Creates a level 1...8...10 spirit (15...39...45 second lifespan). Skills cost 30% more Energy and recharge twice as fast for creatures in range. Does not affect spirits."));
            Data.Add(new Skill("Nature's Renewal", Properties.Resources.Nature_s_Renewal, Skill.Attributes.Wilderness_Survival, Skill.Professions.Ranger, 1, "5E 5C 60R - Nature Ritual. Creates a level 1...8...10 spirit (30...126...150 second lifespan). For creatures in range, enchantments and hexes take twice as long to cast and it costs twice as much Energy to maintain enchantments. Does not affect spirits."));
            Data.Add(new Skill("Muddy Terrain", Properties.Resources.Muddy_Terrain, Skill.Attributes.Wilderness_Survival, Skill.Professions.Ranger, 1, "5E 5C 30R - Nature Ritual. Creates a level 1...8...10 spirit (30...78...90 second lifespan). Creatures in range have 10% slower movement: also negates speed boosts. Does not affect spirits."));

            // This guy is a bit of an anomoly. This is a second id for Lightning Orb (the first being 229) HE IS BROKEN:
            //Data.Add(new Skill("Lightning Orb", Properties.Resources.Lightning_Orb, Skill.Attributes.Air_Magic, Skill.Professions.Elementalist));

            Data.Add(new Skill("Mark of Insecurity", Properties.Resources.Mark_of_Insecurity, Skill.Attributes.Deadly_Arts, Skill.Professions.Assassin, 2, "5E 1C 10R - Elite Hex Spell. (5...21...25 seconds.) Causes 1...3...3 Health degeneration. Enchantments and stances expire 30...70...80% faster on target foe. Disables your non-Assassin skills (10 seconds).", true));
            Data.Add(new Skill("Disrupting Dagger", Properties.Resources.Disrupting_Dagger, Skill.Attributes.Deadly_Arts, Skill.Professions.Assassin, 4, "5E 1/4C 10R - Half Range Spell. Projectile: deals 10...30...35 earth damage. Interrupts a skill."));
            Data.Add(new Skill("Deadly Paradox", Properties.Resources.Deadly_Paradox, Skill.Attributes.Deadly_Arts, Skill.Professions.Assassin, 2, "15E 10R - Stance. (5...13...15 seconds.) Your Assassin skills activate and recharge 33% faster. Disables your attack skills (10 seconds)."));
            Data.Add(new Skill("Jaundiced Gaze", Properties.Resources.Jaundiced_Gaze, Skill.Attributes.Blood_Magic, Skill.Professions.Necromancer, 2, "10E 1C 15R - Enchantment Spell. Removes an enchantment from target foe. Removal effect: your next enchantment casts 0...1...1 second[s] faster and costs 1...8...10 less Energy. (1...12...15 second[s])"));
            Data.Add(new Skill("Wail of Doom", Properties.Resources.Wail_of_Doom, Skill.Attributes.Soul_Reaping, Skill.Professions.Necromancer, 1, "10%HP 1E 1/4C 15R - Elite Hex Spell. (1...3...4 second[s].) Target foe's attributes are 0.", true));
            Data.Add(new Skill("Gaze of Contempt", Properties.Resources.Gaze_of_Contempt, Skill.Attributes.None, Skill.Professions.Necromancer, 3, "10E 2C 15R - Spell. Removes target foe's enchantments. No effect unless this foe has more than 50% Health."));
            Data.Add(new Skill("Viper's Defense", Properties.Resources.Viper_s_Defense, Skill.Attributes.Shadow_Arts, Skill.Professions.Assassin, 3, "5E 1/4C 10R - Spell. Inflict Poisoned condition (5...17...20 seconds) on all adjacent foes and Shadow Step to a nearby location directly away from your target."));
            Data.Add(new Skill("Return", Properties.Resources.Return, Skill.Attributes.Shadow_Arts, Skill.Professions.Assassin, 2, "5E 1/4C 15R - Spell. Inflicts Crippled condition (3...7...8 seconds) on all foes adjacent to you. You Shadow Step to target ally's location. Cannot self-target."));
            Data.Add(new Skill("Aura of Displacement", Properties.Resources.Aura_of_Displacement, Skill.Attributes.None, Skill.Professions.Assassin, 2, "-1ER 10E 1/4C 20R - Elite Enchantment Spell. Shadow Step to target foe. End effect: return to your original location.", true));
            Data.Add(new Skill("Generous Was Tsungrai", Properties.Resources.Generous_Was_Tsungrai, Skill.Attributes.Restoration_Magic, Skill.Professions.Ritualist, 2, "10%HP 5E 1C 15R - Item Spell. (15...51...60 seconds.) You have +50...122...140 maximum Health. Drop effect: you gain 100...244...280 Health."));
            Data.Add(new Skill("Mighty Was Vorizun", Properties.Resources.Mighty_Was_Vorizun, Skill.Attributes.Communing, Skill.Professions.Ritualist, 3, "5E 2C 30R - Item Spell. (15...51...60 seconds.) You have +15 armor and +30 maximum Energy."));
            Data.Add(new Skill("Death Blossom", Properties.Resources.Death_Blossom, Skill.Attributes.Dagger_Mastery, Skill.Professions.Assassin, 5, "5E 2R - Dual Attack. Deals +20...40...45 damage. Also affects foes adjacent to target foe. Must follow an off-hand attack."));
            Data.Add(new Skill("Twisting Fangs", Properties.Resources.Twisting_Fangs, Skill.Attributes.Critical_Strikes, Skill.Professions.Assassin, 3, "10E 15R - Dual Attack. Deals +10...18...20 damage. Inflicts Bleeding and Deep Wound conditions (5...17...20 seconds). Must follow an off-hand attack."));
            Data.Add(new Skill("Horns of the Ox", Properties.Resources.Horns_of_the_Ox, Skill.Attributes.Dagger_Mastery, Skill.Professions.Assassin, 3, "5E 12R - Dual Attack. Deals +1...9...11 damage. Causes knock-down if the target foe is not adjacent to any of its allies. Must follow an off-hand attack."));
            Data.Add(new Skill("Falling Spider", Properties.Resources.Falling_Spider, Skill.Attributes.Dagger_Mastery, Skill.Professions.Assassin, 2, "5E 8R - Off-Hand Attack. Deals +15...31...35 damage. Inflicts Poisoned condition (5...17...20 seconds). No effect unless target foe is knocked-down."));
            Data.Add(new Skill("Black Lotus Strike", Properties.Resources.Black_Lotus_Strike, Skill.Attributes.Critical_Strikes, Skill.Professions.Assassin, 2, "10E 6R - Lead Attack. Deals +10...27...31 damage. You gain 5...11...13 Energy if target foe is hexed."));
            Data.Add(new Skill("Fox Fangs", Properties.Resources.Fox_Fangs, Skill.Attributes.Dagger_Mastery, Skill.Professions.Assassin, 3, "5E 1/2C 3R - Off-Hand Attack. Deals +10...30...35 damage. Unblockable. Must follow a lead attack."));
            Data.Add(new Skill("Moebius Strike", Properties.Resources.Moebius_Strike, Skill.Attributes.Dagger_Mastery, Skill.Professions.Assassin, 4, "5E 2R - Elite Off-Hand Attack. Deals +10...30...35 damage. Recharges all your other attack skills if target foe's Health is below 50%. Must follow a dual attack.", true));
            Data.Add(new Skill("Jagged Strike", Properties.Resources.Jagged_Strike, Skill.Attributes.Dagger_Mastery, Skill.Professions.Assassin, 3, "5E 1/2C 1R - Lead Attack. Inflicts Bleeding condition (5...17...20 seconds)."));
            Data.Add(new Skill("Unsuspecting Strike", Properties.Resources.Unsuspecting_Strike, Skill.Attributes.Critical_Strikes, Skill.Professions.Assassin, 3, "10E 2R - Lead Attack. Deals +19...29...31 damage. Deals 15...63...75 more damage if target foe's Health is above 90%."));
            Data.Add(new Skill("Entangling Asp", Properties.Resources.Entangling_Asp, Skill.Attributes.Deadly_Arts, Skill.Professions.Assassin, 2, "10E 1C 20R - Spell. Causes knock-down. Inflicts Poisoned condition (5...17...20 seconds). Must follow a lead attack."));
            Data.Add(new Skill("Mark of Death", Properties.Resources.Mark_of_Death, Skill.Attributes.Deadly_Arts, Skill.Professions.Assassin, 1, "10E 1/4C 20R - Hex Spell. (4...9...10 seconds.) Target foe receives 33% less from healing."));
            Data.Add(new Skill("Iron Palm", Properties.Resources.Iron_Palm, Skill.Attributes.Deadly_Arts, Skill.Professions.Assassin, 3, "5E 3/4C 20R - Touch Skill. Deals 5...41...50 damage. Causes knock-down if target foe is hexed or has a condition. Counts as a lead attack."));
            Data.Add(new Skill("Resilient Weapon", Properties.Resources.Resilient_Weapon, Skill.Attributes.Restoration_Magic, Skill.Professions.Ritualist, 2, "10E 1C 6R - Weapon Spell. (3...10...12 seconds.) +1...5...6 Health regeneration and +24 armor. No effect unless target ally is hexed or has a condition."));
            Data.Add(new Skill("Blind Was Mingson", Properties.Resources.Blind_Was_Mingson, Skill.Attributes.Restoration_Magic, Skill.Professions.Ritualist, 3, "5E 1C 20R - Item Spell. (15...51...60 seconds.) Drop effect: inflicts Blindness condition (3...7...8 seconds) on all nearby foes."));
            Data.Add(new Skill("Grasping Was Kuurong", Properties.Resources.Grasping_Was_Kuurong, Skill.Attributes.Channeling_Magic, Skill.Professions.Ritualist, 3, "15E 1C 20R - Elite Item Spell. (15...51...60 seconds.) Drop effect: deal 15...63...75 damage and knocks-down all nearby foes.", true));
            Data.Add(new Skill("Vengeful Was Khanhei", Properties.Resources.Vengeful_Was_Khanhei, Skill.Attributes.Restoration_Magic, Skill.Professions.Ritualist, 2, "5E 3/4C 20R - Elite Item Spell. (5...10...11 seconds.) You steal 5...29...35 Health from every foe that hits you with an attack.", true));
            Data.Add(new Skill("Flesh of My Flesh", Properties.Resources.Flesh_of_My_Flesh, Skill.Attributes.Restoration_Magic, Skill.Professions.Ritualist, 4, "5E 4C - Spell. Resurrect target party member (half your current Health and 5...17...20% Energy). Lose half your Health."));
            Data.Add(new Skill("Splinter Weapon", Properties.Resources.Splinter_Weapon, Skill.Attributes.Channeling_Magic, Skill.Professions.Ritualist, 5, "5E 1C 5R - Weapon Spell. (20 seconds.) Attacks deal 5...41...50 damage to 4 adjacent foes. Ends after 1...4...5 attack[s]."));
            Data.Add(new Skill("Weapon of Warding", Properties.Resources.Weapon_of_Warding, Skill.Attributes.Restoration_Magic, Skill.Professions.Ritualist, 3, "10E 1C 5R - Weapon Spell. (3...7...8 seconds.) +2...4...4 Health regeneration. 50% chance to block."));
            Data.Add(new Skill("Wailing Weapon", Properties.Resources.Wailing_Weapon, Skill.Attributes.Channeling_Magic, Skill.Professions.Ritualist, 1, "5E 1C 25R - Weapon Spell. (3...8...9 seconds.) Attacks interrupt attacking foes."));
            Data.Add(new Skill("Nightmare Weapon", Properties.Resources.Nightmare_Weapon, Skill.Attributes.Channeling_Magic, Skill.Professions.Ritualist, 4, "5E 1C 10R - Weapon Spell. (12 seconds.) Target ally's attacks steal 10...42...50 Health but deal 10...42...50 less damage. Ends after 3 attacks."));
            Data.Add(new Skill("Beguiling Haze", Properties.Resources.Beguiling_Haze, Skill.Attributes.Shadow_Arts, Skill.Professions.Assassin, 4, "15E 1/4C 20R - Elite Spell. You Shadow Step to this foe. Inflicts Dazed condition (3...8...9 seconds).", true));
            Data.Add(new Skill("Enduring Toxin", Properties.Resources.Enduring_Toxin, Skill.Attributes.Deadly_Arts, Skill.Professions.Assassin, 3, "5E 1/4C 10R - Hex Spell. (5 seconds.) Causes -1...4...5 Health degeneration. Renewal: if the target foe is moving when this hex ends."));
            Data.Add(new Skill("Shroud of Silence", Properties.Resources.Shroud_of_Silence, Skill.Attributes.Deadly_Arts, Skill.Professions.Assassin, 1, "10E 3/4C 30R - Elite Touch Hex Spell. (1...3...3 second[s].) Target foe cannot cast spells. Your spells are disabled for 15 seconds.", true));
            Data.Add(new Skill("Expose Defenses", Properties.Resources.Expose_Defenses, Skill.Attributes.Deadly_Arts, Skill.Professions.Assassin, 2, "10E 1C 25R - Hex Spell. (1...9...11 second[s].) Target foe cannot block your attacks."));
            Data.Add(new Skill("Power Leech", Properties.Resources.Power_Leech, Skill.Attributes.Inspiration_Magic, Skill.Professions.Mesmer, 3, "5E 1/4C 20R - Elite Hex Spell. Interrupt a spell or a chant. Interruption effect: steal 5...13...15 Energy whenever target foe casts a spell (10 seconds).", true));
            Data.Add(new Skill("Arcane Languor", Properties.Resources.Arcane_Languor, Skill.Attributes.Fast_Casting, Skill.Professions.Mesmer, 1, "10E 2C 15R - Elite Hex Spell. (1...8...10 second[s].) Target foe's spells cause 10 Overcast.", true));
            Data.Add(new Skill("Animate Vampiric Horror", Properties.Resources.Animate_Vampiric_Horror, Skill.Attributes.Death_Magic, Skill.Professions.Necromancer, 3, "15E 3C 15R - Spell. Creates a level 1...14...17 vampiric horror. You gain Health equal to the damage it deals. Exploits a fresh corpse."));
            Data.Add(new Skill("Cultist's Fervor", Properties.Resources.Cultist_s_Fervor, Skill.Attributes.Blood_Magic, Skill.Professions.Necromancer, 2, "5E 1C 25R - Elite Enchantment Spell. (5 seconds plus 3 seconds more for every rank of Soul Reaping.) Your Necromancer spells cost 1...5...6 less Energy. You suffer from Bleeding (10 seconds) each time you cast a Necromancer spell.", true));
            Data.Add(new Skill("Reaper's Mark", Properties.Resources.Reaper_s_Mark, Skill.Attributes.Soul_Reaping, Skill.Professions.Necromancer, 2, "5E 1C 15R - Elite Hex Spell. (30 seconds.) Causes -1...4...5 Health degeneration. You gain 5...13...15 Energy if target foe dies while suffering from this hex.", true));
            Data.Add(new Skill("Shatterstone", Properties.Resources.Shatterstone, Skill.Attributes.Water_Magic, Skill.Professions.Elementalist, 3, "10E 2C 8R - Elite Hex Spell. (3 seconds.) Initial effect: deals 25...85...100 cold damage. End effect: deals 25...85...100 cold damage to target and all nearby foes.", true));
            Data.Add(new Skill("Protector's Defense", Properties.Resources.Protector_s_Defense, Skill.Attributes.Tactics, Skill.Professions.Warrior, 1, "5E 30R - Skill. (5...10...11 seconds.) Allies adjacent to you have 75% chance to block. Ends if you move."));
            Data.Add(new Skill("Run as One", Properties.Resources.Run_as_One, Skill.Attributes.Beast_Mastery, Skill.Professions.Ranger, 4, "5E 15R - Stance. (5...13...15 seconds.) You and your pet move 25% faster."));
            // Artificial break.
            Data.Add(new Skill("Defiant Was Xinrae", Properties.Resources.Defiant_Was_Xinrae, Skill.Attributes.Restoration_Magic, Skill.Professions.Ritualist, 2, "5E 1C 20R - Elite Item Spell. (15...51...60 seconds.) You cannot lose more than 20% of your max Health from a single hit. Drop effect: steal 5...41...50 Health from nearby foes.", true));
            Data.Add(new Skill("Lyssa's Aura", Properties.Resources.Lyssa_s_Aura, Skill.Attributes.Inspiration_Magic, Skill.Professions.Mesmer, 3, "5E 1C 30R - Elite Enchantment Spell. (10 seconds.) You have +0...4...5 Energy regeneration. Renewal: every time you cast a spell on an enemy.", true));
            Data.Add(new Skill("Shadow Refuge", Properties.Resources.Shadow_Refuge, Skill.Attributes.Shadow_Arts, Skill.Professions.Assassin, 2, "5E 1C 8R - Enchantment Spell. (6 seconds.) You have +5...9...10 Health regeneration. End effect: heals you for 40...88...100 if you are attacking."));
            Data.Add(new Skill("Scorpion Wire", Properties.Resources.Scorpion_Wire, Skill.Attributes.Deadly_Arts, Skill.Professions.Assassin, 2, "5E 1C 10R - Half Range Hex Spell. (8...18...20 seconds.) Shadow Step to target foe and cause knock-down the next time this foe is more than 100' away from you."));
            Data.Add(new Skill("Mirrored Stance", Properties.Resources.Mirrored_Stance, Skill.Attributes.Shadow_Arts, Skill.Professions.Assassin, 2, "5E 1/4C 15R - Hex Spell. (10...30...35 seconds.) You enter any stance used by target foe."));
            Data.Add(new Skill("Discord", Properties.Resources.Discord, Skill.Attributes.Death_Magic, Skill.Professions.Necromancer, 4, "5E 1C 2R - Elite Spell. Deals 30...94...110 damage. No effect unless target foe has a condition and is either hexed or enchanted.", true));
            Data.Add(new Skill("Well of Weariness", Properties.Resources.Well_of_Weariness, Skill.Attributes.Curses, Skill.Professions.Necromancer, 2, "5E 1C 5R - Well Spell. (10...46...55 seconds.) Foes in this well have -1 Energy degeneration. Exploits a fresh corpse."));
            Data.Add(new Skill("Vampiric Spirit", Properties.Resources.Vampiric_Spirit, Skill.Attributes.Blood_Magic, Skill.Professions.Necromancer, 2, "5E 2C 8R - Elite Enchantment Spell. Steal 5...41...50 Health from target foe. You have +5...9...10 Health regeneration (10 seconds).", true));
            Data.Add(new Skill("Depravity", Properties.Resources.Depravity, Skill.Attributes.Curses, Skill.Professions.Necromancer, 2, "10E 2C 15R - Elite Hex Spell. (5...17...20 seconds.) Causes 1...4...5 Energy loss whenever target foe casts a spell. One foe near your target also loses Energy.", true));
            Data.Add(new Skill("Icy Veins", Properties.Resources.Icy_Veins, Skill.Attributes.Soul_Reaping, Skill.Professions.Necromancer, 4, "10E 1C 5R - Elite Hex Spell. (10...30...35 seconds.) Deals 20...92...110 cold damage to nearby foes if target foe dies. Initial effect: deals 10...74...90 cold damage.", true));
            Data.Add(new Skill("Weaken Knees", Properties.Resources.Weaken_Knees, Skill.Attributes.Curses, Skill.Professions.Necromancer, 3, "5E 1C 10R - Elite Hex Spell. (1...13...16 second[s].) Target foe has -1...3...4 Health degeneration and takes 5...9...10 damage while moving.", true));
            Data.Add(new Skill("Burning Speed", Properties.Resources.Burning_Speed, Skill.Attributes.Fire_Magic, Skill.Professions.Elementalist, 2, "10E 1/4C - Enchantment Spell. (7 seconds.) You move 30...42...45% faster. You suffer from Burning (7 seconds). End effect: inflicts Burning condition (3...8...9 seconds) on adjacent foes."));
            Data.Add(new Skill("Lava Arrows", Properties.Resources.Lava_Arrows, Skill.Attributes.Fire_Magic, Skill.Professions.Elementalist, 3, "5E 1C 2R - Spell. Projectile: deals 20...56...65 fire damage. Bonus effect: sends projectiles at 2 other foes near your target."));
            Data.Add(new Skill("Bed of Coals", Properties.Resources.Bed_of_Coals, Skill.Attributes.Fire_Magic, Skill.Professions.Elementalist, 3, "10E 1C 15R - Spell. Deals 5...24...29 fire damage each second (5 seconds) to location of target foe. Hits foes adjacent to your target. Inflicts Burning condition (3...6...7 seconds) on knocked down foes."));
            Data.Add(new Skill("Shadow Form", Properties.Resources.Shadow_Form, Skill.Attributes.Shadow_Arts, Skill.Professions.Assassin, 2, "5E 1C 30R - Elite Enchantment Spell. (5...18...21 seconds.) Enemy spells cannot target you. Gain 5 damage reduction for each Assassin enchantment on you. You cannot deal more than 5...21...25 damage with a single skill or attack.", true));
            Data.Add(new Skill("Siphon Strength", Properties.Resources.Siphon_Strength, Skill.Attributes.Deadly_Arts, Skill.Professions.Assassin, 3, "10E 1C 10R - Elite Hex Spell. (5...17...20 seconds.) Target foe deals -5...41...50 attack damage. You have +33% chance to land a critical hit on this foe.", true));
            Data.Add(new Skill("Vile Miasma", Properties.Resources.Vile_Miasma, Skill.Attributes.Death_Magic, Skill.Professions.Necromancer, 3, "10E 1C 15R - Hex Spell. Causes -1...4...5 Health degeneration (10 seconds). Initial effect: deals 10...54...65 cold damage. Hex is only applied if target foe has a condition."));
            Data.Add(new Skill("Ray of Judgment", Properties.Resources.Ray_of_Judgment, Skill.Attributes.Smiting_Prayers, Skill.Professions.Monk, 5, "10E 2C 20R - Elite Spell. Deals 5...37...45 holy damage and inflicts Burning (1...3...3 second[s]) every second (5 seconds). Hits foes adjacent to target's initial location.", true));
            Data.Add(new Skill("Primal Rage", Properties.Resources.Primal_Rage, Skill.Attributes.Strength, Skill.Professions.Warrior, 3, "4A 10R - Elite Stance. (1...12...15 second[s].) You attack 33% faster and move 25% faster. You take double damage.", true));
            Data.Add(new Skill("Animate Flesh Golem", Properties.Resources.Animate_Flesh_Golem, Skill.Attributes.Death_Magic, Skill.Professions.Necromancer, 3, "10E 3C 30R - Elite Spell. Creates a level 3...21...25 flesh golem which leaves a fresh corpse when it dies. Exploits a fresh corpse. You can have only one flesh golem at a time.", true));
            Data.Add(new Skill("Reckless Haste", Properties.Resources.Reckless_Haste, Skill.Attributes.Curses, Skill.Professions.Necromancer, 4, "15E 1C 12R - Hex Spell. Also hexes foes adjacent to your target (6...11...12 seconds). These foes have 50% chance to miss, but attack 25% faster."));
            Data.Add(new Skill("Blood Bond", Properties.Resources.Blood_Bond, Skill.Attributes.Blood_Magic, Skill.Professions.Necromancer, 4, "5E 1C 8R - Hex Spell. Also hexes foes adjacent to your target (3...10...12 seconds). Allies hitting these foes gain 5...17...20 health. If any of these foes dies while hexed, adjacent allies are healed for 20...84...100."));
            Data.Add(new Skill("Ride the Lightning", Properties.Resources.Ride_the_Lightning, Skill.Attributes.Air_Magic, Skill.Professions.Elementalist, 2, "5O 10E 1C 5R - Elite Spell. Deals 10...58...70 lightning damage. 25% armor penetration. Blinds all adjacent foes (1...4...5 second[s]). You instantly move to your target. May target allies.", true));
            Data.Add(new Skill("Energy Boon", Properties.Resources.Energy_Boon, Skill.Attributes.Energy_Storage, Skill.Professions.Elementalist, 2, "10E 1C 20R - Elite Enchantment Spell. (36...55...60 seconds.) Maximum Health for you and target ally is increased by 1...3...3 for each point of maximum Energy. Initial effect: Both gain 1...10...12 Energy. You gain +1 Energy for every 2 points of Energy Storage.", true));
            Data.Add(new Skill("Dwayna's Sorrow", Properties.Resources.Dwayna_s_Sorrow, Skill.Attributes.Healing_Prayers, Skill.Professions.Monk, 2, "5E 1C 5R - Enchantment Spell. Enchants allies near your target (30 seconds). Your party is healed for 5...41...50 whenever one of these allies dies."));
            Data.Add(new Skill("\"Retreat!\"", Properties.Resources._Retreat__, Skill.Attributes.Tactics, Skill.Professions.Warrior, 1, "5E 20R - Shout. (5...10...11 seconds.) Party members in earshot move 33% faster. No effect unless there are dead allies within earshot."));
            Data.Add(new Skill("Poisoned Heart", Properties.Resources.Poisoned_Heart, Skill.Attributes.Curses, Skill.Professions.Necromancer, 2, "5E 1/4C 12R - Spell. Inflicts Poisoned condition (5...13...15 seconds) to adjacent foes. You are also Poisoned."));
            Data.Add(new Skill("Fetid Ground", Properties.Resources.Fetid_Ground, Skill.Attributes.Death_Magic, Skill.Professions.Necromancer, 2, "5E 3/4C 10R - Spell. Deals 15...55...65 cold damage. Inflicts Poisoned condition (5...17...20 seconds) if target foe is knocked-down."));
            Data.Add(new Skill("Arc Lightning", Properties.Resources.Arc_Lightning, Skill.Attributes.Air_Magic, Skill.Professions.Elementalist, 4, "5E 1.5C 8R - Spell. Deals 5...33...40 lightning damage. Deals 15...71...85 lightning damage to two nearby foes if you are Overcast. 25% armor penetration."));
            Data.Add(new Skill("Gust", Properties.Resources.Gust, Skill.Attributes.Air_Magic, Skill.Professions.Elementalist, 3, "10E 3/4C 10R - Elite Enchantment Spell. (5...10...11 seconds.) You and target ally move 33% faster. Initial effect: Foes near you and target ally are struck for 15...59...70 cold damage. Attacking or moving foes are knocked down.", true));
            Data.Add(new Skill("Churning Earth", Properties.Resources.Churning_Earth, Skill.Attributes.Earth_Magic, Skill.Professions.Elementalist, 2, "15E 2C 30R - Spell. Deals 10...34...40 earth damage each second (5 seconds). Hits foes near target's initial location. Causes knock-down to foes moving faster than normal."));
            Data.Add(new Skill("Liquid Flame", Properties.Resources.Liquid_Flame, Skill.Attributes.Fire_Magic, Skill.Professions.Elementalist, 4, "10E 1C 15R - Spell. Deals 7...91...112 fire damage. Deals 7...91...112 fire damage to nearby foes if target was attacking or casting."));
            Data.Add(new Skill("Steam", Properties.Resources.Steam, Skill.Attributes.Water_Magic, Skill.Professions.Elementalist, 2, "5E 1C 8R - Spell. Deals 20...52...60 cold damage. Inflicts Blindness condition (5...9...10 seconds) if target foe is Burning."));
            Data.Add(new Skill("Boon Signet", Properties.Resources.Boon_Signet, Skill.Attributes.Divine_Favor, Skill.Professions.Monk, 3, "1C 8R - Elite Signet. Heals for 20...68...80. Your next Healing or Protection Prayers spell that targets an ally heals for +20...68...80 Health.", true));
            Data.Add(new Skill("Reverse Hex", Properties.Resources.Reverse_Hex, Skill.Attributes.Protection_Prayers, Skill.Professions.Monk, 2, "10E 1/4C 10R - Enchantment Spell. (5...9...10 seconds.) Removes one hex from target ally. The next damage this ally takes is reduced by 5...41...50. No effect unless this ally is hexed."));
            Data.Add(new Skill("Lacerating Chop", Properties.Resources.Lacerating_Chop, Skill.Attributes.Axe_Mastery, Skill.Professions.Warrior, 1, "5A - Axe Attack. Deals +5...17...20 damage. Inflicts Bleeding condition (5...17...20 seconds) if target foe is knocked-down."));
            Data.Add(new Skill("Fierce Blow", Properties.Resources.Fierce_Blow, Skill.Attributes.Hammer_Mastery, Skill.Professions.Warrior, 5, "6A - Hammer Attack. Deals +5...17...20 damage. Inflicts Deep Wound (1...7...8 second[s]) if target foe is Weakened."));
            Data.Add(new Skill("Sun and Moon Slash", Properties.Resources.Sun_and_Moon_Slash, Skill.Attributes.Swordsmanship, Skill.Professions.Warrior, 4, "8A - Sword Attack. You attack target foe twice. Unblockable."));
            Data.Add(new Skill("Splinter Shot", Properties.Resources.Splinter_Shot, Skill.Attributes.Marksmanship, Skill.Professions.Ranger, 2, "10E 5R - Bow Attack. Deals +3...13...15 damage. Deals 5...53...65 damage to adjacent foes if blocked."));
            Data.Add(new Skill("Melandru's Shot", Properties.Resources.Melandru_s_Shot, Skill.Attributes.Marksmanship, Skill.Professions.Ranger, 3, "5E 1C 8R - Elite Bow Attack. Inflicts Bleeding (5...21...25 seconds). Deals +10...22...25 damage and inflicts Crippled (5...13...15 seconds) if target foe was moving or knocked down.", true));
            Data.Add(new Skill("Snare", Properties.Resources.Snare, Skill.Attributes.Wilderness_Survival, Skill.Professions.Ranger, 1, "5E 2C 20R - Trap. (90 seconds.) Affects nearby foes. Inflicts Crippled condition (3...13...15 seconds). Easily interrupted."));
            Data.Add(new Skill("Dancing Daggers", Properties.Resources.Dancing_Daggers, Skill.Attributes.Deadly_Arts, Skill.Professions.Assassin, 3, "5E 1C 5R - Half Range Spell. Three projectiles: each deals 5...29...35 earth damage. Counts as a lead attack."));
            Data.Add(new Skill("Conjure Nightmare", Properties.Resources.Conjure_Nightmare, Skill.Attributes.Illusion_Magic, Skill.Professions.Mesmer, 3, "15E 1C 5R - Hex Spell. (2...13...16 seconds.) Causes -8 Health degeneration."));
            Data.Add(new Skill("Signet of Disruption", Properties.Resources.Signet_of_Disruption, Skill.Attributes.Domination_Magic, Skill.Professions.Mesmer, 2, "1/4C 15R - Signet. Interrupts a spell. Can interrupt any skill if target foe is hexed. Interruption effect: deals 10...43...51 damage."));
            Data.Add(new Skill("Ravenous Gaze", Properties.Resources.Ravenous_Gaze, Skill.Attributes.Blood_Magic, Skill.Professions.Necromancer, 3, "8%HP 1E 1C 10R - Elite Spell. Deals 15...27...30 damage and steals 15...27...30 Health from target and nearby foes.", true));
            Data.Add(new Skill("Order of Apostasy", Properties.Resources.Order_of_Apostasy, Skill.Attributes.Curses, Skill.Professions.Necromancer, 1, "25E 2C - Elite Enchantment Spell. Enchants all party members (5 seconds). These party members remove one enchantment when they deal physical damage. Removal cost: for each Monk enchantment, you lose 25...17...15% maximum Health.", true));
            Data.Add(new Skill("Oppressive Gaze", Properties.Resources.Oppressive_Gaze, Skill.Attributes.Blood_Magic, Skill.Professions.Necromancer, 2, "10E 1C 10R - Spell. Deals 10...26...30 damage to target and adjacent foes. Inflicts Poison and Weakness (3...10...12 second) on foes suffering from a condition."));
            Data.Add(new Skill("Lightning Hammer", Properties.Resources.Lightning_Hammer, Skill.Attributes.Air_Magic, Skill.Professions.Elementalist, 4, "25E 2C 4R - Spell. Deals 10...82...100 lightning damage. Applies Cracked Armor (5...17...20 seconds). 25% armor penetration."));
            Data.Add(new Skill("Vapor Blade", Properties.Resources.Vapor_Blade, Skill.Attributes.Water_Magic, Skill.Professions.Elementalist, 4, "5O 10E 3/2C 7R - Spell. Deals 15...111...135 cold damage. Half damage if target foe is enchanted."));
            Data.Add(new Skill("Healing Light", Properties.Resources.Healing_Light, Skill.Attributes.Healing_Prayers, Skill.Professions.Monk, 3, "5E 1C 4R - Elite Spell. Heals for 40...88...100. You gain 1...3...3 Energy if target ally is enchanted.", true));
            Data.Add(new Skill("\"Coward!\"", Properties.Resources._Coward__, Skill.Attributes.None, Skill.Professions.Warrior, 1, "4A 2R - Elite Shout. If target foe is moving, that foe is knocked down.", true));
            Data.Add(new Skill("Pestilence", Properties.Resources.Pestilence, Skill.Attributes.Wilderness_Survival, Skill.Professions.Ranger, 2, "5E 5C 60R - Nature Ritual. Creates a level 1...8...10 spirit (30...78...90 second lifespan). When any creature in range dies, conditions on this creature spread to any creature in the area with a condition. Spirits are not affected."));
            Data.Add(new Skill("Shadowsong", Properties.Resources.Shadowsong, Skill.Attributes.Communing, Skill.Professions.Ritualist, 4, "15E 1C 30R - Binding Ritual. Creates a level 1...8...10 spirit (30-second lifespan). Its attacks deal 5...17...20 damage and inflict Blindness condition (1...5...6 second[s])."));
            Data.Add(new Skill("Signet of Shadows", Properties.Resources.Signet_of_Shadows, Skill.Attributes.Deadly_Arts, Skill.Professions.Assassin, 2, "1C 30R - Signet. Deals 5...29...35 damage. Deals 15...51...60 more damage if target foe is Blinded."));
            Data.Add(new Skill("Lyssa's Balance", Properties.Resources.Lyssa_s_Balance, Skill.Attributes.None, Skill.Professions.Mesmer, 1, "5E 1C 15R - Spell. Removes one enchantment from target foe. No effect if you have more enchantments than this foe."));
            Data.Add(new Skill("Visions of Regret", Properties.Resources.Visions_of_Regret, Skill.Attributes.Domination_Magic, Skill.Professions.Mesmer, 4, "10E 2C 20R - Elite Hex Spell. Also hexes foes adjacent to target (10 seconds). These foes take 15...39...45 damage whenever they use a skill and 5...41...50 additional damage if not under the effects of another Mesmer hex.", true));
            Data.Add(new Skill("Illusion of Pain", Properties.Resources.Illusion_of_Pain, Skill.Attributes.Illusion_Magic, Skill.Professions.Mesmer, 2, "10E 2C 5R - Hex Spell. (8 seconds.) Causes -3...9...10 Health degeneration and target foe takes 3...9...10 damage each second. End effect: that foe is healed for 36...103...120."));
            Data.Add(new Skill("Stolen Speed", Properties.Resources.Stolen_Speed, Skill.Attributes.Fast_Casting, Skill.Professions.Mesmer, 3, "5E 1C 12R - Elite Hex Spell. Also hexes adjacent foes (1...8...10 seconds). Doubles spell casting time. Spells cast by you or your allies have -50% casting times.", true));
            Data.Add(new Skill("Ether Signet", Properties.Resources.Ether_Signet, Skill.Attributes.Inspiration_Magic, Skill.Professions.Mesmer, 3, "1C 45R - Signet. You gain 10...18...20 Energy. No effect unless you have less than 5...9...10 Energy."));
            Data.Add(new Skill("Signet of Disenchantment", Properties.Resources.Signet_of_Disenchantment, Skill.Attributes.None, Skill.Professions.Mesmer, 2, "1C 15R - Signet. Removes one enchantment. You lose all Energy."));
            Data.Add(new Skill("Vocal Minority", Properties.Resources.Vocal_Minority, Skill.Attributes.Curses, Skill.Professions.Necromancer, 2, "10E 1C 20R - Hex Spell. Also hexes foes near target (5...17...20 seconds). These foes cannot use shouts or chants."));
            // Artificial break.
            Data.Add(new Skill("Searing Flames", Properties.Resources.Searing_Flames, Skill.Attributes.Fire_Magic, Skill.Professions.Elementalist, 3, "15E 1C 2R - Elite Spell. Hits foes near your target. Deals 10...82...100 fire damage to foes already Burning. Inflicts Burning condition (1...6...7 seconds) to foes not Burning.", true));
            Data.Add(new Skill("Shield Guardian", Properties.Resources.Shield_Guardian, Skill.Attributes.Protection_Prayers, Skill.Professions.Monk, 3, "5E 3/2E 20R - Enchantment Spell. (1...3...4 second[s]). Party members in earshot have a 75% chance to block attacks. Block effect: Allies in earshot are healed for 10...34...40, and Shield Guardian ends."));
            Data.Add(new Skill("Restful Breeze", Properties.Resources.Restful_Breeze, Skill.Attributes.Healing_Prayers, Skill.Professions.Monk, 1, "5E 1C 8R - Enchantment Spell. (8...16...18 seconds.) +10 Health regeneration. Ends if target ally attacks or uses a skill."));
            Data.Add(new Skill("Signet of Rejuvenation", Properties.Resources.Signet_of_Rejuvenation, Skill.Attributes.Healing_Prayers, Skill.Professions.Monk, 5, "1C 8R - Signet. Heals for 15...63...75. Heals for 15...63...75 more if target ally is casting a spell or attacking."));
            Data.Add(new Skill("Whirling Axe", Properties.Resources.Whirling_Axe, Skill.Attributes.Axe_Mastery, Skill.Professions.Warrior, 3, "4A - Elite Axe Attack. Deals +5...17...20 damage and removes a stance. Unblockable.", true));
            Data.Add(new Skill("Forceful Blow", Properties.Resources.Forceful_Blow, Skill.Attributes.Hammer_Mastery, Skill.Professions.Warrior, 5, "5A - Elite Hammer Attack. Deals +10...26...30 damage. Remove target foe's stance. Inflicts Weakness condition (5...17...20 seconds). Unblockable.", true));
            Data.Add(new Skill("\"None Shall Pass!\"", Properties.Resources._None_Shall_Pass__, Skill.Attributes.Tactics, Skill.Professions.Warrior, 2, "5E 45R - Shout. Knocks down all nearby moving foes. Recharges 1...7...8 second[s] faster for each affected foe (maximum 25 seconds)."));
            Data.Add(new Skill("Quivering Blade", Properties.Resources.Quivering_Blade, Skill.Attributes.Swordsmanship, Skill.Professions.Warrior, 5, "5A - Elite Sword Attack. Deals +10...34...40 damage. Inflicts Dazed condition (5 seconds) if target foe was moving.", true));
            Data.Add(new Skill("Seeking Arrows", Properties.Resources.Seeking_Arrows, Skill.Attributes.Marksmanship, Skill.Professions.Ranger, 2, "15E 2C 20R - Preparation. (3...12...14 seconds.) Your arrows are unblockable. Ends if you fail to hit."));
            Data.Add(new Skill("Overload", Properties.Resources.Overload, Skill.Attributes.Domination_Magic, Skill.Professions.Mesmer, 3, "5E 1/4C 5R - Hex Spell. (5 seconds.) Causes -1...3...3 Health degeneration. If target foe is using a skill, that foe and all adjacent foes take 15...63...75 damage."));
            Data.Add(new Skill("Images of Remorse", Properties.Resources.Images_of_Remorse, Skill.Attributes.Illusion_Magic, Skill.Professions.Mesmer, 4, "5E 2C 5R - Hex Spell. (5...9...10 seconds.) Causes -1...3...3 Health degeneration. Initial effect: 10...44...52 damage if target foe is attacking."));
            Data.Add(new Skill("Shared Burden", Properties.Resources.Shared_Burden, Skill.Attributes.Illusion_Magic, Skill.Professions.Mesmer, 3, "5E 2C 15R - Elite Hex Spell. Also hexes foes near your target (5...17...20 seconds). These foes attack, cast spells, and move 50% slower.", true));
            Data.Add(new Skill("Soul Bind", Properties.Resources.Soul_Bind, Skill.Attributes.Curses, Skill.Professions.Necromancer, 2, "10E 1C 5R - Elite Hex Spell. (30 seconds.) Every time target foe is healed, the healer takes 20...68...80 damage. Ends if target is suffering from a Smiting Prayers hex.", true));
            Data.Add(new Skill("Blood of the Aggressor", Properties.Resources.Blood_of_the_Aggressor, Skill.Attributes.Blood_Magic, Skill.Professions.Necromancer, 4, "5E 1C 5R - Spell. Steal 5...37...45 Health. Inflicts Weakness (3...10...12 seconds) if target foe was attacking."));
            Data.Add(new Skill("Icy Prism", Properties.Resources.Icy_Prism, Skill.Attributes.Water_Magic, Skill.Professions.Elementalist, 3, "5O 10E 1C 5R - Spell. Deals 15...63...75 cold damage. Deals +15...63...75 cold damage to other nearby foes if target has a Water Magic hex."));
            Data.Add(new Skill("Furious Axe", Properties.Resources.Furious_Axe, Skill.Attributes.Axe_Mastery, Skill.Professions.Warrior, 4, "5E 6R - Axe Attack. Deals +5...29...35 damage. Gives you 3 strikes of adrenaline if blocked."));
            Data.Add(new Skill("Auspicious Blow", Properties.Resources.Auspicious_Blow, Skill.Attributes.Hammer_Mastery, Skill.Professions.Warrior, 4, "5A - Hammer Attack. Deals +5...17...20 damage and you gain 3...7...8 Energy. Unblockable if target foe is Weakened."));
            Data.Add(new Skill("\"On Your Knees!\"", Properties.Resources._On_Your_Knees__, Skill.Attributes.None, Skill.Professions.Warrior, 2, "6A - Shout. Recharges all your stances if you are adjacent to a knocked-down foe. You lose all adrenaline."));
            Data.Add(new Skill("Dragon Slash", Properties.Resources.Dragon_Slash, Skill.Attributes.Swordsmanship, Skill.Professions.Warrior, 2, "10A - Elite Sword Attack. Deals +10...34...40 damage. You gain 1...4...5 strike[s] of adrenaline if it hits.", true));
            Data.Add(new Skill("Marauder's Shot", Properties.Resources.Marauder_s_Shot, Skill.Attributes.Marksmanship, Skill.Professions.Ranger, 1, "10E 6R - Bow Attack. Deals +10...30...35 damage. Your non-attack skills are disabled (10 seconds) if this attack hits."));
            Data.Add(new Skill("Focused Shot", Properties.Resources.Focused_Shot, Skill.Attributes.Marksmanship, Skill.Professions.Ranger, 2, "5E 2R - Bow Attack. Deals +10...22...25 damage. Your other attack skills are disabled (5...3...3 seconds) if this attack hits."));
            Data.Add(new Skill("Spirit Rift", Properties.Resources.Spirit_Rift, Skill.Attributes.Channeling_Magic, Skill.Professions.Ritualist, 3, "10E 2C 5R - Spell. After 3 seconds, affects foes adjacent to target's initial location. Deals 25...105...125 lightning damage and inflicts Cracked Armor (1...16...20 second[s])."));
            Data.Add(new Skill("Union", Properties.Resources.Union, Skill.Attributes.Communing, Skill.Professions.Ritualist, 3, "15E 3/4C 45R - Binding Ritual. Creates a level 1...10...12 spirit (30...54...60 second lifespan). Whenever a non-spirit ally within range takes damage or life steal, it is reduced by 15 and the spirit takes 15 damage."));
            Data.Add(new Skill("Tranquil Was Tanasen", Properties.Resources.Tranquil_Was_Tanasen, Skill.Attributes.Restoration_Magic, Skill.Professions.Ritualist, 2, "10E 1C 20R - Elite Item Spell. (5...17...20 seconds.) You have +10...22...25 armor. You cannot be interrupted.", true));
            Data.Add(new Skill("Consume Soul", Properties.Resources.Consume_Soul, Skill.Attributes.Spawning_Power, Skill.Professions.Ritualist, 2, "5E 1C 5R - Elite Spell. Steals 5...49...60 Health. Deal 25...105...125 damage to hostile summoned creatures in the area of target foe.", true));
            Data.Add(new Skill("Spirit Light", Properties.Resources.Spirit_Light, Skill.Attributes.Restoration_Magic, Skill.Professions.Ritualist, 5, "17%HP 5E 1C 4R - Spell. Heals for 60...156...180. You don't sacrifice Health if you are within earshot of any spirits."));
            Data.Add(new Skill("Lamentation", Properties.Resources.Lamentation, Skill.Attributes.Channeling_Magic, Skill.Professions.Ritualist, 3, "10E 1C 15R - Hex Spell. Also hexes foes near your target (5...17...20 seconds). Causes -0...2...3 Health degeneration. Initial effect: Deals 10...42...50 damage to these foes if you are within earshot of a spirit or corpse."));
            Data.Add(new Skill("Rupture Soul", Properties.Resources.Rupture_Soul, Skill.Attributes.Spawning_Power, Skill.Professions.Ritualist, 3, "10E 3/4C 5R - Spell. Destroys target allied spirit. Deals 50...122...140 lightning damage and inflicts Blindness condition (3...10...12 seconds) to nearby foes."));
            Data.Add(new Skill("Spirit to Flesh", Properties.Resources.Spirit_to_Flesh, Skill.Attributes.Spawning_Power, Skill.Professions.Ritualist, 3, "10E 3/4C 15R - Touch Spell. Destroys target touched allied spirit. Heals nearby allies for 30...198...240."));
            Data.Add(new Skill("Spirit Burn", Properties.Resources.Spirit_Burn, Skill.Attributes.Channeling_Magic, Skill.Professions.Ritualist, 3, "5E 1C 6R - Spell. Deals 5...41...50 lightning damage. Inflicts Burning condition (1...4...5 second[s]) if you are within earshot of a spirit."));
            Data.Add(new Skill("Destruction", Properties.Resources.Destruction, Skill.Attributes.Channeling_Magic, Skill.Professions.Ritualist, 3, "10E 3/4C 20R - Binding Ritual. Create a level 1...11...14 Spirit that dies after 30 seconds. When this Spirit dies, all foes in the area take 5...21...25 damage for each second the Spirit was alive (maximum 150 damage)."));
            Data.Add(new Skill("Dissonance", Properties.Resources.Dissonance, Skill.Attributes.Communing, Skill.Professions.Ritualist, 2, "25E 1C 45R - Binding Ritual. Creates a level 1...10...12 spirit (10...22...25 second lifespan). Its attacks deal 5...17...20 damage and interrupt actions."));
            Data.Add(new Skill("Disenchantment", Properties.Resources.Disenchantment, Skill.Attributes.Communing, Skill.Professions.Ritualist, 3, "15E 1C 30R - Binding Ritual. Creates a level 1...10...12 spirit (10...30...35 second lifespan). Its attacks deal 5...17...20 damage and remove one enchantment."));
            Data.Add(new Skill("Recall", Properties.Resources.Recall, Skill.Attributes.None, Skill.Professions.Assassin, 1, "-1ER 15E 1C 10R - Enchantment Spell. End effect: Shadow Step to target ally. Cannot self-target and disables all of your skills for 10 seconds when it ends."));
            Data.Add(new Skill("Sharpen Daggers", Properties.Resources.Sharpen_Daggers, Skill.Attributes.Critical_Strikes, Skill.Professions.Assassin, 3, "5E 1C 20R - Enchantment Spell. (5...25...30 seconds.) Your dagger attacks inflict the Bleeding condition (5...13...15 seconds)."));
            Data.Add(new Skill("Shameful Fear", Properties.Resources.Shameful_Fear, Skill.Attributes.Deadly_Arts, Skill.Professions.Assassin, 2, "10E 2C 10R - Hex Spell. (10 seconds.) Target foe takes 5...17...20 damage each second while moving, but moves 10% faster."));
            Data.Add(new Skill("Shadow Shroud", Properties.Resources.Shadow_Shroud, Skill.Attributes.Shadow_Arts, Skill.Professions.Assassin, 2, "10E 1C 20R - Elite Hex Spell. (3...8...9 seconds.) Target foe cannot be the target of enchantments.", true));
            Data.Add(new Skill("Shadow of Haste", Properties.Resources.Shadow_of_Haste, Skill.Attributes.Shadow_Arts, Skill.Professions.Assassin, 2, "5E 45R - Stance. (10...34...40 seconds.) You move 15% faster. End effect: return to your original location."));
            Data.Add(new Skill("Auspicious Incantation", Properties.Resources.Auspicious_Incantation, Skill.Attributes.Inspiration_Magic, Skill.Professions.Mesmer, 2, "5E 1C 25R - Enchantment Spell. (20 seconds.) Your next spell gives you 110...182...200% of its Energy cost. That spell takes 10...6...5 seconds longer to recharge."));
            Data.Add(new Skill("Power Return", Properties.Resources.Power_Return, Skill.Attributes.Fast_Casting, Skill.Professions.Mesmer, 4, "5E 1/4C 7R - Spell. Interrupts a spell or chant. Interruption effect: target foe gains 10...6...5 Energy."));
            Data.Add(new Skill("Complicate", Properties.Resources.Complicate, Skill.Attributes.Domination_Magic, Skill.Professions.Mesmer, 4, "10E 1/4C 20R - Spell. Interrupt a skill. Interruption effect: disables interrupted skill (+5...11...12 seconds) for target foe and all foes in the area."));
            Data.Add(new Skill("Shatter Storm", Properties.Resources.Shatter_Storm, Skill.Attributes.None, Skill.Professions.Mesmer, 1, "10E 1C - Elite Spell. Removes all enchantments. Removal cost: Shatter Storm is disabled for +7 seconds for each enchantment removed.", true));
            Data.Add(new Skill("Unnatural Signet", Properties.Resources.Unnatural_Signet, Skill.Attributes.Domination_Magic, Skill.Professions.Mesmer, 5, "1C 10R - Signet. Deals 15...63...75 damage. Deals 5...41...50 damage to other adjacent foes if the target is hexed or enchanted."));
            Data.Add(new Skill("Rising Bile", Properties.Resources.Rising_Bile, Skill.Attributes.Death_Magic, Skill.Professions.Necromancer, 3, "10E 1C 20R - Hex Spell. (20 seconds.) End effect: deals 1...5...6 damage for each second Rising Bile was in effect. Also damages other foes in the area."));
            Data.Add(new Skill("Envenom Enchantments", Properties.Resources.Envenom_Enchantments, Skill.Attributes.Curses, Skill.Professions.Necromancer, 3, "5E 1C 20R - Spell. Removes one enchantment from target foe. Inflicts Poisoned condition (3...9...10 seconds for each remaining enchantment on that foe)."));
            Data.Add(new Skill("Shockwave", Properties.Resources.Shockwave, Skill.Attributes.Earth_Magic, Skill.Professions.Elementalist, 2, "10E 1C 15R - Elite Spell. Foes in the area take 15...51...60 earth damage and are Weakened (1...8...10 second[s]). Nearby foes also take +15...51...60 earth damage and have Cracked Armor (1...8...10 second[s]). Adjacent foes also take +15...51...60 earth damage and are Blinded (1...8...10 second[s]).", true));
            Data.Add(new Skill("Ward of Stability", Properties.Resources.Ward_of_Stability, Skill.Attributes.Earth_Magic, Skill.Professions.Elementalist, 2, "10E 1C 30R - Ward Spell. (10...22...25 seconds.) Allies in this ward cannot be knocked-down. Allied spirits are not affected."));
            Data.Add(new Skill("Icy Shackles", Properties.Resources.Icy_Shackles, Skill.Attributes.Water_Magic, Skill.Professions.Elementalist, 1, "10E 1C 12R - Elite Hex Spell. (1...8...10 second[s].) Target foe moves 66% slower. This foe moves 90% slower if enchanted.", true));
            Data.Add(new Skill("Blessed Light", Properties.Resources.Blessed_Light, Skill.Attributes.Divine_Favor, Skill.Professions.Monk, 3, "10E 3/4C 3R - Elite Spell. Heals for 10...114...140. Removes one condition and one hex.", true));
            Data.Add(new Skill("Withdraw Hexes", Properties.Resources.Withdraw_Hexes, Skill.Attributes.Divine_Favor, Skill.Professions.Monk, 1, "15E 1C 5R - Elite Spell. Removes all hexes. Also affects adjacent allies. Removal cost: +20...8...5 seconds recharge for each hex removed.", true));
            Data.Add(new Skill("Extinguish", Properties.Resources.Extinguish, Skill.Attributes.Protection_Prayers, Skill.Professions.Monk, 2, "15E 1C 12R - Spell. Affects all party members. Removes one condition. Party members relieved of Burning are healed for 10...82...100."));
            Data.Add(new Skill("Signet of Strength", Properties.Resources.Signet_of_Strength, Skill.Attributes.Strength, Skill.Professions.Warrior, 3, "1C 45R - Signet. Your attacks deal +5 damage. Ends after 1...13...16 attack[s]."));
            Data.Add(new Skill("Trapper's Focus", Properties.Resources.Trapper_s_Focus, Skill.Attributes.Expertise, Skill.Professions.Ranger, 2, "5E 2C 12R - Elite Preparation. (12...22...24 seconds.) Your trap skills are no longer easy to interrupt. You gain +0...2...2 to your Wilderness Survival attribute.", true));
            Data.Add(new Skill("Brambles", Properties.Resources.Brambles, Skill.Attributes.Wilderness_Survival, Skill.Professions.Ranger, 2, "10E 5C 60R - Nature Ritual. Creates a level 1...8...10 spirit (30...126...150 second lifespan). Knocked-down creatures take 5 damage and begin Bleeding (5...17...20 seconds). Does not affect spirits."));
            Data.Add(new Skill("Desperate Strike", Properties.Resources.Desperate_Strike, Skill.Attributes.Dagger_Mastery, Skill.Professions.Assassin, 2, "5E 6R - Lead Attack. Deals +15...51...60 damage if you have less than 50...74...80% Health."));
            Data.Add(new Skill("Way of the Fox", Properties.Resources.Way_of_the_Fox, Skill.Attributes.Shadow_Arts, Skill.Professions.Assassin, 1, "5E 1/4C 45R - Enchantment Spell. (10...30...35 seconds.) Your attacks are unblockable. Ends after 1...5...6 attack[s]."));
            Data.Add(new Skill("Shadowy Burden", Properties.Resources.Shadowy_Burden, Skill.Attributes.Shadow_Arts, Skill.Professions.Assassin, 2, "10E 1/4C 15R - Hex Spell. (3...13...15 seconds.) Target foe moves 25% slower and has 20 less armor against your attacks. Armor reduction only affects this foe while it has no other hexes."));
            Data.Add(new Skill("Siphon Speed", Properties.Resources.Siphon_Speed, Skill.Attributes.Deadly_Arts, Skill.Professions.Assassin, 3, "5E 1C 30R - Half Range Hex Spell. (5...13...15 seconds.) Target foe moves 33% slower and you move 33% faster. Recharges 50% faster if cast on a moving foe."));
            Data.Add(new Skill("Death's Charge", Properties.Resources.Death_s_Charge, Skill.Attributes.Shadow_Arts, Skill.Professions.Assassin, 4, "5E 1/4C 30R - Spell. You Shadow Step to target foe. You are healed for 65...173...200 if this foe has more Health than you."));
            Data.Add(new Skill("Power Flux", Properties.Resources.Power_Flux, Skill.Attributes.Domination_Magic, Skill.Professions.Mesmer, 1, "10E 1/4C 10R - Elite Hex Spell. Interrupts a spell or chant. Interruption effect: -2 Energy degeneration (4...9...10 seconds).", true));
            Data.Add(new Skill("Expel Hexes", Properties.Resources.Expel_Hexes, Skill.Attributes.None, Skill.Professions.Mesmer, 2, "5E 1C 8R - Elite Spell. Removes 2 hexes from target ally.", true));
            Data.Add(new Skill("Rip Enchantment", Properties.Resources.Rip_Enchantment, Skill.Attributes.Curses, Skill.Professions.Necromancer, 3, "5E 1C 15R - Spell. Removes 1 enchantment. Removal effect: inflicts Bleeding (5...21...25 seconds)."));
            Data.Add(new Skill("Spell Shield", Properties.Resources.Spell_Shield, Skill.Attributes.Divine_Favor, Skill.Professions.Monk, 1, "10E 2C 30R - Enchantment Spell. (5...17...20 seconds.) While casting spells, you cannot be the target of spells. End effect: your skills are disabled (10...6...5 seconds)."));
            Data.Add(new Skill("Healing Whisper", Properties.Resources.Healing_Whisper, Skill.Attributes.Healing_Prayers, Skill.Professions.Monk, 3, "5E 1C 1R - Half Range Spell. Heals for 40...88...100. Cannot self-target."));
            Data.Add(new Skill("Ethereal Light", Properties.Resources.Ethereal_Light, Skill.Attributes.Healing_Prayers, Skill.Professions.Monk, 4, "5E 1C 5R - Spell. Heals for 25...85...100. Easily interrupted."));
            Data.Add(new Skill("Release Enchantments", Properties.Resources.Release_Enchantments, Skill.Attributes.Divine_Favor, Skill.Professions.Monk, 2, "5E 1C 5R - Spell. Removes all of your enchantments. Heals all party members for 5...29...35 for each Monk Enchantment removed."));
            Data.Add(new Skill("Lacerate", Properties.Resources.Lacerate, Skill.Attributes.Beast_Mastery, Skill.Professions.Ranger, 2, "10E 3C 15R - Elite Nature Ritual. Creates a level 1...8...10 spirit (30...126...150 second lifespan). Bleeding creatures in range have -2 Health degeneration. End effect: Inflicts Bleeding condition (5...21...25 seconds) on creatures in range that have less than 90% health. Does not affect spirits.", true));
            Data.Add(new Skill("Spirit Transfer", Properties.Resources.Spirit_Transfer, Skill.Attributes.Restoration_Magic, Skill.Professions.Ritualist, 2, "10E 1/4C 5R - Spell. The spirit nearest you loses 5...41...50 Health. Heals target ally for 5 for each point of Health lost."));
            Data.Add(new Skill("Restoration", Properties.Resources.Restoration, Skill.Attributes.Communing, Skill.Professions.Ritualist, 1, "10E 1C 30R - Binding Ritual. Creates a level 1...11...14 spirit (30 second lifespan). End effect: resurrects party members in the area with 5...41...50% Health and zero Energy."));
            Data.Add(new Skill("Vengeful Weapon", Properties.Resources.Vengeful_Weapon, Skill.Attributes.Restoration_Magic, Skill.Professions.Ritualist, 4, "5E 1/4C 3R - Weapon Spell. (8 seconds.) Steals 15...51...60 Health from the next foe that deals damage or life steal to target ally."));
            Data.Add(new Skill("Blinding Powder", Properties.Resources.Blinding_Powder, Skill.Attributes.Shadow_Arts, Skill.Professions.Assassin, 3, "5E 1/4C 20R - Spell. Inflicts Blindness condition (3...13...15 seconds) on target and adjacent foes. Must follow an off-hand attack."));
            Data.Add(new Skill("Mantis Touch", Properties.Resources.Mantis_Touch, Skill.Attributes.Deadly_Arts, Skill.Professions.Assassin, 2, "5E 3/4C 15R - Spell. Inflicts Crippled condition (5...17...20 seconds). This skill counts as an off-hand attack. Must follow a lead attack."));
            Data.Add(new Skill("Exhausting Assault", Properties.Resources.Exhausting_Assault, Skill.Attributes.Dagger_Mastery, Skill.Professions.Assassin, 2, "5E 1/2C 8R - Dual Attack. Interrupts an action. Inflicts 10 Overcast if the interrupted action was a spell. Must follow a lead attack."));
            Data.Add(new Skill("Repeating Strike", Properties.Resources.Repeating_Strike, Skill.Attributes.Dagger_Mastery, Skill.Professions.Assassin, 3, "5E - Off-Hand Attack. Deals +10...26...30 damage. This skill has +15 second recharge time if it misses. Must follow an off-hand attack."));
            Data.Add(new Skill("Way of the Lotus", Properties.Resources.Way_of_the_Lotus, Skill.Attributes.Shadow_Arts, Skill.Professions.Assassin, 2, "5E 1/4C 20R - Enchantment Spell. (20 seconds.) You gain 5...17...20 Energy the next time you hit with a dual attack."));
            Data.Add(new Skill("Mark of Instability", Properties.Resources.Mark_of_Instability, Skill.Attributes.None, Skill.Professions.Assassin, 2, "10E 1/4C 20R - Hex Spell. (20 seconds.) Causes knock-down the next time you hit target foe with a dual attack."));
            Data.Add(new Skill("Mistrust", Properties.Resources.Mistrust, Skill.Attributes.Domination_Magic, Skill.Professions.Mesmer, 5, "10E 2C 12R - Hex Spell. (6 seconds.) The next spell that target foe casts on one of your allies fails and deals 10...82...100 damage to target and nearby foes."));
            Data.Add(new Skill("Feast of Souls", Properties.Resources.Feast_of_Souls, Skill.Attributes.Spawning_Power, Skill.Professions.Ritualist, 2, "10E 1/4C 10R - Spell. Heals all party members for 50...90...100 for each nearby allied spirit. All nearby allied spirits are destroyed."));
            Data.Add(new Skill("Recuperation", Properties.Resources.Recuperation, Skill.Attributes.Restoration_Magic, Skill.Professions.Ritualist, 3, "25E 3/4C 45R - Binding Ritual. Creates a level 1...11...14 spirit (15...39...45 second lifespan). Non-spirit allies within range have +1...3...3 Health regeneration."));
            Data.Add(new Skill("Shelter", Properties.Resources.Shelter, Skill.Attributes.Communing, Skill.Professions.Ritualist, 2, "25E 1C 45R - Binding Ritual. Creates a level 1...10...12 spirit (30...54...60 second lifespan). Non-spirit allies within range cannot lose more than 10% maximum Health from a single attack. Damage prevention cost: this spirit loses 75...51...45 Health."));
            // Artificial break.
            Data.Add(new Skill("Weapon of Shadow", Properties.Resources.Weapon_of_Shadow, Skill.Attributes.Restoration_Magic, Skill.Professions.Ritualist, 3, "10E 1C 20R - Weapon Spell. (1...6...7 second[s].) Inflicts Blindness condition (5 seconds) on anyone who attacks target ally. Target ally's next 1...3...3 attack[s] inflict Blindness (5 seconds) if they hit."));
            Data.Add(new Skill("Caltrops", Properties.Resources.Caltrops, Skill.Attributes.Shadow_Arts, Skill.Professions.Assassin, 3, "10E 1/4C 10R - Half Range Spell. Inflicts Crippled condition (5...13...15 seconds) on target and adjacent foes."));
            Data.Add(new Skill("Nine Tail Strike", Properties.Resources.Nine_Tail_Strike, Skill.Attributes.Dagger_Mastery, Skill.Professions.Assassin, 3, "5E 8R - Dual Attack. Deals +15...35...40 damage. Unblockable. Must follow an off-hand attack."));
            Data.Add(new Skill("Way of the Empty Palm", Properties.Resources.Way_of_the_Empty_Palm, Skill.Attributes.Deadly_Arts, Skill.Professions.Assassin, 2, "5E 1/4C 10R - Elite Enchantment Spell. (5...17...20 seconds.) Your off-hand and dual attacks cost no Energy.", true));
            Data.Add(new Skill("Temple Strike", Properties.Resources.Temple_Strike, Skill.Attributes.Dagger_Mastery, Skill.Professions.Assassin, 3, "15E 20R - Elite Off-Hand Attack. Interrupts a spell. Inflicts Dazed and Blindness conditions (1...8...10 seconds). Must follow a lead attack.", true));
            Data.Add(new Skill("Golden Phoenix Strike", Properties.Resources.Golden_Phoenix_Strike, Skill.Attributes.Dagger_Mastery, Skill.Professions.Assassin, 3, "5E 8R - Off-Hand Attack. Deals +10...26...30 damage to target and deals 10...26...30 damage to adjacent foes. Fails if you are not enchanted."));
            Data.Add(new Skill("Expunge Enchantments", Properties.Resources.Expunge_Enchantments, Skill.Attributes.Deadly_Arts, Skill.Professions.Assassin, 2, "10E 3/4C 30R - Touch Skill. Removes one enchantment for each non-attack skill you have. All your non-attack skills are disabled (10...6...5 seconds)."));
            Data.Add(new Skill("Deny Hexes", Properties.Resources.Deny_Hexes, Skill.Attributes.Divine_Favor, Skill.Professions.Monk, 3, "5E 1C 12R - Spell. Removes one hex from target ally and one additional hex for each recharging Divine Favor skill you have."));
            Data.Add(new Skill("Triple Chop", Properties.Resources.Triple_Chop, Skill.Attributes.Axe_Mastery, Skill.Professions.Warrior, 4, "5E 10R - Elite Axe Attack. Deals +10...34...40 damage. Also hits adjacent foes.", true));
            Data.Add(new Skill("Enraged Smash", Properties.Resources.Enraged_Smash, Skill.Attributes.Hammer_Mastery, Skill.Professions.Warrior, 3, "5E 5R - Elite Hammer Attack. Gives you 1...3...4 strike[s] of adrenaline if you hit. Deals +10...34...40 damage and causes knockdown if target foe was moving.", true));
            Data.Add(new Skill("Renewing Smash", Properties.Resources.Renewing_Smash, Skill.Attributes.Hammer_Mastery, Skill.Professions.Warrior, 4, "5E 1C 30R - Hammer Attack. Deals +10...34...40 damage. You gain 3 Energy and this attack recharges instantly if target foe was knocked down."));
            Data.Add(new Skill("Tiger Stance", Properties.Resources.Tiger_Stance, Skill.Attributes.Strength, Skill.Professions.Warrior, 3, "5E 20R - Stance. (4...9...10 seconds.) You attack 33% faster. Ends if you fail to hit."));
            Data.Add(new Skill("Standing Slash", Properties.Resources.Standing_Slash, Skill.Attributes.Swordsmanship, Skill.Professions.Warrior, 5, "6A - Sword Attack. Deals +5...17...20 damage. Deals 5...17...20 more damage if you are in a stance."));
            Data.Add(new Skill("Famine", Properties.Resources.Famine, Skill.Attributes.Wilderness_Survival, Skill.Professions.Ranger, 2, "10E 3C 15R - Elite Nature Ritual. Creates a level 1...8...10 spirit (30...78...90 lifespan). Deals 10...30...35 damage to creatures in range that reach 0 energy. Does not affect spirits.", true));

            Data.Add(new Skill("Critical Eye", Properties.Resources.Critical_Eye, Skill.Attributes.Critical_Strikes, Skill.Professions.Assassin, 4, "5E 30R - Skill. (10...30...35 seconds.) You have +3...13...15% chance to land a critical hit. You gain 1 Energy whenever you land a critical hit."));
            Data.Add(new Skill("Critical Strike", Properties.Resources.Critical_Strike, Skill.Attributes.Critical_Strikes, Skill.Professions.Assassin, 4, "5E 6R - Dual Attack. Deals +10...26...30 damage. Automatic critical hit. You gain 1...3...3 Energy. Must follow an off-hand attack."));
            Data.Add(new Skill("Blades of Steel", Properties.Resources.Blades_of_Steel, Skill.Attributes.Dagger_Mastery, Skill.Professions.Assassin, 2, "5E 8R - Dual Attack. Deals +5...14...16 damage (maximum 60) for each recharging dagger attack. Must follow an off-hand attack."));
            Data.Add(new Skill("Jungle Strike", Properties.Resources.Jungle_Strike, Skill.Attributes.Dagger_Mastery, Skill.Professions.Assassin, 2, "5E 1/2C 10R - Off-Hand Attack. Deals +10...22...25 damage. Deals +1...25...31 damage to target and adjacent foes if target foe is Crippled. Must follow a lead attack."));
            Data.Add(new Skill("Wild Strike", Properties.Resources.Wild_Strike, Skill.Attributes.Dagger_Mastery, Skill.Professions.Assassin, 5, "5E 4R - Off-Hand Attack. Deals +10...30...35 damage. Removes target foe's stance. Unblockable. Must follow a lead attack."));
            Data.Add(new Skill("Leaping Mantis Sting", Properties.Resources.Leaping_Mantis_Sting, Skill.Attributes.Dagger_Mastery, Skill.Professions.Assassin, 3, "5E 1/2C 8R - Lead Attack. Deals +5...13...15 damage. Inflicts Crippled condition (3...13...15 seconds) if target foe is moving."));
            Data.Add(new Skill("Black Mantis Thrust", Properties.Resources.Black_Mantis_Thrust, Skill.Attributes.Dagger_Mastery, Skill.Professions.Assassin, 3, "5E 1C 6R - Lead Attack. Deals +8...18...20 damage. Inflicts Crippled condition (3...13...15 seconds) if target foe is hexed."));
            Data.Add(new Skill("Disrupting Stab", Properties.Resources.Disrupting_Stab, Skill.Attributes.Dagger_Mastery, Skill.Professions.Assassin, 3, "5E 10R - Lead Attack. Interrupts an action. If the interrupted action was a spell, it is disabled (3...9...10 seconds)."));
            Data.Add(new Skill("Golden Lotus Strike", Properties.Resources.Golden_Lotus_Strike, Skill.Attributes.Dagger_Mastery, Skill.Professions.Assassin, 3, "5E 5R - Lead Attack. Deals +5...17...20 damage. You gain 5...7...8 Energy if you are enchanted."));
            Data.Add(new Skill("Critical Defenses", Properties.Resources.Critical_Defenses, Skill.Attributes.Critical_Strikes, Skill.Professions.Assassin, 5, "10E 1C 30R - Enchantment Spell. (4...9...10 seconds.) You have a 75% chance to block. Renewal: every time you land a critical hit."));
            Data.Add(new Skill("Way of Perfection", Properties.Resources.Way_of_Perfection, Skill.Attributes.Shadow_Arts, Skill.Professions.Assassin, 4, "5E 1/4C 30R - Enchantment Spell. (60 seconds.) Your critical hits heal you for 10...34...40."));
            Data.Add(new Skill("Dark Apostasy", Properties.Resources.Dark_Apostasy, Skill.Attributes.Critical_Strikes, Skill.Professions.Assassin, 2, "10E 1/4C 15R - Elite Enchantment Spell. (3...14...17 seconds.) Your critical hits remove an enchantment. Removal cost: lose 10...5...4 Energy or Dark Apostasy ends.", true));
            Data.Add(new Skill("Locust's Fury", Properties.Resources.Locust_s_Fury, Skill.Attributes.Critical_Strikes, Skill.Professions.Assassin, 2, "10E 1C 10R - Elite Enchantment Spell. (10...30...35 seconds.) You have +50% chance to double strike. No effect unless you are wielding daggers.", true));
            Data.Add(new Skill("Shroud of Distress", Properties.Resources.Shroud_of_Distress, Skill.Attributes.Shadow_Arts, Skill.Professions.Assassin, 3, "10E 1C 45R - Enchantment Spell. (30...54...60 seconds.) You have 3...7...8 health regeneration and a 75% chance to block. No effect unless your Health is below 50%."));
            Data.Add(new Skill("Heart of Shadow", Properties.Resources.Heart_of_Shadow, Skill.Attributes.Shadow_Arts, Skill.Professions.Assassin, 2, "5E 1/4C 15R - Spell. You are healed for 30...126...150 and you Shadow Step to a nearby location directly away from your target."));
            Data.Add(new Skill("Impale", Properties.Resources.Impale, Skill.Attributes.Deadly_Arts, Skill.Professions.Assassin, 4, "5E 1C 15R - Skill. Deals 25...85...100 earth damage. Inflicts Deep Wound condition (5...17...20 seconds). Must follow a dual attack."));
            Data.Add(new Skill("Seeping Wound", Properties.Resources.Seeping_Wound, Skill.Attributes.Critical_Strikes, Skill.Professions.Assassin, 3, "15E 1/4C 12R - Elite Half Range Hex Spell. (1...6...7 second[s].) Target foe moves 33% slower. This foe takes 5...21...25 damage each second while suffering from a condition.", true));
            Data.Add(new Skill("Assassin's Promise", Properties.Resources.Assassin_s_Promise, Skill.Attributes.Deadly_Arts, Skill.Professions.Assassin, 3, "5E 3/4C 45R - Elite Hex Spell. (5...13...15 seconds.) You gain 5...17...20 Energy and all your skills recharge if target foe dies.", true));
            Data.Add(new Skill("Signet of Malice", Properties.Resources.Signet_of_Malice, Skill.Attributes.None, Skill.Professions.Assassin, 3, "1/4C 5R - Signet. You lose one condition for each condition on target foe."));
            Data.Add(new Skill("Dark Escape", Properties.Resources.Dark_Escape, Skill.Attributes.Shadow_Arts, Skill.Professions.Assassin, 2, "5E 30R - Stance. (5...13...15 seconds.) You move 25% faster and take half damage. Ends if you hit with an attack."));
            Data.Add(new Skill("Crippling Dagger", Properties.Resources.Crippling_Dagger, Skill.Attributes.Deadly_Arts, Skill.Professions.Assassin, 3, "5E 1C 5R - Half Range Spell. Projectile: deals 15...51...60 earth damage. Inflicts Crippled condition (3...13...15 seconds) if target foe is moving."));
            Data.Add(new Skill("Spirit Walk", Properties.Resources.Spirit_Walk, Skill.Attributes.None, Skill.Professions.Assassin, 2, "5E 1/4C 8R - Spell. Shadow Step to target spirit."));
            Data.Add(new Skill("Unseen Fury", Properties.Resources.Unseen_Fury, Skill.Attributes.Shadow_Arts, Skill.Professions.Assassin, 3, "5E 20R - Stance. Inflicts Blindness condition on adjacent foes (3...9...10). You cannot be blocked by Blinded foes for 10...26...30 seconds."));
            Data.Add(new Skill("Flashing Blades", Properties.Resources.Flashing_Blades, Skill.Attributes.Dagger_Mastery, Skill.Professions.Assassin, 3, "10E 30R - Elite Stance. (5...25...30 seconds.) You have 75% chance to block while attacking. Block effect: 5...17...20 damage to your attacker.", true));
            Data.Add(new Skill("Dash", Properties.Resources.Dash, Skill.Attributes.None, Skill.Professions.Assassin, 4, "5E 8R - Stance. (3 seconds.) You move 50% faster."));
            Data.Add(new Skill("Dark Prison", Properties.Resources.Dark_Prison, Skill.Attributes.Deadly_Arts, Skill.Professions.Assassin, 3, "10E 1/4C 30R - Hex Spell. Shadow Step to target foe. This foe moves 33% slower (1...5...6 seconds)."));
            Data.Add(new Skill("Palm Strike", Properties.Resources.Palm_Strike, Skill.Attributes.Critical_Strikes, Skill.Professions.Assassin, 3, "5E 3/4C 7R - Elite Touch Skill. Deals 10...54...65 damage and inflicts Crippled condition (1...4...5 second[s]). This skill counts as an off-hand attack.", true));

            Data.Add(new Skill("Revealed Enchantment", Properties.Resources.Revealed_Enchantment, Skill.Attributes.Inspiration_Magic, Skill.Professions.Mesmer, 2, "10E 1C - Spell. Removes an enchantment from target foe. Removal effects: you gain 3...13...15 Energy; this spell is replaced with that enchantment (20 seconds)."));
            Data.Add(new Skill("Revealed Hex", Properties.Resources.Revealed_Hex, Skill.Attributes.Inspiration_Magic, Skill.Professions.Mesmer, 3, "5E 1C - Spell. Removes a hex from target ally. Removal effects: you gain 4...9...10 Energy; this spell is replaced with that hex (20 seconds)."));
            Data.Add(new Skill("Accumulated Pain", Properties.Resources.Accumulated_Pain, Skill.Attributes.Illusion_Magic, Skill.Professions.Mesmer, 2, "5E 2C 12R - Spell. Deals 15...63...75 damage. Inflicts Deep Wound condition (5...17...20 seconds) if target foe has 2 or more hexes."));
            Data.Add(new Skill("Psychic Distraction", Properties.Resources.Psychic_Distraction, Skill.Attributes.Domination_Magic, Skill.Professions.Mesmer, 1, "10E 1/4C 2R - Elite Spell. Interrupts a skill. Interruption effect: disables interrupted skill (+5...11...12 seconds). Your other skills are disabled (8 seconds).", true));
            Data.Add(new Skill("Ancestor's Visage", Properties.Resources.Ancestor_s_Visage, Skill.Attributes.Illusion_Magic, Skill.Professions.Mesmer, 1, "10E 1C 20R - Enchantment Spell. (4...9...10 seconds.) All adjacent foes lose all adrenaline and 3 Energy whenever a melee attack hits target ally."));
            Data.Add(new Skill("Recurring Insecurity", Properties.Resources.Recurring_Insecurity, Skill.Attributes.Illusion_Magic, Skill.Professions.Mesmer, 1, "10E 1C 10R - Elite Hex Spell. (10 seconds.) Causes -1...4...5 Health degeneration. Renewal: if target foe has another hex when Recurring Insecurity would end.", true));
            Data.Add(new Skill("Kitah's Burden", Properties.Resources.Kitah_s_Burden, Skill.Attributes.Illusion_Magic, Skill.Professions.Mesmer, 2, "15E 3/2C 30R - Hex Spell. (10 seconds.) Target foe moves 50% slower. End effect: you gain 10...16...18 Energy."));
            Data.Add(new Skill("Psychic Instability", Properties.Resources.Psychic_Instability, Skill.Attributes.Fast_Casting, Skill.Professions.Mesmer, 4, "5E 1/4C 12R - Elite Spell. Interrupts an action. Interruption effect: if the action is a skill, cause knockdown for 2...4...4 seconds on target foe and all nearby foes. 50% failure chance unless Fast Casting 5 or higher.", true));
            Data.Add(new Skill("Hex Eater Signet", Properties.Resources.Hex_Eater_Signet, Skill.Attributes.Inspiration_Magic, Skill.Professions.Mesmer, 3, "1C 25R - Touch Signet. Removes a hex from target and 2...4...5 adjacent allies. Removal effect: you gain 1...3...4 Energy for each hex removed."));
            Data.Add(new Skill("Feedback", Properties.Resources.Feedback, Skill.Attributes.Inspiration_Magic, Skill.Professions.Mesmer, 2, "10E 2C 30R - Spell. Removes one enchantment. Removal effect: target foe loses 4...9...10 Energy."));
            Data.Add(new Skill("Arcane Larceny", Properties.Resources.Arcane_Larceny, Skill.Attributes.Domination_Magic, Skill.Professions.Mesmer, 2, "10E 1C - Spell. (5...29...35 seconds.) Disables one random spell. This skill becomes that spell."));

            Data.Add(new Skill("Spoil Victor", Properties.Resources.Spoil_Victor, Skill.Attributes.Blood_Magic, Skill.Professions.Necromancer, 2, "10E 1C 10R - Elite Hex Spell. (3...13...15 seconds.) Causes 25...85...100 Health loss whenever target foe attacks or casts spells on a creature with less Health.", true));
            Data.Add(new Skill("Lifebane Strike", Properties.Resources.Lifebane_Strike, Skill.Attributes.Blood_Magic, Skill.Professions.Necromancer, 2, "10E 2C 15R - Spell. Deals 12...41...48 damage. Steals 12...41...48 Health if target foe's Health is above 50%."));
            Data.Add(new Skill("Bitter Chill", Properties.Resources.Bitter_Chill, Skill.Attributes.Death_Magic, Skill.Professions.Necromancer, 3, "5E 1C 10R - Spell. Deals 15...51...60 cold damage. Recharges instantly if target foe had more Health than you."));
            Data.Add(new Skill("Taste of Pain", Properties.Resources.Taste_of_Pain, Skill.Attributes.Death_Magic, Skill.Professions.Necromancer, 2, "5E 1/4C 10R - Spell. Heals you for 30...126...150. No effect unless target foe is below 50% Health."));
            Data.Add(new Skill("Defile Enchantments", Properties.Resources.Defile_Enchantments, Skill.Attributes.Curses, Skill.Professions.Necromancer, 3, "10E 2C 15R - Spell. Deals 6...49...60 damage to target and nearby foes. Deals 4...17...20 more damage for each enchantment on them."));
            Data.Add(new Skill("Shivers of Dread", Properties.Resources.Shivers_of_Dread, Skill.Attributes.Curses, Skill.Professions.Necromancer, 2, "10E 2C 15R - Hex Spell. (10...34...40 seconds.) Cold damage interrupts target foe's skills. Interruption cost: you lose 10...6...5 Energy or this hex ends."));
            Data.Add(new Skill("Vampiric Swarm", Properties.Resources.Vampiric_Swarm, Skill.Attributes.Blood_Magic, Skill.Professions.Necromancer, 3, "15E 2C 8R - Spell. Steals 15...51...60 Health. Hits 2 additional foes in the area."));
            Data.Add(new Skill("Blood Drinker", Properties.Resources.Blood_Drinker, Skill.Attributes.Blood_Magic, Skill.Professions.Necromancer, 3, "5E 2C 8R - Spell. Steals 20...56...65 Health. You begin Bleeding (10 seconds) if your Health is above 50%."));
            Data.Add(new Skill("Vampiric Bite", Properties.Resources.Vampiric_Bite, Skill.Attributes.Blood_Magic, Skill.Professions.Necromancer, 2, "15E 3/4C 2R - Touch Skill. Steals 29...65...74 Health."));
            Data.Add(new Skill("Wallow's Bite", Properties.Resources.Wallow_s_Bite, Skill.Attributes.Blood_Magic, Skill.Professions.Necromancer, 2, "10%HP 1E 3/4C 3R - Touch Skill. Deals 20...50...58 damage."));
            Data.Add(new Skill("Enfeebling Touch", Properties.Resources.Enfeebling_Touch, Skill.Attributes.Curses, Skill.Professions.Necromancer, 2, "5E 3/4C 5R - Touch Skill. Causes 5...41...50 Health loss. Inflicts Weakness condition (5...17...20 seconds)."));

            Data.Add(new Skill("Teinai's Wind", Properties.Resources.Teinai_s_Wind, Skill.Attributes.Air_Magic, Skill.Professions.Elementalist, 2, "5E 1C 8R - Spell. Deals 10...34...40 cold damage. Burning foes take +40...72...80 damage and are interrupted. Also strikes adjacent."));
            Data.Add(new Skill("Shock Arrow", Properties.Resources.Shock_Arrow, Skill.Attributes.Air_Magic, Skill.Professions.Elementalist, 4, "5E 1C 8R - Spell. Rapid projectile: deals 5...41...50 lightning damage. Gain 5 Energy plus 1 Energy for every 2 ranks of Energy Storage if you hit a foe suffering from Cracked Armor. 25% armor penetration."));
            Data.Add(new Skill("Unsteady Ground", Properties.Resources.Unsteady_Ground, Skill.Attributes.Earth_Magic, Skill.Professions.Elementalist, 4, "10E 2C 15R - Elite Spell. Deals 10...34...40 earth damage each second (5 seconds) and causes knock-down to attacking foes. Hits foes near target's initial location.", true));
            Data.Add(new Skill("Sliver Armor", Properties.Resources.Sliver_Armor, Skill.Attributes.Earth_Magic, Skill.Professions.Elementalist, 2, "10E 1C 30R - Enchantment Spell. (5...10...11 seconds.) You have 25...45...50% chance to block. Deals 5...29...35 earth damage to one nearby foe whenever you are the target of a hostile spell or attack."));
            Data.Add(new Skill("Ash Blast", Properties.Resources.Ash_Blast, Skill.Attributes.Earth_Magic, Skill.Professions.Elementalist, 2, "5E 1C 15R - Hex Spell. Deals 35...59...65 earth damage. Burning foes are hexed for 5 seconds and miss 20...64...75% of attacks. Also strikes adjacent."));
            Data.Add(new Skill("Dragon's Stomp", Properties.Resources.Dragon_s_Stomp, Skill.Attributes.Earth_Magic, Skill.Professions.Elementalist, 3, "10O 25E 3C 15R - Spell. Deals 26...85...100 earth damage. Causes knock-down. Also hits foes near this foe."));
            Data.Add(new Skill("Second Wind", Properties.Resources.Second_Wind, Skill.Attributes.None, Skill.Professions.Elementalist, 3, "5O 5E 1C 5R - Elite Spell. You gain 1 Energy and 5 Health for each point of Overcast. You lose all enchantments.", true));
            Data.Add(new Skill("Smoldering Embers", Properties.Resources.Smoldering_Embers, Skill.Attributes.Fire_Magic, Skill.Professions.Elementalist, 3, "10E 3/2C 7R - Hex Spell. Deals 10...58...70 fire damage to target. If you are Overcast, foe is hexed for 3 seconds, taking 5...21...25 fire damage each second."));
            Data.Add(new Skill("Double Dragon", Properties.Resources.Double_Dragon, Skill.Attributes.Fire_Magic, Skill.Professions.Elementalist, 4, "10E 1C 20R - Elite Enchantment Spell. (8 seconds.) Enchants you and target ally. Adjacent foes take 5...25...30 fire damage each second. Skills that target a foe also inflict Burning (0...2...3 second[s]).", true));
            Data.Add(new Skill("Teinai's Heat", Properties.Resources.Teinai_s_Heat, Skill.Attributes.Fire_Magic, Skill.Professions.Elementalist, 3, "15E 1C 20R - Ward Spell. (10...14...15 seconds.) Causes -2...4...5 health degeneration. Weakened foes in the ward attack 33% slower. Disabled for 20 seconds."));
            Data.Add(new Skill("Breath of Fire", Properties.Resources.Breath_of_Fire, Skill.Attributes.Fire_Magic, Skill.Professions.Elementalist, 4, "10O 5E 2C 10R - Spell. Deals 10...34...40 fire damage each second (5 seconds). Hits foes adjacent to target's initial location."));
            Data.Add(new Skill("Star Burst", Properties.Resources.Star_Burst, Skill.Attributes.Fire_Magic, Skill.Professions.Elementalist, 5, "5E 3/4C 7R - Elite Touch Spell. Deals 7...91...112 fire damage. Inflicts Burning (1...3...4 second[s]). Gain 2 Energy for each foe struck. Also hits foes in the area.", true));
            Data.Add(new Skill("Glyph of Essence", Properties.Resources.Glyph_of_Essence, Skill.Attributes.None, Skill.Professions.Elementalist, 1, "5E 1C 20R - Glyph. (15 seconds.) Your next spell casts instantly. You lose all Energy."));
            Data.Add(new Skill("Teinai's Prison", Properties.Resources.Teinai_s_Prison, Skill.Attributes.Water_Magic, Skill.Professions.Elementalist, 3, "10E 1C 15R - Hex Spell. (1...5...6 second[s].) Target foe moves 66% slower. Foes with Cracked Armor have 5...8...9 Health degeneration."));
            Data.Add(new Skill("Mirror of Ice", Properties.Resources.Mirror_of_Ice, Skill.Attributes.Water_Magic, Skill.Professions.Elementalist, 3, "10E 3/4C 15R - Elite Hex Spell. Deals 15...59...70 cold damage and slows foes by 66% (2...5...6 seconds). Hits foes near you and target ally. Recharges 50% faster if it hits a foe hexed with Water Magic.", true));
            Data.Add(new Skill("Teinai's Crystals", Properties.Resources.Teinai_s_Crystals, Skill.Attributes.Earth_Magic, Skill.Professions.Elementalist, 2, "5E 1C 8R - Spell. Deals 20...36...40 damage to target. Deals +20...36...40 damage and causes Cracked Armor (5...13...15 seconds) nearby if that foe had a Water Magic hex."));

            Data.Add(new Skill("Kirin's Wrath", Properties.Resources.Kirin_s_Wrath, Skill.Attributes.Smiting_Prayers, Skill.Professions.Monk, 2, "5E 2C 30R - Spell. Deals 8...27...32 holy damage each second (5 seconds). Hits foes adjacent to your initial location."));
            Data.Add(new Skill("Spirit Bond", Properties.Resources.Spirit_Bond, Skill.Attributes.Protection_Prayers, Skill.Professions.Monk, 2, "10E 1/4C 2R - Enchantment Spell. (8 seconds.) Heals for 30...78...90 whenever target ally takes more than 50 damage. Ends after this ally takes damage from 10 attacks or spells."));
            Data.Add(new Skill("Air of Enchantment", Properties.Resources.Air_of_Enchantment, Skill.Attributes.Protection_Prayers, Skill.Professions.Monk, 2, "5E 1/4C 8R - Elite Enchantment Spell. (4...9...10 seconds.) Enchantments cast on target ally cost 5 less Energy (minimum 1 Energy). Cannot self-target.", true));
            Data.Add(new Skill("Heaven's Delight", Properties.Resources.Heaven_s_Delight, Skill.Attributes.Divine_Favor, Skill.Professions.Monk, 4, "5E 1C 12R - Spell. Heals you and party members within earshot for 15...51...60 points."));
            Data.Add(new Skill("Healing Burst", Properties.Resources.Healing_Burst, Skill.Attributes.Healing_Prayers, Skill.Professions.Monk, 5, "5E 3/4C 4R - Elite Spell. Heals for 10...130...160. Party members in earshot of your target gain Health equal to the Divine Favor bonus. Disables your Smiting Prayers (20 seconds).", true));
            Data.Add(new Skill("Karei's Healing Circle", Properties.Resources.Karei_s_Healing_Circle, Skill.Attributes.Healing_Prayers, Skill.Professions.Monk, 1, "10E 1C 5R - Spell. Heals you for 30...150...180. Also heals adjacent creatures."));
            Data.Add(new Skill("Jamei's Gaze", Properties.Resources.Jamei_s_Gaze, Skill.Attributes.Healing_Prayers, Skill.Professions.Monk, 1, "10E 3/4C 3R - Spell. Heals for 35...151...180. Cannot self-target."));
            Data.Add(new Skill("Gift of Health", Properties.Resources.Gift_of_Health, Skill.Attributes.Healing_Prayers, Skill.Professions.Monk, 2, "5E 3/4C 5R - Spell. Heals for 15...123...150. Disables your other Healing Prayers skills (10...6...5 seconds). Cannot self-target."));
            Data.Add(new Skill("Life Sheath", Properties.Resources.Life_Sheath, Skill.Attributes.Protection_Prayers, Skill.Professions.Monk, 3, "5E 1/4C 2R - Elite Enchantment Spell. (8 seconds.) Converts the next incoming damage or life steal (maximum 20...84...100) to healing. Initial effect: Removes 0...2...2 condition[s].", true));
            Data.Add(new Skill("Empathic Removal", Properties.Resources.Empathic_Removal, Skill.Attributes.None, Skill.Professions.Monk, 3, "5E 1C 7R - Elite Spell. Removes one condition and hex from target ally and yourself, and heals for 50. Cannot self-target.", true));
            Data.Add(new Skill("Resurrection Chant", Properties.Resources.Resurrection_Chant, Skill.Attributes.Healing_Prayers, Skill.Professions.Monk, 5, "10E 6C 15R - Half Range Spell. Resurrects target party member at your current Health and with 5...29...35% Energy."));
            Data.Add(new Skill("Word of Censure", Properties.Resources.Word_of_Censure, Skill.Attributes.Smiting_Prayers, Skill.Professions.Monk, 2, "10E 1C 2R - Elite Spell. Deals 15...63...75 holy damage. +20 recharge time if target foe is below 33% Health.", true));
            Data.Add(new Skill("Spear of Light", Properties.Resources.Spear_of_Light, Skill.Attributes.Smiting_Prayers, Skill.Professions.Monk, 2, "5E 1C 15R - Spell. Projectile: deals 26...50...56 holy damage. Deals 15...51...60 more damage if target foe is attacking."));
            Data.Add(new Skill("Stonesoul Strike", Properties.Resources.Stonesoul_Strike, Skill.Attributes.Smiting_Prayers, Skill.Professions.Monk, 2, "5E 3/4C 8R - Touch Skill. Deals 10...46...55 holy damage. Deals 10...46...55 more holy damage if target is knocked down."));

            Data.Add(new Skill("Drunken Blow", Properties.Resources.Drunken_Blow, Skill.Attributes.Tactics, Skill.Professions.Warrior, 2, "5E 7R - Melee Attack. Deals +10...34...40 damage. Inflicts one of the following random conditions: Deep Wound (20 seconds), Weakness (20 seconds), Bleeding (25 seconds), or Crippled (15 seconds). You are knocked-down."));
            Data.Add(new Skill("Leviathan's Sweep", Properties.Resources.Leviathan_s_Sweep, Skill.Attributes.Strength, Skill.Professions.Warrior, 3, "5E 8R - Melee Attack. Deals +5...17...20 damage. Causes knock-down and deals 10...29...34 damage if blocked."));
            Data.Add(new Skill("Jaizhenju Strike", Properties.Resources.Jaizhenju_Strike, Skill.Attributes.Swordsmanship, Skill.Professions.Warrior, 3, "5E 8R - Sword Attack. Deals +1...24...30 damage. Unblockable unless you are in a stance."));
            Data.Add(new Skill("Penetrating Chop", Properties.Resources.Penetrating_Chop, Skill.Attributes.Axe_Mastery, Skill.Professions.Warrior, 4, "5A - Axe Attack. Deals +5...17...20 damage. 20% armor penetration."));
            Data.Add(new Skill("Yeti Smash", Properties.Resources.Yeti_Smash, Skill.Attributes.Hammer_Mastery, Skill.Professions.Warrior, 4, "6A - Hammer Attack. Attack all adjacent foes. Knocks down foes suffering from a condition. You lose all adrenaline. 50% failure chance unless Hammer Mastery is 5 or more."));
            Data.Add(new Skill("\"You Will Die!\"", Properties.Resources._You_Will_Die__, Skill.Attributes.Strength, Skill.Professions.Warrior, 3, "5E 15R - Shout. You gain 1...3...3 strike[s] of adrenaline. No effect unless target foe is under 50% Health."));
            Data.Add(new Skill("Auspicious Parry", Properties.Resources.Auspicious_Parry, Skill.Attributes.Tactics, Skill.Professions.Warrior, 2, "1A 2R - Elite Stance. (8 seconds.) Blocks one attack. End effect: you gain 1...3...4 strike[s] of adrenaline.", true));
            Data.Add(new Skill("Silverwing Slash", Properties.Resources.Silverwing_Slash, Skill.Attributes.Swordsmanship, Skill.Professions.Warrior, 3, "8A - Sword Attack. Deals +1...32...40 damage."));
            Data.Add(new Skill("Shove", Properties.Resources.Shove, Skill.Attributes.Tactics, Skill.Professions.Warrior, 2, "5E 3/4C 15R - Elite Touch Skill. Causes knockdown. Initial effect: ends foe's stance and deals 15...63...75 damage if target foe is moving.", true));

            Data.Add(new Skill("Sundering Attack", Properties.Resources.Sundering_Attack, Skill.Attributes.Marksmanship, Skill.Professions.Ranger, 4, "10E 3/4C 4R - Bow Attack. Deals +5...21...25 damage. 10% armor penetration."));
            Data.Add(new Skill("Zojun's Shot", Properties.Resources.Zojun_s_Shot, Skill.Attributes.Expertise, Skill.Professions.Ranger, 3, "5E 3R - Half Range Bow Attack. Deals +10...34...40 damage."));
            Data.Add(new Skill("Predatory Bond", Properties.Resources.Predatory_Bond, Skill.Attributes.Beast_Mastery, Skill.Professions.Ranger, 2, "10E 30R - Shout. (5...17...20 seconds.) Your pet attacks 25% faster and you gain 1...25...31 Health whenever your pet makes a successful attack."));
            Data.Add(new Skill("Heal as One", Properties.Resources.Heal_as_One, Skill.Attributes.Beast_Mastery, Skill.Professions.Ranger, 3, "5E 1C 5R - Elite Skill. (15 seconds.) Your pet's attacks steal 1...16...20 Health. Initial effect: heals you and your pet for 20...87...104; resurrects your pet (50% Health) if dead. If you have this equipped, your pet will travel with you.", true));
            Data.Add(new Skill("Zojun's Haste", Properties.Resources.Zojun_s_Haste, Skill.Attributes.Expertise, Skill.Professions.Ranger, 1, "5E 30R - Stance. (5...10...11 seconds.) You move 33% faster and have 27...65...75% chance to block projectiles. Ends if you attack."));
            Data.Add(new Skill("Needling Shot", Properties.Resources.Needling_Shot, Skill.Attributes.Marksmanship, Skill.Professions.Ranger, 2, "5E 3/4C 4R - Bow Attack. Fast-moving arrow. Deals 10...26...30 damage. Recharges instantly if target foe is below 50% Health. Your other attack skills are disabled (2 seconds)."));
            Data.Add(new Skill("Broad Head Arrow", Properties.Resources.Broad_Head_Arrow, Skill.Attributes.Marksmanship, Skill.Professions.Ranger, 4, "15E 15R - Elite Bow Attack. Slow moving arrow. Interrupts a spell. Inflicts Dazed condition (5...17...20 seconds).", true));
            Data.Add(new Skill("Glass Arrows", Properties.Resources.Glass_Arrows, Skill.Attributes.Expertise, Skill.Professions.Ranger, 4, "5E 2C 12R - Elite Preparation. (10...30...35 seconds.) Your arrows deal +5...17...20 damage. Inflicts Bleeding condition if blocked (10...18...20 seconds).", true));
            Data.Add(new Skill("Archer's Signet", Properties.Resources.Archer_s_Signet, Skill.Attributes.Expertise, Skill.Professions.Ranger, 1, "1C 12R - Elite Signet. (1...19...24 second[s].) Conditions you apply while wielding a bow last 100% longer.", true));
            Data.Add(new Skill("Savage Pounce", Properties.Resources.Savage_Pounce, Skill.Attributes.Beast_Mastery, Skill.Professions.Ranger, 2, "5E 10R - Pet Attack. Deals +5...17...20 damage. Causes knock-down if target foe is casting a spell."));
            Data.Add(new Skill("Enraged Lunge", Properties.Resources.Enraged_Lunge, Skill.Attributes.Beast_Mastery, Skill.Professions.Ranger, 4, "5E 5R - Elite Pet Attack. Inflicts Deep Wound condition (5...17...20 seconds) and deals +10...42...50 damage.", true));
            Data.Add(new Skill("Bestial Mauling", Properties.Resources.Bestial_Mauling, Skill.Attributes.Beast_Mastery, Skill.Professions.Ranger, 1, "5E 20R - Pet Attack. Deals +5...17...20 damage. Inflicts Dazed condition (4...9...10 seconds) if target foe is knocked-down."));
            Data.Add(new Skill("Poisonous Bite", Properties.Resources.Poisonous_Bite, Skill.Attributes.Beast_Mastery, Skill.Professions.Ranger, 1, "5E 7R - Pet Attack. Inflicts Poisoned condition (5...17...20 seconds)."));
            Data.Add(new Skill("Pounce", Properties.Resources.Pounce, Skill.Attributes.Beast_Mastery, Skill.Professions.Ranger, 1, "5E 20R - Pet Attack. Deals +5...17...20 damage. Causes knock-down if target foe is moving."));
            Data.Add(new Skill("Bestial Fury", Properties.Resources.Bestial_Fury, Skill.Attributes.Beast_Mastery, Skill.Professions.Ranger, 1, "10E 10R - Stance. (5...10...11 seconds.) You attack 25% faster. Your non-attack skills are disabled (5 seconds)."));
            Data.Add(new Skill("Viper's Nest", Properties.Resources.Viper_s_Nest, Skill.Attributes.Beast_Mastery, Skill.Professions.Ranger, 2, "10E 2C 20R - Trap. (90 seconds.) Affects nearby foes. Deals 5...29...35 piercing damage. Inflicts Poisoned condition (5...17...20 seconds). Easily interrupted."));
            Data.Add(new Skill("Equinox", Properties.Resources.Equinox, Skill.Attributes.Wilderness_Survival, Skill.Professions.Ranger, 1, "10E 3C 15R - Elite Nature Ritual. Creates a level 1...8...10 spirit (30...126...150 second lifespan). Overcast-causing spells cast within range cause an additional 10 Overcast.", true));
            Data.Add(new Skill("Tranquility", Properties.Resources.Tranquility, Skill.Attributes.Wilderness_Survival, Skill.Professions.Ranger, 2, "15E 5C 60R - Nature Ritual. Creates a level 1...8...10 spirit (15...51...60 second lifespan). Enchantments expire 20...44...50% faster on creatures in range. Does not affect spirits."));

            Data.Add(new Skill("Clamor of Souls", Properties.Resources.Clamor_of_Souls, Skill.Attributes.Channeling_Magic, Skill.Professions.Ritualist, 2, "10E 1C 8R - Elite Spell. Deals 10...54...65 lightning damage to target and nearby foes. You gain 10 Energy if you are within earshot of a spirit or holding a bundle item.", true));
            Data.Add(new Skill("Ritual Lord", Properties.Resources.Ritual_Lord, Skill.Attributes.Spawning_Power, Skill.Professions.Ritualist, 2, "2%HP 45R - Elite Skill. (5...29...35 seconds.) You have +2...4...4 to all Ritualist attributes for your next skill. If that skill is a Binding Ritual, it recharges 10...50...60% faster and Ritual Lord recharges instantly.", true));
            Data.Add(new Skill("Cruel Was Daoshen", Properties.Resources.Cruel_Was_Daoshen, Skill.Attributes.Channeling_Magic, Skill.Professions.Ritualist, 2, "15E 2C 30R - Item Spell. (15...51...60 seconds.) Your Ritualist skills have 10% armor penetration. Drop effect: deals 15...71...85 lightning damage to all nearby foes."));
            Data.Add(new Skill("Protective Was Kaolai", Properties.Resources.Protective_Was_Kaolai, Skill.Attributes.Restoration_Magic, Skill.Professions.Ritualist, 4, "10E 1C 25R - Item Spell. (15...51...60 seconds.) You have +10 armor. Drop effect: all party members are healed for 10...70...85."));
            Data.Add(new Skill("Attuned Was Songkai", Properties.Resources.Attuned_Was_Songkai, Skill.Attributes.Spawning_Power, Skill.Professions.Ritualist, 2, "10E 2C 60R - Elite Item Spell. (45 seconds.) Your spells and binding rituals cost -5...41...50% of the base Energy.", true));
            Data.Add(new Skill("Resilient Was Xiko", Properties.Resources.Resilient_Was_Xiko, Skill.Attributes.Restoration_Magic, Skill.Professions.Ritualist, 2, "5E 1C 10R - Item Spell. (5...17...20 seconds.) You have +3 Health regeneration for each hex or condition on you. Drop effect: you lose 1...3...4 condition[s]."));
            Data.Add(new Skill("Lively Was Naomei", Properties.Resources.Lively_Was_Naomei, Skill.Attributes.Restoration_Magic, Skill.Professions.Ritualist, 2, "15E 6C 20R - Item Spell. (45 seconds.) Drop effect: resurrects party members in the area (15...63...75% Health and zero Energy)."));
            Data.Add(new Skill("Anguished Was Lingwah", Properties.Resources.Anguished_Was_Lingwah, Skill.Attributes.Spawning_Power, Skill.Professions.Ritualist, 1, "5E 2C 30R - Item Spell. (10...50...60 seconds.) Your Ritualist hexes cost 1...4...5 less energy and last 33% longer. Drop effect: all your Ritualist hexes are recharged."));
            Data.Add(new Skill("Draw Spirit", Properties.Resources.Draw_Spirit, Skill.Attributes.None, Skill.Professions.Ritualist, 2, "5E 1/4C 5R - Spell. Teleports target allied spirit to your location."));
            Data.Add(new Skill("Channeled Strike", Properties.Resources.Channeled_Strike, Skill.Attributes.Channeling_Magic, Skill.Professions.Ritualist, 2, "10E 2C 4R - Spell. Deals 5...77...95 lightning damage. Deals 5...29...35 additional lightning damage if you are holding an item."));
            Data.Add(new Skill("Spirit Boon Strike", Properties.Resources.Spirit_Boon_Strike, Skill.Attributes.Channeling_Magic, Skill.Professions.Ritualist, 2, "5E 1C 3R - Spell. Deals 20...56...65 lightning damage. Spirits you control within earshot gain 20...56...65 Health."));
            Data.Add(new Skill("Essence Strike", Properties.Resources.Essence_Strike, Skill.Attributes.Channeling_Magic, Skill.Professions.Ritualist, 4, "5E 1C 8R - Spell. Deals 15...51...60 lightning damage. You gain 1...7...9 Energy if any spirits are within earshot."));
            Data.Add(new Skill("Spirit Siphon", Properties.Resources.Spirit_Siphon, Skill.Attributes.Channeling_Magic, Skill.Professions.Ritualist, 4, "5E 1/4C 3R - Spell. The spirit nearest you loses all Energy. You gain 15...43...50% of that Energy."));
            Data.Add(new Skill("Explosive Growth", Properties.Resources.Explosive_Growth, Skill.Attributes.Spawning_Power, Skill.Professions.Ritualist, 2, "5E 2C 45R - Enchantment Spell. (15...51...60 seconds.) Deals 20...56...65 lightning damage each time you create a creature to 5 foes near that creature."));
            Data.Add(new Skill("Boon of Creation", Properties.Resources.Boon_of_Creation, Skill.Attributes.Spawning_Power, Skill.Professions.Ritualist, 2, "10E 2C 45R - Enchantment Spell. (15...51...60 seconds.) You gain 5...41...50 Health and 1...5...6 Energy whenever you create a creature."));
            Data.Add(new Skill("Spirit Channeling", Properties.Resources.Spirit_Channeling, Skill.Attributes.Spawning_Power, Skill.Professions.Ritualist, 2, "5E 1C 30R - Elite Enchantment Spell. (12 seconds.) You have +1...5...6 Energy regeneration. Initial effect: you gain 3...10...12 Energy if you are within earshot of a spirit.", true));
            Data.Add(new Skill("Armor of Unfeeling", Properties.Resources.Armor_of_Unfeeling, Skill.Attributes.Communing, Skill.Professions.Ritualist, 2, "5E 1C 20R - Skill. (10...30...35 seconds.) Your spirits within earshot take 50% less damage and are immune to critical attacks."));
            Data.Add(new Skill("Soothing Memories", Properties.Resources.Soothing_Memories, Skill.Attributes.Restoration_Magic, Skill.Professions.Ritualist, 3, "5E 3/4C 4R - Spell. Heals for 10...82...100. You gain 3 Energy if you are holding an item."));
            Data.Add(new Skill("Mend Body and Soul", Properties.Resources.Mend_Body_and_Soul, Skill.Attributes.Restoration_Magic, Skill.Professions.Ritualist, 5, "5E 3/4C 3R - Spell. Heals for 20...96...115. Removes one condition for each spirit within earshot."));
            Data.Add(new Skill("Dulled Weapon", Properties.Resources.Dulled_Weapon, Skill.Attributes.Communing, Skill.Professions.Ritualist, 3, "15E 1C 20R - Hex Spell. Also hexes foes adjacent to your target (5...13...15 seconds). Removes ability to land a critical hit. Reduces damage by 1...12...15."));
            Data.Add(new Skill("Binding Chains", Properties.Resources.Binding_Chains, Skill.Attributes.Communing, Skill.Professions.Ritualist, 2, "10E 1C 15R - Hex Spell. Also hexes foes near your target. (3 seconds.) These foes move 90% slower and takes 1...24...30 damage each second while moving."));
            Data.Add(new Skill("Painful Bond", Properties.Resources.Painful_Bond, Skill.Attributes.Channeling_Magic, Skill.Professions.Ritualist, 5, "15E 1C 12R - Hex Spell. Also hexes foes near your target (10...18...20 seconds). Spirits do 8...18...20 more damage against these foes."));
            Data.Add(new Skill("Signet of Creation", Properties.Resources.Signet_of_Creation, Skill.Attributes.Spawning_Power, Skill.Professions.Ritualist, 2, "1C 20R - Signet. Gain 4 Energy (maximum 3...10...12 Energy) for each summoned creature you control within earshot."));
            Data.Add(new Skill("Signet of Spirits", Properties.Resources.Signet_of_Spirits, Skill.Attributes.Channeling_Magic, Skill.Professions.Ritualist, 5, "1C 30R - Elite Signet. Creates 3 level 1...10...12 spirits (60 second lifespan). These spirits deal 5...17...20 damage with attacks.", true));
            Data.Add(new Skill("Soul Twisting", Properties.Resources.Soul_Twisting, Skill.Attributes.Spawning_Power, Skill.Professions.Ritualist, 2, "5E 15R - Elite Skill. (5...37...45 seconds.) Your Binding Rituals cost 15 less Energy (minimum 10) and recharge instantly. Ends after 1...3...3 Binding Ritual[s].", true));
            Data.Add(new Skill("Ghostly Haste", Properties.Resources.Ghostly_Haste, Skill.Attributes.Spawning_Power, Skill.Professions.Ritualist, 3, "10E 1C 20R - Enchantment Spell. (5...17...20 seconds.) Spells you cast recharge 25% faster. No effect unless you are within earshot of a spirit."));
            Data.Add(new Skill("Gaze from Beyond", Properties.Resources.Gaze_from_Beyond, Skill.Attributes.Channeling_Magic, Skill.Professions.Ritualist, 3, "5E 1C 10R - Spell. Deals 20...56...65 lightning damage. Inflicts Blindness condition for 2...5...6 seconds if you are within earshot of a spirit."));
            Data.Add(new Skill("Ancestors' Rage", Properties.Resources.Ancestors__Rage, Skill.Attributes.Channeling_Magic, Skill.Professions.Ritualist, 3, "5E 1/4C 10R - Skill. (1 second.) End Effect: deals 5...89...110 lightning damage to foes adjacent to target ally."));
            Data.Add(new Skill("Pain", Properties.Resources.Pain, Skill.Attributes.Communing, Skill.Professions.Ritualist, 4, "5E 3/4C 30R - Binding Ritual. Creates a level 1...10...12 spirit (30...126...150 second lifespan). Its attacks deal 5...25...30 damage."));
            Data.Add(new Skill("Displacement", Properties.Resources.Displacement, Skill.Attributes.Communing, Skill.Professions.Ritualist, 2, "15E 3/4C 45R - Binding Ritual. Creates a level 1...11...14 spirit (30...54...60 second lifespan). Non-spirit allies within range have 75% chance to block. Block effect: this spirit takes 60 damage."));
            Data.Add(new Skill("Preservation", Properties.Resources.Preservation, Skill.Attributes.Restoration_Magic, Skill.Professions.Ritualist, 2, "5E 3/4C 20R - Elite Binding Ritual. Creates a level 1...11...14 spirit (90 second lifespan). Every 4 seconds this spirit heals one non-spirit ally for 10...94...115.", true));
            Data.Add(new Skill("Life", Properties.Resources.Life, Skill.Attributes.Restoration_Magic, Skill.Professions.Ritualist, 2, "10E 3/4C 20R - Binding Ritual. Creates a level 1...11...14 spirit (20 second lifespan). Affects non-spirit allies within range. End effect: heals for 1...6...7 for each second this spirit was alive."));
            Data.Add(new Skill("Earthbind", Properties.Resources.Earthbind, Skill.Attributes.Communing, Skill.Professions.Ritualist, 2, "15E 1C 30R - Binding Ritual. Creates a level 1...11...14 spirit (15...39...45 second lifespan). Any time non-spirit foes within range are knocked down, they are knocked down for at least 3 seconds. Knock-down cost: this spirit loses 50...30...25 Health."));
            Data.Add(new Skill("Bloodsong", Properties.Resources.Bloodsong, Skill.Attributes.Channeling_Magic, Skill.Professions.Ritualist, 4, "5E 3/4C 30R - Binding Ritual. Creates a level 1...10...12 spirit (30...126...150 second lifespan). Its attacks steal 5...21...25 Health."));
            Data.Add(new Skill("Wanderlust", Properties.Resources.Wanderlust, Skill.Attributes.Communing, Skill.Professions.Ritualist, 2, "10E 1C 45R - Elite Binding Ritual. Creates a level 1...10...12 spirit (30...54...60 second lifespan). Its attacks against stationary foes cause knock-down. Knock-down cost: this spirit loses 70...54...50 Health.", true));
            Data.Add(new Skill("Spirit Light Weapon", Properties.Resources.Spirit_Light_Weapon, Skill.Attributes.Restoration_Magic, Skill.Professions.Ritualist, 3, "5E 1C 5R - Elite Weapon Spell. (10 seconds.) Target ally gains 1...12...15 Health each second. 1...12...15 more healing per second while within earshot of a spirit.", true));
            Data.Add(new Skill("Brutal Weapon", Properties.Resources.Brutal_Weapon, Skill.Attributes.Communing, Skill.Professions.Ritualist, 2, "10E 1C 15R - Weapon Spell. (10...34...40 seconds.) Attacks deal +5...13...15 damage. No effect while target ally is enchanted."));
            Data.Add(new Skill("Guided Weapon", Properties.Resources.Guided_Weapon, Skill.Attributes.Communing, Skill.Professions.Ritualist, 3, "15E 2C 5R - Weapon Spell. (4...9...10 seconds.) Attacks are unblockable."));

            Data.Add(new Skill("Meekness", Properties.Resources.Meekness, Skill.Attributes.Curses, Skill.Professions.Necromancer, 3, "17%HP 15E 2C 15R - Hex Spell. Also hexes foes in the area of target (5...25...30 seconds). These foes attack 50% slower."));
            Data.Add(new Skill("Frigid Armor", Properties.Resources.Frigid_Armor, Skill.Attributes.Water_Magic, Skill.Professions.Elementalist, 2, "5E 1C 20R - Enchantment Spell. (10...22...25 seconds.) You have +10...34...40 armor against physical damage and immunity to Burning."));
            Data.Add(new Skill("Healing Ring", Properties.Resources.Healing_Ring, Skill.Attributes.Healing_Prayers, Skill.Professions.Monk, 1, "5E 1C 10R - Spell. Heals adjacent allies and foes for 30...150...180. The caster is not healed."));
            Data.Add(new Skill("Renew Life", Properties.Resources.Renew_Life, Skill.Attributes.Healing_Prayers, Skill.Professions.Monk, 2, "15E 4C 5R - Touch Spell. Resurrects target party member (50% Health and 5...17...20% Energy). Heals allies within earshot for 55...115...130."));
            Data.Add(new Skill("Doom", Properties.Resources.Doom, Skill.Attributes.Spawning_Power, Skill.Professions.Ritualist, 2, "10E 1C 8R - Spell. Deals 10...34...40 lightning damage (maximum 135) for each of your recharging binding rituals."));
            Data.Add(new Skill("Wielder's Boon", Properties.Resources.Wielder_s_Boon, Skill.Attributes.Restoration_Magic, Skill.Professions.Ritualist, 3, "5E 1/4C 4R - Spell. Heals for 15...51...60. Heals for 15...63...75 more if this ally is under a Weapon spell."));
            Data.Add(new Skill("Soothing", Properties.Resources.Soothing, Skill.Attributes.Communing, Skill.Professions.Ritualist, 2, "15E 1C 45R - Binding Ritual. Creates a level 1...10...12 spirit (15...39...45 second lifespan). Building adrenaline takes twice as long for foes within range."));
            Data.Add(new Skill("Vital Weapon", Properties.Resources.Vital_Weapon, Skill.Attributes.Communing, Skill.Professions.Ritualist, 2, "5E 1C 2R - Weapon Spell. (5...25...30 seconds.) +40...148...175 maximum Health."));
            Data.Add(new Skill("Weapon of Quickening", Properties.Resources.Weapon_of_Quickening, Skill.Attributes.Communing, Skill.Professions.Ritualist, 3, "10E 2C 5R - Elite Weapon Spell. (5...21...25 seconds.) Spells and binding rituals recharge 33% faster.", true));
            Data.Add(new Skill("Signet of Rage", Properties.Resources.Signet_of_Rage, Skill.Attributes.Smiting_Prayers, Skill.Professions.Monk, 2, "1C 20R - Signet. Deals 5...41...50 holy damage. Deals 5...9...10 more holy damage for each of target foe's adrenaline skills."));

            // Maybe a divide between factions and Nightfall? Anyway, I want some divisions here. Gives me a sense of progress.
            Data.Add(new Skill("Extend Conditions", Properties.Resources.Extend_Conditions, Skill.Attributes.Inspiration_Magic, Skill.Professions.Mesmer, 2, "5E 1/4C 5R - Elite Spell. Spread all conditions from target foe to foes near your target. The durations of those conditions are increased by 5...81...100% (maximum 30 seconds).", true));
            Data.Add(new Skill("Hypochondria", Properties.Resources.Hypochondria, Skill.Attributes.None, Skill.Professions.Mesmer, 2, "5E 1C 7R - Spell. Transfer all conditions from foes in the area to target foe."));
            Data.Add(new Skill("Wastrel's Demise", Properties.Resources.Wastrel_s_Demise, Skill.Attributes.Domination_Magic, Skill.Professions.Mesmer, 3, "5E 1/4C 3R - Hex Spell. (5 seconds.) Each second while hexed, target foe and all foes adjacent to that foe take 1...8...10 damage. Foes take +1...8...10 damage each second this hex is in effect. Ends early if target foe uses a skill."));
            Data.Add(new Skill("Spiritual Pain", Properties.Resources.Spiritual_Pain, Skill.Attributes.Domination_Magic, Skill.Professions.Mesmer, 4, "5E 1C 7R - Spell. Deals 15...63...75 damage. Deals 25...105...125 damage to hostile summoned creatures in the area of your target foe."));
            Data.Add(new Skill("Drain Delusions", Properties.Resources.Drain_Delusions, Skill.Attributes.Inspiration_Magic, Skill.Professions.Mesmer, 2, "5E 1/4C 12R - Spell. Removes one Mesmer hex from target foe. Causes 1...4...5 Energy loss. You gain 4 Energy for each point lost. No effect unless a hex was removed."));
            Data.Add(new Skill("Persistence of Memory", Properties.Resources.Persistence_of_Memory, Skill.Attributes.Fast_Casting, Skill.Professions.Mesmer, 1, "10E 1C 20R - Enchantment Spell. (5...17...20 seconds.) Your interrupted spells recharge instantly."));
            Data.Add(new Skill("Symbols of Inspiration", Properties.Resources.Symbols_of_Inspiration, Skill.Attributes.Fast_Casting, Skill.Professions.Mesmer, 2, "5E 1C 15R - Elite Skill. (1...25...31 seconds.) This skill becomes the Elite of target foe. Elite spells you cast use your Fast Casting attribute instead of their normal attributes.", true));
            Data.Add(new Skill("Symbolic Celerity", Properties.Resources.Symbolic_Celerity, Skill.Attributes.Fast_Casting, Skill.Professions.Mesmer, 2, "15E 1C 30R - Enchantment Spell. (36...55...60 seconds.) Your signets use your Fast Casting attribute."));
            Data.Add(new Skill("Frustration", Properties.Resources.Frustration, Skill.Attributes.Illusion_Magic, Skill.Professions.Mesmer, 2, "10E 1C 7R - Hex Spell. (5...17...20 seconds.) Causes 50% slower spell casting. Target foe takes 5...41...50 damage whenever interrupted. Deals double damage on skill interrupt."));
            Data.Add(new Skill("Tease", Properties.Resources.Tease, Skill.Attributes.Inspiration_Magic, Skill.Professions.Mesmer, 4, "5E 1/4C 15R - Elite Spell. Interrupts a skill. Interruption effect: also interrupts other foes in the area, and you steal 0...4...5 Energy from all foes in the area.", true));
            Data.Add(new Skill("Ether Phantom", Properties.Resources.Ether_Phantom, Skill.Attributes.Inspiration_Magic, Skill.Professions.Mesmer, 2, "5E 1C 10R - Hex Spell. (10 seconds.) Causes -1 Energy degeneration. Causes 1...4...5 Energy loss if this hex ends early."));
            Data.Add(new Skill("Web of Disruption", Properties.Resources.Web_of_Disruption, Skill.Attributes.None, Skill.Professions.Mesmer, 4, "5E 1/4C 15R - Hex Spell. (10 seconds.) Initial effect: interrupts a skill. End effect: interrupts a skill."));
            Data.Add(new Skill("Enchanter's Conundrum", Properties.Resources.Enchanter_s_Conundrum, Skill.Attributes.Domination_Magic, Skill.Professions.Mesmer, 3, "10E 2C 10R - Elite Hex Spell. Causes 100...180...200% slower enchantment casting (10 seconds). Initial effect: deals 10...82...100 damage to target and adjacent foes if target foe is not enchanted.", true));
            Data.Add(new Skill("Signet of Illusions", Properties.Resources.Signet_of_Illusions, Skill.Attributes.Illusion_Magic, Skill.Professions.Mesmer, 2, "2C 5R - Elite Signet. Your next 1...3...3 spell[s] use your Illusion attribute instead of its normal attribute.", true));
            Data.Add(new Skill("Discharge Enchantment", Properties.Resources.Discharge_Enchantment, Skill.Attributes.Inspiration_Magic, Skill.Professions.Mesmer, 3, "10E 1C 15R - Spell. Removes one enchantment from target foe. 20...44...50% faster recharge if that foe was hexed."));
            Data.Add(new Skill("Hex Eater Vortex", Properties.Resources.Hex_Eater_Vortex, Skill.Attributes.Domination_Magic, Skill.Professions.Mesmer, 2, "10E 1C 15R - Elite Spell. Removes one hex from target ally. Removal effect: deals 30...78...90 damage and removes one enchantment from foes near this ally.", true));
            Data.Add(new Skill("Mirror of Disenchantment", Properties.Resources.Mirror_of_Disenchantment, Skill.Attributes.None, Skill.Professions.Mesmer, 4, "5E 1C 15R - Spell. Removes one enchantment from target foe. That foe's party members also lose this enchantment."));
            Data.Add(new Skill("Simple Thievery", Properties.Resources.Simple_Thievery, Skill.Attributes.Domination_Magic, Skill.Professions.Mesmer, 1, "10E 1/4C 10R - Elite Spell. Interrupts an action. Interruption effect: If a skill was interrupted, that skill is disabled and Simple Thievery becomes that skill (5...17...20 seconds).", true));

            Data.Add(new Skill("Animate Shambling Horror", Properties.Resources.Animate_Shambling_Horror, Skill.Attributes.Death_Magic, Skill.Professions.Necromancer, 5, "15E 3C 25R - Spell. Creates a level 1...14...17 shambling horror. When the shambling horror dies, it is replaced by a level 0...12...15 jagged horror that causes Bleeding with each of its attacks. Exploits a fresh corpse."));
            Data.Add(new Skill("Order of Undeath", Properties.Resources.Order_of_Undeath, Skill.Attributes.Death_Magic, Skill.Professions.Necromancer, 2, "10E 1C 5R - Elite Spell. (5 seconds.) Your undead servants deal +3...13...16 damage. You lose 2% of your maximum Health whenever your servants hit.", true));
            Data.Add(new Skill("Putrid Flesh", Properties.Resources.Putrid_Flesh, Skill.Attributes.Death_Magic, Skill.Professions.Necromancer, 2, "10E 1/4C - Spell. Destroys one of your undead servants. Inflicts Diseased condition (5...13...15 seconds) to foes near this servant."));
            Data.Add(new Skill("Feast for the Dead", Properties.Resources.Feast_for_the_Dead, Skill.Attributes.Death_Magic, Skill.Professions.Necromancer, 2, "5E 1/4C 10R - Spell. Destroys one of your undead servants. Heals your servants for 10...82...100."));
            Data.Add(new Skill("Jagged Bones", Properties.Resources.Jagged_Bones, Skill.Attributes.Death_Magic, Skill.Professions.Necromancer, 2, "5E 1C 15R - Elite Enchantment Spell. (30 seconds.) When target undead servant dies, it is replaced by a level 0...12...15 jagged horror that inflicts Bleeding with attacks.", true));
            Data.Add(new Skill("Contagion", Properties.Resources.Contagion, Skill.Attributes.Death_Magic, Skill.Professions.Necromancer, 2, "5E 1C 20R - Elite Enchantment Spell. (60 seconds.) Whenever you gain a condition, all foes in the area gain that same condition. You sacrifice 10...6...5% maximum Health each time this happens.", true));
            Data.Add(new Skill("Ulcerous Lungs", Properties.Resources.Ulcerous_Lungs, Skill.Attributes.Curses, Skill.Professions.Necromancer, 2, "15E 2C 10R - Hex Spell. Also hexes foes near your target (10...22...25 seconds). Causes -4 Health degeneration to any of these foes that are Bleeding. Inflicts Bleeding (3...13...15 seconds) whenever these foes use a shout or chant."));
            Data.Add(new Skill("Pain of Disenchantment", Properties.Resources.Pain_of_Disenchantment, Skill.Attributes.Curses, Skill.Professions.Necromancer, 3, "10E 1C 15R - Elite Spell. Target foe loses 1...3...3 enchantment[s]. Removal effect: that foe and all adjacent foes lose 10...82...100 Health.", true));
            Data.Add(new Skill("Mark of Fury", Properties.Resources.Mark_of_Fury, Skill.Attributes.Blood_Magic, Skill.Professions.Necromancer, 2, "5E 3/4C 10R - Hex Spell. (5 seconds.) Allies hitting target foe gain 0...2...2 strike[s] of adrenaline. End effect: inflicts Cracked Armor (1...12...15 second[s].)"));
            Data.Add(new Skill("Corrupt Enchantment", Properties.Resources.Corrupt_Enchantment, Skill.Attributes.Curses, Skill.Professions.Necromancer, 2, "5E 3/4C 10R - Elite Hex Spell. Removes one enchantment from target foe. Removal effect: -1...7...8 Health degeneration (10 seconds).", true));
            Data.Add(new Skill("Signet of Sorrow", Properties.Resources.Signet_of_Sorrow, Skill.Attributes.Soul_Reaping, Skill.Professions.Necromancer, 3, "1C 30R - Signet. Deals 15...63...75 damage. Recharges instantly if target foe is near a corpse or has a dead pet."));
            Data.Add(new Skill("Signet of Suffering", Properties.Resources.Signet_of_Suffering, Skill.Attributes.Blood_Magic, Skill.Professions.Necromancer, 1, "4R - Elite Signet. You Bleed for 6 seconds. Applies Bleeding (2...13...16 seconds) to the target of your next Necromancer skill.", true));
            Data.Add(new Skill("Signet of Lost Souls", Properties.Resources.Signet_of_Lost_Souls, Skill.Attributes.Soul_Reaping, Skill.Professions.Necromancer, 4, "1/4C 8R - Signet. You gain 10...82...100 Health and 1...8...10 Energy if target foe is below 50% Health."));
            Data.Add(new Skill("Well of Darkness", Properties.Resources.Well_of_Darkness, Skill.Attributes.Curses, Skill.Professions.Necromancer, 3, "10E 1C 20R - Well Spell. (5...41...50 seconds.) Hexed foes in this well have 50% chance to miss. Exploits a fresh corpse."));

            Data.Add(new Skill("Blinding Surge", Properties.Resources.Blinding_Surge, Skill.Attributes.Air_Magic, Skill.Professions.Elementalist, 4, "10E 3/4C 6R - Elite Spell. Deals 5...41...50 lightning damage. Inflicts Blindness condition (3...7...8 seconds) on target and adjacent foes. 25% armor penetration. If target was attacking, also hits adjacent foes and deals 50% more damage.", true));
            Data.Add(new Skill("Chilling Winds", Properties.Resources.Chilling_Winds, Skill.Attributes.Air_Magic, Skill.Professions.Elementalist, 2, "5E 1C 8R - Hex Spell. (10 seconds.) The next water hex on target foe lasts 25...85...100% longer. Initial effect: 30...54...60 cold damage. Also strikes adjacent."));
            Data.Add(new Skill("Lightning Bolt", Properties.Resources.Lightning_Bolt, Skill.Attributes.Air_Magic, Skill.Professions.Elementalist, 3, "5E 1C 8R - Spell. Projectile: deals 5...41...50 lightning damage. Deals 5...41...50 more lightning damage if target foe is moving. 25% armor penetration."));
            Data.Add(new Skill("Storm Djinn's Haste", Properties.Resources.Storm_Djinn_s_Haste, Skill.Attributes.Air_Magic, Skill.Professions.Elementalist, 2, "5E 1/4C 10R - Enchantment Spell. (10...22...25 seconds.) You move 25% faster. Lose 1 Energy each second while moving."));
            Data.Add(new Skill("Stone Striker", Properties.Resources.Stone_Striker, Skill.Attributes.Earth_Magic, Skill.Professions.Elementalist, 2, "5E 1/4C 20R - Enchantment Spell. (5...25...30 seconds.) Converts elemental and physical damage you take or deal to earth damage."));
            Data.Add(new Skill("Sandstorm", Properties.Resources.Sandstorm, Skill.Attributes.Earth_Magic, Skill.Professions.Elementalist, 4, "15E 2C 25R - Elite Spell. Deals 10...26...30 earth damage each second (10 seconds). Hits foes near target foe's initial location. Hits attacking foes for 10...26...30 more earth damage each second.", true));
            Data.Add(new Skill("Stone Sheath", Properties.Resources.Stone_Sheath, Skill.Attributes.Earth_Magic, Skill.Professions.Elementalist, 4, "10E 1C 15R - Elite Enchantment Spell. (5...17...20 seconds.) Gives you and target ally +1...24...30 armor and immunity to critical hits. Initial effect: Foes near you and target ally are struck for 15...59...70 earth damage and are Weakened (5...17...20 seconds).", true));
            Data.Add(new Skill("Ebon Hawk", Properties.Resources.Ebon_Hawk, Skill.Attributes.Earth_Magic, Skill.Professions.Elementalist, 3, "10E 1C 5R - Spell. Projectile: deals 10...70...85 earth damage and inflicts Weakness condition (5...13...15 seconds)."));
            Data.Add(new Skill("Stoneflesh Aura", Properties.Resources.Stoneflesh_Aura, Skill.Attributes.Earth_Magic, Skill.Professions.Elementalist, 2, "10E 2C 15R - Enchantment Spell. (5...13...15 seconds.) Reduces damage you take by 1...25...31, and you are immune to critical hits."));
            Data.Add(new Skill("Glyph of Restoration", Properties.Resources.Glyph_of_Restoration, Skill.Attributes.Energy_Storage, Skill.Professions.Elementalist, 2, "5E 1C 8R - Glyph. (15 seconds.) Your next 2 spells heal you for 30...90...105, and you are healed for 150...350...400% of the Energy cost of each spell."));
            Data.Add(new Skill("Ether Prism", Properties.Resources.Ether_Prism, Skill.Attributes.Energy_Storage, Skill.Professions.Elementalist, 2, "5E 25R - Elite Skill. (3 seconds.) All damage you take is reduced by 75%. End effect: gain 5...17...20 Energy.", true));
            Data.Add(new Skill("Master of Magic", Properties.Resources.Master_of_Magic, Skill.Attributes.Energy_Storage, Skill.Professions.Elementalist, 2, "5E 1C 10R - Elite Enchantment Spell. (1...49...61 second[s]). Your elemental attributes are set to 8...13...14 and elemental spells return 30% of their Energy cost.", true));
            Data.Add(new Skill("Glowing Gaze", Properties.Resources.Glowing_Gaze, Skill.Attributes.Fire_Magic, Skill.Professions.Elementalist, 5, "5E 1C 8R - Spell. Deals 5...41...50 fire damage. You gain 5 Energy plus 1 Energy for every 2 ranks of Energy Storage if target foe is Burning."));
            Data.Add(new Skill("Savannah Heat", Properties.Resources.Savannah_Heat, Skill.Attributes.Fire_Magic, Skill.Professions.Elementalist, 4, "5E 2C 25R - Elite Spell. Deals 5...17...20 fire damage for each second since casting this spell (5 seconds). Hits foes near target's initial location.", true));
            Data.Add(new Skill("Flame Djinn's Haste", Properties.Resources.Flame_Djinn_s_Haste, Skill.Attributes.Fire_Magic, Skill.Professions.Elementalist, 4, "10E 3/4C 20R - Enchantment Spell. (8...13...14 seconds.) You move 25% faster. Initial effect: deals 15...99...120 fire damage to foes adjacent to you. If you damage a foe with this spell, it recharges 50% faster."));
            Data.Add(new Skill("Freezing Gust", Properties.Resources.Freezing_Gust, Skill.Attributes.Water_Magic, Skill.Professions.Elementalist, 1, "10E 1C 8R - Hex Spell. Deals 20...68...80 cold damage if target foe is hexed with Water Magic. Otherwise, this foe moves 66% slower (1...4...5 second[s])."));

            Data.Add(new Skill("Judge's Intervention", Properties.Resources.Judge_s_Intervention, Skill.Attributes.Smiting_Prayers, Skill.Professions.Monk, 1, "5E 1/4C 8R - Enchantment Spell. (10 seconds.) Negates the next fatal damage. Negation effect: deals 30...150...180 holy damage to one foe near target ally."));
            Data.Add(new Skill("Supportive Spirit", Properties.Resources.Supportive_Spirit, Skill.Attributes.Healing_Prayers, Skill.Professions.Monk, 1, "10E 3/4C 8R - Enchantment Spell. (5...19...23 seconds.) Heals for 5...29...35 whenever target ally takes damage while knocked-down."));
            Data.Add(new Skill("Watchful Healing", Properties.Resources.Watchful_Healing, Skill.Attributes.Divine_Favor, Skill.Professions.Monk, 2, "5E 1C 10R - Enchantment Spell. (10 seconds.) Target ally has +1...3...4 Health regeneration and gains 30...102...120 Health if this enchantment ends early."));
            Data.Add(new Skill("Healer's Boon", Properties.Resources.Healer_s_Boon, Skill.Attributes.Divine_Favor, Skill.Professions.Monk, 2, "5E 1/4C 10R - Elite Enchantment Spell. (10...46...55 seconds.) Healing Prayers spells cast 50% faster and heal for 50% more.", true));
            Data.Add(new Skill("Healer's Covenant", Properties.Resources.Healer_s_Covenant, Skill.Attributes.Healing_Prayers, Skill.Professions.Monk, 2, "-1ER 5E 1/4C 5R - Elite Enchantment Spell. Your Healing Prayers spells cost 1...3...3 less Energy. These spells heal for 25% less.", true));
            Data.Add(new Skill("Balthazar's Pendulum", Properties.Resources.Balthazar_s_Pendulum, Skill.Attributes.Smiting_Prayers, Skill.Professions.Monk, 1, "5E 1/4C 5R - Elite Enchantment Spell. (5...9...10 seconds.) Causes knock-down to the next foe attempting to knock-down target ally.", true));
            Data.Add(new Skill("Words of Comfort", Properties.Resources.Words_of_Comfort, Skill.Attributes.Healing_Prayers, Skill.Professions.Monk, 3, "5E 1C 4R - Spell. Heals for 15...51...60. Heals for 15...39...45 more if target ally has a condition."));
            Data.Add(new Skill("Light of Deliverance", Properties.Resources.Light_of_Deliverance, Skill.Attributes.Healing_Prayers, Skill.Professions.Monk, 4, "5E 1C 6R - Elite Spell. Heals entire party for 5...57...70.", true));
            Data.Add(new Skill("Scourge Enchantment", Properties.Resources.Scourge_Enchantment, Skill.Attributes.Smiting_Prayers, Skill.Professions.Monk, 2, "10E 2C 5R - Hex Spell. (30 seconds.) Deals 15...63...75 damage to anyone casting an enchantment on target foe."));
            Data.Add(new Skill("Shield of Absorption", Properties.Resources.Shield_of_Absorption, Skill.Attributes.Protection_Prayers, Skill.Professions.Monk, 5, "5E 1C 10R - Enchantment Spell. (3...6...7 seconds.) Reduces incoming damage by 5 each time target ally takes damage."));
            Data.Add(new Skill("Reversal of Damage", Properties.Resources.Reversal_of_Damage, Skill.Attributes.Smiting_Prayers, Skill.Professions.Monk, 4, "5E 1/4C 3R - Enchantment Spell. (8 seconds.) Negates the next damage and hits the source for that same amount (maximum 5...61...75)."));
            Data.Add(new Skill("Mending Touch", Properties.Resources.Mending_Touch, Skill.Attributes.Protection_Prayers, Skill.Professions.Monk, 2, "5E 3/4C 6R - Touch Spell. Removes two conditions. Heals for 15...51...60 for each condition removed."));

            Data.Add(new Skill("Critical Chop", Properties.Resources.Critical_Chop, Skill.Attributes.Axe_Mastery, Skill.Professions.Warrior, 2, "5E 1C 15R - Axe Attack. Deals +5...17...20 damage. Interrupts an action if you land a critical hit."));
            Data.Add(new Skill("Agonizing Chop", Properties.Resources.Agonizing_Chop, Skill.Attributes.Axe_Mastery, Skill.Professions.Warrior, 2, "6A 1C - Axe Attack. Deals +5...17...20 damage. Interrupts an action if target foe has a Deep Wound."));
            Data.Add(new Skill("Flail", Properties.Resources.Flail, Skill.Attributes.Strength, Skill.Professions.Warrior, 4, "4A - Stance. (1...12...15 second[s].) You attack 33% faster. You move 33% slower."));
            Data.Add(new Skill("Charging Strike", Properties.Resources.Charging_Strike, Skill.Attributes.Strength, Skill.Professions.Warrior, 3, "5E 5R - Elite Stance. (1...8...10 second[s].) You move 33% faster and deal +10...34...40 damage with your next melee hit. Ends when you hit or if you use a skill.", true));
            Data.Add(new Skill("Headbutt", Properties.Resources.Headbutt, Skill.Attributes.Strength, Skill.Professions.Warrior, 2, "15E 3/4C 20R - Elite Touch Skill. Deals 40...88...100 damage. You are Dazed (5...17...20 seconds).", true));
            Data.Add(new Skill("Lion's Comfort", Properties.Resources.Lion_s_Comfort, Skill.Attributes.Strength, Skill.Professions.Warrior, 4, "4A 1C 1R - Skill. You are healed for 50...98...110 and gain 0...2...3 strike[s] of adrenaline. Disables your signets (12 seconds)."));
            Data.Add(new Skill("Rage of the Ntouka", Properties.Resources.Rage_of_the_Ntouka, Skill.Attributes.Strength, Skill.Professions.Warrior, 2, "5E 15R - Elite Skill. You gain 1...6...7 adrenaline. For 10 seconds, adrenal skills have a 5 second recharge when used.", true));
            Data.Add(new Skill("Mokele Smash", Properties.Resources.Mokele_Smash, Skill.Attributes.Hammer_Mastery, Skill.Professions.Warrior, 4, "5E 12R - Hammer Attack. Deals +5...17...20 damage and you gain 2 strikes of adrenaline."));
            Data.Add(new Skill("Overbearing Smash", Properties.Resources.Overbearing_Smash, Skill.Attributes.Hammer_Mastery, Skill.Professions.Warrior, 1, "5E 20R - Hammer Attack. Deals +1...16...20 damage. If target foe is knocked down, they are Dazed (1...7...8 second[s])."));
            Data.Add(new Skill("Signet of Stamina", Properties.Resources.Signet_of_Stamina, Skill.Attributes.Strength, Skill.Professions.Warrior, 1, "1/4C 20R - Signet. You have +50...250...300 maximum Health. Ends if you hit with an attack."));
            Data.Add(new Skill("\"You're All Alone!\"", Properties.Resources._You_re_All_Alone__, Skill.Attributes.None, Skill.Professions.Warrior, 3, "5E 10R - Elite Shout. Inflicts Cripple and Weakness conditions (8 seconds). No effect if target foe is near one of its allies.", true));
            Data.Add(new Skill("Burst of Aggression", Properties.Resources.Burst_of_Aggression, Skill.Attributes.Strength, Skill.Professions.Warrior, 2, "5E 12R - Stance. (2...8...10 seconds.) You attack 33% faster. End effect: lose all adrenaline."));
            Data.Add(new Skill("Enraging Charge", Properties.Resources.Enraging_Charge, Skill.Attributes.Strength, Skill.Professions.Warrior, 3, "5E 20R - Stance. (5...13...15 seconds.) You move 25% faster. Gain +0...2...3 adrenaline on your next melee attack. Ends after your next hit."));
            Data.Add(new Skill("Crippling Slash", Properties.Resources.Crippling_Slash, Skill.Attributes.Swordsmanship, Skill.Professions.Warrior, 3, "6A - Elite Sword Attack. Inflicts Crippled condition (5...13...15 seconds) and Bleeding condition (10...22...25 seconds).", true));
            Data.Add(new Skill("Barbarous Slice", Properties.Resources.Barbarous_Slice, Skill.Attributes.Swordsmanship, Skill.Professions.Warrior, 3, "6A - Sword Attack. Deals +5...25...30 damage. Inflicts Bleeding condition (5...13...15 seconds) if you are not in a stance."));

            Data.Add(new Skill("Prepared Shot", Properties.Resources.Prepared_Shot, Skill.Attributes.Marksmanship, Skill.Professions.Ranger, 3, "5E 6R - Elite Bow Attack. Deals +10...22...25 damage. You gain 1...7...9 Energy if you have a preparation active.", true));
            Data.Add(new Skill("Burning Arrow", Properties.Resources.Burning_Arrow, Skill.Attributes.Marksmanship, Skill.Professions.Ranger, 3, "10E 5R - Elite Bow Attack. Deals +10...26...30 damage. Inflicts Burning condition (1...6...7 seconds).", true));
            Data.Add(new Skill("Arcing Shot", Properties.Resources.Arcing_Shot, Skill.Attributes.Marksmanship, Skill.Professions.Ranger, 3, "5E 6R - Bow Attack. Deals +10...22...25 damage. Unblockable. This arrow moves 50% slower."));
            Data.Add(new Skill("Strike as One", Properties.Resources.Strike_as_One, Skill.Attributes.Beast_Mastery, Skill.Professions.Ranger, 3, "5E 10R - Elite Shout. Your animal companion instantly moves to your target and inflicts Bleeding (5...13...15 seconds) with its next attack. The next time you hit with an attack, you inflict Crippled condition (5...13...15 seconds).", true));
            Data.Add(new Skill("Crossfire", Properties.Resources.Crossfire, Skill.Attributes.Marksmanship, Skill.Professions.Ranger, 4, "5E 4R - Bow Attack. Deals +5...17...20 damage. Unblockable if target foe is near any of your allies."));
            Data.Add(new Skill("Barbed Arrows", Properties.Resources.Barbed_Arrows, Skill.Attributes.Wilderness_Survival, Skill.Professions.Ranger, 2, "10E 2C 12R - Preparation. (24 seconds.) Your arrows inflict Bleeding condition (3...13...15 seconds). You have -40 armor while activating this skill."));
            Data.Add(new Skill("Scavenger's Focus", Properties.Resources.Scavenger_s_Focus, Skill.Attributes.Wilderness_Survival, Skill.Professions.Ranger, 2, "5E 20R - Elite Skill. (10 seconds). You gain 3...10...12 Energy if you strike a foe who has a condition.", true));
            Data.Add(new Skill("Toxicity", Properties.Resources.Toxicity, Skill.Attributes.Beast_Mastery, Skill.Professions.Ranger, 2, "15E 5C 60R - Nature Ritual. Creates a level 1...8...10 spirit (30...78...90 second lifespan.) Poisoned or Diseased creatures within range have -2 Health degeneration."));
            Data.Add(new Skill("Quicksand", Properties.Resources.Quicksand, Skill.Attributes.Wilderness_Survival, Skill.Professions.Ranger, 2, "10E 5C 30R - Elite Nature Ritual. Creates a level 1...8...10 spirit (30...78...90 second lifespan). Creatures lose 1 Energy each time they attack or use a skill. Does not affect spirits.", true));
            Data.Add(new Skill("Storm's Embrace", Properties.Resources.Storm_s_Embrace, Skill.Attributes.None, Skill.Professions.Ranger, 2, "5E 30R - Stance. (10 seconds.) You move 25% faster. Renewal: whenever you take elemental damage."));
            Data.Add(new Skill("Trapper's Speed", Properties.Resources.Trapper_s_Speed, Skill.Attributes.Expertise, Skill.Professions.Ranger, 2, "5E 20R - Stance. (5...25...30 seconds.) Your traps recharge 25% faster and activate 25% faster. Ends if you hit with an attack."));
            Data.Add(new Skill("Tripwire", Properties.Resources.Tripwire, Skill.Attributes.Wilderness_Survival, Skill.Professions.Ranger, 2, "10E 2C 30R - Trap. (90 seconds.) Deals 5...17...20 piercing damage to nearby foes. Causes knock-down to Crippled foes. Easily interrupted."));

            Data.Add(new Skill("Renewing Surge", Properties.Resources.Renewing_Surge, Skill.Attributes.Channeling_Magic, Skill.Professions.Ritualist, 3, "5E 1C 15R - Hex Spell. (8 seconds.) Deals 2...10...12 damage each second. End effect: You gain 1...7...8 energy."));
            Data.Add(new Skill("Offering of Spirit", Properties.Resources.Offering_of_Spirit, Skill.Attributes.Channeling_Magic, Skill.Professions.Ritualist, 1, "17%HP 5E 1/4C 15R - Elite Spell. You gain 8...15...17 Energy. You do not sacrifice Health if any spirits are within earshot.", true));
            Data.Add(new Skill("Spirit's Gift", Properties.Resources.Spirit_s_Gift, Skill.Attributes.Spawning_Power, Skill.Professions.Ritualist, 3, "10E 1C 45R - Enchantment Spell. (60 seconds.) Whenever you create a creature, all nearby allies gain 5...41...50 Health and lose one condition."));
            Data.Add(new Skill("Death Pact Signet", Properties.Resources.Death_Pact_Signet, Skill.Attributes.Restoration_Magic, Skill.Professions.Ritualist, 3, "3C 12R - Signet. Resurrects target party member with your current Health and 15...83...100% maximum Energy. For 120 seconds, you die the next time this party member dies."));
            Data.Add(new Skill("Reclaim Essence", Properties.Resources.Reclaim_Essence, Skill.Attributes.Spawning_Power, Skill.Professions.Ritualist, 3, "5E 1C 30R - Elite Spell. All of your Spirits die. If a Spirit dies in this way, you gain 5...17...20 Energy and all of your Binding Rituals are recharged.", true));
            // Dervish skills below:
            Data.Add(new Skill("Banishing Strike", Properties.Resources.Banishing_Strike, Skill.Attributes.Mysticism, Skill.Professions.Dervish, 3, "5E 1C 3R - Melee Attack. Deals 10...50...60 holy damage. Deals double damage to summoned creatures."));
            Data.Add(new Skill("Mystic Sweep", Properties.Resources.Mystic_Sweep, Skill.Attributes.Mysticism, Skill.Professions.Dervish, 3, "5E 1C 6R - Melee Attack. Deals +3...10...12 damage. Deals an additional +3...10...12 damage if you are enchanted."));
            Data.Add(new Skill("Eremite's Attack", Properties.Resources.Eremite_s_Attack, Skill.Attributes.Scythe_Mastery, Skill.Professions.Dervish, 3, "5E 6R - Scythe Attack. Deals +1...8...10 damage and removes a Dervish enchantment. Removal Effect: Deals +1...8...10 damage and strikes all adjacent foes."));
            Data.Add(new Skill("Reap Impurities", Properties.Resources.Reap_Impurities, Skill.Attributes.Scythe_Mastery, Skill.Professions.Dervish, 2, "5A - Melee Attack. Deals +3...13...15 damage. Struck foes lose 1 condition. Removal Effect: all foes adjacent to those struck take 10...34...40 holy damage."));
            Data.Add(new Skill("Twin Moon Sweep", Properties.Resources.Twin_Moon_Sweep, Skill.Attributes.Scythe_Mastery, Skill.Professions.Dervish, 4, "7A - Melee Attack. Remove 1 of your Dervish enchantments. Gain 10...42...50 Health. Removal effect: unblockable, attack twice, and gain 10...58...70 more Health."));
            Data.Add(new Skill("Victorious Sweep", Properties.Resources.Victorious_Sweep, Skill.Attributes.Scythe_Mastery, Skill.Professions.Dervish, 4, "5E 4R - Melee Attack. Deals +5...21...25 damage. You gain 30...70...80 Health for each foe you hit that has less Health than you."));
            Data.Add(new Skill("Irresistible Sweep", Properties.Resources.Irresistible_Sweep, Skill.Attributes.Scythe_Mastery, Skill.Professions.Dervish, 3, "5E 6R - Scythe Attack. Deals +3...13...15. Remove one of your Dervish enchantments. Removal effect: unblockable, removes a stance, deals +3...13...15 additional damage."));
            Data.Add(new Skill("Pious Assault", Properties.Resources.Pious_Assault, Skill.Attributes.Scythe_Mastery, Skill.Professions.Dervish, 2, "5E 12R - Melee Attack. Deals +10...18...20 damage. Removes 1 of your Dervish enchantments. Removal Effect: this skill recharges 75% faster and adjacent foes take 10...26...30 damage."));
            Data.Add(new Skill("Mystic Twister", Properties.Resources.Mystic_Twister, Skill.Attributes.Wind_Prayers, Skill.Professions.Dervish, 4, "5A 1C 8R - Spell. Deals 10...50...60 cold damage to all nearby foes. Deals 10...50...60 more cold damage if you are enchanted."));
            Data.Add(new Skill("Grenth's Fingers", Properties.Resources.Grenth_s_Fingers, Skill.Attributes.Wind_Prayers, Skill.Professions.Dervish, 3, "5E 10R - Flash Enchantment Spell. (30 seconds.) Your attacks deal cold damage. Initial effect: hits nearby foes for 10...34...40 cold damage. End effect: transfers 1...2...2 condition[s] to all nearby foes."));
            Data.Add(new Skill("Aura of Thorns", Properties.Resources.Aura_of_Thorns, Skill.Attributes.Earth_Prayers, Skill.Professions.Dervish, 3, "5E 10R - Flash Enchantment Spell. (30 seconds.) Initial effect: inflicts Bleeding condition (5...13...15 seconds) on nearby foes. End effect: inflicts Crippled condition (3...7...8 seconds) on nearby foes."));
            Data.Add(new Skill("Balthazar's Rage", Properties.Resources.Balthazar_s_Rage, Skill.Attributes.Mysticism, Skill.Professions.Dervish, 4, "10E 10R - Flash Enchantment Spell. (20 seconds.) Initial Effect: Inflicts burning (1...3...3 second[s]) on nearby foes. End effect: gain 1...2...2 strike[s] of adrenaline if any foes are within earshot."));
            Data.Add(new Skill("Dust Cloak", Properties.Resources.Dust_Cloak, Skill.Attributes.Earth_Prayers, Skill.Professions.Dervish, 4, "10E 6R - Flash Enchantment Spell. (30 seconds.) Your attacks deal earth damage. Initial effect: deals 10...34...40 earth damage to adjacent foes. End effect: inflicts Blindness condition (1...3...4 second[s]) on adjacent foes."));
            Data.Add(new Skill("Staggering Force", Properties.Resources.Staggering_Force, Skill.Attributes.Earth_Prayers, Skill.Professions.Dervish, 4, "10E 6R - Flash Enchantment Spell. (30 seconds.) Your attacks deal earth damage. Initial effect: deals 10...34...40 earth damage to nearby foes. End effect: inflicts Cracked Armor condition (1...8...10 second[s]) on nearby foes."));
            Data.Add(new Skill("Pious Renewal", Properties.Resources.Pious_Renewal, Skill.Attributes.Mysticism, Skill.Professions.Dervish, 2, "5E 8R - Elite Flash Enchantment Spell. (8 seconds.) End Effect: recharges itself and you gain 0...4...5 Energy and 0...24...30 Health.", true));
            Data.Add(new Skill("Mirage Cloak", Properties.Resources.Mirage_Cloak, Skill.Attributes.Earth_Prayers, Skill.Professions.Dervish, 3, "10E 10R - Flash Enchantment Spell. (1...6...7 second[s].) You have 40...72...80% chance to block. Initial Effect: deals 10...34...40 earth damage to nearby foes."));
            Data.Add(new Skill("Arcane Zeal", Properties.Resources.Arcane_Zeal, Skill.Attributes.Mysticism, Skill.Professions.Dervish, 2, "10E 1C 5R - Elite Enchantment Spell. (10 seconds.) You gain 1 Energy (maximum 1...6...7) for each enchantment on you whenever you cast a spell.", true));
            Data.Add(new Skill("Mystic Vigor", Properties.Resources.Mystic_Vigor, Skill.Attributes.Mysticism, Skill.Professions.Dervish, 3, "5E 1/4C 15R - Enchantment Spell. (20 seconds.) You gain 3...13...15 Health (maximum 25) for each enchantment on you whenever you hit with an attack."));
            Data.Add(new Skill("Watchful Intervention", Properties.Resources.Watchful_Intervention, Skill.Attributes.Mysticism, Skill.Professions.Dervish, 2, "10E 1C 15R - Enchantment Spell. (60 seconds.) Heals for 50...170...200 the next time damage drops target ally's Health below 25%."));
            Data.Add(new Skill("Vow of Piety", Properties.Resources.Vow_of_Piety, Skill.Attributes.Wind_Prayers, Skill.Professions.Dervish, 3, "15E 1/4C 45R - Enchantment Spell. (20 seconds) +24 armor and +1...3...4 Health regeneration. Renewal: Whenever an enchantment on you ends."));
            Data.Add(new Skill("Vital Boon", Properties.Resources.Vital_Boon, Skill.Attributes.Earth_Prayers, Skill.Professions.Dervish, 2, "5E 1C 8R - Enchantment Spell. (20 seconds.) You have +40...88...100 maximum Health. End effect: Heals you for 75...175...200."));
            Data.Add(new Skill("Heart of Holy Flame", Properties.Resources.Heart_of_Holy_Flame, Skill.Attributes.Mysticism, Skill.Professions.Dervish, 3, "10E 10R - Flash Enchantment Spell. (30 seconds.) Your attacks deal holy damage. Initial effect: deals 5...25...30 holy damage to nearby foes. End effect: inflicts Burning condition (2...4...5 seconds) on nearby foes."));
            Data.Add(new Skill("Extend Enchantments", Properties.Resources.Extend_Enchantments, Skill.Attributes.Mysticism, Skill.Professions.Dervish, 2, "5E 10R - Skill. (10 seconds.) Your next Dervish enchantment lasts 10...122...150% longer."));
            Data.Add(new Skill("Faithful Intervention", Properties.Resources.Faithful_Intervention, Skill.Attributes.Mysticism, Skill.Professions.Dervish, 2, "5E 2C 20R - Enchantment Spell. You gain 30...126...150 Health the next time damage drops your Health below 50%."));
            Data.Add(new Skill("Sand Shards", Properties.Resources.Sand_Shards, Skill.Attributes.Earth_Prayers, Skill.Professions.Dervish, 3, "10E 10R - Flash Enchantment Spell. (30 seconds.) Deal 10...50...60 earth damage to all other adjacent foes whenever you hit with your scythe. Ends after 1...4...5 hit[s]."));
            Data.Add(new Skill("Lyssa's Haste", Properties.Resources.Lyssa_s_Haste, Skill.Attributes.Wind_Prayers, Skill.Professions.Dervish, 3, "10E 15R - Flash Enchantment Spell. (3...13...15 seconds.) Your Dervish enchantments recharge 33% faster. Initial Effect: interrupts all adjacent foes. End Effect: interrupts all adjacent foes. 50% failure chance unless Wind Prayers 5 or higher."));
            Data.Add(new Skill("Guiding Hands", Properties.Resources.Guiding_Hands, Skill.Attributes.Wind_Prayers, Skill.Professions.Dervish, 2, "10E 1/4C 10R - Enchantment Spell. (20 seconds.) Your next 0...2...3 attack[s] cannot be blocked. Initial effect: removes Blindness."));
            Data.Add(new Skill("Fleeting Stability", Properties.Resources.Fleeting_Stability, Skill.Attributes.Earth_Prayers, Skill.Professions.Dervish, 2, "5E 10R - Flash Enchantment Spell. (2...5...6 seconds.) You cannot be knocked down and move 25% faster. Ends if knockdown prevented."));
            Data.Add(new Skill("Armor of Sanctity", Properties.Resources.Armor_of_Sanctity, Skill.Attributes.Earth_Prayers, Skill.Professions.Dervish, 4, "5E 1/4C 15R - Enchantment Spell. Inflicts Weakness condition on all adjacent foes (5...13...15). You take 5...17...20 less damage from foes with a condition. (15 seconds.)"));
            Data.Add(new Skill("Mystic Regeneration", Properties.Resources.Mystic_Regeneration, Skill.Attributes.Earth_Prayers, Skill.Professions.Dervish, 3, "10E 1/4C 10R - Enchantment Spell. (5...17...20 seconds.) You have +1...3...4 Health regeneration for each enchantment (maximum of 8) on you."));
            Data.Add(new Skill("Vow of Silence", Properties.Resources.Vow_of_Silence, Skill.Attributes.Mysticism, Skill.Professions.Dervish, 2, "5E 1/4C 10R - Elite Enchantment Spell. (5...9...10 seconds.) Spells cannot target you. You cannot cast spells.", true));
            Data.Add(new Skill("Avatar of Balthazar", Properties.Resources.Avatar_of_Balthazar, Skill.Attributes.Mysticism, Skill.Professions.Dervish, 4, "5E 2C 20R - Elite Form. (10...74...90 seconds.) You gain +20 armor against physical damage, you gain adrenaline 25% faster, your attacks deal holy damage, you inflict Burning (1...3...3 second[s]) on nearby foes whenever you lose a Dervish enchantment. This skill is disabled for 45 seconds.", true));
            Data.Add(new Skill("Avatar of Dwayna", Properties.Resources.Avatar_of_Dwayna, Skill.Attributes.Mysticism, Skill.Professions.Dervish, 5, "5E 2C 20R - Elite Form. (10...74...90 seconds.) You deal holy damage. Whenever you use a Dervish attack skill, you lose 1 hex. Heal allies in earshot for 5...41...50 Health when you lose a Dervish enchantment. This skill is disabled for 45 seconds.", true));
            Data.Add(new Skill("Avatar of Grenth", Properties.Resources.Avatar_of_Grenth, Skill.Attributes.Mysticism, Skill.Professions.Dervish, 4, "5E 2C 20R - Elite Form. (10...74...90 seconds.) Your scythe attacks deal dark damage and steal 0...10...12 Health. You are immune to Disease. Apply Disease to all adjacent foes (3 seconds) when you lose a Dervish enchantment. This skill is disabled for 45 seconds.", true));
            Data.Add(new Skill("Avatar of Lyssa", Properties.Resources.Avatar_of_Lyssa, Skill.Attributes.Mysticism, Skill.Professions.Dervish, 4, "5E 2C 20R - Elite Form. (10...74...90 seconds.) Your Dervish enchantments recharge 50% faster and deal chaos damage with attacks. Steal 1 Energy from nearby foes when you lose a Dervish enchantment. This skill is disabled for 45 seconds.", true));
            Data.Add(new Skill("Avatar of Melandru", Properties.Resources.Avatar_of_Melandru, Skill.Attributes.Mysticism, Skill.Professions.Dervish, 4, "5E 2C 20R - Elite Form. (10...74...90 seconds.) You have +150 maximum Health, +30 elemental armor, and your attacks deal earth damage. Cure 1 condition from all party members in earshot whenever you lose a Dervish enchantment. This skill is disabled for 45 seconds.", true));
            Data.Add(new Skill("Meditation", Properties.Resources.Meditation, Skill.Attributes.Mysticism, Skill.Professions.Dervish, 2, "10E 1/4C 20R - Enchantment Spell. (20 seconds.) Lose all adrenaline. Gain 1...3...4 Energy whenever an enchantment on you ends."));
            Data.Add(new Skill("Eremite's Zeal", Properties.Resources.Eremite_s_Zeal, Skill.Attributes.Mysticism, Skill.Professions.Dervish, 2, "5E 1/4C 15R - Enchantment Spell. (10 seconds.) Initial effect: you gain 1...3...3 Energy (maximum 8) for each foe in earshot. End effect: you gain 1...3...3 Energy (maximum 8) for each foe in earshot."));
            Data.Add(new Skill("Natural Healing", Properties.Resources.Natural_Healing, Skill.Attributes.Wind_Prayers, Skill.Professions.Dervish, 2, "5E 2C 6R - Spell. Heals you for 50...146...170. This skill activates 50% faster if you are not enchanted."));
            Data.Add(new Skill("Imbue Health", Properties.Resources.Imbue_Health, Skill.Attributes.Mysticism, Skill.Professions.Dervish, 2, "10E 1/4C 10R - Spell. Heals for 5...41...50% of your current Health (maximum 300). Cannot self-target."));
            Data.Add(new Skill("Mystic Healing", Properties.Resources.Mystic_Healing, Skill.Attributes.Wind_Prayers, Skill.Professions.Dervish, 4, "5E 1C 4R - Spell. Heals you for 5...53...65. Heals all enchanted party members for 5...53...65 Health."));
            Data.Add(new Skill("Dwayna's Touch", Properties.Resources.Dwayna_s_Touch, Skill.Attributes.Wind_Prayers, Skill.Professions.Dervish, 3, "5E 3/4C 5R - Touch Spell. Heals for 15...51...60 (maximum 150) for each enchantment on you."));
            Data.Add(new Skill("Pious Restoration", Properties.Resources.Pious_Restoration, Skill.Attributes.Wind_Prayers, Skill.Professions.Dervish, 2, "5E 1C 8R - Spell. Gain 80...136...150 Health and remove 1 Dervish enchantment. Removal effect: lose 1...2...2 hex[es]."));
            Data.Add(new Skill("Signet of Pious Light", Properties.Resources.Signet_of_Pious_Light, Skill.Attributes.Earth_Prayers, Skill.Professions.Dervish, 3, "1C 20R - Signet. Heals for 30...126...150. Removes one of your Dervish enchantments. Removal effect: recharges 75% faster."));
            Data.Add(new Skill("Intimidating Aura", Properties.Resources.Intimidating_Aura, Skill.Attributes.Mysticism, Skill.Professions.Dervish, 3, "10E 3/4C 20R - Enchantment Spell. (60 seconds.) You have +40...88...100 max Health and take -1...8...10 damage from foes with less Health than you."));
            Data.Add(new Skill("Mystic Sandstorm", Properties.Resources.Mystic_Sandstorm, Skill.Attributes.Earth_Prayers, Skill.Professions.Dervish, 3, "5A 1C 8R - Spell. (3 seconds.) Deals 10...18...20 earth damage each second. Deals an additional 10...18...20 damage to attacking foes. Hits foes nearby your initial location. Lasts twice as long if you are enchanted."));
            Data.Add(new Skill("Winds of Disenchantment", Properties.Resources.Winds_of_Disenchantment, Skill.Attributes.Wind_Prayers, Skill.Professions.Dervish, 3, "10E 3/4C 15R - Spell. Remove one of your Dervish enchantments. Removal effect: all nearby foes lose 1 enchantment and take 20...68...80 cold damage."));
            Data.Add(new Skill("Rending Touch", Properties.Resources.Rending_Touch, Skill.Attributes.Mysticism, Skill.Professions.Dervish, 2, "5E 3/4C 12R - Touch Spell. Deals 15...55...65 cold damage. Lose 1 Dervish enchantment. Removal effect: target foe loses 1 enchantment and you gain 1 strike of adrenaline."));
            Data.Add(new Skill("Crippling Sweep", Properties.Resources.Crippling_Sweep, Skill.Attributes.Scythe_Mastery, Skill.Professions.Dervish, 3, "5E 6R - Scythe Attack. (3...10...12 seconds.) Inflicts Crippled condition. Deals +3...13...15 damage to moving foes."));
            Data.Add(new Skill("Wounding Strike", Properties.Resources.Wounding_Strike, Skill.Attributes.Scythe_Mastery, Skill.Professions.Dervish, 3, "5E 3R - Elite Scythe Attack. Deals +5...17...20 damage. Inflict Bleeding condition (5...17...20 seconds). Removes 1 Dervish enchantment. Also inflicts Deep Wound condition (5...17...20 seconds) if an enchantment is removed.", true));
            Data.Add(new Skill("Wearying Strike", Properties.Resources.Wearying_Strike, Skill.Attributes.Scythe_Mastery, Skill.Professions.Dervish, 3, "6A - Scythe Attack. Remove 1 Dervish Enchantment. Removal Effect: Inflicts Deep Wound condition (3...9...10 seconds). You are Weakened (10 seconds) if an enchantment is not lost."));
            Data.Add(new Skill("Lyssa's Assault", Properties.Resources.Lyssa_s_Assault, Skill.Attributes.Scythe_Mastery, Skill.Professions.Dervish, 3, "5E 1/2C 15R - Scythe Attack. Interrupts an action. If you are enchanted, the interrupted skill is disabled for an additional 1...8...10 second[s]. This attack does 50% of normal damage."));
            Data.Add(new Skill("Chilling Victory", Properties.Resources.Chilling_Victory, Skill.Attributes.Scythe_Mastery, Skill.Professions.Dervish, 4, "6A - Scythe Attack. Deals +3...13...15 damage. Deals 10...26...30 cold damage to each foe hit who has less Health than you and foes adjacent to those targets."));
            Data.Add(new Skill("Conviction", Properties.Resources.Conviction, Skill.Attributes.Earth_Prayers, Skill.Professions.Dervish, 3, "5E 10R - Flash Enchantment Spell. (10 seconds.) Gain +10 armor while conditioned. Gain 1...3...3 Health regeneration for each condition you have. End Effect: Remove 1...2...2 condition[s]."));
            Data.Add(new Skill("Enchanted Haste", Properties.Resources.Enchanted_Haste, Skill.Attributes.None, Skill.Professions.Dervish, 3, "10E 15R - Flash Enchantment Spell. (7 seconds). You move 25% faster. Lose 1 condition if this enchantment ends prematurely."));
            Data.Add(new Skill("Pious Concentration", Properties.Resources.Pious_Concentration, Skill.Attributes.Earth_Prayers, Skill.Professions.Dervish, 1, "10E 10R - Stance. (5...17...20 seconds.) Prevents interrupts. Prevention cost: you lose one Dervish enchantment or Pious Concentration ends."));
            Data.Add(new Skill("Pious Haste", Properties.Resources.Pious_Haste, Skill.Attributes.Mysticism, Skill.Professions.Dervish, 2, "5E 12R - Stance. (1...6...7 second[s].) Lose 1 Dervish enchantment and move 25% faster. Removal Effect: Run 50% faster instead."));
            Data.Add(new Skill("Whirling Charge", Properties.Resources.Whirling_Charge, Skill.Attributes.Wind_Prayers, Skill.Professions.Dervish, 3, "10E 6R - Flash Enchantment Spell. (1...5...6 second[s].) You move 33% faster. Deal 10...50...60 cold damage to all other nearby foes the next time you hit a foe and this enchantment ends."));
            Data.Add(new Skill("Test of Faith", Properties.Resources.Test_of_Faith, Skill.Attributes.Wind_Prayers, Skill.Professions.Dervish, 3, "7A 3/4C 5R - Touch Spell. Deals 15...55...65 cold damage and removes 1 enchantment. Target foe is Dazed for 1...3...4 second[s] if that foe was not enchanted."));
            // Paragon skills:
            Data.Add(new Skill("Blazing Spear", Properties.Resources.Blazing_Spear, Skill.Attributes.Spear_Mastery, Skill.Professions.Paragon, 4, "6A - Spear Attack. Deals +5...21...25 damage. Inflicts Burning condition (1...3...3 second[s])."));
            Data.Add(new Skill("Mighty Throw", Properties.Resources.Mighty_Throw, Skill.Attributes.Spear_Mastery, Skill.Professions.Paragon, 2, "2A 3C - Spear Attack. Deals +10...34...40 damage. This spear moves three times faster."));
            Data.Add(new Skill("Cruel Spear", Properties.Resources.Cruel_Spear, Skill.Attributes.Spear_Mastery, Skill.Professions.Paragon, 3, "7A - Elite Spear Attack. Deals +1...25...31 damage. Inflicts Deep Wound condition (5...17...20 seconds) if target is not moving.", true));
            Data.Add(new Skill("Harrier's Toss", Properties.Resources.Harrier_s_Toss, Skill.Attributes.Spear_Mastery, Skill.Professions.Paragon, 3, "10E 1/2C 10R - Spear Attack. Deals +5...17...20 damage. Deals 5...25...30 more damage if target is moving."));
            Data.Add(new Skill("Unblockable Throw", Properties.Resources.Unblockable_Throw, Skill.Attributes.Spear_Mastery, Skill.Professions.Paragon, 2, "7A 3C - Spear Attack. Deals +10...34...40 damage. Unblockable."));
            Data.Add(new Skill("Spear of Lightning", Properties.Resources.Spear_of_Lightning, Skill.Attributes.Spear_Mastery, Skill.Professions.Paragon, 3, "5E 6R - Spear Attack. Deals +8...18...20 lightning damage. 25% armor penetration."));
            Data.Add(new Skill("Wearying Spear", Properties.Resources.Wearying_Spear, Skill.Attributes.Spear_Mastery, Skill.Professions.Paragon, 2, "3A - Spear Attack. Deals +10...34...40 damage. You are Weakened (5 seconds)."));
            Data.Add(new Skill("Anthem of Fury", Properties.Resources.Anthem_of_Fury, Skill.Attributes.Leadership, Skill.Professions.Paragon, 2, "5E 1C 10R - Elite Chant. (10 seconds.) Party members in earshot gain 1...3...4 adrenaline with their next attack skill.", true));
            Data.Add(new Skill("Crippling Anthem", Properties.Resources.Crippling_Anthem, Skill.Attributes.Command, Skill.Professions.Paragon, 3, "4A 1C - Elite Chant. (10 seconds.) Allies in earshot inflict Crippled condition (5...13...15 seconds) with their next attack skill.", true));
            Data.Add(new Skill("Defensive Anthem", Properties.Resources.Defensive_Anthem, Skill.Attributes.Leadership, Skill.Professions.Paragon, 3, "10E 1C 25R - Elite Chant. (4...9...10 seconds.) Party members in earshot have 50% chance to block. Ends when hitting with an attack skill.", true));
            Data.Add(new Skill("Godspeed", Properties.Resources.Godspeed, Skill.Attributes.Command, Skill.Professions.Paragon, 3, "10E 30R - Shout. (5...17...20 seconds.) Allies in earshot move 25% faster while enchanted."));
            Data.Add(new Skill("Anthem of Flame", Properties.Resources.Anthem_of_Flame, Skill.Attributes.Leadership, Skill.Professions.Paragon, 3, "5E 1C 10R - Chant. (10 seconds.) Party members in earshot inflict Burning condition (1...3...3 second[s]) with their next attack skill."));
            Data.Add(new Skill("\"Go for the Eyes!\"", Properties.Resources._Go_for_the_Eyes__, Skill.Attributes.Command, Skill.Professions.Paragon, 5, "4A - Shout. (10 seconds.) Allies in earshot have +30...86...100% to land a critical hit with their next attack."));
            Data.Add(new Skill("Anthem of Envy", Properties.Resources.Anthem_of_Envy, Skill.Attributes.Command, Skill.Professions.Paragon, 3, "6A 1C - Chant. (10 seconds.) Allies in earshot do +10...22...25 damage with their next attack skill. Damage bonus only applies to foes with more than 50% Health."));
            Data.Add(new Skill("Song of Power", Properties.Resources.Song_of_Power, Skill.Attributes.Motivation, Skill.Professions.Paragon, 1, "25E 1C 30R - Chant. (5...17...20 seconds.) Allies in earshot gain +4 Energy regeneration. Ends when using a skill."));
            Data.Add(new Skill("Zealous Anthem", Properties.Resources.Zealous_Anthem, Skill.Attributes.Motivation, Skill.Professions.Paragon, 2, "10E 1C 20R - Chant. (10 seconds.) Allies in earshot gain 1...7...8 Energy with their next attack skill."));
            Data.Add(new Skill("Aria of Zeal", Properties.Resources.Aria_of_Zeal, Skill.Attributes.Motivation, Skill.Professions.Paragon, 4, "10E 2C 20R - Chant. (10 seconds.) Allies in earshot gain 1...5...6 Energy with their next spell."));
            Data.Add(new Skill("Lyric of Zeal", Properties.Resources.Lyric_of_Zeal, Skill.Attributes.Motivation, Skill.Professions.Paragon, 2, "6A 1C 5R - Chant. (10 seconds.) Allies in earshot gain 1...7...8 Energy with their next signet."));
            Data.Add(new Skill("Ballad of Restoration", Properties.Resources.Ballad_of_Restoration, Skill.Attributes.Motivation, Skill.Professions.Paragon, 4, "10E 1C 20R - Chant. (10 seconds.) Party members in earshot gain 15...63...75 Health the next time they take damage."));
            Data.Add(new Skill("Chorus of Restoration", Properties.Resources.Chorus_of_Restoration, Skill.Attributes.Motivation, Skill.Professions.Paragon, 2, "4A 1C 5R - Chant. (10 seconds.) Allies in earshot are healed for 30...78...90 with their next shout or chant."));
            Data.Add(new Skill("Aria of Restoration", Properties.Resources.Aria_of_Restoration, Skill.Attributes.Motivation, Skill.Professions.Paragon, 4, "10E 1C 20R - Chant. (10 seconds.) Party members in earshot gain 30...78...90 Health with their next spell."));
            Data.Add(new Skill("Song of Concentration", Properties.Resources.Song_of_Concentration, Skill.Attributes.None, Skill.Professions.Paragon, 1, "8A 2C 5R - Chant. (10 seconds.) Allies in earshot are uninterruptible with their next skill."));
            Data.Add(new Skill("Anthem of Guidance", Properties.Resources.Anthem_of_Guidance, Skill.Attributes.Command, Skill.Professions.Paragon, 1, "4A 1C - Elite Chant. (10 seconds.) Party members in earshot are unblockable with their next attack skill.", true));
            Data.Add(new Skill("Energizing Chorus", Properties.Resources.Energizing_Chorus, Skill.Attributes.Motivation, Skill.Professions.Paragon, 3, "4A 1C - Chant. (10 seconds.) The next shout or chant costs 3...6...7 less Energy for allies within earshot."));
            Data.Add(new Skill("Song of Purification", Properties.Resources.Song_of_Purification, Skill.Attributes.Motivation, Skill.Professions.Paragon, 3, "5A 2C - Elite Chant. (20 seconds.) Allies in earshot lose one condition with their next 1...3...3 skill[s].", true));
            Data.Add(new Skill("Hexbreaker Aria", Properties.Resources.Hexbreaker_Aria, Skill.Attributes.None, Skill.Professions.Paragon, 4, "8A 2C - Chant. (10 seconds.) Allies in earshot lose one hex with their next spell."));
            Data.Add(new Skill("\"Brace Yourself!\"", Properties.Resources._Brace_Yourself__, Skill.Attributes.Command, Skill.Professions.Paragon, 2, "5E 4R - Shout. (5...13...15 seconds.) Prevents the next knock-down and deals 15...63...75 damage to all foes near target ally. Cannot self-target."));
            Data.Add(new Skill("Awe", Properties.Resources.Awe, Skill.Attributes.Leadership, Skill.Professions.Paragon, 2, "10E 3/4C 10R - Half Range Skill. Inflicts Dazed condition (5...13...15 seconds). No effect unless target is knocked-down."));
            Data.Add(new Skill("Enduring Harmony", Properties.Resources.Enduring_Harmony, Skill.Attributes.Leadership, Skill.Professions.Paragon, 2, "5E 1C 10R - Echo. (10...30...35 seconds.) Chants and shouts last 50% longer on target ally. Cannot target spirits."));
            Data.Add(new Skill("Blazing Finale", Properties.Resources.Blazing_Finale, Skill.Attributes.Leadership, Skill.Professions.Paragon, 3, "5E 1C 8R - Echo. (10...30...35 seconds.) Inflicts Burning condition (1...6...7 second[s]) to adjacent foes whenever a chant or shout ends on target ally. Cannot target spirits."));
            Data.Add(new Skill("Burning Refrain", Properties.Resources.Burning_Refrain, Skill.Attributes.Leadership, Skill.Professions.Paragon, 2, "10E 1C 10R - Echo. (20 seconds.) Inflicts Burning condition (1...3...3 second[s]) if target ally hits a foe with more Health. Renewal: Whenever a chant or shout ends on that ally. Cannot target spirits."));
            Data.Add(new Skill("Finale of Restoration", Properties.Resources.Finale_of_Restoration, Skill.Attributes.Motivation, Skill.Professions.Paragon, 2, "5E 1C 10R - Echo. (10...30...35 seconds.) Target ally gains 15...63...75 Health whenever a shout or chant ends on that ally. Cannot target spirits."));
            Data.Add(new Skill("Mending Refrain", Properties.Resources.Mending_Refrain, Skill.Attributes.Motivation, Skill.Professions.Paragon, 3, "10E 1C 8R - Echo. (15 seconds.) Target ally has +2...3...3 Health regeneration. Renewal: whenever a chant or shout ends on that ally. Cannot target spirits."));
            Data.Add(new Skill("Purifying Finale", Properties.Resources.Purifying_Finale, Skill.Attributes.Motivation, Skill.Professions.Paragon, 2, "5E 1C 10R - Echo. (10...30...35 seconds.) Target ally loses one condition whenever a chant or shout ends on that ally. Cannot target spirits."));
            Data.Add(new Skill("Bladeturn Refrain", Properties.Resources.Bladeturn_Refrain, Skill.Attributes.Command, Skill.Professions.Paragon, 3, "5E 1C 8R - Echo. (20 seconds.) Target ally has 5...17...20% chance to block. Renewal: Whenever a chant or shout ends on that ally. Cannot target spirits."));
            Data.Add(new Skill("Glowing Signet", Properties.Resources.Glowing_Signet, Skill.Attributes.Leadership, Skill.Professions.Paragon, 2, "1/4C 20R - Signet. You gain 5...13...15 Energy if target foe is Burning."));
            Data.Add(new Skill("Leader's Zeal", Properties.Resources.Leader_s_Zeal, Skill.Attributes.Motivation, Skill.Professions.Paragon, 1, "5E 12R - Skill. You gain 2 Energy (maximum 8...11...12 Energy) for each nearby ally."));
            Data.Add(new Skill("Leader's Comfort", Properties.Resources.Leader_s_Comfort, Skill.Attributes.Leadership, Skill.Professions.Paragon, 1, "5E 2C 8R - Skill. You gain 30...66...75 Health. You also gain 10...18...20 Health (maximum 140) for each ally in earshot."));
            Data.Add(new Skill("Signet of Synergy", Properties.Resources.Signet_of_Synergy, Skill.Attributes.Motivation, Skill.Professions.Paragon, 3, "1C 10R - Signet. Heal target ally for 40...88...100. You are also healed for 40...88...100 if you are not enchanted. Cannot self-target."));
            Data.Add(new Skill("Angelic Protection", Properties.Resources.Angelic_Protection, Skill.Attributes.Leadership, Skill.Professions.Paragon, 2, "5E 30R - Skill. (10 seconds.) Each second that target ally takes damage over 250...130...100, that ally is healed for any damage over that amount. Cannot self target."));
            Data.Add(new Skill("Angelic Bond", Properties.Resources.Angelic_Bond, Skill.Attributes.Leadership, Skill.Professions.Paragon, 1, "5E 1C 30R - Elite Skill. (10 seconds.) The next time an ally within earshot would take fatal damage, that damage is negated and that ally is healed for 20...164...200. Ends on other allies.", true));
            Data.Add(new Skill("Cautery Signet", Properties.Resources.Cautery_Signet, Skill.Attributes.None, Skill.Professions.Paragon, 2, "2C 15R - Elite Signet. All party members lose all conditions. You begin Burning (one second for each condition removed).", true));
            Data.Add(new Skill("\"Stand Your Ground!\"", Properties.Resources._Stand_Your_Ground__, Skill.Attributes.Command, Skill.Professions.Paragon, 3, "10E 20R - Shout. (5...17...20 seconds.) Party members in earshot gain +24 armor when not moving."));
            Data.Add(new Skill("\"Lead the Way!\"", Properties.Resources._Lead_the_Way__, Skill.Attributes.Leadership, Skill.Professions.Paragon, 3, "10E 8R - Shout. Target ally moves 25% faster for 1...4...5 seconds (maximum of 20 seconds) for each ally in earshot."));
            Data.Add(new Skill("\"Make Haste!\"", Properties.Resources._Make_Haste__, Skill.Attributes.Command, Skill.Professions.Paragon, 1, "5E 10R - Shout. (5...17...20 seconds.) Target ally moves 33% faster. Ends if target ally hits with an attack. Cannot self-target."));
            Data.Add(new Skill("\"We Shall Return!\"", Properties.Resources._We_Shall_Return__, Skill.Attributes.Command, Skill.Professions.Paragon, 4, "25E 30R - Shout. All party members in earshot are resurrected (25...45...50% Health and 5...17...20% Energy)."));
            Data.Add(new Skill("\"Never Give Up!\"", Properties.Resources._Never_Give_Up__, Skill.Attributes.Command, Skill.Professions.Paragon, 4, "5E 15R - Shout. Allies in earshot gain 1...8...10 Energy. Only affects allies below 75% Health."));
            Data.Add(new Skill("\"Help Me!\"", Properties.Resources._Help_Me__, Skill.Attributes.Command, Skill.Professions.Paragon, 2, "5E 10R - Shout. (1...8...10 seconds.) Other allies' spells targeting you cast 50% faster. You gain 15...75...90 Health."));
            Data.Add(new Skill("\"Fall Back!\"", Properties.Resources._Fall_Back__, Skill.Attributes.Command, Skill.Professions.Paragon, 4, "10E 20R - Shout. (4...9...10 seconds.) Allies in earshot gain 5...13...15 Health per second while moving and move 33% faster. Ends for an ally if that ally hits with an attack."));
            Data.Add(new Skill("\"Incoming!\"", Properties.Resources._Incoming__, Skill.Attributes.Command, Skill.Professions.Paragon, 3, "5E 20R - Elite Shout. (4...9...10 seconds) Allies in earshot move 33% faster and gain 5...13...15 Health while moving.", true));
            Data.Add(new Skill("\"They're on Fire!\"", Properties.Resources._They_re_on_Fire__, Skill.Attributes.Leadership, Skill.Professions.Paragon, 2, "10E 10R - Shout. (10 seconds.) Party members in earshot take 5...29...35% less damage from Burning foes."));
            Data.Add(new Skill("\"Never Surrender!\"", Properties.Resources._Never_Surrender__, Skill.Attributes.Command, Skill.Professions.Paragon, 4, "5E 15R - Shout. (15 seconds.) Party members in earshot gain +1...4...5 Health regeneration. Only affects party members below 75% Health."));
            Data.Add(new Skill("\"It's Just a Flesh Wound.\"", Properties.Resources._It_s_Just_a_Flesh_Wound__, Skill.Attributes.Motivation, Skill.Professions.Paragon, 2, "5E 2R - Elite Shout. Remove all conditions from target ally. That ally moves 25% faster (1...8...10 second[s]) if a condition was removed. Cannot self-target.", true));
            Data.Add(new Skill("Barbed Spear", Properties.Resources.Barbed_Spear, Skill.Attributes.Spear_Mastery, Skill.Professions.Paragon, 3, "2A - Spear Attack. Inflicts Bleeding condition (5...17...20 seconds)."));
            Data.Add(new Skill("Vicious Attack", Properties.Resources.Vicious_Attack, Skill.Attributes.Spear_Mastery, Skill.Professions.Paragon, 2, "5E 8R - Spear Attack. Deals +5...17...20 damage. Inflicts Deep Wound condition (5...13...15 seconds) with a critical hit."));
            Data.Add(new Skill("Stunning Strike", Properties.Resources.Stunning_Strike, Skill.Attributes.Spear_Mastery, Skill.Professions.Paragon, 2, "10A - Elite Spear Attack. Deals +5...25...30 damage. Inflicts Dazed condition (4...9...10 seconds) if target has a condition.", true));
            Data.Add(new Skill("Merciless Spear", Properties.Resources.Merciless_Spear, Skill.Attributes.Spear_Mastery, Skill.Professions.Paragon, 4, "6A - Spear Attack. Inflicts Deep Wound condition (5...17...20 seconds). No effect unless target has less than 50% Health."));
            Data.Add(new Skill("Disrupting Throw", Properties.Resources.Disrupting_Throw, Skill.Attributes.Spear_Mastery, Skill.Professions.Paragon, 3, "5E 1/2C 10R - Spear Attack. Interrupts actions. No effect unless target has a condition."));
            Data.Add(new Skill("Wild Throw", Properties.Resources.Wild_Throw, Skill.Attributes.Spear_Mastery, Skill.Professions.Paragon, 2, "7A - Spear Attack. Deals +5...17...20 damage. Unblockable. Ends target's stance. Disables your non-spear attack skills for 3 seconds."));
            // Follows: Assassin skills:
            Data.Add(new Skill("Malicious Strike", Properties.Resources.Malicious_Strike, Skill.Attributes.Critical_Strikes, Skill.Professions.Assassin, 3, "5E 6R - Melee Attack. If target foe has a condition, this attack deals +10...26...30 damage and is a critical hit."));
            Data.Add(new Skill("Shattering Assault", Properties.Resources.Shattering_Assault, Skill.Attributes.Critical_Strikes, Skill.Professions.Assassin, 4, "10E 6R - Elite Dual Attack. Deals 5...41...50 damage. Removes one enchantment. Unblockable. Must follow an off-hand attack.", true));
            Data.Add(new Skill("Golden Skull Strike", Properties.Resources.Golden_Skull_Strike, Skill.Attributes.Dagger_Mastery, Skill.Professions.Assassin, 3, "10E 15R - Elite Off-Hand Attack. Inflicts Dazed condition (1...4...5 seconds). No effect unless you are enchanted.", true));
            Data.Add(new Skill("Black Spider Strike", Properties.Resources.Black_Spider_Strike, Skill.Attributes.Dagger_Mastery, Skill.Professions.Assassin, 2, "10E 12R - Off-Hand Attack. Deals +5...17...20 damage. Inflicts Poisoned condition (5...17...20 seconds). Must strike a hexed foe."));
            Data.Add(new Skill("Golden Fox Strike", Properties.Resources.Golden_Fox_Strike, Skill.Attributes.Dagger_Mastery, Skill.Professions.Assassin, 5, "5E 4R - Lead Attack. Deals +10...26...30 damage. Unblockable if you are Enchanted."));
            Data.Add(new Skill("Deadly Haste", Properties.Resources.Deadly_Haste, Skill.Attributes.Critical_Strikes, Skill.Professions.Assassin, 2, "10E 1C 20R - Enchantment Spell. (10...30...35 seconds.) Your half-ranged spells cast 5...41...50% faster and recharge 5...41...50% faster."));
            Data.Add(new Skill("Assassin's Remedy", Properties.Resources.Assassin_s_Remedy, Skill.Attributes.Critical_Strikes, Skill.Professions.Assassin, 4, "5E 1C 20R - Enchantment Spell. (30 seconds.) Your next 1...8...10 attack skills each remove one condition."));
            Data.Add(new Skill("Fox's Promise", Properties.Resources.Fox_s_Promise, Skill.Attributes.Dagger_Mastery, Skill.Professions.Assassin, 2, "10E 1C 20R - Elite Enchantment Spell. (5...17...20 seconds.) Your dagger attacks are unblockable. Ends the next time you fail to hit.", true));
            Data.Add(new Skill("Feigned Neutrality", Properties.Resources.Feigned_Neutrality, Skill.Attributes.Shadow_Arts, Skill.Professions.Assassin, 2, "5E 1/4C 25R - Enchantment Spell. (4...9...10 seconds.) You have +7 Health regeneration and +80 armor. Ends if you hit with an attack or use a skill."));
            Data.Add(new Skill("Hidden Caltrops", Properties.Resources.Hidden_Caltrops, Skill.Attributes.Shadow_Arts, Skill.Professions.Assassin, 2, "5E 1C 12R - Elite Hex Spell. (1...8...10 seconds.) Causes 50% slower movement. End effect: inflicts Crippled condition (1...12...15 seconds). Your non-Assassin skills are disabled (10 seconds.)", true));
            Data.Add(new Skill("Assault Enchantments", Properties.Resources.Assault_Enchantments, Skill.Attributes.None, Skill.Professions.Assassin, 2, "5E 1/4C 8R - Elite Skill. Removes all enchantments. Must follow a dual attack.", true));
            Data.Add(new Skill("Wastrel's Collapse", Properties.Resources.Wastrel_s_Collapse, Skill.Attributes.None, Skill.Professions.Assassin, 4, "5E 1/4C 20R - Elite Skill. Shadow Step to target foe. Causes knock-down if target foe is not using a skill. Disables your non-dagger attack skills (10 seconds).", true));
            Data.Add(new Skill("Lift Enchantment", Properties.Resources.Lift_Enchantment, Skill.Attributes.None, Skill.Professions.Assassin, 2, "5E 1/4C 10R - Touch Skill. Removes one enchantment. No effect unless target foe is knocked-down."));
            Data.Add(new Skill("Augury of Death", Properties.Resources.Augury_of_Death, Skill.Attributes.Deadly_Arts, Skill.Professions.Assassin, 2, "5E 1C 20R - Half Range Hex Spell. (5...29...35 seconds.) Inflict Deep Wound condition (5...17...20 seconds) and Shadow Step to target foe the next time damage would drop this foe's Health below 50%."));
            Data.Add(new Skill("Signet of Toxic Shock", Properties.Resources.Signet_of_Toxic_Shock, Skill.Attributes.Deadly_Arts, Skill.Professions.Assassin, 3, "1C 15R - Signet. Deals 10...82...100 damage. No effect unless target foe is Poisoned"));
            Data.Add(new Skill("Signet of Twilight", Properties.Resources.Signet_of_Twilight, Skill.Attributes.None, Skill.Professions.Assassin, 2, "2C 20R - Signet. Removes one enchantment for each hex on target foe."));
            Data.Add(new Skill("Way of the Assassin", Properties.Resources.Way_of_the_Assassin, Skill.Attributes.Critical_Strikes, Skill.Professions.Assassin, 3, "5E 12R - Elite Stance. (20 seconds.) While wielding daggers, you attack 5...17...20% faster and have +5...29...35% chance to land a critical hit.", true));
            Data.Add(new Skill("Shadow Walk", Properties.Resources.Shadow_Walk, Skill.Attributes.None, Skill.Professions.Assassin, 1, "5E 30R - Stance. (15 seconds.) Shadow Step to target foe. End effect: return to your original location. Disables your attack skills for 1 second. Disables your stances and enchantments for 10 seconds."));
            Data.Add(new Skill("Death's Retreat", Properties.Resources.Death_s_Retreat, Skill.Attributes.Shadow_Arts, Skill.Professions.Assassin, 2, "5E 1/4C 20R - Spell. Gain 40...112...130 Health if you have less Health than target ally. Initial effect: Shadow Step to this ally. Cannot self-target."));
            Data.Add(new Skill("Shadow Prison", Properties.Resources.Shadow_Prison, Skill.Attributes.Deadly_Arts, Skill.Professions.Assassin, 3, "10E 1/4C 25R - Elite Hex Spell. Shadow Step to target foe. This foe moves 66% slower (1...6...7 seconds).", true));
            Data.Add(new Skill("Swap", Properties.Resources.Swap, Skill.Attributes.None, Skill.Professions.Assassin, 2, "5E 1/4C 10R - Spell. You and target summoned creature Shadow Step to each other's locations."));
            Data.Add(new Skill("Shadow Meld", Properties.Resources.Shadow_Meld, Skill.Attributes.None, Skill.Professions.Assassin, 1, "-1ER 10E 1/4C 20R - Elite Enchantment Spell. Shadow Step to target ally. End effect: return to your original location. Cannot self-target.", true));
            // Some mesmer:
            Data.Add(new Skill("Price of Pride", Properties.Resources.Price_of_Pride, Skill.Attributes.Domination_Magic, Skill.Professions.Mesmer, 2, "5E 2C 8R - Hex Spell. (5...17...20 seconds.) Causes 1...6...7 Energy loss the next time target foe uses an elite skill."));
            Data.Add(new Skill("Air of Disenchantment", Properties.Resources.Air_of_Disenchantment, Skill.Attributes.Illusion_Magic, Skill.Professions.Mesmer, 2, "10E 1C 10R - Elite Hex Spell. Also hexes foes near your target (5...17...20 seconds). Remove one enchantment from target and nearby foes. Enchantments expire 150...270...300% faster on those foes.", true));
            Data.Add(new Skill("Signet of Clumsiness", Properties.Resources.Signet_of_Clumsiness, Skill.Attributes.Illusion_Magic, Skill.Professions.Mesmer, 5, "1/4C 8R - Signet. Interrupts an attack for target foe and all adjacent foes. Interruption effect: deals 15...51...60 damage; knocks down foes using attack skills."));
            Data.Add(new Skill("Symbolic Posture", Properties.Resources.Symbolic_Posture, Skill.Attributes.Fast_Casting, Skill.Professions.Mesmer, 1, "10E 20R - Stance. (5...17...20 seconds.) Your next signet recharges 20...68...80% faster."));

            Data.Add(new Skill("Toxic Chill", Properties.Resources.Toxic_Chill, Skill.Attributes.Death_Magic, Skill.Professions.Necromancer, 3, "5E 1C 5R - Elite Spell. Deals 15...63...75 cold damage. Inflicts Poisoned condition (10...22...25 seconds) if target foe is hexed or enchanted.", true));
            Data.Add(new Skill("Well of Silence", Properties.Resources.Well_of_Silence, Skill.Attributes.Curses, Skill.Professions.Necromancer, 3, "10E 1C 20R - Well Spell. (10...26...30 seconds.) Foes in this well cannot use shouts and chants and have -1...3...4 Health degeneration. Exploits a fresh corpse."));

            Data.Add(new Skill("Glowstone", Properties.Resources.Glowstone, Skill.Attributes.Earth_Magic, Skill.Professions.Elementalist, 4, "5E 3/4C 7R - Spell. Projectile: deals 5...41...50 earth damage. You gain 5 Energy plus 1 Energy for every 2 ranks of Energy Storage if target foe is Weakened."));
            Data.Add(new Skill("Mind Blast", Properties.Resources.Mind_Blast, Skill.Attributes.Fire_Magic, Skill.Professions.Elementalist, 3, "5E 1C 2R - Elite Spell. Deals 15...51...60 fire damage. You gain 1...7...8 Energy if you have more Energy than target foe.", true));
            Data.Add(new Skill("Elemental Flame", Properties.Resources.Elemental_Flame, Skill.Attributes.Fire_Magic, Skill.Professions.Elementalist, 2, "10E 1C 30R - Enchantment Spell. (10...30...35 seconds.) Inflicts Burning condition (1...4...5 second[s]) whenever you apply an Elemental hex to a target."));
            Data.Add(new Skill("Invoke Lightning", Properties.Resources.Invoke_Lightning, Skill.Attributes.Air_Magic, Skill.Professions.Elementalist, 4, "5O 10E 1C 6R - Elite Spell. Deals 10...74...90 lightning damage. Hits two foes near target. 25% armor penetration.", true));

            Data.Add(new Skill("Pensive Guardian", Properties.Resources.Pensive_Guardian, Skill.Attributes.Protection_Prayers, Skill.Professions.Monk, 1, "5E 1C 5R - Enchantment Spell. (5...10...11 seconds.) 50% chance to block enchanted foes."));
            Data.Add(new Skill("Scribe's Insight", Properties.Resources.Scribe_s_Insight, Skill.Attributes.Divine_Favor, Skill.Professions.Monk, 1, "5E 1/4C 20R - Elite Enchantment Spell. (10...30...35 seconds.) You gain 3 Energy whenever you use a signet.", true));
            Data.Add(new Skill("Holy Haste", Properties.Resources.Holy_Haste, Skill.Attributes.Divine_Favor, Skill.Professions.Monk, 1, "10E 1C 10R - Enchantment Spell. (1...48...60 second[s].) Your Healing Prayers spells cast 50% faster. Ends if you cast an enchantment."));
            Data.Add(new Skill("Glimmer of Light", Properties.Resources.Glimmer_of_Light, Skill.Attributes.Healing_Prayers, Skill.Professions.Monk, 3, "5E 1/4C 1R - Elite Spell. Heals for 10...94...115.", true));
            Data.Add(new Skill("Zealous Benediction", Properties.Resources.Zealous_Benediction, Skill.Attributes.Protection_Prayers, Skill.Professions.Monk, 2, "10E 3/4C 4R - Elite Spell. Heals for 30...150...180. You gain 7 Energy if target ally was below 50% Health.", true));
            Data.Add(new Skill("Defender's Zeal", Properties.Resources.Defender_s_Zeal, Skill.Attributes.Smiting_Prayers, Skill.Professions.Monk, 3, "5E 1C 5R - Elite Hex Spell. (5...21...25 seconds.) You gain 2 Energy whenever target foe hits with an attack.", true));
            Data.Add(new Skill("Signet of Mystic Wrath", Properties.Resources.Signet_of_Mystic_Wrath, Skill.Attributes.Smiting_Prayers, Skill.Professions.Monk, 2, "2C 20R - Signet. Deals 5...29...35 holy damage for each enchantment on you (maximum 100 holy damage)."));
            Data.Add(new Skill("Signet of Removal", Properties.Resources.Signet_of_Removal, Skill.Attributes.None, Skill.Professions.Monk, 1, "1C 5R - Elite Signet. Removes one hex and one condition. No effect unless target ally is enchanted.", true));
            Data.Add(new Skill("Dismiss Condition", Properties.Resources.Dismiss_Condition, Skill.Attributes.Protection_Prayers, Skill.Professions.Monk, 4, "5E 3/4C 3R - Spell. Removes one condition. Heals for 15...63...75 if target ally is enchanted."));
            Data.Add(new Skill("Divert Hexes", Properties.Resources.Divert_Hexes, Skill.Attributes.Protection_Prayers, Skill.Professions.Monk, 2, "10E 1C 5R - Elite Spell. Removes 1...3...3 hex[es]. For each hex removed, target ally loses one condition and gains 15...63...75 Health.", true));

            Data.Add(new Skill("Counterattack", Properties.Resources.Counterattack, Skill.Attributes.Strength, Skill.Professions.Warrior, 3, "5E 6R - Melee Attack. Deals +5...29...35 damage. You gain 2...5...6 Energy if target foe is attacking."));
            Data.Add(new Skill("Magehunter Strike", Properties.Resources.Magehunter_Strike, Skill.Attributes.Strength, Skill.Professions.Warrior, 3, "5E 1/2C 3R - Elite Melee Attack. Deals +5...17...20 damage. Unblockable if target foe is enchanted.", true));
            Data.Add(new Skill("Soldier's Strike", Properties.Resources.Soldier_s_Strike, Skill.Attributes.Tactics, Skill.Professions.Warrior, 3, "5E 4R - Melee Attack. Deals +10...34...40 damage. Unblockable if you are under the effects of a chant or shout."));
            Data.Add(new Skill("Decapitate", Properties.Resources.Decapitate, Skill.Attributes.Axe_Mastery, Skill.Professions.Warrior, 1, "8A - Elite Axe Attack. Deals +5...41...50 damage. Inflicts Deep Wound condition (5...17...20 seconds). Automatic critical hit. You lose all adrenaline and Energy.", true));
            Data.Add(new Skill("Magehunter's Smash", Properties.Resources.Magehunter_s_Smash, Skill.Attributes.Hammer_Mastery, Skill.Professions.Warrior, 3, "8A - Elite Hammer Attack. Causes knock-down. Unblockable if target foe is enchanted.", true));
            Data.Add(new Skill("Soldier's Stance", Properties.Resources.Soldier_s_Stance, Skill.Attributes.Tactics, Skill.Professions.Warrior, 2, "5E 15R - Elite Stance. (5...13...15 seconds.) You have a 75% chance to block. You attack 33% faster while under the effects of a chant or shout.", true));
            Data.Add(new Skill("Soldier's Defense", Properties.Resources.Soldier_s_Defense, Skill.Attributes.Tactics, Skill.Professions.Warrior, 2, "5E 15R - Stance. (1...5...6 second[s].) You have 75% chance to block while under the effects of a shout or chant. Block effect: gain 1 adrenaline."));
            Data.Add(new Skill("Frenzied Defense", Properties.Resources.Frenzied_Defense, Skill.Attributes.None, Skill.Professions.Warrior, 3, "5E 10R - Stance. (8 seconds.) You have 75% chance to block. You take double damage."));
            Data.Add(new Skill("Steady Stance", Properties.Resources.Steady_Stance, Skill.Attributes.Tactics, Skill.Professions.Warrior, 2, "5E 6R - Elite Stance. (10 seconds.) The next time you would be knocked-down, you gain 1...3...3 adrenaline and 1...6...7 Energy instead.", true));
            Data.Add(new Skill("Steelfang Slash", Properties.Resources.Steelfang_Slash, Skill.Attributes.Swordsmanship, Skill.Professions.Warrior, 2, "8A 1R - Sword Attack. Deals +1...25...31 damage. You gain 1...4...5 adrenaline if target foe is knocked down."));

            Data.Add(new Skill("Screaming Shot", Properties.Resources.Screaming_Shot, Skill.Attributes.Marksmanship, Skill.Professions.Ranger, 3, "10E 8R - Bow Attack. Deals +10...22...25 damage. Inflicts Bleeding condition (5...17...20 seconds) if target foe is within earshot."));
            Data.Add(new Skill("Keen Arrow", Properties.Resources.Keen_Arrow, Skill.Attributes.Marksmanship, Skill.Professions.Ranger, 4, "5E 6R - Bow Attack. Deals +5...17...20 damage. Deals +5...21...25 more damage if you land a critical hit."));
            Data.Add(new Skill("Rampage as One", Properties.Resources.Rampage_as_One, Skill.Attributes.Beast_Mastery, Skill.Professions.Ranger, 2, "25E 10R - Elite Skill. (3...13...15 seconds.) You and your pet attack 33% faster and move 25% faster. No effect unless your pet is alive.", true));
            Data.Add(new Skill("Forked Arrow", Properties.Resources.Forked_Arrow, Skill.Attributes.None, Skill.Professions.Ranger, 2, "10E 5R - Bow Attack. You shoot two arrows simultaneously. Shoot only one arrow if you are enchanted or hexed."));
            Data.Add(new Skill("Disrupting Accuracy", Properties.Resources.Disrupting_Accuracy, Skill.Attributes.Marksmanship, Skill.Professions.Ranger, 2, "5E 2C 12R - Preparation. (36 seconds.) Interrupts an action whenever your arrows critical."));
            Data.Add(new Skill("Expert's Dexterity", Properties.Resources.Expert_s_Dexterity, Skill.Attributes.Expertise, Skill.Professions.Ranger, 4, "5E 20R - Elite Stance. (1...16...20 second[s].) You attack 33% faster and you gain +2 to your Marksmanship attribute.", true));
            Data.Add(new Skill("Roaring Winds", Properties.Resources.Roaring_Winds, Skill.Attributes.Wilderness_Survival, Skill.Professions.Ranger, 2, "10E 5C 60R - Nature Ritual. Creates a level 1...8...10 spirit (30...54...60 second lifespan). Chants and shouts cost 1...4...5 more Energy for creatures in range."));
            Data.Add(new Skill("Magebane Shot", Properties.Resources.Magebane_Shot, Skill.Attributes.None, Skill.Professions.Ranger, 3, "10E 1/2C 5R - Elite Bow Attack. Interrupts an action. Interruption effect: an interrupted spell is disabled for +10 seconds. Unblockable.", true));
            Data.Add(new Skill("Natural Stride", Properties.Resources.Natural_Stride, Skill.Attributes.Wilderness_Survival, Skill.Professions.Ranger, 4, "5E 12R - Stance. (1...7...8 second[s].) You move 33% faster and have 50% chance to block. Ends if you become hexed or enchanted."));
            Data.Add(new Skill("Heket's Rampage", Properties.Resources.Heket_s_Rampage, Skill.Attributes.Beast_Mastery, Skill.Professions.Ranger, 1, "5E 10R - Stance. (5...10...11 seconds). You attack 33% faster. Ends if you use an attack skill."));
            Data.Add(new Skill("Smoke Trap", Properties.Resources.Smoke_Trap, Skill.Attributes.Wilderness_Survival, Skill.Professions.Ranger, 2, "10E 2C 20R - Elite Trap. (90 seconds.) Inflicts Blinded and Dazed conditions (5...9...10 seconds) to nearby foes. Easily interrupted.", true));
            Data.Add(new Skill("Infuriating Heat", Properties.Resources.Infuriating_Heat, Skill.Attributes.Expertise, Skill.Professions.Ranger, 2, "5E 3C 15R - Elite Nature Ritual. Creates a level 1...8...10 spirit (30...54...60 second lifespan). Doubles adrenaline gain for creatures in range.", true));

            Data.Add(new Skill("Vocal Was Sogolon", Properties.Resources.Vocal_Was_Sogolon, Skill.Attributes.Restoration_Magic, Skill.Professions.Ritualist, 2, "10E 1C 30R - Item Spell. (60 seconds.) Shouts and chants you use last 20...44...50% longer."));
            Data.Add(new Skill("Destructive Was Glaive", Properties.Resources.Destructive_Was_Glaive, Skill.Attributes.Channeling_Magic, Skill.Professions.Ritualist, 2, "5E 3/4C 5R - Elite Item Spell. (30...54...60 seconds.) Your Ritualist skills have 20% armor penetration. Drop effect: deals 15...71...85 lightning damage to all foes in the area.", true));
            Data.Add(new Skill("Wielder's Strike", Properties.Resources.Wielder_s_Strike, Skill.Attributes.Channeling_Magic, Skill.Professions.Ritualist, 2, "5E 1C 6R - Spell. Deals 5...41...50 lightning damage. Deals 10...34...40 more lightning damage if you are under a weapon spell."));
            Data.Add(new Skill("Gaze of Fury", Properties.Resources.Gaze_of_Fury, Skill.Attributes.Channeling_Magic, Skill.Professions.Ritualist, 2, "10E 3/4C 20R - Binding Ritual. Destroys a spirit. Creates a level 1...11...14 spirit (30...54...60 second lifespan). Its attacks deal 5...17...20 damage."));
            Data.Add(new Skill("Spirit's Strength", Properties.Resources.Spirit_s_Strength, Skill.Attributes.Spawning_Power, Skill.Professions.Ritualist, 2, "5E 1C 30R - Elite Enchantment Spell. (15...51...60 seconds.) Your attacks deal +5...29...35 damage if you are under a weapon spell.", true));
            Data.Add(new Skill("Wielder's Zeal", Properties.Resources.Wielder_s_Zeal, Skill.Attributes.Spawning_Power, Skill.Professions.Ritualist, 2, "10E 1C 10R - Elite Enchantment Spell. (10...26...30 seconds.) You gain 1...4...5 Energy whenever you cast a weapon spell.", true));
            Data.Add(new Skill("Sight Beyond Sight", Properties.Resources.Sight_Beyond_Sight, Skill.Attributes.Spawning_Power, Skill.Professions.Ritualist, 2, "5E 1/4C 15R - Enchantment Spell. (8...18...20 seconds). You are immune to Blindness."));
            Data.Add(new Skill("Renewing Memories", Properties.Resources.Renewing_Memories, Skill.Attributes.Spawning_Power, Skill.Professions.Ritualist, 2, "5E 1C 20R - Enchantment Spell. (5...17...20 seconds.) Your weapon and item spells cost 5...29...35% less Energy. No effect unless holding an item."));
            Data.Add(new Skill("Wielder's Remedy", Properties.Resources.Wielder_s_Remedy, Skill.Attributes.Spawning_Power, Skill.Professions.Ritualist, 3, "5E 1C 10R - Enchantment Spell. (10...26...30 seconds.) Whenever you cast a weapon spell, the targeted ally loses one condition."));
            Data.Add(new Skill("Ghostmirror Light", Properties.Resources.Ghostmirror_Light, Skill.Attributes.Restoration_Magic, Skill.Professions.Ritualist, 2, "5E 1C 3R - Spell. Heals for 15...75...90. You gain 15...75...90 Health if you are within earshot of a spirit. Cannot self-target."));
            Data.Add(new Skill("Signet of Ghostly Might", Properties.Resources.Signet_of_Ghostly_Might, Skill.Attributes.Communing, Skill.Professions.Ritualist, 2, "1C 15R - Elite Signet. (5...17...20 seconds.) All spirits you control within earshot attack 33% faster and deal +5...9...10 damage.", true));
            Data.Add(new Skill("Signet of Binding", Properties.Resources.Signet_of_Binding, Skill.Attributes.Spawning_Power, Skill.Professions.Ritualist, 2, "2C 15R - Signet. You lose 200...80...50 Health. Gain control of target enemy-controlled spirit. 50% failure chance unless Spawning Power 5 or more."));
            Data.Add(new Skill("Caretaker's Charge", Properties.Resources.Caretaker_s_Charge, Skill.Attributes.Channeling_Magic, Skill.Professions.Ritualist, 2, "5E 1C 4R - Elite Spell. Deals 20...64...75 lightning damage. You gain 5 Energy and 5...41...50 Health if you are holding an item.", true));
            Data.Add(new Skill("Anguish", Properties.Resources.Anguish, Skill.Attributes.Communing, Skill.Professions.Ritualist, 3, "15E 3/4C 45R - Binding Ritual. Creates a level 1...9...11 spirit (15...39...45 second lifespan). Its attacks deal 5...17...20 damage. Double damage to hexed foes."));
            Data.Add(new Skill("Empowerment", Properties.Resources.Empowerment, Skill.Attributes.Spawning_Power, Skill.Professions.Ritualist, 2, "5E 3/4C 30R - Binding Ritual. Creates a level 1...11...14 spirit (15...51...60 second lifespan). Allies in range holding an item gain 15...39...45 maximum Health and 10 maximum Energy."));
            Data.Add(new Skill("Recovery", Properties.Resources.Recovery, Skill.Attributes.Restoration_Magic, Skill.Professions.Ritualist, 2, "15E 3/4C 30R - Binding Ritual. Creates a level 1...11...14 spirit (30...54...60 second lifespan). Conditions on allies in range expire 20...44...50% faster."));
            Data.Add(new Skill("Weapon of Fury", Properties.Resources.Weapon_of_Fury, Skill.Attributes.Channeling_Magic, Skill.Professions.Ritualist, 3, "5E 1C 8R - Elite Weapon Spell. (5...17...20 seconds.) 5...41...50% more adrenaline gain and +1 Energy whenever target ally hits with an attack.", true));
            Data.Add(new Skill("Xinrae's Weapon", Properties.Resources.Xinrae_s_Weapon, Skill.Attributes.Restoration_Magic, Skill.Professions.Ritualist, 3, "5E 1/4C 3R - Elite Weapon Spell. (8 seconds). The next time target ally takes damage from a foe, that damage is limited to 5% of target ally's max Health and that ally steals 20...68...80 Health from that foe.", true));
            Data.Add(new Skill("Warmonger's Weapon", Properties.Resources.Warmonger_s_Weapon, Skill.Attributes.Channeling_Magic, Skill.Professions.Ritualist, 3, "10E 1C 30R - Weapon Spell. (3...11...13 seconds.) Attacks interrupt an action. Does not interrupt attacking foes."));
            Data.Add(new Skill("Weapon of Remedy", Properties.Resources.Weapon_of_Remedy, Skill.Attributes.Restoration_Magic, Skill.Professions.Ritualist, 3, "5E 1/4C 3R - Elite Weapon Spell. (8 seconds.) The next time target ally takes damage or life steal from a foe, this ally steals 15...63...75 Health from that foe and loses 1 condition.", true));

            Data.Add(new Skill("Rending Sweep", Properties.Resources.Rending_Sweep, Skill.Attributes.Scythe_Mastery, Skill.Professions.Dervish, 3, "6A - Scythe Attack. Deals +5...17...20 damage. You lose 1 Dervish enchantment. Removal effect: struck foes lose an enchantment."));
            Data.Add(new Skill("Onslaught", Properties.Resources.Onslaught, Skill.Attributes.Wind_Prayers, Skill.Professions.Dervish, 5, "10E 10R - Elite Flash Enchantment Spell. (3...13...15 seconds.) You attack, move and gain adrenaline 25% faster.", true));
            Data.Add(new Skill("Mystic Corruption", Properties.Resources.Mystic_Corruption, Skill.Attributes.Mysticism, Skill.Professions.Dervish, 2, "10E 10R - Flash Enchantment Spell. (20 seconds.) Initial Effect: all adjacent foes are Diseased (1...2...2 second[s].) Double duration if you are already enchanted. End Effect: party members in earshot lose Disease."));
            Data.Add(new Skill("Grenth's Grasp", Properties.Resources.Grenth_s_Grasp, Skill.Attributes.Wind_Prayers, Skill.Professions.Dervish, 2, "5E 10R - Elite Flash Enchantment Spell. (20 seconds.) Inflicts Crippled condition (1...9...11 second[s]) with your attack skills and transfer 1 condition. No effect unless wielding a cold weapon.", true));
            Data.Add(new Skill("Veil of Thorns", Properties.Resources.Veil_of_Thorns, Skill.Attributes.Earth_Prayers, Skill.Professions.Dervish, 4, "10E 15R - Flash Enchantment Spell. (5...21...25 seconds.) Spell damage is reduced by 5...29...35%. Initial Effect: nearby foes are struck for 5...41...50 piercing damage."));
            Data.Add(new Skill("Harrier's Grasp", Properties.Resources.Harrier_s_Grasp, Skill.Attributes.Wind_Prayers, Skill.Professions.Dervish, 2, "10E 15R - Flash Enchantment Spell. (5...17...20 seconds.) Attacks on moving foes also Cripple them. Ends after you apply Cripple 1...3...3 times. Initial Effect: you lose Cripple and 1 other condition."));
            Data.Add(new Skill("Vow of Strength", Properties.Resources.Vow_of_Strength, Skill.Attributes.Earth_Prayers, Skill.Professions.Dervish, 4, "5E 1/4C 20R - Elite Enchantment Spell. (15 seconds.) When you attack with a scythe, deals 10...22...25 slashing damage to adjacent foes.", true));
            Data.Add(new Skill("Ebon Dust Aura", Properties.Resources.Ebon_Dust_Aura, Skill.Attributes.Earth_Prayers, Skill.Professions.Dervish, 3, "10E 20R - Elite Flash Enchantment Spell. (30 seconds.) Deal +3...13...15 earth damage with your melee attacks. Initial Effect: Blinds nearby foes for 1...6...7 second[s]. End Effect: removes Blindness. No effect unless wielding an earth weapon.", true));
            Data.Add(new Skill("Zealous Vow", Properties.Resources.Zealous_Vow, Skill.Attributes.Wind_Prayers, Skill.Professions.Dervish, 2, "5E 1/4C 12R - Elite Enchantment Spell. (20 seconds.) You gain 1...5...6 Energy each time you hit with an attack. You have -3 Energy regeneration.", true));
            Data.Add(new Skill("Heart of Fury", Properties.Resources.Heart_of_Fury, Skill.Attributes.Mysticism, Skill.Professions.Dervish, 4, "4A - Stance. (2...8...10 seconds.) You attack 25% faster."));
            Data.Add(new Skill("Zealous Renewal", Properties.Resources.Zealous_Renewal, Skill.Attributes.Mysticism, Skill.Professions.Dervish, 4, "5E 10R - Flash Enchantment Spell. (5...21...25 seconds.) Initial effect: deals 5...25...30 holy damage to nearby foes. You have -1 Energy regeneration and gain 1 Energy when you hit. Gain 1...4...5 Energy if this enchantment ends early."));
            Data.Add(new Skill("Attacker's Insight", Properties.Resources.Attacker_s_Insight, Skill.Attributes.Wind_Prayers, Skill.Professions.Dervish, 3, "10E 10R - Flash Enchantment Spell. (3...9...10 seconds.) Gives 50% block chance while attacking. End Effect: Causes Weakness 3...13...15 seconds to adjacent foes."));
            Data.Add(new Skill("Rending Aura", Properties.Resources.Rending_Aura, Skill.Attributes.Wind_Prayers, Skill.Professions.Dervish, 4, "10E 6R - Flash Enchantment Spell. (30 seconds.) Your attack skills remove enchantments from knocked-down foes. Initial effect: deals 10...34...40 cold damage to all nearby foes. End effect: nearby foes have Cracked Armor for 1...8...10 second[s]."));
            Data.Add(new Skill("Featherfoot Grace", Properties.Resources.Featherfoot_Grace, Skill.Attributes.Wind_Prayers, Skill.Professions.Dervish, 3, "10E 1/4C 15R - Enchantment Spell. (5...17...20 seconds.) You move 25% faster, and conditions expire 25% faster."));
            Data.Add(new Skill("Reaper's Sweep", Properties.Resources.Reaper_s_Sweep, Skill.Attributes.Scythe_Mastery, Skill.Professions.Dervish, 2, "8A - Elite Scythe Attack. (3...13...15 seconds.) Cause Cripple. Lose 1 Dervish enchantment. Removal Effect: cause knockdown for 2...3...3 seconds.", true));
            Data.Add(new Skill("Harrier's Haste", Properties.Resources.Harrier_s_Haste, Skill.Attributes.Wind_Prayers, Skill.Professions.Dervish, 3, "10E 10R - Flash Enchantment Spell. (2...7...8 seconds.) You move 25% faster and deal +3...10...12 damage to moving foes."));

            Data.Add(new Skill("Focused Anger", Properties.Resources.Focused_Anger, Skill.Attributes.Leadership, Skill.Professions.Paragon, 3, "10E 60R - Elite Skill. (45 seconds.) You gain 0...120...150% more adrenaline.", true));
            Data.Add(new Skill("Natural Temper", Properties.Resources.Natural_Temper, Skill.Attributes.Leadership, Skill.Professions.Paragon, 1, "3A - Skill. (4...9...10 seconds.) You gain 33% more adrenaline. No effect if you are enchanted."));
            Data.Add(new Skill("Song of Restoration", Properties.Resources.Song_of_Restoration, Skill.Attributes.Motivation, Skill.Professions.Paragon, 3, "10E 1C 20R - Elite Chant. (10 seconds.) Party members in earshot gain 45...97...110 Health with their next skill.", true));
            Data.Add(new Skill("Lyric of Purification", Properties.Resources.Lyric_of_Purification, Skill.Attributes.Motivation, Skill.Professions.Paragon, 1, "5E 1C 20R - Chant. (5...17...20 seconds.) Allies in earshot lose one condition with their next signet."));
            Data.Add(new Skill("Soldier's Fury", Properties.Resources.Soldier_s_Fury, Skill.Attributes.Leadership, Skill.Professions.Paragon, 2, "5E 1C 5R - Elite Echo. (10...30...35 seconds.) You attack 33% faster and gain 33% more adrenaline if under the effects of a shout or chant. You have -20 armor.", true));
            Data.Add(new Skill("Aggressive Refrain", Properties.Resources.Aggressive_Refrain, Skill.Attributes.Leadership, Skill.Professions.Paragon, 3, "25E 2C 20R - Echo. (5...21...25 seconds.) You attack 25% faster. Renewal: whenever a chant or shout ends on you. You have -20 armor."));
            Data.Add(new Skill("Energizing Finale", Properties.Resources.Energizing_Finale, Skill.Attributes.Motivation, Skill.Professions.Paragon, 2, "10E 1C 5R - Echo. (10...30...35 seconds.) Target ally gains 1 Energy whenever a shout or chant ends on that ally. Cannot target spirits."));
            Data.Add(new Skill("Signet of Aggression", Properties.Resources.Signet_of_Aggression, Skill.Attributes.None, Skill.Professions.Paragon, 2, "1C 5R - Signet. You gain 2 adrenaline if you are under the effects of a shout or chant."));
            Data.Add(new Skill("Remedy Signet", Properties.Resources.Remedy_Signet, Skill.Attributes.None, Skill.Professions.Paragon, 3, "1C 4R - Signet. You lose one condition."));
            Data.Add(new Skill("Signet of Return", Properties.Resources.Signet_of_Return, Skill.Attributes.Leadership, Skill.Professions.Paragon, 4, "5C 5R - Signet. Resurrects target party member (5...13...15% Health and 1...3...4% Energy for each party member in earshot)."));
            Data.Add(new Skill("\"Make Your Time!\"", Properties.Resources._Make_Your_Time__, Skill.Attributes.Leadership, Skill.Professions.Paragon, 2, "10E 30R - Shout. You gain one adrenaline (maximum 1...4...5) for each party member in earshot."));
            Data.Add(new Skill("\"Can't Touch This!\"", Properties.Resources._Can_t_Touch_This__, Skill.Attributes.Command, Skill.Professions.Paragon, 2, "5E 20R - Shout. (20 seconds.) The next 1...4...5 touch-range skill[s] used against allies within earshot fail[s]."));
            Data.Add(new Skill("\"Find Their Weakness!\"", Properties.Resources._Find_Their_Weakness__, Skill.Attributes.Command, Skill.Professions.Paragon, 4, "10E 15R - Shout. (5...17...20 seconds.) Target ally deals +5...41...50 damage and inflicts Deep Wound condition (5...17...20 seconds) with the next attack."));
            Data.Add(new Skill("\"The Power Is Yours!\"", Properties.Resources._The_Power_Is_Yours__, Skill.Attributes.Motivation, Skill.Professions.Paragon, 3, "4A - Elite Shout. (3 seconds.) Allies within earshot gain 0...1...1 Energy regeneration.", true));
            Data.Add(new Skill("Slayer's Spear", Properties.Resources.Slayer_s_Spear, Skill.Attributes.Spear_Mastery, Skill.Professions.Paragon, 2, "10E 4R - Spear Attack. Deals +5...21...25 damage. Inflicts Deep Wound condition (5...17...20 seconds) if target has more Health than you."));
            Data.Add(new Skill("Swift Javelin", Properties.Resources.Swift_Javelin, Skill.Attributes.Spear_Mastery, Skill.Professions.Paragon, 2, "5E 10R - Spear Attack. Deals +5...17...20 damage. This spear moves twice as fast and is unblockable if you are enchanted."));
            // Lightbringer skills:
            Data.Add(new Skill("Lightbringer's Gaze", Properties.Resources.Lightbringer_s_Gaze, Skill.Attributes.PvE_Only, Skill.Professions.None, 2, "5E 1C 15R - Skill. Target demonic servant of Abaddon takes 100 holy damage and is interrupted. Hits one additional foe in the area for each rank of Lightbringer you have attained. This skill is disabled (15 seconds)."));
            Data.Add(new Skill("Lightbringer Signet", Properties.Resources.Lightbringer_Signet, Skill.Attributes.PvE_Only, Skill.Professions.None, 2, "1/4C 25R - Signet. You gain 4...5 strikes of adrenaline and 22...24 Energy if you are within the area of a demonic servant of Abaddon."));
            // Raondomly placed signet...:
            Data.Add(new Skill("Sunspear Rebirth Signet", Properties.Resources.Sunspear_Rebirth_Signet, Skill.Attributes.PvE_Only, Skill.Professions.None, 2, "3C - Signet. Resurrect target dead party member at your location (full Health and 10% Energy for each Sunspear Rank attained). This signet only recharges when you gain a morale boost."));
            // Kurz/Lux title track skills:
            Data.Add(new Skill("Shadow Sanctuary (Luxon)", Properties.Resources.Shadow_Sanctuary_Luxon, Skill.Attributes.PvE_Only, Skill.Professions.Assassin, 2, "5E 1/4C 30R - Enchantment Spell. (10 seconds.) You gain +7...10 Health regeneration and +40 armor. You are Blind (5 seconds)."));
            Data.Add(new Skill("Ether Nightmare (Luxon)", Properties.Resources.Ether_Nightmare_Luxon, Skill.Attributes.PvE_Only, Skill.Professions.Mesmer, 3, "10E 3C 25R - Hex spell. Causes 5...8 Energy loss. Target and foes in the area have -1 Health degeneration for each point of Energy lost (10 seconds)."));
            Data.Add(new Skill("Signet of Corruption (Luxon)", Properties.Resources.Signet_of_Corruption_Luxon, Skill.Attributes.PvE_Only, Skill.Professions.Necromancer, 2, "1C 20R - Signet. Deals 20...30 damage to target and nearby foes. You gain 2 Energy (maximum 12...20) for each of these foes with a condition or hex."));
            Data.Add(new Skill("Elemental Lord (Luxon)", Properties.Resources.Elemental_Lord_Luxon, Skill.Attributes.PvE_Only, Skill.Professions.Elementalist, 4, "5E 1/4C 45R - Enchantment Spell. (40...60 seconds.) Your elemental attributes are boosted by 1. Each time you cast a spell, you gain 1 energy for every 10 ranks of Energy Storage and are healed for 100...300% of the spell's Energy cost."));
            Data.Add(new Skill("Selfless Spirit (Luxon)", Properties.Resources.Selfless_Spirit_Luxon, Skill.Attributes.PvE_Only, Skill.Professions.Monk, 3, "5E 1/4C 45R - Enchantment Spell. (15...20 seconds.) Spells you cast that target another ally cost 3 less Energy."));
            Data.Add(new Skill("Triple Shot (Luxon)", Properties.Resources.Triple_Shot_Luxon, Skill.Attributes.PvE_Only, Skill.Professions.Ranger, 2, "10E 10R - Bow Attack. Shoot 3 arrows simultaneously at target foe. These arrows deal 40...25% less damage"));
            Data.Add(new Skill("\"Save Yourselves!\" (Luxon)", Properties.Resources._Save_Yourselves__Luxon, Skill.Attributes.PvE_Only, Skill.Professions.Warrior, 5, "8A - Shout. (4...6 seconds.) All other party members gain +100 armor."));
            Data.Add(new Skill("Aura of Holy Might (Luxon)", Properties.Resources.Aura_of_Holy_Might_Luxon, Skill.Attributes.PvE_Only, Skill.Professions.Dervish, 4, "10E 3/4C 30R - Enchantment Spell. (45 seconds.) Whenever you lose a Dervish enchantment, adjacent foes take 20...25 holy damage."));
            Data.Add(new Skill("Spear of Fury (Luxon)", Properties.Resources.Spear_of_Fury_Luxon, Skill.Attributes.PvE_Only, Skill.Professions.Paragon, 5, "5E 1C 8R - Spear Attack. Deals +30...40 damage. Gain 3...6 strikes of adrenaline if you hit a foe with a condition."));
            // Some eye of the north skills:
            Data.Add(new Skill("Vampiric Assault", Properties.Resources.Vampiric_Assault, Skill.Attributes.Deadly_Arts, Skill.Professions.Assassin, 3, "5E 8R - Dual Attack. Steals 10...34...40 Health if this attack hits. Must follow an off-hand attack."));
            Data.Add(new Skill("Lotus Strike", Properties.Resources.Lotus_Strike, Skill.Attributes.Dagger_Mastery, Skill.Professions.Assassin, 3, "10E 12R - Off-Hand Attack. Deals +10...22...25 damage; you gain 5...17...20 Energy. Must follow a lead attack."));
            Data.Add(new Skill("Golden Fang Strike", Properties.Resources.Golden_Fang_Strike, Skill.Attributes.Dagger_Mastery, Skill.Professions.Assassin, 3, "5E 4R - Off-Hand Attack. Inflicts Deep Wound condition (5...17...20 seconds) if you are enchanted. Must follow a lead attack."));
            Data.Add(new Skill("Falling Lotus Strike", Properties.Resources.Falling_Lotus_Strike, Skill.Attributes.Dagger_Mastery, Skill.Professions.Assassin, 2, "5E 12R - Off-Hand Attack. Deals +15...31...35 damage; you gain 1...10...12 Energy. No effect unless target foe is knocked-down."));
            Data.Add(new Skill("Sadist's Signet", Properties.Resources.Sadist_s_Signet, Skill.Attributes.Deadly_Arts, Skill.Professions.Assassin, 1, "1C 8R - Signet. You gain 10...34...40 Health for each condition on target foe."));

            Data.Add(new Skill("Signet of Distraction", Properties.Resources.Signet_of_Distraction, Skill.Attributes.Domination_Magic, Skill.Professions.Mesmer, 2, "1/4C 15R - Signet. Interrupts a spell. Interruption effect: target foe's spell is disabled for 1...4...5 seconds for each signet you have equipped."));
            Data.Add(new Skill("Signet of Recall", Properties.Resources.Signet_of_Recall, Skill.Attributes.Inspiration_Magic, Skill.Professions.Mesmer, 2, "1C 20R - Signet. (10 seconds.) You have -4 Energy regeneration. End effect: you gain 13...19...20 Energy."));
            Data.Add(new Skill("Power Lock", Properties.Resources.Power_Lock, Skill.Attributes.Domination_Magic, Skill.Professions.Mesmer, 3, "5E 1/4C 12R - Spell. Interrupts a spell or chant. Interruption effect: interrupted spell or chant is disabled for +5...11...13 seconds."));
            Data.Add(new Skill("Waste Not, Want Not", Properties.Resources.Waste_Not__Want_Not, Skill.Attributes.Inspiration_Magic, Skill.Professions.Mesmer, 3, "5E 1/4C 15R - Spell. You gain 8...12...13 Energy if target foe is not attacking or casting a spell."));
            Data.Add(new Skill("Sum of All Fears", Properties.Resources.Sum_of_All_Fears, Skill.Attributes.Illusion_Magic, Skill.Professions.Mesmer, 3, "10E 2C 10R - Hex Spell. (1...8...10 second[s].) Target foe moves, attacks, and casts spells 33% slower."));

            Data.Add(new Skill("Withering Aura", Properties.Resources.Withering_Aura, Skill.Attributes.Death_Magic, Skill.Professions.Necromancer, 2, "5E 1C 3R - Enchantment Spell. (5...17...20 seconds.) Target ally's melee attacks cause Weakness condition (5...17...20 seconds.)"));
            Data.Add(new Skill("Cacophony", Properties.Resources.Cacophony, Skill.Attributes.Curses, Skill.Professions.Necromancer, 1, "10E 2C 15R - Hex Spell. (10 seconds.) Deals 35...91...105 damage whenever target foe uses a shout or chant."));

            Data.Add(new Skill("Winter's Embrace", Properties.Resources.Winter_s_Embrace, Skill.Attributes.Water_Magic, Skill.Professions.Elementalist, 3, "10E 3/4C 15R - Hex Spell. (2...5...6 seconds.) Target foe moves 66% slower and takes 5...13...15 damage while moving."));
            Data.Add(new Skill("Earthen Shackles", Properties.Resources.Earthen_Shackles, Skill.Attributes.Earth_Magic, Skill.Professions.Elementalist, 4, "10E 1C 15R - Hex Spell. (3 seconds.) Target and nearby foes move 90% slower. Applies Weakness for 5...17...20 seconds when it ends."));
            Data.Add(new Skill("Ward of Weakness", Properties.Resources.Ward_of_Weakness, Skill.Attributes.Earth_Magic, Skill.Professions.Elementalist, 3, "10E 1C 20R - Ward Spell. (5...17...20 seconds). Inflicts Weakened condition (5...17...20 seconds) to any foes that take elemental damage in this ward."));
            Data.Add(new Skill("Glyph of Swiftness", Properties.Resources.Glyph_of_Swiftness, Skill.Attributes.Air_Magic, Skill.Professions.Elementalist, 2, "5E 1C 10R - Glyph. (15 seconds.) Your next 1...4...5 spell[s] recharge 25% faster and projectiles from them move 200% faster."));

            Data.Add(new Skill("Cure Hex", Properties.Resources.Cure_Hex, Skill.Attributes.Healing_Prayers, Skill.Professions.Monk, 4, "5E 1C 12R - Spell. Removes a Hex. Removal effect: Heals for 30...102...120."));
            Data.Add(new Skill("Smite Condition", Properties.Resources.Smite_Condition, Skill.Attributes.Smiting_Prayers, Skill.Professions.Monk, 4, "5E 1C 7R - Spell. Removes a condition. Removal effect: deals 10...50...60 holy damage to foes in the area of target ally."));
            Data.Add(new Skill("Smiter's Boon", Properties.Resources.Smiter_s_Boon, Skill.Attributes.Divine_Favor, Skill.Professions.Monk, 2, "5E 1/4C 10R - Enchantment Spell. (30 seconds.) Your Smiting Prayers have double Divine Favor healing bonus."));
            Data.Add(new Skill("Castigation Signet", Properties.Resources.Castigation_Signet, Skill.Attributes.Smiting_Prayers, Skill.Professions.Monk, 4, "1C 20R - Signet. Deals 26...50...56 holy damage. You gain 1...8...10 Energy if target foe is attacking."));
            Data.Add(new Skill("Purifying Veil", Properties.Resources.Purifying_Veil, Skill.Attributes.Protection_Prayers, Skill.Professions.Monk, 2, "-1ER 5E 1C 6R - Enchantment Spell. Conditions expire 5...41...50% faster on target ally. End effect: removes a condition."));

            Data.Add(new Skill("Pulverizing Smash", Properties.Resources.Pulverizing_Smash, Skill.Attributes.Hammer_Mastery, Skill.Professions.Warrior, 1, "4A - Hammer Attack. Inflicts Weakness and Deep Wound conditions (5...17...20 seconds) if target foe is knocked-down."));
            Data.Add(new Skill("Keen Chop", Properties.Resources.Keen_Chop, Skill.Attributes.Axe_Mastery, Skill.Professions.Warrior, 2, "3A - Axe Attack. Always a critical hit."));
            Data.Add(new Skill("Knee Cutter", Properties.Resources.Knee_Cutter, Skill.Attributes.Swordsmanship, Skill.Professions.Warrior, 2, "5A 1R - Sword Attack. You gain 2...6...7 Energy and 1...3...3 adrenaline if target foe is Crippled."));
            Data.Add(new Skill("Grapple", Properties.Resources.Grapple, Skill.Attributes.None, Skill.Professions.Warrior, 2, "5E 3/4C 12R - Touch Skill. Causes knockdown. You are also knocked down. Your current stance ends."));

            Data.Add(new Skill("Radiant Scythe", Properties.Resources.Radiant_Scythe, Skill.Attributes.Scythe_Mastery, Skill.Professions.Dervish, 4, "6A - Scythe Attack. Deals +1 damage (maximum 5...25...30) for each point of Energy you have. Gain 1...6...7 Energy."));
            Data.Add(new Skill("Grenth's Aura", Properties.Resources.Grenth_s_Aura, Skill.Attributes.Wind_Prayers, Skill.Professions.Dervish, 4, "10E 10R - Flash Enchantment Spell. (20 seconds.) You deal 5...21...25 less damage and steal 5...21...25 Health when you hit with a scythe. Initial effect: steal 5...21...25 Health from all adjacent foes."));
            Data.Add(new Skill("Signet of Pious Restraint", Properties.Resources.Signet_of_Pious_Restraint, Skill.Attributes.Wind_Prayers, Skill.Professions.Dervish, 3, "1C 20R - Signet. Inflicts Crippled condition (5...13...15 seconds). Remove one of your Dervish enchantments. Removal effect: also causes Cripple to foes nearby your target and recharges 75% faster."));
            Data.Add(new Skill("Farmer's Scythe", Properties.Resources.Farmer_s_Scythe, Skill.Attributes.Scythe_Mastery, Skill.Professions.Dervish, 3, "5E 12R - Scythe Attack. Deals +5...17...20 damage. Instant recharge if you hit more than one foe."));

            Data.Add(new Skill("Energetic Was Lee Sa", Properties.Resources.Energetic_Was_Lee_Sa, Skill.Attributes.Spawning_Power, Skill.Professions.Ritualist, 2, "10E 2C 20R - Item Spell. (5...13...15 seconds.) You have +2 Energy regeneration. Drop effect: you gain +1...8...10 Energy."));

            Data.Add(new Skill("Anthem of Weariness", Properties.Resources.Anthem_of_Weariness, Skill.Attributes.Command, Skill.Professions.Paragon, 1, "5E 1C 10R - Chant. (8 seconds.) Allies in earshot inflict Weakness (1...13...16 second[s]) with their next attack skill."));
            Data.Add(new Skill("Anthem of Disruption", Properties.Resources.Anthem_of_Disruption, Skill.Attributes.Command, Skill.Professions.Paragon, 3, "10E 1C 15R - Chant. (1...8...10 seconds.) Allies in earshot interrupt an action with their next attack skill."));

            // Random assortment of skills:
            Data.Add(new Skill("Summon Spirits (Luxon)", Properties.Resources.Summon_Spirits_Luxon, Skill.Attributes.PvE_Only, Skill.Professions.Ritualist, 4, "5E 1/4C 5R - Spell. Spirits you control Shadow Step to your location and gain 60...100 Health."));
            Data.Add(new Skill("Shadow Fang", Properties.Resources.Shadow_Fang, Skill.Attributes.Deadly_Arts, Skill.Professions.Assassin, 2, "10E 1/4C 45R - Hex Spell. Shadow Step to target foe. End effect after 10 seconds: inflicts Deep Wound condition (5...17...20 seconds); you return to your original location."));

            Data.Add(new Skill("Calculated Risk", Properties.Resources.Calculated_Risk, Skill.Attributes.Illusion_Magic, Skill.Professions.Mesmer, 3, "5E 1C 7R - Hex Spell. Target foe does +10 damage with attacks (3...20...24 seconds). There is a 50% chance that the damage from each attack (maximum 15...83...100) will be done to that foe instead."));
            Data.Add(new Skill("Shrinking Armor", Properties.Resources.Shrinking_Armor, Skill.Attributes.Illusion_Magic, Skill.Professions.Mesmer, 2, "5E 1C 8R - Hex Spell. (10 seconds.) Causes -1...3...4 Health degeneration. End effect: inflicts Cracked Armor condition (5...17...20 seconds)."));
            Data.Add(new Skill("Aneurysm", Properties.Resources.Aneurysm, Skill.Attributes.Domination_Magic, Skill.Professions.Mesmer, 3, "5E 1C 5R - Spell. Target foe regains all Energy. For each point of Energy gained, target takes 1...3...3 damage and all adjacent foes lose 1 Energy (maximum 1...24...30)."));
            Data.Add(new Skill("Wandering Eye", Properties.Resources.Wandering_Eye, Skill.Attributes.Illusion_Magic, Skill.Professions.Mesmer, 5, "5E 2C 12R - Hex Spell. (4 seconds.) Interrupts target foe's next attack. Interruption effect: 30...94...110 damage to nearby foes."));

            Data.Add(new Skill("Foul Feast", Properties.Resources.Foul_Feast, Skill.Attributes.Soul_Reaping, Skill.Professions.Necromancer, 2, "5E 1/4C 4R - Spell. Transfers all conditions from target ally to yourself. You gain 0...36...45 Health and 0...2...2 Energy for each condition transferred. Half recharge if you remove Disease. Cannot self-target."));
            Data.Add(new Skill("Putrid Bile", Properties.Resources.Putrid_Bile, Skill.Attributes.Death_Magic, Skill.Professions.Necromancer, 4, "10E 1C 12R - Hex Spell. (5...17...20 seconds.) Causes -1...3...3 Health degeneration. Deals 25...73...85 damage to all nearby foes if target foe dies."));

            Data.Add(new Skill("Shell Shock", Properties.Resources.Shell_Shock, Skill.Attributes.Air_Magic, Skill.Professions.Elementalist, 2, "5E 1C 8R - Spell. Deals 10...26...30 lightning damage. Inflicts Cracked Armor condition (5...17...20 seconds). 25% armor penetration. If Overcast, strikes adjacent."));
            Data.Add(new Skill("Glyph of Immolation", Properties.Resources.Glyph_of_Immolation, Skill.Attributes.Fire_Magic, Skill.Professions.Elementalist, 3, "5E 1C 10R - Glyph. (15 seconds.) Your next 1...3...4 spell[s] that target[s] a foe also inflict[s] Burning condition (1...3...4 second[s])."));

            Data.Add(new Skill("Patient Spirit", Properties.Resources.Patient_Spirit, Skill.Attributes.Healing_Prayers, Skill.Professions.Monk, 4, "5E 1/4C 4R - Enchantment Spell. (2 seconds.) End effect: heals for 30...102...120. No effect if ends early."));
            Data.Add(new Skill("Healing Ribbon", Properties.Resources.Healing_Ribbon, Skill.Attributes.Healing_Prayers, Skill.Professions.Monk, 2, "10E 1C 5R - Spell. Heals for 20...92...110. Heals two additional allies near target ally for 10...82...100. Cannot self-target."));
            Data.Add(new Skill("Aura of Stability", Properties.Resources.Aura_of_Stability, Skill.Attributes.Protection_Prayers, Skill.Professions.Monk, 2, "5E 1/4C 12R - Enchantment Spell. (3...7...8 seconds.) Target ally cannot be knocked-down. Cannot self-target."));
            Data.Add(new Skill("Spotless Mind", Properties.Resources.Spotless_Mind, Skill.Attributes.Healing_Prayers, Skill.Professions.Monk, 2, "5E 1/4C 12R - Enchantment Spell. (1...12...15 seconds.) Removes a hex every 5 seconds. Cannot self-target."));
            Data.Add(new Skill("Spotless Soul", Properties.Resources.Spotless_Soul, Skill.Attributes.Healing_Prayers, Skill.Professions.Monk, 3, "5E 1/4C 12R - Enchantment Spell. (1...12...15 seconds.) Removes a condition every 3 seconds. Cannot self-target."));

            Data.Add(new Skill("Disarm", Properties.Resources.Disarm, Skill.Attributes.Strength, Skill.Professions.Warrior, 1, "5A 1/2C 20R - Sword Attack. Interrupts target foe. Interruption effect: disables this foe's attack skills (0...2...3 second[s]) if that action was an attack."));
            Data.Add(new Skill("\"I Meant to Do That!\"", Properties.Resources._I_Meant_to_Do_That__, Skill.Attributes.Strength, Skill.Professions.Warrior, 2, "5E 8R - Shout. You gain 1...4...5 strikes of adrenaline if you are knocked-down."));

            Data.Add(new Skill("Rapid Fire", Properties.Resources.Rapid_Fire, Skill.Attributes.Marksmanship, Skill.Professions.Ranger, 3, "5E 2C 12R - Preparation. (5...21...25 seconds.) You attack 33% faster while wielding a bow."));
            Data.Add(new Skill("Sloth Hunter's Shot", Properties.Resources.Sloth_Hunter_s_Shot, Skill.Attributes.Marksmanship, Skill.Professions.Ranger, 4, "10E 8R - Bow Attack. Deals +10...22...25 damage. Deals +10...30...35 more damage if target foe is not using a skill."));

            Data.Add(new Skill("Aura Slicer", Properties.Resources.Aura_Slicer, Skill.Attributes.Mysticism, Skill.Professions.Dervish, 3, "4A - Melee Attack. (5...13...15 seconds.) Inflicts Bleeding condition. Also inflicts Cracked Armor (1...8...10 second[s]) if you are enchanted."));
            Data.Add(new Skill("Zealous Sweep", Properties.Resources.Zealous_Sweep, Skill.Attributes.Scythe_Mastery, Skill.Professions.Dervish, 3, "5E 10R - Scythe Attack. Deals +5...17...20 damage. You gain 3 Energy and 1 adrenaline for each foe you hit."));

            Data.Add(new Skill("Pure Was Li Ming", Properties.Resources.Pure_Was_Li_Ming, Skill.Attributes.Restoration_Magic, Skill.Professions.Ritualist, 2, "10E 1C 20R - Item Spell. (5...17...20 seconds.) Conditions on you expire 10...42...50% faster. Drop effect: removes 1...3...4 condition[s] from allies in earshot."));
            Data.Add(new Skill("Weapon of Aggression", Properties.Resources.Weapon_of_Aggression, Skill.Attributes.Channeling_Magic, Skill.Professions.Ritualist, 2, "10E 1/4C 10R - Weapon Spell. (5...13...15 seconds.) You attack 25% faster."));

            Data.Add(new Skill("Chest Thumper", Properties.Resources.Chest_Thumper, Skill.Attributes.Spear_Mastery, Skill.Professions.Paragon, 2, "5E 5R - Spear Attack. Inflicts Deep Wound condition (5...17...20 seconds) if target foe has Cracked Armor."));
            Data.Add(new Skill("Hasty Refrain", Properties.Resources.Hasty_Refrain, Skill.Attributes.Leadership, Skill.Professions.Paragon, 3, "5E 1C 10R - Echo. (3...9...11 seconds.) Target ally moves 25% faster. Renewal: every time a chant or shout ends on this ally."));
            // Here they are again...probably one set is kurz, the other lux:
            Data.Add(new Skill("Shadow Sanctuary (Kurzick)", Properties.Resources.Shadow_Sanctuary, Skill.Attributes.PvE_Only, Skill.Professions.Assassin, 2, "5E 1/4C 30R - Enchantment Spell. (10 seconds.) You gain +7...10 Health regeneration and +40 armor. You are Blind (5 seconds)."));
            Data.Add(new Skill("Ether Nightmare (Kurzick)", Properties.Resources.Ether_Nightmare, Skill.Attributes.PvE_Only, Skill.Professions.Mesmer, 3, "10E 3C 25R - Hex spell. Causes 5...8 Energy loss. Target and foes in the area have -1 Health degeneration for each point of Energy lost (10 seconds)."));
            Data.Add(new Skill("Signet of Corruption (Kurzick)", Properties.Resources.Signet_of_Corruption, Skill.Attributes.PvE_Only, Skill.Professions.Necromancer, 2, "1C 20R - Signet. Deals 20...30 damage to target and nearby foes. You gain 2 Energy (maximum 12...20) for each of these foes with a condition or hex."));
            Data.Add(new Skill("Elemental Lord (Kurzick)", Properties.Resources.Elemental_Lord, Skill.Attributes.PvE_Only, Skill.Professions.Elementalist, 4, "5E 1/4C 45R - Enchantment Spell. (40...60 seconds.) Your elemental attributes are boosted by 1. Each time you cast a spell, you gain 1 energy for every 10 ranks of Energy Storage and are healed for 100...300% of the spell's Energy cost."));
            Data.Add(new Skill("Selfless Spirit (Kurzick)", Properties.Resources.Selfless_Spirit, Skill.Attributes.PvE_Only, Skill.Professions.Monk, 3, "5E 1/4C 45R - Enchantment Spell. (15...20 seconds.) Spells you cast that target another ally cost 3 less Energy."));
            Data.Add(new Skill("Triple Shot (Kurzick)", Properties.Resources.Triple_Shot, Skill.Attributes.PvE_Only, Skill.Professions.Ranger, 2, "10E 10R - Bow Attack. Shoot 3 arrows simultaneously at target foe. These arrows deal 40...25% less damage"));
            Data.Add(new Skill("\"Save Yourselves!\" (Kurzick)", Properties.Resources._Save_Yourselves__, Skill.Attributes.PvE_Only, Skill.Professions.Warrior, 5, "8A - Shout. (4...6 seconds.) All other party members gain +100 armor."));
            Data.Add(new Skill("Aura of Holy Might (Kurzick)", Properties.Resources.Aura_of_Holy_Might, Skill.Attributes.PvE_Only, Skill.Professions.Dervish, 4, "10E 3/4C 30R - Enchantment Spell. (45 seconds.) Whenever you lose a Dervish enchantment, adjacent foes take 20...25 holy damage."));
            Data.Add(new Skill("Spear of Fury (Kurzick)", Properties.Resources.Spear_of_Fury, Skill.Attributes.PvE_Only, Skill.Professions.Paragon, 5, "5E 1C 8R - Spear Attack. Deals +30...40 damage. Gain 3...6 strikes of adrenaline if you hit a foe with a condition."));
            Data.Add(new Skill("Summon Spirits (Kurzick)", Properties.Resources.Summon_Spirits, Skill.Attributes.PvE_Only, Skill.Professions.Ritualist, 4, "5E 1/4C 5R - Spell. Spirits you control Shadow Step to your location and gain 60...100 Health."));
            // Sunspear skills:
            Data.Add(new Skill("Critical Agility", Properties.Resources.Critical_Agility, Skill.Attributes.PvE_Only, Skill.Professions.Assassin, 5, "5E 1C 30R - Enchantment Spell. (4 seconds plus 1 second for each rank of Critical Strikes.) You attack 33% faster and gain +15...25 armor. Renewal: every time you land a critical hit."));
            Data.Add(new Skill("Cry of Pain", Properties.Resources.Cry_of_Pain, Skill.Attributes.PvE_Only, Skill.Professions.Mesmer, 4, "10E 1/4C 15R - Spell. Interrupts a skill. If target foe had a Mesmer hex, deals 25...50 damage to target and foes in the area and causes 3...5 Health degeneration (10 seconds)."));
            Data.Add(new Skill("Necrosis", Properties.Resources.Necrosis, Skill.Attributes.PvE_Only, Skill.Professions.Necromancer, 5, "5E 1C 2R - Skill. Deals 60...90 damage if target foe has a condition or hex."));
            Data.Add(new Skill("Intensity", Properties.Resources.Intensity, Skill.Attributes.PvE_Only, Skill.Professions.Elementalist, 4, "5E 10R - Skill. (10 seconds.) The next time you deal elemental damage with a spell, other targets in the area take 60...70% of that damage."));
            Data.Add(new Skill("Seed of Life", Properties.Resources.Seed_of_Life, Skill.Attributes.PvE_Only, Skill.Professions.Monk, 4, "5E 1/4C 20R - Enchantment Spell. (2...5) seconds. Heals party members for 2 for each rank in Divine Favor whenever target takes damage. Cannot self target."));
            Data.Add(new Skill("Whirlwind Attack", Properties.Resources.Whirlwind_Attack, Skill.Attributes.PvE_Only, Skill.Professions.Warrior, 5, "6A 1R - Melee Attack. Deals +13...20 damage to target and adjacent foes."));
            Data.Add(new Skill("Never Rampage Alone", Properties.Resources.Never_Rampage_Alone, Skill.Attributes.PvE_Only, Skill.Professions.Ranger, 5, "15E 20R - Skill. (18...25 seconds.) You and your pet attack 25% faster and have +1...3 Health regeneration."));
            Data.Add(new Skill("Eternal Aura", Properties.Resources.Eternal_Aura, Skill.Attributes.PvE_Only, Skill.Professions.Dervish, 2, "15E 4C 30R - Enchantment Spell. You have +100 max Health. End effect: all party members in the area are resurrected with 40...50% Health and 20...30% Energy."));
            Data.Add(new Skill("Vampirism", Properties.Resources.Vampirism, Skill.Attributes.PvE_Only, Skill.Professions.Ritualist, 4, "10E 3/4C 30R - Binding Ritual. Creates a level 4...14 spirit (lifespan 75...150 seconds). Its attacks steal 10...20 Health, and you are healed for 10...20."));
            Data.Add(new Skill("\"There's Nothing to Fear!\"", Properties.Resources._There_s_Nothing_to_Fear__, Skill.Attributes.PvE_Only, Skill.Professions.Paragon, 5, "15E 20R - Shout. (4 seconds plus 1 second for every 2 ranks in Leadership.) Reduces damage by 20...35% for party members in earshot. End effect: heals for 35...60 Health."));
            // More random skills:
            Data.Add(new Skill("Sneak Attack", Properties.Resources.Sneak_Attack, Skill.Attributes.PvE_Only, Skill.Professions.None, 3, "5E 5R - Melee Attack. Inflicts Blindness (5...8 seconds). Counts as a lead attack."));

            Data.Add(new Skill("Trampling Ox", Properties.Resources.Trampling_Ox, Skill.Attributes.Dagger_Mastery, Skill.Professions.Assassin, 3, "5E 8R - Dual Attack. Deals +5...17...20 damage; causes knock-down if target foe is Crippled. Must follow an off-hand attack."));
            Data.Add(new Skill("Smoke Powder Defense", Properties.Resources.Smoke_Powder_Defense, Skill.Attributes.Shadow_Arts, Skill.Professions.Assassin, 2, "5E 20R - Stance. (8 seconds.) The next time you are struck, you take half damage and inflict Blindness condition (2...5...6 seconds) on adjacent foes."));

            Data.Add(new Skill("Confusing Images", Properties.Resources.Confusing_Images, Skill.Attributes.Illusion_Magic, Skill.Professions.Mesmer, 1, "10E 2C 20R - Hex Spell. (2...8...10 seconds). Target foe takes twice as long to activate non-attack skills."));

            Data.Add(new Skill("Hexer's Vigor", Properties.Resources.Hexer_s_Vigor, Skill.Attributes.Soul_Reaping, Skill.Professions.Necromancer, 2, "5E 1/4C 10R - Enchantment Spell. (10 seconds.) You have +1...7...8 Health regeneration. Ends if you use a non-hex skill."));
            Data.Add(new Skill("Masochism", Properties.Resources.Masochism, Skill.Attributes.Soul_Reaping, Skill.Professions.Necromancer, 4, "5E 1C 20R - Enchantment Spell. (10...34...40 seconds.) You have +2 Death Magic and Soul Reaping. Sacrifice 5...3...3% Health when you cast a spell."));

            Data.Add(new Skill("Piercing Trap", Properties.Resources.Piercing_Trap, Skill.Attributes.Wilderness_Survival, Skill.Professions.Ranger, 2, "10E 2C 30R - Trap. (90 seconds.) Affects nearby foes. Deals 5...41...50 piercing damage. Deals 15...51...60 more damage to any foes with Cracked Armor. Easily interrupted."));
            Data.Add(new Skill("Companionship", Properties.Resources.Companionship, Skill.Attributes.Beast_Mastery, Skill.Professions.Ranger, 1, "5E 2C 10R - Skill. Heals you for 30...102...120 if you have less Health than your pet. Heals your pet for 30...102...120 if it has less Health than you."));
            Data.Add(new Skill("Feral Aggression", Properties.Resources.Feral_Aggression, Skill.Attributes.Beast_Mastery, Skill.Professions.Ranger, 3, "15E 20R - Skill. (5...17...20 seconds.) Your pet attacks 33% faster and deals +3...9...10 damage."));
            Data.Add(new Skill("Disrupting Shot", Properties.Resources.Disrupting_Shot, Skill.Attributes.Marksmanship, Skill.Professions.Ranger, 3, "10E 1/2C 15R - Bow Attack. Interrupts an action. Interruption effect: +10...34...40 damage if you interrupt a skill."));
            Data.Add(new Skill("Volley", Properties.Resources.Volley, Skill.Attributes.Marksmanship, Skill.Professions.Ranger, 5, "5E 2R - Bow Attack. Deals +1...8...10 damage. Hits up to 3 foes adjacent to your target. All your preparations are removed."));
            Data.Add(new Skill("Expert Focus", Properties.Resources.Expert_Focus, Skill.Attributes.Expertise, Skill.Professions.Ranger, 3, "10E 2C 12R - Preparation. (24 seconds.) Your bow attack skills cost 1...2...2 less Energy and do +1...8...10 damage."));

            Data.Add(new Skill("Pious Fury", Properties.Resources.Pious_Fury, Skill.Attributes.Mysticism, Skill.Professions.Dervish, 4, "5E 10R - Stance. (1...6...7 second[s].) You attack 25% faster and remove 1 of your Dervish enchantments. Removal effect: this stance lasts twice as long."));
            Data.Add(new Skill("Crippling Victory", Properties.Resources.Crippling_Victory, Skill.Attributes.Scythe_Mastery, Skill.Professions.Dervish, 3, "6A - Scythe Attack. (3...7...8 seconds) Cripples target foe. If your health is greater than target foe's, all adjacent foes take 10...26...30 earth damage."));

            Data.Add(new Skill("Sundering Weapon", Properties.Resources.Sundering_Weapon, Skill.Attributes.Communing, Skill.Professions.Ritualist, 3, "5E 1C 10R - Weapon Spell. (4...9...10 seconds.) Target ally's next 3 attacks inflict Cracked Armor condition (5...17...20 seconds) and have 10% armor penetration."));
            Data.Add(new Skill("Weapon of Renewal", Properties.Resources.Weapon_of_Renewal, Skill.Attributes.Spawning_Power, Skill.Professions.Ritualist, 1, "5E 1C 10R - Weapon Spell. (4...9...10 seconds.) Target ally gains 1...6...7 Energy the next time this ally hits with an attack skill."));

            Data.Add(new Skill("Maiming Spear", Properties.Resources.Maiming_Spear, Skill.Attributes.Spear_Mastery, Skill.Professions.Paragon, 2, "5E 5R - Spear Attack. Inflicts Crippled condition (5...17...20 seconds) if target foe is Bleeding."));

            Data.Add(new Skill("Signet of Deadly Corruption", Properties.Resources.Signet_of_Deadly_Corruption, Skill.Attributes.Deadly_Arts, Skill.Professions.Assassin, 3, "1C 12R - Signet. Deals 5...29...35 damage (maximum 130) for each condition on target foe."));
            Data.Add(new Skill("Way of the Master", Properties.Resources.Way_of_the_Master, Skill.Attributes.Critical_Strikes, Skill.Professions.Assassin, 2, "5E 1/4C 30R - Enchantment Spell. (60 seconds.) While holding a non-dagger weapon, you have +3...27...33% chance to land a critical hit."));

            Data.Add(new Skill("Defile Defenses", Properties.Resources.Defile_Defenses, Skill.Attributes.Curses, Skill.Professions.Necromancer, 1, "5E 1C 5R - Hex Spell. (5...17...20 seconds.) Deals 30...102...120 damage the next time target foe blocks."));
            Data.Add(new Skill("Angorodon's Gaze", Properties.Resources.Angorodon_s_Gaze, Skill.Attributes.Soul_Reaping, Skill.Professions.Necromancer, 2, "5E 1C 10R - Spell. Steals 10...42...50 Health. You gain 3...10...12 Energy if you have a condition."));

            Data.Add(new Skill("Magnetic Surge", Properties.Resources.Magnetic_Surge, Skill.Attributes.Earth_Magic, Skill.Professions.Elementalist, 4, "10E 3/2C 15R - Enchantment Spell. Deals 15...63...75 damage. If you are Overcast, allies in earshot are enchanted for 1...4...5 seconds and block the next attack against them."));
            Data.Add(new Skill("Slippery Ground", Properties.Resources.Slippery_Ground, Skill.Attributes.Water_Magic, Skill.Professions.Elementalist, 2, "5E 1C 10R - Spell. Causes knock-down if this foe is Blind or moving. Affects foes adjacent to your target. 50% failure chance unless Water Magic greater than 4."));
            Data.Add(new Skill("Glowing Ice", Properties.Resources.Glowing_Ice, Skill.Attributes.Water_Magic, Skill.Professions.Elementalist, 5, "5E 1C 8R - Spell. Deals 5...41...50 cold damage. You gain 5 Energy plus 1 Energy for every 2 ranks of Energy Storage if target foe is hexed with Water Magic."));
            Data.Add(new Skill("Energy Blast", Properties.Resources.Energy_Blast, Skill.Attributes.Energy_Storage, Skill.Professions.Elementalist, 2, "10E 3/2C 20R - Spell. Deals 1...2...2 damage (maximum 130) for each point of Energy you have."));

            Data.Add(new Skill("Distracting Strike", Properties.Resources.Distracting_Strike, Skill.Attributes.None, Skill.Professions.Warrior, 3, "5E 1/2C 15R - Melee Attack. Interrupts an action. Interruption effect: Disables interrupted skill (20 seconds) if target foe has Cracked Armor. Deals no damage."));
            Data.Add(new Skill("Symbolic Strike", Properties.Resources.Symbolic_Strike, Skill.Attributes.None, Skill.Professions.Warrior, 2, "4A - Melee Attack. Deals +12 damage (maximum 70) for each signet you have equipped."));
            Data.Add(new Skill("Soldier's Speed", Properties.Resources.Soldier_s_Speed, Skill.Attributes.Tactics, Skill.Professions.Warrior, 2, "5E 15R - Stance. (3...15...18 seconds.) You move 15% faster and an additional 15% while affected by a chant or shout."));
            Data.Add(new Skill("Body Blow", Properties.Resources.Body_Blow, Skill.Attributes.Strength, Skill.Professions.Warrior, 4, "7A - Melee Attack. Deals +10...34...40 damage. Inflicts Deep Wound (0...12...15 second[s]) if target foe has Cracked Armor."));

            Data.Add(new Skill("Body Shot", Properties.Resources.Body_Shot, Skill.Attributes.Marksmanship, Skill.Professions.Ranger, 4, "5E 8R - Bow Attack. Deals +5...17...20 damage. You gain 5...9...10 Energy if target foe has Cracked Armor."));
            Data.Add(new Skill("Poison Tip Signet", Properties.Resources.Poison_Tip_Signet, Skill.Attributes.Wilderness_Survival, Skill.Professions.Ranger, 3, "1C 6R - Signet. (60 seconds.) Inflicts Poisoned condition (8...14...15 seconds) with your next attack."));

            Data.Add(new Skill("Signet of Mystic Speed", Properties.Resources.Signet_of_Mystic_Speed, Skill.Attributes.Wind_Prayers, Skill.Professions.Dervish, 1, "1C 20R - Signet. (30 seconds.) Your next 1...3...3 self-targeted enchantments cast instantly. Flash enchantments do not consume uses of this skill."));
            Data.Add(new Skill("Shield of Force", Properties.Resources.Shield_of_Force, Skill.Attributes.Earth_Prayers, Skill.Professions.Dervish, 3, "10E 12R - Flash Enchantment Spell. (1...13...16 second[s].) Blocks the next 1 attack against you. Knocks down and inflicts Weakness (5...17...20 seconds) on all adjacent attacking foes."));

            Data.Add(new Skill("Mending Grip", Properties.Resources.Mending_Grip, Skill.Attributes.Restoration_Magic, Skill.Professions.Ritualist, 2, "5E 1C 6R - Spell. Heals for 15...63...75. Removes one condition if target ally is under a Weapon spell."));
            Data.Add(new Skill("Spiritleech Aura", Properties.Resources.Spiritleech_Aura, Skill.Attributes.Restoration_Magic, Skill.Professions.Ritualist, 2, "5E 1/4C 20R - Skill. (5...17...20 seconds.) All of your spirits within earshot deal 5...17...20 less damage and steal 5...17...20 Health when they attack."));
            Data.Add(new Skill("Rejuvenation", Properties.Resources.Rejuvenation, Skill.Attributes.Restoration_Magic, Skill.Professions.Ritualist, 3, "10E 3/4C 30R - Binding Ritual. Creates a level 1...16...20 spirit (30...78...90 second lifespan). Heals party members in earshot for 3...9...10 each second. Healing cost: this spirit loses 3...9...10 Health."));
            Data.Add(new Skill("Agony", Properties.Resources.Agony, Skill.Attributes.Channeling_Magic, Skill.Professions.Ritualist, 3, "10E 3/4C 30R - Binding Ritual. Creates a level 1...10...12 spirit (30...78...90 second lifespan). Causes 3...9...10 Health loss each second to foes in earshot. This spirit loses 3...9...10 Health for each foe that loses Health."));
            Data.Add(new Skill("Ghostly Weapon", Properties.Resources.Ghostly_Weapon, Skill.Attributes.Communing, Skill.Professions.Ritualist, 2, "5E 1/4C 1R - Weapon Spell. (5...17...20 seconds.) Target ally's next attack is unblockable. Cannot self-target."));

            Data.Add(new Skill("Inspirational Speech", Properties.Resources.Inspirational_Speech, Skill.Attributes.Motivation, Skill.Professions.Paragon, 2, "5E 20R - Skill. Target ally gains 1...3...4 strike[s] of adrenaline. Cannot self-target. You lose all adrenaline."));
            Data.Add(new Skill("Burning Shield", Properties.Resources.Burning_Shield, Skill.Attributes.Leadership, Skill.Professions.Paragon, 3, "5E 20R - Skill. (3...8...9 seconds.) Blocks the next attack skill against you. Inflicts Burning condition (1...5...6 second[s]) if it was a melee attack. No effect unless you are wielding a shield."));
            Data.Add(new Skill("Holy Spear", Properties.Resources.Holy_Spear, Skill.Attributes.Spear_Mastery, Skill.Professions.Paragon, 4, "4A - Spear Attack. Deals +5...17...20 damage. Deals 15...75...90 holy damage and inflicts Burning condition (3 seconds) to nearby foes if you hit a summoned creature."));
            Data.Add(new Skill("Spear Swipe", Properties.Resources.Spear_Swipe, Skill.Attributes.Leadership, Skill.Professions.Paragon, 3, "10E 20R - Spear Melee Attack. Deals +5...17...20 damage and inflicts Dazed condition (4...9...10 seconds). This attack has melee range."));
            // Dwarf skills:
            Data.Add(new Skill("Alkar's Alchemical Acid", Properties.Resources.Alkar_s_Alchemical_Acid, Skill.Attributes.PvE_Only, Skill.Professions.None, 3, "10E 1C 10R - Spell. Projectile: deals 40...50 damage to target and adjacent foes. Deals 45...70 more damage and inflicts Cracked Armor condition (14...20 seconds) to Destroyers."));
            Data.Add(new Skill("Light of Deldrimor", Properties.Resources.Light_of_Deldrimor, Skill.Attributes.PvE_Only, Skill.Professions.None, 4, "5E 3/4C 20R - Spell. Deals 55...80 holy damage to foes in the area. Pings hidden objects within the area on the compass."));
            Data.Add(new Skill("Ear Bite", Properties.Resources.Ear_Bite, Skill.Attributes.PvE_Only, Skill.Professions.None, 2, "4A 3/4C - Touch Skill. Deals 50...70 piercing damage and inflicts Bleeding condition (15...25 seconds)."));
            Data.Add(new Skill("Low Blow", Properties.Resources.Low_Blow, Skill.Attributes.PvE_Only, Skill.Professions.None, 2, "6A 3/4C - Touch Skill. Deals 45...70 damage. Inflicts 30...50 damage and Cracked Armor (14...20 seconds) if target foe is knocked down."));
            Data.Add(new Skill("Brawling Headbutt", Properties.Resources.Brawling_Headbutt, Skill.Attributes.PvE_Only, Skill.Professions.None, 3, "7A 3/4C - Touch Skill. Deals 45...70 damage; causes knock-down."));
            Data.Add(new Skill("\"Don't Trip!\"", Properties.Resources._Don_t_Trip__, Skill.Attributes.PvE_Only, Skill.Professions.None, 2, "5E 20R - Shout. (3...5 seconds.) Prevents knock-down; affects party members within earshot."));
            Data.Add(new Skill("\"By Ural's Hammer!\"", Properties.Resources._By_Ural_s_Hammer__, Skill.Attributes.PvE_Only, Skill.Professions.None, 2, "5E 60R - Shout. (30 seconds.) Resurrect all dead party members in earshot with full Health and Energy. Affected party members deal 25...33% more damage. Affected party members die when it ends, but do not receive a death penalty."));
            Data.Add(new Skill("Drunken Master", Properties.Resources.Drunken_Master, Skill.Attributes.PvE_Only, Skill.Professions.None, 2, "5E 60R - Stance. (72...90 seconds.) You move and attack 10...15% faster if you are not drunk. You move and attack 25...33% faster if you are drunk."));
            Data.Add(new Skill("Great Dwarf Weapon", Properties.Resources.Great_Dwarf_Weapon, Skill.Attributes.PvE_Only, Skill.Professions.None, 3, "10E 1C 5R - Weapon Spell. (20 seconds.) +15...20 weapon damage and 28...40% chance to cause knock-down with attacks. Cannot self-target."));
            Data.Add(new Skill("Great Dwarf Armor", Properties.Resources.Great_Dwarf_Armor, Skill.Attributes.PvE_Only, Skill.Professions.None, 3, "5E 1C 10R - Enchantment Spell. (22...40 seconds.) +24 armor and +60 maximum Health. Additional +24 armor against Destroyers."));
            Data.Add(new Skill("Breath of the Great Dwarf", Properties.Resources.Breath_of_the_Great_Dwarf, Skill.Attributes.PvE_Only, Skill.Professions.None, 3, "10E 1/4C 15R - Spell. Removes burning and heals for 50...60 Health. Affects party members."));
            Data.Add(new Skill("Snow Storm", Properties.Resources.Snow_Storm, Skill.Attributes.PvE_Only, Skill.Professions.None, 4, "10E 1C 10R - Spell. Deals 30...40 cold damage each second (5 seconds). Hits foes adjacent to target foe's initial location."));
            Data.Add(new Skill("Black Powder Mine", Properties.Resources.Black_Powder_Mine, Skill.Attributes.PvE_Only, Skill.Professions.None, 2, "10E 2C 20R - Trap. (90 seconds.) Affects nearby foes. Deals 20...30 damage. Inflicts Blindness and Bleeding conditions (7...10 seconds). Easily interrupted."));
            // The Asura summon skills:
            Data.Add(new Skill("Summon Mursaat", Properties.Resources.Summon_Mursaat, Skill.Attributes.PvE_Only, Skill.Professions.None, 2, "10E 3C 60R - Spell. Summon a level 14...20 Mursaat (40...60 lifespan) that has Enervating Charge. Only 1 Asura Summon can be active a time."));
            Data.Add(new Skill("Summon Ruby Djinn", Properties.Resources.Summon_Ruby_Djinn, Skill.Attributes.PvE_Only, Skill.Professions.None, 3, "10E 3C 60R - Spell. Summon a level 14...20 Ruby Djinn (40...60 lifespan) that has Immolate. Only 1 Asura Summon can be active a time."));
            Data.Add(new Skill("Summon Ice Imp", Properties.Resources.Summon_Ice_Imp, Skill.Attributes.PvE_Only, Skill.Professions.None, 2, "10E 3C 60R - Spell. Summon a level 14...20 Ice Imp (40...60 lifespan) that has Ice Spikes. Only 1 Asura Summon can be active a time."));
            Data.Add(new Skill("Summon Naga Shaman", Properties.Resources.Summon_Naga_Shaman, Skill.Attributes.PvE_Only, Skill.Professions.None, 2, "10E 3C 60R - Spell. Summon a level 14...20 Naga Shaman (40...60 lifespan) that has Stoning. Only 1 Asura Summon can be active a time."));
            // Ebon Vanguard skills:
            Data.Add(new Skill("Deft Strike", Properties.Resources.Deft_Strike, Skill.Attributes.PvE_Only, Skill.Professions.None, 2, "5E 1/2C 8R - Ranged Attack. Deals 18...30 damage. Inflicts Bleeding condition (18...30 seconds) if target foe has Cracked Armor."));
            Data.Add(new Skill("Signet of Infection", Properties.Resources.Signet_of_Infection, Skill.Attributes.PvE_Only, Skill.Professions.None, 2, "1C 10R - Signet. Inflicts Diseased condition 13...20 seconds if target foe is Bleeding."));
            Data.Add(new Skill("Tryptophan Signet", Properties.Resources.Tryptophan_Signet, Skill.Attributes.PvE_Only, Skill.Professions.None, 4, "1C 15R - Signet. (14...20 seconds.) Target and adjacent foes move and attack 23...40% slower."));
            Data.Add(new Skill("Ebon Battle Standard of Courage", Properties.Resources.Ebon_Battle_Standard_of_Courage, Skill.Attributes.PvE_Only, Skill.Professions.None, 3, "10E 1C 20R - Ward Spell. (14...20 seconds.) Allies in this ward have +24 armor and +24 more armor against Charr. Spirits are unaffected."));
            Data.Add(new Skill("Ebon Battle Standard of Wisdom", Properties.Resources.Ebon_Battle_Standard_of_Wisdom, Skill.Attributes.PvE_Only, Skill.Professions.None, 3, "10E 1C 20R - Ward Spell. (14...20 seconds.) Spells that allies in this ward cast have a 44...60% chance to recharge 50% faster. Spirits are unaffected."));
            Data.Add(new Skill("Ebon Battle Standard of Honor", Properties.Resources.Ebon_Battle_Standard_of_Honor, Skill.Attributes.PvE_Only, Skill.Professions.None, 4, "10E 1C 20R - Ward Spell. (14...20 seconds.) Allies in this ward deal +8...15 damage and +7...10 more damage against Charr. Spirits are unaffected."));
            Data.Add(new Skill("Ebon Vanguard Sniper Support", Properties.Resources.Ebon_Vanguard_Sniper_Support, Skill.Attributes.PvE_Only, Skill.Professions.None, 3, "10E 1C 15R - Spell. Deals 54...90 piercing damage and inflicts Bleeding condition (5...25 seconds). 10% chance of +540...900 piercing damage. 25% chance of +540...900 piercing damage if target foe is a Charr."));
            Data.Add(new Skill("Ebon Vanguard Assassin Support", Properties.Resources.Ebon_Vanguard_Assassin_Support, Skill.Attributes.PvE_Only, Skill.Professions.None, 5, "10E 1C 30R - Spell. Summon a level 14...20 assassin that has Iron Palm, Fox Fangs, and Nine Tail Strike; it Shadow Steps to this foe. If this foe is a Charr, the assassin lives for 24...30 seconds. If target foe is not a Charr, the Assassin lives for 12...15 seconds."));
            // A few more random skills:
            Data.Add(new Skill("Well of Ruin", Properties.Resources.Well_of_Ruin, Skill.Attributes.Curses, Skill.Professions.Necromancer, 3, "10E 1C 20R - Well Spell. (5...25...30 seconds.) Inflicts Cracked Armor condition (5...17...20 seconds) to any foe in the well that takes physical damage. Exploits a fresh corpse."));
            Data.Add(new Skill("Atrophy", Properties.Resources.Atrophy, Skill.Attributes.Curses, Skill.Professions.Necromancer, 1, "10E 1C 20R - Hex Spell. (3...6...7 seconds.) Reduces this foe's primary attribute to 0."));
            Data.Add(new Skill("Spear of Redemption", Properties.Resources.Spear_of_Redemption, Skill.Attributes.Spear_Mastery, Skill.Professions.Paragon, 3, "3A - Spear Attack. Deals +5...17...20 damage. If it fails to hit, you lose one condition."));
            // Some norn skills:
            Data.Add(new Skill("\"Finish Him!\"", Properties.Resources._Finish_Him__, Skill.Attributes.PvE_Only, Skill.Professions.None, 4, "10E 15R - Shout. Deals 44...80 damage and inflicts Cracked Armor and Deep Wound conditions (12...20 seconds). No effect unless target foe has less than 50% Health."));
            Data.Add(new Skill("\"Dodge This!\"", Properties.Resources._Dodge_This__, Skill.Attributes.PvE_Only, Skill.Professions.None, 3, "4A - Shout. (16...20 seconds.) Your next attack is unblockable and deals +14...20 damage."));
            Data.Add(new Skill("\"I Am the Strongest!\"", Properties.Resources._I_Am_the_Strongest__, Skill.Attributes.PvE_Only, Skill.Professions.None, 3, "5E 20R - Shout. Your next 5...8 attacks deal +14...20 damage."));
            Data.Add(new Skill("\"I Am Unstoppable!\"", Properties.Resources._I_Am_Unstoppable__, Skill.Attributes.PvE_Only, Skill.Professions.None, 2, "5E 30R - Shout. (16...20 seconds.) You have +24 armor and cannot be knocked-down or Crippled."));
            Data.Add(new Skill("A Touch of Guile", Properties.Resources.A_Touch_of_Guile, Skill.Attributes.PvE_Only, Skill.Professions.None, 2, "5E 3/4C 15R - Touch Hex Spell. Deals 44...80 damage. Target foe cannot attack (5...7...8 seconds) if it was knocked-down."));
            Data.Add(new Skill("\"You Move Like a Dwarf!\"", Properties.Resources._You_Move_Like_a_Dwarf__, Skill.Attributes.PvE_Only, Skill.Professions.None, 5, "10E 10R - Shout. Deals 44...80 damage, causes knock-down, and inflicts Crippled condition (8...15 seconds)."));
            Data.Add(new Skill("\"You Are All Weaklings!\"", Properties.Resources._You_Are_All_Weaklings__, Skill.Attributes.PvE_Only, Skill.Professions.None, 2, "5E 12R - Shout. Inflicts Weakness condition (8...12 seconds). Also affects adjacent foes."));
            Data.Add(new Skill("Feel No Pain", Properties.Resources.Feel_No_Pain, Skill.Attributes.PvE_Only, Skill.Professions.None, 3, "5E 20R - Skill. (30 seconds.) You have +2...3 Health regeneration. You have +200...300 maximum Health if you are drunk when activating this skill."));
            Data.Add(new Skill("Club of a Thousand Bears", Properties.Resources.Club_of_a_Thousand_Bears, Skill.Attributes.PvE_Only, Skill.Professions.None, 2, "5E 12R - Melee Attack. Deals +6...9 damage for each adjacent foe (maximum 60 damage). Causes knock-down if target foe is non-human."));
            Data.Add(new Skill("Ursan Blessing", Properties.Resources.Ursan_Blessing, Skill.Attributes.PvE_Only, Skill.Professions.None, 2, "10E 60R - Elite Form. You lose all effects and take on the aspect of the bear (60 seconds). All your attributes are set to 0 and bear attacks replace your skills, and you have 100 armor and 750...790...800 Health.", true));
            Data.Add(new Skill("Volfen Blessing", Properties.Resources.Volfen_Blessing, Skill.Attributes.PvE_Only, Skill.Professions.None, 2, "10E 60R - Elite Form. You lose all effects and take on the aspect of the wolf (60 seconds). All your attributes are set to 0 and wolf attacks replace your skills, and you have 80 armor, 660...700 Health and 2...4 Health regeneration.", true));
            Data.Add(new Skill("Raven Blessing", Properties.Resources.Raven_Blessing, Skill.Attributes.PvE_Only, Skill.Professions.None, 2, "10E 60R - Elite Form. You lose all effects and take on the aspect of the Raven (60 seconds). All your attributes are set to 0 and raven attacks replace your skills, and you have 80 armor, 660...700 Health, and a 20...30% block chance.", true));
            // Asuran skills for a bit:
            Data.Add(new Skill("Mindbender", Properties.Resources.Mindbender, Skill.Attributes.PvE_Only, Skill.Professions.None, 2, "5E 1C 24R - Enchantment Spell. (10...16 seconds.) You move 20...33% faster and cast Spells 20% faster."));
            Data.Add(new Skill("Smooth Criminal", Properties.Resources.Smooth_Criminal, Skill.Attributes.PvE_Only, Skill.Professions.None, 2, "5E 1C 20R - Spell. (10...20 seconds.) Disables one Spell. This skill becomes that Spell. You gain 5...10 Energy."));
            Data.Add(new Skill("Technobabble", Properties.Resources.Technobabble, Skill.Attributes.PvE_Only, Skill.Professions.None, 4, "10E 1C 10R - Spell. Deals 30...40 damage to target and adjacent foes. Inflicts Dazed condition (3...5 seconds) on these foes if target was not a boss."));
            Data.Add(new Skill("Radiation Field", Properties.Resources.Radiation_Field, Skill.Attributes.PvE_Only, Skill.Professions.None, 2, "15E 2C 12R - Ward Spell. (5 seconds.) Causes -4...6 Health degeneration to foes in the area. End effect: inflicts Disease condition (12...20 seconds) to foes in the area."));
            Data.Add(new Skill("Asuran Scan", Properties.Resources.Asuran_Scan, Skill.Attributes.PvE_Only, Skill.Professions.None, 3, "5E 5R - Hex Spell. (9...12 seconds.) You cannot miss target foe. If you kill this foe, you lose 5% Death Penalty."));
            Data.Add(new Skill("Air of Superiority", Properties.Resources.Air_of_Superiority, Skill.Attributes.PvE_Only, Skill.Professions.None, 4, "5E 20R - Skill. (20...30 seconds). Gain a random Asura benefit every time you earn experience from killing an enemy."));
            Data.Add(new Skill("Mental Block", Properties.Resources.Mental_Block, Skill.Attributes.PvE_Only, Skill.Professions.None, 3, "10E 1C 15R - Enchantment Spell. (5...11 seconds.) You have a 50% chance to block. Renewal: every time an enemy hits you."));
            Data.Add(new Skill("Pain Inverter", Properties.Resources.Pain_Inverter, Skill.Attributes.PvE_Only, Skill.Professions.None, 4, "10E 1C 20R - Hex Spell. (6...10 seconds.) Deals 100...140% of the attack damage (maximum 80) back to target foe every time it does damage."));
            // Back to ebon vanguard...:
            Data.Add(new Skill("Ebon Escape", Properties.Resources.Ebon_Escape, Skill.Attributes.PvE_Only, Skill.Professions.None, 2, "5E 1/4C 10R - Spell. Heals you and target ally for 70...110. Initial effect: Shadow Step to this ally. Cannot self-target."));
            Data.Add(new Skill("Weakness Trap", Properties.Resources.Weakness_Trap, Skill.Attributes.PvE_Only, Skill.Professions.None, 2, "10E 2C 30R - Trap. (90 seconds.) Affects nearby foes. Deals 24...50 lightning damage. Inflicts Weakness condition (10...20 seconds). Knocks-down Charr. Easily interrupted."));
            Data.Add(new Skill("Winds", Properties.Resources.Winds, Skill.Attributes.PvE_Only, Skill.Professions.None, 1, "5E 5C 30R - Ebon Vanguard Ritual. Creates a level 4...10 spirit (54...90 second lifespan.) Affects foes within range. 15% chance to miss with ranged attacks."));
            // It's pretty random from here on out:
            Data.Add(new Skill("Dwarven Stability", Properties.Resources.Dwarven_Stability, Skill.Attributes.PvE_Only, Skill.Professions.None, 2, "5E 1/4C 30R - Enchantment Spell. (24...30 seconds.) Your stances last 55...100% longer. You cannot be knocked-down if you activated this skill while drunk."));

            // SKILLS BELOW HERE: Assumed bad, tested first few, and they were:
            //Data.Add(new Skill("Complicate", Properties.Resources.Complicate, Skill.Attributes.Domination_Magic, Skill.Professions.Mesmer));

            //Data.Add(new Skill("Reaper's Mark", Properties.Resources.Reaper_s_Mark, Skill.Attributes.Soul_Reaping, Skill.Professions.Necromancer));
            //Data.Add(new Skill("Enfeeble", Properties.Resources.Enfeeble, Skill.Attributes.Curses, Skill.Professions.Necromancer));
            //Data.Add(new Skill("Signet of Lost Souls", Properties.Resources.Signet_of_Lost_Souls, Skill.Attributes.Soul_Reaping, Skill.Professions.Necromancer));

            //Data.Add(new Skill("Searing Flames", Properties.Resources.Searing_Flames, Skill.Attributes.Fire_Magic, Skill.Professions.Elementalist));
            //Data.Add(new Skill("Glowing Gaze", Properties.Resources.Glowing_Gaze, Skill.Attributes.Fire_Magic, Skill.Professions.Elementalist));
            //Data.Add(new Skill("Steam", Properties.Resources.Steam, Skill.Attributes.Water_Magic, Skill.Professions.Elementalist));
            //Data.Add(new Skill("Flame Djinn's Haste", Properties.Resources.Flame_Djinn_s_Haste, Skill.Attributes.Fire_Magic, Skill.Professions.Elementalist));
            //Data.Add(new Skill("Liquid Flame", Properties.Resources.Liquid_Flame, Skill.Attributes.Fire_Magic, Skill.Professions.Elementalist));

            //Data.Add(new Skill("Smite Condition", Properties.Resources.Smite_Condition, Skill.Attributes.Smiting_Prayers, Skill.Professions.Monk));

            //Data.Add(new Skill("Crippling Slash", Properties.Resources.Crippling_Slash, Skill.Attributes.Swordsmanship, Skill.Professions.Warrior));
            //Data.Add(new Skill("Sun and Moon Slash", Properties.Resources.Sun_and_Moon_Slash, Skill.Attributes.Swordsmanship, Skill.Professions.Warrior));
            //Data.Add(new Skill("Enraging Charge", Properties.Resources.Enraging_Charge, Skill.Attributes.Strength, Skill.Professions.Warrior));
            //Data.Add(new Skill("Tiger Stance", Properties.Resources.Tiger_Stance, Skill.Attributes.Strength, Skill.Professions.Warrior));

            //Data.Add(new Skill("Burning Arrow", Properties.Resources.Burning_Arrow, Skill.Attributes.Marksmanship, Skill.Professions.Ranger));
            //Data.Add(new Skill("Natural Stride", Properties.Resources.Natural_Stride, Skill.Attributes.Wilderness_Survival, Skill.Professions.Ranger));

            //Data.Add(new Skill("Falling Lotus Strike", Properties.Resources.Falling_Lotus_Strike, Skill.Attributes.Dagger_Mastery, Skill.Professions.Assassin));

            //Data.Add(new Skill("Anthem of Weariness", Properties.Resources.Anthem_of_Weariness, Skill.Attributes.Command, Skill.Professions.Paragon));

            //Data.Add(new Skill("Pious Fury", Properties.Resources.Pious_Fury, Skill.Attributes.Mysticism, Skill.Professions.Dervish));

            // 15th Anniversary PvE only elite skills. These don't have their proper attributes because PvE only overrides that.
            Data.Add(new Skill("Time Ward", Properties.Resources.Time_Ward, Skill.Attributes.PvE_Only, Skill.Professions.Mesmer, 3, "10E 2C 30R - Elite Ward Spell. (3...13...15 seconds.) Allies in this ward cast spells 15...19...20% faster and recharge skills 15...19...20% faster. Allied spirits are not affected. PvE Fast-Casting Skill.", true));
            Data.Add(new Skill("Soul Taker", Properties.Resources.Soul_Taker, Skill.Attributes.PvE_Only, Skill.Professions.Necromancer, 2, "5E 1C 15R - Elite Enchantment Spell. (3...25...30 seconds.) Attacks deal +15...19...20 damage and sacrifice 15...19...20 health. PvE Soul Reaping Skill.", true));
            Data.Add(new Skill("Over the Limit", Properties.Resources.Over_the_Limit, Skill.Attributes.PvE_Only, Skill.Professions.Elementalist, 2, "-1ER 5E 1C 20R - Elite Enchantment Spell. Spells cast 15...19...20% faster and recharge 15...43...50% faster. Continuously gain Overcast while active. PvE Energy Storage Skill.", true));
            Data.Add(new Skill("Judgment Strike", Properties.Resources.Judgment_Strike, Skill.Attributes.PvE_Only, Skill.Professions.Monk, 2, "5E 1C 8R - Elite Melee Attack. Attacks target and adjacent foes for +13...27...30 Holy damage. Causes knock down on attacking foes. PvE Divine Favor Skill.", true));
            Data.Add(new Skill("Seven Weapons Stance", Properties.Resources.Seven_Weapons_Stance, Skill.Attributes.PvE_Only, Skill.Professions.Warrior, 2, "5E 20R - Elite Stance. (3...17...20 seconds.) Weapon attributes are increased by +1...12...15. You attack 33% faster. PvE Strength Skill.", true));
            Data.Add(new Skill("\"Together as One!\"", Properties.Resources._Together_as_One__, Skill.Attributes.PvE_Only, Skill.Professions.Ranger, 4, "10E 15R - Elite Shout. (3...13...15 seconds.) All party members near you or your pet deal +5...13...15 damage with attacks and gain +1...6...7 Health regeneration. PvE Expertise Skill.", true));
            Data.Add(new Skill("Shadow Theft", Properties.Resources.Shadow_Theft, Skill.Attributes.PvE_Only, Skill.Professions.Assassin, 4, "5E 1/4C 20R - Elite Skill. Shadow Step to target foe. For 5...17...20 seconds that foe's attributes are reduced by 1...4...5 and your attributes are increased by 1...4...5. Counts as a Lead Attack. PvE Critical Strikes Skill.", true));
            Data.Add(new Skill("Weapons of Three Forges", Properties.Resources.Weapons_of_Three_Forges, Skill.Attributes.PvE_Only, Skill.Professions.Ritualist, 2, "10E 2C 15R - Elite Weapon Spell. (3...17...20 seconds.) Allies in earshot gain the effect of a random Weapon Spell. Allied spirits are not affected. PvE Spawning Power Skill.", true));
            Data.Add(new Skill("Vow of Revolution", Properties.Resources.Vow_of_Revolution, Skill.Attributes.PvE_Only, Skill.Professions.Dervish, 2, "10E 1C 20R - Elite Enchantment Spell. (3...9...10 seconds.) Gain +1...4...5 energy regeneration. Renewal: whenever you use a non-Dervish skill. PvE Mysticism Skill.", true));
            Data.Add(new Skill("Heroic Refrain", Properties.Resources.Heroic_Refrain, Skill.Attributes.PvE_Only, Skill.Professions.Paragon, 5, "5E 1C 10R - Elite Echo. (3...13...15 seconds.) Target ally gains +1...3...3 to all attributes. Renewal: every time a chant or shout ends on this ally. Cannot target spirits. PvE Leadership Skill.", true));

            // Give skills their IDs.
            for (int i = 0; i < Data.Count; ++i)
            {
                Data[i].SetID(i);
            }

            CheckRatingsFile();
            RarityLabels = Properties.Settings.Default.RarityLabels.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            CheckRaritiesFile();
            PopulateLists();
        }

        internal class TemplateInformation
        {
            public TemplateInformation(string code)
            {
                Attributes = new List<AttributeRank>();
                ParseTemplateCode(code);
            }

            public TemplateInformation()
            {
                Attributes = new List<AttributeRank>();
            }

            public bool ValidTemplate { get; private set; }

            private Skill[] _SkillBar = new Skill[8];
            public Skill[] SkillBar { get { return _SkillBar; } }
            public Skill.Professions PrimaryProfession { get; private set; }
            public Skill.Professions SecondaryProfession { get; private set; }
            public string TemplateCode { get; private set; }

            public struct AttributeRank
            {
                public AttributeRank(Skill.Attributes attr, int rank)
                {
                    Attribute = attr;
                    Rank = rank;
                }

                public Skill.Attributes Attribute;
                public int Rank;
            }

            public List<AttributeRank> Attributes { get; private set; }
            public bool ParseTemplateCode(string code)
            {
                int PrimaryProfessionID = 0;
                int SecondaryProfessionID = 0;
                AttributeRank[] AttributesArray = null;
                int[] SkillbarIDs = new int[8];
                try
                {
                    byte[] bytes = TranslateString(code);
                    BitReader bits = new BitReader(bytes);

                    // Get version information:
                    int version = bits.Read(4);
                    if (version == 0xE) bits.Read(4);

                    // Professions:
                    int bits_per_profession_id = bits.Read(2) * 2 + 4;
                    PrimaryProfessionID = bits.Read(bits_per_profession_id);
                    SecondaryProfessionID = bits.Read(bits_per_profession_id);

                    // Attributes:
                    int num_attributes = bits.Read(4);
                    AttributesArray = new AttributeRank[num_attributes];
                    int bits_per_attribute_id = bits.Read(4) + 4;
                    for (int i = 0; i < num_attributes; ++i)
                    {
                        // Need to verify that my attributes are in the proper order and such.
                        int attrID = bits.Read(bits_per_attribute_id);
                        int rank = bits.Read(4);
                        AttributesArray[i] = new AttributeRank(GetAttributeFromID(attrID), rank);
                    }

                    // Skills:
                    int bits_per_skill_id = bits.Read(4) + 8;
                    for (int i = 0; i < 8; ++i)
                    {
                        SkillbarIDs[i] = bits.Read(bits_per_skill_id);
                    }

                    ValidTemplate = true;
                }
                catch (IndexOutOfRangeException)
                {
                    ValidTemplate = false;
                }

                if(ValidTemplate)
                {
                    TemplateCode = code;
                    Attributes.Clear();
                    if(AttributesArray != null) Attributes.AddRange(AttributesArray);
                    PrimaryProfession = GetProfessionFromID(PrimaryProfessionID);
                    SecondaryProfession = GetProfessionFromID(SecondaryProfessionID);
                    for(int i = 0; i < 8; ++i)
                    {
                        _SkillBar[i] = GetSkillByEngineID(SkillbarIDs[i]);
                    }
                }

                return ValidTemplate;
            }

            public string DebugTemplateCode(string code)
            {
                int PrimaryProfessionID = 0;
                int SecondaryProfessionID = 0;
                AttributeRank[] AttributesArray = null;
                int[] SkillbarIDs = new int[8];
                try
                {
                    byte[] bytes = TranslateString(code);
                    BitReader bits = new BitReader(bytes);

                    // Get version information:
                    int version = bits.Read(4);
                    if (version == 0xE) bits.Read(4);

                    // Professions:
                    int bits_per_profession_id = bits.Read(2) * 2 + 4;
                    PrimaryProfessionID = bits.Read(bits_per_profession_id);
                    SecondaryProfessionID = bits.Read(bits_per_profession_id);

                    // Attributes:
                    int num_attributes = bits.Read(4);
                    AttributesArray = new AttributeRank[num_attributes];
                    int bits_per_attribute_id = bits.Read(4) + 4;
                    for (int i = 0; i < num_attributes; ++i)
                    {
                        // Need to verify that my attributes are in the proper order and such.
                        int attrID = bits.Read(bits_per_attribute_id);
                        int rank = bits.Read(4);
                        AttributesArray[i] = new AttributeRank(GetAttributeFromID(attrID), rank);
                    }

                    // Skills:
                    int bits_per_skill_id = bits.Read(4) + 8;
                    for (int i = 0; i < 8; ++i)
                    {
                        SkillbarIDs[i] = bits.Read(bits_per_skill_id);
                    }

                    ValidTemplate = true;
                }
                catch (IndexOutOfRangeException)
                {
                    ValidTemplate = false;
                }

                if (ValidTemplate)
                {
                    TemplateCode = code;
                    Attributes.Clear();
                    if (AttributesArray != null) Attributes.AddRange(AttributesArray);
                    PrimaryProfession = GetProfessionFromID(PrimaryProfessionID);
                    SecondaryProfession = GetProfessionFromID(SecondaryProfessionID);
                    for (int i = 0; i < 8; ++i)
                    {
                        _SkillBar[i] = GetSkillByEngineID(SkillbarIDs[i]);
                    }

                    string Information = "Primary Profession: " + PrimaryProfession.ToString() + Environment.NewLine;
                    Information += "Secondary Profession: " + SecondaryProfession.ToString() + Environment.NewLine;
                    Information += Environment.NewLine + "Attributes: " + Environment.NewLine;


                    foreach(AttributeRank attr in Attributes)
                    {
                        Information += attr.Attribute.ToString() + ": " + attr.Rank.ToString() + Environment.NewLine;
                    }

                    Information += Environment.NewLine + "Skills:" + Environment.NewLine;

                    for(int i = 0; i < 8; ++i)
                    {
                        Information += "Skill Slot " + (i + 1).ToString() + ": Skill ID " + SkillbarIDs[i].ToString() + " (";
                        if (_SkillBar[i] == null) Information += "[Unknown Skill]";
                        else Information += _SkillBar[i].Name;
                        Information += ")" + Environment.NewLine;
                    }

                    return Information;
                }

                return "Invalid Code";
            }

            #region Template parsing helper functions

            Skill.Professions GetProfessionFromID(int id)
            {
                //    public enum Profession { None = 0, Warrior = 1, Ranger = 2, Monk = 3, Necromancer = 4, Mesmer = 5, Elementalist = 6, Assassin = 7, Ritualist = 8, Paragon = 9, Dervish = 10 };
                switch (id)
                {
                    default: return Skill.Professions.None;
                    case 1: return Skill.Professions.Warrior;
                    case 2: return Skill.Professions.Ranger;
                    case 3: return Skill.Professions.Monk;
                    case 4: return Skill.Professions.Necromancer;
                    case 5: return Skill.Professions.Mesmer;
                    case 6: return Skill.Professions.Elementalist;
                    case 7: return Skill.Professions.Assassin;
                    case 8: return Skill.Professions.Ritualist;
                    case 9: return Skill.Professions.Paragon;
                    case 10: return Skill.Professions.Dervish;
                }
            }

            Skill.Attributes GetAttributeFromID(int id)
            {
                switch (id)
                {
                    default: return Skill.Attributes.None; // Shouldn't happen, but eh?
                    case 0: return Skill.Attributes.Fast_Casting;//Fast_Casting
                    case 1: return Skill.Attributes.Illusion_Magic;//Illusion_Magic
                    case 2: return Skill.Attributes.Domination_Magic;//Domination_Magic
                    case 3: return Skill.Attributes.Inspiration_Magic;//Inspiration_Magic
                    case 4: return Skill.Attributes.Blood_Magic;//Blood_Magic
                    case 5: return Skill.Attributes.Death_Magic;//Death_Magic
                    case 6: return Skill.Attributes.Soul_Reaping;//Soul_Reaping
                    case 7: return Skill.Attributes.Curses;//Curses
                    case 8: return Skill.Attributes.Air_Magic;//Air_Magic
                    case 9: return Skill.Attributes.Earth_Magic;//Earth_Magic
                    case 10: return Skill.Attributes.Fire_Magic;//Fire_Magic
                    case 11: return Skill.Attributes.Water_Magic;//Water_Magic
                    case 12: return Skill.Attributes.Energy_Storage;//Energy_Storage
                    case 13: return Skill.Attributes.Healing_Prayers;//Healing_Prayers
                    case 14: return Skill.Attributes.Smiting_Prayers;//Smiting_Prayers
                    case 15: return Skill.Attributes.Protection_Prayers;//Protection_Prayers
                    case 16: return Skill.Attributes.Divine_Favor;//Divine_Favor
                    case 17: return Skill.Attributes.Strength;//Strength
                    case 18: return Skill.Attributes.Axe_Mastery;//Axe_Mastery
                    case 19: return Skill.Attributes.Hammer_Mastery;//Hammer_Mastery
                    case 20: return Skill.Attributes.Swordsmanship;//Swordsmanship
                    case 21: return Skill.Attributes.Tactics;//Tactics
                    case 22: return Skill.Attributes.Beast_Mastery;//Beast_Mastery
                    case 23: return Skill.Attributes.Expertise;//Expertise
                    case 24: return Skill.Attributes.Wilderness_Survival;//Wilderness_Survival
                    case 25: return Skill.Attributes.Marksmanship;//Marksmanship
                    case 29: return Skill.Attributes.Dagger_Mastery;//Dagger_Mastery = 29 <-- I have no idea why this is, but for some reason, there's a skip between Marksmanship and Dagger Mastery. Probably an abandoned profession idea or something?
                    case 30: return Skill.Attributes.Deadly_Arts;//Deadly_Arts
                    case 31: return Skill.Attributes.Shadow_Arts;//Shadow_Arts
                    case 32: return Skill.Attributes.Communing;//Communing
                    case 33: return Skill.Attributes.Restoration_Magic;//Restoration_Magic
                    case 34: return Skill.Attributes.Channeling_Magic;//Channeling_Magic
                    case 35: return Skill.Attributes.Critical_Strikes;//Critical_Strikes
                    case 36: return Skill.Attributes.Spawning_Power;//Spawning_Power
                    case 37: return Skill.Attributes.Spear_Mastery;//Spear_Mastery
                    case 38: return Skill.Attributes.Command;//Command
                    case 39: return Skill.Attributes.Motivation;//Motivation
                    case 40: return Skill.Attributes.Leadership;//Leadership
                    case 41: return Skill.Attributes.Scythe_Mastery;//Scythe_Mastery
                    case 42: return Skill.Attributes.Wind_Prayers;//Wind_Prayers
                    case 43: return Skill.Attributes.Earth_Prayers;//Earth_Prayers
                    case 44: return Skill.Attributes.Mysticism;//Mysticism
                    case 45: //Ebon_Vanguard <-- I'm including these here for no particular reason, since you can't technically put points in them!
                    case 46: //Norn
                    case 47: //Dwarven
                    case 48: //Asuran
                    case 49: //Lightbringer
                    case 50: //Allegiance
                    case 51: //Sunspear
                        return Skill.Attributes.PvE_Only;
                }
            }
            static public byte[] TranslateString(string str)
            {
                // Start by converting the string into a bitlist:
                List<bool> bitlist = new List<bool>();
                foreach (char c in str.ToCharArray())
                {
                    int? value = TranslateChar(c);
                    if (value.HasValue)
                    {
                        // Note that this process flips them as it reads, as that is how GW does it. Yeah.
                        for (int bit = 0; bit < 6; ++bit)
                        {
                            bitlist.Add(((value.Value >> bit) & 1) == 1);
                        }
                    }
                }

                // Then figure out how many bytes we're returning:
                int numBytes = bitlist.Count / 8; if (bitlist.Count % 8 != 0) numBytes++;
                byte[] ret = new byte[numBytes];
                for (int i = 0; i < numBytes; ++i) ret[i] = 0;

                // Iterate over the bits and put them into the array:
                int currentByte = 0;
                int currentBit = 7;
                foreach (bool b in bitlist)
                {
                    ret[currentByte] |= (byte)((b ? 1 : 0) << currentBit);
                    currentBit--;
                    if (currentBit < 0)
                    {
                        currentBit = 7;
                        currentByte++;
                    }
                }

                return ret;
            }

            static private int? TranslateChar(char c)
            {
                if (c >= 'A' && c <= 'Z') return c - 'A';
                if (c >= 'a' && c <= 'z') return (c - 'a') + 26;
                if (c >= '0' && c <= '9') return (c - '0') + 52;
                if (c == '+') return 62;
                if (c == '/') return 63;
                return null;
                /*
            0	A	16	Q	32	g	48	w
            1	B	17	R	33	h	49	x
            2	C	18	S	34	i	50	y
            3	D	19	T	35	j	51	z
            4	E	20	U	36	k	52	0
            5	F	21	V	37	l	53	1
            6	G	22	W	38	m	54	2
            7	H	23	X	39	n	55	3
            8	I	24	Y	40	o	56	4
            9	J	25	Z	41	p	57	5
            10	K	26	a	42	q	58	6
            11	L	27	b	43	r	59	7
            12	M	28	c	44	s	60	8
            13	N	29	d	45	t	61	9
            14	O	30	e	46	u	62	+
            15	P	31	f	47	v	63	/
                 */
            }

            static private string TranslateBitArray(BitArray bits)
            {
                string[] Character = {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z",
                                     "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z",
                                     "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "+", "/"};
                string ret = "";
                int pos = 0, len = bits.Length;
                bool looping = true;
                while (looping)
                {
                    int bitsToRead = len - pos; if (bitsToRead > 6) bitsToRead = 6;
                    ret += Character[bits.Get(pos, bitsToRead)];
                    pos += bitsToRead;
                    if (pos >= len) looping = false;
                }
                return ret;
            }

            class BitArray
            {
                bool[] _Bits = new bool[0];
                public BitArray(byte[] bytes)
                {
                    if (bytes == null) return;
                    _Bits = new bool[bytes.Length * 8];
                    int index = 0;
                    foreach (byte b in bytes)
                    {
                        for (int bit = 7; bit >= 0; --bit)
                            _Bits[index++] = ((b >> bit) & 1) == 1;
                    }
                }

                /// <summary>
                /// Gets bits. Flips them. Because that is how GW templates work. Don't ask ME why; it's probably because it's easier to code that way.
                /// </summary>
                /// <param name="index">The index of the bit to start reading.</param>
                /// <param name="bits">The number of bits to read.</param>
                /// <returns>A integer containg the bits.</returns>
                virtual public int Get(int index, int bits)
                {
                    int ret = 0;
                    for (int i = 0; i < bits; ++i)
                    {
                        if (_Bits[index + i])
                        {
                            ret |= (1 << i);
                        }
                    }
                    return ret;
                }

                virtual public int Length { get { return _Bits.Length; } }
            }

            class BitReader : BitArray
            {
                int _Position = 0;
                public BitReader(byte[] bytes) : base(bytes) { }
                public void ResetPosition() { _Position = 0; }
                public int Read(int numBits)
                {
                    int ret = Get(_Position, numBits);
                    _Position += numBits;
                    return ret;
                }
            }

            class BitWriter : BitArray
            {
                List<bool> _Bits = new List<bool>();
                public BitWriter() : base(null) { }

                public override int Get(int index, int bits)
                {
                    int ret = 0;
                    for (int i = 0; i < bits; ++i)
                    {
                        if (_Bits[index + i])
                        {
                            ret |= (1 << i);
                        }
                    }
                    return ret;
                }

                /// <summary>
                /// Adds a bit to the end of the bit array.
                /// </summary>
                /// <param name="bit">True if 1, false if 0.</param>
                public void Write(bool bit) { _Bits.Add(bit); }

                /// <summary>
                /// Writes the "numBits" rightmost bits into the bitarray.
                /// </summary>
                /// <param name="value">A value.</param>
                /// <param name="numBits">The number of bits [0-32) from the right to add in.</param>
                public void Write(int value, int numBits)
                {
                    for (int i = 0; i < numBits; ++i)
                    {
                        _Bits.Add(((value >> i) & 1) == 1);
                    }
                }

                public bool this[int index] { get { return _Bits[index]; } set { _Bits[index] = value; } }

                public override int Length { get { return _Bits.Count; } }
            }
            #endregion
        }

        static public Skill GetSkillByEngineID(int id)
        {
            switch (id)
            {
                default: return null;
                case 1: return Data[0];
                case 2: return Data[1];
                //case 3: return Data[-1] // Signet of Capture, which I'm going to exclude because I don't allow the signet of capture!
                case 5: return Data[2];
                case 6: return Data[3];
                case 7: return Data[4];
                case 8: return Data[5];
                case 9: return Data[6];
                case 10: return Data[7];
                case 11: return Data[8];
                case 13: return Data[9];
                case 14: return Data[10];
                case 15: return Data[11];
                case 16: return Data[12];
                case 17: return Data[13];
                case 18: return Data[14];
                case 19: return Data[15];
                case 21: return Data[16];
                case 22: return Data[17];
                case 23: return Data[18];
                case 24: return Data[19];
                case 25: return Data[20];
                case 26: return Data[21];
                case 27: return Data[22];
                case 28: return Data[23];
                case 29: return Data[24];
                case 30: return Data[25];
                case 31: return Data[26];
                case 32: return Data[27];
                case 33: return Data[28];
                case 34: return Data[29];
                case 35: return Data[30];
                case 36: return Data[31];
                case 37: return Data[32];
                case 38: return Data[33];
                case 39: return Data[34];
                case 40: return Data[35];
                case 41: return Data[36];
                case 42: return Data[37];
                case 43: return Data[38];
                case 44: return Data[39];
                case 45: return Data[40];
                case 46: return Data[41];
                case 47: return Data[42];
                case 48: return Data[43];
                case 49: return Data[44];
                case 50: return Data[45];
                case 51: return Data[46];
                case 52: return Data[47];
                case 53: return Data[48];
                case 54: return Data[49];
                case 55: return Data[50];
                case 56: return Data[51];
                case 57: return Data[52];
                case 58: return Data[53];
                case 59: return Data[54];
                case 61: return Data[55];
                case 62: return Data[56];
                case 63: return Data[57];
                case 65: return Data[58];
                case 66: return Data[59];
                case 67: return Data[60];
                case 68: return Data[61];
                case 69: return Data[62];
                case 72: return Data[63];
                case 73: return Data[64];
                case 74: return Data[65];
                case 75: return Data[66];
                case 76: return Data[67];
                case 77: return Data[68];
                case 78: return Data[69];
                case 79: return Data[70];
                case 80: return Data[71];
                case 81: return Data[72];
                case 82: return Data[73];
                case 83: return Data[74];
                case 84: return Data[75];
                case 85: return Data[76];
                case 86: return Data[77];
                case 87: return Data[78];
                case 88: return Data[79];
                case 89: return Data[80];
                case 90: return Data[81];
                case 91: return Data[82];
                case 92: return Data[83];
                case 93: return Data[84];
                case 94: return Data[85];
                case 95: return Data[86];
                case 96: return Data[87];
                case 97: return Data[88];
                case 98: return Data[89];
                case 99: return Data[90];
                case 100: return Data[91];
                case 101: return Data[92];
                case 102: return Data[93];
                case 103: return Data[94];
                case 104: return Data[95];
                case 105: return Data[96];
                case 106: return Data[97];
                case 107: return Data[98];
                case 108: return Data[99];
                case 109: return Data[100];
                case 110: return Data[101];
                case 111: return Data[102];
                case 112: return Data[103];
                case 113: return Data[104];
                case 114: return Data[105];
                case 115: return Data[106];
                case 116: return Data[107];
                case 117: return Data[108];
                case 118: return Data[109];
                case 119: return Data[110];
                case 120: return Data[111];
                case 121: return Data[112];
                case 122: return Data[113];
                case 123: return Data[114];
                case 124: return Data[115];
                case 125: return Data[116];
                case 126: return Data[117];
                case 127: return Data[118];
                case 128: return Data[119];
                case 129: return Data[120];
                case 130: return Data[121];
                case 131: return Data[122];
                case 132: return Data[123];
                case 133: return Data[124];
                case 134: return Data[125];
                case 135: return Data[126];
                case 136: return Data[127];
                case 137: return Data[128];
                case 138: return Data[129];
                case 139: return Data[130];
                case 140: return Data[131];
                case 141: return Data[132];
                case 142: return Data[133];
                case 143: return Data[134];
                case 144: return Data[135];
                case 145: return Data[136];
                case 146: return Data[137];
                case 147: return Data[138];
                case 148: return Data[139];
                case 149: return Data[140];
                case 150: return Data[141];
                case 151: return Data[142];
                case 152: return Data[143];
                case 153: return Data[144];
                case 154: return Data[145];
                case 155: return Data[146];
                case 156: return Data[147];
                case 157: return Data[148];
                case 158: return Data[149];
                case 159: return Data[150];
                case 160: return Data[151];
                case 162: return Data[152];
                case 163: return Data[153];
                case 164: return Data[154];
                case 165: return Data[155];
                case 166: return Data[156];
                case 167: return Data[157];
                case 168: return Data[158];
                case 169: return Data[159];
                case 170: return Data[160];
                case 171: return Data[161];
                case 172: return Data[162];
                case 173: return Data[163];
                case 174: return Data[164];
                case 175: return Data[165];
                case 176: return Data[166];
                case 177: return Data[167];
                case 178: return Data[168];
                case 179: return Data[169];
                case 180: return Data[170];
                case 181: return Data[171];
                case 182: return Data[172];
                case 183: return Data[173];
                case 184: return Data[174];
                case 185: return Data[175];
                case 186: return Data[176];
                case 187: return Data[177];
                case 188: return Data[178];
                case 189: return Data[179];
                case 190: return Data[180];
                case 191: return Data[181];
                case 192: return Data[182];
                case 193: return Data[183];
                case 194: return Data[184];
                case 195: return Data[185];
                case 196: return Data[186];
                case 197: return Data[187];
                case 198: return Data[188];
                case 199: return Data[189];
                case 200: return Data[190];
                case 201: return Data[191];
                case 202: return Data[192];
                case 203: return Data[193];
                case 204: return Data[194];
                case 205: return Data[195];
                case 206: return Data[196];
                case 207: return Data[197];
                case 208: return Data[198];
                case 209: return Data[199];
                case 210: return Data[200];
                case 211: return Data[201];
                case 212: return Data[202];
                case 213: return Data[203];
                case 214: return Data[204];
                case 215: return Data[205];
                case 216: return Data[206];
                case 217: return Data[207];
                case 218: return Data[208];
                case 219: return Data[209];
                case 220: return Data[210];
                case 221: return Data[211];
                case 222: return Data[212];
                case 223: return Data[213];
                case 224: return Data[214];
                case 225: return Data[215];
                case 226: return Data[216];
                case 227: return Data[217];
                case 228: return Data[218];
                case 229: return Data[219];
                case 230: return Data[220];
                case 231: return Data[221];
                case 232: return Data[222];
                case 233: return Data[223];
                case 234: return Data[224];
                case 235: return Data[225];
                case 236: return Data[226];
                case 237: return Data[227];
                case 238: return Data[228];
                case 239: return Data[229];
                case 240: return Data[230];
                case 241: return Data[231];
                case 242: return Data[232];
                case 243: return Data[233];
                case 244: return Data[234];
                case 245: return Data[235];
                case 246: return Data[236];
                case 247: return Data[237];
                case 248: return Data[238];
                case 249: return Data[239];
                case 250: return Data[240];
                case 251: return Data[241];
                case 252: return Data[242];
                case 253: return Data[243];
                case 254: return Data[244];
                case 255: return Data[245];
                case 256: return Data[246];
                case 257: return Data[247];
                case 258: return Data[248];
                case 259: return Data[249];
                case 260: return Data[250];
                case 261: return Data[251];
                case 262: return Data[252];
                case 263: return Data[253];
                case 264: return Data[254];
                case 265: return Data[255];
                case 266: return Data[256];
                case 267: return Data[257];
                case 268: return Data[258];
                case 269: return Data[259];
                case 270: return Data[260];
                case 271: return Data[261];
                case 272: return Data[262];
                case 273: return Data[263];
                case 274: return Data[264];
                case 275: return Data[265];
                case 276: return Data[266];
                case 277: return Data[267];
                case 278: return Data[268];
                case 279: return Data[269];
                case 280: return Data[270];
                case 281: return Data[271];
                case 282: return Data[272];
                case 283: return Data[273];
                case 284: return Data[274];
                case 285: return Data[275];
                case 286: return Data[276];
                case 287: return Data[277];
                case 288: return Data[278];
                case 289: return Data[279];
                case 290: return Data[280];
                case 291: return Data[281];
                case 292: return Data[282];
                case 293: return Data[283];
                case 294: return Data[284];
                case 295: return Data[285];
                case 296: return Data[286];
                case 297: return Data[287];
                case 298: return Data[288];
                case 299: return Data[289];
                case 300: return Data[290];
                case 301: return Data[291];
                case 302: return Data[292];
                case 303: return Data[293];
                case 304: return Data[294];
                case 305: return Data[295];
                case 306: return Data[296];
                case 307: return Data[297];
                case 308: return Data[298];
                case 309: return Data[299];
                case 310: return Data[300];
                case 311: return Data[301];
                case 312: return Data[302];
                case 313: return Data[303];
                case 314: return Data[304];
                case 315: return Data[305];
                case 316: return Data[306];
                case 317: return Data[307];
                case 318: return Data[308];
                case 319: return Data[309];
                case 320: return Data[310];
                case 321: return Data[311];
                case 322: return Data[312];
                case 323: return Data[313];
                case 324: return Data[314];
                case 325: return Data[315];
                case 326: return Data[316];
                case 327: return Data[317];
                case 328: return Data[318];
                case 329: return Data[319];
                case 330: return Data[320];
                case 331: return Data[321];
                case 332: return Data[322];
                case 333: return Data[323];
                case 334: return Data[324];
                case 335: return Data[325];
                case 336: return Data[326];
                case 337: return Data[327];
                case 338: return Data[328];
                case 339: return Data[329];
                case 340: return Data[330];
                case 341: return Data[331];
                case 342: return Data[332];
                case 343: return Data[333];
                case 344: return Data[334];
                case 345: return Data[335];
                case 346: return Data[336];
                case 347: return Data[337];
                case 348: return Data[338];
                case 349: return Data[339];
                case 350: return Data[340];
                case 351: return Data[341];
                case 352: return Data[342];
                case 353: return Data[343];
                case 354: return Data[344];
                case 355: return Data[345];
                case 356: return Data[346];
                case 357: return Data[347];
                case 358: return Data[348];
                case 359: return Data[349];
                case 360: return Data[350];
                case 361: return Data[351];
                case 362: return Data[352];
                case 363: return Data[353];
                case 364: return Data[354];
                case 365: return Data[355];
                case 366: return Data[356];
                case 367: return Data[357];
                case 368: return Data[358];
                case 370: return Data[359];
                case 371: return Data[360];
                case 372: return Data[361];
                case 373: return Data[362];
                case 374: return Data[363];
                case 375: return Data[364];
                case 376: return Data[365];
                case 377: return Data[366];
                case 378: return Data[367];
                case 379: return Data[368];
                case 380: return Data[369];
                case 381: return Data[370];
                case 382: return Data[371];
                case 383: return Data[372];
                case 384: return Data[373];
                case 385: return Data[374];
                case 386: return Data[375];
                case 387: return Data[376];
                case 388: return Data[377];
                case 389: return Data[378];
                case 390: return Data[379];
                case 391: return Data[380];
                case 392: return Data[381];
                case 393: return Data[382];
                case 394: return Data[383];
                case 395: return Data[384];
                case 396: return Data[385];
                case 397: return Data[386];
                case 398: return Data[387];
                case 399: return Data[388];
                case 400: return Data[389];
                case 402: return Data[390];
                case 403: return Data[391];
                case 404: return Data[392];
                case 405: return Data[393];
                case 406: return Data[394];
                case 407: return Data[395];
                case 408: return Data[396];
                case 409: return Data[397];
                case 411: return Data[398];
                case 412: return Data[399];
                case 415: return Data[400];
                case 422: return Data[401];
                case 423: return Data[402];
                case 424: return Data[403];
                case 425: return Data[404];
                case 426: return Data[405];
                case 427: return Data[406];
                case 428: return Data[407];
                case 429: return Data[408];
                case 430: return Data[409];
                case 431: return Data[410];
                case 432: return Data[411];
                case 433: return Data[412];
                case 434: return Data[413];
                case 435: return Data[414];
                case 436: return Data[415];
                case 437: return Data[416];
                case 438: return Data[417];
                case 439: return Data[418];
                case 440: return Data[419];
                case 441: return Data[420];
                case 442: return Data[421];
                case 443: return Data[422];
                case 444: return Data[423];
                case 445: return Data[424];
                case 446: return Data[425];
                case 447: return Data[426];
                case 448: return Data[427];
                case 449: return Data[428];
                case 450: return Data[429];
                case 451: return Data[430];
                case 452: return Data[431];
                case 453: return Data[432];
                case 454: return Data[433];
                case 455: return Data[434];
                case 456: return Data[435];
                case 457: return Data[436];
                case 458: return Data[437];
                case 459: return Data[438];
                case 460: return Data[439];
                case 461: return Data[440];
                case 462: return Data[441];
                case 463: return Data[442];
                case 464: return Data[443];
                case 465: return Data[444];
                case 466: return Data[445];
                case 467: return Data[446];
                case 468: return Data[447];
                case 469: return Data[448];
                case 470: return Data[449];
                case 471: return Data[450];
                case 472: return Data[451];
                case 474: return Data[452];
                case 475: return Data[453];
                case 476: return Data[454];
                case 477: return Data[455];
                case 570: return Data[456];
                case 571: return Data[457];
                case 572: return Data[458];
                case 763: return Data[459];
                case 764: return Data[460];
                case 766: return Data[461];
                case 769: return Data[462];
                case 770: return Data[463];
                case 771: return Data[464];
                case 772: return Data[465];
                case 773: return Data[466];
                case 775: return Data[467];
                case 776: return Data[468];
                case 777: return Data[469];
                case 778: return Data[470];
                case 779: return Data[471];
                case 780: return Data[472];
                case 781: return Data[473];
                case 782: return Data[474];
                case 783: return Data[475];
                case 784: return Data[476];
                case 785: return Data[477];
                case 786: return Data[478];
                case 787: return Data[479];
                case 788: return Data[480];
                case 789: return Data[481];
                case 790: return Data[482];
                case 791: return Data[483];
                case 792: return Data[484];
                case 793: return Data[485];
                case 794: return Data[486];
                case 795: return Data[487];
                case 799: return Data[488];
                case 800: return Data[489];
                case 801: return Data[490];
                case 802: return Data[491];
                case 803: return Data[492];
                case 804: return Data[493];
                case 805: return Data[494];
                case 806: return Data[495];
                case 808: return Data[496];
                case 809: return Data[497];
                case 810: return Data[498];
                case 811: return Data[499];
                case 812: return Data[500];
                case 813: return Data[501];
                case 814: return Data[502];
                case 815: return Data[503];
                case 816: return Data[504];
                case 817: return Data[505];
                case 818: return Data[506];
                case 819: return Data[507];
                case 820: return Data[508];
                case 821: return Data[509];
                case 822: return Data[510];
                case 823: return Data[511];
                case 824: return Data[512];
                case 825: return Data[513];
                case 826: return Data[514];
                case 827: return Data[515];
                case 828: return Data[516];
                case 830: return Data[517];
                case 831: return Data[518];
                case 832: return Data[519];
                case 834: return Data[520];
                case 835: return Data[521];
                case 836: return Data[522];
                case 837: return Data[523];
                case 838: return Data[524];
                case 839: return Data[525];
                case 840: return Data[526];
                case 841: return Data[527];
                case 842: return Data[528];
                case 843: return Data[529];
                case 844: return Data[530];
                case 845: return Data[531];
                case 846: return Data[532];
                case 847: return Data[533];
                case 848: return Data[534];
                case 849: return Data[535];
                case 850: return Data[536];
                case 851: return Data[537];
                case 852: return Data[538];
                case 853: return Data[539];
                case 854: return Data[540];
                case 858: return Data[541];
                case 859: return Data[542];
                case 860: return Data[543];
                case 862: return Data[544];
                case 863: return Data[545];
                case 864: return Data[546];
                case 865: return Data[547];
                case 866: return Data[548];
                case 867: return Data[549];
                case 869: return Data[550];
                case 870: return Data[551];
                case 871: return Data[552];
                case 876: return Data[553];
                case 877: return Data[554];
                case 878: return Data[555];
                case 879: return Data[556];
                case 880: return Data[557];
                case 881: return Data[558];
                case 882: return Data[559];
                case 883: return Data[560];
                case 884: return Data[561];
                case 885: return Data[562];
                case 886: return Data[563];
                case 887: return Data[564];
                case 888: return Data[565];
                case 889: return Data[566];
                case 891: return Data[567];
                case 892: return Data[568];
                case 893: return Data[569];
                case 898: return Data[570];
                case 899: return Data[571];
                case 900: return Data[572];
                case 901: return Data[573];
                case 902: return Data[574];
                case 903: return Data[575];
                case 904: return Data[576];
                case 905: return Data[577];
                case 906: return Data[578];
                case 907: return Data[579];
                case 908: return Data[580];
                case 909: return Data[581];
                case 910: return Data[582];
                case 911: return Data[583];
                case 913: return Data[584];
                case 914: return Data[585];
                case 915: return Data[586];
                case 916: return Data[587];
                case 917: return Data[588];
                case 918: return Data[589];
                case 919: return Data[590];
                case 920: return Data[591];
                case 921: return Data[592];
                case 923: return Data[593];
                case 925: return Data[594];
                case 926: return Data[595];
                case 927: return Data[596];
                case 928: return Data[597];
                case 929: return Data[598];
                case 930: return Data[599];
                case 931: return Data[600];
                case 932: return Data[601];
                case 933: return Data[602];
                case 934: return Data[603];
                case 935: return Data[604];
                case 936: return Data[605];
                case 937: return Data[606];
                case 938: return Data[607];
                case 939: return Data[608];
                case 941: return Data[609];
                case 942: return Data[610];
                case 943: return Data[611];
                case 944: return Data[612];
                case 946: return Data[613];
                case 947: return Data[614];
                case 948: return Data[615];
                case 949: return Data[616];
                case 950: return Data[617];
                case 951: return Data[618];
                case 952: return Data[619];
                case 953: return Data[620];
                case 954: return Data[621];
                case 955: return Data[622];
                case 957: return Data[623];
                case 958: return Data[624];
                case 959: return Data[625];
                case 960: return Data[626];
                case 961: return Data[627];
                case 962: return Data[628];
                case 963: return Data[629];
                case 964: return Data[630];
                case 973: return Data[631];
                case 974: return Data[632];
                case 975: return Data[633];
                case 976: return Data[634];
                case 977: return Data[635];
                case 978: return Data[636];
                case 979: return Data[637];
                case 980: return Data[638];
                case 981: return Data[639];
                case 982: return Data[640];
                case 983: return Data[641];
                case 985: return Data[642];
                case 986: return Data[643];
                case 987: return Data[644];
                case 988: return Data[645];
                case 989: return Data[646];
                case 990: return Data[647];
                case 991: return Data[648];
                case 992: return Data[649];
                case 993: return Data[650];
                case 994: return Data[651];
                case 995: return Data[652];
                case 996: return Data[653];
                case 997: return Data[654];
                case 1018: return Data[655];
                case 1019: return Data[656];
                case 1020: return Data[657];
                case 1021: return Data[658];
                case 1022: return Data[659];
                case 1023: return Data[660];
                case 1024: return Data[661];
                case 1025: return Data[662];
                case 1026: return Data[663];
                case 1027: return Data[664];
                case 1028: return Data[665];
                case 1029: return Data[666];
                case 1030: return Data[667];
                case 1031: return Data[668];
                case 1032: return Data[669];
                case 1033: return Data[670];
                case 1034: return Data[671];
                case 1035: return Data[672];
                case 1036: return Data[673];
                case 1037: return Data[674];
                case 1038: return Data[675];
                case 1040: return Data[676];
                case 1041: return Data[677];
                case 1042: return Data[678];
                case 1043: return Data[679];
                case 1044: return Data[680];
                case 1045: return Data[681];
                case 1048: return Data[682];
                case 1049: return Data[683];
                case 1052: return Data[684];
                case 1053: return Data[685];
                case 1054: return Data[686];
                case 1055: return Data[687];
                case 1056: return Data[688];
                case 1057: return Data[689];
                case 1059: return Data[690];
                case 1061: return Data[691];
                case 1062: return Data[692];
                case 1066: return Data[693];
                case 1067: return Data[694];
                case 1068: return Data[695];
                case 1069: return Data[696];
                case 1070: return Data[697];
                case 1071: return Data[698];
                case 1075: return Data[699];
                case 1076: return Data[700];
                case 1077: return Data[701];
                case 1078: return Data[702];
                case 1079: return Data[703];
                case 1081: return Data[704];
                case 1082: return Data[705];
                case 1083: return Data[706];
                case 1084: return Data[707];
                case 1085: return Data[708];
                case 1086: return Data[709];
                case 1088: return Data[710];
                case 1090: return Data[711];
                case 1091: return Data[712];
                case 1093: return Data[713];
                case 1094: return Data[714];
                case 1095: return Data[715];
                case 1096: return Data[716];
                case 1097: return Data[717];
                case 1098: return Data[718];
                case 1099: return Data[719];
                case 1113: return Data[720];
                case 1114: return Data[721];
                case 1115: return Data[722];
                case 1117: return Data[723];
                case 1118: return Data[724];
                case 1119: return Data[725];
                case 1120: return Data[726];
                case 1121: return Data[727];
                case 1123: return Data[728];
                case 1126: return Data[729];
                case 1128: return Data[730];
                case 1129: return Data[731];
                case 1130: return Data[732];
                case 1131: return Data[733];
                case 1133: return Data[734];
                case 1134: return Data[735];
                case 1135: return Data[736];
                case 1136: return Data[737];
                case 1137: return Data[738];
                case 1141: return Data[739];
                case 1142: return Data[740];
                case 1144: return Data[741];
                case 1146: return Data[742];
                case 1191: return Data[743];
                case 1192: return Data[744];
                case 1194: return Data[745];
                case 1195: return Data[746];
                case 1196: return Data[747];
                case 1197: return Data[748];
                case 1198: return Data[749];
                case 1199: return Data[750];
                case 1200: return Data[751];
                case 1201: return Data[752];
                case 1202: return Data[753];
                case 1203: return Data[754];
                case 1205: return Data[755];
                case 1206: return Data[756];
                case 1209: return Data[757];
                case 1211: return Data[758];
                case 1212: return Data[759];
                case 1213: return Data[760];
                case 1215: return Data[761];
                case 1217: return Data[762];
                case 1218: return Data[763];
                case 1219: return Data[764];
                case 1220: return Data[765];
                case 1221: return Data[766];
                case 1222: return Data[767];
                case 1223: return Data[768];
                case 1224: return Data[769];
                case 1225: return Data[770];
                case 1226: return Data[771];
                case 1227: return Data[772];
                case 1228: return Data[773];
                case 1229: return Data[774];
                case 1230: return Data[775];
                case 1231: return Data[776];
                case 1232: return Data[777];
                case 1233: return Data[778];
                case 1234: return Data[779];
                case 1235: return Data[780];
                case 1236: return Data[781];
                case 1237: return Data[782];
                case 1238: return Data[783];
                case 1239: return Data[784];
                case 1240: return Data[785];
                case 1244: return Data[786];
                case 1245: return Data[787];
                case 1246: return Data[788];
                case 1247: return Data[789];
                case 1249: return Data[790];
                case 1250: return Data[791];
                case 1251: return Data[792];
                case 1252: return Data[793];
                case 1253: return Data[794];
                case 1255: return Data[795];
                case 1257: return Data[796];
                case 1258: return Data[797];
                case 1259: return Data[798];
                case 1260: return Data[799];
                case 1261: return Data[800];
                case 1262: return Data[801];
                case 1263: return Data[802];
                case 1264: return Data[803];
                case 1265: return Data[804];
                case 1266: return Data[805];
                case 1267: return Data[806];
                case 1268: return Data[807];
                case 1269: return Data[808];
                case 1333: return Data[809];
                case 1334: return Data[810];
                case 1335: return Data[811];
                case 1336: return Data[812];
                case 1337: return Data[813];
                case 1338: return Data[814];
                case 1339: return Data[815];
                case 1340: return Data[816];
                case 1341: return Data[817];
                case 1342: return Data[818];
                case 1343: return Data[819];
                case 1344: return Data[820];
                case 1345: return Data[821];
                case 1346: return Data[822];
                case 1347: return Data[823];
                case 1348: return Data[824];
                case 1349: return Data[825];
                case 1350: return Data[826];
                case 1351: return Data[827];
                case 1352: return Data[828];
                case 1353: return Data[829];
                case 1354: return Data[830];
                case 1355: return Data[831];
                case 1356: return Data[832];
                case 1358: return Data[833];
                case 1359: return Data[834];
                case 1360: return Data[835];
                case 1362: return Data[836];
                case 1363: return Data[837];
                case 1364: return Data[838];
                case 1365: return Data[839];
                case 1366: return Data[840];
                case 1367: return Data[841];
                case 1368: return Data[842];
                case 1369: return Data[843];
                case 1370: return Data[844];
                case 1371: return Data[845];
                case 1372: return Data[846];
                case 1373: return Data[847];
                case 1374: return Data[848];
                case 1375: return Data[849];
                case 1376: return Data[850];
                case 1377: return Data[851];
                case 1378: return Data[852];
                case 1379: return Data[853];
                case 1380: return Data[854];
                case 1381: return Data[855];
                case 1382: return Data[856];
                case 1390: return Data[857];
                case 1391: return Data[858];
                case 1392: return Data[859];
                case 1393: return Data[860];
                case 1394: return Data[861];
                case 1395: return Data[862];
                case 1396: return Data[863];
                case 1397: return Data[864];
                case 1398: return Data[865];
                case 1399: return Data[866];
                case 1400: return Data[867];
                case 1401: return Data[868];
                case 1402: return Data[869];
                case 1403: return Data[870];
                case 1404: return Data[871];
                case 1405: return Data[872];
                case 1406: return Data[873];
                case 1407: return Data[874];
                case 1408: return Data[875];
                case 1409: return Data[876];
                case 1410: return Data[877];
                case 1411: return Data[878];
                case 1412: return Data[879];
                case 1413: return Data[880];
                case 1414: return Data[881];
                case 1415: return Data[882];
                case 1416: return Data[883];
                case 1465: return Data[884];
                case 1466: return Data[885];
                case 1467: return Data[886];
                case 1468: return Data[887];
                case 1469: return Data[888];
                case 1470: return Data[889];
                case 1471: return Data[890];
                case 1472: return Data[891];
                case 1473: return Data[892];
                case 1474: return Data[893];
                case 1475: return Data[894];
                case 1476: return Data[895];
                case 1478: return Data[896];
                case 1479: return Data[897];
                case 1480: return Data[898];
                case 1481: return Data[899];
                case 1482: return Data[900];
                case 1483: return Data[901];
                case 1484: return Data[902];
                case 1485: return Data[903];
                case 1486: return Data[904];
                case 1487: return Data[905];
                case 1488: return Data[906];
                case 1489: return Data[907];
                case 1490: return Data[908];
                case 1491: return Data[909];
                case 1493: return Data[910];
                case 1495: return Data[911];
                case 1496: return Data[912];
                case 1497: return Data[913];
                case 1498: return Data[914];
                case 1499: return Data[915];
                case 1500: return Data[916];
                case 1502: return Data[917];
                case 1503: return Data[918];
                case 1504: return Data[919];
                case 1505: return Data[920];
                case 1506: return Data[921];
                case 1507: return Data[922];
                case 1508: return Data[923];
                case 1509: return Data[924];
                case 1510: return Data[925];
                case 1512: return Data[926];
                case 1513: return Data[927];
                case 1514: return Data[928];
                case 1515: return Data[929];
                case 1516: return Data[930];
                case 1517: return Data[931];
                case 1518: return Data[932];
                case 1519: return Data[933];
                case 1520: return Data[934];
                case 1521: return Data[935];
                case 1522: return Data[936];
                case 1523: return Data[937];
                case 1524: return Data[938];
                case 1525: return Data[939];
                case 1526: return Data[940];
                case 1527: return Data[941];
                case 1528: return Data[942];
                case 1529: return Data[943];
                case 1530: return Data[944];
                case 1531: return Data[945];
                case 1532: return Data[946];
                case 1533: return Data[947];
                case 1534: return Data[948];
                case 1535: return Data[949];
                case 1536: return Data[950];
                case 1537: return Data[951];
                case 1538: return Data[952];
                case 1539: return Data[953];
                case 1540: return Data[954];
                case 1541: return Data[955];
                case 1542: return Data[956];
                case 1543: return Data[957];
                case 1544: return Data[958];
                case 1545: return Data[959];
                case 1546: return Data[960];
                case 1547: return Data[961];
                case 1548: return Data[962];
                case 1549: return Data[963];
                case 1550: return Data[964];
                case 1551: return Data[965];
                case 1552: return Data[966];
                case 1553: return Data[967];
                case 1554: return Data[968];
                case 1555: return Data[969];
                case 1556: return Data[970];
                case 1557: return Data[971];
                case 1558: return Data[972];
                case 1559: return Data[973];
                case 1560: return Data[974];
                case 1561: return Data[975];
                case 1562: return Data[976];
                case 1563: return Data[977];
                case 1564: return Data[978];
                case 1565: return Data[979];
                case 1566: return Data[980];
                case 1567: return Data[981];
                case 1568: return Data[982];
                case 1569: return Data[983];
                case 1570: return Data[984];
                case 1571: return Data[985];
                case 1572: return Data[986];
                case 1573: return Data[987];
                case 1574: return Data[988];
                case 1575: return Data[989];
                case 1576: return Data[990];
                case 1577: return Data[991];
                case 1578: return Data[992];
                case 1579: return Data[993];
                case 1580: return Data[994];
                case 1581: return Data[995];
                case 1583: return Data[996];
                case 1584: return Data[997];
                case 1585: return Data[998];
                case 1586: return Data[999];
                case 1587: return Data[1000];
                case 1588: return Data[1001];
                case 1589: return Data[1002];
                case 1590: return Data[1003];
                case 1591: return Data[1004];
                case 1592: return Data[1005];
                case 1593: return Data[1006];
                case 1594: return Data[1007];
                case 1595: return Data[1008];
                case 1596: return Data[1009];
                case 1597: return Data[1010];
                case 1598: return Data[1011];
                case 1599: return Data[1012];
                case 1600: return Data[1013];
                case 1601: return Data[1014];
                case 1602: return Data[1015];
                case 1603: return Data[1016];
                case 1604: return Data[1017];
                case 1605: return Data[1018];
                case 1633: return Data[1019];
                case 1634: return Data[1020];
                case 1635: return Data[1021];
                case 1636: return Data[1022];
                case 1637: return Data[1023];
                case 1638: return Data[1024];
                case 1639: return Data[1025];
                case 1640: return Data[1026];
                case 1641: return Data[1027];
                case 1642: return Data[1028];
                case 1643: return Data[1029];
                case 1644: return Data[1030];
                case 1645: return Data[1031];
                case 1646: return Data[1032];
                case 1647: return Data[1033];
                case 1648: return Data[1034];
                case 1649: return Data[1035];
                case 1650: return Data[1036];
                case 1651: return Data[1037];
                case 1652: return Data[1038];
                case 1653: return Data[1039];
                case 1654: return Data[1040];
                case 1655: return Data[1041];
                case 1656: return Data[1042];
                case 1657: return Data[1043];
                case 1658: return Data[1044];
                case 1659: return Data[1045];
                case 1660: return Data[1046];
                case 1661: return Data[1047];
                case 1662: return Data[1048];
                case 1663: return Data[1049];
                case 1664: return Data[1050];
                case 1683: return Data[1051];
                case 1684: return Data[1052];
                case 1685: return Data[1053];
                case 1686: return Data[1054];
                case 1687: return Data[1055];
                case 1688: return Data[1056];
                case 1689: return Data[1057];
                case 1690: return Data[1058];
                case 1691: return Data[1059];
                case 1692: return Data[1060];
                case 1693: return Data[1061];
                case 1694: return Data[1062];
                case 1695: return Data[1063];
                case 1696: return Data[1064];
                case 1697: return Data[1065];
                case 1698: return Data[1066];
                case 1699: return Data[1067];
                case 1700: return Data[1068];
                case 1701: return Data[1069];
                case 1702: return Data[1070];
                case 1719: return Data[1071];
                case 1720: return Data[1072];
                case 1721: return Data[1073];
                case 1722: return Data[1074];
                case 1723: return Data[1075];
                case 1724: return Data[1076];
                case 1725: return Data[1077];
                case 1726: return Data[1078];
                case 1727: return Data[1079];
                case 1728: return Data[1080];
                case 1729: return Data[1081];
                case 1730: return Data[1082];
                case 1731: return Data[1083];
                case 1732: return Data[1084];
                case 1733: return Data[1085];
                case 1734: return Data[1086];
                case 1736: return Data[1087];
                case 1737: return Data[1088];
                case 1738: return Data[1089];
                case 1739: return Data[1090];
                case 1740: return Data[1091];
                case 1741: return Data[1092];
                case 1742: return Data[1093];
                case 1743: return Data[1094];
                case 1744: return Data[1095];
                case 1745: return Data[1096];
                case 1747: return Data[1097];
                case 1748: return Data[1098];
                case 1749: return Data[1099];
                case 1750: return Data[1100];
                case 1751: return Data[1101];
                case 1752: return Data[1102];
                case 1753: return Data[1103];
                case 1754: return Data[1104];
                case 1755: return Data[1105];
                case 1756: return Data[1106];
                case 1757: return Data[1107];
                case 1758: return Data[1108];
                case 1759: return Data[1109];
                case 1760: return Data[1110];
                case 1761: return Data[1111];
                case 1762: return Data[1112];
                case 1763: return Data[1113];
                case 1764: return Data[1114];
                case 1765: return Data[1115];
                case 1766: return Data[1116];
                case 1767: return Data[1117];
                case 1768: return Data[1118];
                case 1769: return Data[1119];
                case 1770: return Data[1120];
                case 1771: return Data[1121];
                case 1772: return Data[1122];
                case 1773: return Data[1123];
                case 1774: return Data[1124];
                case 1775: return Data[1125];
                case 1776: return Data[1126];
                case 1777: return Data[1127];
                case 1778: return Data[1128];
                case 1779: return Data[1129];
                case 1780: return Data[1130];
                case 1781: return Data[1131];
                case 1782: return Data[1132];
                case 1783: return Data[1133];
                case 1784: return Data[1134];
                case 1814: return Data[1135];
                case 1815: return Data[1136];
                case 1816: return Data[1137];
                case 1948: return Data[1138];
                case 1949: return Data[1139];
                case 1950: return Data[1140];
                case 1951: return Data[1141];
                case 1952: return Data[1142];
                case 1953: return Data[1143];
                case 1954: return Data[1144];
                case 1955: return Data[1145];
                case 1957: return Data[1146];
                case 1986: return Data[1147];
                case 1987: return Data[1148];
                case 1988: return Data[1149];
                case 1990: return Data[1150];
                case 1991: return Data[1151];
                case 1992: return Data[1152];
                case 1993: return Data[1153];
                case 1994: return Data[1154];
                case 1995: return Data[1155];
                case 1996: return Data[1156];
                case 1997: return Data[1157];
                case 1998: return Data[1158];
                case 1999: return Data[1159];
                case 2000: return Data[1160];
                case 2001: return Data[1161];
                case 2002: return Data[1162];
                case 2003: return Data[1163];
                case 2004: return Data[1164];
                case 2005: return Data[1165];
                case 2006: return Data[1166];
                case 2007: return Data[1167];
                case 2008: return Data[1168];
                case 2009: return Data[1169];
                case 2010: return Data[1170];
                case 2011: return Data[1171];
                case 2012: return Data[1172];
                case 2013: return Data[1173];
                case 2014: return Data[1174];
                case 2015: return Data[1175];
                case 2016: return Data[1176];
                case 2017: return Data[1177];
                case 2018: return Data[1178];
                case 2051: return Data[1179];
                case 2052: return Data[1180];
                case 2053: return Data[1181];
                case 2054: return Data[1182];
                case 2055: return Data[1183];
                case 2056: return Data[1184];
                case 2057: return Data[1185];
                case 2058: return Data[1186];
                case 2059: return Data[1187];
                case 2060: return Data[1188];
                case 2061: return Data[1189];
                case 2062: return Data[1190];
                case 2063: return Data[1191];
                case 2064: return Data[1192];
                case 2065: return Data[1193];
                case 2066: return Data[1194];
                case 2067: return Data[1195];
                case 2068: return Data[1196];
                case 2069: return Data[1197];
                case 2070: return Data[1198];
                case 2071: return Data[1199];
                case 2072: return Data[1200];
                case 2073: return Data[1201];
                case 2074: return Data[1202];
                case 2075: return Data[1203];
                case 2091: return Data[1204];
                case 2092: return Data[1205];
                case 2093: return Data[1206];
                case 2094: return Data[1207];
                case 2095: return Data[1208];
                case 2096: return Data[1209];
                case 2097: return Data[1210];
                case 2098: return Data[1211];
                case 2099: return Data[1212];
                case 2100: return Data[1213];
                case 2101: return Data[1214];
                case 2102: return Data[1215];
                case 2103: return Data[1216];
                case 2104: return Data[1217];
                case 2105: return Data[1218];
                case 2107: return Data[1219];
                case 2108: return Data[1220];
                case 2109: return Data[1221];
                case 2110: return Data[1222];
                case 2112: return Data[1223];
                case 2116: return Data[1224];
                case 2135: return Data[1225];
                case 2136: return Data[1226];
                case 2137: return Data[1227];
                case 2138: return Data[1228];
                case 2139: return Data[1229];
                case 2140: return Data[1230];
                case 2141: return Data[1231];
                case 2142: return Data[1232];
                case 2143: return Data[1233];
                case 2144: return Data[1234];
                case 2145: return Data[1235];
                case 2146: return Data[1236];
                case 2147: return Data[1237];
                case 2148: return Data[1238];
                case 2149: return Data[1239];
                case 2150: return Data[1240];
                case 2186: return Data[1241];
                case 2187: return Data[1242];
                case 2188: return Data[1243];
                case 2189: return Data[1244];
                case 2190: return Data[1245];
                case 2191: return Data[1246];
                case 2192: return Data[1247];
                case 2193: return Data[1248];
                case 2194: return Data[1249];
                case 2195: return Data[1250];
                case 2196: return Data[1251];
                case 2197: return Data[1252];
                case 2198: return Data[1253];
                case 2199: return Data[1254];
                case 2200: return Data[1255];
                case 2201: return Data[1256];
                case 2202: return Data[1257];
                case 2203: return Data[1258];
                case 2204: return Data[1259];
                case 2205: return Data[1260];
                case 2206: return Data[1261];
                case 2207: return Data[1262];
                case 2208: return Data[1263];
                case 2209: return Data[1264];
                case 2210: return Data[1265];
                case 2211: return Data[1266];
                case 2212: return Data[1267];
                case 2213: return Data[1268];
                case 2214: return Data[1269];
                case 2215: return Data[1270];
                case 2216: return Data[1271];
                case 2217: return Data[1272];
                case 2218: return Data[1273];
                case 2219: return Data[1274];
                case 2220: return Data[1275];
                case 2221: return Data[1276];
                case 2222: return Data[1277];
                case 2223: return Data[1278];
                case 2224: return Data[1279];
                case 2225: return Data[1280];
                case 2226: return Data[1281];
                case 2227: return Data[1282];
                case 2228: return Data[1283];
                case 2229: return Data[1284];
                case 2230: return Data[1285];
                case 2231: return Data[1286];
                case 2232: return Data[1287];
                case 2233: return Data[1288];
                case 2234: return Data[1289];
                case 2235: return Data[1290];
                case 2236: return Data[1291];
                case 2237: return Data[1292];
                case 2238: return Data[1293];
                case 2353: return Data[1294];
                case 2354: return Data[1295];
                case 2355: return Data[1296];
                case 2356: return Data[1297];
                case 2357: return Data[1298];
                case 2358: return Data[1299];
                case 2359: return Data[1300];
                case 2360: return Data[1301];
                case 2361: return Data[1302];
                case 2374: return Data[1303];
                case 2379: return Data[1304];
                case 2384: return Data[1305];
                case 2411: return Data[1306];
                case 2412: return Data[1307];
                case 2413: return Data[1308];
                case 2414: return Data[1309];
                case 2415: return Data[1310];
                case 2416: return Data[1311];
                case 2417: return Data[1312];
                case 2418: return Data[1313];
                case 2420: return Data[1314];
                case 2421: return Data[1315];
                case 2422: return Data[1316];
                case 2423: return Data[1317];
                case 3422: return Data[1318]; // Time Ward
                case 3423: return Data[1319]; // Soul Taker
                case 3424: return Data[1320]; // Over the Limit
                case 3425: return Data[1321]; // Judgment Strike
                case 3426: return Data[1322]; // Seven Weapon Stance
                case 3427: return Data[1323]; // "Together as One!"
                case 3428: return Data[1324]; // Shadow Theft
                case 3429: return Data[1325]; // Weapons of Three Forges
                case 3430: return Data[1326]; // Vow of Revolution
                case 3431: return Data[1327]; // Heroic Refrain
            }
        }

        static public List<string> TemplatesDatabase = new List<string>();

        static public bool LoadTemplatesFile()
        {
            if (System.IO.File.Exists("Templates.txt"))
            {
                System.IO.StreamReader input = new System.IO.StreamReader("Templates.txt");
                while (input.EndOfStream == false)
                {
                    string line = input.ReadLine();
                    if (line.Length > 0) TemplatesDatabase.Add(line);
                }
                input.Close();

                LoadTemplatesDatabaseAsDeck();
                return true;
            }
            else // If there is no file, then just populate with stuff.
            {
                TemplatesDatabase.Add("Shadow Blade (A)|OwFj0xf4oOAw+HCrh5yCdwMBA");
                TemplatesDatabase.Add("Dark Flame (E/A)|OgdToYm+R5WGGeJYw9BGXEA");
                TemplatesDatabase.Add("Ice Blighter (E/N)|OgRDc4ysCTrOyX3O0NKA");
                TemplatesDatabase.Add("Pyromancer (E/R)|OgJToYmaxQXanYFrX4SA5C");
                TemplatesDatabase.Add("Spell Slasher (Me/W)|OQFUAIBPOqGCIwFBFJtJcIDA");
                TemplatesDatabase.Add("DW Healer (Mo)|OwYT043A5IjaMnSsiucn6E");
                TemplatesDatabase.Add("Fi Boon Healer (Mo/N)|OwQTQwmC5gjtkkZMnb0jSC");
                TemplatesDatabase.Add("Protection Healer (Mo/W)|OwET8Y4WtYmCclvkvVsg1D");
                TemplatesDatabase.Add("Minion Master (N/Me)|OAVDMaxGCp9kAopFmUNF");
                TemplatesDatabase.Add("Warrior's Bane (N/Me)|OAVEE8gWdJIwh48yHtAFKA");
                TemplatesDatabase.Add("Fang of Melandru (R/A)|OgcTcXs1ZxhZ4EWhRYgz3EA");
                TemplatesDatabase.Add("IVEX Trapper (R)|OgUTcV8m14xUGxoe5K/4+G");
                TemplatesDatabase.Add("Brutal Bombardier (R/Rt)|OggkcpZMIGKzip4gaSNqzvJ25iB");
                TemplatesDatabase.Add("Earthbinder (Rt/Me)|OAWjAyk7gNnTy53scGBqcOzLG");
                TemplatesDatabase.Add("Soul Spinner (Rt/Mo)|OAOiEyk8M5kmxzMm4s61SuYA");
                TemplatesDatabase.Add("Abominable Snowman (W/E)|OQYUs0IO3qFj9rnVABwdUrrA");
                TemplatesDatabase.Add("Paladin (W/Mo)|OQMT0woS5wvA+vBurdUk6E");
                TemplatesDatabase.Add("Hot Stepper (A/N)|OwRj0xf4oKDc3nwKMC7yaiAA");
                TemplatesDatabase.Add("Blinding Prodigy (E/Mo)|OgNDgMjtGlzbcDcy6jgUAA");
                TemplatesDatabase.Add("Energy Denied (Me/Mo)|OQNDApwTGnQFeoIEJDtMmA");
                TemplatesDatabase.Add("Warrior's Anguish (Me/Mo)|OQNEAWsT+pYDDOMkBEBC5ACA");
                TemplatesDatabase.Add("Air of Smite (Mo)|OwAT443CtZmIQ2iPkFCExlIAA");
                TemplatesDatabase.Add("Blessed Light Monk (Mo/Me)|OwUUMuG+QIP1jW3MZiUSCjEhMI");
                TemplatesDatabase.Add("Boon Prot (Mo/Me)|OwUTMyXC5YmVUglkjs0CSB");
                TemplatesDatabase.Add("Tainted Ally (N/Mo)|OANDUshfGx5LqJNe5mBUAA");
                TemplatesDatabase.Add("Spirit Lord (Rt)|OACjAuhKpOwzzm17xV9CmOTBA");
                TemplatesDatabase.Add("Cripshot Ranger (R/Me)|OgUUEmLjzcGJ+xqe2++4LQAA");
                TemplatesDatabase.Add("Frenzy Theory (W/E)|OQYUgqYK5KGS1pQNAnTrdVAA");
                TemplatesDatabase.Add("Final Master (W/Me)|OQUUIkoR56G+FwBmtBQrKQAA");
                TemplatesDatabase.Add("Hamstorm (W/E)|OQYTo4IS1Boo9Ps/EmSxwIKAAA"); // WE (Sword)
                TemplatesDatabase.Add("Touch Ranger (R/N)|OgQScZCP1QOBqRTwKTNSChD"); // RN
                TemplatesDatabase.Add("Earthshaker Aftershock (W/E)|OQYTk4IOVRsclEXq/pyqVEAA"); // WE (Hammer)
                TemplatesDatabase.Add("Plague Martyr (N/Mo)|OANDY4xuSVCOV7O4VJgqEPE7EA"); // NMo
                TemplatesDatabase.Add("Bunny Thumper (R/W)|OgETMZrexZpAWCL56E3qh2IAAA"); // RW
                TemplatesDatabase.Add("Charr Hunter (R)|OgATcVsm3hx4YytvGfzve1IAAA"); // R
                TemplatesDatabase.Add("Distressing Touch (N/A)|OAdUQ2iasPO2QPhggOk9YzHQBAA"); // NA
                TemplatesDatabase.Add("Unseen Lyssa (D/A)|Ogek8hp6KyqD3Pb/cjgZ34LuTAA"); // DA
                TemplatesDatabase.Add("Gift of Protection (Mo)|OwAT00nC35uKQaJb6w4l+iEQAA"); // Mo
                TemplatesDatabase.Add("Antidote Healer (Mo/R)|OwIT043A5Zj4Rm+jNQi/4msaAA"); // MoR
                TemplatesDatabase.Add("Deadly Symbolism (W/A)|OQcTERJ7VKqYSkw6n44jBSRUAAA"); // WA (Axe)
                TemplatesDatabase.Add("Dwayna's Cultist (N/D)|OApjQwGoqOmsOxkwQgfDv3XBAA"); // ND
                TemplatesDatabase.Add("Critical Disruption (A/R)|OwJikxjMhr1btrVAtIq/saIAAA"); // AR
                TemplatesDatabase.Add("Terrifying Ox (A/W)|OwFjUpd8ISZFfaVQXhEFcFuFCAA"); // AW
                TemplatesDatabase.Add("Fragile Spider's Fangs (A/Me)|OwVjEod8IOTAyMCTHwAdCgQBAA"); // AMe
                TemplatesDatabase.Add("Signet Flash (E/A)|OgdTgYG6VibgcwcocQRktJcQAAA"); // EA
                TemplatesDatabase.Add("Burning Ice (E)|OghkooLMDGKz0QdsOYG/ZoBp4iB"); // E
                TemplatesDatabase.Add("Holy Ground (E/Mo)|OgNDkKjOT+MUfuEbDOioCID7EA"); // EMo
                TemplatesDatabase.Add("Healer's Boon (Mo)|OwAS0YIPT/NyvLjIR+Ix13J"); // Mo
                TemplatesDatabase.Add("Fevered Frustration (Me/N)|OQRDAawHO3gead4Ts74gjOBA"); // MeN
                TemplatesDatabase.Add("Guided Teasing (Me/Rt)|OQhjAoDK4OTTq560nmBgCZukLA"); // MeRt
                TemplatesDatabase.Add("The Hex Punisher (Me/Mo)|OQNDAqsuOE1hgLx6b6uA5gdC"); // MeMo
                TemplatesDatabase.Add("Extreme Binding (Rt)|OACjAqhK5SfTnNPOWPbOciVPBTA"); // Rt
                TemplatesDatabase.Add("Weapon Healer (Rt/Me)|OAWjAihMpSkhZMEPDTxTRTMb6OA"); // RtMe
                TemplatesDatabase.Add("Explosive Minions (Rt/N)|OASjUwhDJPVhjKaIp6MRuNzLGA"); // RtN
                TemplatesDatabase.Add("Reap Poison (D/R)|OgKjghpMrScfOXWgibFYzGrGCAA"); // DR
                TemplatesDatabase.Add("Contagious Reaper (D/N)|OgSjUopMLSWgjhCYibfXNfMVCAA"); // DN
                TemplatesDatabase.Add("Shock and Awe (P/E)|OQakgolqZguUoYR2QmFWzw5QJm8G"); // PE
                TemplatesDatabase.Add("Triple Finale (P/W)|OQGlUhlqpcio68gPxp43t9mrYhl3"); // PW
                TemplatesDatabase.Add("Crippling Song (P/Rt)|OQikIilqJiqzDOIvjtwS4Lc5EHD"); // PRt
                //TemplatesDatabase.Add("|"); // TEMPLATE EXTRA

                LoadTemplatesDatabaseAsDeck();
                return false;
            }
        }

        static public void LoadTemplatesDatabaseAsDeck()
        {
            TemplateDeck.Clear();
            foreach (string str in TemplatesDatabase)
            {
                TemplateDeck.Add(str);
            }

            TemplateDeckName = "Challenges Templates";
        }

        static public void LoadTemplateDeck(string filename)
        {
            List<string> templateDeck = new List<string>();
            System.IO.StreamReader input = new System.IO.StreamReader(filename);
            while (input.EndOfStream == false)
            {
                string line = input.ReadLine();
                if (line.Length > 0) templateDeck.Add(line);
            }
            input.Close();

            TemplateDeck.Clear();
            TemplateDeck = templateDeck;
            TemplateDeckName = filename.Substring(filename.LastIndexOf('\\') + 1);
        }

        static public bool CheckRatingsFile()
        {
            if (System.IO.File.Exists("Skills Ratings.txt"))
            {
                System.IO.StreamReader input = new System.IO.StreamReader("Skills Ratings.txt");
                input.ReadLine(); // Toss out the instructions line.
                List<Pair<int, int>> ratings = new List<Pair<int, int>>();

                while (input.EndOfStream == false)
                {
                    string linecopy = input.ReadLine();
                    string[] line = linecopy.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                    if (line.Length < 2)
                    {
                        System.Windows.Forms.MessageBox.Show("Warning: Malformed line \"" + linecopy + "\".", "Malformed Line", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                    }

                    int id = -1;
                    int rating = -1;
                    try
                    {
                        id = Convert.ToInt32(line[0]);
                        rating = Convert.ToInt32(line[1].Substring(7));
                    }
                    catch (Exception e)
                    {
                        System.Windows.Forms.MessageBox.Show("Warning: Malformed line \"" + linecopy + "\"" + Environment.NewLine + Environment.NewLine + "Error message: " + e.Message, "Malformed Line", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                        continue;
                    }

                    ratings.Add(new Pair<int, int>(id, rating));
                }

                input.Close();

                foreach (Pair<int, int> rating in ratings)
                {
                    Data[rating.first].UpdateRating(rating.second);
                }

                return true;
            }

            return false;
        }

        static public bool CheckRaritiesFile()
        {
            if(System.IO.File.Exists(Form1.SkillRaritiesFilename))
            {
                System.IO.StreamReader input = new System.IO.StreamReader(Form1.SkillRaritiesFilename);
                input.ReadLine(); // Toss out the instructions line.
                List<Pair<int, int>> rarities = new List<Pair<int, int>>();

                while (input.EndOfStream == false)
                {
                    string linecopy = input.ReadLine();
                    string[] line = linecopy.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                    if (line.Length < 2)
                    {
                        System.Windows.Forms.MessageBox.Show("Warning: Malformed line \"" + linecopy + "\".", "Malformed Line", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                    }

                    int id = -1;
                    int rarity = -1;
                    try
                    {
                        id = Convert.ToInt32(line[0]);
                        rarity = Convert.ToInt32(line[1]);
                    }
                    catch (Exception e)
                    {
                        System.Windows.Forms.MessageBox.Show("Warning: Malformed line \"" + linecopy + "\"" + Environment.NewLine + Environment.NewLine + "Error message: " + e.Message, "Malformed Line", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                        continue;
                    }

                    rarities.Add(new Pair<int, int>(id, rarity));
                }

                input.Close();

                foreach (Pair<int, int> rating in rarities)
                {
                    Data[rating.first].Rarity = rating.second;
                }

                return true;
            }

            return false;
        }
    }
}
