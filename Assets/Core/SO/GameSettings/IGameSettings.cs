using Core.Mono.BaseClass;

namespace Core.SO.GameSettings {
  /// <summary>
  ///   Interface for game settings.
  /// </summary>
  public interface IGameSettings {
    /// <summary>
    ///   Start a new game.
    /// </summary>
    /// <returns>True - start a new game, false - continue the current game.</returns>
    public bool StartNewGame();

    /// <summary>
    ///   Use saves.
    /// </summary>
    /// <returns>True - use, false - do not use.</returns>
    public bool UseGameSave();

    /// <summary>
    ///   Get a scene that will be forced to open right after the scene <see cref="Scene.Intro" />.
    /// </summary>
    /// <returns>Target scene.</returns>
    public Scene GetTargetScene();

    /// <summary>
    ///   Get the starting scene.
    /// </summary>
    /// <returns>starting scene.</returns>
    public Scene GetNewGameScene();

    /// <summary>
    ///   Checks for a permitted savepoint.
    /// </summary>
    /// <param name="savePoint">Save point.</param>
    /// <returns>Can save.</returns>
    public bool MustBeSave(SavePoints savePoint);
  }
}