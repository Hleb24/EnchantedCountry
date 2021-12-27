namespace Core.Rule.GameRule.NPC.Variants {
  public class Cyclops : Npc {
    private bool _isBlind;
    public override bool GetDamaged(int diceRoll, float damage, int weaponId = 100, Weapon.WeaponType type = Weapon.WeaponType.None, bool isSpell = false) {
      if (((type & Weapon.WeaponType.LongBow) == Weapon.WeaponType.LongBow && diceRoll >= 16)
      ||((type & Weapon.WeaponType.ShortBow) == Weapon.WeaponType.ShortBow && diceRoll >= 16)
      ) {
        _isBlind = true;
      }

      return base.GetDamaged(diceRoll, damage, weaponId, type, isSpell);
    }
    
    public override int Accuracy(int index = 0) {
      int accuracy = 0;
      if (_weapons != null && _weapons.Count > 0) {
        accuracy = _weapons[GetIndexOfWeapon(index)].Attack.Accuracy;
        if (_isBlind) {
          accuracy -= 2;
        }
      }

      return accuracy;
    }
  }
}