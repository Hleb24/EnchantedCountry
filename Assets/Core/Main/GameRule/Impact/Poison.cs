using Core.Main.Dice;

namespace Core.Main.GameRule.Impact {
  public sealed class Poison : Impact<IImpactOnRiskPoints> {
    public Poison(ImpactType impactType = ImpactType.Poison, string name = "Poison", int diceRollValue = 19, int protectiveThrow = 0) : base(impactType, name, diceRollValue,
      protectiveThrow) { }
 
    public override void ImpactAction(IImpactOnRiskPoints impactAction) {
      int damage = KitOfDice.DicesKit[KitOfDice.SET_WITH_ONE_TWELVE_SIDED_AND_ONE_SIX_SIDED_DICE].GetSumRollOfBoxDices();
      impactAction.SetRiskPoints(_typeOfImpact, damage, _protectiveThrow);
    }
  }
}