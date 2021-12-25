namespace Core.Rule.GameRule.NPC.NpcClasses {
  public class Quant : Npc {
    private static int _numberOfSuccessHit;

    protected override float WeaponsDamage(int weapon = 0) {
      _numberOfSuccessHit++;
      if (_numberOfSuccessHit >= 3) {
        _numberOfSuccessHit = 0;
        return 1f;
      }

      return 0;
    }
  }
}