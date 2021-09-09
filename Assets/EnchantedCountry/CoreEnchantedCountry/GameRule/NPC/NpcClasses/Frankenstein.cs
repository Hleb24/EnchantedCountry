using UnityEngine;

namespace Core.EnchantedCountry.CoreEnchantedCountry.GameRule.NPC.NpcClasses {
  public class Frankenstein : Npc {
    private const int NUMBER_OF_ATTACK = 3;

    protected override float WeaponsDamage(int weapon = 0) {
      float damage = 0;
      if (_weapons != null && _weapons.Count > 0) {
        int numberOfAttack = Random.Range(1, NUMBER_OF_ATTACK + 1);
        damage = _weapons[GetIndexOfWeapon()].ToDamage();
        damage *= numberOfAttack;
      }

      return damage;
    }
  }
}