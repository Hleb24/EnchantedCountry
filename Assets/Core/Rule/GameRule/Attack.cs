using System;
using System.Collections.Generic;
using Core.Rule.Dice;

namespace Core.Rule.GameRule {
  public class Attack {
    #region Fields
    private const int BOTTOM_BORDER = 0;
    private const int TOP_BORDER_FOR_ACCURANCY = 17;
    private const int TOP_BORDER_FOR_DAMAGE = 18;
    public readonly List<float> DamageList;
    private int _accuracy;
    private float _minDamage;
    private float _maxDamage;
    #endregion

    #region Constructors
    public Attack() { }

    public Attack(float maxDamage, float minDamage = 0f, int accuracy = 0) {
      this.MinDamage = minDamage;
      this.MaxDamage = maxDamage;
      this.Accuracy = accuracy;
      DamageList = new List<float>();
    }

    public Attack(List<float> damageList, int accuracy = 0) {
        DamageList = new List<float>();
        DamageList.AddRange(damageList);

      if (DamageList.Count != 0) {
        if (DamageList != null) {
          MinDamage = DamageList[0];
          MaxDamage = DamageList[DamageList.Count - 1];
        }
      } else {
        MinDamage = 0;
        MaxDamage = 0;
      }

      this.Accuracy = accuracy;
    }
    #endregion

    #region Properties
    public bool IsFatalDamage { get; set; }

    public int Accuracy {
      get {
        return _accuracy;
      }
      set {
        if (AccurancyWithinInBorders(value)) {
          _accuracy = value;
        } else {
          throw new InvalidOperationException("Value is invalid");
        }
      }
    }

    public float MinDamage {
      get {
        return _minDamage;
      }
      set {
        if (DamageWithinInBorders(value)) {
          _minDamage = value;
          if (IsMaxDamageGreaterOrEqualToMinDamage()) {
            GetDiceEdges();
          }
        } else {
          throw new InvalidOperationException("Value is invalid");
        }
      }
    }

    public float MaxDamage {
      get {
        return _maxDamage;
      }
      set {
        if (DamageWithinInBorders(value)) {
          _maxDamage = value;
          if (IsMaxDamageGreaterOrEqualToMinDamage()) {
            GetDiceEdges();
          }
        } else {
          throw new InvalidOperationException("Value is invalid");
        }
      }
    }

    public int DiceEdges { get; private set; }
    #endregion

    #region Methods
    private static bool AccurancyWithinInBorders(int value) {
      return value >= BOTTOM_BORDER && value <= TOP_BORDER_FOR_ACCURANCY;
    }

    private static bool DamageWithinInBorders(float value) {
      return value >= BOTTOM_BORDER && value <= TOP_BORDER_FOR_DAMAGE;
    }

    private bool IsMaxDamageGreaterOrEqualToMinDamage() {
      return _maxDamage >= MinDamage;
    }

    private int GetDiceEdges() {
      var delta = 0;
      float minD = MinDamage;
      float maxD = MaxDamage;
      if (DamageList != null && DamageList.Count != 0 && MinDamage != MaxDamage) {
        return DamageList.Count;
      }

      if (MinDamage == MaxDamage) {
        DiceEdges = 1;
      }

      if (minD == 0.5f) {
        while (maxD > 0f) {
          maxD -= minD;
          delta++;
        }

        DiceEdges = delta;
        return DiceEdges;
      }

      float deltaDamage = maxD + 1f - minD;
      delta = (int) deltaDamage;
      DiceEdges = delta;
      return DiceEdges;
    }

    public float GetDamageAfterDiceRoll() {
      Dices dice = new SixSidedDice(DiceType.SixEdges);
      return GetDamage(dice.RollOfDice(DiceEdges));
    }

    public float GetDamage(int edge) {
      if (edge == 0) {
        return 0;
      }

      if (DamageList.Count != 0) {
        return DamageList[edge];
      }

      if (edge == DiceEdges) {
        return MaxDamage;
      }

      if (edge == 1) {
        return MinDamage;
      }

      if (DiceEdges != 0) {
        return MaxDamage / DiceEdges * edge;
      }

      return 0;
    }

    public float GetDamage() {
      int edge = GetDiceEdges();
      if (edge == 0) {
        return 0;
      }

      if (edge == DiceEdges) {
        return MaxDamage;
      }

      if (edge == 1) {
        return MinDamage;
      }

      if (DiceEdges != 0) {
        return MaxDamage / DiceEdges * edge;
      }

      return 0;
    }
  }
  #endregion
}