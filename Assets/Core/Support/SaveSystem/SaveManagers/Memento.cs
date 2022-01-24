using System;
using System.Collections.Generic;
using Core.Support.Data;
using Core.Support.SaveSystem.Saver;
using Core.Support.SaveSystem.Scribe;

namespace Core.Support.SaveSystem.SaveManagers {
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
    private readonly Dictionary<Type, IScribe> _scribesMemento = new Dictionary<Type, IScribe> {
      { typeof(IDiceRoll), new DiceRollScribe() },
      { typeof(IEquipment), new EquipmentScribe() },
      { typeof(IEquipmentUsed), new EquipmentUsedScribe() },
      { typeof(IWallet), new WalletScribe() },
      { typeof(IGamePoints), new GamePointsScribe() },
      { typeof(IQualityPoints), new QualityPointsScribe() },
      { typeof(IRiskPoints), new RiskPointsScribe() },
      { typeof(IClassType), new ClassTypeScribe() }
    };
    private ISaver _saver;

    bool IDataInit.StillInitializing() {
      return StillInitializing;
    }

    /// <summary>
    ///   Инициализировать хранителей данных с загрузкой сохранений.
    /// </summary>
    public void Init(out bool isNewGame) {
      InitializeSaver();

      foreach (IScribe scribe in _scribesMemento.Values) {
        scribe.Init();
      }

      ScribeDealer.Init(_scribesMemento);
      Load(out bool newGame);
      StillInitializing = false;
      isNewGame = newGame;
      IsNewGame = isNewGame;
    }
    
    /// <summary>
    /// Это новая игра. Истинна - новая игра, ложь - продолжение с сохранений.
    /// </summary>
    public bool IsNewGame { get; private set; }

    /// <summary>
    ///   Сохранить всё.
    /// </summary>
    public void Save() {
      _saver.Save(SaveAll());
    }

    /// <summary>
    ///   Удалить сохранения.
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
      _saver ??= new Auditor.PrefsSaver();
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