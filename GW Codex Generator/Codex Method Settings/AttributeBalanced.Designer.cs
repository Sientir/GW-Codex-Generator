namespace GW_Codex_Generator.Codex_Method_Settings
{
    partial class AttributeBalanced
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.@__Regular = new System.Windows.Forms.NumericUpDown();
            this.@__Elites = new System.Windows.Forms.NumericUpDown();
            this.@__PvEOnly = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.@__Regular)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.@__Elites)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.@__PvEOnly)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Regular skills per attribute";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Elite skills per attribute";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(124, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "PvE-Only skills (if in pool)";
            // 
            // __Regular
            // 
            this.@__Regular.Location = new System.Drawing.Point(137, 3);
            this.@__Regular.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.@__Regular.Name = "__Regular";
            this.@__Regular.Size = new System.Drawing.Size(120, 20);
            this.@__Regular.TabIndex = 3;
            this.@__Regular.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            // 
            // __Elites
            // 
            this.@__Elites.Location = new System.Drawing.Point(137, 29);
            this.@__Elites.Name = "__Elites";
            this.@__Elites.Size = new System.Drawing.Size(120, 20);
            this.@__Elites.TabIndex = 4;
            this.@__Elites.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // __PvEOnly
            // 
            this.@__PvEOnly.Location = new System.Drawing.Point(137, 55);
            this.@__PvEOnly.Name = "__PvEOnly";
            this.@__PvEOnly.Size = new System.Drawing.Size(120, 20);
            this.@__PvEOnly.TabIndex = 5;
            this.@__PvEOnly.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // AttributeBalanced
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.@__PvEOnly);
            this.Controls.Add(this.@__Elites);
            this.Controls.Add(this.@__Regular);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "AttributeBalanced";
            this.Size = new System.Drawing.Size(262, 81);
            ((System.ComponentModel.ISupportInitialize)(this.@__Regular)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.@__Elites)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.@__PvEOnly)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown __Regular;
        private System.Windows.Forms.NumericUpDown __Elites;
        private System.Windows.Forms.NumericUpDown __PvEOnly;
    }
}
