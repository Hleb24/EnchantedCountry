using System;
using System.Collections.Generic;
using Core.EnchantedCountry.CoreEnchantedCountry.Character.Equipment;
using Core.EnchantedCountry.CoreEnchantedCountry.GameRule.EquipmentIdConstants;
using Core.EnchantedCountry.SupportSystems.SaveSystem;
using UnityEngine;

namespace Core.EnchantedCountry.SupportSystems.Data {
  [Serializable]
  public class EquipmentsOfCharacterData: ResetSave {
    public List<EquipmentCard> EquipmentCards;
    public EquipmentsOfCharacterData(List<EquipmentCard> list) {
    EquipmentCards = new List<EquipmentCard>() {
	    new EquipmentCard(EquipmentIdConstants.Pockets, 1),
	    new EquipmentCard(EquipmentIdConstants.NoArmorId, 1)
    };

    EquipmentCards.AddRange(list);
    }
		
    public EquipmentsOfCharacterData() {
	    EquipmentCards = new List<EquipmentCard>() {
		    new EquipmentCard(EquipmentIdConstants.Pockets, 1),
		    new EquipmentCard(EquipmentIdConstants.NoArmorId, 1)
	    };
    }

    public void Reset() {
	    for (int i = EquipmentCards.Count - 1; i >= 1; i--) {
		    EquipmentCards.RemoveAt(i);
	    }

	    EquipmentCards = new List<EquipmentCard>() {
		    new EquipmentCard(EquipmentIdConstants.Pockets, 1),
		    new EquipmentCard(EquipmentIdConstants.NoArmorId, 1)
	    };
    }
    
    #region OPERATIONS
    public static implicit operator EquipmentsOfCharacterDataHandler(EquipmentsOfCharacterData equipmentsOfCharacterData) {
	    return new EquipmentsOfCharacterDataHandler(equipmentsOfCharacterData);
    }
    #endregion
  }

  public class EquipmentsOfCharacterDataHandler {
	  #region FIELDS
	  public EquipmentsOfCharacterData EquipmentsOfCharacter;
	  #endregion
	  #region CONSTRUCTORS
	  public EquipmentsOfCharacterDataHandler(EquipmentsOfCharacterData equipmentsOfCharacter) {
		  EquipmentsOfCharacter = equipmentsOfCharacter;
	  }
	  #endregion
    #region GET_EQUIPMENT_CARD
		public EquipmentCard GetEquipmentCard(int id) {
			for (int i = 0; i < EquipmentsOfCharacter.EquipmentCards.Count; i++) {
				if (EquipmentsOfCharacter.EquipmentCards[i].ID == id) {
					return EquipmentsOfCharacter.EquipmentCards[i];
				}
			}
			return null;
		}
		#endregion
		#region GET_QUANTITY_OF_PRODUCT
		public int GetQuantityOfProductInEquipmentCard(int id) {
			for (int i = 0; i < EquipmentsOfCharacter.EquipmentCards.Count; i++) {
				if (EquipmentsOfCharacter.EquipmentCards[i].ID == id) {
					return EquipmentsOfCharacter.EquipmentCards[i].Quantity;
				}
			}
			return 0;
		}
		#endregion
		#region BOOLEAN
		public bool ProductIdExistsInEquipmentCards(int id) {
			for (int i = 0; i < EquipmentsOfCharacter.EquipmentCards.Count; i++) {
				if (EquipmentsOfCharacter.EquipmentCards[i].ID == id) {
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
			EquipmentsOfCharacter.EquipmentCards.Add(new EquipmentCard(id, quantity));
		}
		#endregion
		#region REMOVE_FROM_EQUIPMENT_CARDS
		public void RemoveFromEquipmentCards(int id) {
			int index = GetEquipmentCardIndexInEquipmentCards(id);
			EquipmentsOfCharacter.EquipmentCards.RemoveAt(index);
		}

		public void RemoveAtIndex(int index) {
			if (index == 0)
				return;
			EquipmentsOfCharacter.EquipmentCards.RemoveAt(index);
		}
		private int GetEquipmentCardIndexInEquipmentCards(int id) {
			for (int i = 0; i < EquipmentsOfCharacter.EquipmentCards.Count; i++) {
				if (EquipmentsOfCharacter.EquipmentCards[i].ID == id) {
					return i;
				}
			}
			Debug.LogError("Index of equipment card not found");
			return -1;
		}

		public void IfQuantityEqualZeroRemoveFromEquipmentCards() {
			for (int i = 0; i < EquipmentsOfCharacter.EquipmentCards.Count; i++) {
				int index = i;
				if (QuantityOfProductInEquipmentCardsEqualOrLessZero(EquipmentsOfCharacter.EquipmentCards[index].ID)) {
					RemoveAtIndex(index);
				}
			}
		}
		public void RemoveAllEquipmentCards() {
			for (int i = EquipmentsOfCharacter.EquipmentCards.Count - 1; i >= 0; i--) {
				if (EquipmentsOfCharacter.EquipmentCards[i].ID == EquipmentIdConstants.NoArmorId ||
				EquipmentsOfCharacter.EquipmentCards[i].ID == EquipmentIdConstants.Pockets
				)
					continue;
				EquipmentsOfCharacter.EquipmentCards.RemoveAt(i);
			}
		}
		#endregion
  }
}