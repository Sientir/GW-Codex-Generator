using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GW_Codex_Generator.Codex_Method_Settings
{
    public partial class RegularFromAttributesElitesFromProfession : UserControl, ICodexPoolSettingsGenerator
    {
        public RegularFromAttributesElitesFromProfession()
        {
            InitializeComponent();
        }

        List<Skill> ICodexPoolSettingsGenerator.GenerateCodex(List<Skill> pool)
        {
            return CodexGenerator.AttributeRegularProfessionElite(pool, (int)__RegularSkillsPerAttribute.Value, (int)__EliteSkillsPerProfession.Value, (int)__PvEOnlySkills.Value);
        }
    }
}
