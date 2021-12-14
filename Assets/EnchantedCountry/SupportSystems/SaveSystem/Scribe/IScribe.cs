namespace Core {
  /// <summary>
  /// Интерфейс для записи и загрузки сохранненных данных.
  /// </summary>
  /// <remarks>При работе с коллекциями сохранений он представляет интерфейс для полых "hollow" данных.</remarks>
  public interface IScribe {
    public void Init(SaveGame saveGame = null);
    public void Save(SaveGame saveGame);
    public void Loaded(SaveGame saveGame);
  }
}