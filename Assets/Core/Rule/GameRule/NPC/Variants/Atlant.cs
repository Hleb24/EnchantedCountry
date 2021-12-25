namespace Core.Rule.GameRule.NPC.NpcClasses {
  public class Atlant : Npc {
    private static int _numberOfSuccessHit;

    protected override float WeaponsDamage(int weapon = 0) {
      float damage = 0;
      _numberOfSuccessHit++;
      if (_numberOfSuccessHit > 1) {
        _numberOfSuccessHit = 0;
        if (_weapons != null && _weapons.Count > 0) {
          damage = _weapons[GetIndexOfWeapon(weapon)].ToDamage();
        }

        return damage;
      }

      return damage;
    }
  }
}