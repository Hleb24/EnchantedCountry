using JetBrains.Annotations;
using UnityEngine;

namespace Core.Main.NonPlayerCharacters.Variants {
  public class Frankenstein : NonPlayerCharacter {
    private const int NUMBER_OF_ATTACK = 3;

    public Frankenstein([NotNull] NpcMetadata npcMetadata, [NotNull] NpcMorality npcMorality, [NotNull] NpcCombatAttributes npcCombatAttributes,
      [NotNull] NpcEquipments npcEquipments) : base(npcMetadata, npcMorality, npcCombatAttributes, npcEquipments) { }

    protected override float WeaponsDamage(int weapon = 0) {
      float damage = 0;
      if (_npcEquipments.IsHasWeapon()) {
        int numberOfAttack = Random.Range(1, NUMBER_OF_ATTACK + 1);

        damage = _npcEquipments.ToDamage(GetIndexOfWeapon());
        damage *= numberOfAttack;
      }

      return damage;
    }
  }
}