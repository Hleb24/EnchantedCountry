using System;
using Core.Main.Dice;

namespace Core.Main.GameRule.Impact {
    [Serializable]
    public sealed class Paralysis : Impact<IImpactOnRiskPoints> {
        #region Constructors
        public Paralysis(ImpactType impactType = ImpactType.Negative,
            string name = "Paralysis", int diceRollValue = 19, int protectiveThrow = 0) : base(impactType, name, diceRollValue, protectiveThrow) { }

        #endregion

        #region Implementations
        public override void ImpactAction(IImpactOnRiskPoints impactAction) {
            int damage = KitOfDice.DicesKit[KitOfDice.SetWithOneTwelveSidedAndOneSixSidedDice].GetSumRollOfBoxDices();
            impactAction.SetRiskPoints(typeOfImpact, damage, ProtectiveThrow);
        }
        #endregion
    }
}
