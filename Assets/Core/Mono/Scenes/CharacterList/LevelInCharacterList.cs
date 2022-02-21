using Core.Main.Character;
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
    [FormerlySerializedAs("_gamePointsInCharacterList"), SerializeField]
    private CharacterGamePoints _characterGamePoints;
    private CharacterLevel _characterLevel;
    private bool _getGamePoints;
    private bool _getCharacterType;

    private void Awake() {
      AddListeners();
    }

    private void OnDestroy() {
      RemoveListeners();
    }

    [Inject]
    public void Constructor(CharacterLevel characterLevel) {
      _characterLevel = characterLevel;
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
      _levelText.text = GetCurrentLevel().ToString();
    }

    public int GetCurrentLevel() {
    return  _characterLevel.GetCurrentLevel();
    }

  }
}