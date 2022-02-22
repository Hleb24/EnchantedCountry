namespace Core.Main.GameRule.Impact {
  public sealed class MagesStaff : Impact<IImpactOnRiskPoints> {
    public MagesStaff(ImpactType impactType = ImpactType.MagesStaff, string name = "Mages Staff", int diceRollValue = 19, int protectiveThrow = 0) : base(impactType, name,
      diceRollValue, protectiveThrow) { }

    public override void ImpactAction(IImpactOnRiskPoints impactAction) {
      impactAction.SetRiskPoints(_typeOfImpact, 0, _protectiveThrow);
    }
  }
}