using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GW_Codex_Generator.Template_Draft
{
    public partial class TemplateDraftDeckViewer : Form
    {
        public TemplateDraftDeckViewer()
        {
            InitializeComponent();
            LoadTemplateDeck();
        }

        public void LoadTemplateDeck()
        {
            __Contents.Controls.Clear();

            int y = 3;
            foreach(string tde in SkillDatabase.TemplateDeck)
            {
                Challenge_UI.TemplateDisplay td = new Challenge_UI.TemplateDisplay();
                td.Location = new Point(3, y);
                y += td.Height + 3;

                __Contents.Controls.Add(td);

                td.SkillInfoDisplay = __SkillInfo;
                td.SetTemplateInformation(tde);
            }
        }
    }
}
