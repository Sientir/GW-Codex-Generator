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
    public partial class SkillValueSetter : Form
    {
        internal delegate void ModifySkill(int skill_index, int value);

        private ModifySkill ModifySkillFunction;
        private string TitleName = "";

        /// <summary>
        /// The index of the skill currently being evaluated.
        /// </summary>
        public int SkillIndex { get; private set; }

        /// <summary>
        /// Whether or not all skills have been processed!
        /// </summary>
        public bool Done { get { return SkillIndex >= SkillDatabase.Data.Count; } }
        public SkillValueSetter()
        {
            InitializeComponent();
            Resize += SkillValueSetter_Resize;
        }

        private void SkillValueSetter_Resize(object sender, EventArgs e)
        {
            // The Math.Max() calls set a minimum possible size!
            __EvaluationGroupbox.Height = Math.Max(ClientSize.Height - __EvaluationGroupbox.Top - __EvaluationGroupbox.Left, 30);
            __EvaluationGroupbox.Width = Math.Max(ClientSize.Width - 2 * __EvaluationGroupbox.Width, 100);
            __SkillInformation.Width = Math.Max(ClientSize.Width - __SkillInformation.Left - __SkillInformation.Top, 112-85); // Math for minimum should result in this control ending at the same place as the group box.
        }

        static internal SkillValueSetter Create(string evaluationName, ModifySkill SkillUpdateFunction, string[] ButtonLabels, int startingSkillIndex)
        {
            SkillValueSetter svs = new SkillValueSetter();

            svs.LoadSkill(startingSkillIndex);
            svs.SetEvaluationName(evaluationName);
            svs.ModifySkillFunction = SkillUpdateFunction;
            svs.CreateButtons(ButtonLabels);

            return svs;
        }

        void CreateButtons(string[] buttonLabels)
        {
            __EvaluationButtonsHolder.Controls.Clear();
            for(int i = 0; i < buttonLabels.Length; ++i)
            {
                Button b = new Button();
                b.Text = buttonLabels[i];
                b.Tag = i;
                b.Click += ValueButton_Click;
                __EvaluationButtonsHolder.Controls.Add(b);
            }
        }

        private void ValueButton_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            ModifySkillFunction(SkillIndex, (int)b.Tag);
            LoadSkill(SkillIndex + 1); // Load the next skill, of course!
        }

        void SetEvaluationName(string name)
        {
            __EvaluationGroupbox.Text = "Evaluate Skills for " + name;
            TitleName = name;
            UpdateTitle();
        }

        void UpdateTitle()
        {
            Text = "Evaluating Skills for " + TitleName + " (" + (SkillIndex + 1).ToString() + " of " + SkillDatabase.Data.Count.ToString() + ")";
        }

        void LoadSkill(int index)
        {
            SkillIndex = index;
            if (Done) // If we're done, we should indicate as such and close the form!
            {
                MessageBox.Show("Last skill evaluated!", "Job's Done!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
                return; // Make sure I don't go out of bounds, ya'hear?
            }

            UpdateTitle();

            // Load information about the skill!
            __SkillIcon.Image = SkillDatabase.Data[index].Icon;
            __SkillInformation.DrawSkill(SkillDatabase.Data[index]);
        }

    }
}
