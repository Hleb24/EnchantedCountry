namespace Core {
  /// <summary>
  ///   Интерфейс для записи и загрузки сохранненных данных.
  /// </summary>
  public interface IScribe {
    /// <summary>
    ///   Инициализировать данные сохранений.
    /// </summary>
    /// <param name="saveGame">Данные сохранений.</param>
    public void Init(SaveGame saveGame = null);

    /// <summary>
    ///   Сохранить данные.
    /// </summary>
    /// <param name="saveGame">Данные сохранений.</param>
    public void Save(SaveGame saveGame);

    /// <summary>
    ///   Загрузить данные сохранений.
    /// </summary>
    /// <param name="saveGame">Данные сохранений.</param>
    public void Loaded(SaveGame saveGame);
  }
}