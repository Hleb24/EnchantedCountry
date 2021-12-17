using System;
using System.Collections.Generic;
using Core.EnchantedCountry.SupportSystems.Data;

namespace Core {
  
  /// <summary>
  /// Класс для работы с сохранёнными данных.
  /// </summary>
  public class Memento {
    private Dictionary<Type, ICommonData> _commonMemento;
    private Dictionary<Type, IFloatData> _floatMemento;
    private Dictionary<Type, IIntData> _integerMemento;
    private Dictionary<Type, IStringData> _stringMemento;
    private Dictionary<Type, IScribe> _hollowMemento;
    private ISaver _saver;

    /// <summary>
    /// Инициализировать хранителей данных.
    /// </summary>
    public void Init() {
      InitializeSaver();
      _commonMemento = new Dictionary<Type, ICommonData> {
        // { typeof(CharacterData), new CharacterData() },
      };

      _floatMemento = new Dictionary<Type, IFloatData> {
        // { typeof(CharacterData), new CharacterData() },
      };

      _integerMemento = new Dictionary<Type, IIntData> {
        {typeof(DiceRollData), new DiceRollData()}
      };
      
      _stringMemento = new Dictionary<Type, IStringData> {
        // { typeof(CharacterData), new CharacterData() },
      };
      
      _hollowMemento = new Dictionary<Type, IScribe> {
        // { typeof(CharacterData), new CharacterData() },
      };

      foreach (ICommonData commonData in _commonMemento.Values) {
        commonData.Init();
      }

      foreach (IFloatData floatData in _floatMemento.Values) {
        floatData.Init();
      }

      foreach (IIntData intData in _integerMemento.Values) {
        intData.Init();
      }
      
      foreach (IStringData stringData in _stringMemento.Values) {
        stringData.Init();
      }
      
      foreach (IScribe hollowData in _hollowMemento.Values) {
        hollowData.Init();
      }

      DataDealer.Init(_commonMemento, _floatMemento, _integerMemento, _stringMemento, _hollowMemento);
      Load();
    }

    /// <summary>
    /// Сохранить всё.
    /// </summary>
    public void Save() {
      _saver.Save(SaveAll());
    }

    private void Load() {
      LoadAll();
    }

    private void InitializeSaver() {
#if UNITY_EDITOR
      _saver = new JsonSaver();
#elif UNITY_ANDROID
      _saver = new PrefsSaver();
#endif
    }

    private SaveGame SaveAll() {
      var save = new SaveGame();
      foreach (ICommonData commonData in _commonMemento.Values) {
        commonData.Save(save);
      }

      foreach (IFloatData floatData in _floatMemento.Values) {
        floatData.Save(save);
      }

      foreach (IIntData intData in _integerMemento.Values) {
        intData.Save(save);
      }
      
      foreach (IStringData stringData in _stringMemento.Values) {
        stringData.Save(save);
      }
      
      foreach (IScribe hollowData in _hollowMemento.Values) {
        hollowData.Save(save);
      }

      return save;
    }

    private void LoadAll() {
      SaveGame saveGame = _saver.Load();
      foreach (ICommonData commonData in _commonMemento.Values) {
        commonData.Loaded(saveGame);
      }

      foreach (IFloatData floatData in _floatMemento.Values) {
        floatData.Loaded(saveGame);
      }

      foreach (IIntData intData in _integerMemento.Values) {
        intData.Loaded(saveGame);
      }
      
      foreach (IStringData stringData in _stringMemento.Values) {
        stringData.Loaded(saveGame);
      }
      
      foreach (IScribe hollowData in _hollowMemento.Values) {
        hollowData.Loaded(saveGame);
      }
    }
  }
}