namespace DarkEzreal.Main
{
    using System.Collections.Generic;
    using System.Linq;

    using DarkEzreal.Common;

    using LeagueSharp;
    using LeagueSharp.SDK;
    using LeagueSharp.SDK.Enumerations;
    using LeagueSharp.SDK.UI;
    using LeagueSharp.SDK.Utils;

    internal static class SpellsManager
    {
        public static Spell Q, W, E, R;

        public static List<Spell> Spells = new List<Spell>();

        public static void Init()
        {
            Q = new Spell(SpellSlot.Q, 1150);
            W = new Spell(SpellSlot.W, 1000);
            E = new Spell(SpellSlot.E, 475);
            R = new Spell(SpellSlot.R, 3000);

            Q.SetSkillshot(250, 50, 2000, true, SkillshotType.SkillshotLine);
            W.SetSkillshot(250, 70, 1500, false, SkillshotType.SkillshotLine);
            E.SetSkillshot(250, 190, int.MaxValue, false, SkillshotType.SkillshotCircle);
            R.SetSkillshot(1000, 150, 2000, false, SkillshotType.SkillshotLine);

            Spells.Add(Q);
            Spells.Add(W);
            Spells.Add(E);
            Spells.Add(R);
        }

        public static void QCast(this Obj_AI_Base target, Menu menu)
        {
            if (target == null || !Q.IsReady() || !target.IsValidTarget(Q.Range))
            {
                return;
            }

            var pred = Q.GetPrediction(target);
            if (pred.Hitchance >= Q.hitchance(menu))
            {
                Q.Cast(pred.CastPosition);
            }
        }

        public static void WCast(this Obj_AI_Base target, Menu menu)
        {
            if (target == null || !W.IsReady() || !target.IsValidTarget(W.Range))
            {
                return;
            }

            var pred = W.GetPrediction(target);
            if (pred.Hitchance >= W.hitchance(menu))
            {
                W.Cast(pred.CastPosition);
            }
        }

        public static void ECast(this Obj_AI_Base target, Menu menu, bool safe = false)
        {
            if (target == null || !E.IsReady() || !target.IsValidTarget(E.Range))
            {
                return;
            }

            var pred = E.GetPrediction(target);
            if (pred.Hitchance >= E.hitchance(menu))
            {
                var ally =
                    GameObjects.Ally.OrderByDescending(a => a.CountAllyHeroesInRange(1000))
                        .FirstOrDefault(a => a.IsValidTarget(1000) && !a.IsMe && a.Distance(target) <= Config.Player.GetRealAutoAttackRange());
                E.Cast(ally != null && menu["ES"] && ally.SafetyManager(menu) ? pred.CastPosition.Extend(ally.ServerPosition, 475) : pred.CastPosition);
            }
        }
    }
}
