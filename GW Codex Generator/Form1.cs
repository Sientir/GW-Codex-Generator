using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            // Hook up skill info displays:
            __CodexSkillDisplay.DescriptionBox = __CodexSkillInfo;
            __Codex_UseRatings.Checked = GrabBag.UseRatings; // We may save this setting, so better load it!
            __Codex_UseRatings.CheckedChanged += __Codex_UseRatings_CheckedChanged;
            __Codex_SelectMethod.SelectedIndexChanged += __Codex_SelectMethod_SelectedIndexChanged;
            __Codex_SelectMethod.SelectedIndex = 3;

            // Create challenge controls:
            Challenge_UI.BinaryChallenges binaryChallenges = new Challenge_UI.BinaryChallenges();
            _ChallengeControls.Add(binaryChallenges);
            binaryChallenges.Dock = DockStyle.Top;

            Challenge_UI.RequiredElites requiredElites = new Challenge_UI.RequiredElites();
            _ChallengeControls.Add(requiredElites);
            requiredElites.Dock = DockStyle.Top;
            requiredElites.SetSkillDisplayInfo(__CodexSkillInfo);


            // Properly establish the challenge controls:
            for(int i = _ChallengeControls.Count-1; i >= 0; --i) // running this in reverse, as previous experience has taught me that adding a new top-docked control will put it above previous ones, and I want to lead with the order they are in the list.
            {
                _ChallengeControls[i].SetHTMLRefreshFunction(GenerateHTMLPage);
                __Tabs.TabPages[2].Controls.Add((Control)_ChallengeControls[i]);
            }

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

            //GenerateCampaignCode(); // Only do this to generate the code, which I've done!
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
