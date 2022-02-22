namespace Core.Main.GameRule.Impact {
  public interface IImpactOnRiskPoints {
    public void SetRiskPoints(ImpactType impactType, int damage, int protectiveThrow);
  }
}