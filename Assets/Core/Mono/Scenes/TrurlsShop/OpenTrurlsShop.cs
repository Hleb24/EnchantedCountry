using System;
using Core.Mono.MainManagers;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Core.Mono.Scenes.TrurlsShop {
  public class OpenTrurlsShop : MonoBehaviour {
    public static event Action OpenTrurlsShopCanvas;
    [SerializeField]
    private GameObject _trurlsShopCanvas;
    [SerializeField]
    private GameObject _diceRollForCoinsCanvas;
    [SerializeField]
    private Button _openTrurlsShop;
    private ILauncher _launcher;

    private void Start() {
      FirstTimeOpenTrurlsShop();
    }

    private void OnEnable() {
      _openTrurlsShop.onClick.AddListener(OnDiceRollButtonClicked);
    }

    private void OnDisable() {
      _openTrurlsShop.onClick.RemoveListener(OnDiceRollButtonClicked);
    }

    [Inject]
    public void Constructor(ILauncher launcher) {
      _launcher = launcher;
    }

    private void OnDiceRollButtonClicked() {
      OpenShopOfTrurl();
      OpenTrurlsShopCanvas?.Invoke();
    }

    private void FirstTimeOpenTrurlsShop() {
      if (_launcher.IsNewGame()) {
        OpenDiceRollForCoinsCanvas();
        return;
      }

      OpenShopOfTrurl();
    }

    private void OpenShopOfTrurl() {
      _trurlsShopCanvas.SetActive(true);
      _diceRollForCoinsCanvas.SetActive(false);
    }

    private void OpenDiceRollForCoinsCanvas() {
      _diceRollForCoinsCanvas.SetActive(true);
      _trurlsShopCanvas.SetActive(false);
    }
  }
}