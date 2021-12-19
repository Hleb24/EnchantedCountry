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

  /// <summary>
  ///   Класс для хранения данных о игровых очках.
  /// </summary>
  [Serializable]
  public class GamePointsScribe : IScribe, IGamePoints {
    private const int StartGamePoints = 0;
    private GamePointsDataScroll _gamePoints;

    public bool EnoughGamePoints(int gamePoints) {
      return _gamePoints.Points - gamePoints >= 0;
    }

    int IGamePoints.GetPoints() {
      return _gamePoints.Points;
    }

    void IGamePoints.SetPoints(int gamePoints) {
      Assert.IsTrue(gamePoints >= 0);
      _gamePoints.Points = gamePoints;
    }

    void IGamePoints.ChangePoints(int gamePoints) {
      _gamePoints.Points += gamePoints;
      _gamePoints.Points = _gamePoints.Points >= 0 ? _gamePoints.Points : 0;
    }

    void IScribe.Init(Scrolls scrolls) {
      _gamePoints = new GamePointsDataScroll(StartGamePoints);
      if (scrolls is null) {
        return;
      }

      scrolls.GamePointsDataScroll = _gamePoints;
    }

    void IScribe.Save(Scrolls scrolls) {
      scrolls.GamePointsDataScroll = _gamePoints;
    }

    void IScribe.Loaded(Scrolls scrolls) {
      _gamePoints.Points = scrolls.GamePointsDataScroll.Points;
    }
  }

  [Serializable]
  public struct GamePointsDataScroll {
    public int Points;

    public GamePointsDataScroll(int points) {
      Points = points;
    }
  }
}