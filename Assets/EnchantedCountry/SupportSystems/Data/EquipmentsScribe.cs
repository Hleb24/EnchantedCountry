﻿using System;
using System.Collections.Generic;
using Core.EnchantedCountry.CoreEnchantedCountry.Character.Equipment;
using Core.EnchantedCountry.CoreEnchantedCountry.GameRule.EquipmentIdConstants;
using UnityEngine;
using UnityEngine.Assertions;

namespace Core.EnchantedCountry.SupportSystems.Data {
  public interface IEquipment {
    public void ChangeQuantity(int id, int amount);
    public List<EquipmentCard> GetEquipmentCards();
  }

  [Serializable]
  public class EquipmentsScribe : IScribe, IEquipment {
    private readonly List<int> _startEquipments = new List<int> {
      EquipmentIdConstants.Pockets,
      EquipmentIdConstants.NoArmorId
    };

    private EquipmentsDataSave _equipments;

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

    void IScribe.Init(SaveGame saveGame) {
      _equipments = new EquipmentsDataSave(_startEquipments);
      if (saveGame is null) {
        return;
      }

      saveGame.EquipmentsDataSave = _equipments;
    }

    void IScribe.Save(SaveGame saveGame) {
      saveGame.EquipmentsDataSave = _equipments;
    }

    void IScribe.Loaded(SaveGame saveGame) {
      _equipments.EquipmentCards = saveGame.EquipmentsDataSave.EquipmentCards;
    }
  }

  [Serializable]
  public struct EquipmentsDataSave {
    private const int StartQuantity = 1;
    private readonly List<int> _startEquipments;
    public List<EquipmentCard> EquipmentCards;

    public EquipmentsDataSave(IReadOnlyList<int> startEquipmentsId) {
      EquipmentCards = new List<EquipmentCard>();
      _startEquipments = new List<int>();
      for (var i = 0; i < startEquipmentsId.Count; i++) {
        EquipmentCards.Add(new EquipmentCard(startEquipmentsId[i], StartQuantity));
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
        int index = i;
        if (QuantityOfProductInEquipmentCardsEqualOrLessZero(EquipmentCards[index].ID)) {
          RemoveAtIndex(index);
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
      if (!ProductIdExistsInEquipmentCards(id)) {
        AddNewEquipmentCard(id, 0);
      }

      int totalAmount = GetQuantityOfProductInEquipmentCard(id) + amount;
      SetNewQuantityOfEquipment(id, totalAmount);
    }

    private bool ProductIdExistsInEquipmentCards(int id) {
      for (var i = 0; i < EquipmentCards.Count; i++) {
        if (EquipmentCards[i].ID == id) {
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
        if (EquipmentCards[i].ID == id) {
          return EquipmentCards[i].Quantity;
        }
      }

      return 0;
    }

    private void SetNewQuantityOfEquipment(int id, int newQuantityOfProduct) {
      EquipmentCard equipmentCard = GetEquipmentCard(id);
      equipmentCard.Quantity = newQuantityOfProduct;
    }

    private EquipmentCard GetEquipmentCard(int id) {
      for (var i = 0; i < EquipmentCards.Count; i++) {
        if (EquipmentCards[i].ID == id) {
          return EquipmentCards[i];
        }
      }

      return null;
    }
  }
}