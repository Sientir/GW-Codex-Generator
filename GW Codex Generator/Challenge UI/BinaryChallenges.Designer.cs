namespace GW_Codex_Generator.Challenge_UI
{
    partial class BinaryChallenges
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.@__IncludeHTML = new System.Windows.Forms.CheckBox();
            this.@__NumberOfRules = new System.Windows.Forms.NumericUpDown();
            this.@__RollRules = new System.Windows.Forms.Button();
            this.@__Rules = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.@__NumberOfRules)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.@__Rules);
            this.groupBox1.Controls.Add(this.@__RollRules);
            this.groupBox1.Controls.Add(this.@__NumberOfRules);
            this.groupBox1.Controls.Add(this.@__IncludeHTML);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(459, 186);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Simple Rule Challenge(s)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Number of rules";
            // 
            // __IncludeHTML
            // 
            this.@__IncludeHTML.AutoSize = true;
            this.@__IncludeHTML.Location = new System.Drawing.Point(299, 22);
            this.@__IncludeHTML.Name = "__IncludeHTML";
            this.@__IncludeHTML.Size = new System.Drawing.Size(152, 17);
            this.@__IncludeHTML.TabIndex = 1;
            this.@__IncludeHTML.Text = "Include in HTML summary.";
            this.@__IncludeHTML.UseVisualStyleBackColor = true;
            // 
            // __NumberOfRules
            // 
            this.@__NumberOfRules.Location = new System.Drawing.Point(102, 19);
            this.@__NumberOfRules.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.@__NumberOfRules.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.@__NumberOfRules.Name = "__NumberOfRules";
            this.@__NumberOfRules.Size = new System.Drawing.Size(55, 20);
            this.@__NumberOfRules.TabIndex = 2;
            this.@__NumberOfRules.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // __RollRules
            // 
            this.@__RollRules.Location = new System.Drawing.Point(163, 16);
            this.@__RollRules.Name = "__RollRules";
            this.@__RollRules.Size = new System.Drawing.Size(75, 23);
            this.@__RollRules.TabIndex = 3;
            this.@__RollRules.Text = "Roll";
            this.@__RollRules.UseVisualStyleBackColor = true;
            this.@__RollRules.Click += new System.EventHandler(this.@__RollRules_Click);
            // 
            // __Rules
            // 
            this.@__Rules.Location = new System.Drawing.Point(6, 45);
            this.@__Rules.Multiline = true;
            this.@__Rules.Name = "__Rules";
            this.@__Rules.ReadOnly = true;
            this.@__Rules.Size = new System.Drawing.Size(445, 132);
            this.@__Rules.TabIndex = 4;
            this.@__Rules.Text = "No rules.";
            // 
            // BinaryChallenges
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "BinaryChallenges";
            this.Size = new System.Drawing.Size(459, 186);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.@__NumberOfRules)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown __NumberOfRules;
        private System.Windows.Forms.CheckBox __IncludeHTML;
        private System.Windows.Forms.TextBox __Rules;
        private System.Windows.Forms.Button __RollRules;
    }
}
