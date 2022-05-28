namespace Core.Mono.MainManagers {
  /// <summary>
  ///   Интерфейс для получения данных о начале игры.
  /// </summary>
  public interface ILauncher {
    /// <summary>
    ///   Это новая игра.
    /// </summary>
    /// <returns>Истина - новая игра, ложь - продолжение с сохранений.</returns>
    public bool IsNewGame();

    /// <summary>
    ///   Начать новую игру.
    /// </summary>
    /// <returns>Истина - начать новую игру, ложь - продолжить с сохранений.</returns>
    public bool StartNewGame();

    /// <summary>
    ///   Использовать сохранения.
    /// </summary>
    /// <returns>Истина - использовать сохраненияч, ложь - не использовать.</returns>
    public bool UseGameSave();

    /// <summary>
    ///   Данные загруженый для игры.
    /// </summary>
    /// <returns>Истина - данные загружены, ложь - даные ещё загружаються</returns>
    public bool DataLoaded();

    /// <summary>
    ///   Данные игры ещё инициализируются.
    /// </summary>
    /// <returns>Истина - данные инициализируются, ложь - данные уже инициализированы.</returns>
    public bool StillInitializing();
  }
}