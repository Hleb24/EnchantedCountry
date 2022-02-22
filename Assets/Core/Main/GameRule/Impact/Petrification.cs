namespace Core.Main.GameRule.Impact {
  public sealed class Petrification : Impact<IImpactOnRiskPoints> {
    public Petrification(ImpactType impactType = ImpactType.Petrification, string name = "Petrification", int diceRollValue = 19, int protectiveThrow = 0) : base(impactType, name,
      diceRollValue, protectiveThrow) { }

    public override void ImpactAction(IImpactOnRiskPoints impactAction) {
      impactAction.SetRiskPoints(_typeOfImpact, 0, _protectiveThrow);
    }
  }
}