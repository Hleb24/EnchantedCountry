using Character;
using Core.Support.Data;
using Core.Support.SaveSystem.SaveManagers;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Core.Mono.Scenes.CharacterList {
  public class LevelInCharacterList : MonoBehaviour {
   
    [SerializeField]
    private TMP_Text _levelText;
    [SerializeField]
    private CharacterInCharacterList _characterInCharacterList;
    [FormerlySerializedAs("_gamePointsInCharacterList"),SerializeField]
    private CharacterGamePoints _characterGamePoints;
    private DefiningLevelsForСharacterTypes _definingLevels;
    private IDealer _dealer;
    private int _level;
    private bool _getGamePoints;
    private bool _getCharacterType;
    
    [Inject]
    private void InjectDealer(IDealer dealer) {
      _dealer = dealer;
    }
    private void Awake() {
      AddListeners();
    }

    private void OnDestroy() {
      RemoveListeners();
    }
    private void AddListeners() {
      _characterInCharacterList.GetCharacterType += OnGetCharacterType;
      _characterGamePoints.LoadGamePoints += OnLoadCharacterGamePoints;
    }

    private void RemoveListeners() {
      _characterInCharacterList.GetCharacterType -= OnGetCharacterType;
      _characterGamePoints.LoadGamePoints -= OnLoadCharacterGamePoints;
    }

    private void OnLoadCharacterGamePoints(int points) {
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
    private void SetLevelText() {
        _definingLevels = new DefiningLevelsForСharacterTypes(_characterInCharacterList.ClassType
          , _dealer.Peek<IGamePoints>());
      _level = _definingLevels.Levels.Level;
      _levelText.text = _level.ToString();
    }
    public int Level {
      get {
        return _level;
      }
    }
  }
}