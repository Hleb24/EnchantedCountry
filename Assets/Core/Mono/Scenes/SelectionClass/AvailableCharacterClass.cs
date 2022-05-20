using System;
using Core.Main.Character.Class;
using Core.Main.Character.Quality;
using JetBrains.Annotations;
using UnityEngine.Assertions;

namespace Core.Mono.Scenes.SelectionClass {
  public class AvailableCharacterClass {
    private readonly int _lowerLimitQualityValueForClass;
    private readonly bool[] _canBeSet = new bool[Enum.GetValues(typeof(ClassType)).Length];
    private readonly IQualityPoints _qualityPoints;

    public AvailableCharacterClass([NotNull] IQualityPoints qualityPoints, int lowerLimit = 9) {
      Assert.IsNotNull(qualityPoints, nameof(qualityPoints));
      _qualityPoints = qualityPoints;
      _lowerLimitQualityValueForClass = lowerLimit;
      FillCanBeSet();
    }

    public bool CanBe(ClassType classType) {
      return _canBeSet[(int)classType];
    }

    private void FillCanBeSet() {
      _canBeSet[(int)ClassType.Warrior] = _qualityPoints.GetQualityPoints(QualityType.Strength) >= _lowerLimitQualityValueForClass;
      _canBeSet[(int)ClassType.Elf] = _qualityPoints.GetQualityPoints(QualityType.Strength) >= _lowerLimitQualityValueForClass &&
                                      _qualityPoints.GetQualityPoints(QualityType.Courage) >= _lowerLimitQualityValueForClass;
      _canBeSet[(int)ClassType.Wizard] = _qualityPoints.GetQualityPoints(QualityType.Wisdom) >= _lowerLimitQualityValueForClass;
      _canBeSet[(int)ClassType.Kron] = _qualityPoints.GetQualityPoints(QualityType.Agility) >= _lowerLimitQualityValueForClass &&
                                       _qualityPoints.GetQualityPoints(QualityType.Wisdom) >= _lowerLimitQualityValueForClass;
      _canBeSet[(int)ClassType.Gnom] = _qualityPoints.GetQualityPoints(QualityType.Agility) >= _lowerLimitQualityValueForClass &&
                                       _qualityPoints.GetQualityPoints(QualityType.Constitution) >= _lowerLimitQualityValueForClass;
      _canBeSet[(int)ClassType.Human] = true;
    }
  }
}