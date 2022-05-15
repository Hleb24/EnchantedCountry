using System;
using Aberrance.Extensions;
using Core.Main.GameRule.Point;
using Core.Support.Data.Equipment;
using Core.Support.SaveSystem.SaveManagers;
using Core.Support.SaveSystem.Scribe;
using UnityEngine.Assertions;

namespace Core.Support.Data.GamePonts {
  /// <summary>
  ///   Класс для хранения данных о игровых очках.
  /// </summary>
  [Serializable]
  public class GamePointsScribe : IScribe, IGamePoints {
    private static GamePointsScribe _originGamePointsScribe;
    private const int START_GAME_POINTS = 0;
    private GamePointsDataScroll _gamePoints;

    public bool EnoughGamePoints(int gamePoints) {
      return _gamePoints.Points - gamePoints >= 0;
    }

    int IGamePoints.GetPoints() {
      return _gamePoints.Points;
    }

    void IGamePoints.SetPoints(int gamePoints) {
      Assert.IsTrue(gamePoints >= 0);
      UpdateLastChanged();
      _gamePoints.Points = gamePoints;
    }

    void IGamePoints.ChangePoints(int gamePoints) {
      UpdateLastChanged();
      _gamePoints.Points += gamePoints;
      _gamePoints.Points = _gamePoints.Points >= 0 ? _gamePoints.Points : 0;
    }

    public T Clone<T>() {
      return (T)MemberwiseClone();
    }

    public T CloneWithTracking<T>() {
      IsTracking = true;
      return Clone<T>();
    }

    public void ReplaceOriginal<T>(T newOriginValue) {
      throw new NotImplementedException();
    }

    public void ReplaceOriginal() {
      _originGamePointsScribe = this;
    }

    void IScribe.SaveOnQuit(Scrolls scrolls) {
      bool changeOrigin = ScribeHandler.ChangeOrigin(this, this, _originGamePointsScribe);
      if (changeOrigin) {
        _originGamePointsScribe = this;
      }

      IsTracking = false;
      scrolls.GamePointsDataScroll = _originGamePointsScribe._gamePoints;
    }

    void IScribe.Init(Scrolls scrolls) {
      _gamePoints = new GamePointsDataScroll(START_GAME_POINTS);
      UpdateLastChanged();
      _originGamePointsScribe = this;
      if (scrolls.IsNull()) {
        return;
      }

      scrolls.GamePointsDataScroll = _originGamePointsScribe._gamePoints;
    }

    void IScribe.Save(Scrolls scrolls) {
      scrolls.GamePointsDataScroll = _originGamePointsScribe._gamePoints;
    }

    void IScribe.Loaded(Scrolls scrolls) {
      _gamePoints.Points = scrolls.GamePointsDataScroll.Points;
      _originGamePointsScribe = this;
    }

    private void UpdateLastChanged() {
      LastChanged = DateTime.Now;
    }

    public bool IsTracking { get; private set; }

    public DateTime LastChanged { get; private set; }
  }
}