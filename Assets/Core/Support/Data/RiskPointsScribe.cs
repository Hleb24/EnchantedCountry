using System;
using Core.Support.SaveSystem.SaveManagers;
using Core.Support.SaveSystem.Scribe;

namespace Core.Support.Data {
  /// <summary>
  ///   Интерфейс для работы с очками риска персонажа.
  /// </summary>
  public interface IRiskPoints {
    /// <summary>
    ///   Получить очки риска персонажа.
    /// </summary>
    /// <returns>Очки риска.</returns>
    public float GetRiskPoints();

    /// <summary>
    ///   Присвоить значение очкам риска.
    /// </summary>
    /// <param name="riskPoints">Новое значение очков риска.</param>
    public void SetRiskPoints(float riskPoints);

    /// <summary>
    ///   Изменить значение очков риска.
    /// </summary>
    /// <param name="riskPoints">Значение, на которое изменяется количество очков риска.</param>
    public void ChangeRiskPoints(float riskPoints);

    /// <summary>
    ///   Хватает ли очков риска.
    /// </summary>
    /// <param name="riskPoints">Котрольное значение очков риска, которое должно быть положительным.</param>
    /// <returns>Истина - очков риска хватает, ложь - очков риска не хватает.</returns>
    public bool EnoughRiskPoints(float riskPoints);
  }

  [Serializable]
  public class RiskPointsScribe : IScribe, IRiskPoints {
    private const int StartRiskPoints = 0;
    private RiskPointDataScroll _riskPointDataScroll;

    float IRiskPoints.GetRiskPoints() {
      return _riskPointDataScroll.Points;
    }

    void IRiskPoints.SetRiskPoints(float riskPoints) {
      _riskPointDataScroll.Points = riskPoints;
    }

    void IRiskPoints.ChangeRiskPoints(float riskPoints) {
      _riskPointDataScroll.Points += riskPoints;
    }

    bool IRiskPoints.EnoughRiskPoints(float riskPoints) {
      return _riskPointDataScroll.Points - riskPoints >= 0;
    }

    void IScribe.Init(Scrolls scrolls) {
      _riskPointDataScroll = new RiskPointDataScroll(StartRiskPoints);
      if (scrolls is null) {
        return;
      }

      scrolls.RiskPointsDataScroll = _riskPointDataScroll;
    }

    void IScribe.Save(Scrolls scrolls) {
      scrolls.RiskPointsDataScroll = _riskPointDataScroll;
    }

    void IScribe.Loaded(Scrolls scrolls) {
      _riskPointDataScroll.Points = scrolls.RiskPointsDataScroll.Points;
    }
  }

  [Serializable]
  public struct RiskPointDataScroll {
    public float Points;

    public RiskPointDataScroll(float points) {
      Points = points;
    }
  }
}