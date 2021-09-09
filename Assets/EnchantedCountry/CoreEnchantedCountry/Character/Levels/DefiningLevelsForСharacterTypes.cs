// ReSharper disable once CheckNamespace

using System.Collections.Generic;
using Core.EnchantedCountry.CoreEnchantedCountry.Character;
using Core.EnchantedCountry.CoreEnchantedCountry.Character.GamePoints;
using Core.EnchantedCountry.CoreEnchantedCountry.Character.Levels;

namespace Character {
  public class DefiningLevelsForСharacterTypes {
    #region FIELDS
    private GamePoints _gamePoints;
    private Levels _levels;
    private CharacterType _characterType;
    private int _levelSpell;
    #endregion
    #region CONTSTRUCTOR
    public DefiningLevelsForСharacterTypes(CharacterType characterType, int gamePoints) {
      _levels = new Levels();
      _characterType = characterType;
      _gamePoints = new GamePoints(gamePoints);
      SetLevelByGamePoints();
      SetSpellLevelForCharacterType();
    }
    public DefiningLevelsForСharacterTypes(CharacterType characterType, GamePoints gamePoints) {
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
        if (_gamePoints.Points >= levelsList[i]) {
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