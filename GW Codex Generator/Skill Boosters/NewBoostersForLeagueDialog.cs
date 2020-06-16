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
    public partial class NewBoostersForLeagueDialog : Form
    {
        public NewBoostersForLeagueDialog()
        {
            InitializeComponent();
            __Pool.DescriptionBox = __SkillInfoDisplay;
        }

        static internal void ShowAddedBoostersDialog(int boosters, BoosterLeaguePool pool)
        {
            NewBoostersForLeagueDialog dialog = new NewBoostersForLeagueDialog();
            dialog.Text = "Added skills from " + boosters.ToString() + " booster packs!";
            dialog.__Pool.SetPool(pool);
            dialog.ShowDialog();
        }

        private void __OK_Button_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
