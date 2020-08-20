using System.Collections.Generic;
using System.Windows.Forms;

namespace GW_Codex_Generator.Codex_Method_Settings
{
    public partial class AttributeBalanced : UserControl, ICodexPoolSettingsGenerator
    {
        public AttributeBalanced()
        {
            InitializeComponent();
        }

        List<Skill> ICodexPoolSettingsGenerator.GenerateCodex(List<Skill> pool)
        {
            return CodexGenerator.AttributeBalanced(pool, (int)__Regular.Value, (int)__Elites.Value, (int)__PvEOnly.Value);
        }
    }
}
