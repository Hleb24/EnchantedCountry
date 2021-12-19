using Character;
using Core.EnchantedCountry.SupportSystems.Data;
using TMPro;
using UnityEngine;

namespace Core.EnchantedCountry.MonoBehaviourScripts.ScriptsForScenes.CharacterList {
  public class LevelInCharacterList : MonoBehaviour {
    #region FIELDS
    [SerializeField]
    private TMP_Text _levelText;
    [SerializeField]
    private CharacterInCharacterList _characterInCharacterList;
    [SerializeField]
    private GamePointsInCharacterList _gamePointsInCharacterList;
    private DefiningLevelsForСharacterTypes _definingLevels;
    private int _level;
    private bool _getGamePoints;
    private bool _getCharacterType;
    #endregion
    #region MONOBEHAVIOUR_METHODS
    private void Awake() {
      AddListenrs();
    }

    private void OnDestroy() {
      RemoveListeners();
    }
    #endregion
    #region HANDLERS
    private void AddListenrs() {
      _characterInCharacterList.GetCharacterType += OnGetCharacterType;
      _gamePointsInCharacterList.LoadGamePoints += OnLoadGamePoints;
    }

    private void RemoveListeners() {
      _characterInCharacterList.GetCharacterType -= OnGetCharacterType;
      _gamePointsInCharacterList.LoadGamePoints -= OnLoadGamePoints;
    }

    private void OnLoadGamePoints(int points) {
      _getGamePoints = true;
      if (_getGamePoints && _getCharacterType) {
        SetLevelText();
      }
    }

    private void OnGetCharacterType() {
      _getCharacterType = true;
      if (_getGamePoints && _getCharacterType) {
        SetLevelText();
      }
    }
    #endregion
    #region SET_LEVEL_TEXT
    private void SetLevelText() {
        _definingLevels = new DefiningLevelsForСharacterTypes(_characterInCharacterList.CharacterType
          , ScribeDealer.Peek<GamePointsScribe>());
      _level = _definingLevels.Levels.Level;
      _levelText.text = _level.ToString();
    }
    #endregion
    #region PROPERTIES
    public int Level {
      get {
        return _level;
      }
    }
    #endregion
  }
}