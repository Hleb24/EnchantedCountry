using System;

namespace Core.Support.Data.GamePonts {
  [Serializable]
  public struct GamePointsDataScroll {
    public int Points;

    public GamePointsDataScroll(int points) {
      Points = points;
    }
  }
}