﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GW_Codex_Generator
{
    static class CodexGenerator
    {
        internal static List<Skill> PureRandom(List<Skill> pool, int count)
        {
            GrabBag bag = new GrabBag(pool);
            return bag.PullXFromBag(count);
        }

        internal static List<Skill> ProfessionBalanced(List<Skill> pool, int regularPerProfession, int elitePerProfession, int commonSkills)
        {
            // Create empty bags:
            GrabBag[] regular = new GrabBag[10];
            GrabBag[] elite = new GrabBag[10];
            GrabBag common = new GrabBag();

            for(int i = 0; i < 10; ++i)
            {
                regular[i] = new GrabBag();
                elite[i] = new GrabBag();
            }

            // Fill the bags!
            foreach(Skill skill in pool)
            {
                if(skill.Profession == Skill.Professions.None)
                {
                    // Fill up a single common skills bag. These are mostly PvE only skills. I'm not differentiating elite as there are only the three norn forms.
                    common.Add(skill);
                }
                else
                {
                    if (skill.IsElite) elite[(int)skill.Profession].Add(skill);
                    else regular[(int)skill.Profession].Add(skill);
                }
            }

            // Draw from the bags!
            List<Skill> codex = new List<Skill>();
            for(int i = 0; i < 10; ++i)
            {
                regular[i].PutXIntoCodex(codex, regularPerProfession);
                elite[i].PutXIntoCodex(codex, elitePerProfession);
            }

            common.PutXIntoCodex(codex, commonSkills);

            // Sort the codex properly and return it:
            codex.SkillSort();
            return codex;
        }

        internal static List<Skill> AttributeBalanced(List<Skill> pool, int regularPerAttribute, int elitePerAttribute, int pveOnlyPulls)
        {
            // Prepare bags:
            GrabBag[] attrRegularBags = new GrabBag[42];
            GrabBag[] attrEliteBags = new GrabBag[42];
            GrabBag common = new GrabBag();
            GrabBag noAttrRegularBag = new GrabBag();
            GrabBag noAttrEliteBag = new GrabBag();

            for(int i = 0; i < 42; ++i)
            {
                attrEliteBags[i] = new GrabBag();
                attrRegularBags[i] = new GrabBag();
            }

            // Fill bags:
            foreach(Skill skill in pool)
            {
                if(skill.Attribute == Skill.Attributes.None)
                {
                    if(skill.IsElite)
                    {
                        skill.AddToBag(noAttrEliteBag);
                    }
                    else
                    {
                        skill.AddToBag(noAttrRegularBag);
                    }
                }
                else if (skill.Attribute== Skill.Attributes.PvE_Only)
                {
                    skill.AddToBag(common);
                }
                else // Any other attribute...
                {
                    if(skill.IsElite)
                    {
                        attrEliteBags[(int)skill.Attribute].Add(skill);
                    }
                    else
                    {
                        attrRegularBags[(int)skill.Attribute].Add(skill);
                    }
                }
            }

            // Draw from bags:
            List<Skill> codex = new List<Skill>();

            common.PutXIntoCodex(codex, pveOnlyPulls);
            noAttrRegularBag.PutXIntoCodex(codex, regularPerAttribute);
            noAttrEliteBag.PutXIntoCodex(codex, elitePerAttribute);
            for(int i = 0; i < 42; ++i)
            {
                attrRegularBags[i].PutXIntoCodex(codex, regularPerAttribute);
                attrEliteBags[i].PutXIntoCodex(codex, elitePerAttribute);
            }

            codex.SkillSort();

            return codex;
        }

        static internal List<Skill> AttributeRegularProfessionElite(List<Skill> pool, int skillPerAttribute, int elitePerProfession, int pveOnlyPulls)
        {
            // Prepare bags:
            GrabBag[] attrRegularBags = new GrabBag[42];
            GrabBag[] EliteBags = new GrabBag[10];
            GrabBag common = new GrabBag();
            GrabBag noAttrRegularBag = new GrabBag();

            for (int i = 0; i < 42; ++i)
            {
                attrRegularBags[i] = new GrabBag();
            }

            for(int i= 0; i < EliteBags.Length;++i)
            {
                EliteBags[i] = new GrabBag();
            }

            // Fill bags:
            foreach (Skill skill in pool)
            {
                if (skill.IsElite)
                {
                    if(skill.Profession == Skill.Professions.None)
                    {
                        // The only elites without a profession are the norn blessings:
                        common.Add(skill);
                    }
                    else
                    {
                        EliteBags[(int)skill.Profession].Add(skill);
                    }
                }
                else
                {
                    if (skill.Attribute == Skill.Attributes.None)
                    {
                        skill.AddToBag(noAttrRegularBag);
                    }
                    else if (skill.Attribute == Skill.Attributes.PvE_Only)
                    {
                        skill.AddToBag(common);
                    }
                    else // Any other attribute...
                    {
                        attrRegularBags[(int)skill.Attribute].Add(skill);
                    }
                }
            }

            // Draw from bags:
            List<Skill> codex = new List<Skill>();

            common.PutXIntoCodex(codex, pveOnlyPulls);
            noAttrRegularBag.PutXIntoCodex(codex, skillPerAttribute);
            for (int i = 0; i < 42; ++i)
            {
                attrRegularBags[i].PutXIntoCodex(codex, skillPerAttribute);
            }

            for(int i = 0; i < EliteBags.Length; ++i)
            {
                EliteBags[i].PutXIntoCodex(codex, elitePerProfession);
            }

            codex.SkillSort();

            return codex;
        }

        static internal List<Skill> ByRating(List<Skill> pool, int rating1, int rating2, int rating3, int rating4, int rating5, bool includePvEonly)
        {
            GrabBag[] bags = new GrabBag[5];
            for(int i= 0; i < 5; ++i)
            {
                bags[i] = new GrabBag();
            }

            foreach(Skill skill in pool)
            {
                // If we're not including PvE only skills and this skill is one, then skip it. Otherwise, add the skill to the correct bag.
                if(skill.Attribute == Skill.Attributes.PvE_Only && !includePvEonly)
                {
                    continue;
                }
                else
                {
                    bags[skill.Rating - 1].Add(skill);
                }
            }

            List<Skill> codex = new List<Skill>();

            bags[0].PutXIntoCodex(codex, rating1);
            bags[1].PutXIntoCodex(codex, rating2);
            bags[2].PutXIntoCodex(codex, rating3);
            bags[3].PutXIntoCodex(codex, rating4);
            bags[4].PutXIntoCodex(codex, rating5);

            codex.SkillSort();

            return codex;
        }

        internal class SmartBalanceParameters
        {

        }
        internal static List<Skill> SmartBalance(SmartBalanceParameters parameters)
        {
            List<Skill> codex = new List<Skill>();

            // This needs to generate some very specific stuff:
            // - Include a way to bring a pet. (Charm Animal, Heal as One, or Comfort Animal)
            // - Include a lead attack, offhand attack, and dual attack
            // - Include condition/hex/enchantment/stance removal
            // - Take ratings into account
            // - Add some skills that can actually heal party members for solid amounts


            /*
             Let's think about some categories of skills:
             - Sword Attacks
             - Axe Attacks
             - Hammer Attacks
             - Dagger Attacks
             - Scythe Attacks
             - Bow Attacks
             - Spear Attacks
             - Generic Melee Attacks
             - Heal an ally (weak)
             - Heal an ally (medium)
             - Heal an ally (strong)
             - Self heals
             - Cure condition ally
             - Cure condition self
             - Protect an ally
             - Protect self
             - Protect party
             - Heal party
             - Cure conditions party
             - Remove hex
             - Increase attack speed
             - Increase move speed ally
             - Increase move speed self
             - Increase move speed party
             - Interrupt
             - Pressure
             - High damage AoE
             - High Damage single-target
             - Pet Skills
             - General Utility
             - Energy Denial
             - Energy Management Self
             - Energy Management Ally
             - Control spells (e.g. snares, slower attack)
             - Punisher Hexes?
             
             
            Something I'm thinking is that it makes sense to introduce possible pushes and pulls. For example, maybe I want self healing to be
            plentiful, but healing allies (e.g. Heal Other) to be weak. I also like the idea that sometimes you only get skills from one
            warrior weapon, sometimes from all three. Basically, I want to move the focus around a bit, in different ways.

            Maybe this could focus on different areas? Like, Protection is general through self skills or ally-targetting skills, etc.

            I'm thinking I want a few axes to kinda sort between:
            * Self-Heals <--> Ally Heals
            * Self-Protection (e.g. Defensive Stance) <--> Ally Protection (e.g. Guardian)
            * Spikes <--> Pressure
            * Control (Inhibit, such as Blind or Snares) <--> Control Interrupt (KDs, interrupts, one-shot hexes such as Guilt)
            * Control Strip
            * Party Cleanse (can weight towards party-wide and self for conditions)
            * Party Augments (barbs, conjure frost, etc.)
            * Where do punish skills come in?
            * A miscellaneous stuff category (for things like Warrior's Endurance)


            -----------------------
            Thing to add:
            - Skill booster packs, and a Skill League and Skill Drafting!
            - Skills will need assigned rarities (just an int; let user specify number of rarities and names); also requires a way to assign rarities.
            - Need to allow user to specify contents of a booster pack.
                - This needs to include sourcing info, just like a Codex pool.
            - System for tracking skill pools in the form of what skills are available and how many copies are available.
             */

            return codex;
        }

        static internal string CodexToHTML(this List<Skill> codex)
        {
            // Update this with the following modes:
            // 1. List. Shouldn't put breaks or newlines in between skills, just spaces. Skills should be separated
            //    out by category (<h2>warrior</h2><h3>strength</h3>).
            // 2. When using just icons, just plop all of them together and let the webpage newline it.
            // 3. Use something more like what I've got now for the full descriptions, maybe with headers for profession and attribute as above.

            string html = "";
            // if(Mode is list)...
            {
                Skill.Professions lastProf = (Skill.Professions)(-1);
                Skill.Attributes lastAttr = (Skill.Attributes)(-1);
                foreach (Skill skill in codex)
                {
                    if(skill.Profession != lastProf)
                    {
                        html += "<h2>" + skill.Profession.ToString() + "</h2>" + Environment.NewLine;
                        lastProf = skill.Profession;
                    }
                    if(skill.Attribute != lastAttr)
                    {
                        html += "<h3>" + skill.AttributeName + "</h3>" + Environment.NewLine;
                        lastAttr = skill.Attribute;
                    }
                    html += skill.AHref() + " | ";
                }
            }

            return html;
        }
    }
}
