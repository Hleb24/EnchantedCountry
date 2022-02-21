using System;

namespace Core.Main.GameRule {
  [Flags]
  public enum ArmorType {
    None = 0,
    No = 1 << 0,
    Leather = 1 << 1,
    LeatherSilver = 1 << 2,
    IronChainMail = 1 << 3,
    SilverChainMail = 1 << 4,
    Carapace = 1 << 5,
    PlateArmor = 1 << 6,
    Shield = 1 << 7,
    All = No | Leather | LeatherSilver | IronChainMail | SilverChainMail | Carapace | PlateArmor | Shield,
    WarriorArmorKit = All,
    ElfArmorKit = All ^ PlateArmor,
    WizardArmorKit = No | Leather | LeatherSilver,
    KronArmorKit = All ^ PlateArmor,
    GnomArmorKit = All ^ PlateArmor,
    OnlyArmor = All ^ Shield
  }
}