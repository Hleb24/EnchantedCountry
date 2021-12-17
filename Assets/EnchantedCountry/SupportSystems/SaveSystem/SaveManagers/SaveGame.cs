using System;
using System.Collections.Generic;
using Core.EnchantedCountry.SupportSystems.Data;

namespace Core {
  /// <summary>
  /// Класс игровых сохранений.
  /// </summary>
  [Serializable]
  public class SaveGame {
    public CharacterDataSave CharacterDataSave;
    public DiceRollDataSave DiceRollDataSave;

    public SaveGame NewSaveGame() {
      var save = new SaveGame();
      var dataDict = new Dictionary<Type, ICommonData>();
      foreach (ICommonData commonData in dataDict.Values) {
        commonData.Init(save);
      }

      var floatDict = new Dictionary<Type, IFloatData>();
      foreach (IFloatData floatData in floatDict.Values) {
        floatData.Init(save);
      }

      var intDict = new Dictionary<Type, IIntData>{
        {typeof(DiceRollData), new DiceRollData()}
      };
      foreach (IIntData intData in intDict.Values) {
        intData.Init(save);
      }
      
      var stringDict = new Dictionary<Type, IStringData>();
      foreach (IStringData stringData in stringDict.Values) {
        stringData.Init(save);
      }
      
      var hollowDict = new Dictionary<Type, IScribe>();
      foreach (IScribe hollowData in hollowDict.Values) {
        hollowData.Init(save);
      }

      return save;
    }
  }
}