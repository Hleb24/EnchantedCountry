// ReSharper disable once CheckNamespace

using System.Collections.Generic;
using Core.EnchantedCountry.CoreEnchantedCountry.Character;
using Core.EnchantedCountry.CoreEnchantedCountry.Character.GamePoints;
using Core.EnchantedCountry.CoreEnchantedCountry.Character.Levels;
using Core.EnchantedCountry.SupportSystems.Data;

namespace Character {
  public class DefiningLevelsForСharacterTypes {
    #region FIELDS
    private IGamePoints _gamePoints;
    private Levels _levels;
    private CharacterType _characterType;
    private int _levelSpell;
    #endregion
    #region CONTSTRUCTOR
    public DefiningLevelsForСharacterTypes(CharacterType characterType, IGamePoints gamePoints) {
      _levels = new Levels();
      _characterType = characterType;
      _gamePoints = gamePoints;
      SetLevelByGamePoints();
      SetSpellLevelForCharacterType();
    }
  
    #endregion
    #region SET_FIELD
    private void SetLevelByGamePoints() {
    
      int lvl = -1;
      List<int> levelsList = new List<int>();
      levelsList.AddRange(LevelDictionaries.DefiningLevelsForСharacterTypes[_characterType]);
      for (int i = 0; i < levelsList.Count; i++) {
        if (_gamePoints.GetPoints() >= levelsList[i]) {
          lvl++;
        }
      }
      _levels.Level = lvl;
    }

    private void SetSpellLevelForCharacterType() {
      if (LevelDictionaries.DefiningSpellLevelsForСharacterTypes[_characterType].ContainsKey(_levels.Level)) {
        _levelSpell = LevelDictionaries.DefiningSpellLevelsForСharacterTypes[_characterType][_levels.Level];
      } else {
        _levelSpell = -1;
      }
    }
    #endregion
    #region PROPERTIES
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
    #endregion
  }
}