using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GW_Codex_Generator.Challenge_UI
{
    public partial class MandatorySkillsUI : UserControl, IChallengeToHTML
    {
        CheckBox[] _UsedCampaignsInCodex = new CheckBox[5];
        public MandatorySkillsUI()
        {
            InitializeComponent();
        }

        public void SetCodexPoolCampaignCheckboxes(CheckBox core, CheckBox prophecies, CheckBox factions, CheckBox nightfall, CheckBox eyeofthenorth)
        {
            _UsedCampaignsInCodex[0] = core;
            _UsedCampaignsInCodex[1] = prophecies;
            _UsedCampaignsInCodex[2] = factions;
            _UsedCampaignsInCodex[3] = nightfall;
            _UsedCampaignsInCodex[4] = eyeofthenorth;
        }

        internal void SetSkillInfoDisplay(Skill_UI.SkillInfoDisplay infobox)
        {
            __SkillsDisplay.DescriptionBox = infobox;
        }

        string IChallengeToHTML.GetHTML()
        {
            if (__SkillsDisplay.Skills == null || __SkillsDisplay.Skills.Count == 0) return "";
            string str = "<h1>Mandated Skills</h1>" + Environment.NewLine;
            str += "These are skills that must appear somewhere on your team, but can be distributed among your party however much you want. " +
                "Of course, if there are more elites than characters (because you allowed elites), you must pick from among those elites, but " +
                "you don't have to bring all of them, seeing as you can't.<br/><br/>"+Environment.NewLine;

            str += __SkillsDisplay.Skills.CodexToHTML();
            return str;
        }

        Challenges.RefreshHTMLFunction refreshHTML;
        void IChallengeToHTML.SetHTMLRefreshFunction(Challenges.RefreshHTMLFunction refreshFunction)
        {
            refreshHTML = refreshFunction;
        }

        private void __RollSkillsButton_Click(object sender, EventArgs e)
        {
            List<Skill> pool;
            if(__UseCodexPool.Checked)
            {
                int usedCampaigns = 0;
                for(int i = 0; i < 5; ++i)
                {
                    if (_UsedCampaignsInCodex[i].Checked) usedCampaigns |= (1 << i);
                }

                // Just grab the entire thing if elites are included:
                if(__IncludeElites.Checked)
                {
                    pool = SkillDatabase.GetSkillsByCampaign(usedCampaigns, false);
                }
                else
                {
                    // Gonna need to filter out the elites here:
                    pool = new List<Skill>();
                    foreach(Skill skill in SkillDatabase.GetSkillsByCampaign(usedCampaigns, false))
                    {
                        if (skill.IsElite == false) pool.Add(skill);
                    }
                }
            }
            else
            {
                pool = new List<Skill>();
                foreach(Skill skill in SkillDatabase.Data)
                {
                    // You can enter this IF only if elites are included OR you aren't an elite skill (which always get included) and you also aren't a PvE only skill:
                    if((__IncludeElites.Checked || skill.IsElite==false) && skill.Attribute != Skill.Attributes.PvE_Only)
                    {
                        pool.Add(skill);
                    }
                }
            }

            __SkillsDisplay.Skills = Challenges.RequiredSkills(pool, (int)__SkillsToPick.Value);
            __SkillsDisplay.Redraw();

            refreshHTML();
        }
    }
}
