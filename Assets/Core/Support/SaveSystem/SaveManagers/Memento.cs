using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aberrance.UnityEngine.Saver;
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
  ///   Класс для работы с сохранёнными данных.
  /// </summary>
  public class Memento : IMemento {
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
    private string _pathToFile = "";

   
    public void Init() {
      InitializeSaver();

      foreach (IScribe scribe in _scribesMemento.Values) {
        scribe.Init();
      }

      ScribeDealer.Init(_scribesMemento);
      Load().Forget();
    }

    
    public void Save() {
      _saver.SaveAsync(SaveAll(), _pathToFile, SaveHandler).Forget();
    }

    
    public void SaveOnQuit() {
      _saver.SaveAsync(SaveAllOnQuit(), _pathToFile, SaveHandler).Forget();
    }

   
    public void DeleteSave() {
      InitializeSaver();
      _saver.DeleteSaves(_pathToFile);
    }

    private async UniTaskVoid Load() {
      IsNewGame = await LoadAll();
      StillInitializing = false;
    }

    private void InitializeSaver() {
#if UNITY_EDITOR
      _pathToFile = SavePath.PathToJsonFile;
      _saver ??= new JsonSaver();
#elif UNITY_ANDROID
      // _saver ??= new PrefsSaver();
      // _pathToFile = SavePath.PathToPrefsFile;
#endif
    }

    private Scrolls SaveAll() {
      var save = new Scrolls();

      foreach (IScribe hollowData in _scribesMemento.Values) {
        hollowData.Save(save);
      }

      save.IsNewGame = false;
      return save;
    }

    private Scrolls SaveAllOnQuit() {
      var save = new Scrolls();

      foreach (IScribe scribe in _scribesMemento.Values) {
        scribe.SaveOnQuit(save);
      }

      save.IsNewGame = false;
      return save;
    }

    private async UniTask<bool> LoadAll() {
      var scrolls = await _saver.LoadAsync<Scrolls>(_pathToFile, LoadHandler);
      scrolls ??= new Scrolls().NewScrollGame();
      foreach (IScribe scribe in _scribesMemento.Values) {
        scribe.Loaded(scrolls);
      }

      return await IsNewGameAsync(scrolls);
    }

    private void SaveHandler(Exception exception) {
      Debug.LogError($"Exeption on save {exception.Message},type {exception}, trace {exception.StackTrace}");
    }

    private void LoadHandler(Exception exception) {
      Debug.LogError($"Exeption on load {exception.Message}, type {exception}, trace {exception.StackTrace}");
    }

    private async UniTask<bool> IsNewGameAsync(Scrolls scrolls) {
      return await Task.Run(() => scrolls.IsNewGame);
    }

    /// <summary>
    ///   Это новая игра. Истинна - новая игра, ложь - продолжение с сохранений.
    /// </summary>
    public bool? IsNewGame { get; private set; }
  }
}