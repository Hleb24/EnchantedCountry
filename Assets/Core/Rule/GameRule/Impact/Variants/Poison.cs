using Core.Rule.Dice;

namespace Core.Rule.GameRule.Impact.Variants {
    public sealed class Poison : Impact<ImpactOnRiskPoints> {
        public Poison(ImpactType impactType = ImpactType.Negative,
        string name = "Poison", int diceRollValue = 19, int protectiveThrow = 0) : base(impactType, name, diceRollValue, protectiveThrow) { }

        public override void ImpactAction(ImpactOnRiskPoints impactAction) {
            int damage = KitOfDice.diceKit[KitOfDice.SetWithOneTwelveSidedAndOneSixSidedDice].SumRollsOfDice();
            impactAction.SetRiskPoints(typeOfImpact, damage, ProtectiveThrow);
        }
    }
}

