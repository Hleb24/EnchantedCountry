namespace Core.Rule.GameRule.NPC.Variants {
  public class Hul : Npc {
    public override bool GetDamaged(int diceRoll, float damage, int weaponId, Weapon.WeaponType type = Weapon.WeaponType.None, bool isSpell = false) {
      if ((type & Weapon.WeaponType.LongSword) == Weapon.WeaponType.LongSword || (type & Weapon.WeaponType.ShortSword) == Weapon.WeaponType.ShortSword ||
          (type & Weapon.WeaponType.TwoHandedSword) == Weapon.WeaponType.TwoHandedSword) {
        return false;
      }

      return base.GetDamaged(diceRoll, damage, weaponId, type, isSpell);
    }
  }
}