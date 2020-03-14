using System;
using System.Windows.Forms;

namespace GW_Codex_Generator
{
    public partial class TemplateReaderDisplay : UserControl
    {
        PictureBox[] SkillBar = new PictureBox[8];
        SkillDatabase.TemplateInformation TemplateInformation = null;
        public TemplateReaderDisplay()
        {
            InitializeComponent();

            SkillBar[0] = __Skill1;
            SkillBar[1] = __Skill2;
            SkillBar[2] = __Skill3;
            SkillBar[3] = __Skill4;
            SkillBar[4] = __Skill5;
            SkillBar[5] = __Skill6;
            SkillBar[6] = __Skill7;
            SkillBar[7] = __Skill8;

            for (int i = 0; i < SkillBar.Length; ++i)
            {
                SkillBar[i].Tag = i;
                SkillBar[i].MouseEnter += TemplateReaderDisplay_MouseEnter;
            }

            __GeneralInformation.Text = "No Template." + Environment.NewLine + Environment.NewLine + "Paste template code into" + Environment.NewLine + "code area to translate.";
            __TemplateCode.TextChanged += __TemplateCode_TextChanged;
        }

        private void __TemplateCode_TextChanged(object sender, EventArgs e)
        {
            if(TemplateInformation == null) TemplateInformation = new SkillDatabase.TemplateInformation(__TemplateCode.Text);
            else TemplateInformation.ParseTemplateCode(__TemplateCode.Text);
            if(TemplateInformation.ValidTemplate)
            {
                // Start off by placing the profession information:
                __GeneralInformation.Text = TemplateInformation.PrimaryProfession.ToString();
                if (TemplateInformation.SecondaryProfession != Skill.Professions.None) __GeneralInformation.Text += "/" + TemplateInformation.SecondaryProfession.ToString();
                __GeneralInformation.Text += Environment.NewLine;

                // Now, let's grab the attributes information:
                foreach(SkillDatabase.TemplateInformation.AttributeRank attr in TemplateInformation.Attributes)
                {
                    __GeneralInformation.Text += attr.Attribute.ToString().Replace('_', ' ') + ": " + attr.Rank.ToString() + Environment.NewLine;
                }

                for(int i = 0; i < 8; ++i)
                {
                    if(TemplateInformation.SkillBar[i] != null)
                    {
                        SkillBar[i].Image = TemplateInformation.SkillBar[i].Icon;
                    }
                    else
                    {
                        SkillBar[i].Image = null;
                    }
                }
            }
            else
            {
                foreach (PictureBox box in SkillBar) box.Image = null;
                __GeneralInformation.Text = "Invalid template code.";
            }
        }

        private void TemplateReaderDisplay_MouseEnter(object sender, EventArgs e)
        {
            if (SkillInformationDisplay != null && TemplateInformation != null && TemplateInformation.ValidTemplate)
            {
                SkillInformationDisplay.DrawSkill(TemplateInformation.SkillBar[(int)((PictureBox)sender).Tag]);
            }
        }

        public Skill_UI.SkillInfoDisplay SkillInformationDisplay = null;
    }
}
