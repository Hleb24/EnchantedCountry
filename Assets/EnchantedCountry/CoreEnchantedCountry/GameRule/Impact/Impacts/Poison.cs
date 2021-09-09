using Core.EnchantedCountry.CoreEnchantedCountry.Dice;

namespace Core.EnchantedCountry.CoreEnchantedCountry.GameRule.Impact.Impacts {
    public sealed class Poison : Impact<ImpactOnRiskPoints> {
        #region Constructors
        public Poison(ImpactType impactType = ImpactType.Negative,
        string name = "Poison", int diceRollValue = 19, int protectiveThrow = 0) : base(impactType, name, diceRollValue, protectiveThrow) { }
        #endregion

        #region Implementations
        public override void ImpactAction(ImpactOnRiskPoints impactAction) {
            int damage = KitOfDice.diceKit[KitOfDice.SetWithOneTwelveSidedAndOneSixSidedDice].SumRollsOfDice();
            impactAction.SetRiskPoints(typeOfImpact, damage, ProtectiveThrow);
        }
        #endregion
    }
}

