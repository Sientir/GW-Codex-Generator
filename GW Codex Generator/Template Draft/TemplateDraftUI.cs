using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GW_Codex_Generator.Challenge_UI;
using System.IO;

namespace GW_Codex_Generator.Template_Draft
{
    public partial class TemplateDraftUI : UserControl
    {
        /*
         See: Wooden Potatoes monster draft.

        The idea is you get presented 3 templates for each character slot. You pick one and pass on the other two (they'll
        get put back into the "deck"--the pool of available templates--at the end of the draft round).

        The drafted templates get put into your current selection pool, then you pick N templates from that pool to use.

        Before the next round, you put two templates back into the pool, then the remaining N-2 templates you used get tossed
        to the winds. Presumably, they get shuffled back into the deck if the deck runs empty.

        Then you do another round of drafting.
     

        Needs:
        - Save, Load, and New draft buttons
        - Control for number of picks in the draft round. (needs to be able to save hand quantity and current draft hand number.)
        - Current Pool
	        - Ability to mark templates in pool to save
        - Current draft hand
	        - Ability to make choices here
        - Button to start draft round
             */
        public Skill_UI.SkillInfoDisplay SkillInfoDisplay = null;
        List<Control> PoolTemplates = new List<Control>();
        List<Control> PoolButtons = new List<Control>();
        List<Control> DraftHandTemplates = new List<Control>();
        List<Control> DraftHandButtons = new List<Control>();
        TemplateDraft Draft = null;
        int PartySize = 8;
        int DraftHandCount = 0;
        int SavedFromPoolFirst = -1;
        int SavedFromPoolSecond = -1;
        const int DraftHandSize = 3;
        public TemplateDraftUI()
        {
            InitializeComponent();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Start a Template Draft using the currently loaded deck, \"" + SkillDatabase.TemplateDeckName + "\"?" + Environment.NewLine + Environment.NewLine + "This draft will always use this deck, even if you change it later. Decks can be selected in Settings tab. It will also override the current draft, if one is ongoing. Make sure to save it first if you wish to continue it later!", "Start Draft?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // Start a new actual draft:


                Draft = TemplateDraft.Create();

                // Clear the contents of the old draft:
                __PoolContainer.Controls.Clear();
                PoolTemplates.Clear();
                PoolButtons.Clear();
                __DraftHandContainer.Controls.Clear();
                DraftHandButtons.Clear();
                DraftHandTemplates.Clear();

                // Reset draft control values:
                DraftHandCount = 0;
                SavedFromPoolFirst = SavedFromPoolSecond = -1;

                // Re-enable some controls if in the middle of a draft round (where they get turned off):
                startDraftRoundToolStripMenuItem.Enabled = setPartySizeToolStripMenuItem.Enabled = __PartySize4.Enabled = __PartySize6.Enabled = __PartySize8.Enabled = true;

                // Note: Not touching party size here, as that's connected to an entirely separate thing!
            }
        }

        private void __PartySize4_Click(object sender, EventArgs e)
        {
            PartySize = 4;
            __PartySize4.Checked = true;
            __PartySize6.Checked = false;
            __PartySize8.Checked = false;
        }

        private void __PartySize6_Click(object sender, EventArgs e)
        {
            PartySize = 6;
            __PartySize4.Checked = false;
            __PartySize6.Checked = true;
            __PartySize8.Checked = false;
        }

        private void __PartySize8_Click(object sender, EventArgs e)
        {
            PartySize = 8;
            __PartySize4.Checked = false;
            __PartySize6.Checked = false;
            __PartySize8.Checked = true;
        }

        private void startDraftRoundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // In case someone tries to do a draft without creating a new one... (Note: Need to do it here because of loading order among controls!)
            if (Draft == null)
            {
                Draft = TemplateDraft.Create();
            }

            // Turn off the ability to change the party size while the draft round is going on:
            startDraftRoundToolStripMenuItem.Enabled = setPartySizeToolStripMenuItem.Enabled = __PartySize4.Enabled = __PartySize6.Enabled = __PartySize8.Enabled = false;
            Draft.StartDraftRound(SavedFromPoolFirst, SavedFromPoolSecond);

            // Add code to re-add the saved items...
            List<string> savedTemplates = Draft.GetPool();
            __PoolContainer.Controls.Clear();
            PoolButtons.Clear();
            PoolTemplates.Clear();
            SavedFromPoolFirst = -1;
            SavedFromPoolSecond = -1;
            DraftHandCount = 0;
            for (int i = 0; i < savedTemplates.Count; ++i)
            {
                AddTemplateToPool(savedTemplates[i], i);
            }

            // Now, let's draft a hand, shall we?
            DisplayDraftHand(Draft.DealDraftHand(DraftHandSize));
        }

        private void DisplayDraftHand(List<string> hand)
        {
            // Clear the current set of draft hand stuff:
            __DraftHandContainer.Controls.Clear();
            DraftHandButtons.Clear();
            DraftHandTemplates.Clear();

            // OK, let's make some stuff happen!
            int y = 3;
            for (int i = 0; i < hand.Count; ++i)
            {
                // Set up the template display for this draft hand:
                TemplateDisplay td = new TemplateDisplay();
                td.SetTemplateInformation(hand[i]);
                td.Location = new Point(3, y);
                td.SkillInfoDisplay = SkillInfoDisplay;

                DraftHandTemplates.Add(td);
                __DraftHandContainer.Controls.Add(td);

                // Now add the pick buttons:
                Button b = new Button();
                b.Text = "Pick!";
                b.Height = td.Height;
                b.Location = new Point(td.Right + 3, y);
                b.Tag = i;
                b.Click += PickTemplate_Click;
                b.BackColor = SystemColors.Control;
                DraftHandButtons.Add(b);
                __DraftHandContainer.Controls.Add(b);

                // Increment y:
                y += 3 + td.Height;
            }
        }

        private void PickTemplate_Click(object sender, EventArgs e)
        {
            // OK, so we've picked an item!
            Button b = (Button)sender;
            int id = (int)b.Tag;

            // Now, let's pick it!
            AddTemplateToPool(Draft.MakeDraftPick(id), PoolTemplates.Count); // Adding a new one, yes? Hopefully this doesn't break with scrolling, but it might!

            // Now that we've picked it, we need to ask a very important question: Was that the last pick of this round?
            ++DraftHandCount;
            if (DraftHandCount < PartySize) // At least one more hand to draft.
            {
                DisplayDraftHand(Draft.DealDraftHand(DraftHandSize));
            }
            else // Done drafting hands!
            {
                // To finish the drafting portion, we need to do several things. Let's start by clearing out the hand:
                __DraftHandContainer.Controls.Clear();
                DraftHandButtons.Clear();
                DraftHandTemplates.Clear();

                // Now we need to enable and make visible the buttons in the pool for saving up to two items:
                for (int i = 0; i < PoolButtons.Count; ++i)
                {
                    PoolButtons[i].Enabled = true;
                    PoolButtons[i].Visible = true;
                    PoolButtons[i].Top = PoolTemplates[i].Top; // Reposition them in case scrolling has happened, since invisible controls seem to get...lost.
                }

                // Next, tell the draft the round is done:
                Draft.EndDraftRound(); // This is important for re-integrating set-aside templates into the deck!

                // Finally, we need to re-enable party-size controls and the control that starts the drafting round:
                startDraftRoundToolStripMenuItem.Enabled = setPartySizeToolStripMenuItem.Enabled = __PartySize4.Enabled = __PartySize6.Enabled = __PartySize8.Enabled = true;
            }
        }

        private void AddTemplateToPool(string template, int id)
        {
            int x = 3;
            int y = 3;

            // Set up the template display:
            TemplateDisplay td = new TemplateDisplay();
            td.SetTemplateInformation(template);

            if (id > 0)
            {
                y = PoolTemplates[id - 1].Bottom + 3;
            }
            td.Location = new Point(x, y);
            td.SkillInfoDisplay = SkillInfoDisplay;

            PoolTemplates.Add(td);
            __PoolContainer.Controls.Add(td);

            // Set up the button display for the template:
            Button b = new Button();
            b.Text = "Save";
            b.Visible = false;
            b.Enabled = false;
            b.Tag = id;
            b.Click += SaveTemplateInPool_Click;
            x += 3 + td.Width;
            b.Location = new Point(x, y);
            b.Height = td.Height;
            b.BackColor = SystemColors.Control;

            PoolButtons.Add(b);
            __PoolContainer.Controls.Add(b);
        }

        private void SaveTemplateInPool_Click(object sender, EventArgs e)
        {
            // Grab some info that needs to be cast into usability:
            Button b = (Button)sender;
            int id = (int)b.Tag;

            // We need to determine a few things. First, is this being toggled off?
            if (id == SavedFromPoolFirst)
            {
                SavedFromPoolFirst = -1;
                b.BackColor = SystemColors.Control;
                b.Text = "Save";
                // Need to re-enable buttons, since we turn off buttons if we've got two saved:
                foreach (Control c in PoolButtons) c.Enabled = true;
                return;
            }
            if (id == SavedFromPoolSecond)
            {
                SavedFromPoolSecond = -1;
                b.BackColor = SystemColors.Control;
                b.Text = "Save";
                // Need to re-enable buttons, since we turn off buttons if we've got two saved:
                foreach (Control c in PoolButtons) c.Enabled = true;
                return;
            }

            // OK, the buttons aren't the first or second! That means we're saving this as one of our two templates.
            b.BackColor = SystemColors.Highlight;
            b.Text = "Saved";

            // Save this template to an empty slot:
            if (SavedFromPoolFirst < 0) SavedFromPoolFirst = id;
            else SavedFromPoolSecond = id;

            // If both slots are full, disable the non-saving buttons:
            if (SavedFromPoolFirst >= 0 && SavedFromPoolSecond >= 0)
            {
                foreach (Control c in PoolButtons)
                {
                    if (c.BackColor != SystemColors.Highlight) c.Enabled = false;
                }
            }
        }

        private void saveDraftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            __SaveTemplateDraftDialog.ShowDialog();
        }

        private void loadDraftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("The current draft will be unloaded and any unsaved progress will be lost." + Environment.NewLine + Environment.NewLine + "Are you sure you want to continue?", "Confirm Operation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                __OpenTemplateDraftDialog.ShowDialog();
            }
        }

        private void __OpenTemplateDraftDialog_FileOk(object sender, CancelEventArgs e)
        {
            LoadDraft(__OpenTemplateDraftDialog.FileName);
        }

        private void __SaveTemplateDraftDialog_FileOk(object sender, CancelEventArgs e)
        {
            SaveDraft(__SaveTemplateDraftDialog.FileName);
        }

        void SaveDraft(string filename)
        {
            int VERSION = 0;

            BinaryWriter output = new BinaryWriter(File.Create(filename));

            // Start with the version number:
            output.Write(VERSION);
            output.Write(DraftHandSize); // Yes, this is a constant, but I want to backwards account for if I want to make it an option to change in the future.

            // Let's start with this, so that we have all of the core, underlying data loaded in first on the load phase:
            Draft.Write(output);

            // Now, let's record whether or not we are in the middle of drafting:
            output.Write(startDraftRoundToolStripMenuItem.Enabled == false); // If this is false, then we're in a draft round!

            // Now to record all of the tracking variables:
            output.Write(PartySize);
            output.Write(DraftHandCount);
            output.Write(SavedFromPoolFirst);
            output.Write(SavedFromPoolSecond);

            // And that's it!
            output.Close();
        }

        void LoadDraft(string filename)
        {
            BinaryReader input = new BinaryReader(File.OpenRead(filename));
            int version = input.ReadInt32(); // Needed for if a new version needs to happen. ALWAYS include a version number! It'll save headaches in the future if needed!
            int handsize = input.ReadInt32(); // Don't care about this right now...here for ease of backwards compatibility in a theoretical future.

            // OK, let's clear some stuff out:
            __PoolContainer.Controls.Clear();
            PoolTemplates.Clear();
            PoolButtons.Clear();
            __DraftHandContainer.Controls.Clear();
            DraftHandButtons.Clear();
            DraftHandTemplates.Clear();
            startDraftRoundToolStripMenuItem.Enabled = setPartySizeToolStripMenuItem.Enabled = __PartySize4.Enabled = __PartySize6.Enabled = __PartySize8.Enabled = true;

            // Now, let's load the draft:
            Draft = TemplateDraft.Read(input);

            // Now, load in these values:
            bool MidDraft = input.ReadBoolean();

            // And load in the tracking values:
            PartySize = input.ReadInt32();
            DraftHandCount = input.ReadInt32();
            SavedFromPoolFirst = input.ReadInt32();
            SavedFromPoolSecond = input.ReadInt32();

            // OK, we've loaded everything, so we can close the stream:
            input.Close();

            // Now we need to set up the UI according to how things were when it was saved!

            // First, let's set the party size on the UI:
            __PartySize4.Checked = PartySize == 4;
            __PartySize6.Checked = PartySize == 6;
            __PartySize8.Checked = PartySize == 8;

            // Next, let's load in the pool:
            List<string> pool = Draft.GetPool();
            for (int i = 0; i < pool.Count; ++i)
            {
                AddTemplateToPool(pool[i], i);
            }

            // Let's set up "save" values:
            if (SavedFromPoolFirst >= 0)
            {
                PoolButtons[SavedFromPoolFirst].Text = "Saved";
                PoolButtons[SavedFromPoolFirst].BackColor = SystemColors.Highlight;
            }
            if (SavedFromPoolSecond >= 0)
            {
                PoolButtons[SavedFromPoolSecond].Text = "Saved";
                PoolButtons[SavedFromPoolSecond].BackColor = SystemColors.Highlight;
            }

            // If both slots are full, disable the non-saving buttons:
            if (SavedFromPoolFirst >= 0 && SavedFromPoolSecond >= 0)
            {
                foreach (Control c in PoolButtons)
                {
                    // Enable the highlighted ones and disable the rest:
                    c.Enabled = c.BackColor == SystemColors.Highlight;
                }
            }
            else
            {
                foreach(Control c in PoolButtons)
                {
                    c.Enabled = true;
                }
            }

            // Now, let's set up the draft if we're in the middle of it!
            if (MidDraft)
            {
                startDraftRoundToolStripMenuItem.Enabled = setPartySizeToolStripMenuItem.Enabled = __PartySize4.Enabled = __PartySize6.Enabled = __PartySize8.Enabled = false;
                DisplayDraftHand(Draft.GetDraftHand());
            }
            else // Not drafting? Gotta show those save buttons!
            {
                foreach (Control c in PoolButtons) c.Visible = true;
            }

            MessageBox.Show("Finished loading Template Draft using deck \"" + Draft.GetDeckName() + "\".", "Operation Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
