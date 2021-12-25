using System.Collections.Generic;
using Core.Rule.Character.Equipment;
using Core.Rule.GameRule.EquipmentIdConstants;
using UnityEngine;

namespace Core.ScriptableObject.Equipment {
	[CreateAssetMenu(menuName = "Equipment", fileName = "Equipment", order = 58)]
	public class EquipmentObject : UnityEngine.ScriptableObject {
		#region FIELDS
		public List<EquipmentCard> equipmentCards = new List<EquipmentCard>() {
			new EquipmentCard(EquipmentIdConstants.Pockets, 1),
		new EquipmentCard(EquipmentIdConstants.NoArmorId, 1)
		};
		#endregion
		#region GET_EQUIPMENT_CARD
		public EquipmentCard GetEquipmentCard(int id) {
			for (int i = 0; i < equipmentCards.Count; i++) {
				if (equipmentCards[i].ID == id) {
					return equipmentCards[i];
				}
			}
			return null;
		}
		#endregion
		#region GET_QUANTITY_OF_PRODUCT
		public int GetQuantityOfProductInEquipmentCard(int id) {
			for (int i = 0; i < equipmentCards.Count; i++) {
				if (equipmentCards[i].ID == id) {
					return equipmentCards[i].Quantity;
				}
			}
			return 0;
		}
		#endregion
		#region BOOLEAN
		public bool ProductIdExistsInEquipmentCards(int id) {
			for (int i = 0; i < equipmentCards.Count; i++) {
				if (equipmentCards[i].ID == id) {
					return true;
				}
			}
			return false;
		}

		public bool QuantityOfProductInEquipmentCardsAtMostZero(int id) {
			int numberOfProductInBackpack = GetQuantityOfProductInEquipmentCard(id);
			return numberOfProductInBackpack > 0;
		}

		public bool QuantityOfProductInEquipmentCardsEqualOrLessZero(int id) {
			int numberOfProductInBackpack = GetQuantityOfProductInEquipmentCard(id);
			return numberOfProductInBackpack <= 0;
		}
		public bool QuantityOfProductInEquipmentCardsEqualZero(int id) {
			int numberOfProductInBackpack = GetQuantityOfProductInEquipmentCard(id);
			return numberOfProductInBackpack == 0;
		}

		public bool QuantityOfProductMoreThan(int id, int amount) {
			return GetQuantityOfProductInEquipmentCard(id) >= amount;
		}
		#endregion
		#region INCREASE_AND_DEACREASE_QUANTITY_OF_PRODUCT
		public void IncreaseQuantityOfProduct(int id, int amount = 1) {
			if (!ProductIdExistsInEquipmentCards(id)) {
				AddNewEquipmentCard(id, 0);
			}
			int totalAmount = GetQuantityOfProductInEquipmentCard(id) + amount;
			SetNewQuantityOfProduct(id, totalAmount);

		}
		public void DecreaseQuantityOfProduct(int id, int amount) {
			if (QuantityOfProductMoreThan(id, amount)) {
				int totalAmount = GetQuantityOfProductInEquipmentCard(id) - amount;
				SetNewQuantityOfProduct(id, totalAmount);
			}
			IfQuantityEqualZeroRemoveFromEquipmentCards();
		}
		public void SetNewQuantityOfProduct(int id, int newQuantityOfProduct) {
			EquipmentCard equipmentCard = GetEquipmentCard(id);
			equipmentCard.Quantity = newQuantityOfProduct;
		}
		#endregion
		#region ADD_NEW_EQUIPMENT_CARD
		public void AddNewEquipmentCard(int id, int quantity = 1) {
			equipmentCards.Add(new EquipmentCard(id, quantity));
		}
		#endregion
		#region REMOVE_FROM_EQUIPMENT_CARDS
		public void RemoveFromEquipmentCards(int id) {
			int index = GetEquipmentCardIndexInEquipmentCards(id);
			equipmentCards.RemoveAt(index);
		}

		public void RemoveAtIndex(int index) {
			if (index == 0)
				return;
			equipmentCards.RemoveAt(index);
		}
		private int GetEquipmentCardIndexInEquipmentCards(int id) {
			for (int i = 0; i < equipmentCards.Count; i++) {
				if (equipmentCards[i].ID == id) {
					return i;
				}
			}
			Debug.LogError("Index of equipment card not found");
			return -1;
		}

		public void IfQuantityEqualZeroRemoveFromEquipmentCards() {
			for (int i = 0; i < equipmentCards.Count; i++) {
				int index = i;
				if (QuantityOfProductInEquipmentCardsEqualOrLessZero(equipmentCards[index].ID)) {
					RemoveAtIndex(index);
				}
			}
		}
		[ContextMenu("Remove all equipment cards")]
		public void RemoveAllEquipmentCards() {
			for (int i = equipmentCards.Count - 1; i >= 0; i--) {
				if (equipmentCards[i].ID == EquipmentIdConstants.NoArmorId)
					continue;
				equipmentCards.RemoveAt(i);
			}
		}
		#endregion
	}
}
