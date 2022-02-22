using Core.Main.Dice;

namespace Core.Main.GameRule.Impact {
    public sealed class Poison : Impact<IImpactOnRiskPoints> {
        public Poison(ImpactType impactType = ImpactType.Negative,
        string name = "Poison", int diceRollValue = 19, int protectiveThrow = 0) : base(impactType, name, diceRollValue, protectiveThrow) { }

        public override void ImpactAction(IImpactOnRiskPoints impactAction) {
            int damage = KitOfDice.DicesKit[KitOfDice.SetWithOneTwelveSidedAndOneSixSidedDice].GetSumRollOfBoxDices();
            impactAction.SetRiskPoints(typeOfImpact, damage, ProtectiveThrow);
        }
    }
}

