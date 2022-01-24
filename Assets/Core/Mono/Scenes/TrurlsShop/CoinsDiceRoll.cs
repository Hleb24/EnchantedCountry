using Core.Main.Dice;
using Core.Support.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Core.Mono.Scenes.TrurlsShop {
  /// <summary>
  ///   Класс отвечает за бросок костей для стартовых монет.
  /// </summary>
  public class CoinsDiceRoll : MonoBehaviour {
    [SerializeField]
    private Button _diceRollButton;
    [SerializeField]
    private TMP_Text _numberOfCoinsText;
    private IWallet _wallet;
    private DiceRollCalculator _diceRollCalculator;
    private int _numberOfCoins;

    private void OnEnable() {
      AddListener();
    }

    private void OnDisable() {
      RemoveListener();
    }

    [Inject]
    public void Constructor(IWallet wallet, DiceRollCalculator diceRollCalculator) {
      _wallet = wallet;
      _diceRollCalculator = diceRollCalculator;
    }

    private void AddListener() {
      _diceRollButton.onClick.AddListener(CoinsDiceRollAndSwitchUI);
    }

    private void RemoveListener() {
      _diceRollButton.onClick.RemoveListener(CoinsDiceRollAndSwitchUI);
    }

    private void CoinsDiceRollAndSwitchUI() {
      _numberOfCoins = GetNumberOfCoins();
      SetNumberOfCoinsInWallet();
      SetUITextForNumberOfCoins();
      DeactivateDiceRollButton();
    }

    private int GetNumberOfCoins() {
      return _diceRollCalculator.GetSumDiceRollForCoins();
    }

    private void SetNumberOfCoinsInWallet() {
      _wallet.SetCoins(_numberOfCoins);
    }

    private void SetUITextForNumberOfCoins() {
      _numberOfCoinsText.text = _wallet.GetCoins().ToString();
    }

    private void DeactivateDiceRollButton() {
      _diceRollButton.gameObject.SetActive(false);
    }
  }
}