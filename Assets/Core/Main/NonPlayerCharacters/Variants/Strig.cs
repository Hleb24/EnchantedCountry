using JetBrains.Annotations;

namespace Core.Main.NonPlayerCharacters.Variants {
  public class Strig : NonPlayerCharacter {
    public Strig([NotNull] NpcMetadata npcMetadata, [NotNull] NpcMorality npcMorality, [NotNull] NpcCombatAttributes npcCombatAttributes, [NotNull] NpcEquipments npcEquipments) :
      base(npcMetadata, npcMorality, npcCombatAttributes, npcEquipments) { }

    protected override float WeaponsDamage(int weapon = 0) {
      float damage = 0;
      if (_npcEquipments.IsHasWeapon()) {
        damage = _npcEquipments.ToDamage(GetIndexOfWeapon());
      }

      VampireHealing(damage);

      return damage;
    }

    private void VampireHealing(float damage) {
      _npcCombatAttributes.Heal(damage);
    }
  }
}