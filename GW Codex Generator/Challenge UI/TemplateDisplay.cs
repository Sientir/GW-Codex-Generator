using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GW_Codex_Generator.Challenge_UI
{
    public partial class TemplateDisplay : UserControl
    {
        public TemplateDisplay()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Sets the information in the control from a template information string.
        /// </summary>
        /// <param name="str">The template information string.</param>
        public void SetTemplateInformation(string str)
        {
            string[] info = str.Split('|');
            __TemplateName.Text = info[0];
            __TemplateCode.Text = info[1];
        }

        private void __CopyTemplateCode_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(__TemplateCode.Text);
        }

        public override string ToString()
        {
            return __TemplateName.Text + ": " + __TemplateCode.Text;
        }
    }
}
