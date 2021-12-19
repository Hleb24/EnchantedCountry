using System;

namespace Core.EnchantedCountry.CoreEnchantedCountry.GameRule.NPC.NpcClasses {
  public class Hydra : Npc {
    public override bool IsHit(int hit) {
      bool isHit = _armorClass.IsHit(hit);
      if (!isHit) {
        _riskPoints.ChangeRiskPoints(1);
      }

      NumberOfAttacks();

      return isHit;
    }

    protected override void NumberOfAttacks() {
      _numberOfAttack = (int) Math.Round(_riskPoints.GetPoints(), MidpointRounding.AwayFromZero);
    }
  }
}