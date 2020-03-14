using System.Collections.Generic;
using System.Windows.Forms;

namespace GW_Codex_Generator.Codex_Method_Settings
{
    public partial class ByRating : UserControl, ICodexPoolSettingsGenerator
    {
        public ByRating()
        {
            InitializeComponent();
        }

        List<Skill> ICodexPoolSettingsGenerator.GenerateCodex(List<Skill> pool)
        {
            if (__Rating1.Value + __Rating2.Value + __Rating3.Value + __Rating4.Value + __Rating5.Value == 0)
            {
                MessageBox.Show("You've selected to have 0 skills in the codex! Generation aborted.", "Warning: Empty Codex", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return new List<Skill>();
            }
            return CodexGenerator.ByRating(pool, (int)__Rating1.Value, (int)__Rating2.Value, (int)__Rating3.Value, (int)__Rating4.Value, (int)__Rating5.Value, false);
        }
    }
}
