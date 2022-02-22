using System.Collections.Generic;
using Core.Main.Character.Class;
using Core.Main.GameRule.Point;
using JetBrains.Annotations;
using UnityEngine.Assertions;

namespace Core.Main.Character.Level {
  public class CharacterLevel {
    private readonly IGamePoints _gamePoints;
    private readonly ClassType _classType;
    private readonly BaseLevel _baseLevel;

    public CharacterLevel([NotNull] IClassType classType, [NotNull] IGamePoints gamePoints, [NotNull] BaseLevel baseLevel) {
      Assert.IsNotNull(classType, nameof(classType));
      Assert.IsNotNull(gamePoints, nameof(gamePoints));
      Assert.IsNotNull(baseLevel, nameof(baseLevel));
      _classType = classType.GetClassType();
      _gamePoints = gamePoints;
      _baseLevel = baseLevel;
      SetLevelByGamePoints();
    }

    public int GetCurrentLevel() {
      return _baseLevel.GetCurrentLevel();
    }

    public int GetSpellLevelForCharacterType() {
      return LevelDictionaries.GetSpellLevelByCharacterLevel(_classType, _baseLevel.GetCurrentLevel());
    }

    private void SetLevelByGamePoints() {
      int defaultLevel = -1;
      List<int> levelProgress = LevelDictionaries.GetCharacterLevelsProgress(_classType);
      for (var i = 0; i < levelProgress.Count; i++) {
        if (_gamePoints.GetPoints() >= levelProgress[i]) {
          defaultLevel++;
        }
      }

      _baseLevel.SetCurrentLevel(defaultLevel);
    }
  }
}