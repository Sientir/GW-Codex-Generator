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
    public partial class RequiredElites : UserControl, IChallengeToHTML
    {
        Challenges.RefreshHTMLFunction refreshHTML;
        public RequiredElites()
        {
            InitializeComponent();
        }

        public void SetSkillDisplayInfo(Skill_UI.SkillInfoDisplay info)
        {
            __SkillDisplay.DescriptionBox = info;
        }

        string IChallengeToHTML.GetHTML()
        {
            if (__IncludeInHTMLSummary.Checked)
            {
                string html = "<h1>Required Elites</h1>" + Environment.NewLine;
                int i = 1;
                foreach(Skill skill in __SkillDisplay.Skills)
                {
                    html += i.ToString()+". "+skill.AHref() + "<br/>" + Environment.NewLine;
                    ++i;
                }

                return html;
            }
            else return "";
        }

        void IChallengeToHTML.SetHTMLRefreshFunction(Challenges.RefreshHTMLFunction refreshFunction)
        {
            refreshHTML = refreshFunction;
        }

        private void __RollButton_Click(object sender, EventArgs e)
        {
            __IncludeInHTMLSummary.Checked = true;

            __SkillDisplay.Skills = Challenges.MandatoryElites((int)__PartySize.Value, (int)Math.Min(__RatingLow.Value, __RangeMax.Value), (int)Math.Max(__RatingLow.Value, __RangeMax.Value), __NoSkillsFromPrimaries.Checked);
            __SkillDisplay.Redraw();

            refreshHTML?.Invoke();
        }
    }
}
