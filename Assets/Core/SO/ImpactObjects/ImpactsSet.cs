using System.Collections.Generic;
using Core.Main.GameRule.Impact;
using JetBrains.Annotations;
using UnityEngine;

namespace Core.SO.ImpactObjects {
  [CreateAssetMenu(menuName = "ImpactsSO/ImpactsSet", fileName = "ImpactsSet")]
  public class ImpactsSet : ScriptableObject, IImpactsSet {
    [SerializeField]
    private List<ImpactsSO> _impactsSet;

    [CanBeNull]
    public Impact<IImpactOnRiskPoints> GetImpactOnRiskPoints(int impactId) {
      for (var i = 0; i < _impactsSet.Count; i++) {
        if (_impactsSet[i].GetId() == impactId) {
          return _impactsSet[i].GetImpact();
        }
      }

      Debug.LogWarning("Импакт не найден!");
      return null;
    }
  }
}