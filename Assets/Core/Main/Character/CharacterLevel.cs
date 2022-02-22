using System.Collections.Generic;
using Core.Main.GameRule;
using JetBrains.Annotations;
using UnityEngine.Assertions;

namespace Core.Main.Character {
  public class CharacterLevel {
    private readonly IGamePoints _gamePoints;
    private readonly ClassType _classType;
    private readonly Level _level;

    public CharacterLevel([NotNull] IClassType classType, [NotNull] IGamePoints gamePoints, [NotNull] Level level) {
      Assert.IsNotNull(classType, nameof(classType));
      Assert.IsNotNull(gamePoints, nameof(gamePoints));
      Assert.IsNotNull(level, nameof(level));
      _classType = classType.GetClassType();
      _gamePoints = gamePoints;
      _level = level;
      SetLevelByGamePoints();
    }

    public int GetCurrentLevel() {
      return _level.GetCurrentLevel();
    }

    public int GetSpellLevelForCharacterType() {
      return LevelDictionaries.GetSpellLevelByCharacterLevel(_classType, _level.GetCurrentLevel());
    }

    private void SetLevelByGamePoints() {
      int defaultLevel = -1;
      List<int> levelProgress = LevelDictionaries.GetCharacterLevelsProgress(_classType);
      for (var i = 0; i < levelProgress.Count; i++) {
        if (_gamePoints.GetPoints() >= levelProgress[i]) {
          defaultLevel++;
        }
      }

      _level.SetCurrentLevel(defaultLevel);
    }
  }
}