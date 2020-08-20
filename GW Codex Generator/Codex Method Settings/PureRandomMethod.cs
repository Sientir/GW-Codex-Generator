using System.Collections.Generic;
using System.Windows.Forms;

namespace GW_Codex_Generator.Codex_Method_Settings
{
    public partial class PureRandomMethod : UserControl, ICodexPoolSettingsGenerator
    {
        public PureRandomMethod()
        {
            InitializeComponent();
        }

        List<Skill> ICodexPoolSettingsGenerator.GenerateCodex(List<Skill> pool)
        {
            return CodexGenerator.PureRandom(pool, (int)__Count.Value);
        }
    }
}
