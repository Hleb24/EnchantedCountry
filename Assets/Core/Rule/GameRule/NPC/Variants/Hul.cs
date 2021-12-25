namespace Core.Rule.GameRule.NPC.NpcClasses {
  public class Hul : Npc {
    public override bool GetDamaged(int diceRoll, float damage, int weaponId, Weapon.Weapon.WeaponType type = Weapon.Weapon.WeaponType.None, bool isSpell = false) {
      if ((type & Weapon.Weapon.WeaponType.LongSword) == Weapon.Weapon.WeaponType.LongSword || (type & Weapon.Weapon.WeaponType.ShortSword) == Weapon.Weapon.WeaponType.ShortSword ||
          (type & Weapon.Weapon.WeaponType.TwoHandedSword) == Weapon.Weapon.WeaponType.TwoHandedSword) {
        return false;
      }

      return base.GetDamaged(diceRoll, damage, weaponId, type, isSpell);
    }
  }
}