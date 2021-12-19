using System;
using UnityEngine.Assertions;

namespace Core.EnchantedCountry.SupportSystems.Data {
  /// <summary>
  ///   Интерфейс для работы с игровыми очками персонажа.
  /// </summary>
  public interface IGamePoints {
    /// <summary>
    ///   Получить игровые очки.
    /// </summary>
    /// <returns>Игровые очки.</returns>
    public int GetPoints();

    /// <summary>
    ///   Присвоить новое значение игровым очкам.
    /// </summary>
    /// <param name="gamePoints">Новое значение игровых очков.</param>
    public void SetPoints(int gamePoints);

    /// <summary>
    ///   Изменить количестов игровых очков.
    /// </summary>
    /// <param name="gamePoints">Значение на которое поменяються игровые очки.</param>
    public void ChangePoints(int gamePoints);

    /// <summary>
    ///   Хватает ли игровых очков.
    /// </summary>
    /// <param name="gamePoints">Контрольное значения игровых очков на проверку.Для проверки оно должно быть положительным.</param>
    /// <returns>Истина - игровых очков хватает, ложь - игровых очков не хватает.</returns>
    public bool EnoughGamePoints(int gamePoints);
  }

  [Serializable]
  public class GamePointsScribe : IScribe, IGamePoints {
    private const int StartGamePoints = 0;
    private GamePointsDataSave _gamePoints;

    public int GetPoints() {
      return _gamePoints.Points;
    }

    public void SetPoints(int gamePoints) {
      Assert.IsTrue(gamePoints >= 0);
      _gamePoints.Points = gamePoints;
    }

    public void ChangePoints(int gamePoints) {
      _gamePoints.Points += gamePoints;
      _gamePoints.Points = _gamePoints.Points >= 0 ? _gamePoints.Points : 0;
    }

    public bool EnoughGamePoints(int gamePoints) {
      return _gamePoints.Points - gamePoints >= 0;
    }

    public void Init(SaveGame saveGame = null) {
      _gamePoints = new GamePointsDataSave(StartGamePoints);
      if (saveGame is null) {
        return;
      }

      saveGame.GamePointsDataSave = _gamePoints;
    }

    public void Save(SaveGame saveGame) {
      saveGame.GamePointsDataSave = _gamePoints;
    }

    public void Loaded(SaveGame saveGame) {
      _gamePoints.Points = saveGame.GamePointsDataSave.Points;
    }
  }

  [Serializable]
  public struct GamePointsDataSave {
    public int Points;

    public GamePointsDataSave(int points) {
      Points = points;
    }
  }
}