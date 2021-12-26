namespace Core.Rule.GameRule.Impact.Variants {
  public sealed class Lightning : Impact<ImpactOnRiskPoints> {
    #region Constructors
    public Lightning(ImpactType impactType = ImpactType.Negative,
      string name = "Lightning", int diceRollValue = 19, int protectiveThrow = 0) : base(impactType, name, diceRollValue, protectiveThrow) { }

    #endregion
    #region Implementations
    public override void ImpactAction(ImpactOnRiskPoints impactAction) {
      int damage = 10000;
      impactAction.SetRiskPoints(typeOfImpact, damage, ProtectiveThrow);
    }
    #endregion
  }
}