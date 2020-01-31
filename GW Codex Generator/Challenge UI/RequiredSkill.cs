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
    public partial class RequiredSkill : UserControl, IChallengeToHTML
    {
        Skill _RolledSkill = null;
        public RequiredSkill()
        {
            InitializeComponent();
            __SkillIcon.MouseEnter += __SkillIcon_MouseEnter;
        }

        private void __SkillIcon_MouseEnter(object sender, EventArgs e)
        {
            if(_RolledSkill != null && SkillInfoDisplay != null)
            {
                SkillInfoDisplay.DrawSkill(_RolledSkill);
            }
        }

        private void __RollSkill_Click(object sender, EventArgs e)
        {
            int eliteIndex = 0;
            if (__ElitesAllowed.Checked) eliteIndex = 1;
            if (__OnlyElites.Checked) eliteIndex = 2;

            _RolledSkill = Challenges.RequiredSkill(eliteIndex);
            __SkillIcon.Image = _RolledSkill.Icon;
            refreshHTMLFunction?.Invoke();
        }

        string IChallengeToHTML.GetHTML()
        {
            if (_RolledSkill != null)
            {
                return "<h1>Required Skill</h1>All characters must bring the following skill: " + _RolledSkill.AHref() + Environment.NewLine;
            }
            else
            {
                return "";
            }
        }
        Challenges.RefreshHTMLFunction refreshHTMLFunction;
        void IChallengeToHTML.SetHTMLRefreshFunction(Challenges.RefreshHTMLFunction refreshFunction)
        {
            refreshHTMLFunction = refreshFunction;
        }

        public Skill_UI.SkillInfoDisplay SkillInfoDisplay = null;
    }
}
