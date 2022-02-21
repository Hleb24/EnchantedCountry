using UnityEngine.Assertions;

namespace Core.Main.Character {
  public class GamePoints {
    private const int BOTTOM_BORDER = 0;
    private int _points;

    public GamePoints(int startGamePoints) {
      Assert.IsTrue(startGamePoints >= BOTTOM_BORDER, nameof(startGamePoints));
      _points = startGamePoints;
    }
  }
}