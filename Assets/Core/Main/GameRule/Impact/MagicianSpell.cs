namespace Core.Main.GameRule.Impact {
  public sealed class MagicianSpell : Impact<IImpactOnRiskPoints> {
    public MagicianSpell(ImpactType impactType = ImpactType.MagicianSpell, string name = "Magician Spell", int diceRollValue = 19, int protectiveThrow = 0) : base(impactType, name,
      diceRollValue, protectiveThrow) { }

    public override void ImpactAction(IImpactOnRiskPoints impactAction) {
      impactAction.SetRiskPoints(_typeOfImpact, 0, _protectiveThrow);
    }
  }
}