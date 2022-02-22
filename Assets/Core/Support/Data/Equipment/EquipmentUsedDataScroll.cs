using System;

namespace Core.Support.Data.Equipment {
  [Serializable]
  public struct EquipmentUsedDataScroll {
    public int ArmorId;
    public int ShieldId;
    public int OneHandedId;
    public int TwoHandedId;
    public int RangeId;
    public int ProjectilesId;
    public int BagId;
    public int AnimalId;
    public int CarriageId;

    public EquipmentUsedDataScroll(int armorId) {
      ArmorId = armorId;
      ShieldId = default;
      OneHandedId = default;
      TwoHandedId = default;
      RangeId = default;
      ProjectilesId = default;
      BagId = default;
      AnimalId = default;
      AnimalId = default;
      CarriageId = default;
    }
  }
}