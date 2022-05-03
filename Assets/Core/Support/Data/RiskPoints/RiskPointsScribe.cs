using System;
using Aberrance.Extensions;
using Core.Main.GameRule.Point;
using Core.Support.Data.Equipment;
using Core.Support.SaveSystem.SaveManagers;
using Core.Support.SaveSystem.Scribe;

namespace Core.Support.Data.RiskPoints {
  [Serializable]
  public class RiskPointsScribe : IScribe, IRiskPoints {
    private static RiskPointsScribe _originRiskPointsScribe;
    private const int START_RISK_POINTS = 0;
    private RiskPointDataScroll _riskPointDataScroll;

    float IRiskPoints.GetRiskPoints() {
      return _riskPointDataScroll.Points;
    }

    void IRiskPoints.SetRiskPoints(float riskPoints) {
      UpdateLastChanged();
      _riskPointDataScroll.Points = riskPoints;
    }

    void IRiskPoints.ChangeRiskPoints(float riskPoints) {
      UpdateLastChanged();
      _riskPointDataScroll.Points += riskPoints;
    }

    bool IRiskPoints.EnoughRiskPoints(float riskPoints) {
      return _riskPointDataScroll.Points - riskPoints >= 0;
    }

    public T Clone<T>() {
      return (T)MemberwiseClone();
    }

    public T CloneWithTracking<T>() {
      IsTracking = true;
      return Clone<T>();
    }

    public void ReplaceOriginal<T>(T newOriginValue) {
      if (newOriginValue is RiskPointsScribe riskPointsScribe) {
        _originRiskPointsScribe = riskPointsScribe;
      }
    }

    public void ReplaceOriginal() {
      _originRiskPointsScribe = this;
    }

    void IScribe.SaveOnQuit(Scrolls scrolls) {
      bool changeOrigin = ScribeHandler.ChangeOrigin(this, this, _originRiskPointsScribe);
      if (changeOrigin) {
        _originRiskPointsScribe = this;
      }

      IsTracking = false;

      scrolls.RiskPointsDataScroll = _originRiskPointsScribe._riskPointDataScroll;
    }

    void IScribe.Init(Scrolls scrolls) {
      _riskPointDataScroll = new RiskPointDataScroll(START_RISK_POINTS);
      UpdateLastChanged();
      _originRiskPointsScribe = this;
      if (scrolls.Null()) {
        return;
      }

      scrolls.RiskPointsDataScroll = _originRiskPointsScribe._riskPointDataScroll;
    }

    void IScribe.Save(Scrolls scrolls) {
      scrolls.RiskPointsDataScroll = _originRiskPointsScribe._riskPointDataScroll;
    }

    void IScribe.Loaded(Scrolls scrolls) {
      _riskPointDataScroll.Points = scrolls.RiskPointsDataScroll.Points;
      _originRiskPointsScribe = this;
    }

    private void UpdateLastChanged() {
      LastChanged = DateTime.Now;
    }

    public bool IsTracking { get; private set; }

    public DateTime LastChanged { get; private set; }
  }
}