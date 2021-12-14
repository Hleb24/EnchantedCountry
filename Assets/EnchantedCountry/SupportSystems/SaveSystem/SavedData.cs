using System;
using System.Collections.Generic;

namespace Core {
  public class SavedData {
    private Dictionary<Type, IData> _data;
    private Dictionary<Type, IFloatData> _floatData;
    private Dictionary<Type, IIntData> _intData;
    private ISaver _saver;

    public void Init() {
      DefineSerializationType();
      _data = new Dictionary<Type, IData> {
        // { typeof(CharacterData), new CharacterData() },
      };

      _floatData = new Dictionary<Type, IFloatData> {
        // { typeof(CharacterData), new CharacterData() },
      };

      _intData = new Dictionary<Type, IIntData> {
        // { typeof(CharacterData), new CharacterData() },
      };

      foreach (IData data in _data.Values) {
        data.Init();
      }

      foreach (IFloatData data in _floatData.Values) {
        data.Init();
      }

      foreach (IIntData data in _intData.Values) {
        data.Init();
      }

      DataDealer.Init(_data, _floatData, _intData);
      LoadSave();
    }

    public void Save() {
      _saver.Save(SaveAll());
    }

    private void LoadSave() {
      LoadAll();
    }

    private void DefineSerializationType() {
#if UNITY_EDITOR
      _saver = new JsonSaver();
#elif UNITY_ANDROID
      _saver = new PrefsSaver();
#endif
    }

    private Save SaveAll() {
      var save = new Save();
      foreach (IData data in _data.Values) {
        data.Save(save);
      }

      foreach (IFloatData data in _floatData.Values) {
        data.Save(save);
      }

      foreach (IIntData data in _intData.Values) {
        data.Save(save);
      }

      return save;
    }

    private void LoadAll() {
      Save save = _saver.Load();
      foreach (IData data in _data.Values) {
        data.Loaded(save);
      }

      foreach (IFloatData data in _floatData.Values) {
        data.Loaded(save);
      }

      foreach (IIntData data in _intData.Values) {
        data.Loaded(save);
      }
    }
  }
}