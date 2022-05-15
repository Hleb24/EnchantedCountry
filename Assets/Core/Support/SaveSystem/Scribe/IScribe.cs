using System;
using Core.Support.SaveSystem.SaveManagers;
using ICloneable = Core.Main.GameRule.Equipment.ICloneable;

namespace Core.Support.SaveSystem.Scribe {
  /// <summary>
  ///   Интерфейс для записи и загрузки сохранненных данных.
  /// </summary>
  public interface IScribe : ICloneable {
    /// <summary>
    ///   Инициализировать данные сохранений.
    /// </summary>
    /// <param name="scrolls">Данные сохранений.</param>
    public void Init(Scrolls scrolls = null);

    /// <summary>
    ///   Сохранить данные.
    /// </summary>
    /// <param name="scrolls">Данные сохранений.</param>
    public void Save(Scrolls scrolls);

    /// <summary>
    ///   Сохранить данные при выходе с игры.
    /// </summary>
    /// <param name="scrolls">Данные сохранений при выходе с игры.</param>
    public void SaveOnQuit(Scrolls scrolls);

    /// <summary>
    ///   Загрузить данные сохранений.
    /// </summary>
    /// <param name="scrolls">Данные сохранений.</param>
    public void Loaded(Scrolls scrolls);

    public DateTime LastChanged { get; }
  }
}