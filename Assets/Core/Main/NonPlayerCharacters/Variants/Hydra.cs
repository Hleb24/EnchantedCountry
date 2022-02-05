using System;
using JetBrains.Annotations;

namespace Core.Main.NonPlayerCharacters.Variants {
  public class Hydra : NonPlayerCharacter {
    public override bool IsHit(int hit) {
      bool isHit = _npcEquipments.IsHit(hit);
      if (!isHit) {
        _npcCombatAttributes.Heal(1);
      }

      PrepareNumberOfAttacks();

      return isHit;
    }

    internal override void PrepareNumberOfAttacks() {
      int numberOfAttack = (int)Math.Round(GetPointsOfRisk(), MidpointRounding.AwayFromZero);
      _npcCombatAttributes.SetNumberOfAttack(numberOfAttack);
    }

    public Hydra([NotNull] NpcMetadata npcMetadata, [NotNull] NpcMorality npcMorality, [NotNull] NpcCombatAttributes npcCombatAttributes, [NotNull] NpcEquipments npcEquipments) : base(npcMetadata, npcMorality, npcCombatAttributes, npcEquipments) { }
  }
}