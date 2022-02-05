using System;

namespace Core.Main.GameRule.Initiative {
  /// <summary>
  /// Интерфейс для сравнивания инициативы.
  /// </summary>
  public interface IInitiative : IComparable<IInitiative> {
    /// <summary>
    /// Значение инициативы.
    /// </summary>
    public int Initiative { get; set; }
  }
}