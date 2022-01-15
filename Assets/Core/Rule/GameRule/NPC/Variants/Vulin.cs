namespace Core.Rule.GameRule.NPC.Variants {
  public class Vulin : Npc {
    private const int KillStake = 8;
    public override bool GetDamaged(int diceRoll, float damage, int weaponId, Weapon.WeaponType type = Weapon.WeaponType.None, bool isSpell = false) {
      if (weaponId == EquipmentIdConstants.EquipmentIdConstants.STAKE && diceRoll >= KillStake) {
        damage += 10000f;
      }

      return base.GetDamaged(diceRoll, damage, weaponId, type, isSpell);
    }
  }
}