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

    public SaveGame NewSaveGame() {
      var save = new SaveGame();

      var scribes = new Dictionary<Type, IScribe> {
        { typeof(DiceRollData), new DiceRollData() }
      };
      foreach (IScribe hollowData in scribes.Values) {
        hollowData.Init(save);
      }

      return save;
    }
  }
}