using System;
using System.Windows.Forms;

namespace GW_Codex_Generator.Challenge_UI
{
    public partial class TemplateDisplay : UserControl
    {
        SkillDatabase.TemplateInformation TemplateInfo = null;
        PictureBox[] Skillbar = new PictureBox[8];
        public TemplateDisplay()
        {
            InitializeComponent();

            Skillbar[0] = __Skill1;
            Skillbar[1] = __Skill2;
            Skillbar[2] = __Skill3;
            Skillbar[3] = __Skill4;
            Skillbar[4] = __Skill5;
            Skillbar[5] = __Skill6;
            Skillbar[6] = __Skill7;
            Skillbar[7] = __Skill8;

            for(int i = 0; i < 8; ++i)
            {
                Skillbar[i].Tag = i;
                Skillbar[i].MouseEnter += TemplateDisplay_MouseEnter;
            }
        }

        private void TemplateDisplay_MouseEnter(object sender, EventArgs e)
        {
            if (TemplateInfo != null && TemplateInfo.ValidTemplate && SkillInfoDisplay != null)
            {
                SkillInfoDisplay.DrawSkill(TemplateInfo.SkillBar[(int)((PictureBox)sender).Tag]);
            }
        }

        public Skill_UI.SkillInfoDisplay SkillInfoDisplay = null;

        /// <summary>
        /// Sets the information in the control from a template information string.
        /// </summary>
        /// <param name="str">The template information string.</param>
        public void SetTemplateInformation(string str)
        {
            string[] info = str.Split('|');
            __TemplateName.Text = info[0];
            __TemplateCode.Text = info[1];
            if (TemplateInfo == null) TemplateInfo = new SkillDatabase.TemplateInformation(info[1]);
            else TemplateInfo.ParseTemplateCode(info[1]);

            if (TemplateInfo.ValidTemplate)
            {
                // Start off by placing the profession information:
                __TemplateInfo.Text = TemplateInfo.PrimaryProfession.ToString();
                if (TemplateInfo.SecondaryProfession != Skill.Professions.None) __TemplateInfo.Text += "/" + TemplateInfo.SecondaryProfession.ToString();
                __TemplateInfo.Text += Environment.NewLine;

                // Now, let's grab the attributes information:
                foreach (SkillDatabase.TemplateInformation.AttributeRank attr in TemplateInfo.Attributes)
                {
                    __TemplateInfo.Text += attr.Attribute.ToString().Replace('_', ' ') + ": " + attr.Rank.ToString() + Environment.NewLine;
                }

                for (int i = 0; i < 8; ++i)
                {
                    if (TemplateInfo.SkillBar[i] != null)
                    {
                        Skillbar[i].Image = TemplateInfo.SkillBar[i].Icon;
                    }
                    else
                    {
                        Skillbar[i].Image = null;
                    }
                }
            }
            else
            {
                foreach (PictureBox box in Skillbar) box.Image = null;
                __TemplateInfo.Text = "Invalid template code.";
            }
        }

        private void __CopyTemplateCode_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(__TemplateCode.Text);
        }

        public override string ToString()
        {
            return __TemplateName.Text + ": " + __TemplateCode.Text;
        }
    }
}
