using System;
using Core.EnchantedCountry.MonoBehaviourScripts.BaseClasses;
using Core.EnchantedCountry.MonoBehaviourScripts.GameSaveSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.EnchantedCountry.MonoBehaviourScripts.Intro {
  public class GoToNextScene: MonoBehaviour  {
    #region FIELDS
    private string _nameOfScene;
    #endregion
    #region MONOBEHAVIOUR_METHODS

    private void Start() {
      Invoke(nameof(NextScene), 0.3f);
    }

    private void NextScene() {
      SetNameOfNextScene();
      LoadNextScene();
    }
    #endregion
    #region LOAD_NEXT_SCENE
    private void SetNameOfNextScene() {
      _nameOfScene = SceneNameConstants.SceneDiceRollsForQualities;

      // if (GSSSingleton.Instance.IsNewGame) {
        // _nameOfScene = SceneNameConstants.SceneDiceRollsForQualities;
      // } else {
        // _nameOfScene = SceneNameConstants.SceneCharacterList;
      // }
    }

    private void LoadNextScene() {
      SceneManager.LoadSceneAsync(_nameOfScene);
    }
    #endregion
  }
}