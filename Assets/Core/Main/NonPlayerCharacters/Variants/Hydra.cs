using System;
using Core.Main.NonPlayerCharacters.Model;
using JetBrains.Annotations;

namespace Core.Main.NonPlayerCharacters.Variants {
  public class Hydra : NonPlayerCharacter {
    public Hydra([NotNull] NpcMetadata npcMetadata, [NotNull] NpcMorality npcMorality, [NotNull] NpcCombatAttributes npcCombatAttributes, [NotNull] NpcEquipments npcEquipments) :
      base(npcMetadata, npcMorality, npcCombatAttributes, npcEquipments) { }

    protected override bool IsHit(int hit) {
      bool isHit = _npcEquipments.IsHit(hit);
      if (!isHit) {
        _npcCombatAttributes.Heal(1);
      }

      PrepareNumberOfAttacks();

      return isHit;
    }

    internal override void PrepareNumberOfAttacks() {
      var numberOfAttack = (int)Math.Round(GetPointsOfRisk(), MidpointRounding.AwayFromZero);
      _npcCombatAttributes.SetNumberOfAttack(numberOfAttack);
    }
  }
}