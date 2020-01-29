using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GW_Codex_Generator.Challenge_UI
{
    public partial class BinaryChallenges : UserControl, IChallengeToHTML
    {
        Challenges.RefreshHTMLFunction refreshHTML;
        public BinaryChallenges()
        {
            InitializeComponent();
            __NumberOfRules.Maximum = Challenges.Restrictions.Length;
        }

        public string GetHTML()
        {
            if(__IncludeHTML.Checked)
            {
                return "<h1>Rules</h1>" + Environment.NewLine + __Rules.Text.Replace(Environment.NewLine, "<br/>" + Environment.NewLine) + "<br/>";
            }
            else return "";
        }

        void IChallengeToHTML.SetHTMLRefreshFunction(Challenges.RefreshHTMLFunction refreshFunction)
        {
            refreshHTML = refreshFunction;
        }

        private void __RollRules_Click(object sender, EventArgs e)
        {
            __IncludeHTML.Checked = true;
            string[] rules = Challenges.BooleanRestrictions((int)__NumberOfRules.Value);
            __Rules.Text = "";
            for(int i = 0; i < rules.Length;++i)
            {
                if (i > 0) __Rules.Text += Environment.NewLine;
                __Rules.Text += rules[i];
            }
            refreshHTML?.Invoke();
        }
    }
}
