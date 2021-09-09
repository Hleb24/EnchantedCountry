using System;
using Core.EnchantedCountry.CoreEnchantedCountry.Dice;

namespace Core.EnchantedCountry.CoreEnchantedCountry.GameRule.Impact.Impacts {
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
