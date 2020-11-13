namespace GW_Codex_Generator.Template_Draft
{
    partial class TemplateDraftUI
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startDraftRoundToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setPartySizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.@__PartySize4 = new System.Windows.Forms.ToolStripMenuItem();
            this.@__PartySize6 = new System.Windows.Forms.ToolStripMenuItem();
            this.@__PartySize8 = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.@__DraftHandContainer = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.@__PoolContainer = new System.Windows.Forms.Panel();
            this.saveDraftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadDraftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.@__OpenTemplateDraftDialog = new System.Windows.Forms.OpenFileDialog();
            this.@__SaveTemplateDraftDialog = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.startDraftRoundToolStripMenuItem,
            this.setPartySizeToolStripMenuItem,
            this.saveDraftToolStripMenuItem,
            this.loadDraftToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(793, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(72, 20);
            this.newToolStripMenuItem.Text = "New Draft";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // startDraftRoundToolStripMenuItem
            // 
            this.startDraftRoundToolStripMenuItem.Name = "startDraftRoundToolStripMenuItem";
            this.startDraftRoundToolStripMenuItem.Size = new System.Drawing.Size(110, 20);
            this.startDraftRoundToolStripMenuItem.Text = "Start Draft Round";
            this.startDraftRoundToolStripMenuItem.Click += new System.EventHandler(this.startDraftRoundToolStripMenuItem_Click);
            // 
            // setPartySizeToolStripMenuItem
            // 
            this.setPartySizeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.@__PartySize4,
            this.@__PartySize6,
            this.@__PartySize8});
            this.setPartySizeToolStripMenuItem.Name = "setPartySizeToolStripMenuItem";
            this.setPartySizeToolStripMenuItem.Size = new System.Drawing.Size(88, 20);
            this.setPartySizeToolStripMenuItem.Text = "Set Party Size";
            // 
            // __PartySize4
            // 
            this.@__PartySize4.Name = "__PartySize4";
            this.@__PartySize4.Size = new System.Drawing.Size(80, 22);
            this.@__PartySize4.Text = "4";
            this.@__PartySize4.Click += new System.EventHandler(this.@__PartySize4_Click);
            // 
            // __PartySize6
            // 
            this.@__PartySize6.Name = "__PartySize6";
            this.@__PartySize6.Size = new System.Drawing.Size(80, 22);
            this.@__PartySize6.Text = "6";
            this.@__PartySize6.Click += new System.EventHandler(this.@__PartySize6_Click);
            // 
            // __PartySize8
            // 
            this.@__PartySize8.Checked = true;
            this.@__PartySize8.CheckState = System.Windows.Forms.CheckState.Checked;
            this.@__PartySize8.Name = "__PartySize8";
            this.@__PartySize8.Size = new System.Drawing.Size(80, 22);
            this.@__PartySize8.Text = "8";
            this.@__PartySize8.Click += new System.EventHandler(this.@__PartySize8_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Size = new System.Drawing.Size(793, 675);
            this.splitContainer1.SplitterDistance = 259;
            this.splitContainer1.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.@__DraftHandContainer);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(793, 259);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Draft Hand";
            // 
            // __DraftHandContainer
            // 
            this.@__DraftHandContainer.AutoScroll = true;
            this.@__DraftHandContainer.BackColor = System.Drawing.SystemColors.ControlDark;
            this.@__DraftHandContainer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.@__DraftHandContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.@__DraftHandContainer.Location = new System.Drawing.Point(3, 16);
            this.@__DraftHandContainer.Name = "__DraftHandContainer";
            this.@__DraftHandContainer.Size = new System.Drawing.Size(787, 240);
            this.@__DraftHandContainer.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.@__PoolContainer);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(793, 412);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Drafted Pool";
            // 
            // __PoolContainer
            // 
            this.@__PoolContainer.AutoScroll = true;
            this.@__PoolContainer.BackColor = System.Drawing.SystemColors.ControlDark;
            this.@__PoolContainer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.@__PoolContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.@__PoolContainer.Location = new System.Drawing.Point(3, 16);
            this.@__PoolContainer.Name = "__PoolContainer";
            this.@__PoolContainer.Size = new System.Drawing.Size(787, 393);
            this.@__PoolContainer.TabIndex = 0;
            // 
            // saveDraftToolStripMenuItem
            // 
            this.saveDraftToolStripMenuItem.Name = "saveDraftToolStripMenuItem";
            this.saveDraftToolStripMenuItem.Size = new System.Drawing.Size(81, 20);
            this.saveDraftToolStripMenuItem.Text = "Save Draft...";
            this.saveDraftToolStripMenuItem.Click += new System.EventHandler(this.saveDraftToolStripMenuItem_Click);
            // 
            // loadDraftToolStripMenuItem
            // 
            this.loadDraftToolStripMenuItem.Name = "loadDraftToolStripMenuItem";
            this.loadDraftToolStripMenuItem.Size = new System.Drawing.Size(83, 20);
            this.loadDraftToolStripMenuItem.Text = "Load Draft...";
            this.loadDraftToolStripMenuItem.Click += new System.EventHandler(this.loadDraftToolStripMenuItem_Click);
            // 
            // __OpenTemplateDraftDialog
            // 
            this.@__OpenTemplateDraftDialog.DefaultExt = "gwtd";
            this.@__OpenTemplateDraftDialog.Filter = "Guild Wars Template Draft|*.gwtd";
            this.@__OpenTemplateDraftDialog.Title = "Open Template Draft...";
            this.@__OpenTemplateDraftDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.@__OpenTemplateDraftDialog_FileOk);
            // 
            // __SaveTemplateDraftDialog
            // 
            this.@__SaveTemplateDraftDialog.DefaultExt = "gwtd";
            this.@__SaveTemplateDraftDialog.Filter = "Guild Wars Template Draft|*.gwtd";
            this.@__SaveTemplateDraftDialog.Title = "Save Template Draft...";
            this.@__SaveTemplateDraftDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.@__SaveTemplateDraftDialog_FileOk);
            // 
            // TemplateDraftUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.Name = "TemplateDraftUI";
            this.Size = new System.Drawing.Size(793, 699);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem startDraftRoundToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setPartySizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem __PartySize4;
        private System.Windows.Forms.ToolStripMenuItem __PartySize6;
        private System.Windows.Forms.ToolStripMenuItem __PartySize8;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel __DraftHandContainer;
        private System.Windows.Forms.Panel __PoolContainer;
        private System.Windows.Forms.ToolStripMenuItem saveDraftToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadDraftToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog __OpenTemplateDraftDialog;
        private System.Windows.Forms.SaveFileDialog __SaveTemplateDraftDialog;
    }
}
