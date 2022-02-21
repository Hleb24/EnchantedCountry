using System.Collections.Generic;
using Aberrance.Extensions;

namespace Core.Main.GameRule {
  public class Attack {
    private static bool AccurancyWithinInBorders(int value) {
      return value >= BOTTOM_BORDER && value <= TOP_BORDER_FOR_ACCURACY;
    }

    private static bool DamageWithinInBorders(float value) {
      return value >= BOTTOM_BORDER && value <= TOP_BORDER_FOR_DAMAGE;
    }

    private const int BOTTOM_BORDER = 0;
    private const int TOP_BORDER_FOR_ACCURACY = 17;
    private const int TOP_BORDER_FOR_DAMAGE = 18;
    private readonly List<float> _damageList;
    private readonly float _minDamage;
    private readonly float _maxDamage;
    private readonly int _accuracy;

    public Attack(float maxDamage, float minDamage = 0f, int accuracy = 0) {
      

      if (DamageWithinInBorders(minDamage)) {
        _minDamage = minDamage;
        if (IsMaxDamageGreaterOrEqualToMinDamage()) {
          GetDiceEdges();
        }
      }
      
      if (DamageWithinInBorders(maxDamage)) {
        _maxDamage = maxDamage;
        if (IsMaxDamageGreaterOrEqualToMinDamage()) {
          GetDiceEdges();
        }
      }

      if (AccurancyWithinInBorders(accuracy)) {
        _accuracy = accuracy;
      }

      _damageList = new List<float>();
    }

    public Attack(List<float> damageList, int accuracy = 0) {
      _damageList = new List<float>();
      _damageList.AddRange(damageList);

      if (_damageList.NotNullAndEmpty()) {
        _minDamage = _damageList[0];
        _maxDamage = _damageList[_damageList.LastIndex()];
      } else {
        _minDamage = 0;
        _maxDamage = 0;
      }

      if (AccurancyWithinInBorders(accuracy)) {
        _accuracy = accuracy;
      }
    }

    public float GetDamage(int edge) {
      if (edge == 0) {
        return 0;
      }

      if (_damageList.CountNotEqual(0)) {
        return _damageList[edge];
      }

      if (edge == DiceEdges) {
        return _maxDamage;
      }

      if (edge == 1) {
        return _minDamage;
      }

      if (DiceEdges != 0) {
        return _maxDamage / DiceEdges * edge;
      }

      return 0;
    }

    public float GetDamage() {
      int edge = GetDiceEdges();
      if (edge == 0) {
        return 0;
      }

      if (edge == DiceEdges) {
        return _maxDamage;
      }

      if (edge == 1) {
        return _minDamage;
      }

      if (DiceEdges != 0) {
        return _maxDamage / DiceEdges * edge;
      }

      return 0;
    }

    private bool IsMaxDamageGreaterOrEqualToMinDamage() {
      return _maxDamage >= _minDamage;
    }

    private int GetDiceEdges() {
      var delta = 0;
      float minD = _minDamage;
      float maxD = _maxDamage;
      if (_damageList.NotNullAndEmpty() && _minDamage != _maxDamage) {
        return _damageList.Count;
      }

      if (_minDamage == _maxDamage) {
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
      delta = (int)deltaDamage;
      DiceEdges = delta;
      return DiceEdges;
    }

    public int DiceEdges { get; private set; }

    public float GetMaxDamage() {
      return _maxDamage;
    }
    

    public float GetMinDamage() {
      return _minDamage;
    }

    public int GetAccuracy() {
      return _accuracy;
    }
  }
}