using Core.EnchantedCountry.CoreEnchantedCountry.GameRule.Impact;

namespace Core.EnchantedCountry.CoreEnchantedCountry.GameRule.NPC.NpcClasses {
  public class Bargul : Npc {
    public override void ToDamagedOfImpact(int diceRoll, ImpactOnRiskPoints character, int indexOfImpact) {
      if (diceRoll >= _impacts[indexOfImpact].DiceRollValueForInvokeImpact) {
        _impacts[indexOfImpact].ImpactAction(character);
      }
    }
  }
}