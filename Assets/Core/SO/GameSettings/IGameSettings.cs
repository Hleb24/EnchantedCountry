﻿using Core.Mono.BaseClass;

namespace Core.SO.GameSettings {
  /// <summary>
  ///   Интерфейс для настроек игры.
  /// </summary>
  public interface IGameSettings {
    /// <summary>
    ///   Начать новую игру.
    /// </summary>
    /// <returns>Истина - начать новую игру, ложь - продолжить текущую игру.</returns>
    public bool StartNewGame();

    /// <summary>
    ///   Использовать сохранения.
    /// </summary>
    /// <returns>Истинна -использовать, ложь - не использовать.</returns>
    public bool UseGameSave();

    /// <summary>
    ///   Получить сцену, котороя будет открыватся принудительно сразу после сцены <see cref="Scene.Intro" />.
    /// </summary>
    /// <returns>Целевая сцена.</returns>
    public Scene GetTargetScene();

    /// <summary>
    ///   Получить стартовую сцену.
    /// </summary>
    /// <returns>Стартовая сцена</returns>
    public Scene GetStartScene();
  }
}