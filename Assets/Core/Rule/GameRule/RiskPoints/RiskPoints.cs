using Core.SupportSystems.Data;

namespace Core.Rule.GameRule.RiskPoints {
  public class RiskPoints {
    private readonly IRiskPoints _riskPoints;

    public RiskPoints(IRiskPoints riskPoints) {
      _riskPoints = riskPoints;
    }

    public RiskPoints(IRiskPoints riskPoints, float startPoints) {
      _riskPoints = riskPoints;
      _riskPoints.SetRiskPoints(startPoints);
    }

    public float GetPoints() {
      return _riskPoints.GetRiskPoints();
    }

    public void SetRiskPoints(float riskPoints) {
      _riskPoints.SetRiskPoints(riskPoints);
    }

    public void ChangeRiskPoints(float riskPoints) {
      _riskPoints.ChangeRiskPoints(riskPoints);
    }

    public bool IsDead() {
      return _riskPoints.GetRiskPoints() <= 0;
    }

    public bool IsAlive() {
      return _riskPoints.GetRiskPoints() > 0;
    }

    public bool WillDie(float damage) {
      return !_riskPoints.EnoughRiskPoints(damage);
    }
  }
}