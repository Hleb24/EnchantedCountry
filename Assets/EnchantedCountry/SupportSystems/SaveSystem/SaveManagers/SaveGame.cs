using System;
using System.Collections.Generic;
using Core.EnchantedCountry.SupportSystems.Data;

namespace Core {
  /// <summary>
  ///   Класс игровых сохранений.
  /// </summary>
  [Serializable]
  public class SaveGame {
    public DiceRollDataSave DiceRollDataSave;
    public EquipmentsDataSave EquipmentsDataSave;
    public EquipmentUsedDataSave EquipmentUsedDataSave;
    public WalletDataSave WalletDataSave;

    /// <summary>
    ///   Создаёт новые сохранённые данные.
    /// </summary>
    /// <returns></returns>
    public SaveGame NewSaveGame() {
      var save = new SaveGame();

      var scribes = new Dictionary<Type, IScribe> {
        { typeof(DiceRollScribe), new DiceRollScribe() },
        { typeof(EquipmentsScribe), new EquipmentsScribe() },
        { typeof(EquipmentUsedScribe), new EquipmentUsedScribe() },
        { typeof(WalletScribe), new WalletScribe() }
        
      };
      foreach (IScribe scribe in scribes.Values) {
        scribe.Init(save);
      }

      return save;
    }
  }
}