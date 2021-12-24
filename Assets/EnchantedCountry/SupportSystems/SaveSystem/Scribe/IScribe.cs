using Core.EnchantedCountry.SupportSystems.SaveSystem.SaveManagers;

namespace Core.EnchantedCountry.SupportSystems.SaveSystem.Scribe {
  /// <summary>
  ///   Интерфейс для записи и загрузки сохранненных данных.
  /// </summary>
  public interface IScribe {
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
    ///   Загрузить данные сохранений.
    /// </summary>
    /// <param name="scrolls">Данные сохранений.</param>
    public void Loaded(Scrolls scrolls);
  }
}