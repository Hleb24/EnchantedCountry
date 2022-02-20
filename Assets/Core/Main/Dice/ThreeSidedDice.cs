namespace Core.Main.Dice {
  public sealed class ThreeSidedDice : Dice {
    public override DiceType DiceType {
      get {
        return DiceType.ThreeEdges;
      }
    }

    protected override int[] Edges { get; } = { 0, 0, 2, 2, 3, 3 };
  }
}