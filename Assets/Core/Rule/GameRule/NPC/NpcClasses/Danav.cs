namespace Core.Rule.GameRule.NPC.NpcClasses {
  public class Danav : Npc {
    private int _numberOfHands = 6;

    public override bool IsHit(int hit) {
      if (hit >= 9 && _numberOfHands != 0) {
        _numberOfHands--;
        NumberOfAttacks();
      }

      return base.IsHit(hit);
    }

    protected override void NumberOfAttacks() {
      _numberOfAttack = _numberOfHands;
    }
  }
}