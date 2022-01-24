using System;

namespace Core.Main.GameRule {
  public class Armor {
    public const string DEFAULT_NAME_FOR_ARMOR = "No";

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

    private string _armorName;
    private string _effectName = string.Empty;

    public Armor(string armorName = DEFAULT_NAME_FOR_ARMOR, int classOfArmor = 8, ArmorType armorType = ArmorType.None, string effectName = "") {
      ArmorName = armorName;
      ArmorClass = new ArmorClass(classOfArmor);
      EffectName = effectName;
      this.armorType = armorType;
    }

    public void Init(string armorName = DEFAULT_NAME_FOR_ARMOR, int classOfArmor = 8, ArmorType armorType = ArmorType.None, string effectName = "") {
      ArmorName = armorName;
      ArmorClass = new ArmorClass(classOfArmor);
      EffectName = effectName;
      this.armorType = armorType;
    }

    public void AddEffectOnArmor(string nameOfEffect, int value) {
      EffectName = nameOfEffect;
      ArmorClass.ClassOfArmor += value;
    }

    public ArmorType armorType { get; private set; } = ArmorType.None;

    public string ArmorName {
      get {
        return _armorName;
      }
      set {
        if (value != null) {
          _armorName = value;
        } else {
          throw new InvalidOperationException("Value is invalid");
        }
      }
    }

    public ArmorClass ArmorClass { get; private set; }

    public string EffectName {
      get {
        return _effectName;
      }
      set {
        if (value != null) {
          _effectName = value;
        } else {
          throw new InvalidOperationException("Value is invalid");
        }
      }
    }
  }
}