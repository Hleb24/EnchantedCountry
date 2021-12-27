namespace Core.Rule.GameRule.NPC.Variants {
  public class GoldDragon : Npc {
    public override bool GetDamaged(int diceRoll, float damage, int weaponId = 100, Weapon.WeaponType type = Weapon.WeaponType.None, bool isSpell = false) {
      if ((type & Weapon.WeaponType.GoldDagger) == Weapon.WeaponType.GoldDagger) {
        damage += 3f;
      }

      return base.GetDamaged(diceRoll, damage, weaponId, type, isSpell);
    }
  }
}