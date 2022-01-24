namespace Core.Main.GameRule.Impact.Variants {
    public sealed class MagicianSpell : Impact<ImpactOnRiskPoints> {
        #region Constructors
        public MagicianSpell(ImpactType impactType = ImpactType.Negative,
            string name = "Magician Spell", int diceRollValue = 19, int protectiveThrow = 0) : base(impactType, name, diceRollValue, protectiveThrow) { }
        #endregion

        #region Implementations
        public override void ImpactAction(ImpactOnRiskPoints impactAction) {
            impactAction.SetRiskPoints(typeOfImpact, 0, ProtectiveThrow);
        }
        #endregion
    }
}
