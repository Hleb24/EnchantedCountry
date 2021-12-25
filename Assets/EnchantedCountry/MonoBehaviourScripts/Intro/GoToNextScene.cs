using System.Threading.Tasks;
using Core.EnchantedCountry.MonoBehaviourScripts.BaseClasses;
using Core.EnchantedCountry.MonoBehaviourScripts.MainManagers;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Core.EnchantedCountry.MonoBehaviourScripts.Intro {
  public class GoToNextScene : MonoBehaviour {
    [Inject]
    private IStartGame _startGame;
    private string _nameOfScene;

    private void Start() {
      NextScene();
    }

    private async void NextScene() {
      if (_startGame.StillInitializing()) {
        await Task.Yield();
      }

      SetNameOfNextScene();
      LoadNextScene();
    }

    private void SetNameOfNextScene() {
      _nameOfScene = SceneNamesConstants.SCENE_DICE_ROLLS_FOR_QUALITIES;

      _nameOfScene = _startGame.IsNewGame() ? SceneNamesConstants.SCENE_DICE_ROLLS_FOR_QUALITIES : SceneNamesConstants.SCENE_CHARACTER_LIST;
    }

    private void LoadNextScene() {
      SceneManager.LoadSceneAsync(_nameOfScene);
    }
  }
}