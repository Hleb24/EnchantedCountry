using System;
using System.Collections.Generic;
using Core.EnchantedCountry.SupportSystems.Data;

namespace Core {
  /// <summary>
  ///   Класс для работы с сохранёнными данных.
  /// </summary>
  public class Memento {
    private Dictionary<Type, IScribe> _scribesMemento;
    private ISaver _saver;

    /// <summary>
    ///   Инициализировать хранителей данных.
    /// </summary>
    public void Init() {
      InitializeSaver();

      _scribesMemento = new Dictionary<Type, IScribe> {
        { typeof(DiceRollData), new DiceRollData() }
      };

      foreach (IScribe scribe in _scribesMemento.Values) {
        scribe.Init();
      }

      DataDealer.Init(_scribesMemento);
      Load();
    }

    /// <summary>
    ///   Сохранить всё.
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

      foreach (IScribe hollowData in _scribesMemento.Values) {
        hollowData.Save(save);
      }

      return save;
    }

    private void LoadAll() {
      SaveGame saveGame = _saver.Load();

      foreach (IScribe scribe in _scribesMemento.Values) {
        scribe.Loaded(saveGame);
      }
    }
  }
}