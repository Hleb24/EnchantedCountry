namespace Core.Main.GameRule.Impact {
  public sealed class Lightning : Impact<IImpactOnRiskPoints> {
    #region Constructors
    public Lightning(ImpactType impactType = ImpactType.Negative,
      string name = "Lightning", int diceRollValue = 19, int protectiveThrow = 0) : base(impactType, name, diceRollValue, protectiveThrow) { }

    #endregion
    #region Implementations
    public override void ImpactAction(IImpactOnRiskPoints impactAction) {
      int damage = 10000;
      impactAction.SetRiskPoints(typeOfImpact, damage, ProtectiveThrow);
    }
    #endregion
  }
}