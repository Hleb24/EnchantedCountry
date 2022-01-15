using System.Threading.Tasks;
using Core.Mono.BaseClass;
using Core.Mono.MainManagers;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;
using Scene = Core.Mono.BaseClass.Scene;

namespace Core.Mono.Scenes.Intro {
  /// <summary>
  /// Класс для перехода на слудующую сцену со сцены <see cref="Scene.Intro"/>
  /// </summary>
  public class GoToNextScene : MonoBehaviour {
    [Inject]
    private IStartGame _startGame;
    private Scene _nameOfScene;

    private void Start() {
      NextScene();
    }

    private void NextScene() {
      SetNameOfNextScene();
      LoadNextSceneAsync();
    }

    private void SetNameOfNextScene() {
      _nameOfScene = Scene.DiceRolls;
      _nameOfScene = _startGame.IsNewGame()? Scene.DiceRolls : Scene.CharacterList;
    }

    private  void LoadNextSceneAsync() {
      SceneManager.LoadSceneAsync((int)_nameOfScene);
    }
  }
}