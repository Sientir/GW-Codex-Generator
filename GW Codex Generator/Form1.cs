using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace GW_Codex_Generator
{
    public partial class Form1 : Form
    {
        /*
         * Some notes!
         *
         *  Codex Settings: DONE!
         *  - Select Pool (All Campaigns, or some mixture of Core/Prophecies/Factions/Nightfall/EotN). Should include a checkbox for whether or
         *    not to allow PvE-only skills?
         *  - Option to choose which selection method should be used.
         *  - Toggle for whether or not to use ratings.
         *  - Settings appropriate to the selected selection method.
         *    
         *  Challenges:
         *  - Each challenge should have configuration settings (as well as Active for getting added to the HTML generator).
         *  - Each challenge setting should also provide the output of the challenge. E.g. which skills are mandatory.
         *  - There should be a button (or something) to randomize all challenge settings.
         *  
         *  Settings tab:
         *  - Colors (foreground/background); make a recursive setting function, see if that works. The codex settings controls will need to grab these values.
         *  - HTML output settings, particularly for skills. Use <h1> tags for section headers.
         *  
         *  Finished State for v1:
         *  - Above notes are completed. Anything else?
         *  
         *  Future Goals:
         *  - Ratings filters?
         *  - Generate pool by profession instead of campaign?
         *  - Filter by campaign progress (e.g. what skills are available in pre-searing, or by the time you get to Kryta, etc.)?
         *  - Add a display mode to the skill display that supports seperating skills out by attribute line? This is to improve readability of codex pools.
         *  - Alternatively to above, change the codex display specifically to use seperate skill displays per attribute line?
         *  - Smart balance codex method; see function.
         *  - Add support for an external way to set ratings values. Probably have the program able to generate a file with indices, names, and ratings, then use ratings from that file if it exists.
         *  - Save settings, things like default codex type settings, challenge settings, use of rating, codex method, etc...
         */
        Control _CodexMethodSettingsControl = null;
        List<Challenge_UI.IChallengeToHTML> _ChallengeControls = new List<Challenge_UI.IChallengeToHTML>();
        public Form1()
        {
            InitializeComponent();

            SkillDatabase.LoadSkillInformation();
            SkillDatabase.LoadTemplatesFile();

            // Hook up skill info displays:
            __CodexSkillDisplay.DescriptionBox = __CodexSkillInfo;
            __Codex_UseRatings.Checked = GrabBag.UseRatings; // We may save this setting, so better load it!
            __Codex_UseRatings.CheckedChanged += __Codex_UseRatings_CheckedChanged;
            __Codex_SelectMethod.SelectedIndexChanged += __Codex_SelectMethod_SelectedIndexChanged;
            __Codex_SelectMethod.SelectedIndex = 3;
            __TemplateCodeParserTest.SkillInformationDisplay = __CodexSkillInfo;

            // Create challenge controls:
            Challenge_UI.BinaryChallenges binaryChallenges = new Challenge_UI.BinaryChallenges();
            _ChallengeControls.Add(binaryChallenges);
            binaryChallenges.Dock = DockStyle.Top;

            Challenge_UI.RequiredElites requiredElites = new Challenge_UI.RequiredElites();
            _ChallengeControls.Add(requiredElites);
            requiredElites.Dock = DockStyle.Top;
            requiredElites.SetSkillDisplayInfo(__CodexSkillInfo);

            Challenge_UI.RequiredSkill requiredSkill = new Challenge_UI.RequiredSkill();
            _ChallengeControls.Add(requiredSkill);
            requiredSkill.Dock = DockStyle.Top;
            requiredSkill.SkillInfoDisplay = __CodexSkillInfo;

            Challenge_UI.MandatorySkillsUI mandatedSkills = new Challenge_UI.MandatorySkillsUI();
            _ChallengeControls.Add(mandatedSkills);
            mandatedSkills.Dock = DockStyle.Top;
            mandatedSkills.SetSkillInfoDisplay(__CodexSkillInfo);

            Challenge_UI.TemplateChallengeUI requiredTemplates = new Challenge_UI.TemplateChallengeUI();
            _ChallengeControls.Add(requiredTemplates);
            requiredTemplates.Dock = DockStyle.Top;
            requiredTemplates.SkillInfoDisplay = __CodexSkillInfo;


            // Properly establish the challenge controls:
            for(int i = _ChallengeControls.Count-1; i >= 0; --i) // running this in reverse, as previous experience has taught me that adding a new top-docked control will put it above previous ones, and I want to lead with the order they are in the list.
            {
                _ChallengeControls[i].SetHTMLRefreshFunction(GenerateHTMLPage);
                __Tabs.TabPages[2].Controls.Add((Control)_ChallengeControls[i]);
            }

            #region Skill ID look up greencode
            //string[] leadAttacks = {
            //    "blades of steel",
            //    "death blossom",
            //    "horns of the ox",
            //    "nine tail strike",
            //    "trampling ox",
            //    "vampiric assault"
            //};
            //foreach(string str in leadAttacks)
            //{
            //    __HTMLSummary.Text += "DualAttacks.Add(Data[" + SkillDatabase.GetSkillIndexByName(str).ToString() + "]);" + Environment.NewLine;
            //}

            //string[] badrequiredskills =
            //{
            //    "Barbed Arrows",
            //    "Choking Gas",
            //    "Brambles",
            //    "Conflagration",
            //    "Equinox",
            //    "Famine",
            //    "Frozen Soil",
            //    "Greater Conflagration",
            //    "Ignite Arrows",
            //    "Incendiary Arrows",
            //    "Kindle Arrows",
            //    "Melandru's Arrows",
            //    "Muddy Terrain",
            //    "Nature's Renewal",
            //    "Pestilence",
            //    "Poison Arrow",
            //    "Quickening Zephyr",
            //    "Quicksand",
            //    "Roaring Winds",
            //    "Tranquility",
            //    "Winnowing",
            //    "Winter",
            //    "Called Shot",
            //    "Dual Shot",
            //    "Forked Arrow",
            //    "Magebane Shot",
            //    "Quick Shot",
            //    "Deadly Riposte",
            //    "Desperation Blow",
            //    "Drunken Blow",
            //    "Riposte",
            //    "Shield Stance",
            //    "Thrill of Victory",
            //    "Distracting Blow",
            //    "Distracting Strike",
            //    "Skull Crack",
            //    "Symbolic Strike",
            //    "Wild Blow",
            //    "Impale",
            //    "Deadly Paradox",
            //    "Entangling Asp",
            //    "Mantis Touch",
            //    "Mark of Insecurity",
            //    "Vampiric Assault",
            //    "Way of the Empty Palm",
            //    "Blinding Powder",
            //    "Hidden Caltrops",
            //    "Way of the Lotus",
            //    "Assault Enchantments",
            //    "Mark of Instability",
            //    "Recall",
            //    "Wastrel's Collapse",
            //    "Healer's Covenant",
            //    "Healing Burst",
            //    "Healing Touch",
            //    "Rebirth",
            //    "Succor",
            //    "Awaken the Blood",
            //    "Cultist's Fervor",
            //    "Dark Bond",
            //    "Order of Apostasy",
            //    "Blood of the Master",
            //    "Dark Aura",
            //    "Feast for the Dead",
            //    "Infuse Condition",
            //    "Jagged Bones",
            //    "Order of Undeath",
            //    "Putrid Flesh",
            //    "Taste of Death",
            //    "Verata's Aura",
            //    "Verata's Sacrifice",
            //    "Illusionary Weaponry",
            //    "Ether Lord",
            //    "Mantra of Inscriptions",
            //    "Mantra of Persistence",
            //    "Mantra of Signets",
            //    "Signet of Humility",
            //    "Arcane Echo",
            //    "Air Attunement",
            //    "Arc Lightning",
            //    "Conjure Lightning",
            //    "Shell Shock",
            //    "Shock Arrow",
            //    "Earth Attunement",
            //    "Glowstone",
            //    "Iron Mist",
            //    "Obsidian Flesh",
            //    "Conjure Flame",
            //    "Elemental Flame",
            //    "Fire Attunement",
            //    "Glowing Gaze",
            //    "Conjure Frost",
            //    "Glowing Ice",
            //    "Water Attunement",
            //    "Glyph of Elemental Power",
            //    "Glyph of Concentration",
            //    "Glyph of Essence",
            //    "Glyph of Sacrifice",
            //    "Agony",
            //    "Bloodsong",
            //    "Destruction",
            //    "Gaze of Fury",
            //    "Signet of Spirits",
            //    "Spirit Siphon",
            //    "Weapon of Aggression",
            //    "Anguish",
            //    "Armor of Unfeeling",
            //    "Disenchantment",
            //    "Displacement",
            //    "Dissonance",
            //    "Earthbind",
            //    "Pain",
            //    "Mighty Was Vorizun",
            //    "Restoration",
            //    "Shadowsong",
            //    "Shelter",
            //    "Signet of Ghostly Might",
            //    "Soothing",
            //    "Union",
            //    "Wanderlust",
            //    "Life",
            //    "Preservation",
            //    "Recovery",
            //    "Recuperation",
            //    "Rejuvenation",
            //    "Spiritleech Aura",
            //    "Tranquil Was Tanasen",
            //    "Vocal Was Sogolon",
            //    "Draw Spirit",
            //    "Signet of Aggression",
            //    "Ebon Dust Aura",
            //    "Sand Shards",
            //    "Vow of Strength",
            //    "Grenth's Aura",
            //    "Grenth's Grasp",
            //    "Signet of Mystic Speed",
            //    "Winds of Disenchantment"
            //};

            //foreach(string str in badrequiredskills)
            //{
            //    __HTMLSummary.Text += "case " + SkillDatabase.GetSkillIndexByName(str).ToString() + ": // " + str + Environment.NewLine + "skipSkill = true;" +
            //        Environment.NewLine + "break;" + Environment.NewLine;
            //}

            //GenerateCampaignCode(); // Only do this to generate the code, which I've done!
            //GenerateSkillIDIndexCode(); // Only need to do this once to generate the code...
            #endregion
        }

        private void __Codex_SelectMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Clear the current control for the current method...
            if (_CodexMethodSettingsControl != null)
            {
                __CodexSettingsLayoutTable.Controls.Remove(_CodexMethodSettingsControl);
            }
            _CodexMethodSettingsControl = null;

            switch (__Codex_SelectMethod.SelectedIndex)
            {
                case 0: // Pure Random
                    _CodexMethodSettingsControl = new Codex_Method_Settings.PureRandomMethod();
                    break;
                case 1: // Profession balanced
                    _CodexMethodSettingsControl = new Codex_Method_Settings.ProfessionBalanced();
                    break;
                case 2: // Attribute Balanced
                    _CodexMethodSettingsControl = new Codex_Method_Settings.AttributeBalanced();
                    break;
                case 3: // Regular skills per attribute, elites per profession
                    _CodexMethodSettingsControl = new Codex_Method_Settings.RegularFromAttributesElitesFromProfession();
                    break;
                case 4: // By rating
                    _CodexMethodSettingsControl = new Codex_Method_Settings.ByRating();
                    break;
                default: return;
            }

            if (_CodexMethodSettingsControl != null)
            {
                __CodexSettingsLayoutTable.Controls.Add(_CodexMethodSettingsControl);
            }
        }

        private void __Codex_UseRatings_CheckedChanged(object sender, EventArgs e)
        {
            GrabBag.UseRatings = __Codex_UseRatings.Checked;
        }

        private void __GenerateCodexButton_Click(object sender, EventArgs e)
        {
            // Step One: Generate pool...
            int campaignsUsedInPool = 0;
            if (__Codex_Pool_Core.Checked) campaignsUsedInPool |= SkillDatabase.Core;
            if (__Codex_Pool_Prophecies.Checked) campaignsUsedInPool |= SkillDatabase.Prophecies;
            if (__Codex_Pool_Factions.Checked) campaignsUsedInPool |= SkillDatabase.Factions;
            if (__Codex_Pool_Nightfall.Checked) campaignsUsedInPool |= SkillDatabase.Nightfall;
            if (__Codex_Pool_EotN.Checked) campaignsUsedInPool |= SkillDatabase.EyeOfTheNorth;

            if (campaignsUsedInPool == 0)
            {
                MessageBox.Show("Error with codex pool: No campaigns selected. Please select at least one campaign for the skill pool!");
                return;
            }

            // This is the line I will be using, once I can actually have the lists populated...
            List<Skill> pool = SkillDatabase.GetSkillsByCampaign(campaignsUsedInPool, __Codex_Pool_PvEOnly.Checked);
            // Temporary line:
            //List<Skill> pool = SkillDatabase.Data;

            // Use the settings control for the selection method to generate the codex:
            __CodexSkillDisplay.Skills = ((Codex_Method_Settings.ICodexPoolSettingsGenerator)_CodexMethodSettingsControl).GenerateCodex(pool);

            // Update the HTML page and redraw the codex display:
            GenerateHTMLPage();
            __CodexSkillDisplay.Redraw();
        }

        private void GenerateHTMLPage()
        {
            string skillInformation = __CodexSkillDisplay.Skills.CodexToHTML();

            // Will need to generate information about challenges here. I'll likely have that information before the codex information,
            //  simply because the codex information will be MUCH longer.

            string challengesInfo = "";
            foreach(Challenge_UI.IChallengeToHTML ccont in _ChallengeControls)
            {
                challengesInfo += ccont.GetHTML();
            }

            __HTMLSummary.Text = challengesInfo + Environment.NewLine + "<h1>Codex</h1>" + Environment.NewLine + skillInformation;
        }

        private void GenerateCampaignCode()
        {
            // Start by adding core skills:
            __HTMLSummary.Text = "// Core skills:" + Environment.NewLine + "int index = GetCampaignIndex(Core);" + Environment.NewLine + Environment.NewLine;
            foreach (string name in SkillDatabase.CoreSkillNames)
            {
                int index = SkillDatabase.Data.FindIndex((x) => string.Compare(x.Name, name) == 0);
                if (index < 0) MessageBox.Show("Failed to find skill: " + name);
                __HTMLSummary.Text += "SkillsByCampaign[WithPvEOnly][index].Add(Data[" + index.ToString() + "]);" + Environment.NewLine;
                // I can just blindly add this, since Core doesn't have any PvE only skills.
                __HTMLSummary.Text += "SkillsByCampaign[WithoutPvEOnly][index].Add(Data[" + index.ToString() + "]);" + Environment.NewLine;
            }

            // Now Prophecies
            __HTMLSummary.Text += "// Prophecies skills:" + Environment.NewLine + "index = GetCampaignIndex(Prophecies);" + Environment.NewLine + Environment.NewLine;
            foreach (string name in SkillDatabase.PropheciesSkillNames)
            {
                int index = SkillDatabase.Data.FindIndex((x) => string.Compare(x.Name, name) == 0);
                if (index < 0) MessageBox.Show("Failed to find skill: " + name);
                __HTMLSummary.Text += "SkillsByCampaign[WithPvEOnly][index].Add(Data[" + index.ToString() + "]);" + Environment.NewLine;
                // I can just blindly add this, since Prophecies doesn't have any PvE only skills.
                __HTMLSummary.Text += "SkillsByCampaign[WithoutPvEOnly][index].Add(Data[" + index.ToString() + "]);" + Environment.NewLine;
            }

            // Now Factions
            __HTMLSummary.Text += "// Factions skills:" + Environment.NewLine + "index = GetCampaignIndex(Factions);" + Environment.NewLine + Environment.NewLine;
            foreach (string name in SkillDatabase.FactionsSkillNames)
            {
                int index = SkillDatabase.Data.FindIndex((x) => string.Compare(x.Name, name) == 0);
                if (index < 0) MessageBox.Show("Failed to find skill: " + name);
                __HTMLSummary.Text += "SkillsByCampaign[WithPvEOnly][index].Add(Data[" + index.ToString() + "]);" + Environment.NewLine;
                if (SkillDatabase.Data[index].Attribute != Skill.Attributes.PvE_Only)
                {
                    __HTMLSummary.Text += "SkillsByCampaign[WithoutPvEOnly][index].Add(Data[" + index.ToString() + "]);" + Environment.NewLine;
                }
            }

            // Now Nightfall
            __HTMLSummary.Text += "// Nightfall skills:" + Environment.NewLine + "index = GetCampaignIndex(Nightfall);" + Environment.NewLine + Environment.NewLine;
            foreach (string name in SkillDatabase.NightfallSkillNames)
            {
                int index = SkillDatabase.Data.FindIndex((x) => string.Compare(x.Name, name) == 0);
                if (index < 0) MessageBox.Show("Failed to find skill: " + name);
                __HTMLSummary.Text += "SkillsByCampaign[WithPvEOnly][index].Add(Data[" + index.ToString() + "]);" + Environment.NewLine;
                if (SkillDatabase.Data[index].Attribute != Skill.Attributes.PvE_Only)
                {
                    __HTMLSummary.Text += "SkillsByCampaign[WithoutPvEOnly][index].Add(Data[" + index.ToString() + "]);" + Environment.NewLine;
                }
            }

            // Now Eye of the North
            __HTMLSummary.Text += "// Eye of the North skills:" + Environment.NewLine + "index = GetCampaignIndex(EyeOfTheNorth);" + Environment.NewLine + Environment.NewLine;
            foreach (string name in SkillDatabase.EyeOfTheNorthSkillNames)
            {
                int index = SkillDatabase.Data.FindIndex((x) => string.Compare(x.Name, name) == 0);
                if (index < 0) MessageBox.Show("Failed to find skill: " + name);
                __HTMLSummary.Text += "SkillsByCampaign[WithPvEOnly][index].Add(Data[" + index.ToString() + "]);" + Environment.NewLine;
                if (SkillDatabase.Data[index].Attribute != Skill.Attributes.PvE_Only)
                {
                    __HTMLSummary.Text += "SkillsByCampaign[WithoutPvEOnly][index].Add(Data[" + index.ToString() + "]);" + Environment.NewLine;
                }
            }
        }

        private void GenerateSkillIDIndexCode()
        {
            __HTMLSummary.Text += "case 1: return Data[" + SkillDatabase.GetSkillIndexByName("Healing Signet").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2: return Data[" + SkillDatabase.GetSkillIndexByName("Resurrection Signet").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 3: return Data[" + SkillDatabase.GetSkillIndexByName("Signet of Capture").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 5: return Data[" + SkillDatabase.GetSkillIndexByName("Power Block").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 6: return Data[" + SkillDatabase.GetSkillIndexByName("Mantra of Earth").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 7: return Data[" + SkillDatabase.GetSkillIndexByName("Mantra of Flame").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 8: return Data[" + SkillDatabase.GetSkillIndexByName("Mantra of Frost").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 9: return Data[" + SkillDatabase.GetSkillIndexByName("Mantra of Lightning").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 10: return Data[" + SkillDatabase.GetSkillIndexByName("Hex Breaker").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 11: return Data[" + SkillDatabase.GetSkillIndexByName("Distortion").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 13: return Data[" + SkillDatabase.GetSkillIndexByName("Mantra of Recovery").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 14: return Data[" + SkillDatabase.GetSkillIndexByName("Mantra of Persistence").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 15: return Data[" + SkillDatabase.GetSkillIndexByName("Mantra of Inscriptions").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 16: return Data[" + SkillDatabase.GetSkillIndexByName("Mantra of Concentration").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 17: return Data[" + SkillDatabase.GetSkillIndexByName("Mantra of Resolve").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 18: return Data[" + SkillDatabase.GetSkillIndexByName("Mantra of Signets").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 19: return Data[" + SkillDatabase.GetSkillIndexByName("Fragility").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 21: return Data[" + SkillDatabase.GetSkillIndexByName("Inspired Enchantment").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 22: return Data[" + SkillDatabase.GetSkillIndexByName("Inspired Hex").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 23: return Data[" + SkillDatabase.GetSkillIndexByName("Power Spike").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 24: return Data[" + SkillDatabase.GetSkillIndexByName("Power Leak").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 25: return Data[" + SkillDatabase.GetSkillIndexByName("Power Drain").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 26: return Data[" + SkillDatabase.GetSkillIndexByName("Empathy").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 27: return Data[" + SkillDatabase.GetSkillIndexByName("Shatter Delusions").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 28: return Data[" + SkillDatabase.GetSkillIndexByName("Backfire").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 29: return Data[" + SkillDatabase.GetSkillIndexByName("Blackout").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 30: return Data[" + SkillDatabase.GetSkillIndexByName("Diversion").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 31: return Data[" + SkillDatabase.GetSkillIndexByName("Conjure Phantasm").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 32: return Data[" + SkillDatabase.GetSkillIndexByName("Illusion of Weakness").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 33: return Data[" + SkillDatabase.GetSkillIndexByName("Illusionary Weaponry").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 34: return Data[" + SkillDatabase.GetSkillIndexByName("Sympathetic Visage").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 35: return Data[" + SkillDatabase.GetSkillIndexByName("Ignorance").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 36: return Data[" + SkillDatabase.GetSkillIndexByName("Arcane Conundrum").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 37: return Data[" + SkillDatabase.GetSkillIndexByName("Illusion of Haste").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 38: return Data[" + SkillDatabase.GetSkillIndexByName("Channeling").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 39: return Data[" + SkillDatabase.GetSkillIndexByName("Energy Surge").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 40: return Data[" + SkillDatabase.GetSkillIndexByName("Ether Feast").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 41: return Data[" + SkillDatabase.GetSkillIndexByName("Ether Lord").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 42: return Data[" + SkillDatabase.GetSkillIndexByName("Energy Burn").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 43: return Data[" + SkillDatabase.GetSkillIndexByName("Clumsiness").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 44: return Data[" + SkillDatabase.GetSkillIndexByName("Phantom Pain").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 45: return Data[" + SkillDatabase.GetSkillIndexByName("Ethereal Burden").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 46: return Data[" + SkillDatabase.GetSkillIndexByName("Guilt").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 47: return Data[" + SkillDatabase.GetSkillIndexByName("Ineptitude").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 48: return Data[" + SkillDatabase.GetSkillIndexByName("Spirit of Failure").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 49: return Data[" + SkillDatabase.GetSkillIndexByName("Mind Wrack").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 50: return Data[" + SkillDatabase.GetSkillIndexByName("Wastrel's Worry").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 51: return Data[" + SkillDatabase.GetSkillIndexByName("Shame").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 52: return Data[" + SkillDatabase.GetSkillIndexByName("Panic").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 53: return Data[" + SkillDatabase.GetSkillIndexByName("Migraine").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 54: return Data[" + SkillDatabase.GetSkillIndexByName("Crippling Anguish").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 55: return Data[" + SkillDatabase.GetSkillIndexByName("Fevered Dreams").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 56: return Data[" + SkillDatabase.GetSkillIndexByName("Soothing Images").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 57: return Data[" + SkillDatabase.GetSkillIndexByName("Cry of Frustration").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 58: return Data[" + SkillDatabase.GetSkillIndexByName("Signet of Midnight").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 59: return Data[" + SkillDatabase.GetSkillIndexByName("Signet of Weariness").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 61: return Data[" + SkillDatabase.GetSkillIndexByName("Leech Signet").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 62: return Data[" + SkillDatabase.GetSkillIndexByName("Signet of Humility").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 63: return Data[" + SkillDatabase.GetSkillIndexByName("Keystone Signet").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 65: return Data[" + SkillDatabase.GetSkillIndexByName("Arcane Mimicry").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 66: return Data[" + SkillDatabase.GetSkillIndexByName("Spirit Shackles").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 67: return Data[" + SkillDatabase.GetSkillIndexByName("Shatter Hex").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 68: return Data[" + SkillDatabase.GetSkillIndexByName("Drain Enchantment").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 69: return Data[" + SkillDatabase.GetSkillIndexByName("Shatter Enchantment").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 72: return Data[" + SkillDatabase.GetSkillIndexByName("Elemental Resistance").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 73: return Data[" + SkillDatabase.GetSkillIndexByName("Physical Resistance").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 74: return Data[" + SkillDatabase.GetSkillIndexByName("Echo").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 75: return Data[" + SkillDatabase.GetSkillIndexByName("Arcane Echo").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 76: return Data[" + SkillDatabase.GetSkillIndexByName("Imagined Burden").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 77: return Data[" + SkillDatabase.GetSkillIndexByName("Chaos Storm").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 78: return Data[" + SkillDatabase.GetSkillIndexByName("Epidemic").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 79: return Data[" + SkillDatabase.GetSkillIndexByName("Energy Drain").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 80: return Data[" + SkillDatabase.GetSkillIndexByName("Energy Tap").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 81: return Data[" + SkillDatabase.GetSkillIndexByName("Arcane Thievery").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 82: return Data[" + SkillDatabase.GetSkillIndexByName("Mantra of Recall").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 83: return Data[" + SkillDatabase.GetSkillIndexByName("Animate Bone Horror").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 84: return Data[" + SkillDatabase.GetSkillIndexByName("Animate Bone Fiend").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 85: return Data[" + SkillDatabase.GetSkillIndexByName("Animate Bone Minions").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 86: return Data[" + SkillDatabase.GetSkillIndexByName("Grenth's Balance").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 87: return Data[" + SkillDatabase.GetSkillIndexByName("Verata's Gaze").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 88: return Data[" + SkillDatabase.GetSkillIndexByName("Verata's Aura").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 89: return Data[" + SkillDatabase.GetSkillIndexByName("Deathly Chill").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 90: return Data[" + SkillDatabase.GetSkillIndexByName("Verata's Sacrifice").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 91: return Data[" + SkillDatabase.GetSkillIndexByName("Well of Power").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 92: return Data[" + SkillDatabase.GetSkillIndexByName("Well of Blood").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 93: return Data[" + SkillDatabase.GetSkillIndexByName("Well of Suffering").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 94: return Data[" + SkillDatabase.GetSkillIndexByName("Well of the Profane").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 95: return Data[" + SkillDatabase.GetSkillIndexByName("Putrid Explosion").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 96: return Data[" + SkillDatabase.GetSkillIndexByName("Soul Feast").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 97: return Data[" + SkillDatabase.GetSkillIndexByName("Necrotic Traversal").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 98: return Data[" + SkillDatabase.GetSkillIndexByName("Consume Corpse").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 99: return Data[" + SkillDatabase.GetSkillIndexByName("Parasitic Bond").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 100: return Data[" + SkillDatabase.GetSkillIndexByName("Soul Barbs").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 101: return Data[" + SkillDatabase.GetSkillIndexByName("Barbs").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 102: return Data[" + SkillDatabase.GetSkillIndexByName("Shadow Strike").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 103: return Data[" + SkillDatabase.GetSkillIndexByName("Price of Failure").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 104: return Data[" + SkillDatabase.GetSkillIndexByName("Death Nova").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 105: return Data[" + SkillDatabase.GetSkillIndexByName("Deathly Swarm").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 106: return Data[" + SkillDatabase.GetSkillIndexByName("Rotting Flesh").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 107: return Data[" + SkillDatabase.GetSkillIndexByName("Virulence").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 108: return Data[" + SkillDatabase.GetSkillIndexByName("Suffering").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 109: return Data[" + SkillDatabase.GetSkillIndexByName("Life Siphon").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 110: return Data[" + SkillDatabase.GetSkillIndexByName("Unholy Feast").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 111: return Data[" + SkillDatabase.GetSkillIndexByName("Awaken the Blood").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 112: return Data[" + SkillDatabase.GetSkillIndexByName("Desecrate Enchantments").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 113: return Data[" + SkillDatabase.GetSkillIndexByName("Tainted Flesh").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 114: return Data[" + SkillDatabase.GetSkillIndexByName("Aura of the Lich").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 115: return Data[" + SkillDatabase.GetSkillIndexByName("Blood Renewal").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 116: return Data[" + SkillDatabase.GetSkillIndexByName("Dark Aura").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 117: return Data[" + SkillDatabase.GetSkillIndexByName("Enfeeble").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 118: return Data[" + SkillDatabase.GetSkillIndexByName("Enfeebling Blood").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 119: return Data[" + SkillDatabase.GetSkillIndexByName("Blood is Power").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 120: return Data[" + SkillDatabase.GetSkillIndexByName("Blood of the Master").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 121: return Data[" + SkillDatabase.GetSkillIndexByName("Spiteful Spirit").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 122: return Data[" + SkillDatabase.GetSkillIndexByName("Malign Intervention").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 123: return Data[" + SkillDatabase.GetSkillIndexByName("Insidious Parasite").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 124: return Data[" + SkillDatabase.GetSkillIndexByName("Spinal Shivers").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 125: return Data[" + SkillDatabase.GetSkillIndexByName("Wither").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 126: return Data[" + SkillDatabase.GetSkillIndexByName("Life Transfer").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 127: return Data[" + SkillDatabase.GetSkillIndexByName("Mark of Subversion").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 128: return Data[" + SkillDatabase.GetSkillIndexByName("Soul Leech").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 129: return Data[" + SkillDatabase.GetSkillIndexByName("Defile Flesh").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 130: return Data[" + SkillDatabase.GetSkillIndexByName("Demonic Flesh").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 131: return Data[" + SkillDatabase.GetSkillIndexByName("Barbed Signet").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 132: return Data[" + SkillDatabase.GetSkillIndexByName("Plague Signet").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 133: return Data[" + SkillDatabase.GetSkillIndexByName("Dark Pact").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 134: return Data[" + SkillDatabase.GetSkillIndexByName("Order of Pain").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 135: return Data[" + SkillDatabase.GetSkillIndexByName("Faintheartedness").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 136: return Data[" + SkillDatabase.GetSkillIndexByName("Shadow of Fear").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 137: return Data[" + SkillDatabase.GetSkillIndexByName("Rigor Mortis").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 138: return Data[" + SkillDatabase.GetSkillIndexByName("Dark Bond").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 139: return Data[" + SkillDatabase.GetSkillIndexByName("Infuse Condition").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 140: return Data[" + SkillDatabase.GetSkillIndexByName("Malaise").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 141: return Data[" + SkillDatabase.GetSkillIndexByName("Rend Enchantments").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 142: return Data[" + SkillDatabase.GetSkillIndexByName("Lingering Curse").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 143: return Data[" + SkillDatabase.GetSkillIndexByName("Strip Enchantment").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 144: return Data[" + SkillDatabase.GetSkillIndexByName("Chilblains").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 145: return Data[" + SkillDatabase.GetSkillIndexByName("Signet of Agony").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 146: return Data[" + SkillDatabase.GetSkillIndexByName("Offering of Blood").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 147: return Data[" + SkillDatabase.GetSkillIndexByName("Dark Fury").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 148: return Data[" + SkillDatabase.GetSkillIndexByName("Order of the Vampire").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 149: return Data[" + SkillDatabase.GetSkillIndexByName("Plague Sending").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 150: return Data[" + SkillDatabase.GetSkillIndexByName("Mark of Pain").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 151: return Data[" + SkillDatabase.GetSkillIndexByName("Feast of Corruption").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 152: return Data[" + SkillDatabase.GetSkillIndexByName("Taste of Death").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 153: return Data[" + SkillDatabase.GetSkillIndexByName("Vampiric Gaze").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 154: return Data[" + SkillDatabase.GetSkillIndexByName("Plague Touch").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 155: return Data[" + SkillDatabase.GetSkillIndexByName("Vile Touch").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 156: return Data[" + SkillDatabase.GetSkillIndexByName("Vampiric Touch").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 157: return Data[" + SkillDatabase.GetSkillIndexByName("Blood Ritual").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 158: return Data[" + SkillDatabase.GetSkillIndexByName("Touch of Agony").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 159: return Data[" + SkillDatabase.GetSkillIndexByName("Weaken Armor").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 160: return Data[" + SkillDatabase.GetSkillIndexByName("Windborne Speed").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 162: return Data[" + SkillDatabase.GetSkillIndexByName("Gale").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 163: return Data[" + SkillDatabase.GetSkillIndexByName("Whirlwind").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 164: return Data[" + SkillDatabase.GetSkillIndexByName("Elemental Attunement").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 165: return Data[" + SkillDatabase.GetSkillIndexByName("Armor of Earth").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 166: return Data[" + SkillDatabase.GetSkillIndexByName("Kinetic Armor").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 167: return Data[" + SkillDatabase.GetSkillIndexByName("Eruption").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 168: return Data[" + SkillDatabase.GetSkillIndexByName("Magnetic Aura").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 169: return Data[" + SkillDatabase.GetSkillIndexByName("Earth Attunement").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 170: return Data[" + SkillDatabase.GetSkillIndexByName("Earthquake").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 171: return Data[" + SkillDatabase.GetSkillIndexByName("Stoning").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 172: return Data[" + SkillDatabase.GetSkillIndexByName("Stone Daggers").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 173: return Data[" + SkillDatabase.GetSkillIndexByName("Grasping Earth").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 174: return Data[" + SkillDatabase.GetSkillIndexByName("Aftershock").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 175: return Data[" + SkillDatabase.GetSkillIndexByName("Ward Against Elements").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 176: return Data[" + SkillDatabase.GetSkillIndexByName("Ward Against Melee").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 177: return Data[" + SkillDatabase.GetSkillIndexByName("Ward Against Foes").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 178: return Data[" + SkillDatabase.GetSkillIndexByName("Ether Prodigy").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 179: return Data[" + SkillDatabase.GetSkillIndexByName("Incendiary Bonds").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 180: return Data[" + SkillDatabase.GetSkillIndexByName("Aura of Restoration").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 181: return Data[" + SkillDatabase.GetSkillIndexByName("Ether Renewal").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 182: return Data[" + SkillDatabase.GetSkillIndexByName("Conjure Flame").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 183: return Data[" + SkillDatabase.GetSkillIndexByName("Inferno").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 184: return Data[" + SkillDatabase.GetSkillIndexByName("Fire Attunement").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 185: return Data[" + SkillDatabase.GetSkillIndexByName("Mind Burn").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 186: return Data[" + SkillDatabase.GetSkillIndexByName("Fireball").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 187: return Data[" + SkillDatabase.GetSkillIndexByName("Meteor").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 188: return Data[" + SkillDatabase.GetSkillIndexByName("Flame Burst").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 189: return Data[" + SkillDatabase.GetSkillIndexByName("Rodgort's Invocation").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 190: return Data[" + SkillDatabase.GetSkillIndexByName("Mark of Rodgort").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 191: return Data[" + SkillDatabase.GetSkillIndexByName("Immolate").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 192: return Data[" + SkillDatabase.GetSkillIndexByName("Meteor Shower").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 193: return Data[" + SkillDatabase.GetSkillIndexByName("Phoenix").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 194: return Data[" + SkillDatabase.GetSkillIndexByName("Flare").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 195: return Data[" + SkillDatabase.GetSkillIndexByName("Lava Font").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 196: return Data[" + SkillDatabase.GetSkillIndexByName("Searing Heat").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 197: return Data[" + SkillDatabase.GetSkillIndexByName("Fire Storm").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 198: return Data[" + SkillDatabase.GetSkillIndexByName("Glyph of Elemental Power").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 199: return Data[" + SkillDatabase.GetSkillIndexByName("Glyph of Energy").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 200: return Data[" + SkillDatabase.GetSkillIndexByName("Glyph of Lesser Energy").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 201: return Data[" + SkillDatabase.GetSkillIndexByName("Glyph of Concentration").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 202: return Data[" + SkillDatabase.GetSkillIndexByName("Glyph of Sacrifice").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 203: return Data[" + SkillDatabase.GetSkillIndexByName("Glyph of Renewal").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 204: return Data[" + SkillDatabase.GetSkillIndexByName("Rust").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 205: return Data[" + SkillDatabase.GetSkillIndexByName("Lightning Surge").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 206: return Data[" + SkillDatabase.GetSkillIndexByName("Armor of Frost").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 207: return Data[" + SkillDatabase.GetSkillIndexByName("Conjure Frost").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 208: return Data[" + SkillDatabase.GetSkillIndexByName("Water Attunement").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 209: return Data[" + SkillDatabase.GetSkillIndexByName("Mind Freeze").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 210: return Data[" + SkillDatabase.GetSkillIndexByName("Ice Prison").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 211: return Data[" + SkillDatabase.GetSkillIndexByName("Ice Spikes").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 212: return Data[" + SkillDatabase.GetSkillIndexByName("Frozen Burst").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 213: return Data[" + SkillDatabase.GetSkillIndexByName("Shard Storm").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 214: return Data[" + SkillDatabase.GetSkillIndexByName("Ice Spear").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 215: return Data[" + SkillDatabase.GetSkillIndexByName("Maelstrom").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 216: return Data[" + SkillDatabase.GetSkillIndexByName("Iron Mist").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 217: return Data[" + SkillDatabase.GetSkillIndexByName("Crystal Wave").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 218: return Data[" + SkillDatabase.GetSkillIndexByName("Obsidian Flesh").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 219: return Data[" + SkillDatabase.GetSkillIndexByName("Obsidian Flame").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 220: return Data[" + SkillDatabase.GetSkillIndexByName("Blinding Flash").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 221: return Data[" + SkillDatabase.GetSkillIndexByName("Conjure Lightning").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 222: return Data[" + SkillDatabase.GetSkillIndexByName("Lightning Strike").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 223: return Data[" + SkillDatabase.GetSkillIndexByName("Chain Lightning").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 224: return Data[" + SkillDatabase.GetSkillIndexByName("Enervating Charge").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 225: return Data[" + SkillDatabase.GetSkillIndexByName("Air Attunement").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 226: return Data[" + SkillDatabase.GetSkillIndexByName("Mind Shock").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 227: return Data[" + SkillDatabase.GetSkillIndexByName("Glimmering Mark").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 228: return Data[" + SkillDatabase.GetSkillIndexByName("Thunderclap").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 229: return Data[" + SkillDatabase.GetSkillIndexByName("Lightning Orb").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 230: return Data[" + SkillDatabase.GetSkillIndexByName("Lightning Javelin").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 231: return Data[" + SkillDatabase.GetSkillIndexByName("Shock").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 232: return Data[" + SkillDatabase.GetSkillIndexByName("Lightning Touch").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 233: return Data[" + SkillDatabase.GetSkillIndexByName("Swirling Aura").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 234: return Data[" + SkillDatabase.GetSkillIndexByName("Deep Freeze").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 235: return Data[" + SkillDatabase.GetSkillIndexByName("Blurred Vision").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 236: return Data[" + SkillDatabase.GetSkillIndexByName("Mist Form").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 237: return Data[" + SkillDatabase.GetSkillIndexByName("Water Trident").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 238: return Data[" + SkillDatabase.GetSkillIndexByName("Armor of Mist").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 239: return Data[" + SkillDatabase.GetSkillIndexByName("Ward Against Harm").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 240: return Data[" + SkillDatabase.GetSkillIndexByName("Smite").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 241: return Data[" + SkillDatabase.GetSkillIndexByName("Life Bond").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 242: return Data[" + SkillDatabase.GetSkillIndexByName("Balthazar's Spirit").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 243: return Data[" + SkillDatabase.GetSkillIndexByName("Strength of Honor").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 244: return Data[" + SkillDatabase.GetSkillIndexByName("Life Attunement").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 245: return Data[" + SkillDatabase.GetSkillIndexByName("Protective Spirit").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 246: return Data[" + SkillDatabase.GetSkillIndexByName("Divine Intervention").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 247: return Data[" + SkillDatabase.GetSkillIndexByName("Symbol of Wrath").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 248: return Data[" + SkillDatabase.GetSkillIndexByName("Retribution").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 249: return Data[" + SkillDatabase.GetSkillIndexByName("Holy Wrath").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 250: return Data[" + SkillDatabase.GetSkillIndexByName("Essence Bond").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 251: return Data[" + SkillDatabase.GetSkillIndexByName("Scourge Healing").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 252: return Data[" + SkillDatabase.GetSkillIndexByName("Banish").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 253: return Data[" + SkillDatabase.GetSkillIndexByName("Scourge Sacrifice").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 254: return Data[" + SkillDatabase.GetSkillIndexByName("Vigorous Spirit").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 255: return Data[" + SkillDatabase.GetSkillIndexByName("Watchful Spirit").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 256: return Data[" + SkillDatabase.GetSkillIndexByName("Blessed Aura").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 257: return Data[" + SkillDatabase.GetSkillIndexByName("Aegis").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 258: return Data[" + SkillDatabase.GetSkillIndexByName("Guardian").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 259: return Data[" + SkillDatabase.GetSkillIndexByName("Shield of Deflection").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 260: return Data[" + SkillDatabase.GetSkillIndexByName("Aura of Faith").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 261: return Data[" + SkillDatabase.GetSkillIndexByName("Shield of Regeneration").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 262: return Data[" + SkillDatabase.GetSkillIndexByName("Shield of Judgment").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 263: return Data[" + SkillDatabase.GetSkillIndexByName("Protective Bond").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 264: return Data[" + SkillDatabase.GetSkillIndexByName("Pacifism").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 265: return Data[" + SkillDatabase.GetSkillIndexByName("Amity").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 266: return Data[" + SkillDatabase.GetSkillIndexByName("Peace and Harmony").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 267: return Data[" + SkillDatabase.GetSkillIndexByName("Judge's Insight").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 268: return Data[" + SkillDatabase.GetSkillIndexByName("Unyielding Aura").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 269: return Data[" + SkillDatabase.GetSkillIndexByName("Mark of Protection").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 270: return Data[" + SkillDatabase.GetSkillIndexByName("Life Barrier").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 271: return Data[" + SkillDatabase.GetSkillIndexByName("Zealot's Fire").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 272: return Data[" + SkillDatabase.GetSkillIndexByName("Balthazar's Aura").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 273: return Data[" + SkillDatabase.GetSkillIndexByName("Spell Breaker").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 274: return Data[" + SkillDatabase.GetSkillIndexByName("Healing Seed").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 275: return Data[" + SkillDatabase.GetSkillIndexByName("Mend Condition").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 276: return Data[" + SkillDatabase.GetSkillIndexByName("Restore Condition").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 277: return Data[" + SkillDatabase.GetSkillIndexByName("Mend Ailment").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 278: return Data[" + SkillDatabase.GetSkillIndexByName("Purge Conditions").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 279: return Data[" + SkillDatabase.GetSkillIndexByName("Divine Healing").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 280: return Data[" + SkillDatabase.GetSkillIndexByName("Heal Area").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 281: return Data[" + SkillDatabase.GetSkillIndexByName("Orison of Healing").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 282: return Data[" + SkillDatabase.GetSkillIndexByName("Word of Healing").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 283: return Data[" + SkillDatabase.GetSkillIndexByName("Dwayna's Kiss").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 284: return Data[" + SkillDatabase.GetSkillIndexByName("Divine Boon").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 285: return Data[" + SkillDatabase.GetSkillIndexByName("Healing Hands").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 286: return Data[" + SkillDatabase.GetSkillIndexByName("Heal Other").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 287: return Data[" + SkillDatabase.GetSkillIndexByName("Heal Party").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 288: return Data[" + SkillDatabase.GetSkillIndexByName("Healing Breeze").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 289: return Data[" + SkillDatabase.GetSkillIndexByName("Vital Blessing").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 290: return Data[" + SkillDatabase.GetSkillIndexByName("Mending").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 291: return Data[" + SkillDatabase.GetSkillIndexByName("Live Vicariously").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 292: return Data[" + SkillDatabase.GetSkillIndexByName("Infuse Health").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 293: return Data[" + SkillDatabase.GetSkillIndexByName("Signet of Devotion").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 294: return Data[" + SkillDatabase.GetSkillIndexByName("Signet of Judgment").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 295: return Data[" + SkillDatabase.GetSkillIndexByName("Purge Signet").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 296: return Data[" + SkillDatabase.GetSkillIndexByName("Bane Signet").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 297: return Data[" + SkillDatabase.GetSkillIndexByName("Blessed Signet").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 298: return Data[" + SkillDatabase.GetSkillIndexByName("Martyr").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 299: return Data[" + SkillDatabase.GetSkillIndexByName("Shielding Hands").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 300: return Data[" + SkillDatabase.GetSkillIndexByName("Contemplation of Purity").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 301: return Data[" + SkillDatabase.GetSkillIndexByName("Remove Hex").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 302: return Data[" + SkillDatabase.GetSkillIndexByName("Smite Hex").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 303: return Data[" + SkillDatabase.GetSkillIndexByName("Convert Hexes").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 304: return Data[" + SkillDatabase.GetSkillIndexByName("Light of Dwayna").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 305: return Data[" + SkillDatabase.GetSkillIndexByName("Resurrect").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 306: return Data[" + SkillDatabase.GetSkillIndexByName("Rebirth").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 307: return Data[" + SkillDatabase.GetSkillIndexByName("Reversal of Fortune").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 308: return Data[" + SkillDatabase.GetSkillIndexByName("Succor").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 309: return Data[" + SkillDatabase.GetSkillIndexByName("Holy Veil").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 310: return Data[" + SkillDatabase.GetSkillIndexByName("Divine Spirit").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 311: return Data[" + SkillDatabase.GetSkillIndexByName("Draw Conditions").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 312: return Data[" + SkillDatabase.GetSkillIndexByName("Holy Strike").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 313: return Data[" + SkillDatabase.GetSkillIndexByName("Healing Touch").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 314: return Data[" + SkillDatabase.GetSkillIndexByName("Restore Life").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 315: return Data[" + SkillDatabase.GetSkillIndexByName("Vengeance").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 316: return Data[" + SkillDatabase.GetSkillIndexByName("\"To the Limit!\"").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 317: return Data[" + SkillDatabase.GetSkillIndexByName("Battle Rage").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 318: return Data[" + SkillDatabase.GetSkillIndexByName("Defy Pain").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 319: return Data[" + SkillDatabase.GetSkillIndexByName("Rush").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 320: return Data[" + SkillDatabase.GetSkillIndexByName("Hamstring").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 321: return Data[" + SkillDatabase.GetSkillIndexByName("Wild Blow").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 322: return Data[" + SkillDatabase.GetSkillIndexByName("Power Attack").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 323: return Data[" + SkillDatabase.GetSkillIndexByName("Desperation Blow").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 324: return Data[" + SkillDatabase.GetSkillIndexByName("Thrill of Victory").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 325: return Data[" + SkillDatabase.GetSkillIndexByName("Distracting Blow").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 326: return Data[" + SkillDatabase.GetSkillIndexByName("Protector's Strike").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 327: return Data[" + SkillDatabase.GetSkillIndexByName("Griffon's Sweep").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 328: return Data[" + SkillDatabase.GetSkillIndexByName("Pure Strike").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 329: return Data[" + SkillDatabase.GetSkillIndexByName("Skull Crack").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 330: return Data[" + SkillDatabase.GetSkillIndexByName("Cyclone Axe").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 331: return Data[" + SkillDatabase.GetSkillIndexByName("Hammer Bash").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 332: return Data[" + SkillDatabase.GetSkillIndexByName("Bull's Strike").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 333: return Data[" + SkillDatabase.GetSkillIndexByName("\"I Will Avenge You!\"").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 334: return Data[" + SkillDatabase.GetSkillIndexByName("Axe Rake").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 335: return Data[" + SkillDatabase.GetSkillIndexByName("Cleave").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 336: return Data[" + SkillDatabase.GetSkillIndexByName("Executioner's Strike").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 337: return Data[" + SkillDatabase.GetSkillIndexByName("Dismember").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 338: return Data[" + SkillDatabase.GetSkillIndexByName("Eviscerate").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 339: return Data[" + SkillDatabase.GetSkillIndexByName("Penetrating Blow").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 340: return Data[" + SkillDatabase.GetSkillIndexByName("Disrupting Chop").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 341: return Data[" + SkillDatabase.GetSkillIndexByName("Swift Chop").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 342: return Data[" + SkillDatabase.GetSkillIndexByName("Axe Twist").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 343: return Data[" + SkillDatabase.GetSkillIndexByName("\"For Great Justice!\"").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 344: return Data[" + SkillDatabase.GetSkillIndexByName("Flurry").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 345: return Data[" + SkillDatabase.GetSkillIndexByName("Defensive Stance").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 346: return Data[" + SkillDatabase.GetSkillIndexByName("Frenzy").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 347: return Data[" + SkillDatabase.GetSkillIndexByName("Endure Pain").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 348: return Data[" + SkillDatabase.GetSkillIndexByName("\"Watch Yourself!\"").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 349: return Data[" + SkillDatabase.GetSkillIndexByName("Sprint").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 350: return Data[" + SkillDatabase.GetSkillIndexByName("Belly Smash").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 351: return Data[" + SkillDatabase.GetSkillIndexByName("Mighty Blow").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 352: return Data[" + SkillDatabase.GetSkillIndexByName("Crushing Blow").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 353: return Data[" + SkillDatabase.GetSkillIndexByName("Crude Swing").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 354: return Data[" + SkillDatabase.GetSkillIndexByName("Earth Shaker").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 355: return Data[" + SkillDatabase.GetSkillIndexByName("Devastating Hammer").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 356: return Data[" + SkillDatabase.GetSkillIndexByName("Irresistible Blow").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 357: return Data[" + SkillDatabase.GetSkillIndexByName("Counter Blow").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 358: return Data[" + SkillDatabase.GetSkillIndexByName("Backbreaker").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 359: return Data[" + SkillDatabase.GetSkillIndexByName("Heavy Blow").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 360: return Data[" + SkillDatabase.GetSkillIndexByName("Staggering Blow").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 361: return Data[" + SkillDatabase.GetSkillIndexByName("Dolyak Signet").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 362: return Data[" + SkillDatabase.GetSkillIndexByName("Warrior's Cunning").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 363: return Data[" + SkillDatabase.GetSkillIndexByName("Shield Bash").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 364: return Data[" + SkillDatabase.GetSkillIndexByName("\"Charge!\"").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 365: return Data[" + SkillDatabase.GetSkillIndexByName("\"Victory is Mine!\"").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 366: return Data[" + SkillDatabase.GetSkillIndexByName("\"Fear Me!\"").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 367: return Data[" + SkillDatabase.GetSkillIndexByName("\"Shields Up!\"").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 368: return Data[" + SkillDatabase.GetSkillIndexByName("\"I Will Survive!\"").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 370: return Data[" + SkillDatabase.GetSkillIndexByName("Berserker Stance").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 371: return Data[" + SkillDatabase.GetSkillIndexByName("Balanced Stance").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 372: return Data[" + SkillDatabase.GetSkillIndexByName("Gladiator's Defense").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 373: return Data[" + SkillDatabase.GetSkillIndexByName("Deflect Arrows").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 374: return Data[" + SkillDatabase.GetSkillIndexByName("Warrior's Endurance").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 375: return Data[" + SkillDatabase.GetSkillIndexByName("Dwarven Battle Stance").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 376: return Data[" + SkillDatabase.GetSkillIndexByName("Disciplined Stance").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 377: return Data[" + SkillDatabase.GetSkillIndexByName("Wary Stance").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 378: return Data[" + SkillDatabase.GetSkillIndexByName("Shield Stance").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 379: return Data[" + SkillDatabase.GetSkillIndexByName("Bull's Charge").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 380: return Data[" + SkillDatabase.GetSkillIndexByName("Bonetti's Defense").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 381: return Data[" + SkillDatabase.GetSkillIndexByName("Hundred Blades").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 382: return Data[" + SkillDatabase.GetSkillIndexByName("Sever Artery").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 383: return Data[" + SkillDatabase.GetSkillIndexByName("Galrath Slash").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 384: return Data[" + SkillDatabase.GetSkillIndexByName("Gash").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 385: return Data[" + SkillDatabase.GetSkillIndexByName("Final Thrust").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 386: return Data[" + SkillDatabase.GetSkillIndexByName("Seeking Blade").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 387: return Data[" + SkillDatabase.GetSkillIndexByName("Riposte").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 388: return Data[" + SkillDatabase.GetSkillIndexByName("Deadly Riposte").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 389: return Data[" + SkillDatabase.GetSkillIndexByName("Flourish").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 390: return Data[" + SkillDatabase.GetSkillIndexByName("Savage Slash").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 391: return Data[" + SkillDatabase.GetSkillIndexByName("Hunter's Shot").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 392: return Data[" + SkillDatabase.GetSkillIndexByName("Pin Down").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 393: return Data[" + SkillDatabase.GetSkillIndexByName("Crippling Shot").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 394: return Data[" + SkillDatabase.GetSkillIndexByName("Power Shot").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 395: return Data[" + SkillDatabase.GetSkillIndexByName("Barrage").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 396: return Data[" + SkillDatabase.GetSkillIndexByName("Dual Shot").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 397: return Data[" + SkillDatabase.GetSkillIndexByName("Quick Shot").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 398: return Data[" + SkillDatabase.GetSkillIndexByName("Penetrating Attack").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 399: return Data[" + SkillDatabase.GetSkillIndexByName("Distracting Shot").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 400: return Data[" + SkillDatabase.GetSkillIndexByName("Precision Shot").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 402: return Data[" + SkillDatabase.GetSkillIndexByName("Determined Shot").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 403: return Data[" + SkillDatabase.GetSkillIndexByName("Called Shot").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 404: return Data[" + SkillDatabase.GetSkillIndexByName("Poison Arrow").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 405: return Data[" + SkillDatabase.GetSkillIndexByName("Oath Shot").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 406: return Data[" + SkillDatabase.GetSkillIndexByName("Debilitating Shot").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 407: return Data[" + SkillDatabase.GetSkillIndexByName("Point Blank Shot").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 408: return Data[" + SkillDatabase.GetSkillIndexByName("Concussion Shot").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 409: return Data[" + SkillDatabase.GetSkillIndexByName("Punishing Shot").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 411: return Data[" + SkillDatabase.GetSkillIndexByName("Charm Animal").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 412: return Data[" + SkillDatabase.GetSkillIndexByName("Call of Protection").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 415: return Data[" + SkillDatabase.GetSkillIndexByName("Call of Haste").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 422: return Data[" + SkillDatabase.GetSkillIndexByName("Revive Animal").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 423: return Data[" + SkillDatabase.GetSkillIndexByName("Symbiotic Bond").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 424: return Data[" + SkillDatabase.GetSkillIndexByName("Throw Dirt").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 425: return Data[" + SkillDatabase.GetSkillIndexByName("Dodge").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 426: return Data[" + SkillDatabase.GetSkillIndexByName("Savage Shot").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 427: return Data[" + SkillDatabase.GetSkillIndexByName("Antidote Signet").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 428: return Data[" + SkillDatabase.GetSkillIndexByName("Incendiary Arrows").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 429: return Data[" + SkillDatabase.GetSkillIndexByName("Melandru's Arrows").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 430: return Data[" + SkillDatabase.GetSkillIndexByName("Marksman's Wager").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 431: return Data[" + SkillDatabase.GetSkillIndexByName("Ignite Arrows").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 432: return Data[" + SkillDatabase.GetSkillIndexByName("Read the Wind").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 433: return Data[" + SkillDatabase.GetSkillIndexByName("Kindle Arrows").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 434: return Data[" + SkillDatabase.GetSkillIndexByName("Choking Gas").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 435: return Data[" + SkillDatabase.GetSkillIndexByName("Apply Poison").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 436: return Data[" + SkillDatabase.GetSkillIndexByName("Comfort Animal").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 437: return Data[" + SkillDatabase.GetSkillIndexByName("Bestial Pounce").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 438: return Data[" + SkillDatabase.GetSkillIndexByName("Maiming Strike").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 439: return Data[" + SkillDatabase.GetSkillIndexByName("Feral Lunge").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 440: return Data[" + SkillDatabase.GetSkillIndexByName("Scavenger Strike").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 441: return Data[" + SkillDatabase.GetSkillIndexByName("Melandru's Assault").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 442: return Data[" + SkillDatabase.GetSkillIndexByName("Ferocious Strike").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 443: return Data[" + SkillDatabase.GetSkillIndexByName("Predator's Pounce").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 444: return Data[" + SkillDatabase.GetSkillIndexByName("Brutal Strike").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 445: return Data[" + SkillDatabase.GetSkillIndexByName("Disrupting Lunge").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 446: return Data[" + SkillDatabase.GetSkillIndexByName("Troll Unguent").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 447: return Data[" + SkillDatabase.GetSkillIndexByName("Otyugh's Cry").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 448: return Data[" + SkillDatabase.GetSkillIndexByName("Escape").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 449: return Data[" + SkillDatabase.GetSkillIndexByName("Practiced Stance").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 450: return Data[" + SkillDatabase.GetSkillIndexByName("Whirling Defense").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 451: return Data[" + SkillDatabase.GetSkillIndexByName("Melandru's Resilience").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 452: return Data[" + SkillDatabase.GetSkillIndexByName("Dryder's Defenses").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 453: return Data[" + SkillDatabase.GetSkillIndexByName("Lightning Reflexes").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 454: return Data[" + SkillDatabase.GetSkillIndexByName("Tiger's Fury").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 455: return Data[" + SkillDatabase.GetSkillIndexByName("Storm Chaser").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 456: return Data[" + SkillDatabase.GetSkillIndexByName("Serpent's Quickness").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 457: return Data[" + SkillDatabase.GetSkillIndexByName("Dust Trap").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 458: return Data[" + SkillDatabase.GetSkillIndexByName("Barbed Trap").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 459: return Data[" + SkillDatabase.GetSkillIndexByName("Flame Trap").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 460: return Data[" + SkillDatabase.GetSkillIndexByName("Healing Spring").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 461: return Data[" + SkillDatabase.GetSkillIndexByName("Spike Trap").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 462: return Data[" + SkillDatabase.GetSkillIndexByName("Winter").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 463: return Data[" + SkillDatabase.GetSkillIndexByName("Winnowing").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 464: return Data[" + SkillDatabase.GetSkillIndexByName("Edge of Extinction").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 465: return Data[" + SkillDatabase.GetSkillIndexByName("Greater Conflagration").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 466: return Data[" + SkillDatabase.GetSkillIndexByName("Conflagration").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 467: return Data[" + SkillDatabase.GetSkillIndexByName("Fertile Season").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 468: return Data[" + SkillDatabase.GetSkillIndexByName("Symbiosis").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 469: return Data[" + SkillDatabase.GetSkillIndexByName("Primal Echoes").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 470: return Data[" + SkillDatabase.GetSkillIndexByName("Predatory Season").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 471: return Data[" + SkillDatabase.GetSkillIndexByName("Frozen Soil").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 472: return Data[" + SkillDatabase.GetSkillIndexByName("Favorable Winds").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 474: return Data[" + SkillDatabase.GetSkillIndexByName("Energizing Wind").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 475: return Data[" + SkillDatabase.GetSkillIndexByName("Quickening Zephyr").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 476: return Data[" + SkillDatabase.GetSkillIndexByName("Nature's Renewal").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 477: return Data[" + SkillDatabase.GetSkillIndexByName("Muddy Terrain").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 570: return Data[" + SkillDatabase.GetSkillIndexByName("Mark of Insecurity").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 571: return Data[" + SkillDatabase.GetSkillIndexByName("Disrupting Dagger").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 572: return Data[" + SkillDatabase.GetSkillIndexByName("Deadly Paradox").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 763: return Data[" + SkillDatabase.GetSkillIndexByName("Jaundiced Gaze").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 764: return Data[" + SkillDatabase.GetSkillIndexByName("Wail of Doom").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 766: return Data[" + SkillDatabase.GetSkillIndexByName("Gaze of Contempt").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 769: return Data[" + SkillDatabase.GetSkillIndexByName("Viper's Defense").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 770: return Data[" + SkillDatabase.GetSkillIndexByName("Return").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 771: return Data[" + SkillDatabase.GetSkillIndexByName("Aura of Displacement").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 772: return Data[" + SkillDatabase.GetSkillIndexByName("Generous Was Tsungrai").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 773: return Data[" + SkillDatabase.GetSkillIndexByName("Mighty Was Vorizun").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 775: return Data[" + SkillDatabase.GetSkillIndexByName("Death Blossom").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 776: return Data[" + SkillDatabase.GetSkillIndexByName("Twisting Fangs").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 777: return Data[" + SkillDatabase.GetSkillIndexByName("Horns of the Ox").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 778: return Data[" + SkillDatabase.GetSkillIndexByName("Falling Spider").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 779: return Data[" + SkillDatabase.GetSkillIndexByName("Black Lotus Strike").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 780: return Data[" + SkillDatabase.GetSkillIndexByName("Fox Fangs").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 781: return Data[" + SkillDatabase.GetSkillIndexByName("Moebius Strike").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 782: return Data[" + SkillDatabase.GetSkillIndexByName("Jagged Strike").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 783: return Data[" + SkillDatabase.GetSkillIndexByName("Unsuspecting Strike").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 784: return Data[" + SkillDatabase.GetSkillIndexByName("Entangling Asp").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 785: return Data[" + SkillDatabase.GetSkillIndexByName("Mark of Death").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 786: return Data[" + SkillDatabase.GetSkillIndexByName("Iron Palm").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 787: return Data[" + SkillDatabase.GetSkillIndexByName("Resilient Weapon").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 788: return Data[" + SkillDatabase.GetSkillIndexByName("Blind Was Mingson").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 789: return Data[" + SkillDatabase.GetSkillIndexByName("Grasping Was Kuurong").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 790: return Data[" + SkillDatabase.GetSkillIndexByName("Vengeful Was Khanhei").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 791: return Data[" + SkillDatabase.GetSkillIndexByName("Flesh of My Flesh").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 792: return Data[" + SkillDatabase.GetSkillIndexByName("Splinter Weapon").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 793: return Data[" + SkillDatabase.GetSkillIndexByName("Weapon of Warding").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 794: return Data[" + SkillDatabase.GetSkillIndexByName("Wailing Weapon").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 795: return Data[" + SkillDatabase.GetSkillIndexByName("Nightmare Weapon").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 799: return Data[" + SkillDatabase.GetSkillIndexByName("Beguiling Haze").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 800: return Data[" + SkillDatabase.GetSkillIndexByName("Enduring Toxin").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 801: return Data[" + SkillDatabase.GetSkillIndexByName("Shroud of Silence").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 802: return Data[" + SkillDatabase.GetSkillIndexByName("Expose Defenses").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 803: return Data[" + SkillDatabase.GetSkillIndexByName("Power Leech").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 804: return Data[" + SkillDatabase.GetSkillIndexByName("Arcane Languor").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 805: return Data[" + SkillDatabase.GetSkillIndexByName("Animate Vampiric Horror").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 806: return Data[" + SkillDatabase.GetSkillIndexByName("Cultist's Fervor").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 808: return Data[" + SkillDatabase.GetSkillIndexByName("Reaper's Mark").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 809: return Data[" + SkillDatabase.GetSkillIndexByName("Shatterstone").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 810: return Data[" + SkillDatabase.GetSkillIndexByName("Protector's Defense").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 811: return Data[" + SkillDatabase.GetSkillIndexByName("Run as One").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 812: return Data[" + SkillDatabase.GetSkillIndexByName("Defiant Was Xinrae").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 813: return Data[" + SkillDatabase.GetSkillIndexByName("Lyssa's Aura").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 814: return Data[" + SkillDatabase.GetSkillIndexByName("Shadow Refuge").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 815: return Data[" + SkillDatabase.GetSkillIndexByName("Scorpion Wire").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 816: return Data[" + SkillDatabase.GetSkillIndexByName("Mirrored Stance").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 817: return Data[" + SkillDatabase.GetSkillIndexByName("Discord").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 818: return Data[" + SkillDatabase.GetSkillIndexByName("Well of Weariness").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 819: return Data[" + SkillDatabase.GetSkillIndexByName("Vampiric Spirit").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 820: return Data[" + SkillDatabase.GetSkillIndexByName("Depravity").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 821: return Data[" + SkillDatabase.GetSkillIndexByName("Icy Veins").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 822: return Data[" + SkillDatabase.GetSkillIndexByName("Weaken Knees").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 823: return Data[" + SkillDatabase.GetSkillIndexByName("Burning Speed").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 824: return Data[" + SkillDatabase.GetSkillIndexByName("Lava Arrows").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 825: return Data[" + SkillDatabase.GetSkillIndexByName("Bed of Coals").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 826: return Data[" + SkillDatabase.GetSkillIndexByName("Shadow Form").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 827: return Data[" + SkillDatabase.GetSkillIndexByName("Siphon Strength").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 828: return Data[" + SkillDatabase.GetSkillIndexByName("Vile Miasma").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 830: return Data[" + SkillDatabase.GetSkillIndexByName("Ray of Judgment").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 831: return Data[" + SkillDatabase.GetSkillIndexByName("Primal Rage").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 832: return Data[" + SkillDatabase.GetSkillIndexByName("Animate Flesh Golem").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 834: return Data[" + SkillDatabase.GetSkillIndexByName("Reckless Haste").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 835: return Data[" + SkillDatabase.GetSkillIndexByName("Blood Bond").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 836: return Data[" + SkillDatabase.GetSkillIndexByName("Ride the Lightning").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 837: return Data[" + SkillDatabase.GetSkillIndexByName("Energy Boon").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 838: return Data[" + SkillDatabase.GetSkillIndexByName("Dwayna's Sorrow").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 839: return Data[" + SkillDatabase.GetSkillIndexByName("\"Retreat!\"").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 840: return Data[" + SkillDatabase.GetSkillIndexByName("Poisoned Heart").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 841: return Data[" + SkillDatabase.GetSkillIndexByName("Fetid Ground").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 842: return Data[" + SkillDatabase.GetSkillIndexByName("Arc Lightning").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 843: return Data[" + SkillDatabase.GetSkillIndexByName("Gust").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 844: return Data[" + SkillDatabase.GetSkillIndexByName("Churning Earth").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 845: return Data[" + SkillDatabase.GetSkillIndexByName("Liquid Flame").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 846: return Data[" + SkillDatabase.GetSkillIndexByName("Steam").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 847: return Data[" + SkillDatabase.GetSkillIndexByName("Boon Signet").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 848: return Data[" + SkillDatabase.GetSkillIndexByName("Reverse Hex").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 849: return Data[" + SkillDatabase.GetSkillIndexByName("Lacerating Chop").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 850: return Data[" + SkillDatabase.GetSkillIndexByName("Fierce Blow").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 851: return Data[" + SkillDatabase.GetSkillIndexByName("Sun and Moon Slash").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 852: return Data[" + SkillDatabase.GetSkillIndexByName("Splinter Shot").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 853: return Data[" + SkillDatabase.GetSkillIndexByName("Melandru's Shot").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 854: return Data[" + SkillDatabase.GetSkillIndexByName("Snare").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 858: return Data[" + SkillDatabase.GetSkillIndexByName("Dancing Daggers").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 859: return Data[" + SkillDatabase.GetSkillIndexByName("Conjure Nightmare").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 860: return Data[" + SkillDatabase.GetSkillIndexByName("Signet of Disruption").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 862: return Data[" + SkillDatabase.GetSkillIndexByName("Ravenous Gaze").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 863: return Data[" + SkillDatabase.GetSkillIndexByName("Order of Apostasy").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 864: return Data[" + SkillDatabase.GetSkillIndexByName("Oppressive Gaze").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 865: return Data[" + SkillDatabase.GetSkillIndexByName("Lightning Hammer").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 866: return Data[" + SkillDatabase.GetSkillIndexByName("Vapor Blade").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 867: return Data[" + SkillDatabase.GetSkillIndexByName("Healing Light").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 869: return Data[" + SkillDatabase.GetSkillIndexByName("\"Coward!\"").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 870: return Data[" + SkillDatabase.GetSkillIndexByName("Pestilence").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 871: return Data[" + SkillDatabase.GetSkillIndexByName("Shadowsong").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 876: return Data[" + SkillDatabase.GetSkillIndexByName("Signet of Shadows").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 877: return Data[" + SkillDatabase.GetSkillIndexByName("Lyssa's Balance").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 878: return Data[" + SkillDatabase.GetSkillIndexByName("Visions of Regret").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 879: return Data[" + SkillDatabase.GetSkillIndexByName("Illusion of Pain").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 880: return Data[" + SkillDatabase.GetSkillIndexByName("Stolen Speed").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 881: return Data[" + SkillDatabase.GetSkillIndexByName("Ether Signet").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 882: return Data[" + SkillDatabase.GetSkillIndexByName("Signet of Disenchantment").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 883: return Data[" + SkillDatabase.GetSkillIndexByName("Vocal Minority").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 884: return Data[" + SkillDatabase.GetSkillIndexByName("Searing Flames").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 885: return Data[" + SkillDatabase.GetSkillIndexByName("Shield Guardian").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 886: return Data[" + SkillDatabase.GetSkillIndexByName("Restful Breeze").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 887: return Data[" + SkillDatabase.GetSkillIndexByName("Signet of Rejuvenation").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 888: return Data[" + SkillDatabase.GetSkillIndexByName("Whirling Axe").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 889: return Data[" + SkillDatabase.GetSkillIndexByName("Forceful Blow").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 891: return Data[" + SkillDatabase.GetSkillIndexByName("\"None Shall Pass!\"").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 892: return Data[" + SkillDatabase.GetSkillIndexByName("Quivering Blade").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 893: return Data[" + SkillDatabase.GetSkillIndexByName("Seeking Arrows").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 898: return Data[" + SkillDatabase.GetSkillIndexByName("Overload").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 899: return Data[" + SkillDatabase.GetSkillIndexByName("Images of Remorse").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 900: return Data[" + SkillDatabase.GetSkillIndexByName("Shared Burden").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 901: return Data[" + SkillDatabase.GetSkillIndexByName("Soul Bind").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 902: return Data[" + SkillDatabase.GetSkillIndexByName("Blood of the Aggressor").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 903: return Data[" + SkillDatabase.GetSkillIndexByName("Icy Prism").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 904: return Data[" + SkillDatabase.GetSkillIndexByName("Furious Axe").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 905: return Data[" + SkillDatabase.GetSkillIndexByName("Auspicious Blow").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 906: return Data[" + SkillDatabase.GetSkillIndexByName("\"On Your Knees!\"").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 907: return Data[" + SkillDatabase.GetSkillIndexByName("Dragon Slash").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 908: return Data[" + SkillDatabase.GetSkillIndexByName("Marauder's Shot").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 909: return Data[" + SkillDatabase.GetSkillIndexByName("Focused Shot").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 910: return Data[" + SkillDatabase.GetSkillIndexByName("Spirit Rift").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 911: return Data[" + SkillDatabase.GetSkillIndexByName("Union").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 913: return Data[" + SkillDatabase.GetSkillIndexByName("Tranquil Was Tanasen").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 914: return Data[" + SkillDatabase.GetSkillIndexByName("Consume Soul").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 915: return Data[" + SkillDatabase.GetSkillIndexByName("Spirit Light").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 916: return Data[" + SkillDatabase.GetSkillIndexByName("Lamentation").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 917: return Data[" + SkillDatabase.GetSkillIndexByName("Rupture Soul").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 918: return Data[" + SkillDatabase.GetSkillIndexByName("Spirit to Flesh").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 919: return Data[" + SkillDatabase.GetSkillIndexByName("Spirit Burn").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 920: return Data[" + SkillDatabase.GetSkillIndexByName("Destruction").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 921: return Data[" + SkillDatabase.GetSkillIndexByName("Dissonance").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 923: return Data[" + SkillDatabase.GetSkillIndexByName("Disenchantment").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 925: return Data[" + SkillDatabase.GetSkillIndexByName("Recall").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 926: return Data[" + SkillDatabase.GetSkillIndexByName("Sharpen Daggers").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 927: return Data[" + SkillDatabase.GetSkillIndexByName("Shameful Fear").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 928: return Data[" + SkillDatabase.GetSkillIndexByName("Shadow Shroud").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 929: return Data[" + SkillDatabase.GetSkillIndexByName("Shadow of Haste").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 930: return Data[" + SkillDatabase.GetSkillIndexByName("Auspicious Incantation").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 931: return Data[" + SkillDatabase.GetSkillIndexByName("Power Return").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 932: return Data[" + SkillDatabase.GetSkillIndexByName("Complicate").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 933: return Data[" + SkillDatabase.GetSkillIndexByName("Shatter Storm").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 934: return Data[" + SkillDatabase.GetSkillIndexByName("Unnatural Signet").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 935: return Data[" + SkillDatabase.GetSkillIndexByName("Rising Bile").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 936: return Data[" + SkillDatabase.GetSkillIndexByName("Envenom Enchantments").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 937: return Data[" + SkillDatabase.GetSkillIndexByName("Shockwave").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 938: return Data[" + SkillDatabase.GetSkillIndexByName("Ward of Stability").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 939: return Data[" + SkillDatabase.GetSkillIndexByName("Icy Shackles").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 941: return Data[" + SkillDatabase.GetSkillIndexByName("Blessed Light").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 942: return Data[" + SkillDatabase.GetSkillIndexByName("Withdraw Hexes").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 943: return Data[" + SkillDatabase.GetSkillIndexByName("Extinguish").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 944: return Data[" + SkillDatabase.GetSkillIndexByName("Signet of Strength").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 946: return Data[" + SkillDatabase.GetSkillIndexByName("Trapper's Focus").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 947: return Data[" + SkillDatabase.GetSkillIndexByName("Brambles").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 948: return Data[" + SkillDatabase.GetSkillIndexByName("Desperate Strike").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 949: return Data[" + SkillDatabase.GetSkillIndexByName("Way of the Fox").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 950: return Data[" + SkillDatabase.GetSkillIndexByName("Shadowy Burden").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 951: return Data[" + SkillDatabase.GetSkillIndexByName("Siphon Speed").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 952: return Data[" + SkillDatabase.GetSkillIndexByName("Death's Charge").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 953: return Data[" + SkillDatabase.GetSkillIndexByName("Power Flux").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 954: return Data[" + SkillDatabase.GetSkillIndexByName("Expel Hexes").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 955: return Data[" + SkillDatabase.GetSkillIndexByName("Rip Enchantment").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 957: return Data[" + SkillDatabase.GetSkillIndexByName("Spell Shield").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 958: return Data[" + SkillDatabase.GetSkillIndexByName("Healing Whisper").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 959: return Data[" + SkillDatabase.GetSkillIndexByName("Ethereal Light").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 960: return Data[" + SkillDatabase.GetSkillIndexByName("Release Enchantments").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 961: return Data[" + SkillDatabase.GetSkillIndexByName("Lacerate").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 962: return Data[" + SkillDatabase.GetSkillIndexByName("Spirit Transfer").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 963: return Data[" + SkillDatabase.GetSkillIndexByName("Restoration").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 964: return Data[" + SkillDatabase.GetSkillIndexByName("Vengeful Weapon").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 973: return Data[" + SkillDatabase.GetSkillIndexByName("Blinding Powder").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 974: return Data[" + SkillDatabase.GetSkillIndexByName("Mantis Touch").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 975: return Data[" + SkillDatabase.GetSkillIndexByName("Exhausting Assault").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 976: return Data[" + SkillDatabase.GetSkillIndexByName("Repeating Strike").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 977: return Data[" + SkillDatabase.GetSkillIndexByName("Way of the Lotus").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 978: return Data[" + SkillDatabase.GetSkillIndexByName("Mark of Instability").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 979: return Data[" + SkillDatabase.GetSkillIndexByName("Mistrust").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 980: return Data[" + SkillDatabase.GetSkillIndexByName("Feast of Souls").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 981: return Data[" + SkillDatabase.GetSkillIndexByName("Recuperation").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 982: return Data[" + SkillDatabase.GetSkillIndexByName("Shelter").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 983: return Data[" + SkillDatabase.GetSkillIndexByName("Weapon of Shadow").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 985: return Data[" + SkillDatabase.GetSkillIndexByName("Caltrops").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 986: return Data[" + SkillDatabase.GetSkillIndexByName("Nine Tail Strike").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 987: return Data[" + SkillDatabase.GetSkillIndexByName("Way of the Empty Palm").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 988: return Data[" + SkillDatabase.GetSkillIndexByName("Temple Strike").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 989: return Data[" + SkillDatabase.GetSkillIndexByName("Golden Phoenix Strike").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 990: return Data[" + SkillDatabase.GetSkillIndexByName("Expunge Enchantments").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 991: return Data[" + SkillDatabase.GetSkillIndexByName("Deny Hexes").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 992: return Data[" + SkillDatabase.GetSkillIndexByName("Triple Chop").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 993: return Data[" + SkillDatabase.GetSkillIndexByName("Enraged Smash").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 994: return Data[" + SkillDatabase.GetSkillIndexByName("Renewing Smash").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 995: return Data[" + SkillDatabase.GetSkillIndexByName("Tiger Stance").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 996: return Data[" + SkillDatabase.GetSkillIndexByName("Standing Slash").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 997: return Data[" + SkillDatabase.GetSkillIndexByName("Famine").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1018: return Data[" + SkillDatabase.GetSkillIndexByName("Critical Eye").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1019: return Data[" + SkillDatabase.GetSkillIndexByName("Critical Strike").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1020: return Data[" + SkillDatabase.GetSkillIndexByName("Blades of Steel").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1021: return Data[" + SkillDatabase.GetSkillIndexByName("Jungle Strike").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1022: return Data[" + SkillDatabase.GetSkillIndexByName("Wild Strike").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1023: return Data[" + SkillDatabase.GetSkillIndexByName("Leaping Mantis Sting").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1024: return Data[" + SkillDatabase.GetSkillIndexByName("Black Mantis Thrust").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1025: return Data[" + SkillDatabase.GetSkillIndexByName("Disrupting Stab").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1026: return Data[" + SkillDatabase.GetSkillIndexByName("Golden Lotus Strike").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1027: return Data[" + SkillDatabase.GetSkillIndexByName("Critical Defenses").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1028: return Data[" + SkillDatabase.GetSkillIndexByName("Way of Perfection").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1029: return Data[" + SkillDatabase.GetSkillIndexByName("Dark Apostasy").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1030: return Data[" + SkillDatabase.GetSkillIndexByName("Locust's Fury").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1031: return Data[" + SkillDatabase.GetSkillIndexByName("Shroud of Distress").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1032: return Data[" + SkillDatabase.GetSkillIndexByName("Heart of Shadow").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1033: return Data[" + SkillDatabase.GetSkillIndexByName("Impale").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1034: return Data[" + SkillDatabase.GetSkillIndexByName("Seeping Wound").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1035: return Data[" + SkillDatabase.GetSkillIndexByName("Assassin's Promise").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1036: return Data[" + SkillDatabase.GetSkillIndexByName("Signet of Malice").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1037: return Data[" + SkillDatabase.GetSkillIndexByName("Dark Escape").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1038: return Data[" + SkillDatabase.GetSkillIndexByName("Crippling Dagger").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1040: return Data[" + SkillDatabase.GetSkillIndexByName("Spirit Walk").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1041: return Data[" + SkillDatabase.GetSkillIndexByName("Unseen Fury").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1042: return Data[" + SkillDatabase.GetSkillIndexByName("Flashing Blades").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1043: return Data[" + SkillDatabase.GetSkillIndexByName("Dash").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1044: return Data[" + SkillDatabase.GetSkillIndexByName("Dark Prison").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1045: return Data[" + SkillDatabase.GetSkillIndexByName("Palm Strike").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1048: return Data[" + SkillDatabase.GetSkillIndexByName("Revealed Enchantment").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1049: return Data[" + SkillDatabase.GetSkillIndexByName("Revealed Hex").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1052: return Data[" + SkillDatabase.GetSkillIndexByName("Accumulated Pain").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1053: return Data[" + SkillDatabase.GetSkillIndexByName("Psychic Distraction").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1054: return Data[" + SkillDatabase.GetSkillIndexByName("Ancestor's Visage").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1055: return Data[" + SkillDatabase.GetSkillIndexByName("Recurring Insecurity").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1056: return Data[" + SkillDatabase.GetSkillIndexByName("Kitah's Burden").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1057: return Data[" + SkillDatabase.GetSkillIndexByName("Psychic Instability").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1059: return Data[" + SkillDatabase.GetSkillIndexByName("Hex Eater Signet").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1061: return Data[" + SkillDatabase.GetSkillIndexByName("Feedback").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1062: return Data[" + SkillDatabase.GetSkillIndexByName("Arcane Larceny").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1066: return Data[" + SkillDatabase.GetSkillIndexByName("Spoil Victor").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1067: return Data[" + SkillDatabase.GetSkillIndexByName("Lifebane Strike").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1068: return Data[" + SkillDatabase.GetSkillIndexByName("Bitter Chill").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1069: return Data[" + SkillDatabase.GetSkillIndexByName("Taste of Pain").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1070: return Data[" + SkillDatabase.GetSkillIndexByName("Defile Enchantments").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1071: return Data[" + SkillDatabase.GetSkillIndexByName("Shivers of Dread").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1075: return Data[" + SkillDatabase.GetSkillIndexByName("Vampiric Swarm").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1076: return Data[" + SkillDatabase.GetSkillIndexByName("Blood Drinker").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1077: return Data[" + SkillDatabase.GetSkillIndexByName("Vampiric Bite").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1078: return Data[" + SkillDatabase.GetSkillIndexByName("Wallow's Bite").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1079: return Data[" + SkillDatabase.GetSkillIndexByName("Enfeebling Touch").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1081: return Data[" + SkillDatabase.GetSkillIndexByName("Teinai's Wind").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1082: return Data[" + SkillDatabase.GetSkillIndexByName("Shock Arrow").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1083: return Data[" + SkillDatabase.GetSkillIndexByName("Unsteady Ground").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1084: return Data[" + SkillDatabase.GetSkillIndexByName("Sliver Armor").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1085: return Data[" + SkillDatabase.GetSkillIndexByName("Ash Blast").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1086: return Data[" + SkillDatabase.GetSkillIndexByName("Dragon's Stomp").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1088: return Data[" + SkillDatabase.GetSkillIndexByName("Second Wind").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1090: return Data[" + SkillDatabase.GetSkillIndexByName("Smoldering Embers").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1091: return Data[" + SkillDatabase.GetSkillIndexByName("Double Dragon").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1093: return Data[" + SkillDatabase.GetSkillIndexByName("Teinai's Heat").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1094: return Data[" + SkillDatabase.GetSkillIndexByName("Breath of Fire").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1095: return Data[" + SkillDatabase.GetSkillIndexByName("Star Burst").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1096: return Data[" + SkillDatabase.GetSkillIndexByName("Glyph of Essence").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1097: return Data[" + SkillDatabase.GetSkillIndexByName("Teinai's Prison").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1098: return Data[" + SkillDatabase.GetSkillIndexByName("Mirror of Ice").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1099: return Data[" + SkillDatabase.GetSkillIndexByName("Teinai's Crystals").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1113: return Data[" + SkillDatabase.GetSkillIndexByName("Kirin's Wrath").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1114: return Data[" + SkillDatabase.GetSkillIndexByName("Spirit Bond").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1115: return Data[" + SkillDatabase.GetSkillIndexByName("Air of Enchantment").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1117: return Data[" + SkillDatabase.GetSkillIndexByName("Heaven's Delight").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1118: return Data[" + SkillDatabase.GetSkillIndexByName("Healing Burst").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1119: return Data[" + SkillDatabase.GetSkillIndexByName("Karei's Healing Circle").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1120: return Data[" + SkillDatabase.GetSkillIndexByName("Jamei's Gaze").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1121: return Data[" + SkillDatabase.GetSkillIndexByName("Gift of Health").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1123: return Data[" + SkillDatabase.GetSkillIndexByName("Life Sheath").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1126: return Data[" + SkillDatabase.GetSkillIndexByName("Empathic Removal").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1128: return Data[" + SkillDatabase.GetSkillIndexByName("Resurrection Chant").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1129: return Data[" + SkillDatabase.GetSkillIndexByName("Word of Censure").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1130: return Data[" + SkillDatabase.GetSkillIndexByName("Spear of Light").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1131: return Data[" + SkillDatabase.GetSkillIndexByName("Stonesoul Strike").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1133: return Data[" + SkillDatabase.GetSkillIndexByName("Drunken Blow").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1134: return Data[" + SkillDatabase.GetSkillIndexByName("Leviathan's Sweep").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1135: return Data[" + SkillDatabase.GetSkillIndexByName("Jaizhenju Strike").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1136: return Data[" + SkillDatabase.GetSkillIndexByName("Penetrating Chop").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1137: return Data[" + SkillDatabase.GetSkillIndexByName("Yeti Smash").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1141: return Data[" + SkillDatabase.GetSkillIndexByName("\"You Will Die!\"").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1142: return Data[" + SkillDatabase.GetSkillIndexByName("Auspicious Parry").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1144: return Data[" + SkillDatabase.GetSkillIndexByName("Silverwing Slash").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1146: return Data[" + SkillDatabase.GetSkillIndexByName("Shove").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1191: return Data[" + SkillDatabase.GetSkillIndexByName("Sundering Attack").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1192: return Data[" + SkillDatabase.GetSkillIndexByName("Zojun's Shot").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1194: return Data[" + SkillDatabase.GetSkillIndexByName("Predatory Bond").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1195: return Data[" + SkillDatabase.GetSkillIndexByName("Heal as One").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1196: return Data[" + SkillDatabase.GetSkillIndexByName("Zojun's Haste").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1197: return Data[" + SkillDatabase.GetSkillIndexByName("Needling Shot").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1198: return Data[" + SkillDatabase.GetSkillIndexByName("Broad Head Arrow").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1199: return Data[" + SkillDatabase.GetSkillIndexByName("Glass Arrows").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1200: return Data[" + SkillDatabase.GetSkillIndexByName("Archer's Signet").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1201: return Data[" + SkillDatabase.GetSkillIndexByName("Savage Pounce").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1202: return Data[" + SkillDatabase.GetSkillIndexByName("Enraged Lunge").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1203: return Data[" + SkillDatabase.GetSkillIndexByName("Bestial Mauling").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1205: return Data[" + SkillDatabase.GetSkillIndexByName("Poisonous Bite").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1206: return Data[" + SkillDatabase.GetSkillIndexByName("Pounce").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1209: return Data[" + SkillDatabase.GetSkillIndexByName("Bestial Fury").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1211: return Data[" + SkillDatabase.GetSkillIndexByName("Viper's Nest").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1212: return Data[" + SkillDatabase.GetSkillIndexByName("Equinox").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1213: return Data[" + SkillDatabase.GetSkillIndexByName("Tranquility").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1215: return Data[" + SkillDatabase.GetSkillIndexByName("Clamor of Souls").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1217: return Data[" + SkillDatabase.GetSkillIndexByName("Ritual Lord").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1218: return Data[" + SkillDatabase.GetSkillIndexByName("Cruel Was Daoshen").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1219: return Data[" + SkillDatabase.GetSkillIndexByName("Protective Was Kaolai").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1220: return Data[" + SkillDatabase.GetSkillIndexByName("Attuned Was Songkai").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1221: return Data[" + SkillDatabase.GetSkillIndexByName("Resilient Was Xiko").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1222: return Data[" + SkillDatabase.GetSkillIndexByName("Lively Was Naomei").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1223: return Data[" + SkillDatabase.GetSkillIndexByName("Anguished Was Lingwah").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1224: return Data[" + SkillDatabase.GetSkillIndexByName("Draw Spirit").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1225: return Data[" + SkillDatabase.GetSkillIndexByName("Channeled Strike").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1226: return Data[" + SkillDatabase.GetSkillIndexByName("Spirit Boon Strike").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1227: return Data[" + SkillDatabase.GetSkillIndexByName("Essence Strike").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1228: return Data[" + SkillDatabase.GetSkillIndexByName("Spirit Siphon").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1229: return Data[" + SkillDatabase.GetSkillIndexByName("Explosive Growth").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1230: return Data[" + SkillDatabase.GetSkillIndexByName("Boon of Creation").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1231: return Data[" + SkillDatabase.GetSkillIndexByName("Spirit Channeling").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1232: return Data[" + SkillDatabase.GetSkillIndexByName("Armor of Unfeeling").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1233: return Data[" + SkillDatabase.GetSkillIndexByName("Soothing Memories").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1234: return Data[" + SkillDatabase.GetSkillIndexByName("Mend Body and Soul").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1235: return Data[" + SkillDatabase.GetSkillIndexByName("Dulled Weapon").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1236: return Data[" + SkillDatabase.GetSkillIndexByName("Binding Chains").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1237: return Data[" + SkillDatabase.GetSkillIndexByName("Painful Bond").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1238: return Data[" + SkillDatabase.GetSkillIndexByName("Signet of Creation").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1239: return Data[" + SkillDatabase.GetSkillIndexByName("Signet of Spirits").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1240: return Data[" + SkillDatabase.GetSkillIndexByName("Soul Twisting").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1244: return Data[" + SkillDatabase.GetSkillIndexByName("Ghostly Haste").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1245: return Data[" + SkillDatabase.GetSkillIndexByName("Gaze from Beyond").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1246: return Data[" + SkillDatabase.GetSkillIndexByName("Ancestors' Rage").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1247: return Data[" + SkillDatabase.GetSkillIndexByName("Pain").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1249: return Data[" + SkillDatabase.GetSkillIndexByName("Displacement").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1250: return Data[" + SkillDatabase.GetSkillIndexByName("Preservation").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1251: return Data[" + SkillDatabase.GetSkillIndexByName("Life").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1252: return Data[" + SkillDatabase.GetSkillIndexByName("Earthbind").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1253: return Data[" + SkillDatabase.GetSkillIndexByName("Bloodsong").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1255: return Data[" + SkillDatabase.GetSkillIndexByName("Wanderlust").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1257: return Data[" + SkillDatabase.GetSkillIndexByName("Spirit Light Weapon").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1258: return Data[" + SkillDatabase.GetSkillIndexByName("Brutal Weapon").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1259: return Data[" + SkillDatabase.GetSkillIndexByName("Guided Weapon").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1260: return Data[" + SkillDatabase.GetSkillIndexByName("Meekness").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1261: return Data[" + SkillDatabase.GetSkillIndexByName("Frigid Armor").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1262: return Data[" + SkillDatabase.GetSkillIndexByName("Healing Ring").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1263: return Data[" + SkillDatabase.GetSkillIndexByName("Renew Life").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1264: return Data[" + SkillDatabase.GetSkillIndexByName("Doom").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1265: return Data[" + SkillDatabase.GetSkillIndexByName("Wielder's Boon").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1266: return Data[" + SkillDatabase.GetSkillIndexByName("Soothing").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1267: return Data[" + SkillDatabase.GetSkillIndexByName("Vital Weapon").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1268: return Data[" + SkillDatabase.GetSkillIndexByName("Weapon of Quickening").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1269: return Data[" + SkillDatabase.GetSkillIndexByName("Signet of Rage").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1333: return Data[" + SkillDatabase.GetSkillIndexByName("Extend Conditions").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1334: return Data[" + SkillDatabase.GetSkillIndexByName("Hypochondria").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1335: return Data[" + SkillDatabase.GetSkillIndexByName("Wastrel's Demise").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1336: return Data[" + SkillDatabase.GetSkillIndexByName("Spiritual Pain").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1337: return Data[" + SkillDatabase.GetSkillIndexByName("Drain Delusions").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1338: return Data[" + SkillDatabase.GetSkillIndexByName("Persistence of Memory").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1339: return Data[" + SkillDatabase.GetSkillIndexByName("Symbols of Inspiration").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1340: return Data[" + SkillDatabase.GetSkillIndexByName("Symbolic Celerity").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1341: return Data[" + SkillDatabase.GetSkillIndexByName("Frustration").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1342: return Data[" + SkillDatabase.GetSkillIndexByName("Tease").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1343: return Data[" + SkillDatabase.GetSkillIndexByName("Ether Phantom").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1344: return Data[" + SkillDatabase.GetSkillIndexByName("Web of Disruption").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1345: return Data[" + SkillDatabase.GetSkillIndexByName("Enchanter's Conundrum").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1346: return Data[" + SkillDatabase.GetSkillIndexByName("Signet of Illusions").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1347: return Data[" + SkillDatabase.GetSkillIndexByName("Discharge Enchantment").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1348: return Data[" + SkillDatabase.GetSkillIndexByName("Hex Eater Vortex").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1349: return Data[" + SkillDatabase.GetSkillIndexByName("Mirror of Disenchantment").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1350: return Data[" + SkillDatabase.GetSkillIndexByName("Simple Thievery").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1351: return Data[" + SkillDatabase.GetSkillIndexByName("Animate Shambling Horror").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1352: return Data[" + SkillDatabase.GetSkillIndexByName("Order of Undeath").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1353: return Data[" + SkillDatabase.GetSkillIndexByName("Putrid Flesh").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1354: return Data[" + SkillDatabase.GetSkillIndexByName("Feast for the Dead").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1355: return Data[" + SkillDatabase.GetSkillIndexByName("Jagged Bones").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1356: return Data[" + SkillDatabase.GetSkillIndexByName("Contagion").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1358: return Data[" + SkillDatabase.GetSkillIndexByName("Ulcerous Lungs").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1359: return Data[" + SkillDatabase.GetSkillIndexByName("Pain of Disenchantment").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1360: return Data[" + SkillDatabase.GetSkillIndexByName("Mark of Fury").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1362: return Data[" + SkillDatabase.GetSkillIndexByName("Corrupt Enchantment").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1363: return Data[" + SkillDatabase.GetSkillIndexByName("Signet of Sorrow").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1364: return Data[" + SkillDatabase.GetSkillIndexByName("Signet of Suffering").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1365: return Data[" + SkillDatabase.GetSkillIndexByName("Signet of Lost Souls").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1366: return Data[" + SkillDatabase.GetSkillIndexByName("Well of Darkness").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1367: return Data[" + SkillDatabase.GetSkillIndexByName("Blinding Surge").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1368: return Data[" + SkillDatabase.GetSkillIndexByName("Chilling Winds").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1369: return Data[" + SkillDatabase.GetSkillIndexByName("Lightning Bolt").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1370: return Data[" + SkillDatabase.GetSkillIndexByName("Storm Djinn's Haste").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1371: return Data[" + SkillDatabase.GetSkillIndexByName("Stone Striker").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1372: return Data[" + SkillDatabase.GetSkillIndexByName("Sandstorm").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1373: return Data[" + SkillDatabase.GetSkillIndexByName("Stone Sheath").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1374: return Data[" + SkillDatabase.GetSkillIndexByName("Ebon Hawk").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1375: return Data[" + SkillDatabase.GetSkillIndexByName("Stoneflesh Aura").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1376: return Data[" + SkillDatabase.GetSkillIndexByName("Glyph of Restoration").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1377: return Data[" + SkillDatabase.GetSkillIndexByName("Ether Prism").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1378: return Data[" + SkillDatabase.GetSkillIndexByName("Master of Magic").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1379: return Data[" + SkillDatabase.GetSkillIndexByName("Glowing Gaze").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1380: return Data[" + SkillDatabase.GetSkillIndexByName("Savannah Heat").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1381: return Data[" + SkillDatabase.GetSkillIndexByName("Flame Djinn's Haste").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1382: return Data[" + SkillDatabase.GetSkillIndexByName("Freezing Gust").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1390: return Data[" + SkillDatabase.GetSkillIndexByName("Judge's Intervention").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1391: return Data[" + SkillDatabase.GetSkillIndexByName("Supportive Spirit").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1392: return Data[" + SkillDatabase.GetSkillIndexByName("Watchful Healing").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1393: return Data[" + SkillDatabase.GetSkillIndexByName("Healer's Boon").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1394: return Data[" + SkillDatabase.GetSkillIndexByName("Healer's Covenant").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1395: return Data[" + SkillDatabase.GetSkillIndexByName("Balthazar's Pendulum").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1396: return Data[" + SkillDatabase.GetSkillIndexByName("Words of Comfort").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1397: return Data[" + SkillDatabase.GetSkillIndexByName("Light of Deliverance").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1398: return Data[" + SkillDatabase.GetSkillIndexByName("Scourge Enchantment").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1399: return Data[" + SkillDatabase.GetSkillIndexByName("Shield of Absorption").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1400: return Data[" + SkillDatabase.GetSkillIndexByName("Reversal of Damage").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1401: return Data[" + SkillDatabase.GetSkillIndexByName("Mending Touch").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1402: return Data[" + SkillDatabase.GetSkillIndexByName("Critical Chop").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1403: return Data[" + SkillDatabase.GetSkillIndexByName("Agonizing Chop").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1404: return Data[" + SkillDatabase.GetSkillIndexByName("Flail").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1405: return Data[" + SkillDatabase.GetSkillIndexByName("Charging Strike").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1406: return Data[" + SkillDatabase.GetSkillIndexByName("Headbutt").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1407: return Data[" + SkillDatabase.GetSkillIndexByName("Lion's Comfort").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1408: return Data[" + SkillDatabase.GetSkillIndexByName("Rage of the Ntouka").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1409: return Data[" + SkillDatabase.GetSkillIndexByName("Mokele Smash").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1410: return Data[" + SkillDatabase.GetSkillIndexByName("Overbearing Smash").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1411: return Data[" + SkillDatabase.GetSkillIndexByName("Signet of Stamina").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1412: return Data[" + SkillDatabase.GetSkillIndexByName("\"You're All Alone!\"").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1413: return Data[" + SkillDatabase.GetSkillIndexByName("Burst of Aggression").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1414: return Data[" + SkillDatabase.GetSkillIndexByName("Enraging Charge").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1415: return Data[" + SkillDatabase.GetSkillIndexByName("Crippling Slash").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1416: return Data[" + SkillDatabase.GetSkillIndexByName("Barbarous Slice").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1465: return Data[" + SkillDatabase.GetSkillIndexByName("Prepared Shot").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1466: return Data[" + SkillDatabase.GetSkillIndexByName("Burning Arrow").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1467: return Data[" + SkillDatabase.GetSkillIndexByName("Arcing Shot").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1468: return Data[" + SkillDatabase.GetSkillIndexByName("Strike as One").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1469: return Data[" + SkillDatabase.GetSkillIndexByName("Crossfire").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1470: return Data[" + SkillDatabase.GetSkillIndexByName("Barbed Arrows").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1471: return Data[" + SkillDatabase.GetSkillIndexByName("Scavenger's Focus").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1472: return Data[" + SkillDatabase.GetSkillIndexByName("Toxicity").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1473: return Data[" + SkillDatabase.GetSkillIndexByName("Quicksand").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1474: return Data[" + SkillDatabase.GetSkillIndexByName("Storm's Embrace").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1475: return Data[" + SkillDatabase.GetSkillIndexByName("Trapper's Speed").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1476: return Data[" + SkillDatabase.GetSkillIndexByName("Tripwire").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1478: return Data[" + SkillDatabase.GetSkillIndexByName("Renewing Surge").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1479: return Data[" + SkillDatabase.GetSkillIndexByName("Offering of Spirit").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1480: return Data[" + SkillDatabase.GetSkillIndexByName("Spirit's Gift").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1481: return Data[" + SkillDatabase.GetSkillIndexByName("Death Pact Signet").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1482: return Data[" + SkillDatabase.GetSkillIndexByName("Reclaim Essence").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1483: return Data[" + SkillDatabase.GetSkillIndexByName("Banishing Strike").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1484: return Data[" + SkillDatabase.GetSkillIndexByName("Mystic Sweep").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1485: return Data[" + SkillDatabase.GetSkillIndexByName("Eremite's Attack").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1486: return Data[" + SkillDatabase.GetSkillIndexByName("Reap Impurities").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1487: return Data[" + SkillDatabase.GetSkillIndexByName("Twin Moon Sweep").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1488: return Data[" + SkillDatabase.GetSkillIndexByName("Victorious Sweep").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1489: return Data[" + SkillDatabase.GetSkillIndexByName("Irresistible Sweep").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1490: return Data[" + SkillDatabase.GetSkillIndexByName("Pious Assault").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1491: return Data[" + SkillDatabase.GetSkillIndexByName("Mystic Twister").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1493: return Data[" + SkillDatabase.GetSkillIndexByName("Grenth's Fingers").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1495: return Data[" + SkillDatabase.GetSkillIndexByName("Aura of Thorns").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1496: return Data[" + SkillDatabase.GetSkillIndexByName("Balthazar's Rage").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1497: return Data[" + SkillDatabase.GetSkillIndexByName("Dust Cloak").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1498: return Data[" + SkillDatabase.GetSkillIndexByName("Staggering Force").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1499: return Data[" + SkillDatabase.GetSkillIndexByName("Pious Renewal").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1500: return Data[" + SkillDatabase.GetSkillIndexByName("Mirage Cloak").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1502: return Data[" + SkillDatabase.GetSkillIndexByName("Arcane Zeal").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1503: return Data[" + SkillDatabase.GetSkillIndexByName("Mystic Vigor").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1504: return Data[" + SkillDatabase.GetSkillIndexByName("Watchful Intervention").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1505: return Data[" + SkillDatabase.GetSkillIndexByName("Vow of Piety").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1506: return Data[" + SkillDatabase.GetSkillIndexByName("Vital Boon").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1507: return Data[" + SkillDatabase.GetSkillIndexByName("Heart of Holy Flame").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1508: return Data[" + SkillDatabase.GetSkillIndexByName("Extend Enchantments").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1509: return Data[" + SkillDatabase.GetSkillIndexByName("Faithful Intervention").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1510: return Data[" + SkillDatabase.GetSkillIndexByName("Sand Shards").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1512: return Data[" + SkillDatabase.GetSkillIndexByName("Lyssa's Haste").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1513: return Data[" + SkillDatabase.GetSkillIndexByName("Guiding Hands").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1514: return Data[" + SkillDatabase.GetSkillIndexByName("Fleeting Stability").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1515: return Data[" + SkillDatabase.GetSkillIndexByName("Armor of Sanctity").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1516: return Data[" + SkillDatabase.GetSkillIndexByName("Mystic Regeneration").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1517: return Data[" + SkillDatabase.GetSkillIndexByName("Vow of Silence").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1518: return Data[" + SkillDatabase.GetSkillIndexByName("Avatar of Balthazar").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1519: return Data[" + SkillDatabase.GetSkillIndexByName("Avatar of Dwayna").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1520: return Data[" + SkillDatabase.GetSkillIndexByName("Avatar of Grenth").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1521: return Data[" + SkillDatabase.GetSkillIndexByName("Avatar of Lyssa").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1522: return Data[" + SkillDatabase.GetSkillIndexByName("Avatar of Melandru").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1523: return Data[" + SkillDatabase.GetSkillIndexByName("Meditation").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1524: return Data[" + SkillDatabase.GetSkillIndexByName("Eremite's Zeal").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1525: return Data[" + SkillDatabase.GetSkillIndexByName("Natural Healing").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1526: return Data[" + SkillDatabase.GetSkillIndexByName("Imbue Health").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1527: return Data[" + SkillDatabase.GetSkillIndexByName("Mystic Healing").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1528: return Data[" + SkillDatabase.GetSkillIndexByName("Dwayna's Touch").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1529: return Data[" + SkillDatabase.GetSkillIndexByName("Pious Restoration").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1530: return Data[" + SkillDatabase.GetSkillIndexByName("Signet of Pious Light").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1531: return Data[" + SkillDatabase.GetSkillIndexByName("Intimidating Aura").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1532: return Data[" + SkillDatabase.GetSkillIndexByName("Mystic Sandstorm").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1533: return Data[" + SkillDatabase.GetSkillIndexByName("Winds of Disenchantment").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1534: return Data[" + SkillDatabase.GetSkillIndexByName("Rending Touch").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1535: return Data[" + SkillDatabase.GetSkillIndexByName("Crippling Sweep").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1536: return Data[" + SkillDatabase.GetSkillIndexByName("Wounding Strike").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1537: return Data[" + SkillDatabase.GetSkillIndexByName("Wearying Strike").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1538: return Data[" + SkillDatabase.GetSkillIndexByName("Lyssa's Assault").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1539: return Data[" + SkillDatabase.GetSkillIndexByName("Chilling Victory").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1540: return Data[" + SkillDatabase.GetSkillIndexByName("Conviction").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1541: return Data[" + SkillDatabase.GetSkillIndexByName("Enchanted Haste").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1542: return Data[" + SkillDatabase.GetSkillIndexByName("Pious Concentration").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1543: return Data[" + SkillDatabase.GetSkillIndexByName("Pious Haste").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1544: return Data[" + SkillDatabase.GetSkillIndexByName("Whirling Charge").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1545: return Data[" + SkillDatabase.GetSkillIndexByName("Test of Faith").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1546: return Data[" + SkillDatabase.GetSkillIndexByName("Blazing Spear").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1547: return Data[" + SkillDatabase.GetSkillIndexByName("Mighty Throw").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1548: return Data[" + SkillDatabase.GetSkillIndexByName("Cruel Spear").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1549: return Data[" + SkillDatabase.GetSkillIndexByName("Harrier's Toss").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1550: return Data[" + SkillDatabase.GetSkillIndexByName("Unblockable Throw").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1551: return Data[" + SkillDatabase.GetSkillIndexByName("Spear of Lightning").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1552: return Data[" + SkillDatabase.GetSkillIndexByName("Wearying Spear").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1553: return Data[" + SkillDatabase.GetSkillIndexByName("Anthem of Fury").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1554: return Data[" + SkillDatabase.GetSkillIndexByName("Crippling Anthem").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1555: return Data[" + SkillDatabase.GetSkillIndexByName("Defensive Anthem").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1556: return Data[" + SkillDatabase.GetSkillIndexByName("Godspeed").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1557: return Data[" + SkillDatabase.GetSkillIndexByName("Anthem of Flame").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1558: return Data[" + SkillDatabase.GetSkillIndexByName("\"Go for the Eyes!\"").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1559: return Data[" + SkillDatabase.GetSkillIndexByName("Anthem of Envy").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1560: return Data[" + SkillDatabase.GetSkillIndexByName("Song of Power").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1561: return Data[" + SkillDatabase.GetSkillIndexByName("Zealous Anthem").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1562: return Data[" + SkillDatabase.GetSkillIndexByName("Aria of Zeal").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1563: return Data[" + SkillDatabase.GetSkillIndexByName("Lyric of Zeal").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1564: return Data[" + SkillDatabase.GetSkillIndexByName("Ballad of Restoration").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1565: return Data[" + SkillDatabase.GetSkillIndexByName("Chorus of Restoration").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1566: return Data[" + SkillDatabase.GetSkillIndexByName("Aria of Restoration").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1567: return Data[" + SkillDatabase.GetSkillIndexByName("Song of Concentration").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1568: return Data[" + SkillDatabase.GetSkillIndexByName("Anthem of Guidance").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1569: return Data[" + SkillDatabase.GetSkillIndexByName("Energizing Chorus").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1570: return Data[" + SkillDatabase.GetSkillIndexByName("Song of Purification").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1571: return Data[" + SkillDatabase.GetSkillIndexByName("Hexbreaker Aria").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1572: return Data[" + SkillDatabase.GetSkillIndexByName("\"Brace Yourself!\"").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1573: return Data[" + SkillDatabase.GetSkillIndexByName("Awe").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1574: return Data[" + SkillDatabase.GetSkillIndexByName("Enduring Harmony").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1575: return Data[" + SkillDatabase.GetSkillIndexByName("Blazing Finale").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1576: return Data[" + SkillDatabase.GetSkillIndexByName("Burning Refrain").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1577: return Data[" + SkillDatabase.GetSkillIndexByName("Finale of Restoration").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1578: return Data[" + SkillDatabase.GetSkillIndexByName("Mending Refrain").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1579: return Data[" + SkillDatabase.GetSkillIndexByName("Purifying Finale").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1580: return Data[" + SkillDatabase.GetSkillIndexByName("Bladeturn Refrain").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1581: return Data[" + SkillDatabase.GetSkillIndexByName("Glowing Signet").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1583: return Data[" + SkillDatabase.GetSkillIndexByName("Leader's Zeal").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1584: return Data[" + SkillDatabase.GetSkillIndexByName("Leader's Comfort").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1585: return Data[" + SkillDatabase.GetSkillIndexByName("Signet of Synergy").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1586: return Data[" + SkillDatabase.GetSkillIndexByName("Angelic Protection").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1587: return Data[" + SkillDatabase.GetSkillIndexByName("Angelic Bond").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1588: return Data[" + SkillDatabase.GetSkillIndexByName("Cautery Signet").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1589: return Data[" + SkillDatabase.GetSkillIndexByName("\"Stand Your Ground!\"").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1590: return Data[" + SkillDatabase.GetSkillIndexByName("\"Lead the Way!\"").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1591: return Data[" + SkillDatabase.GetSkillIndexByName("\"Make Haste!\"").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1592: return Data[" + SkillDatabase.GetSkillIndexByName("\"We Shall Return!\"").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1593: return Data[" + SkillDatabase.GetSkillIndexByName("\"Never Give Up!\"").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1594: return Data[" + SkillDatabase.GetSkillIndexByName("\"Help Me!\"").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1595: return Data[" + SkillDatabase.GetSkillIndexByName("\"Fall Back!\"").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1596: return Data[" + SkillDatabase.GetSkillIndexByName("\"Incoming!\"").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1597: return Data[" + SkillDatabase.GetSkillIndexByName("\"They're on Fire!\"").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1598: return Data[" + SkillDatabase.GetSkillIndexByName("\"Never Surrender!\"").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1599: return Data[" + SkillDatabase.GetSkillIndexByName("\"It's just a flesh wound.\"").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1600: return Data[" + SkillDatabase.GetSkillIndexByName("Barbed Spear").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1601: return Data[" + SkillDatabase.GetSkillIndexByName("Vicious Attack").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1602: return Data[" + SkillDatabase.GetSkillIndexByName("Stunning Strike").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1603: return Data[" + SkillDatabase.GetSkillIndexByName("Merciless Spear").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1604: return Data[" + SkillDatabase.GetSkillIndexByName("Disrupting Throw").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1605: return Data[" + SkillDatabase.GetSkillIndexByName("Wild Throw").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1633: return Data[" + SkillDatabase.GetSkillIndexByName("Malicious Strike").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1634: return Data[" + SkillDatabase.GetSkillIndexByName("Shattering Assault").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1635: return Data[" + SkillDatabase.GetSkillIndexByName("Golden Skull Strike").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1636: return Data[" + SkillDatabase.GetSkillIndexByName("Black Spider Strike").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1637: return Data[" + SkillDatabase.GetSkillIndexByName("Golden Fox Strike").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1638: return Data[" + SkillDatabase.GetSkillIndexByName("Deadly Haste").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1639: return Data[" + SkillDatabase.GetSkillIndexByName("Assassin's Remedy").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1640: return Data[" + SkillDatabase.GetSkillIndexByName("Fox's Promise").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1641: return Data[" + SkillDatabase.GetSkillIndexByName("Feigned Neutrality").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1642: return Data[" + SkillDatabase.GetSkillIndexByName("Hidden Caltrops").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1643: return Data[" + SkillDatabase.GetSkillIndexByName("Assault Enchantments").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1644: return Data[" + SkillDatabase.GetSkillIndexByName("Wastrel's Collapse").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1645: return Data[" + SkillDatabase.GetSkillIndexByName("Lift Enchantment").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1646: return Data[" + SkillDatabase.GetSkillIndexByName("Augury of Death").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1647: return Data[" + SkillDatabase.GetSkillIndexByName("Signet of Toxic Shock").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1648: return Data[" + SkillDatabase.GetSkillIndexByName("Signet of Twilight").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1649: return Data[" + SkillDatabase.GetSkillIndexByName("Way of the Assassin").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1650: return Data[" + SkillDatabase.GetSkillIndexByName("Shadow Walk").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1651: return Data[" + SkillDatabase.GetSkillIndexByName("Death's Retreat").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1652: return Data[" + SkillDatabase.GetSkillIndexByName("Shadow Prison").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1653: return Data[" + SkillDatabase.GetSkillIndexByName("Swap").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1654: return Data[" + SkillDatabase.GetSkillIndexByName("Shadow Meld").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1655: return Data[" + SkillDatabase.GetSkillIndexByName("Price of Pride").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1656: return Data[" + SkillDatabase.GetSkillIndexByName("Air of Disenchantment").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1657: return Data[" + SkillDatabase.GetSkillIndexByName("Signet of Clumsiness").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1658: return Data[" + SkillDatabase.GetSkillIndexByName("Symbolic Posture").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1659: return Data[" + SkillDatabase.GetSkillIndexByName("Toxic Chill").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1660: return Data[" + SkillDatabase.GetSkillIndexByName("Well of Silence").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1661: return Data[" + SkillDatabase.GetSkillIndexByName("Glowstone").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1662: return Data[" + SkillDatabase.GetSkillIndexByName("Mind Blast").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1663: return Data[" + SkillDatabase.GetSkillIndexByName("Elemental Flame").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1664: return Data[" + SkillDatabase.GetSkillIndexByName("Invoke Lightning").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1683: return Data[" + SkillDatabase.GetSkillIndexByName("Pensive Guardian").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1684: return Data[" + SkillDatabase.GetSkillIndexByName("Scribe's Insight").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1685: return Data[" + SkillDatabase.GetSkillIndexByName("Holy Haste").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1686: return Data[" + SkillDatabase.GetSkillIndexByName("Glimmer of Light").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1687: return Data[" + SkillDatabase.GetSkillIndexByName("Zealous Benediction").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1688: return Data[" + SkillDatabase.GetSkillIndexByName("Defender's Zeal").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1689: return Data[" + SkillDatabase.GetSkillIndexByName("Signet of Mystic Wrath").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1690: return Data[" + SkillDatabase.GetSkillIndexByName("Signet of Removal").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1691: return Data[" + SkillDatabase.GetSkillIndexByName("Dismiss Condition").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1692: return Data[" + SkillDatabase.GetSkillIndexByName("Divert Hexes").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1693: return Data[" + SkillDatabase.GetSkillIndexByName("Counterattack").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1694: return Data[" + SkillDatabase.GetSkillIndexByName("Magehunter Strike").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1695: return Data[" + SkillDatabase.GetSkillIndexByName("Soldier's Strike").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1696: return Data[" + SkillDatabase.GetSkillIndexByName("Decapitate").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1697: return Data[" + SkillDatabase.GetSkillIndexByName("Magehunter's Smash").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1698: return Data[" + SkillDatabase.GetSkillIndexByName("Soldier's Stance").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1699: return Data[" + SkillDatabase.GetSkillIndexByName("Soldier's Defense").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1700: return Data[" + SkillDatabase.GetSkillIndexByName("Frenzied Defense").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1701: return Data[" + SkillDatabase.GetSkillIndexByName("Steady Stance").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1702: return Data[" + SkillDatabase.GetSkillIndexByName("Steelfang Slash").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1719: return Data[" + SkillDatabase.GetSkillIndexByName("Screaming Shot").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1720: return Data[" + SkillDatabase.GetSkillIndexByName("Keen Arrow").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1721: return Data[" + SkillDatabase.GetSkillIndexByName("Rampage as One").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1722: return Data[" + SkillDatabase.GetSkillIndexByName("Forked Arrow").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1723: return Data[" + SkillDatabase.GetSkillIndexByName("Disrupting Accuracy").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1724: return Data[" + SkillDatabase.GetSkillIndexByName("Expert's Dexterity").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1725: return Data[" + SkillDatabase.GetSkillIndexByName("Roaring Winds").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1726: return Data[" + SkillDatabase.GetSkillIndexByName("Magebane Shot").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1727: return Data[" + SkillDatabase.GetSkillIndexByName("Natural Stride").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1728: return Data[" + SkillDatabase.GetSkillIndexByName("Heket's Rampage").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1729: return Data[" + SkillDatabase.GetSkillIndexByName("Smoke Trap").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1730: return Data[" + SkillDatabase.GetSkillIndexByName("Infuriating Heat").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1731: return Data[" + SkillDatabase.GetSkillIndexByName("Vocal Was Sogolon").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1732: return Data[" + SkillDatabase.GetSkillIndexByName("Destructive Was Glaive").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1733: return Data[" + SkillDatabase.GetSkillIndexByName("Wielder's Strike").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1734: return Data[" + SkillDatabase.GetSkillIndexByName("Gaze of Fury").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1736: return Data[" + SkillDatabase.GetSkillIndexByName("Spirit's Strength").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1737: return Data[" + SkillDatabase.GetSkillIndexByName("Wielder's Zeal").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1738: return Data[" + SkillDatabase.GetSkillIndexByName("Sight Beyond Sight").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1739: return Data[" + SkillDatabase.GetSkillIndexByName("Renewing Memories").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1740: return Data[" + SkillDatabase.GetSkillIndexByName("Wielder's Remedy").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1741: return Data[" + SkillDatabase.GetSkillIndexByName("Ghostmirror Light").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1742: return Data[" + SkillDatabase.GetSkillIndexByName("Signet of Ghostly Might").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1743: return Data[" + SkillDatabase.GetSkillIndexByName("Signet of Binding").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1744: return Data[" + SkillDatabase.GetSkillIndexByName("Caretaker's Charge").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1745: return Data[" + SkillDatabase.GetSkillIndexByName("Anguish").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1747: return Data[" + SkillDatabase.GetSkillIndexByName("Empowerment").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1748: return Data[" + SkillDatabase.GetSkillIndexByName("Recovery").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1749: return Data[" + SkillDatabase.GetSkillIndexByName("Weapon of Fury").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1750: return Data[" + SkillDatabase.GetSkillIndexByName("Xinrae's Weapon").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1751: return Data[" + SkillDatabase.GetSkillIndexByName("Warmonger's Weapon").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1752: return Data[" + SkillDatabase.GetSkillIndexByName("Weapon of Remedy").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1753: return Data[" + SkillDatabase.GetSkillIndexByName("Rending Sweep").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1754: return Data[" + SkillDatabase.GetSkillIndexByName("Onslaught").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1755: return Data[" + SkillDatabase.GetSkillIndexByName("Mystic Corruption").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1756: return Data[" + SkillDatabase.GetSkillIndexByName("Grenth's Grasp").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1757: return Data[" + SkillDatabase.GetSkillIndexByName("Veil of Thorns").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1758: return Data[" + SkillDatabase.GetSkillIndexByName("Harrier's Grasp").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1759: return Data[" + SkillDatabase.GetSkillIndexByName("Vow of Strength").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1760: return Data[" + SkillDatabase.GetSkillIndexByName("Ebon Dust Aura").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1761: return Data[" + SkillDatabase.GetSkillIndexByName("Zealous Vow").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1762: return Data[" + SkillDatabase.GetSkillIndexByName("Heart of Fury").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1763: return Data[" + SkillDatabase.GetSkillIndexByName("Zealous Renewal").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1764: return Data[" + SkillDatabase.GetSkillIndexByName("Attacker's Insight").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1765: return Data[" + SkillDatabase.GetSkillIndexByName("Rending Aura").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1766: return Data[" + SkillDatabase.GetSkillIndexByName("Featherfoot Grace").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1767: return Data[" + SkillDatabase.GetSkillIndexByName("Reaper's Sweep").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1768: return Data[" + SkillDatabase.GetSkillIndexByName("Harrier's Haste").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1769: return Data[" + SkillDatabase.GetSkillIndexByName("Focused Anger").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1770: return Data[" + SkillDatabase.GetSkillIndexByName("Natural Temper").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1771: return Data[" + SkillDatabase.GetSkillIndexByName("Song of Restoration").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1772: return Data[" + SkillDatabase.GetSkillIndexByName("Lyric of Purification").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1773: return Data[" + SkillDatabase.GetSkillIndexByName("Soldier's Fury").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1774: return Data[" + SkillDatabase.GetSkillIndexByName("Aggressive Refrain").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1775: return Data[" + SkillDatabase.GetSkillIndexByName("Energizing Finale").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1776: return Data[" + SkillDatabase.GetSkillIndexByName("Signet of Aggression").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1777: return Data[" + SkillDatabase.GetSkillIndexByName("Remedy Signet").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1778: return Data[" + SkillDatabase.GetSkillIndexByName("Signet of Return").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1779: return Data[" + SkillDatabase.GetSkillIndexByName("\"Make Your Time!\"").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1780: return Data[" + SkillDatabase.GetSkillIndexByName("\"Can't Touch This!\"").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1781: return Data[" + SkillDatabase.GetSkillIndexByName("\"Find Their Weakness!\"").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1782: return Data[" + SkillDatabase.GetSkillIndexByName("\"The Power Is Yours!\"").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1783: return Data[" + SkillDatabase.GetSkillIndexByName("Slayer's Spear").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1784: return Data[" + SkillDatabase.GetSkillIndexByName("Swift Javelin").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1814: return Data[" + SkillDatabase.GetSkillIndexByName("Lightbringer's Gaze").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1815: return Data[" + SkillDatabase.GetSkillIndexByName("Lightbringer Signet").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1816: return Data[" + SkillDatabase.GetSkillIndexByName("Sunspear Rebirth Signet").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1948: return Data[" + SkillDatabase.GetSkillIndexByName("Shadow Sanctuary (Luxon)").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1949: return Data[" + SkillDatabase.GetSkillIndexByName("Ether Nightmare (Luxon)").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1950: return Data[" + SkillDatabase.GetSkillIndexByName("Signet of Corruption (Luxon)").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1951: return Data[" + SkillDatabase.GetSkillIndexByName("Elemental Lord (Luxon)").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1952: return Data[" + SkillDatabase.GetSkillIndexByName("Selfless Spirit (Luxon)").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1953: return Data[" + SkillDatabase.GetSkillIndexByName("Triple Shot (Luxon)").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1954: return Data[" + SkillDatabase.GetSkillIndexByName("\"Save Yourselves!\" (Luxon)").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1955: return Data[" + SkillDatabase.GetSkillIndexByName("Aura of Holy Might (Luxon)").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1957: return Data[" + SkillDatabase.GetSkillIndexByName("Spear of Fury (Luxon)").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1986: return Data[" + SkillDatabase.GetSkillIndexByName("Vampiric Assault").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1987: return Data[" + SkillDatabase.GetSkillIndexByName("Lotus Strike").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1988: return Data[" + SkillDatabase.GetSkillIndexByName("Golden Fang Strike").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1990: return Data[" + SkillDatabase.GetSkillIndexByName("Falling Lotus Strike").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1991: return Data[" + SkillDatabase.GetSkillIndexByName("Sadist's Signet").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1992: return Data[" + SkillDatabase.GetSkillIndexByName("Signet of Distraction").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1993: return Data[" + SkillDatabase.GetSkillIndexByName("Signet of Recall").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1994: return Data[" + SkillDatabase.GetSkillIndexByName("Power Lock").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1995: return Data[" + SkillDatabase.GetSkillIndexByName("Waste Not, Want Not").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1996: return Data[" + SkillDatabase.GetSkillIndexByName("Sum of All Fears").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1997: return Data[" + SkillDatabase.GetSkillIndexByName("Withering Aura").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1998: return Data[" + SkillDatabase.GetSkillIndexByName("Cacophony").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 1999: return Data[" + SkillDatabase.GetSkillIndexByName("Winter's Embrace").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2000: return Data[" + SkillDatabase.GetSkillIndexByName("Earthen Shackles").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2001: return Data[" + SkillDatabase.GetSkillIndexByName("Ward of Weakness").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2002: return Data[" + SkillDatabase.GetSkillIndexByName("Glyph of Swiftness").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2003: return Data[" + SkillDatabase.GetSkillIndexByName("Cure Hex").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2004: return Data[" + SkillDatabase.GetSkillIndexByName("Smite Condition").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2005: return Data[" + SkillDatabase.GetSkillIndexByName("Smiter's Boon").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2006: return Data[" + SkillDatabase.GetSkillIndexByName("Castigation Signet").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2007: return Data[" + SkillDatabase.GetSkillIndexByName("Purifying Veil").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2008: return Data[" + SkillDatabase.GetSkillIndexByName("Pulverizing Smash").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2009: return Data[" + SkillDatabase.GetSkillIndexByName("Keen Chop").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2010: return Data[" + SkillDatabase.GetSkillIndexByName("Knee Cutter").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2011: return Data[" + SkillDatabase.GetSkillIndexByName("Grapple").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2012: return Data[" + SkillDatabase.GetSkillIndexByName("Radiant Scythe").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2013: return Data[" + SkillDatabase.GetSkillIndexByName("Grenth's Aura").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2014: return Data[" + SkillDatabase.GetSkillIndexByName("Signet of Pious Restraint").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2015: return Data[" + SkillDatabase.GetSkillIndexByName("Farmer's Scythe").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2016: return Data[" + SkillDatabase.GetSkillIndexByName("Energetic Was Lee Sa").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2017: return Data[" + SkillDatabase.GetSkillIndexByName("Anthem of Weariness").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2018: return Data[" + SkillDatabase.GetSkillIndexByName("Anthem of Disruption").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2051: return Data[" + SkillDatabase.GetSkillIndexByName("Summon Spirits (Luxon)").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2052: return Data[" + SkillDatabase.GetSkillIndexByName("Shadow Fang").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2053: return Data[" + SkillDatabase.GetSkillIndexByName("Calculated Risk").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2054: return Data[" + SkillDatabase.GetSkillIndexByName("Shrinking Armor").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2055: return Data[" + SkillDatabase.GetSkillIndexByName("Aneurysm").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2056: return Data[" + SkillDatabase.GetSkillIndexByName("Wandering Eye").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2057: return Data[" + SkillDatabase.GetSkillIndexByName("Foul Feast").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2058: return Data[" + SkillDatabase.GetSkillIndexByName("Putrid Bile").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2059: return Data[" + SkillDatabase.GetSkillIndexByName("Shell Shock").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2060: return Data[" + SkillDatabase.GetSkillIndexByName("Glyph of Immolation").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2061: return Data[" + SkillDatabase.GetSkillIndexByName("Patient Spirit").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2062: return Data[" + SkillDatabase.GetSkillIndexByName("Healing Ribbon").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2063: return Data[" + SkillDatabase.GetSkillIndexByName("Aura of Stability").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2064: return Data[" + SkillDatabase.GetSkillIndexByName("Spotless Mind").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2065: return Data[" + SkillDatabase.GetSkillIndexByName("Spotless Soul").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2066: return Data[" + SkillDatabase.GetSkillIndexByName("Disarm").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2067: return Data[" + SkillDatabase.GetSkillIndexByName("\"I Meant to Do That!\"").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2068: return Data[" + SkillDatabase.GetSkillIndexByName("Rapid Fire").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2069: return Data[" + SkillDatabase.GetSkillIndexByName("Sloth Hunter's Shot").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2070: return Data[" + SkillDatabase.GetSkillIndexByName("Aura Slicer").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2071: return Data[" + SkillDatabase.GetSkillIndexByName("Zealous Sweep").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2072: return Data[" + SkillDatabase.GetSkillIndexByName("Pure Was Li Ming").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2073: return Data[" + SkillDatabase.GetSkillIndexByName("Weapon of Aggression").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2074: return Data[" + SkillDatabase.GetSkillIndexByName("Chest Thumper").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2075: return Data[" + SkillDatabase.GetSkillIndexByName("Hasty Refrain").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2091: return Data[" + SkillDatabase.GetSkillIndexByName("Shadow Sanctuary (Kurzick)").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2092: return Data[" + SkillDatabase.GetSkillIndexByName("Ether Nightmare (Kurzick)").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2093: return Data[" + SkillDatabase.GetSkillIndexByName("Signet of Corruption (Kurzick)").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2094: return Data[" + SkillDatabase.GetSkillIndexByName("Elemental Lord (Kurzick)").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2095: return Data[" + SkillDatabase.GetSkillIndexByName("Selfless Spirit (Kurzick)").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2096: return Data[" + SkillDatabase.GetSkillIndexByName("Triple Shot (Kurzick)").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2097: return Data[" + SkillDatabase.GetSkillIndexByName("\"Save Yourselves!\" (Kurzick)").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2098: return Data[" + SkillDatabase.GetSkillIndexByName("Aura of Holy Might (Kurzick)").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2099: return Data[" + SkillDatabase.GetSkillIndexByName("Spear of Fury (Kurzick)").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2100: return Data[" + SkillDatabase.GetSkillIndexByName("Summon Spirits (Kurzick)").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2101: return Data[" + SkillDatabase.GetSkillIndexByName("Critical Agility").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2102: return Data[" + SkillDatabase.GetSkillIndexByName("Cry of Pain").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2103: return Data[" + SkillDatabase.GetSkillIndexByName("Necrosis").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2104: return Data[" + SkillDatabase.GetSkillIndexByName("Intensity").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2105: return Data[" + SkillDatabase.GetSkillIndexByName("Seed of Life").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2107: return Data[" + SkillDatabase.GetSkillIndexByName("Whirlwind Attack").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2108: return Data[" + SkillDatabase.GetSkillIndexByName("Never Rampage Alone").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2109: return Data[" + SkillDatabase.GetSkillIndexByName("Eternal Aura").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2110: return Data[" + SkillDatabase.GetSkillIndexByName("Vampirism").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2112: return Data[" + SkillDatabase.GetSkillIndexByName("\"There's Nothing to Fear!\"").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2116: return Data[" + SkillDatabase.GetSkillIndexByName("Sneak Attack").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2135: return Data[" + SkillDatabase.GetSkillIndexByName("Trampling Ox").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2136: return Data[" + SkillDatabase.GetSkillIndexByName("Smoke Powder Defense").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2137: return Data[" + SkillDatabase.GetSkillIndexByName("Confusing Images").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2138: return Data[" + SkillDatabase.GetSkillIndexByName("Hexer's Vigor").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2139: return Data[" + SkillDatabase.GetSkillIndexByName("Masochism").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2140: return Data[" + SkillDatabase.GetSkillIndexByName("Piercing Trap").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2141: return Data[" + SkillDatabase.GetSkillIndexByName("Companionship").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2142: return Data[" + SkillDatabase.GetSkillIndexByName("Feral Aggression").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2143: return Data[" + SkillDatabase.GetSkillIndexByName("Disrupting Shot").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2144: return Data[" + SkillDatabase.GetSkillIndexByName("Volley").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2145: return Data[" + SkillDatabase.GetSkillIndexByName("Expert Focus").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2146: return Data[" + SkillDatabase.GetSkillIndexByName("Pious Fury").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2147: return Data[" + SkillDatabase.GetSkillIndexByName("Crippling Victory").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2148: return Data[" + SkillDatabase.GetSkillIndexByName("Sundering Weapon").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2149: return Data[" + SkillDatabase.GetSkillIndexByName("Weapon of Renewal").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2150: return Data[" + SkillDatabase.GetSkillIndexByName("Maiming Spear").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2186: return Data[" + SkillDatabase.GetSkillIndexByName("Signet of Deadly Corruption").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2187: return Data[" + SkillDatabase.GetSkillIndexByName("Way of the Master").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2188: return Data[" + SkillDatabase.GetSkillIndexByName("Defile Defenses").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2189: return Data[" + SkillDatabase.GetSkillIndexByName("Angorodon's Gaze").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2190: return Data[" + SkillDatabase.GetSkillIndexByName("Magnetic Surge").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2191: return Data[" + SkillDatabase.GetSkillIndexByName("Slippery Ground").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2192: return Data[" + SkillDatabase.GetSkillIndexByName("Glowing Ice").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2193: return Data[" + SkillDatabase.GetSkillIndexByName("Energy Blast").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2194: return Data[" + SkillDatabase.GetSkillIndexByName("Distracting Strike").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2195: return Data[" + SkillDatabase.GetSkillIndexByName("Symbolic Strike").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2196: return Data[" + SkillDatabase.GetSkillIndexByName("Soldier's Speed").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2197: return Data[" + SkillDatabase.GetSkillIndexByName("Body Blow").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2198: return Data[" + SkillDatabase.GetSkillIndexByName("Body Shot").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2199: return Data[" + SkillDatabase.GetSkillIndexByName("Poison Tip Signet").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2200: return Data[" + SkillDatabase.GetSkillIndexByName("Signet of Mystic Speed").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2201: return Data[" + SkillDatabase.GetSkillIndexByName("Shield of Force").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2202: return Data[" + SkillDatabase.GetSkillIndexByName("Mending Grip").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2203: return Data[" + SkillDatabase.GetSkillIndexByName("Spiritleech Aura").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2204: return Data[" + SkillDatabase.GetSkillIndexByName("Rejuvenation").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2205: return Data[" + SkillDatabase.GetSkillIndexByName("Agony").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2206: return Data[" + SkillDatabase.GetSkillIndexByName("Ghostly Weapon").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2207: return Data[" + SkillDatabase.GetSkillIndexByName("Inspirational Speech").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2208: return Data[" + SkillDatabase.GetSkillIndexByName("Burning Shield").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2209: return Data[" + SkillDatabase.GetSkillIndexByName("Holy Spear").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2210: return Data[" + SkillDatabase.GetSkillIndexByName("Spear Swipe").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2211: return Data[" + SkillDatabase.GetSkillIndexByName("Alkar's Alchemical Acid").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2212: return Data[" + SkillDatabase.GetSkillIndexByName("Light of Deldrimor").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2213: return Data[" + SkillDatabase.GetSkillIndexByName("Ear Bite").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2214: return Data[" + SkillDatabase.GetSkillIndexByName("Low Blow").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2215: return Data[" + SkillDatabase.GetSkillIndexByName("Brawling Headbutt").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2216: return Data[" + SkillDatabase.GetSkillIndexByName("\"Don't Trip!\"").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2217: return Data[" + SkillDatabase.GetSkillIndexByName("\"By Ural's Hammer!\"").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2218: return Data[" + SkillDatabase.GetSkillIndexByName("Drunken Master").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2219: return Data[" + SkillDatabase.GetSkillIndexByName("Great Dwarf Weapon").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2220: return Data[" + SkillDatabase.GetSkillIndexByName("Great Dwarf Armor").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2221: return Data[" + SkillDatabase.GetSkillIndexByName("Breath of the Great Dwarf").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2222: return Data[" + SkillDatabase.GetSkillIndexByName("Snow Storm").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2223: return Data[" + SkillDatabase.GetSkillIndexByName("Black Powder Mine").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2224: return Data[" + SkillDatabase.GetSkillIndexByName("Summon Mursaat").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2225: return Data[" + SkillDatabase.GetSkillIndexByName("Summon Ruby Djinn").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2226: return Data[" + SkillDatabase.GetSkillIndexByName("Summon Ice Imp").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2227: return Data[" + SkillDatabase.GetSkillIndexByName("Summon Naga Shaman").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2228: return Data[" + SkillDatabase.GetSkillIndexByName("Deft Strike").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2229: return Data[" + SkillDatabase.GetSkillIndexByName("Signet of Infection").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2230: return Data[" + SkillDatabase.GetSkillIndexByName("Tryptophan Signet").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2231: return Data[" + SkillDatabase.GetSkillIndexByName("Ebon Battle Standard of Courage").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2232: return Data[" + SkillDatabase.GetSkillIndexByName("Ebon Battle Standard of Wisdom").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2233: return Data[" + SkillDatabase.GetSkillIndexByName("Ebon Battle Standard of Honor").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2234: return Data[" + SkillDatabase.GetSkillIndexByName("Ebon Vanguard Sniper Support").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2235: return Data[" + SkillDatabase.GetSkillIndexByName("Ebon Vanguard Assassin Support").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2236: return Data[" + SkillDatabase.GetSkillIndexByName("Well of Ruin").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2237: return Data[" + SkillDatabase.GetSkillIndexByName("Atrophy").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2238: return Data[" + SkillDatabase.GetSkillIndexByName("Spear of Redemption").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2353: return Data[" + SkillDatabase.GetSkillIndexByName("\"Finish Him!\"").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2354: return Data[" + SkillDatabase.GetSkillIndexByName("\"Dodge This!\"").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2355: return Data[" + SkillDatabase.GetSkillIndexByName("\"I Am The Strongest!\"").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2356: return Data[" + SkillDatabase.GetSkillIndexByName("\"I Am Unstoppable!\"").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2357: return Data[" + SkillDatabase.GetSkillIndexByName("A Touch of Guile").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2358: return Data[" + SkillDatabase.GetSkillIndexByName("\"You Move Like a Dwarf!\"").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2359: return Data[" + SkillDatabase.GetSkillIndexByName("\"You Are All Weaklings!\"").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2360: return Data[" + SkillDatabase.GetSkillIndexByName("Feel No Pain").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2361: return Data[" + SkillDatabase.GetSkillIndexByName("Club of a Thousand Bears").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2374: return Data[" + SkillDatabase.GetSkillIndexByName("Ursan Blessing").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2379: return Data[" + SkillDatabase.GetSkillIndexByName("Volfen Blessing").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2384: return Data[" + SkillDatabase.GetSkillIndexByName("Raven Blessing").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2411: return Data[" + SkillDatabase.GetSkillIndexByName("Mindbender").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2412: return Data[" + SkillDatabase.GetSkillIndexByName("Smooth Criminal").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2413: return Data[" + SkillDatabase.GetSkillIndexByName("Technobabble").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2414: return Data[" + SkillDatabase.GetSkillIndexByName("Radiation Field").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2415: return Data[" + SkillDatabase.GetSkillIndexByName("Asuran Scan").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2416: return Data[" + SkillDatabase.GetSkillIndexByName("Air of Superiority").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2417: return Data[" + SkillDatabase.GetSkillIndexByName("Mental Block").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2418: return Data[" + SkillDatabase.GetSkillIndexByName("Pain Inverter").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2420: return Data[" + SkillDatabase.GetSkillIndexByName("Ebon Escape").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2421: return Data[" + SkillDatabase.GetSkillIndexByName("Weakness Trap").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2422: return Data[" + SkillDatabase.GetSkillIndexByName("Winds").ToString() + "]" + Environment.NewLine;
            __HTMLSummary.Text += "case 2423: return Data[" + SkillDatabase.GetSkillIndexByName("Dwarven Stability").ToString() + "]" + Environment.NewLine;

        }

        private void SetColors(Control control, Color foreground, Color background)
        {
            control.ForeColor = foreground;
            control.BackColor = background;
            
            foreach(Control c in control.Controls)
            {
                SetColors(c, foreground, background);
            }
        }

        private void __GenerateRatingsFile_Click(object sender, EventArgs e)
        {
            System.IO.StreamWriter output = new System.IO.StreamWriter("Skills Ratings.txt");

            output.WriteLine("You can modify any ratings value or delete lines, but skills are referenced by the initial numbers, so don't touch those if you want the file to load properly! Also, don't delete this line or split it into multiple lines or the first entry will be skipped.");

            foreach(Skill skill in SkillDatabase.Data)
            {
                output.WriteLine(skill.ID.ToString() + "|rating:" + skill.Rating.ToString() + "|" + skill.Name);
            }

            output.Close();

            MessageBox.Show("Ratings file generated. Please open the install directory to edit.", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void __LoadRatingFilebutton_Click(object sender, EventArgs e)
        {
            if(SkillDatabase.CheckRatingsFile())
            {
                MessageBox.Show("Ratings file loaded!", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No ratings file found!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
