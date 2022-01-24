using JetBrains.Annotations;

namespace Core.Main.NonPlayerCharacters.Variants {
  public class Danav : NonPlayerCharacter {
    private int _numberOfHands = 6;

    public override bool IsHit(int hit) {
      if (hit >= 9 && _numberOfHands != 0) {
        _numberOfHands--;
        NumberOfAttacks();
      }

      return base.IsHit(hit);
    }

    protected override void NumberOfAttacks() {
      _npcCombatAttributes.SetNumberOfAttack(_numberOfHands);
    }

    public Danav([NotNull] NpcMetadata npcMetadata, [NotNull] NpcMorality npcMorality, [NotNull] NpcCombatAttributes npcCombatAttributes, [NotNull] NpcEquipments npcEquipments) : base(npcMetadata, npcMorality, npcCombatAttributes, npcEquipments) { }
  }
}