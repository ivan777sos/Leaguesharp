﻿using LeagueSharp;
using LeagueSharp.Common;
using Color = System.Drawing.Color;

namespace Princess_LeBlanc
{
    internal class MenuHandler
    {
        public static Menu LeBlancConfig;
        public static Menu TargetSelectorMenu;
        internal static Orbwalking2.Orbwalker2 Orbwalker;

        public static void Init()
        {

            LeBlancConfig = new Menu(
                "Princess " + ObjectManager.Player.ChampionName, "Princess" + ObjectManager.Player.ChampionName, true);

            LeBlancConfig.AddSubMenu(new Menu("Orbwalking", "Orbwalking"));
            Orbwalker = new Orbwalking2.Orbwalker2(LeBlancConfig.SubMenu("Orbwalking"));

            TargetSelectorMenu = new Menu("Target Selector", "Common_TargetSelector");
            TargetSelector.AddToMenu(TargetSelectorMenu);
            LeBlancConfig.AddSubMenu(TargetSelectorMenu);

            new AssassinManager();

            LeBlancConfig.AddSubMenu(new Menu("Combo", "Combo"));
            LeBlancConfig.SubMenu("Combo")
                .AddItem(
                    new MenuItem("ComboActive", "Combo!").SetValue(
                        new KeyBind(LeBlancConfig.Item("Orbwalk").GetValue<KeyBind>().Key, KeyBindType.Press)));
            LeBlancConfig.SubMenu("Combo").AddItem(new MenuItem("useQ", "Use Q").SetValue(true));
            LeBlancConfig.SubMenu("Combo").AddItem(new MenuItem("useW", "Use W").SetValue(true));
            LeBlancConfig.SubMenu("Combo").AddItem(new MenuItem("useE", "Use E").SetValue(true));
            LeBlancConfig.SubMenu("Combo").AddItem(new MenuItem("useR", "Use R").SetValue(true));

            LeBlancConfig.AddSubMenu(new Menu("Lane Clear", "ClearL"));
            LeBlancConfig.SubMenu("ClearL")
                .AddItem(
                    new MenuItem("laneClearActive", "LaneClear!").SetValue(
                        new KeyBind(LeBlancConfig.Item("LaneClear").GetValue<KeyBind>().Key, KeyBindType.Press)));
            LeBlancConfig.SubMenu("ClearL").AddItem(new MenuItem("LaneClearQ", "Use Q").SetValue(true));
            LeBlancConfig.SubMenu("ClearL").AddItem(new MenuItem("LaneClearW", "Use W").SetValue(true));
            LeBlancConfig.SubMenu("ClearL").AddItem(new MenuItem("LaneClear2W", "Use Second W").SetValue(true));
            LeBlancConfig.SubMenu("ClearL")
                .AddItem(new MenuItem("LaneClearWHit", "Min minions by W").SetValue(new Slider(2, 0, 5)));
            LeBlancConfig.SubMenu("ClearL")
                .AddItem(new MenuItem("LaneClearManaPercent", "Minimum Mana Percent").SetValue(new Slider(30)));

            LeBlancConfig.AddSubMenu(new Menu("Jungle Clear", "ClearJ"));
            LeBlancConfig.SubMenu("ClearJ")
                .AddItem(
                    new MenuItem("ClearJActive", "Jungle Clear!").SetValue(
                        new KeyBind(LeBlancConfig.Item("LaneClear").GetValue<KeyBind>().Key, KeyBindType.Press)));
            LeBlancConfig.SubMenu("ClearJ").AddItem(new MenuItem("JungleClearQ", "Use Q").SetValue(true));
            LeBlancConfig.SubMenu("ClearJ").AddItem(new MenuItem("JungleClearW", "Use W").SetValue(true));
            LeBlancConfig.SubMenu("ClearJ").AddItem(new MenuItem("JungleClear2W", "Use Second W").SetValue(true));
            LeBlancConfig.SubMenu("ClearJ")
                .AddItem(new MenuItem("JungleClearManaPercent", "Minimum Mana Percent").SetValue(new Slider(30)));

            LeBlancConfig.AddSubMenu(new Menu("Harass", "Harass"));
            LeBlancConfig.SubMenu("Harass")
                .AddItem(
                    new MenuItem("HarassActive", "Harass!").SetValue(
                        new KeyBind(LeBlancConfig.Item("Farm").GetValue<KeyBind>().Key, KeyBindType.Press)));
            LeBlancConfig.SubMenu("Harass").AddItem(new MenuItem("useQ", "Use Q").SetValue(true));
            LeBlancConfig.SubMenu("Harass").AddItem(new MenuItem("useW", "Use W").SetValue(true));
            LeBlancConfig.SubMenu("Harass").AddItem(new MenuItem("use2W", "Use Second W").SetValue(true));
            LeBlancConfig.SubMenu("Harass").AddItem(new MenuItem("useE", "Use E").SetValue(true));
            LeBlancConfig.SubMenu("Harass")
                .AddItem(new MenuItem("HarassManaPercent", "Minimum Mana Percent").SetValue(new Slider(30)));

            LeBlancConfig.AddSubMenu(new Menu("Flee", "Flee"));
            LeBlancConfig.SubMenu("Flee")
                .AddItem(new MenuItem("FleeK", "Key").SetValue(new KeyBind('A', KeyBindType.Press)));
            LeBlancConfig.SubMenu("Flee").AddItem(new MenuItem("FleeW", "Use W").SetValue(true));
            LeBlancConfig.SubMenu("Flee").AddItem(new MenuItem("FleeE", "Use E").SetValue(true));
            LeBlancConfig.SubMenu("Flee").AddItem(new MenuItem("FleeR", "Use R").SetValue(true));

            LeBlancConfig.AddSubMenu(new Menu("Items", "Items"));
            LeBlancConfig.SubMenu("Items").AddItem(new MenuItem("useDfg", "Use DFG").SetValue(true));
            LeBlancConfig.SubMenu("Items").AddItem(new MenuItem("useIgnite", "Use Ignite").SetValue(true));

            MenuItem dmgAfterComboItem = new MenuItem("DamageAfterCombo", "Draw damage after combo").SetValue(true);
            Utility.HpBarDamageIndicator.DamageToUnit = MathHandler.ComboDamage;
            Utility.HpBarDamageIndicator.Enabled = dmgAfterComboItem.GetValue<bool>();
            dmgAfterComboItem.ValueChanged += delegate(object sender, OnValueChangeEventArgs eventArgs)
            {
                Utility.HpBarDamageIndicator.Enabled = eventArgs.GetNewValue<bool>();
            };

            LeBlancConfig.AddSubMenu(new Menu("Drawings", "Drawing"));
            LeBlancConfig.SubMenu("Drawing").AddItem(dmgAfterComboItem);
            LeBlancConfig.SubMenu("Drawing")
                .AddItem(new MenuItem("drawQ", "Draw Q").SetValue(new Circle(true, Color.FromArgb(100, Color.Aqua))));
            LeBlancConfig.SubMenu("Drawing")
                .AddItem(new MenuItem("drawW", "Draw W").SetValue(new Circle(true, Color.FromArgb(100, Color.Aqua))));
            LeBlancConfig.SubMenu("Drawing")
                .AddItem(new MenuItem("drawE", "Draw E").SetValue(new Circle(true, Color.FromArgb(100, Color.Aqua))));

            LeBlancConfig.AddSubMenu(new Menu("Misc", "Misc"));
            LeBlancConfig.SubMenu("Misc").AddSubMenu(new Menu("Second W Logic", "backW"));
            LeBlancConfig.SubMenu("Misc").SubMenu("backW").AddItem(new MenuItem("useSW", "Use Second W Logic").SetValue(true));
            LeBlancConfig.SubMenu("Misc").SubMenu("backW").AddItem(new MenuItem("SWcountEnemy", "Minimum of Enemys around Second W Point").SetValue(new Slider(0, 0, 5)));
            LeBlancConfig.SubMenu("Misc").SubMenu("backW").AddItem(new MenuItem("SWplayerHp", "Minimum Player HP%").SetValue(new Slider(10, 0, 100)));
            LeBlancConfig.SubMenu("Misc").SubMenu("backW").AddItem(new MenuItem("SWpos", "If Second Wpos is closer to Cursor then Playerpos").SetValue(true));
            LeBlancConfig.SubMenu("Misc").SubMenu("backW").AddItem(new MenuItem("SWflee", "If FleeModeON").SetValue(true));
            LeBlancConfig.SubMenu("Misc").AddItem(new MenuItem("Interrupt", "Interrupt with E").SetValue(true));
            LeBlancConfig.SubMenu("Misc").AddItem(new MenuItem("Gapclose", "Anit Gapclose with E").SetValue(true));
            LeBlancConfig.SubMenu("Misc").AddItem(new MenuItem("UsePacket", "Use Packets").SetValue(false));
            LeBlancConfig.SubMenu("Misc")
                .AddItem(
                    new MenuItem("Clone", "Clone Logic").SetValue(
                        new StringList(
                            new[]
                            {
                                "None", "Towards Enemy", "Towards Player", "Shield Mode (between me and enemy)",
                                "Towards Mouse"
                            })));
            LeBlancConfig.AddToMainMenu();
        }
    }
}
