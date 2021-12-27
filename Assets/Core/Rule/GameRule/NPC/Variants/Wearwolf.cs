namespace Core.Rule.GameRule.NPC.Variants {
  public class Wearwolf : Npc {
    public override bool GetDamaged(int diceRoll, float damage, int weaponId = 100, Weapon.WeaponType type = Weapon.WeaponType.None, bool isSpell = false) {
      if ((type & Weapon.WeaponType.SilverDagger) == Weapon.WeaponType.SilverDagger && diceRoll >= 8) {
        damage += 10000f;
      }

      return base.GetDamaged(diceRoll, damage, weaponId, type, isSpell);
    }
  }
}