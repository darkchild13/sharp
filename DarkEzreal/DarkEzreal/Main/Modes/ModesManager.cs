namespace DarkEzreal.Main.Modes
{
    using System;

    using DarkEzreal.Common;

    using LeagueSharp;

    internal class ModesManager
    {
        public static void GameOnOnUpdate(EventArgs args)
        {
            if (Config.Player.IsDead)
            {
                return;
            }

            if (Config.MiscMenu.GetKeyBind("EW") && SpellsManager.E.IsReady() && SpellsManager.W.IsReady() && SpellsManager.E.Instance.ManaCost + SpellsManager.W.Instance.ManaCost < Config.Player.Mana)
            {
                SpellsManager.W.Cast(Game.CursorPos);
            }

            Active.Extcute();

            if (Misc.ComboMode)
            {
                Combo.Execute();
            }

            if (Misc.HybridMode)
            {
                Hybrid.Execute();
            }

            if (Misc.LastHitMode)
            {
                LastHit.Execute();
            }

            if (Misc.LaneClearMode)
            {
                LaneClear.Execute();
                JungleClear.Execute();
            }
        }
    }
}
