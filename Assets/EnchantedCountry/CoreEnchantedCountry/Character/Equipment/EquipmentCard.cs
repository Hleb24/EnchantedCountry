using System;

namespace Core.EnchantedCountry.CoreEnchantedCountry.Character.Equipment {
	[Serializable]
	public class EquipmentCard {
		public int ID;
		public int Quantity;
		public EquipmentCard() { }
		public EquipmentCard(int id, int quantity) {
			this.ID = id;
			this.Quantity = quantity;
		}
	}
}
