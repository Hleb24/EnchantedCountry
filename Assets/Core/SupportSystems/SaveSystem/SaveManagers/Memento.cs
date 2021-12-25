using System;
using System.Collections.Generic;
using Core.SupportSystems.Data;
using Core.SupportSystems.SaveSystem.Saver;
using Core.SupportSystems.SaveSystem.Scribe;

namespace Core.SupportSystems.SaveSystem.SaveManagers {
  /// <summary>
  ///   Интерфей для получение информации о процессе инициализации данных.
  /// </summary>
  public interface IDataInit {
    public bool StillInitializing();
  }

  /// <summary>
  ///   Класс для работы с сохранёнными данных.
  /// </summary>
  public class Memento : IDataInit {
    private static bool StillInitializing { get; set; } = true;
    private Dictionary<Type, IScribe> _scribesMemento;
    private ISaver _saver;

    bool IDataInit.StillInitializing() {
      return StillInitializing;
    }

    /// <summary>
    ///   Инициализировать хранителей данных с загрузкой сохранений.
    /// </summary>
    public void Init(out bool isNewGame) {
      InitializeSaver();

      _scribesMemento = new Dictionary<Type, IScribe> {
        { typeof(DiceRollScribe), new DiceRollScribe() },
        { typeof(EquipmentsScribe), new EquipmentsScribe() },
        { typeof(EquipmentUsedScribe), new EquipmentUsedScribe() },
        { typeof(WalletScribe), new WalletScribe() },
        { typeof(GamePointsScribe), new GamePointsScribe() },
        { typeof(QualityPointsScribe), new QualityPointsScribe() },
        { typeof(RiskPointsScribe), new RiskPointsScribe() },
        { typeof(ClassTypeScribe), new ClassTypeScribe() }
      };

      foreach (IScribe scribe in _scribesMemento.Values) {
        scribe.Init();
      }

      ScribeDealer.Init(_scribesMemento);
      Load(out bool newGame);
      StillInitializing = false;
      isNewGame = newGame;
    }

    /// <summary>
    ///   Сохранить всё.
    /// </summary>
    public void Save() {
      _saver.Save(SaveAll());
    }

    /// <summary>
    /// Удалить сохранения.
    /// </summary>
    public void DeleteSave() {
      InitializeSaver();
      _saver.DeleteSave();
    }

    private void Load(out bool isNewGame) {
      LoadAll(out bool newGame);
      isNewGame = newGame;
    }

    private void InitializeSaver() {
#if UNITY_EDITOR
      _saver ??= new JsonSaver();
#elif UNITY_ANDROID
      _saver ??= new PrefsSaver();
#endif
    }

    private Scrolls SaveAll() {
      var save = new Scrolls();

      foreach (IScribe hollowData in _scribesMemento.Values) {
        hollowData.Save(save);
      }

      return save;
    }

    private void LoadAll(out bool newGame) {
      Scrolls scrolls = _saver.Load(out bool isNewGame);

      foreach (IScribe scribe in _scribesMemento.Values) {
        scribe.Loaded(scrolls);
      }

      newGame = isNewGame;
    }
  }
}