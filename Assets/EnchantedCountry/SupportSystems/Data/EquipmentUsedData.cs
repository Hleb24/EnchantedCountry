using System;
using Core.EnchantedCountry.CoreEnchantedCountry.GameRule.EquipmentIdConstants;
using Core.EnchantedCountry.SupportSystems.SaveSystem;

namespace Core.EnchantedCountry.SupportSystems.Data {
  [Serializable]
  public class EquipmentUsedData: ResetSave {
    public string armorType;
    public string shieldType;
    public string oneHandedType;
    public string twoHandedType;
    public string rangeType;
    public string projectiliesType;
    public int armorId;
    public int shieldId;
    public int oneHandedId;
    public int twoHandedId;
    public int rangeId;
    public int projectiliesId;
    public int bagId;
    public int animalId;
    public int carriageId;
    public void Reset() {
      armorId = EquipmentIdConstants.NoArmorId;
      shieldId = default;
      oneHandedId = default;
      twoHandedId = default;
      rangeId = default;
      projectiliesId = default;
      bagId = default;
      animalId = default;
      animalId = default;
      carriageId = default;
      armorType = default;
      shieldType = default;
      oneHandedType = default;
      twoHandedType = default;
      rangeType = default;
      projectiliesType = default;
    }

    public EquipmentUsedData() {
      armorId = EquipmentIdConstants.NoArmorId;
      shieldId = default;
      oneHandedId = default;
      twoHandedId = default;
      rangeId = default;
      projectiliesId = default;
      bagId = default;
      animalId = default;
      animalId = default;
      carriageId = default;
      armorType = default;
      shieldType = default;
      oneHandedType = default;
      twoHandedType = default;
      rangeType = default;
      projectiliesType = default;
    }
  }
}