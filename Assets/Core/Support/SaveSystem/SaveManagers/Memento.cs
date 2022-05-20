using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Main.Character.Class;
using Core.Main.Character.Item;
using Core.Main.Character.Quality;
using Core.Main.Dice;
using Core.Main.GameRule.Equipment;
using Core.Main.GameRule.Point;
using Core.Support.Data.ClassType;
using Core.Support.Data.DiceRoll;
using Core.Support.Data.Equipment;
using Core.Support.Data.GamePonts;
using Core.Support.Data.QualityPoints;
using Core.Support.Data.RiskPoints;
using Core.Support.Data.Wallet;
using Core.Support.SaveSystem.Saver;
using Core.Support.SaveSystem.Scribe;
using Cysharp.Threading.Tasks;
using UnityEngine;

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
    private readonly Dictionary<Type, IScribe> _scribesMemento = new() {
      { typeof(IDiceRoll), new DiceRollScribe(new DiceRollDataScroll(DiceRollScribe.StartRollValues)) },
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
    public void Init() {
      InitializeSaver();

      foreach (IScribe scribe in _scribesMemento.Values) {
        scribe.Init();
      }

      ScribeDealer.Init(_scribesMemento);
      Load().Forget();
    }

    /// <summary>
    ///   Сохранить всё.
    /// </summary>
    public void Save() {
      _saver.Save(SaveAll(), SaveHandler).Forget();
    }

    /// <summary>
    ///   Сохранить всё при выходе с игры.
    /// </summary>
    public void SaveOnQuit() {
      _saver.Save(SaveAllOnQuit(), SaveHandler).Forget();
    }

    /// <summary>
    ///   Удалить сохранения.
    /// </summary>
    public void DeleteSave() {
      InitializeSaver();
      _saver.DeleteSave();
    }

    private async UniTaskVoid Load() {
      IsNewGame = await LoadAll();
      StillInitializing = false;
    }

    private void InitializeSaver() {
#if UNITY_EDITOR
      _saver ??= new JsonScrollSaver();
#elif UNITY_ANDROID
      _saver ??= new Auditor.PrefsSaver();
#endif
    }

    private Scrolls SaveAll() {
      var save = new Scrolls();

      foreach (IScribe hollowData in _scribesMemento.Values) {
        hollowData.Save(save);
      }

      save.isNewGame = false;
      return save;
    }

    private Scrolls SaveAllOnQuit() {
      var save = new Scrolls();

      foreach (IScribe hollowData in _scribesMemento.Values) {
        hollowData.SaveOnQuit(save);
      }

      return save;
    }

    private async UniTask<bool> LoadAll() {
      Scrolls scrolls = await _saver.Load(LoadHandler);
      if (scrolls.DiceRollDataScroll.DiceRollValues is null) {
        Debug.LogWarning("Null In Load All");
      }
      foreach (IScribe scribe in _scribesMemento.Values) {
        scribe.Loaded(scrolls);
      }

      return await IsNewGameAsync(scrolls);
    }

    private void SaveHandler(Exception exception) {
      Debug.LogError($"Exeption on save {exception.Message} trace {exception.StackTrace}");
    }

    private void LoadHandler(Exception exception) {
      Debug.LogError($"Exeption on load {exception.Message} trace {exception.StackTrace}");
    }

    private async UniTask<bool> IsNewGameAsync(Scrolls scrolls) {
      return await Task.Run(() => scrolls.isNewGame);
    }

    /// <summary>
    ///   Это новая игра. Истинна - новая игра, ложь - продолжение с сохранений.
    /// </summary>
    public bool? IsNewGame { get; private set; }
  }
}