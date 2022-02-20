using System;
using System.Collections.Generic;
using Aberrance.Extensions;
using Core.Main.Dice;

namespace Core.Main.GameRule {
  public class Weapon {
    public static bool Is(WeaponType conditionalWeaponType, WeaponType targetWeaponType) {
      return (conditionalWeaponType & targetWeaponType) == targetWeaponType;
    }

    private static bool IsEdgesEqualOne(int edges) {
      return edges == 1;
    }

    private static bool IsEdgesAtMostSix(int edges) {
      return edges <= 6;
    }

    private static bool IsEdgesEqualToNine(int edges) {
      return edges == 9;
    }

    private static bool IsEdgesEqualToEighteen(int edges) {
      return edges == 18;
    }

    public const string DefalultName = "Rock";

    [Flags]
    public enum WeaponType {
      //None                = 0b0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000,
      //Rock                = 0b0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0001,
      //Dart                = 0b0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0010,
      //SlingStone          = 0b0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0100,
      //PlainDagger         = 0b0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_1000,
      //WarHammer           = 0b0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0001_0000,
      //Spear               = 0b0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0010_0000,
      //ThrowingAx          = 0b0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0100_0000,
      //CrossbowArrows      = 0b0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_1000_0000,
      //SlingCores          = 0b0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0001_0000_0000,
      //GoldDagger          = 0b0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0010_0000_0000,
      //SilverDagger        = 0b0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0100_0000_0000,
      //ShortSword          = 0b0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_1000_0000_0000,
      //ShortBowArrows      = 0b0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0001_0000_0000_0000,
      //LongSword           = 0b0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0010_0000_0000_0000,
      //LongbowArrows       = 0b0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0100_0000_0000_0000,
      //TwoHandedHalberd    = 0b0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_1000_0000_0000_0000,
      //BattleAxe           = 0b0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0001_0000_0000_0000_0000,
      //TwoHandedSword      = 0b0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0010_0000_0000_0000_0000,
      //Sling               = 0b0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0100_0000_0000_0000_0000,
      //CatapultCores       = 0b0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_1000_0000_0000_0000_0000,
      //Crossbow            = 0b0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0001_0000_0000_0000_0000_0000,
      //ShortBow            = 0b0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0010_0000_0000_0000_0000_0000,
      //LongBow             = 0b0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0100_0000_0000_0000_0000_0000,
      //Catapult            = 0b0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_1000_0000_0000_0000_0000_0000,
      //WarriorWeaponKit    = 0b0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_1111_1111_1111_1111_1111_1111,
      //ElfWeaponKit        = 0b0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_1111_1100_0111_1111_1111_1111,
      //WizardWeaponKit     = 0b0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0110_0000_1000,
      //KronWeaponKit       = 0b0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0010_0000_0001_1000_0000_0000,
      //GnomWeaponKit       = 0b0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_1011_1100_0001_1111_1111_1111,
      None = 0,
      Rock = 1 << 0,
      Dart = 1 << 1,
      SlingStone = 1 << 2,
      PlainDagger = 1 << 3,
      WarHammer = 1 << 4,
      Spear = 1 << 5,
      ThrowingAx = 1 << 6,
      CrossbowArrows = 1 << 7,
      SlingCores = 1 << 8,
      GoldDagger = 1 << 9,
      SilverDagger = 1 << 10,
      ShortSword = 1 << 11,
      ShortBowArrows = 1 << 12,
      LongSword = 1 << 13,
      LongbowArrows = 1 << 14,
      TwoHandedHalberd = 1 << 15,
      BattleAxe = 1 << 16,
      TwoHandedSword = 1 << 17,
      Sling = 1 << 18,
      CatapultCores = 1 << 19,
      Crossbow = 1 << 20,
      ShortBow = 1 << 21,
      LongBow = 1 << 22,
      Catapult = 1 << 23,
      Claw = 1 << 24,
      Fang = 1 << 25,
      Horn = 1 << 26,
      Jaws = 1 << 27,
      Scythe = 1 << 28,
      Club = 1 << 29,

      All = Rock | Dart | SlingStone | PlainDagger | WarHammer | Spear | ThrowingAx | CrossbowArrows | SlingCores | GoldDagger | SilverDagger | ShortSword | ShortBowArrows |
            LongSword | LongbowArrows | TwoHandedHalberd | BattleAxe | TwoHandedSword | Sling | CatapultCores | Crossbow | ShortBow | LongBow | Catapult | Claw | Fang | Horn |
            Jaws | Scythe | Club,
      WarriorWeaponKit = All ^ (Claw | Fang | Horn | Jaws),
      ElfWeaponKit = All ^ (TwoHandedSword | TwoHandedHalberd | BattleAxe | Claw | Fang | Horn | Jaws | Scythe | Club),
      WizardWeaponKit = PlainDagger | SilverDagger | GoldDagger | Rock,
      KronWeaponKit = Rock | ShortSword | ShortBow | ShortBowArrows,
      GnomWeaponKit = All ^ TwoHandedSword ^ TwoHandedHalberd ^ BattleAxe ^ LongSword ^ LongBow ^ LongbowArrows ^ (Claw | Fang | Horn | Jaws | Scythe),
      OneHanded = Rock | PlainDagger | WarHammer | Spear | GoldDagger | SilverDagger | ShortSword | LongSword | Club,
      TwoHanded = TwoHandedHalberd | BattleAxe | TwoHandedSword | Scythe,
      Range = Sling | Dart | Crossbow | ShortBow | LongBow | Catapult | ThrowingAx,
      Projectilies = SlingStone | SlingCores | CrossbowArrows | ShortBowArrows | LongbowArrows | CatapultCores,
      SlingSet = Sling | SlingStone | SlingCores,
      CrossbowSet = Crossbow | CrossbowArrows,
      ShotbowSet = ShortBow | ShortBowArrows,
      LongbowSet = LongBow | LongbowArrows,
      CatapultSet = Catapult | CatapultCores
    }

    public string NameOfWeapon;
    private string _effectName = string.Empty;

    public Weapon(float maxDamage = 0, WeaponType weaponType = WeaponType.None, string name = DefalultName, float minDamage = 0, int accurancy = 0, string effectName = "",
      int id = 100) {
      NameOfWeapon = name;
      EffectName = effectName;
      this.weaponType = weaponType;
      Attack = new Attack(maxDamage, minDamage, accurancy);
      Id = id;
    }

    public Weapon(List<float> damageList, WeaponType weaponType = WeaponType.None, string name = DefalultName, int accurancy = 0, string effectName = "", int id = 100) {
      NameOfWeapon = name;
      EffectName = effectName;
      this.weaponType = weaponType;
      Attack = new Attack(damageList, accurancy);
      Id = id;
    }

    public void Init(float maxDamage = 1, WeaponType weaponType = WeaponType.None, string name = DefalultName, float minDamage = 0, int accurancy = 0, string effectName = "normal",
      int id = 100) {
      NameOfWeapon = name;
      EffectName = effectName;
      this.weaponType = weaponType;
      Attack = new Attack(maxDamage, minDamage, accurancy);
      Id = id;
    }

    public void Init(List<float> damageList, WeaponType weaponType = WeaponType.None, string name = DefalultName, int accurancy = 0, string effectName = "", int id = 100) {
      NameOfWeapon = name;
      EffectName = effectName;
      this.weaponType = weaponType;
      Attack = new Attack(damageList, accurancy);
      Id = id;
    }

    public float ToDamage() {
      int edges = Attack.DiceEdges;
      if (edges == 0) {
        return 0;
      }

      if (IsEdgesEqualOne(edges)) {
        return Attack.MaxDamage;
      }

      if (IsEdgesAtMostSix(edges)) {
        var sixSidedDice = new SixSidedDice();
        int edge = sixSidedDice.GetDiceRollAccordingToEdges(edges);
        return Attack.GetDamage(edge);
      }

      if (IsEdgesEqualToNine(edges)) {
        int edge = KitOfDice.DiceKit[KitOfDice.SetWithOneThreeSidedAndOneSixSidedDice].SumRollsOfDice();
        return Attack.GetDamage(edge);
      }

      if (IsEdgesEqualToEighteen(edges)) {
        int edge = KitOfDice.DiceKit[KitOfDice.SetWithOneTwelveSidedAndOneSixSidedDice].SumRollsOfDice();
        return Attack.GetDamage(edge);
      }

      throw new InvalidOperationException("Edges is invalid");
    }

    public void AddEffectOnWeapon(string nameOfEffect, float maxDamage) {
      EffectName = nameOfEffect;
      Attack.MaxDamage = maxDamage;
    }

    public void AddEffectOnWeapon(string nameOfEffect, float maxDamage, float minDamge) {
      EffectName = nameOfEffect;
      Attack.MaxDamage = maxDamage;
      Attack.MinDamage = minDamge;
    }

    public void AddEffectOnWeapon(string nameOfEffect, float maxDamage, int accurancy) {
      EffectName = nameOfEffect;
      Attack.MaxDamage = maxDamage;
      Attack.Accuracy = accurancy;
    }

    public void AddEffectOnWeapon(string nameOfEffect, float maxDamage, float minDamge, int accurancy) {
      EffectName = nameOfEffect;
      Attack.MaxDamage = maxDamage;
      Attack.MinDamage = minDamge;
      Attack.Accuracy = accurancy;
    }

    public Attack Attack { get; private set; }
    public WeaponType weaponType { get; set; }

    public string EffectName {
      get {
        return _effectName;
      }
      set {
        if (value.NotNull()) {
          _effectName = value;
        } else {
          throw new InvalidOperationException("Value is invalid");
        }
      }
    }

    public int Id { get; private set; }
  }
}