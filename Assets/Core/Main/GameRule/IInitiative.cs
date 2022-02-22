using System;

namespace Core.Main.GameRule {
  /// <summary>
  ///   Интерфейс для сравнивания инициативы.
  /// </summary>
  public interface IInitiative : IComparable<IInitiative> {
    /// <summary>
    ///   Значение инициативы.
    /// </summary>
    public int Initiative { get; set; }
  }
}