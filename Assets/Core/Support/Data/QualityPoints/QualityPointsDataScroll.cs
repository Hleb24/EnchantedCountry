using System;

namespace Core.Support.Data.QualityPoints {
  [Serializable]
  public struct QualityPointsDataScroll {
    public int Strength;
    public int Agility;
    public int Constitution;
    public int Wisdom;
    public int Courage;

    public QualityPointsDataScroll(int strength = 0, int agility = 0, int constitution = 0, int wisdom = 0, int courage = 0) {
      Strength = strength;
      Agility = agility;
      Constitution = constitution;
      Wisdom = wisdom;
      Courage = courage;
    }
  }
}