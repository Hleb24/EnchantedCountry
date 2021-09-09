namespace Core.EnchantedCountry.CoreEnchantedCountry.GameRule.NPC.NpcClasses {
  public class GoldDragon : Npc {
    public override bool GetDamaged(int diceRoll, float damage, int weaponId = 100, Weapon.Weapon.WeaponType type = Weapon.Weapon.WeaponType.None, bool isSpell = false) {
      if ((type & Weapon.Weapon.WeaponType.GoldDagger) == Weapon.Weapon.WeaponType.GoldDagger) {
        damage += 3f;
      }

      return base.GetDamaged(diceRoll, damage, weaponId, type, isSpell);
    }
  }
}