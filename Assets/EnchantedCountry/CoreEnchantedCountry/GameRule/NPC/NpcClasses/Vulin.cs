namespace Core.EnchantedCountry.CoreEnchantedCountry.GameRule.NPC.NpcClasses {
  public class Vulin : Npc {
    private const int KillStake = 8;
    public override bool GetDamaged(int diceRoll, float damage, int weaponId, Weapon.Weapon.WeaponType type = Weapon.Weapon.WeaponType.None, bool isSpell = false) {
      if (weaponId == EquipmentIdConstants.EquipmentIdConstants.Stake && diceRoll >= KillStake) {
        damage += 10000f;
      }

      return base.GetDamaged(diceRoll, damage, weaponId, type, isSpell);
    }
  }
}