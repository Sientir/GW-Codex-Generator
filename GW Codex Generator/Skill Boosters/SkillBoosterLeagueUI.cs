using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GW_Codex_Generator.Skill_Boosters
{
    public partial class SkillBoosterLeagueUI : UserControl
    {
        BoosterLeaguePool League = new BoosterLeaguePool();
        public SkillBoosterLeagueUI()
        {
            InitializeComponent();

            // Let's just grab whatever sets are here at this time...
            foreach (SkillBoosterSet set in SkillBoosterSet.Sets)
            {
                __SelectedSet.Items.Add(set.Name);
            }

            // Hook these guys up...
            __LeaguePoolDisplay.SetPool(League);

            Resize += SkillBoosterLeagueUI_Resize;
            /*
             Things this needs to do:
             - Be able to add N booster packs to a league.
             - Display list of skills in pool.
             - Save and load leagues.
             - Specify set used for the league...needs sets!
             */
        }

        private void SkillBoosterLeagueUI_Resize(object sender, EventArgs e)
        {
            __LeaguePoolDisplay.Width = Math.Max(ClientSize.Width - __LeaguePoolDisplay.Left * 2, 100);
            __LeaguePoolDisplay.Height = Math.Max(ClientSize.Height - __LeaguePoolDisplay.Left - __LeaguePoolDisplay.Top, 100);
            __LeaguePoolDisplay.Redraw();
        }

        internal void SetSkillDescriptionBox(Skill_UI.SkillInfoDisplay display)
        {
            __LeaguePoolDisplay.DescriptionBox = display;
        }

        private void __Button_RefreshSetList_Click(object sender, EventArgs e)
        {
            __SelectedSet.Items.Clear();
            foreach(SkillBoosterSet set in SkillBoosterSet.Sets)
            {
                __SelectedSet.Items.Add(set.Name);
            }
        }

        private void __Button_Save_Click(object sender, EventArgs e)
        {
            __SaveBoosterLeagueDialog.ShowDialog();
        }

        private void __Button_Open_Click(object sender, EventArgs e)
        {
            __OpenBoosterLeagueDialog.ShowDialog();
        }

        private void __SaveBoosterLeagueDialog_FileOk(object sender, CancelEventArgs e)
        {
            League.SavePool(__SaveBoosterLeagueDialog.FileName);
        }

        private void __OpenBoosterLeagueDialog_FileOk(object sender, CancelEventArgs e)
        {
            string filename = __OpenBoosterLeagueDialog.FileName;
            BoosterLeaguePool openedLeague = BoosterLeaguePool.ReadFromFile(filename);
            if(openedLeague != null)
            {
                __SaveBoosterLeagueDialog.FileName = filename;
                League = openedLeague;
                __LeaguePoolDisplay.SetPool(League);
            }
        }

        private void __NumberOfBoostersToAdd_ValueChanged(object sender, EventArgs e)
        {
            __Button_AddBoosters.Text = "Add " + __NumberOfBoostersToAdd.Value.ToString("N0") + " Boosters";
        }

        private void __Button_AddBoosters_Click(object sender, EventArgs e)
        {
            // Add boosters to league:
            List<SkillBoosterPack> boosters = SkillBoosterPack.GenerateBoosterBox(__SelectedSet.SelectedIndex < 0 ? 0 : __SelectedSet.SelectedIndex, (int)__NumberOfBoostersToAdd.Value);
            // Temporary league to hold the added skills for display purposes...
            BoosterLeaguePool newBoosters = new BoosterLeaguePool();
            newBoosters.AddBox(boosters);
            newBoosters.SortByProfessionAttribute();
            newBoosters.SortByRarity();
            League.AddBox(boosters);
            // Redraw the pool...
            __LeaguePoolDisplay.Redraw();
            // Show a dialog containing the new skills:
            NewBoostersForLeagueDialog.ShowAddedBoostersDialog((int)__NumberOfBoostersToAdd.Value, newBoosters);
        }

        private void __SortButton_Standard_Click(object sender, EventArgs e)
        {
            League.SortByProfessionAttribute();
            __LeaguePoolDisplay.Redraw();
        }

        private void __SortButton_Rarity_Click(object sender, EventArgs e)
        {
            League.SortByRarity();
            __LeaguePoolDisplay.Redraw();
        }

        private void __SortButton_Rating_Click(object sender, EventArgs e)
        {
            League.SortByRating();
            __LeaguePoolDisplay.Redraw();
        }
    }
}
