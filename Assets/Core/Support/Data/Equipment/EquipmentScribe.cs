using System;
using System.Collections.Generic;
using Aberrance.Extensions;
using Core.Main.GameRule.Equipment;
using Core.Support.SaveSystem.SaveManagers;
using Core.Support.SaveSystem.Scribe;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Assertions;

namespace Core.Support.Data.Equipment {
  /// <summary>
  ///   Класс для хранения данных об снаряжении.
  /// </summary>
  [Serializable]
  public class EquipmentScribe : IScribe, IEquipment {
    private readonly List<int> _startEquipments = new() {
      EquipmentIdConstants.POCKETS,
      EquipmentIdConstants.NO_ARMOR_ID
    };

    private EquipmentsDataScroll _equipments;

    void IEquipment.ChangeQuantity(int id, int amount) {
      Assert.IsTrue(id >= 0);
      if (amount >= 0) {
        _equipments.IncreaseQuantityOfEquipment(id, amount);
      } else {
        _equipments.DecreaseQuantityOfEquipment(id, amount);
      }
    }

    List<EquipmentCard> IEquipment.GetEquipmentCards() {
      Assert.IsNotNull(_equipments.EquipmentCards);
      return _equipments.EquipmentCards;
    }

    void IScribe.Init(Scrolls scrolls) {
      _equipments = new EquipmentsDataScroll(_startEquipments);
      if (scrolls.Null()) {
        return;
      }

      scrolls.EquipmentsDataScroll = _equipments;
    }

    void IScribe.Save(Scrolls scrolls) {
      scrolls.EquipmentsDataScroll = _equipments;
    }

    void IScribe.Loaded(Scrolls scrolls) {
      _equipments.EquipmentCards = scrolls.EquipmentsDataScroll.EquipmentCards;
    }
  }

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
      if (ProductIdExistsInEquipmentCards(id).False()) {
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