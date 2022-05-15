using System;
using System.Collections.Generic;
using Aberrance.Extensions;
using Core.Main.GameRule.Equipment;
using JetBrains.Annotations;
using UnityEngine;

namespace Core.Support.Data.Equipment {
  [Serializable]
  public struct EquipmentsDataScroll {
    private const int START_QUANTITY = 1;
    private readonly List<int> _startEquipments;
    public List<EquipmentCard> EquipmentCards;

    public EquipmentsDataScroll(IReadOnlyList<int> startEquipmentsId) {
      EquipmentCards = new List<EquipmentCard>();
      _startEquipments = new List<int>();
      for (var i = 0; i < startEquipmentsId.Count; i++) {
        EquipmentCards.Add(new EquipmentCard(startEquipmentsId[i], START_QUANTITY));
        _startEquipments.Add(startEquipmentsId[i]);
      }
    }

    internal void DecreaseQuantityOfEquipment(int id, int amount) {
      if (QuantityOfProductMoreThan(id, amount)) {
        int totalAmount = GetQuantityOfProductInEquipmentCard(id) - amount;
        SetNewQuantityOfEquipment(id, totalAmount);
      }

      IfQuantityEqualZeroRemoveFromEquipmentCards();
    }

    private bool QuantityOfProductMoreThan(int id, int amount) {
      return GetQuantityOfProductInEquipmentCard(id) >= amount;
    }

    private void IfQuantityEqualZeroRemoveFromEquipmentCards() {
      for (var i = 0; i < EquipmentCards.Count; i++) {
        if (QuantityOfProductInEquipmentCardsEqualOrLessZero(EquipmentCards[i].Id)) {
          RemoveAtIndex(i);
        }
      }
    }

    private void RemoveAtIndex(int index) {
      if (_startEquipments.Contains(index)) {
        Debug.LogWarning("Стандартное снаряжение");
        return;
      }

      EquipmentCards.RemoveAt(index);
    }

    private bool QuantityOfProductInEquipmentCardsEqualOrLessZero(int id) {
      int numberOfProductInBackpack = GetQuantityOfProductInEquipmentCard(id);
      return numberOfProductInBackpack <= 0;
    }

    internal void IncreaseQuantityOfEquipment(int id, int amount = 1) {
      if (ProductIdExistsInEquipmentCards(id).IsFalse()) {
        AddNewEquipmentCard(id, 0);
      }

      int totalAmount = GetQuantityOfProductInEquipmentCard(id) + amount;
      SetNewQuantityOfEquipment(id, totalAmount);
    }

    private bool ProductIdExistsInEquipmentCards(int id) {
      for (var i = 0; i < EquipmentCards.Count; i++) {
        if (EquipmentCards[i].Id == id) {
          return true;
        }
      }

      return false;
    }

    private void AddNewEquipmentCard(int id, int quantity = 1) {
      EquipmentCards.Add(new EquipmentCard(id, quantity));
    }

    private int GetQuantityOfProductInEquipmentCard(int id) {
      for (var i = 0; i < EquipmentCards.Count; i++) {
        if (EquipmentCards[i].Id == id) {
          return EquipmentCards[i].Quantity;
        }
      }

      return 0;
    }

    private void SetNewQuantityOfEquipment(int id, int newQuantityOfProduct) {
      EquipmentCard equipmentCard = GetEquipmentCard(id);
      if (equipmentCard is null) {
        return;
      }

      equipmentCard.Quantity = newQuantityOfProduct;
    }

    [CanBeNull]
    private EquipmentCard GetEquipmentCard(int id) {
      for (var i = 0; i < EquipmentCards.Count; i++) {
        if (EquipmentCards[i].Id == id) {
          return EquipmentCards[i];
        }
      }

      Debug.LogWarning($"Снаряжение не найдено: id {id}");
      return null;
    }
  }
}