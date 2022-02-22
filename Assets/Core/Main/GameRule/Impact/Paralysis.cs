using System;
using Core.Main.Dice;

namespace Core.Main.GameRule.Impact {
  [Serializable]
  public sealed class Paralysis : Impact<IImpactOnRiskPoints> {
    public Paralysis(ImpactType impactType = ImpactType.Paralysis, string name = "Paralysis", int diceRollValue = 19, int protectiveThrow = 0) : base(impactType, name,
      diceRollValue, protectiveThrow) { }

    public override void ImpactAction(IImpactOnRiskPoints impactAction) {
      int damage = KitOfDice.DicesKit[KitOfDice.SET_WITH_ONE_TWELVE_SIDED_AND_ONE_SIX_SIDED_DICE].GetSumRollOfBoxDices();
      impactAction.SetRiskPoints(_typeOfImpact, damage, _protectiveThrow);
    }
  }
}