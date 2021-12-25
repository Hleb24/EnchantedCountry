using System.Collections.Generic;
using Core.Rule.Character.Levels;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Mono.Scenes.CharacterList {
  public class ProgressPresenterInCharacterList : MonoBehaviour {
    #region FIELDS
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
    private List<ProgressViewInCharacterList> _progressViewList;
    private bool _isSpawnProgress;
    #endregion

    #region MONOBEHAVIOUR_METHODS
    private void Start() {
      AddListeners();
    }

    private void OnDestroy() {
      RemoveListeners();
    }
    #endregion

    #region HANDLERS
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
    #endregion
    
    #region SPAWN_PROGRESS
    private void SpawnProgress() {
      if(_isSpawnProgress)
        return;
      _progressViewList = new List<ProgressViewInCharacterList>();
      for (int i = 0; i < LevelDictionaries.DefiningLevelsForСharacterTypes[default].Count; i++) {
        GameObject gObject = Instantiate(_progressPrefab, _contentOfProgress);
        _progressViewList.Add(gObject.GetComponent<ProgressViewInCharacterList>());
      }
      SetFieldsForProgressViewPrefab();
      _isSpawnProgress = true;
    }

    private void SetFieldsForProgressViewPrefab() {
      int currentLevel = _levelInCharacterList.Level;
      for (int i = 0; i < _progressViewList.Count; i++) {
        int level = i;
        bool isCurrentLevel = currentLevel == level;
        if (!LevelDictionaries.DefiningSpellLevelsForСharacterTypes[_characterInCharacterList.ClassType]
          .ContainsKey(level)) {
          _progressViewList[level].SetTextFields(level
            ,LevelDictionaries.DefiningLevelsForСharacterTypes[_characterInCharacterList.ClassType][level]
            , isCurrentLevel);
        } else {
          _progressViewList[level].SetTextFields(level
            ,LevelDictionaries.DefiningLevelsForСharacterTypes[_characterInCharacterList.ClassType][level]
            ,LevelDictionaries.DefiningSpellLevelsForСharacterTypes[_characterInCharacterList.ClassType][level]
            , isCurrentLevel);
        }
      }
    }
    #endregion
  }
}