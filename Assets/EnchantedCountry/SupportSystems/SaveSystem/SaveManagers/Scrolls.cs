using System;
using System.Collections.Generic;
using Core.EnchantedCountry.SupportSystems.Data;

namespace Core {
  /// <summary>
  ///   Класс игровых сохранений.
  /// </summary>
  [Serializable]
  public class Scrolls {
    public DiceRollDataScroll DiceRollDataScroll;
    public EquipmentsDataScroll EquipmentsDataScroll;
    public EquipmentUsedDataScroll EquipmentUsedDataScroll;
    public WalletDataScroll WalletDataScroll;
    public GamePointsDataScroll GamePointsDataScroll;
    public QualityPointsDataScroll QualityPointsDataScroll;
    public RiskPointDataScroll RiskPointsDataScroll;
    public ClassTypeDataScroll ClassTypeDataScroll;

    /// <summary>
    ///   Создаёт новые сохранённые данные.
    /// </summary>
    /// <returns></returns>
    public Scrolls NewScrollGame() {
      var save = new Scrolls();

      var scribes = new Dictionary<Type, IScribe> {
        { typeof(DiceRollScribe), new DiceRollScribe() },
        { typeof(EquipmentsScribe), new EquipmentsScribe() },
        { typeof(EquipmentUsedScribe), new EquipmentUsedScribe() },
        { typeof(WalletScribe), new WalletScribe() },
        { typeof(GamePointsScribe), new GamePointsScribe() },
        { typeof(QualityPointsScribe), new QualityPointsScribe() },
        { typeof(RiskPointsScribe), new RiskPointsScribe() },
        { typeof(ClassTypeScribe), new ClassTypeScribe() }
      };
      foreach (IScribe scribe in scribes.Values) {
        scribe.Init(save);
      }

      return save;
    }
  }
}