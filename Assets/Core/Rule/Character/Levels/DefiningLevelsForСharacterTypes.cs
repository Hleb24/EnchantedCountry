// ReSharper disable once CheckNamespace

using System.Collections.Generic;
using Core.Rule.Character;
using Core.Rule.Character.Levels;
using Core.SupportSystems.Data;

namespace Character {
  public class DefiningLevelsForСharacterTypes {
    private IGamePoints _gamePoints;
    private Levels _levels;
    private ClassType _classType;
    private int _levelSpell;
    public DefiningLevelsForСharacterTypes(ClassType classType, IGamePoints gamePoints) {
      _levels = new Levels();
      _classType = classType;
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
    public Levels Levels {
      get {
        return _levels;
      }
      set {
        _levels = value;
      }
    }
    
    public int LevelSpell {
      get {
        return _levelSpell;
      }
    }
  }
}