using System;
using Aberrance.Extensions;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Core.Mono.BaseClass {
  /// <summary>
  ///   Класс для перехода на следующую сцену.
  /// </summary>
  public class GoToScene : MonoBehaviour {
    [SerializeField]
    protected Scene _nameOfScene;
    [SerializeField]
    protected Button _goToScene;

    private void OnEnable() {
      AddListener();
    }

    private void OnDisable() {
      RemoveListener();
    }

    protected void AddListener() {
      _goToScene.onClick.AddListener(GoToNextSceneFuncAsync);
    }

    protected void RemoveAllListener() {
      _goToScene.onClick.RemoveAllListeners();
    }

    protected void EnableInteractableForButton() {
      _goToScene.interactable = true;
    }

    protected void DisableInteractableForButton() {
      _goToScene.interactable = false;
    }

    private async void GoToNextSceneFuncAsync() {
      if (PreloadSceneAction.IsNotNull()) {
        await PreloadSceneAction.Invoke();
      }

      await SceneManager.LoadSceneAsync((int)_nameOfScene);
    }

    private void RemoveListener() {
      _goToScene.onClick.RemoveListener(GoToNextSceneFuncAsync);
      PreloadSceneAction = null;
    }

    public Func<UniTask> PreloadSceneAction { get; set; }
  }
}