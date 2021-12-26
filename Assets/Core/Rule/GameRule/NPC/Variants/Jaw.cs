namespace Core.Rule.GameRule.NPC.Variants {
  public class Jaw : Npc {
    public override bool GetDamaged(int diceRoll, float damage, int weaponId = 100, Weapon.Weapon.WeaponType type = Weapon.Weapon.WeaponType.None, bool isSpell = false) {
      if ((type & Weapon.Weapon.WeaponType.Spear) == Weapon.Weapon.WeaponType.Spear) {
        damage += 2f;
      }

      return base.GetDamaged(diceRoll, damage, weaponId, type, isSpell);
    }
  }
}