using System;
using Aberrance.Extensions;
using Core.Main.GameRule;
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
    private EquipmentUsedDataScroll _equipmentUsed;

    // ReSharper disable once CognitiveComplexity
    void IEquipmentUsed.SetEquipment(EquipmentsUsedId type, int newId) {
      Assert.IsTrue(newId >= 0);
      switch (type) {
        case EquipmentsUsedId.ArmorId:
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
               EquipmentsUsedId.ArmorId => _equipmentUsed.AnimalId,
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

    void IScribe.Init(Scrolls scrolls) {
      _equipmentUsed = new EquipmentUsedDataScroll(EquipmentIdConstants.NO_ARMOR_ID);
      if (scrolls.Null()) {
        return;
      }

      scrolls.EquipmentUsedDataScroll = _equipmentUsed;
    }

    void IScribe.Save(Scrolls scrolls) {
      scrolls.EquipmentUsedDataScroll = _equipmentUsed;
    }

    void IScribe.Loaded(Scrolls scrolls) {
      _equipmentUsed = scrolls.EquipmentUsedDataScroll;
    }
  }
}