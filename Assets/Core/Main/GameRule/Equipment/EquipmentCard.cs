using System;

namespace Core.Main.GameRule.Equipment {
  /// <summary>
  ///   Класс описывает карту снаряжения.
  /// </summary>
  [Serializable]
  public class EquipmentCard {
    public int Id;
    public int Quantity;

    public EquipmentCard() { }

    public EquipmentCard(int id, int quantity) {
      Id = id;
      Quantity = quantity;
    }
  }
}