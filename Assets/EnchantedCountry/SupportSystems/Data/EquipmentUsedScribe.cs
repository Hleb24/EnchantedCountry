﻿using System;
using Core.EnchantedCountry.CoreEnchantedCountry.GameRule.EquipmentIdConstants;
using UnityEngine;
using UnityEngine.Assertions;

namespace Core.EnchantedCountry.SupportSystems.Data {
  /// <summary>
  ///   Перечисление содержит типы индефикаторов экипированого снаряжения.
  /// </summary>
  public enum EquipmentsUsedId {
    ArmorId,
    ShieldId,
    OneHandedId,
    TwoHandedId,
    RangeId,
    ProjectilesId,
    BagId,
    AnimalId,
    CarriageId
  }

  /// <summary>
  ///   Интерфейс для работы с экипированым снаряжение.
  /// </summary>
  public interface IEquipmentUsed {
    /// <summary>
    ///   Присвоить новое значение индефикатора эикипрированого снаяржения.
    /// </summary>
    /// <param name="type">Тип экиприованого снаряжения.</param>
    /// <param name="newId">Новый индефикатор для экипированого снаряжения.</param>
    public void SetEquipment(EquipmentsUsedId type, int newId);

    /// <summary>
    ///   Получить значение индефикатора эикипрированого снаяржения.
    /// </summary>
    /// <param name="type">Тип экиприованого снаряжения.</param>
    /// <returns>Индефикатора эикипрированого снаяржения.</returns>
    public int GetEquipment(EquipmentsUsedId type);
  }

  /// <summary>
  ///   Класс для хранения данных об экипированом снаряжении.
  /// </summary>
  [Serializable]
  public class EquipmentUsedScribe : IScribe, IEquipmentUsed {
    private EquipmentUsedDataSave _equipmentUsed;

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

    void IScribe.Init(SaveGame saveGame) {
      _equipmentUsed = new EquipmentUsedDataSave(EquipmentIdConstants.NoArmorId);
      if (saveGame is null) {
        return;
      }

      saveGame.EquipmentUsedDataSave = _equipmentUsed;
    }

    void IScribe.Save(SaveGame saveGame) {
      saveGame.EquipmentUsedDataSave = _equipmentUsed;
    }

    void IScribe.Loaded(SaveGame saveGame) {
      _equipmentUsed = saveGame.EquipmentUsedDataSave;
    }
  }

  [Serializable]
  public struct EquipmentUsedDataSave {
    public int ArmorId;
    public int ShieldId;
    public int OneHandedId;
    public int TwoHandedId;
    public int RangeId;
    public int ProjectilesId;
    public int BagId;
    public int AnimalId;
    public int CarriageId;

    public EquipmentUsedDataSave(int armorId) {
      ArmorId = armorId;
      ShieldId = default;
      OneHandedId = default;
      TwoHandedId = default;
      RangeId = default;
      ProjectilesId = default;
      BagId = default;
      AnimalId = default;
      AnimalId = default;
      CarriageId = default;
    }
  }
}