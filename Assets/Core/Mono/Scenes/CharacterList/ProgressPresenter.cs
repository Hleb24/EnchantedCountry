using System.Collections.Generic;
using Aberrance.Extensions;
using Core.Main.Character;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Mono.Scenes.CharacterList {
  public class ProgressPresenter : MonoBehaviour {
    [Header("Set in Inspector"), SerializeField]
    private GameObject _progressPrefab;
    [SerializeField]
    private Transform _contentOfProgress;
    [SerializeField]
    private CharacterInCharacterList _characterInCharacterList;
    [SerializeField]
    private LevelInCharacterList _levelInCharacterList;
    [SerializeField]
    private GameObject _canvasCharacterList;
    [SerializeField]
    private GameObject _canvasProgress;
    [SerializeField]
    private Button _progressButton;
    [SerializeField]
    private Button _closeButton;
    [Header("Set dynamically"), SerializeField]
    private List<ProgressView> _progressViewList;
    private bool _isSpawnProgress;

    private void Start() {
      AddListeners();
    }

    private void OnDestroy() {
      RemoveListeners();
    }

    private void AddListeners() {
      _progressButton.onClick.AddListener(OnOpenButtonClicked);
      _closeButton.onClick.AddListener(OnCloseButtonClicked);
    }

    private void RemoveListeners() {
      _progressButton.onClick.RemoveListener(OnOpenButtonClicked);
      _closeButton.onClick.RemoveListener(OnCloseButtonClicked);
    }

    private void OnOpenButtonClicked() {
      _canvasCharacterList.SetActive(false);
      _canvasProgress.SetActive(true);
      SpawnProgress();
    }

    private void OnCloseButtonClicked() {
      _canvasCharacterList.SetActive(true);
      _canvasProgress.SetActive(false);
    }

    private void SpawnProgress() {
      if (_isSpawnProgress) {
        return;
      }

      _progressViewList = new List<ProgressView>();
      ClassType classType = _characterInCharacterList.ClassTypeEnum;
      for (var i = 0; i < LevelDictionaries.GetNumberOfLevelsByClassType(classType); i++) {
        GameObject gObject = Instantiate(_progressPrefab, _contentOfProgress);
        _progressViewList.Add(gObject.GetComponent<ProgressView>());
      }

      SetFieldsForProgressViewPrefab();
      _isSpawnProgress = true;
    }

    private void SetFieldsForProgressViewPrefab() {
      int currentLevel = _levelInCharacterList.GetCurrentLevel();
      ClassType characterType = _characterInCharacterList.ClassTypeEnum;
      for (var i = 0; i < _progressViewList.Count; i++) {
        int level = i;
        bool isCurrentLevel = currentLevel == level;
        int gamePoints = LevelDictionaries.GetGamePointsByCharacterLevel(characterType, level);
        if (LevelDictionaries.HasSpellLevelInCharacterLevel(characterType, level).False()) {
          _progressViewList[level].SetTextFields(level, gamePoints, isCurrentLevel);
        } else {
          int spellLevel = LevelDictionaries.GetSpellLevelByCharacterLevel(characterType, level);
          _progressViewList[level].SetTextFields(level, gamePoints, spellLevel, isCurrentLevel);
        }
      }
    }
  }
}