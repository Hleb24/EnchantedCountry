using System;
using System.Collections.Generic;

namespace Core {
  [Serializable]
  public class Save {
    public CharacterDataSave CharacterDataSave;

    public Save LoadDefault() {
      var save = new Save();
      var dataDict = new Dictionary<Type, IData>();
      foreach (IData data in dataDict.Values) {
        data.Init(save);
      }

      var floatDict = new Dictionary<Type, IFloatData>();
      foreach (IFloatData data in floatDict.Values) {
        data.Init(save);
      }

      var intDict = new Dictionary<Type, IIntData>();
      foreach (IIntData data in intDict.Values) {
        data.Init(save);
      }

      return save;
    }
  }
}