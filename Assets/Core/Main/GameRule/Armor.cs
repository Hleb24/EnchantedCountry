using JetBrains.Annotations;

namespace Core.Main.GameRule {
  public class Armor {
    private readonly ArmorClass _armorClass;
    private readonly ArmorType _armorType;
    private string _armorName;
    private string _effectName;

    public Armor(string armorName, ArmorClass armorClass, ArmorType armorType, string effectName) {
      _armorName = armorName;
      _armorClass = armorClass;
      _effectName = effectName;
      _armorType = armorType;
    }

    [MustUseReturnValue]
    public int GetArmorClass() {
      return _armorClass.GetArmorClass();
    }
  }
}