using System;
using System.Collections.Generic;
using Core.EnchantedCountry.SupportSystems.Data;
using Core.EnchantedCountry.SupportSystems.SaveSystem.Saver;
using Core.EnchantedCountry.SupportSystems.SaveSystem.Scribe;

namespace Core.EnchantedCountry.SupportSystems.SaveSystem.SaveManagers {
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

    private Scrolls SaveAll() {
      var save = new Scrolls();

      foreach (IScribe hollowData in _scribesMemento.Values) {
        hollowData.Save(save);
      }

      return save;
    }

    private void LoadAll() {
      Scrolls scrolls = _saver.Load(out bool isNewGame);

      foreach (IScribe scribe in _scribesMemento.Values) {
        scribe.Loaded(scrolls);
      }
    }
  }
}