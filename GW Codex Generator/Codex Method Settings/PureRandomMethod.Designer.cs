namespace GW_Codex_Generator.Codex_Method_Settings
{
    partial class PureRandomMethod
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
            this.@__Count = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.@__Count)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Number of skills to select";
            // 
            // __Count
            // 
            this.@__Count.Location = new System.Drawing.Point(133, 3);
            this.@__Count.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.@__Count.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.@__Count.Name = "__Count";
            this.@__Count.Size = new System.Drawing.Size(120, 20);
            this.@__Count.TabIndex = 1;
            this.@__Count.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            // 
            // PureRandomMethod
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.@__Count);
            this.Controls.Add(this.label1);
            this.Name = "PureRandomMethod";
            this.Size = new System.Drawing.Size(255, 24);
            ((System.ComponentModel.ISupportInitialize)(this.@__Count)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown __Count;
    }
}
