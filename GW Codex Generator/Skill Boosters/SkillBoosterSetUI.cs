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
        public Skill_UI.SkillInfoDisplay DescriptionBox = null;
        internal List<Pair<Skill, bool>> Skills = new List<Pair<Skill, bool>>();
        private int _Rows = 1;
        private int _IconsPerRow = 1;
        private SolidBrush DarkenBrush = new SolidBrush(Color.FromArgb(192, 0, 0, 0));

        public delegate void UpdateSetDescriptionFunction();
        public UpdateSetDescriptionFunction UpdateSetDescription;

        /// <summary>
        /// Pixel size of icons. Can be adjusted.
        /// </summary>
        public int IconSize = 64;
        public SkillBoosterSetUI()
        {
            InitializeComponent();
            Paint += SkillDisplay_Paint;
            Resize += SkillDisplay_Resize;
            MouseMove += SkillDisplay_MouseMove;
            MouseWheel += SkillDisplay_MouseWheel;
            Click += SkillBoosterSetUI_Click;
            __ScrollBar.ValueChanged += __ScrollBar_ValueChanged;
            foreach(Skill skill in SkillDatabase.Data)
            {
                Skills.Add(new Pair<Skill, bool>(skill, true)); // Default to including all skills in the pool.
            }
            // Let's get some proper sorting going on here!
            Skills.Sort((x, y) => x.first.Compare(y.first));
            CalculateRows();
        }

        internal void SetFromSet(SkillBoosterSet set)
        {
            // I can't help but internally scream at how inefficient this is, but the only "better" idea I have is to
            //  sort all of the skills by ID, set them all to false (instead of the default on), turn them on by indexing in via the ID,
            //  then sort them by Standard method again. I can't even tell if that feels better or not than doing this...
            for (int i = 0; i < Skills.Count; ++i)
            {
                Skills[i].second = set.SetSkills.Contains(Skills[i].first.ID);
            }
            Redraw();
        }

        public void SetAll(bool value)
        {
            for(int i = 0; i < Skills.Count; ++i)
            {
                Skills[i].second = value;
            }
            UpdateSetDescription?.Invoke();
            Redraw();
        }

        internal void SetByCampaign(Skill.Campaigns campaign, bool value)
        {
            for (int i = 0; i < Skills.Count; ++i)
            {
                if (Skills[i].first.Campaign == campaign)
                {
                    Skills[i].second = value;
                }
            }
            UpdateSetDescription?.Invoke();
            Redraw();
        }

        internal void SetByAttribute(Skill.Attributes attribute, bool value)
        {
            for (int i = 0; i < Skills.Count; ++i)
            {
                if (Skills[i].first.Attribute == attribute)
                {
                    Skills[i].second = value;
                }
            }
            UpdateSetDescription?.Invoke();
            Redraw();
        }

        internal void SetByProfession(Skill.Professions profession, bool value)
        {
            for (int i = 0; i < Skills.Count; ++i)
            {
                if (Skills[i].first.Profession == profession)
                {
                    Skills[i].second = value;
                }
            }
            UpdateSetDescription?.Invoke();
            Redraw();
        }

        internal void SetByRating(int rating, bool value)
        {
            for (int i = 0; i < Skills.Count; ++i)
            {
                if (Skills[i].first.Rating == rating)
                {
                    Skills[i].second = value;
                }
            }
            UpdateSetDescription?.Invoke();
            Redraw();
        }

        internal void SetByEliteStatus(bool isElite, bool value)
        {
            for (int i = 0; i < Skills.Count; ++i)
            {
                if (Skills[i].first.IsElite == isElite)
                {
                    Skills[i].second = value;
                }
            }
            UpdateSetDescription?.Invoke();
            Redraw();
        }

        private void SkillBoosterSetUI_Click(object sender, EventArgs e_args)
        {
            MouseEventArgs e = (MouseEventArgs)e_args;
            for (int i = 0; i < Skills.Count; ++i)
            {
                int x = 3 + (i % _IconsPerRow) * (IconSize + 3);
                // Calculating the y positions requires offseting the position by the scrollbar's value.
                //  This effectively moves them up if the scrollbar has been scrolled to a lower position, if that makes sense.
                int y = 3 + (i / _IconsPerRow - __ScrollBar.Value) * (IconSize + 3);

                // Make a rectangle to do a contain test with:
                Rectangle testRect = new Rectangle(x, y, IconSize, IconSize);
                if (testRect.Contains(e.Location))
                {
                    Skills[i].second = !Skills[i].second;
                    // Redraw this image to proper demonstrate its new state:
                    Graphics g = CreateGraphics();
                    Skills[i].first.Draw(g, x, y);
                    if(Skills[i].second == false) // Disabled...
                    {
                        g.FillRectangle(DarkenBrush, testRect);
                    }
                    g.Dispose();
                    UpdateSetDescription?.Invoke();
                    break;
                }
            }
        }

        private void __ScrollBar_ValueChanged(object sender, EventArgs e)
        {
            if (_CalcRowsLock) return; // Don't do this on resizing, as it is redundant.

            Invalidate();
        }

        private void SkillDisplay_MouseWheel(object sender, MouseEventArgs e)
        {
            // Need to pass this on to the scroll bar.

            /*
             Note about the math:

            The scrollbar being positive moves the visible rows "down", so if we scroll up on the wheel, then
            we need to decrease the scrollbar value, and vice versa if we scroll down.
             
             
             */
            if (e.Delta > 0 && __ScrollBar.Value > __ScrollBar.Minimum)
            {
                __ScrollBar.Value--;
            }
            if (e.Delta < 0 && __ScrollBar.Value < __ScrollBar.Maximum)
            {
                __ScrollBar.Value++;
            }
        }

        private void SkillDisplay_MouseMove(object sender, MouseEventArgs e)
        {
            // If the mouse is in the control, check to see if it is over any icons.
            //  Also, only do this if we have a description box!
            if (DescriptionBox != null && ClientRectangle.Contains(e.Location))
            {
                for (int i = 0; i < Skills.Count; ++i)
                {
                    int x = 3 + (i % _IconsPerRow) * (IconSize + 3);
                    // Calculating the y positions requires offseting the position by the scrollbar's value.
                    //  This effectively moves them up if the scrollbar has been scrolled to a lower position, if that makes sense.
                    int y = 3 + (i / _IconsPerRow - __ScrollBar.Value) * (IconSize + 3);

                    // Make a rectangle to do a contain test with:
                    Rectangle testRect = new Rectangle(x, y, IconSize, IconSize);
                    if (testRect.Contains(e.Location))
                    {
                        DescriptionBox.DrawSkill(Skills[i].first);
                        break;
                    }
                }
            }
        }

        private void SkillDisplay_Resize(object sender, EventArgs e)
        {
            CalculateRows();
            Redraw();
        }

        private void SkillDisplay_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(BackColor);

            // Calculate starting position.
            int firstRow = __ScrollBar.Value; // 0 to something...

            int x = 0;
            int y = 3; // We won't be drawing any rows hidden off the top, but we will draw rows hidden off the bottom incase one of them is partial.
            for (int i = firstRow * _IconsPerRow; i < Skills.Count; ++i)
            {
                Skills[i].first.Draw(e.Graphics, 3 + x * (IconSize + 3), y);
                if(Skills[i].second == false) // Not select, then draw a dark box over it.
                {
                    e.Graphics.FillRectangle(DarkenBrush, 3 + x * (IconSize + 3), y, IconSize, IconSize);
                }
                // Advance a slot. If necessary, go to a new row.
                ++x;
                if (x >= _IconsPerRow)
                {
                    x = 0;
                    y += IconSize + 3;
                }
            }
        }

        public void Redraw()
        {
            Invalidate();
        }

        bool _CalcRowsLock = false;
        public void CalculateRows()
        {
            _CalcRowsLock = true;
            // If there aren't any skills, don't try to pretend there are!
            if (Skills.Count == 0)
            {
                _IconsPerRow = 0;
                _Rows = 0;
                __ScrollBar.Maximum = 0;
                _CalcRowsLock = false;
                return;
            }

            _IconsPerRow = (ClientSize.Width - __ScrollBar.Width - 3) / (IconSize + 3); // The 3's are for buffer! The first 3 is leading buffer.
            _Rows = Skills.Count / _IconsPerRow;
            if (Skills.Count % _IconsPerRow != 0) ++_Rows; // We need an additional row for the remainders.

            // The scrollbar will need to allow us to see rows hidden, so figure out how many are obscured and set the maximum to that.
            int visibleRows = (ClientSize.Height - 3) / (IconSize + 3);
            if (visibleRows < 1) visibleRows = 1; // We should be able to always see at least one row, right?
            int obscuredRows = Math.Max(_Rows - visibleRows, 0);
            __ScrollBar.Maximum = obscuredRows;
            __ScrollBar.Value = 0; // Reset it incase that's something I want to do...
            __ScrollBar.Visible = __ScrollBar.Maximum > 0; // Only show this if we have obscured rows, I guess?
            _CalcRowsLock = false;
        }
    }
}
