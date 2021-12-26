namespace Core.Rule.GameRule.NPC.Variants {
  public class Strig : Npc {
    protected override float WeaponsDamage(int weapon = 0) {
      float damage = 0;
      if (_weapons != null && _weapons.Count > 0) {
        damage = _weapons[GetIndexOfWeapon()].ToDamage();
      }

      VampireHealing(damage);

      return damage;
    }

    private void VampireHealing(float damage) {
      _riskPoints.ChangeRiskPoints(damage);
    }
  }
}