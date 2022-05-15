using System;
using System.Collections.Generic;
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
using Core.Support.SaveSystem.Scribe;

namespace Core.Support.SaveSystem.SaveManagers {
  public interface ITest<T> {
    public T type { get; }
  }

  /// <summary>
  ///   Класс игровых сохранений.
  /// </summary>
  [Serializable]
  public class Scrolls {
    public readonly Dictionary<Type, IScribe> _scribes = new() {
      { typeof(IDiceRoll), new DiceRollScribe(new DiceRollDataScroll(DiceRollScribe.StartRollValues)) },
      { typeof(IEquipment), new EquipmentScribe() },
      { typeof(IEquipmentUsed), new EquipmentUsedScribe() },
      { typeof(IWallet), new WalletScribe() },
      { typeof(IGamePoints), new GamePointsScribe() },
      { typeof(IQualityPoints), new QualityPointsScribe() },
      { typeof(IRiskPoints), new RiskPointsScribe() },
      { typeof(IClassType), new ClassTypeScribe() }
    };
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
      foreach (IScribe scribe in _scribes.Values) {
        scribe.Init(this);
      }

      return this;
    }
  }
}