using Core.Main.NonPlayerCharacters.Model;
using JetBrains.Annotations;

namespace Core.Main.NonPlayerCharacters.Variants {
  public class Danav : NonPlayerCharacter {
    private int _numberOfHands = 6;

    public Danav([NotNull] NpcMetadata npcMetadata, [NotNull] NpcMorality npcMorality, [NotNull] NpcCombatAttributes npcCombatAttributes, [NotNull] NpcEquipments npcEquipments) :
      base(npcMetadata, npcMorality, npcCombatAttributes, npcEquipments) { }

    protected override bool IsHit(int hit) {
      if (hit >= 9 && _numberOfHands != 0) {
        _numberOfHands--;
        PrepareNumberOfAttacks();
      }

      return base.IsHit(hit);
    }

    internal override void PrepareNumberOfAttacks() {
      _npcCombatAttributes.SetNumberOfAttack(_numberOfHands);
    }
  }
}