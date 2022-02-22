using System;
using UnityEngine.Serialization;

namespace Core.Main.GameRule {
  /// <summary>
  ///   Класс описывает карту снаряжения.
  /// </summary>
  [Serializable]
  public class EquipmentCard {
    [FormerlySerializedAs("ID")]
    public int Id;
    public int Quantity;

    public EquipmentCard(int id, int quantity) {
      Id = id;
      Quantity = quantity;
    }
  }
}