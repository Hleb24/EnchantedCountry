using Core.Main.GameRule.Impact;

namespace Core.SO.ImpactObjects {
  public interface IImpactsSet {
    public Impact<IImpactOnRiskPoints> GetImpactOnRiskPoints(int impactId);
  }
}