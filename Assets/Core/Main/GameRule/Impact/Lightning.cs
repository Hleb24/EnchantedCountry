namespace Core.Main.GameRule.Impact {
  public sealed class Lightning : Impact<IImpactOnRiskPoints> {
    public Lightning(ImpactType impactType = ImpactType.Lightning, string name = "Lightning", int diceRollValue = 19, int protectiveThrow = 0) : base(impactType, name,
      diceRollValue, protectiveThrow) { }

    public override void ImpactAction(IImpactOnRiskPoints impactAction) {
      var damage = 10000;
      impactAction.SetRiskPoints(_typeOfImpact, damage, _protectiveThrow);
    }
  }
}