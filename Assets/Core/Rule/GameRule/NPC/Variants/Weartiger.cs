namespace Core.Rule.GameRule.NPC.Variants {
  public class Weartiger : Npc {
    public override bool GetDamaged(int diceRoll, float damage, int weaponId = 100, Weapon.Weapon.WeaponType type = Weapon.Weapon.WeaponType.None, bool isSpell = false) {
      if ((type & Weapon.Weapon.WeaponType.SilverDagger) == Weapon.Weapon.WeaponType.SilverDagger) {
        damage += 2f;
      }

      return base.GetDamaged(diceRoll, damage, weaponId, type, isSpell);
    }
  }
}