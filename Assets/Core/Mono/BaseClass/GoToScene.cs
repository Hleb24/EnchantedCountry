using System;
using Core.Animations;
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
    [SerializeField]
    private LevelLoaderAnimation _levelLoader;
    
    private void OnEnable() {
      AddListener();
    }

    private void OnDisable() {
      RemoveListener();
    }

    protected void AddListener() {
      _goToScene.onClick.AddListener(GoToNextSceneAsync);
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

    private async void GoToNextSceneAsync() {
      await _levelLoader.StartTransitionAnimation().ContinueWith(GoToNextScene());
    }

    private Func<UniTask> GoToNextScene() {
      return async () => await SceneManager.LoadSceneAsync((int)_nameOfScene);
    }

    private void RemoveListener() {
      _goToScene.onClick.RemoveListener(() => SceneManager.LoadSceneAsync((int)_nameOfScene));
    }
  }

  public enum Scene {
    /// <summary>
    ///   Начальная сцена для загрузки данных игровой сессии.
    /// </summary>
    Intro,
    /// <summary>
    ///   Сцена бросков кубиков для качеств.
    /// </summary>
    DiceRolls,
    /// <summary>
    ///   Сцена выбора класса персонажа.
    /// </summary>
    SelectClass,
    /// <summary>
    ///   Сцена повышение качеств для класса - Волшебник.
    /// </summary>
    WizardImprovement,
    /// <summary>
    ///   Сцена повышение качеств для класса - Крон.
    /// </summary>
    KronImprovement,
    /// <summary>
    ///   Сцена стартового магазина Трурля.
    /// </summary>
    TrurlsShop,
    /// <summary>
    ///   Сцена листа персонажа.
    /// </summary>
    CharacterList,
    /// <summary>
    ///   Сцена битвы.
    /// </summary>
    Fight
  }
}