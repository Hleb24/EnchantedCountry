using System.Collections.Generic;

namespace Core.Main.GameRule.Equipment {
  /// <summary>
  ///   Интерфейс для работы с снаряжением.
  /// </summary>
  public interface IEquipment {
    public void ChangeQuantity(int id, int amount);
    public List<EquipmentCard> GetEquipmentCards();
  }
}