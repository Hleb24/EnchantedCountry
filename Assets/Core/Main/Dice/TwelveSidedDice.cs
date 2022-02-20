namespace Core.Main.Dice {
  public sealed class TwelveSidedDice : Dice {
    public override DiceType DiceType {
      get {
        return DiceType.TwelveEdges;
      }
    }

    protected override int[] Edges { get; } = { 0, 0, 6, 6, 12, 12 };
  }
}