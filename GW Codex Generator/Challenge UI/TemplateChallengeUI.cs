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
    public partial class TemplateChallengeUI : UserControl, IChallengeToHTML
    {
        List<TemplateDisplay> _TemplateDisplays = new List<TemplateDisplay>();
        public TemplateChallengeUI()
        {
            InitializeComponent();
        }

        public Skill_UI.SkillInfoDisplay SkillInfoDisplay = null;
        private void __RollTemplateSelection_Click(object sender, EventArgs e)
        {
            string[] templates = Challenges.RandomBuildTemplates((int)__TemplateCount.Value, __AllowDuplicates.Checked);
            __TemplatesPanel.Controls.Clear();
            _TemplateDisplays.Clear();
            foreach (string str in templates)
            {
                TemplateDisplay td = new TemplateDisplay();
                td.SetTemplateInformation(str);
                td.SkillInfoDisplay = SkillInfoDisplay;
                _TemplateDisplays.Add(td);
                __TemplatesPanel.Controls.Add(td);
            }
            __IncludeInHTMLSummary.Checked = true;
            refreshHTML();
        }

        private void __SaveTemplateDatabase_Click(object sender, EventArgs e)
        {
            System.IO.StreamWriter output = new System.IO.StreamWriter("Templates.txt");
            foreach (string str in SkillDatabase.TemplatesDatabase)
            {
                output.WriteLine(str);
            }
            output.Close();
            MessageBox.Show("Templates.txt template database exported!", "Operation Finished", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void __LoadTemplateFile_Click(object sender, EventArgs e)
        {
            if (SkillDatabase.LoadTemplatesFile())
            {
                MessageBox.Show("Loaded templates found in Templates.txt database file.", "Loaded Templates Database", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No Templates.txt file found. Reloaded default templates.", "Loading Templates Database File Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        string IChallengeToHTML.GetHTML()
        {
            if (__IncludeInHTMLSummary.Checked == false) return "";
            string ret = Environment.NewLine + "<h1>Required Template";
            if (_TemplateDisplays.Count == 1)
            {
                ret += "</h1>" + Environment.NewLine + _TemplateDisplays[0].ToString() + Environment.NewLine;
            }
            else
            {
                ret += "s</h1>" + Environment.NewLine;
                for (int i = 0; i < _TemplateDisplays.Count; ++i)
                {
                    ret += (i + 1).ToString() + ". " + _TemplateDisplays[i].ToString();
                    if (i < _TemplateDisplays.Count - 1)
                    {
                        ret += "<br/>" + Environment.NewLine;
                    }
                    else
                    {
                        ret += Environment.NewLine;
                    }
                }
            }

            return ret;
        }

        Challenges.RefreshHTMLFunction refreshHTML;
        void IChallengeToHTML.SetHTMLRefreshFunction(Challenges.RefreshHTMLFunction refreshFunction)
        {
            refreshHTML = refreshFunction;
        }
    }
}
