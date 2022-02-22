using System;
using Aberrance.Extensions;
using Core.Main.GameRule;
using Core.Support.SaveSystem.SaveManagers;
using Core.Support.SaveSystem.Scribe;
using UnityEngine.Assertions;

namespace Core.Support.Data.GamePonts {
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
      if (scrolls.Null()) {
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
}