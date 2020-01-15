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
