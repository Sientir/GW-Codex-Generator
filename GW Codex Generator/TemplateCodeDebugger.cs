using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GW_Codex_Generator
{
    public partial class TemplateCodeDebugger : UserControl
    {
        public TemplateCodeDebugger()
        {
            InitializeComponent();

            __TemplateCode.TextChanged += __TemplateCode_TextChanged;
        }

        private void __TemplateCode_TextChanged(object sender, EventArgs e)
        {
            SkillDatabase.TemplateInformation reader = new SkillDatabase.TemplateInformation();
            __TemplateInformation.Text = reader.DebugTemplateCode(__TemplateCode.Text);
        }
    }
}
