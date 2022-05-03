using System;
using System.Collections.Generic;
using Aberrance.Extensions;
using Core.Main.GameRule.Equipment;
using Core.Support.SaveSystem.SaveManagers;
using Core.Support.SaveSystem.Scribe;
using UnityEngine;
using UnityEngine.Assertions;

namespace Core.Support.Data.Equipment {
  /// <summary>
  ///   Класс для хранения данных об экипированом снаряжении.
  /// </summary>
  [Serializable]
  public class EquipmentUsedScribe : IScribe, IEquipmentUsed {
    private static readonly Stack<EquipmentUsedScribe> _equipmentUsedStack = new();
    private static EquipmentUsedScribe _originEquipmentUsedScribe;
    private EquipmentUsedDataScroll _equipmentUsed;

    // ReSharper disable once CognitiveComplexity
    void IEquipmentUsed.SetEquipment(EquipmentsUsedId type, int newId) {
      Assert.IsTrue(newId >= 0);
      UpdateLastChanged();
      switch (type) {
        case EquipmentsUsedId.ArmorId:
          Debug.LogWarning("Set id" + newId);
          _equipmentUsed.ArmorId = newId;
          break;
        case EquipmentsUsedId.ShieldId:
          _equipmentUsed.ShieldId = newId;
          break;
        case EquipmentsUsedId.OneHandedId:
          _equipmentUsed.OneHandedId = newId;
          break;
        case EquipmentsUsedId.TwoHandedId:
          _equipmentUsed.TwoHandedId = newId;
          break;
        case EquipmentsUsedId.RangeId:
          _equipmentUsed.RangeId = newId;
          break;
        case EquipmentsUsedId.ProjectilesId:
          _equipmentUsed.ProjectilesId = newId;
          break;
        case EquipmentsUsedId.BagId:
          _equipmentUsed.BagId = newId;
          break;
        case EquipmentsUsedId.AnimalId:
          _equipmentUsed.AnimalId = newId;
          break;
        case EquipmentsUsedId.CarriageId:
          _equipmentUsed.CarriageId = newId;
          break;
        default:
          Debug.LogWarning("Тип экипированого снаряжения не найден!");
          break;
      }
    }

    int IEquipmentUsed.GetEquipment(EquipmentsUsedId type) {
      return type switch {
               EquipmentsUsedId.ArmorId => _equipmentUsed.ArmorId,
               EquipmentsUsedId.ShieldId => _equipmentUsed.ShieldId,
               EquipmentsUsedId.OneHandedId => _equipmentUsed.OneHandedId,
               EquipmentsUsedId.TwoHandedId => _equipmentUsed.TwoHandedId,
               EquipmentsUsedId.RangeId => _equipmentUsed.RangeId,
               EquipmentsUsedId.ProjectilesId => _equipmentUsed.ProjectilesId,
               EquipmentsUsedId.BagId => _equipmentUsed.BagId,
               EquipmentsUsedId.AnimalId => _equipmentUsed.AnimalId,
               EquipmentsUsedId.CarriageId => _equipmentUsed.CarriageId,
               _ => default
             };
    }

    public T Clone<T>() {
      return (T)MemberwiseClone();
    }

    public T CloneWithTracking<T>() {
      IsTracking = true;
      Debug.LogWarning("CloneWithTracking");
      

      var clone = Clone<T>();
      if (_equipmentUsedStack.Contains(clone as EquipmentUsedScribe).False()) {
        _equipmentUsedStack.Push(clone as EquipmentUsedScribe);
        Debug.LogWarning("Push");
      }
      return clone;
    }

    public void ReplaceOriginal<T>(T newOriginValue) {
      if (newOriginValue is EquipmentUsedScribe equipmentUsedScribe) {
        _originEquipmentUsedScribe = equipmentUsedScribe;
      }
    }

    public void ReplaceOriginal() {
      _originEquipmentUsedScribe = this;
    }

    void IScribe.SaveOnQuit(Scrolls scrolls) {
      Debug.LogWarning($"Count {_equipmentUsedStack.Count}");
      foreach (EquipmentUsedScribe equipmentUsedScribe in _equipmentUsedStack) {
        bool changeOrigin = ScribeHandler.ChangeOrigin(equipmentUsedScribe, equipmentUsedScribe, _originEquipmentUsedScribe);
        if (changeOrigin) {
          _originEquipmentUsedScribe = equipmentUsedScribe;
        }

        equipmentUsedScribe.IsTracking = false;
      }

      _equipmentUsedStack.Clear();
      scrolls.EquipmentUsedDataScroll = _originEquipmentUsedScribe._equipmentUsed;
    }

    void IScribe.Init(Scrolls scrolls) {
      _equipmentUsed = new EquipmentUsedDataScroll(EquipmentIdConstants.NO_ARMOR_ID);
      UpdateLastChanged();
      _originEquipmentUsedScribe = this;
      _equipmentUsedStack.Push(this);
      if (scrolls.Null()) {
        return;
      }

      scrolls.EquipmentUsedDataScroll = _originEquipmentUsedScribe._equipmentUsed;
    }

    void IScribe.Save(Scrolls scrolls) {
      scrolls.EquipmentUsedDataScroll = _originEquipmentUsedScribe._equipmentUsed;
    }

    void IScribe.Loaded(Scrolls scrolls) {
      _equipmentUsed = scrolls.EquipmentUsedDataScroll;
      _originEquipmentUsedScribe = this;
    }

    private void UpdateLastChanged() {
      LastChanged = DateTime.Now;
    }

    public bool IsTracking { get; private set; }

    public DateTime LastChanged { get; private set; }
  }
}