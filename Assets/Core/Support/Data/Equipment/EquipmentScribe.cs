using System;
using System.Collections.Generic;
using Aberrance.Extensions;
using Core.Main.GameRule.Equipment;
using Core.Support.SaveSystem.SaveManagers;
using Core.Support.SaveSystem.Scribe;
using UnityEngine.Assertions;

namespace Core.Support.Data.Equipment {
  /// <summary>
  ///   Класс для хранения данных об снаряжении.
  /// </summary>
  [Serializable]
  public class EquipmentScribe : IScribe, IEquipment {
    private static EquipmentScribe _originEquipmentScribe;
    private readonly List<int> _startEquipments = new() {
      EquipmentIdConstants.POCKETS,
      EquipmentIdConstants.NO_ARMOR_ID
    };

    private EquipmentsDataScroll _equipments;

    void IEquipment.ChangeQuantity(int id, int amount) {
      Assert.IsTrue(id >= 0);
      UpdateLastChanged();
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

    public T Clone<T>() {
      return (T)MemberwiseClone();
    }

    public T CloneWithTracking<T>() {
      IsTracking = true;
      return Clone<T>();
    }

    public void ReplaceOriginal<T>(T newOriginValue) {
      if (newOriginValue is EquipmentScribe equipmentScribe) {
        _originEquipmentScribe = equipmentScribe;
      }
    }

    public void ReplaceOriginal() {
      _originEquipmentScribe = this;
    }

    void IScribe.SaveOnQuit(Scrolls scrolls) {
      bool changeOrigin = ScribeHandler.ChangeOrigin(this, this, _originEquipmentScribe);
      if (changeOrigin) {
        _originEquipmentScribe = this;
      }

      IsTracking = false;
      scrolls.EquipmentsDataScroll = _originEquipmentScribe._equipments;
    }

    void IScribe.Init(Scrolls scrolls) {
      _equipments = new EquipmentsDataScroll(_startEquipments);
      UpdateLastChanged();
      _originEquipmentScribe = this;
      if (scrolls.IsNull()) {
        return;
      }

      scrolls.EquipmentsDataScroll = _originEquipmentScribe._equipments;
    }

    void IScribe.Save(Scrolls scrolls) {
      scrolls.EquipmentsDataScroll = _originEquipmentScribe._equipments;
    }

    void IScribe.Loaded(Scrolls scrolls) {
      _equipments.EquipmentCards = scrolls.EquipmentsDataScroll.EquipmentCards;
      _originEquipmentScribe = this;
    }

    private void UpdateLastChanged() {
      LastChanged = DateTime.Now;
    }

    public bool IsTracking { get; private set; }

    public DateTime LastChanged { get; private set; }
  }
}