using System.Collections.Generic;
using Aberrance.Extensions;
using Core.Main.GameRule;
using Core.Support.Attributes;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Serialization;

namespace Core.SO.Equipment {
  [CreateAssetMenu(menuName = "Equipment", fileName = "Equipment", order = 58)]
  public class EquipmentSO : ScriptableObject, IEquipment {
    [FormerlySerializedAs("equipmentCards"), SerializeField]
    private List<EquipmentCard> _equipmentCards = new() {
      new EquipmentCard(EquipmentIdConstants.POCKETS, 1),
      new EquipmentCard(EquipmentIdConstants.NO_ARMOR_ID, 1)
    };

    public void ChangeQuantity(int id, int amount) {
      Assert.IsTrue(id >= 0);
      if (amount >= 0) {
        IncreaseQuantityOfEquipment(id, amount);
      } else {
        DecreaseQuantityOfEquipment(id, amount);
      }
    }

    [NotNull]
    public List<EquipmentCard> GetEquipmentCards() {
      return _equipmentCards;
    }

    public void IncreaseQuantityOfEquipment(int id, int amount = 1) {
      if (ProductIdExistsInEquipmentCards(id).False()) {
        AddNewEquipmentCard(id, 0);
      }

      int totalAmount = GetQuantityOfProductInEquipmentCard(id) + amount;
      SetNewQuantityOfProduct(id, totalAmount);
    }

    [Button]
    public void RemoveAllEquipmentCards() {
      for (int i = _equipmentCards.LastIndex(); i >= 0; i--) {
        if (_equipmentCards[i].Id == EquipmentIdConstants.NO_ARMOR_ID) {
          continue;
        }

        _equipmentCards.RemoveAt(i);
      }
    }

    private void DecreaseQuantityOfEquipment(int id, int amount) {
      if (QuantityOfProductMoreThan(id, amount)) {
        int totalAmount = GetQuantityOfProductInEquipmentCard(id) - amount;
        SetNewQuantityOfProduct(id, totalAmount);
      }

      IfQuantityEqualZeroRemoveFromEquipmentCards();
    }

    [CanBeNull]
    private EquipmentCard GetEquipmentCard(int id) {
      for (var i = 0; i < _equipmentCards.Count; i++) {
        if (_equipmentCards[i].Id == id) {
          return _equipmentCards[i];
        }
      }

      return null;
    }

    private int GetQuantityOfProductInEquipmentCard(int id) {
      for (var i = 0; i < _equipmentCards.Count; i++) {
        if (_equipmentCards[i].Id == id) {
          return _equipmentCards[i].Quantity;
        }
      }

      return 0;
    }

    private bool ProductIdExistsInEquipmentCards(int id) {
      for (var i = 0; i < _equipmentCards.Count; i++) {
        if (_equipmentCards[i].Id == id) {
          return true;
        }
      }

      return false;
    }

    private bool QuantityOfProductInEquipmentCardsEqualOrLessZero(int id) {
      int numberOfProductInBackpack = GetQuantityOfProductInEquipmentCard(id);
      return numberOfProductInBackpack <= 0;
    }

    private bool QuantityOfProductMoreThan(int id, int amount) {
      return GetQuantityOfProductInEquipmentCard(id) >= amount;
    }

    private void SetNewQuantityOfProduct(int id, int newQuantityOfProduct) {
      EquipmentCard equipmentCard = GetEquipmentCard(id);
      if (equipmentCard is null) {
        return;
      }

      equipmentCard.Quantity = newQuantityOfProduct;
    }

    private void AddNewEquipmentCard(int id, int quantity = 1) {
      _equipmentCards.Add(new EquipmentCard(id, quantity));
    }

    private void RemoveAtIndex(int index) {
      if (index == 0) {
        return;
      }

      _equipmentCards.RemoveAt(index);
    }

    private void IfQuantityEqualZeroRemoveFromEquipmentCards() {
      for (var i = 0; i < _equipmentCards.Count; i++) {
        if (QuantityOfProductInEquipmentCardsEqualOrLessZero(_equipmentCards[i].Id)) {
          RemoveAtIndex(i);
        }
      }
    }
  }
}