using System;
using System.Drawing;
using System.Windows.Forms;

namespace GW_Codex_Generator.Skill_UI
{
    public partial class SkillInfoDisplay : UserControl
    {
        Skill CurrentSkill = null;

        Font NameFont = null;
        public SkillInfoDisplay()
        {
            InitializeComponent();
            Resize += SkillInfoDisplay_Resize;
            Paint += SkillInfoDisplay_Paint;

            NameFont = new Font(Font.FontFamily, Font.Size * 1.25f, FontStyle.Bold);
        }

        private void SkillInfoDisplay_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(BackColor);
            if (CurrentSkill == null) return;

            SolidBrush TextColor = new SolidBrush(ForeColor);

            float fontHeight = e.Graphics.MeasureString("1l", Font).Height;

            // Step 1: Draw the name.
            float y = Padding.Top;
            float x = Padding.Left;
            SizeF NameSize = e.Graphics.MeasureString(CurrentSkill.Name, NameFont);

            e.Graphics.DrawString(CurrentSkill.Name + " (" + CurrentSkill.AttributeName + "—Rating: " + CurrentSkill.Rating.ToString() + " | Rarity: " + CurrentSkill.GetRarityLabel() + ")", NameFont, TextColor, x, y);

            y += NameSize.Height + 3;

            // Step 2: Draw costs.
            
            #region Draw Costs...

            bool renderedACost = false;

            if (CurrentSkill.Cost_Maintainence.Length > 0) // Upkeep
            {
                SizeF size = e.Graphics.MeasureString(CurrentSkill.Cost_Maintainence, Font);

                e.Graphics.DrawString(CurrentSkill.Cost_Maintainence, Font, TextColor, x, y);
                x += size.Width + 2; // Buffer...
                e.Graphics.DrawImage(Properties.Resources.Cost_Upkeep, x, y);
                x += Properties.Resources.Cost_Upkeep.Width + 5;
                renderedACost = true;
            }
            if (CurrentSkill.Cost_Sacrifice.Length > 0) // HP sacrifice
            {
                SizeF size = e.Graphics.MeasureString(CurrentSkill.Cost_Sacrifice, Font);

                e.Graphics.DrawString(CurrentSkill.Cost_Sacrifice, Font, TextColor, x, y);
                x += size.Width + 2; // Buffer...
                e.Graphics.DrawImage(Properties.Resources.Cost_Sacrifice, x, y);
                x += Properties.Resources.Cost_Sacrifice.Width + 5;
                renderedACost = true;
            }
            if (CurrentSkill.Cost_Overcast.Length > 0) // Overcast
            {
                SizeF size = e.Graphics.MeasureString(CurrentSkill.Cost_Overcast, Font);

                e.Graphics.DrawString(CurrentSkill.Cost_Overcast, Font, TextColor, x, y);
                x += size.Width + 2; // Buffer...
                e.Graphics.DrawImage(Properties.Resources.Cost_Overcast, x, y);
                x += Properties.Resources.Cost_Overcast.Width + 5;
                renderedACost = true;
            }
            if (CurrentSkill.Cost_Adrenaline.Length > 0) // Adrenaline
            {
                SizeF size = e.Graphics.MeasureString(CurrentSkill.Cost_Adrenaline, Font);

                e.Graphics.DrawString(CurrentSkill.Cost_Adrenaline, Font, TextColor, x, y);
                x += size.Width + 2; // Buffer...
                e.Graphics.DrawImage(Properties.Resources.Cost_Adrenaline, x, y);
                x += Properties.Resources.Cost_Adrenaline.Width + 5;
                renderedACost = true;
            }
            if (CurrentSkill.Cost_Energy.Length > 0) // Energy
            {
                SizeF size = e.Graphics.MeasureString(CurrentSkill.Cost_Energy, Font);

                e.Graphics.DrawString(CurrentSkill.Cost_Energy, Font, TextColor, x, y);
                x += size.Width + 2; // Buffer...
                e.Graphics.DrawImage(Properties.Resources.Cost_Energy, x, y);
                x += Properties.Resources.Cost_Energy.Width + 5;
                renderedACost = true;
            }
            if (CurrentSkill.Cost_CastTime.Length > 0) // Cast time
            {
                SizeF size = e.Graphics.MeasureString(CurrentSkill.Cost_CastTime, Font);

                e.Graphics.DrawString(CurrentSkill.Cost_CastTime, Font, TextColor, x, y);
                x += size.Width + 2; // Buffer...
                e.Graphics.DrawImage(Properties.Resources.Cost_Activation, x, y);
                x += Properties.Resources.Cost_Activation.Width + 5;
                renderedACost = true;
            }
            if (CurrentSkill.Cost_Recharge.Length > 0) // Recharge...HAS to come after upkeep so I don't get a false match!
            {
                SizeF size = e.Graphics.MeasureString(CurrentSkill.Cost_Recharge, Font);

                e.Graphics.DrawString(CurrentSkill.Cost_Recharge, Font, TextColor, x, y);
                x += size.Width + 2; // Buffer...
                e.Graphics.DrawImage(Properties.Resources.Cost_Recharge, x, y);
                x += Properties.Resources.Cost_Recharge.Width + 5;
                renderedACost = true;
            }

            if (renderedACost == false)
            {
                e.Graphics.DrawString("No costs?!", Font, TextColor, x, y);
                y += fontHeight + 3;
            }
            #endregion

            x = Padding.Left;
            y += fontHeight * 0.5f + 20; // The cost icons are all 20 pixels tall, so I want to make sure I give enough room for them!

            // Step 3: Draw description.
            e.Graphics.DrawString(CurrentSkill.Description, Font, TextColor, new RectangleF(x, y, ClientSize.Width - Padding.Horizontal, ClientSize.Height - y - Padding.Bottom));
        }


        private void SkillInfoDisplay_Resize(object sender, EventArgs e)
        {
            Invalidate();
        }

        internal void DrawSkill(Skill skill)
        {
            CurrentSkill = skill;
            Invalidate();
        }
    }
}
