using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GW_Codex_Generator.Template_Draft
{
    class TemplateDraft
    {
        /*
         See: Wooden Potatoes monster draft.

        The idea is you get presented 3 templates for each character slot. You pick one and pass on the other two (they'll
        get put back into the "deck"--the pool of available templates--at the end of the draft round).

        The drafted templates get put into your current selection pool, then you pick N templates from that pool to use.

        Before the next round, you put two templates back into the pool, then the remaining N-2 templates you used get tossed
        to the winds. Presumably, they get shuffled back into the deck if the deck runs empty.

        Then you do another round of drafting.
     
             */

        List<string> _Templates = new List<string>();
        string _DeckName = "None";
        List<int> _Deck = new List<int>();
        List<int> _Trashed = new List<int>();
        List<int> _Pool = new List<int>();
        /// <summary>
        /// The templates not selected for the current draft round. Get re-added to the deck after the draft round is finished.
        /// </summary>
        List<int> _Rejecteds = new List<int>();
        List<int> _DraftHand = new List<int>();

        static public TemplateDraft Create()
        {
            TemplateDraft td = new TemplateDraft();

            // Create the new Template Draft using the templates and name of the currently loaded deck:
            td._DeckName = SkillDatabase.TemplateDeckName;
            td._Templates = new List<string>(SkillDatabase.TemplateDeck); // Make a copy of the deck for the draft, however!

            // Now set everything up:
            td.Reset();

            // Hand it over:
            return td;
        }
        public void Reset()
        {
            // Clear the indexing lists:
            _Deck.Clear();
            _Trashed.Clear();
            _Pool.Clear();
            _Rejecteds.Clear();

            // Fill the deck with indices to draw from randomly:
            for (int i = 0; i < _Templates.Count; ++i) { _Deck.Add(i); }
        }

        int Draw()
        {
            // Need to reconstitute the deck:
            if (_Deck.Count <= 0)
            {
                if (_Trashed.Count > 0)
                {
                    _Deck = _Trashed;
                    _Trashed = new List<int>();
                }
                else // We have no Trashed to recycle, so just grab a random template as a fallback:
                {
                    return SkillDatabase.RNG.Next(_Templates.Count);
                }
            }

            // Grab a random index into the deck:
            int i = SkillDatabase.RNG.Next(_Deck.Count);

            // Store the value at that index, which is an index into the templates list:
            int index = _Deck[i];

            // Remove the value at that index because we drew it and it shouldn't be in the deck anymore:
            _Deck.RemoveAt(i);

            // Return the value we drew:
            return index;
        }

        public void StartDraftRound(int saveFirst, int saveSecond)
        {
            if (_Pool.Count > 0)
            {
                // Here are a couple of safety checks to make sure you can't pass negative values into this.
                //  If you do (by not selecting templates to save), random ones will be selected for you.
                //  By using "while" loops, I'm basically doing an if() that also makes sure the two don't match each other.
                //  The first one needs a while in case something has happened to set the second value but not the first.
                //  I don't want them to converge.
                while (saveFirst < 0 || saveFirst == saveSecond) saveFirst = SkillDatabase.RNG.Next(_Pool.Count);
                while (saveSecond < 0 || saveSecond == saveFirst) saveSecond = SkillDatabase.RNG.Next(_Pool.Count);
                // Save the items indicated:
                int first = _Pool[saveFirst];
                int second = _Pool[saveSecond];
                _Pool[saveFirst] = _Pool[saveSecond] = -1; // Setting these to skip 'em in a moment.

                // Move the not-saved values to the trash:
                foreach (int i in _Pool) { if (i >= 0) _Trashed.Add(i); }
                _Pool.Clear();

                // Restore the saved items to the pool:
                _Pool.Add(first);
                _Pool.Add(second);
            }
        }

        public void EndDraftRound()
        {
            // At the end of the draft round, the rejected items get added back to the deck.
            _Deck.AddRange(_Rejecteds);
            _Rejecteds.Clear();
        }

        public List<string> DealDraftHand(int number)
        {
            _DraftHand.Clear();
            List<string> cards = new List<string>();
            for (int i = 0; i < number; ++i)
            {
                // Draw a template index:
                _DraftHand.Add(Draw());
                // Also add that actual template to the list to return so it can be displayed to the user:
                cards.Add(_Templates[_DraftHand[i]]);
            }

            return cards;
        }

        public string MakeDraftPick(int number)
        {
            // Add the selected pick to the pool:
            _Pool.Add(_DraftHand[number]);
            string template = _Templates[_DraftHand[number]];

            // Add the rejected picks to the rejecteds pile:
            for (int i = 0; i < _DraftHand.Count; ++i)
            {
                if (i != number) // Obviously, don't add the picked template!
                {
                    _Rejecteds.Add(_DraftHand[i]);
                }
            }

            // Now that we've put all of the templates from the hand somewhere, clear it out:
            _DraftHand.Clear();

            return template;
        }

        /// <summary>
        /// Gets the templates in the pool. These are the templates the player will use for putting together their team composition.
        /// </summary>
        /// <returns>List of templates.</returns>
        public List<string> GetPool()
        {
            List<string> cards = new List<string>();
            foreach (int i in _Pool)
            {
                cards.Add(_Templates[i]);
            }

            return cards;
        }

        public string GetTemplate(int id)
        {
            return _Templates[id];
        }

        public List<string> GetDraftHand()
        {
            List<string> cards = new List<string>();
            foreach (int i in _DraftHand)
            {
                cards.Add(_Templates[i]);
            }
            return cards;
        }
    }
}
