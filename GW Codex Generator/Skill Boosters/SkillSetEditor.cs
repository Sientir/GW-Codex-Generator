using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GW_Codex_Generator.Skill_Boosters
{
    public partial class SkillSetEditor : Form
    {
        internal SkillBoosterSet CurrentSet { get; private set; }
        public event EventHandler<string> UpdateName;
        List<NumericUpDown> RarityDistributionControls = new List<NumericUpDown>();
        internal SkillSetEditor(SkillBoosterSet set)
        {
            InitializeComponent();
            // Attributes
            __Deselect_AttributeList.SelectedIndex = 0;
            __Select_AttributeList.SelectedIndex = 0;
            // Professions
            __Deselect_ProfessionList.SelectedIndex = 0;
            __Select_ProfessionList.SelectedIndex = 0;
            // Ratings
            int maximum_rating = 5;
            for (int i = 1; i <= maximum_rating; ++i)
            {
                __Deselect_RatingList.Items.Add(i.ToString());
                __Select_RatingsList.Items.Add(i.ToString());
            }
            __Deselect_RatingList.SelectedIndex = 0;
            __Select_RatingsList.SelectedIndex = 0;

            // Set up some functionalities!
            __SkillSetDisplay.DescriptionBox = __SkillInfoDisplay;
            __SkillSetDisplay.UpdateSetDescription = UpdateSetDescription;

            // Set information for a set being edited, I guess? Seems like a good idea!
            if (set == null)
            {
                CurrentSet = new SkillBoosterSet();
                __SetName.Text = set.Name;
            }
            else
            {
                CurrentSet = set;
                __SkillSetDisplay.SetFromSet(set);
                __SetName.Text = set.Name;
            }
            UpdateSetDescription();
            __RarityDistribution.RowCount = SkillDatabase.RarityLabels.Length;
            // Set the height to make sure this is compact!
            for(int i = 0; i < __RarityDistribution.RowStyles.Count; ++i)
            {
                __RarityDistribution.RowStyles[i].Height = 22; // Just hardcoding this here....
            }

            // Hardcoding some widths to try and make the horizontal scrollbar not show up...
            __RarityDistribution.ColumnStyles[0].SizeType = SizeType.Absolute;
            __RarityDistribution.ColumnStyles[1].SizeType = SizeType.Absolute;
            __RarityDistribution.ColumnStyles[0].Width = 90;
            __RarityDistribution.ColumnStyles[1].Width = 65;

            // Now, place the rarity distribution controls into the table:
            for (int i = 0; i < SkillDatabase.RarityLabels.Length; ++i)
            {
                // Label for the rarity:
                Label rarity_label = new Label();
                rarity_label.Size = new Size(90, 20);
                rarity_label.TextAlign = ContentAlignment.MiddleRight;
                rarity_label.Text = SkillDatabase.RarityLabels[i];
                __RarityDistribution.Controls.Add(rarity_label, 0, i);

                // Numeric update for the quantity:
                NumericUpDown nud = new NumericUpDown();
                nud.Width = 60;
                if (i < CurrentSet.PackRarityContents.Length)
                {
                    nud.Value = CurrentSet.PackRarityContents[i];
                }
                __RarityDistribution.Controls.Add(nud, 1, i);
                RarityDistributionControls.Add(nud);
            }
        }

        private void UpdateSetDescription()
        {
            __SetStats.Text = "";
            int[] counts = new int[SkillDatabase.RarityLabels.Length];
            for (int i = 0; i < counts.Length; ++i) counts[i] = 0;
            foreach (Pair<Skill, bool> skill in __SkillSetDisplay.Skills)
            {
                // Count it if it is included and also if it fits in the rarity range:
                if (skill.second && skill.first.Rarity >= 0 && skill.first.Rarity < counts.Length)
                {
                    counts[skill.first.Rarity]++;
                }
            }

            for (int i = 0; i < counts.Length; ++i)
            {
                if (i > 0) __SetStats.Text += Environment.NewLine;
                __SetStats.Text += SkillDatabase.RarityLabels[i] + ": " + counts[i].ToString();
            }
        }

        public void Save()
        {
            string skill_set_dir = Environment.CurrentDirectory + "\\GW Codex Skill Sets";
            if (System.IO.Directory.Exists(skill_set_dir) == false)
            {
                System.IO.Directory.CreateDirectory(skill_set_dir);
            }

            try
            {
                CurrentSet.Save(skill_set_dir + "\\" + CurrentSet.Name + ".gwset");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Saving file. Did you include illegal characters for a filename?" + Environment.NewLine + Environment.NewLine + "Error message: " + ex.Message, "Error Saving Set", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            UpdateName?.Invoke(CurrentSet, CurrentSet.Name);
        }

        private void __Select_Core_Click(object sender, EventArgs e)
        {
            __SkillSetDisplay.SetByCampaign(Skill.Campaigns.Core, true);
        }

        private void __Select_Prophecies_Click(object sender, EventArgs e)
        {
            __SkillSetDisplay.SetByCampaign(Skill.Campaigns.Prophecies, true);
        }

        private void __Select_Factions_Click(object sender, EventArgs e)
        {
            __SkillSetDisplay.SetByCampaign(Skill.Campaigns.Factions, true);
        }

        private void __Select_Nightfall_Click(object sender, EventArgs e)
        {
            __SkillSetDisplay.SetByCampaign(Skill.Campaigns.Nightfall, true);
        }

        private void __Select_EyeOfTheNorth_Click(object sender, EventArgs e)
        {
            __SkillSetDisplay.SetByCampaign(Skill.Campaigns.Eye_of_the_North, true);
        }

        private void __Select_All_Click(object sender, EventArgs e)
        {
            __SkillSetDisplay.SetAll(true);
        }

        private void __Deselect_Core_Click(object sender, EventArgs e)
        {
            __SkillSetDisplay.SetByCampaign(Skill.Campaigns.Core, false);
        }

        private void __Deselect_Prophecies_Click(object sender, EventArgs e)
        {
            __SkillSetDisplay.SetByCampaign(Skill.Campaigns.Prophecies, false);
        }

        private void __Deselect_Factions_Click(object sender, EventArgs e)
        {
            __SkillSetDisplay.SetByCampaign(Skill.Campaigns.Factions, false);
        }

        private void __Deselect_Nightfall_Click(object sender, EventArgs e)
        {
            __SkillSetDisplay.SetByCampaign(Skill.Campaigns.Nightfall, false);
        }

        private void __Deselect_EotN_Click(object sender, EventArgs e)
        {
            __SkillSetDisplay.SetByCampaign(Skill.Campaigns.Eye_of_the_North, false);
        }

        private void __Deselect_All_Click(object sender, EventArgs e)
        {
            __SkillSetDisplay.SetAll(false);
        }

        private void __Select_Attribute_Click(object sender, EventArgs e)
        {
            __SkillSetDisplay.SetByAttribute((Skill.Attributes)__Select_AttributeList.SelectedIndex, true);
        }

        private void __Select_Profession_Click(object sender, EventArgs e)
        {
            __SkillSetDisplay.SetByProfession((Skill.Professions)__Select_ProfessionList.SelectedIndex, true);
        }

        private void __Select_Rating_Click(object sender, EventArgs e)
        {
            __SkillSetDisplay.SetByRating(__Select_RatingsList.SelectedIndex + 1, true);
        }

        private void __Deselect_Attribute_Click(object sender, EventArgs e)
        {
            __SkillSetDisplay.SetByAttribute((Skill.Attributes)__Deselect_AttributeList.SelectedIndex, false);
        }

        private void __Deselect_Profession_Click(object sender, EventArgs e)
        {
            __SkillSetDisplay.SetByProfession((Skill.Professions)__Deselect_ProfessionList.SelectedIndex, false);
        }

        private void __Deselect_Rating_Click(object sender, EventArgs e)
        {
            __SkillSetDisplay.SetByRating(__Deselect_RatingList.SelectedIndex + 1, false);
        }

        private void __Select_Elites_Click(object sender, EventArgs e)
        {
            __SkillSetDisplay.SetByEliteStatus(true, true);
        }

        private void __Select_NonElites_Click(object sender, EventArgs e)
        {
            __SkillSetDisplay.SetByEliteStatus(false, true);
        }

        private void __Deselect_Elites_Click(object sender, EventArgs e)
        {
            __SkillSetDisplay.SetByEliteStatus(true, false);
        }

        private void __Deselect_NonElites_Click(object sender, EventArgs e)
        {
            __SkillSetDisplay.SetByEliteStatus(false, false);
        }

        private void __Apply_And_Close_Click(object sender, EventArgs e)
        {
            // Grab skills:
            CurrentSet.SetSkills.Clear();
            foreach (Pair<Skill, bool> skill in __SkillSetDisplay.Skills)
            {
                if (skill.second)
                {
                    CurrentSet.SetSkills.Add(skill.first.ID);
                }
            }

            // Set the name:
            CurrentSet.Name = __SetName.Text;

            // Handle rarity distribution:
            if (CurrentSet.PackRarityContents.Length != RarityDistributionControls.Count)
            {
                CurrentSet.PackRarityContents = new int[RarityDistributionControls.Count];
            }
            for (int i = 0; i < CurrentSet.PackRarityContents.Length; ++i)
            {
                CurrentSet.PackRarityContents[i] = (int)(RarityDistributionControls[i].Value);
            }

            // Save and close:
            Save();
            Close();
        }
    }
}
