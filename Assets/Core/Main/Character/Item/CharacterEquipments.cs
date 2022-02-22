using System.Collections.Generic;
using Core.Main.GameRule.Equipment;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Assertions;

namespace Core.Main.Character.Item {
  public class CharacterEquipments {
    private readonly List<EquipmentCard> _equipmentCards;

    public CharacterEquipments([NotNull, ItemNotNull] List<EquipmentCard> equipmentCards) {
      Assert.IsNotNull(equipmentCards, nameof(equipmentCards));
      _equipmentCards = new List<EquipmentCard>(equipmentCards);
    }

    private EquipmentCard GetEquipmentCardById(int id) {
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

    private bool QuantityOfProductInEquipmentCardsEqualZero(int id) {
      int numberOfProductInBackpack = GetQuantityOfProductInEquipmentCard(id);
      return numberOfProductInBackpack == 0;
    }

    private void IncreaseQuantityOfProduct(int id, int amount = 1) {
      if (!ProductIdExistsInEquipmentCards(id)) {
        AddNewEquipmentCard(id, 0);
      }

      int totalAmount = GetQuantityOfProductInEquipmentCard(id) + amount;
      SetNewQuantityOfProduct(id, totalAmount);
    }

    private void DecreaseQuantityOfProduct(int id, int amount) {
      if (QuantityOfProductMoreThan(id, amount)) {
        int totalAmount = GetQuantityOfProductInEquipmentCard(id) - amount;
        SetNewQuantityOfProduct(id, totalAmount);
      }

      IfQuantityEqualZeroRemoveFromEquipmentCards();
    }

    private void RemoveFromEquipmentCards(int id) {
      int index = GetEquipmentCardIndexInEquipmentCards(id);
      _equipmentCards.RemoveAt(index);
    }

    private void RemoveAllEquipmentCards() {
      for (int i = _equipmentCards.Count - 1; i >= 0; i--) {
        if (_equipmentCards[i].Id == EquipmentIdConstants.POCKETS || _equipmentCards[i].Id == EquipmentIdConstants.NO_ARMOR_ID) {
          continue;
        }

        _equipmentCards.RemoveAt(i);
      }
    }

    private bool ProductIdExistsInEquipmentCards(int id) {
      for (var i = 0; i < _equipmentCards.Count; i++) {
        if (_equipmentCards[i].Id == id) {
          return true;
        }
      }

      return false;
    }

    private bool QuantityOfProductInEquipmentCardsAtMostZero(int id) {
      int numberOfProductInBackpack = GetQuantityOfProductInEquipmentCard(id);
      return numberOfProductInBackpack > 0;
    }

    private bool QuantityOfProductInEquipmentCardsEqualOrLessZero(int id) {
      int numberOfProductInBackpack = GetQuantityOfProductInEquipmentCard(id);
      return numberOfProductInBackpack <= 0;
    }

    private bool QuantityOfProductMoreThan(int id, int amount) {
      return GetQuantityOfProductInEquipmentCard(id) >= amount;
    }

    private void SetNewQuantityOfProduct(int id, int newQuantityOfProduct) {
      EquipmentCard equipmentCard = GetEquipmentCardById(id);
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
        int index = i;
        if (QuantityOfProductInEquipmentCardsEqualOrLessZero(_equipmentCards[index].Id)) {
          RemoveAtIndex(index);
        }
      }
    }

    private int GetEquipmentCardIndexInEquipmentCards(int id) {
      for (var i = 0; i < _equipmentCards.Count; i++) {
        if (_equipmentCards[i].Id == id) {
          return i;
        }
      }

      Debug.LogError($"Индекс снаряжения в наборе карточек снаряжений не найден: id={id}");
      return -1;
    }
  }
}