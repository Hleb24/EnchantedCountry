using System;

namespace Core.EnchantedCountry.CoreEnchantedCountry.Character.Equipment {
	/// <summary>
	///   Класс описывает карту снаряжения.
	/// </summary>
	[Serializable]
  public class EquipmentCard {
    public int ID;
    public int Quantity;
    public EquipmentCard() { }

    public EquipmentCard(int id, int quantity) {
      ID = id;
      Quantity = quantity;
    }
  }
}