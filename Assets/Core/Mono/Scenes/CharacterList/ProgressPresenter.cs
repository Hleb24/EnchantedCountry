using System.Collections.Generic;
using Core.Main.Character.Levels;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Mono.Scenes.CharacterList {
  public class ProgressPresenter : MonoBehaviour {
    [Header("Set in Inspector"),SerializeField]
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
    [Header("Set dynamically"),SerializeField]
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
      if(_isSpawnProgress)
        return;
      _progressViewList = new List<ProgressView>();
      for (int i = 0; i < LevelDictionaries.DefiningLevelsForСharacterTypes[default].Count; i++) {
        GameObject gObject = Instantiate(_progressPrefab, _contentOfProgress);
        _progressViewList.Add(gObject.GetComponent<ProgressView>());
      }
      SetFieldsForProgressViewPrefab();
      _isSpawnProgress = true;
    }

    private void SetFieldsForProgressViewPrefab() {
      int currentLevel = _levelInCharacterList.GetCurrentLevel();
      for (int i = 0; i < _progressViewList.Count; i++) {
        int level = i;
        bool isCurrentLevel = currentLevel == level;
        if (!LevelDictionaries.DefiningSpellLevelsForСharacterTypes[_characterInCharacterList.ClassTypeEnum]
          .ContainsKey(level)) {
          _progressViewList[level].SetTextFields(level
            ,LevelDictionaries.DefiningLevelsForСharacterTypes[_characterInCharacterList.ClassTypeEnum][level]
            , isCurrentLevel);
        } else {
          _progressViewList[level].SetTextFields(level
            ,LevelDictionaries.DefiningLevelsForСharacterTypes[_characterInCharacterList.ClassTypeEnum][level]
            ,LevelDictionaries.DefiningSpellLevelsForСharacterTypes[_characterInCharacterList.ClassTypeEnum][level]
            , isCurrentLevel);
        }
      }
    }
  }
}