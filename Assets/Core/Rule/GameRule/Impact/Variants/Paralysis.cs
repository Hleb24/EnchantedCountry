using System;
using Core.Rule.Dice;

namespace Core.Rule.GameRule.Impact.Variants {
    [Serializable]
    public sealed class Paralysis : Impact<ImpactOnRiskPoints> {
        #region Constructors
        public Paralysis(ImpactType impactType = ImpactType.Negative,
            string name = "Paralysis", int diceRollValue = 19, int protectiveThrow = 0) : base(impactType, name, diceRollValue, protectiveThrow) { }

        #endregion

        #region Implementations
        public override void ImpactAction(ImpactOnRiskPoints impactAction) {
            int damage = KitOfDice.diceKit[KitOfDice.SetWithOneTwelveSidedAndOneSixSidedDice].SumRollsOfDice();
            impactAction.SetRiskPoints(typeOfImpact, damage, ProtectiveThrow);
        }
        #endregion
    }
}
