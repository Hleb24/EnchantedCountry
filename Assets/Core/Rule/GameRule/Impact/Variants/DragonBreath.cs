using Core.Rule.Dice;

namespace Core.Rule.GameRule.Impact.Impacts {
    public sealed class DragonBreath : Impact<ImpactOnRiskPoints> {
        #region Fields
        #endregion
        #region Constructors
        public DragonBreath(ImpactType impactType = ImpactType.Negative,
            string name = "Dragon Breath", int diceRollValue = 19, int protectiveThrow = 0) : base(impactType, name, diceRollValue, protectiveThrow) { }

        #endregion
        #region Implementations
        public override void ImpactAction(ImpactOnRiskPoints impactAction) {
            int damage = KitOfDice.diceKit[KitOfDice.SetWithOneTwelveSidedAndOneSixSidedDice].SumRollsOfDice();
            impactAction.SetRiskPoints(typeOfImpact, damage, ProtectiveThrow);
        }
        #endregion
    }
}
