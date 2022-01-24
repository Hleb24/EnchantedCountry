using JetBrains.Annotations;

namespace Core.Main.NonPlayerCharacters.Variants {
  public class Quant : NonPlayerCharacter {
    private static int _numberOfSuccessHit;

    protected override float WeaponsDamage(int weapon = 0) {
      _numberOfSuccessHit++;
      if (_numberOfSuccessHit >= 3) {
        _numberOfSuccessHit = 0;
        return 1f;
      }

      return 0;
    }

    public Quant([NotNull] NpcMetadata npcMetadata, [NotNull] NpcMorality npcMorality, [NotNull] NpcCombatAttributes npcCombatAttributes, [NotNull] NpcEquipments npcEquipments) : base(npcMetadata, npcMorality, npcCombatAttributes, npcEquipments) { }
  }
}