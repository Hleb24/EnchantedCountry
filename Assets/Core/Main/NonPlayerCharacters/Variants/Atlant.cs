using JetBrains.Annotations;

namespace Core.Main.NonPlayerCharacters.Variants {
  public class Atlant : NonPlayerCharacter {
    private static int _numberOfSuccessHit;

    protected override float WeaponsDamage(int weapon = 0) {
      float damage = 0;
      _numberOfSuccessHit++;
      if (_numberOfSuccessHit > 1) {
        _numberOfSuccessHit = 0;
        if (_npcEquipments.HasWeapon()) {
          damage = _npcEquipments.ToDamage(weapon);
        }

        return damage;
      }

      return damage;
    }

    public Atlant([NotNull] NpcMetadata npcMetadata, [NotNull] NpcMorality npcMorality, [NotNull] NpcCombatAttributes npcCombatAttributes, [NotNull] NpcEquipments npcEquipments) : base(npcMetadata, npcMorality, npcCombatAttributes, npcEquipments) { }
  }
}