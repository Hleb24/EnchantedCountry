using System.Collections.Generic;
using Core.Support.Data;

namespace Core.Rule.Character.Levels {
  public class DefiningLevels {
    private readonly IGamePoints _gamePoints;
    private readonly ClassType _classType;
    private readonly Levels _levels;
    private int _levelSpell;
    public DefiningLevels(IClassType classType, IGamePoints gamePoints, Levels levels) {
      _levels = levels;
      _classType = classType.GetClassType();
      _gamePoints = gamePoints;
      SetLevelByGamePoints();
      SetSpellLevelForCharacterType();
    }
  
    private void SetLevelByGamePoints() {
      int lvl = -1;
      List<int> levelsList = new List<int>();
      levelsList.AddRange(LevelDictionaries.DefiningLevelsForСharacterTypes[_classType]);
      for (int i = 0; i < levelsList.Count; i++) {
        if (_gamePoints.GetPoints() >= levelsList[i]) {
          lvl++;
        }
      }
      _levels.Level = lvl;
    }

    private void SetSpellLevelForCharacterType() {
      if (LevelDictionaries.DefiningSpellLevelsForСharacterTypes[_classType].ContainsKey(_levels.Level)) {
        _levelSpell = LevelDictionaries.DefiningSpellLevelsForСharacterTypes[_classType][_levels.Level];
      } else {
        _levelSpell = -1;
      }
    }

    public int GetCurrentLevel() {
      return _levels.GetLevel();
    }
    public Levels Levels {
      get {
        return _levels;
      }
      
    }
    
    public int LevelSpell {
      get {
        return _levelSpell;
      }
    }
  }
}