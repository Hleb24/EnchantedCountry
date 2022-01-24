using System;
using JetBrains.Annotations;

namespace Core.Main.NonPlayerCharacters.Variants {
  public class Hydra : NonPlayerCharacter {
    public override bool IsHit(int hit) {
      bool isHit = _npcEquipments.IsHit(hit);
      if (!isHit) {
        _npcCombatAttributes.Heal(1);
      }

      NumberOfAttacks();

      return isHit;
    }

    protected override void NumberOfAttacks() {
      int numberOfAttack = (int)Math.Round(RiskPoints.GetPoints(), MidpointRounding.AwayFromZero);
      _npcCombatAttributes.SetNumberOfAttack(numberOfAttack);
    }

    public Hydra([NotNull] NpcMetadata npcMetadata, [NotNull] NpcMorality npcMorality, [NotNull] NpcCombatAttributes npcCombatAttributes, [NotNull] NpcEquipments npcEquipments) : base(npcMetadata, npcMorality, npcCombatAttributes, npcEquipments) { }
  }
}