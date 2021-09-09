namespace Core.EnchantedCountry.CoreEnchantedCountry.GameRule.Impact.Impacts {
    public sealed class Petrification : Impact<ImpactOnRiskPoints> {
        #region Constructors
        public Petrification ( ImpactType impactType = ImpactType.Negative,
            string name = "Petrification", int diceRollValue = 19, int protectiveThrow = 0) : base(impactType, name, diceRollValue, protectiveThrow) { }
        #endregion

        #region Implementations
        public override void ImpactAction(ImpactOnRiskPoints impactAction) {
            impactAction.SetRiskPoints(typeOfImpact, 0, ProtectiveThrow);
        }
        #endregion
    }
}
