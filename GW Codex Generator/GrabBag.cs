using System.Collections.Generic;

namespace GW_Codex_Generator
{
    class GrabBag
    {
        public List<Skill> Skills = new List<Skill>();

        static public bool UseRatings = true;
        /// <summary>
        /// If a value, then this will override the ratings usage for this grab bag only with the specified value.
        /// If null, it'll use the global setting, "UseRatings".
        /// </summary>
        public bool? RatingsOverride_UseRatings = null;

        public GrabBag()
        {

        }

        public GrabBag (List<Skill> pool)
        {
            Skills.AddRange(pool);
        }

        public void Add(Skill skill)
        {
            if(skill != null)
            {
                Skills.Add(skill);
            }
        }

        #region Private skill pull functions
        private delegate Skill PullFunction();
        private Skill _SinglePull_NoRating()
        {
            // If we have no skills, return null:
            if (Skills.Count == 0) return null;

            // Grab a random index, remove the item from that index, then return it:
            int index = SkillDatabase.RNG.Next(Skills.Count);
            Skill ret = Skills[index];
            Skills.RemoveAt(index);

            return ret;
        }

        private Skill _SinglePull_WithRating()
        {
            // If we have no skills, return null:
            if (Skills.Count == 0) return null;
            if (Skills.Count == 1)
            {
                Skill ret = Skills[0];
                Skills.RemoveAt(0);
                return ret;
            }
            else
            {
                // Grab a random index, remove the item from that index, then return it:
                int index = SkillDatabase.RNG.Next(Skills.Count);

                // Take a roll. If the value is less than the skill's rating, then re-roll.
                //  e.g. a rating of 1 will always be selected (because the minimum roll is a 1), while a rating of 5 only has a 1/5 chance to be
                //       selected, as the maximum roll is 5 (6 is capped).
                while(SkillDatabase.RNG.Next(1, 6) < Skills[index].Rating)
                {
                    index = SkillDatabase.RNG.Next(Skills.Count);
                }

                Skill ret = Skills[index];
                Skills.RemoveAt(index);

                return ret;
            }
        }

        private PullFunction GetPullFunction()
        {
            bool useRating = UseRatings;
            if (RatingsOverride_UseRatings.HasValue)
            {
                useRating = RatingsOverride_UseRatings.Value;
            }

            if (useRating) return _SinglePull_WithRating;
            else return _SinglePull_NoRating;
        }
        #endregion

        internal Skill PullFromBag()
        {
            // Well, this looks weird! But GetPullFunction returns a function that we only need to use once, so...
            return GetPullFunction()();
        }

        /// <summary>
        /// Will pull up to the specified number of items from the bag. Can't pull more than that, obviously.
        /// </summary>
        /// <param name="count">The number of items to pull.</param>
        /// <returns>A sorted list of pulled items.</returns>
        internal List<Skill> PullXFromBag(int count)
        {
            PullFunction puller = GetPullFunction();
            List<Skill> pulls = new List<Skill>();
            for (int i = 0; i < count && !IsEmpty; ++i)
            {
                pulls.Add(puller());
            }

            if (pulls.Count > 1) pulls.SkillSort();
            return pulls;
        }

        internal void PutXIntoCodex(List<Skill> codex, int count)
        {
            PullFunction puller = GetPullFunction();
            for(int i = 0; i < count && !IsEmpty; ++i)
            {
                Skill skill = puller();
                if (skill != null) codex.Add(skill);
            }
        }


        public bool IsEmpty { get { return Skills.Count == 0; } }
    }
}
