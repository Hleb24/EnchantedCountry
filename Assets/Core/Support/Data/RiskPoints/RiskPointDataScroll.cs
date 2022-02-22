using System;

namespace Core.Support.Data.RiskPoints {
  [Serializable]
  public struct RiskPointDataScroll {
    public float Points;

    public RiskPointDataScroll(float points) {
      Points = points;
    }
  }
}