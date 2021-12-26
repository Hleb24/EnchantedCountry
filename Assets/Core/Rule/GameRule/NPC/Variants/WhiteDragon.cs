using Core.Rule.GameRule.Impact;

namespace Core.Rule.GameRule.NPC.Variants {
  public class WhiteDragon : Npc {
    private const int MaxNumberOfDragonBreath = 3;
    private int _numberOfDragonBreath;
    
    public override void ToDamagedOfImpact(int diceRoll, ImpactOnRiskPoints character, int indexOfImpact) {
      if (_numberOfDragonBreath == MaxNumberOfDragonBreath) {
        return;
      }
      if (diceRoll >= _impacts[indexOfImpact].DiceRollValueForInvokeImpact) {
        _impacts[indexOfImpact].ImpactAction(character);
        _numberOfDragonBreath++;
      }
    }
  }
}