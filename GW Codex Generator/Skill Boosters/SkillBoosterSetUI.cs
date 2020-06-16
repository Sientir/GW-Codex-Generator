using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GW_Codex_Generator.Skill_Boosters
{
    public partial class SkillBoosterSetUI : UserControl
    {
        SkillBoosterSet CurrentSet = null;

        public event EventHandler<string> UpdateName;
        public SkillBoosterSetUI()
        {
            InitializeComponent();
        }

        private void __SaveSetButton_Click(object sender, EventArgs e)
        {
            if (System.IO.Directory.Exists("\\GW Codex Skill Sets") == false)
            {
                System.IO.Directory.CreateDirectory("\\GW Codex Skill Sets");
            }

            try
            {
                CurrentSet.Save("\\GW Codex Skill Sets\\" + CurrentSet.Name + ".gwset");
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error Saving file. Did you include illegal characters for a filename?" + Environment.NewLine + Environment.NewLine + "Error message: " + ex.Message, "Error Saving Set", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            UpdateName?.Invoke(CurrentSet, CurrentSet.Name);
        }
    }
}
