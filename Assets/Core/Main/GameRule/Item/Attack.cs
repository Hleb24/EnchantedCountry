using System.Collections.Generic;
using Aberrance.Extensions;
using JetBrains.Annotations;
using UnityEngine.Assertions;

namespace Core.Main.GameRule.Item {
  public class Attack {
    private readonly List<float> _damageList;
    private readonly float _minDamage;
    private readonly float _maxDamage;
    private readonly int _accuracy;

    public Attack([NotNull] List<float> damageList, int accuracy) {
      Assert.IsNotNull(damageList, nameof(damageList));
      Assert.IsTrue(damageList.NotEmpty(), nameof(damageList));
      _damageList = new List<float>(damageList);
      _minDamage = _damageList[0];
      _maxDamage = _damageList[_damageList.LastIndex()];
      _accuracy = accuracy;
    }

    public float GetDamage(int edge) {
      if (edge == 0) {
        return 0;
      }

      if (_damageList.CountNotEqual(0)) {
        return _damageList[edge];
      }

      if (edge == GetDiceEdges()) {
        return _maxDamage;
      }

      if (edge == 1) {
        return _minDamage;
      }

      return _maxDamage / GetDiceEdges() * edge;
    }

    public float GetMaxDamage() {
      return _maxDamage;
    }

    public float GetMinDamage() {
      return _minDamage;
    }

    public int GetAccuracy() {
      return _accuracy;
    }

    public int GetDiceEdges() {
      return _damageList.Count;
    }
  }
}