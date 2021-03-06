using System;

namespace Core.Main.GameRule.Item {
  [Flags]
  public enum WeaponType {
    #region BitOperation
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
    #endregion

    None = 0,
    Rock = 1 << 0,
    Dart = 1 << 1,
    SlingStone = 1 << 2,
    PlainDagger = 1 << 3,
    WarHammer = 1 << 4,
    Spear = 1 << 5,
    ThrowingAxe = 1 << 6,
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

    All = Rock | Dart | SlingStone | PlainDagger | WarHammer | Spear | ThrowingAxe | CrossbowArrows | SlingCores | GoldDagger | SilverDagger | ShortSword | ShortBowArrows |
          LongSword | LongbowArrows | TwoHandedHalberd | BattleAxe | TwoHandedSword | Sling | CatapultCores | Crossbow | ShortBow | LongBow | Catapult | Claw | Fang | Horn | Jaws |
          Scythe | Club,
    WarriorWeaponKit = All ^ (Claw | Fang | Horn | Jaws),
    ElfWeaponKit = All ^ (TwoHandedSword | TwoHandedHalberd | BattleAxe | Claw | Fang | Horn | Jaws | Scythe | Club),
    WizardWeaponKit = PlainDagger | SilverDagger | GoldDagger | Rock,
    KronWeaponKit = Rock | ShortSword | ShortBow | ShortBowArrows,
    GnomWeaponKit = All ^ TwoHandedSword ^ TwoHandedHalberd ^ BattleAxe ^ LongSword ^ LongBow ^ LongbowArrows ^ (Claw | Fang | Horn | Jaws | Scythe),
    OneHanded = Rock | PlainDagger | WarHammer | Spear | GoldDagger | SilverDagger | ShortSword | LongSword | Club,
    TwoHanded = TwoHandedHalberd | BattleAxe | TwoHandedSword | Scythe,
    Range = Sling | Dart | Crossbow | ShortBow | LongBow | Catapult | ThrowingAxe,
    Projectiles = SlingStone | SlingCores | CrossbowArrows | ShortBowArrows | LongbowArrows | CatapultCores,
    SlingSet = Sling | SlingStone | SlingCores,
    CrossbowSet = Crossbow | CrossbowArrows,
    ShortBowSet = ShortBow | ShortBowArrows,
    LongBowSet = LongBow | LongbowArrows,
    CatapultSet = Catapult | CatapultCores
  }
}