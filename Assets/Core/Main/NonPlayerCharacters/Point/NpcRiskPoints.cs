using Core.Main.GameRule.Point;

namespace Core.Main.NonPlayerCharacters.Point {
  public class NpcRiskPoints : IRiskPoints {
    private float _riskPoints;

    public float GetRiskPoints() {
      return _riskPoints;
    }

    public void SetRiskPoints(float riskPoints) {
      _riskPoints = riskPoints;
    }

    public void ChangeRiskPoints(float riskPoints) {
      _riskPoints += riskPoints;
    }

    public bool EnoughRiskPoints(float riskPoints) {
      return _riskPoints > riskPoints;
    }
  }
}