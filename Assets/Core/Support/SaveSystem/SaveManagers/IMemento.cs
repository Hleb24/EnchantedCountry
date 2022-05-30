namespace Core.Support.SaveSystem.SaveManagers {
  public interface IMemento {
    /// <summary>
    ///   Инициализировать хранителей данных с загрузкой сохранений.
    /// </summary>
    public void Init();

    /// <summary>
    ///   Сохранить всё.
    /// </summary>
    public void Save();

    /// <summary>
    ///   Сохранить всё при выходе с игры.
    /// </summary>
    public void SaveOnQuit();

    /// <summary>
    ///   Удалить сохранения.
    /// </summary>
    public void DeleteSave();
    
    public bool? IsNewGame { get; }
  }
}