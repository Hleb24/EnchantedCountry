namespace Core.Rule.GameRule.Impact.Variants {
    public sealed class MagesStaff : Impact<ImpactOnRiskPoints> {
        #region Constructors
        public MagesStaff(ImpactType impactType = ImpactType.Negative,
            string name = "Mages Staff", int diceRollValue = 19, int protectiveThrow = 0) : base(impactType, name, diceRollValue, protectiveThrow) { }

        #endregion
        #region Implementations
        public override void ImpactAction(ImpactOnRiskPoints impactAction) {
            impactAction.SetRiskPoints(typeOfImpact, 0, ProtectiveThrow);
        }
        #endregion
    }
}
