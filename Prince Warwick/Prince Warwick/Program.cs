﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSharp;
using LeagueSharp.Common;

namespace Prince_Warwick
{
    internal class Program
    {

        internal static Orbwalking.Orbwalker Orbwalker;

        static void Main(string[] args)
        {
            CustomEvents.Game.OnGameLoad += Load;
        }
        public static void Load(EventArgs args)
        {
            if (ObjectManager.Player.ChampionName != "Warwick") return;

            SkillHandler.Init();
            ItemHandler.Init();
            MenuHandler.Init();
            DrawingHandler.Init();

            Game.OnGameUpdate += OnGameUpdateModes;

            Game.PrintChat("Prince " + ObjectManager.Player.ChampionName);

            AntiGapcloser.OnEnemyGapcloser += FightHandler.AntiGapCloser;
            Interrupter.OnPossibleToInterrupt += FightHandler.Interrupter_OnPossibleToInterrupt;
        }
        public static void OnGameUpdateModes(EventArgs args)
        {

            Orbwalker.SetAttack(true);

            if (ObjectManager.Player.IsDead) return;
            if (ObjectManager.Player.HasBuff("Recall")) return;

            switch (Orbwalker.ActiveMode)
            {
                case Orbwalking.OrbwalkingMode.Combo:
                    FightHandler.Combo();
                    break;

                case Orbwalking.OrbwalkingMode.Mixed:
                    break;

                case Orbwalking.OrbwalkingMode.LaneClear:
                    FightHandler.LaneClear();
                    break;

                default:
                    if (MenuHandler.WarwickConfig.Item("autoULT").GetValue<KeyBind>().Active)
                    {
                        FightHandler.UltonClick();
                    }
                    break;
            

            }
            if (MenuHandler.WarwickConfig.Item("KSi").GetValue<bool>() ||
                MenuHandler.WarwickConfig.Item("KSq").GetValue<bool>())
            {
                FightHandler.KillSteal();
            }
        }
    }
}
