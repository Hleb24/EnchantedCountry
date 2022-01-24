using Core.Support.Data;
using UnityEngine;

namespace Core.SO.Mock {
  [CreateAssetMenu(menuName = "Mock/QualitiesPoints", fileName = "QualitiesPoints")]
  public class MockQualitiesPoints : UnityEngine.ScriptableObject, IQualityPoints {
    public const string PATH = "Mock/QualitiesPoints";
    [SerializeField]
    private int _strength;
    [SerializeField]
    private int _agility;
    [SerializeField]
    private int _constitution;
    [SerializeField]
    private int _wisdom;
    [SerializeField]
    private int _courage;

    int IQualityPoints.GetQualityPoints(in QualityType qualityType) {
      return qualityType switch {
               QualityType.Strength => _strength,
               QualityType.Agility => _agility,
               QualityType.Constitution => _constitution,
               QualityType.Wisdom => _wisdom,
               QualityType.Courage => _courage,
               _ => default
             };
    }

    void IQualityPoints.SetQualityPoints(in QualityType qualityType, in int qualityPoints) {
      switch (qualityType) {
        case QualityType.Strength:
          _strength = qualityPoints;
          break;
        case QualityType.Agility:
          _agility = qualityPoints;
          break;
        case QualityType.Constitution:
          _constitution = qualityPoints;
          break;
        case QualityType.Wisdom:
          _wisdom = qualityPoints;
          break;
        case QualityType.Courage:
          _courage = qualityPoints;
          break;
        default:
          Debug.LogWarning("Типа качества не существует!");
          break;
      }
    }

    void IQualityPoints.ChangeQualityPoints(in QualityType qualityType, in int qualityPoints) {
      switch (qualityType) {
        case QualityType.Strength:
          _strength += qualityPoints;
          break;
        case QualityType.Agility:
          _agility += qualityPoints;
          break;
        case QualityType.Constitution:
          _constitution += qualityPoints;
          break;
        case QualityType.Wisdom:
          _wisdom += qualityPoints;
          break;
        case QualityType.Courage:
          _courage += qualityPoints;
          break;
        default:
          Debug.LogWarning("Типа качества не существует!");
          break;
      }
    }

    bool IQualityPoints.EnoughQualityPoints(in QualityType qualityType, in int qualityPoints) {
      return qualityType switch {
               QualityType.Strength => _strength - qualityPoints >= 0,
               QualityType.Agility => _agility >= 0,
               QualityType.Constitution => _constitution >= 0,
               QualityType.Wisdom => _wisdom >= 0,
               QualityType.Courage => _courage >= 0,
               _ => default
             };
    }
  }
}