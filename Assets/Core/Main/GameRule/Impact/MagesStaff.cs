namespace Core.Main.GameRule.Impact {
  public sealed class MagesStaff : Impact<IImpactOnRiskPoints> {
    #region Constructors
    public MagesStaff(ImpactType impactType = ImpactType.Negative, string name = "Mages Staff", int diceRollValue = 19, int protectiveThrow = 0) : base(impactType, name,
      diceRollValue, protectiveThrow) { }
    #endregion

    #region Implementations
    public override void ImpactAction(IImpactOnRiskPoints impactAction) {
      impactAction.SetRiskPoints(typeOfImpact, 0, ProtectiveThrow);
    }
    #endregion
  }
}