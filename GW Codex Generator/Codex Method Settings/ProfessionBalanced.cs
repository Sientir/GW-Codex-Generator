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
    public partial class ProfessionBalanced : UserControl, ICodexPoolSettingsGenerator
    {
        public ProfessionBalanced()
        {
            InitializeComponent();
        }

        List<Skill> ICodexPoolSettingsGenerator.GenerateCodex(List<Skill> pool)
        {
            return CodexGenerator.ProfessionBalanced(pool, (int)__Regular.Value, (int)__Elites.Value, (int)__Common.Value);
        }
    }
}
