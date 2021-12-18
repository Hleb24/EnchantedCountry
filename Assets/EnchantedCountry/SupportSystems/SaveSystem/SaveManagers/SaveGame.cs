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

    /// <summary>
    ///   Создаёт новые сохранённые данные.
    /// </summary>
    /// <returns></returns>
    public SaveGame NewSaveGame() {
      var save = new SaveGame();

      var scribes = new Dictionary<Type, IScribe> {
        { typeof(DiceRollScribe), new DiceRollScribe() },
        { typeof(EquipmentsScribe), new EquipmentsScribe() }
      };
      foreach (IScribe scribe in scribes.Values) {
        scribe.Init(save);
      }

      return save;
    }
  }
}