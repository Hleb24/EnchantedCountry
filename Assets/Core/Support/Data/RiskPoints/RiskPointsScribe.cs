using System;
using Aberrance.Extensions;
using Core.Main.GameRule.Point;
using Core.Support.SaveSystem.SaveManagers;
using Core.Support.SaveSystem.Scribe;

namespace Core.Support.Data.RiskPoints {
  [Serializable]
  public class RiskPointsScribe : IScribe, IRiskPoints {
    private const int START_RISK_POINTS = 0;
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
      _riskPointDataScroll = new RiskPointDataScroll(START_RISK_POINTS);
      if (scrolls.Null()) {
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
}