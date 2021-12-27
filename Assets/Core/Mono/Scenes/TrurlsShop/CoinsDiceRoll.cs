using Core.Rule.Dice;
using Core.Support.Data;
using Core.Support.SaveSystem.SaveManagers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Mono.Scenes.TrurlsShop {
  public class CoinsDiceRoll : MonoBehaviour {
    [SerializeField]
    private Button _diceRollButton;
    [SerializeField]
    private TMP_Text _numberOfCoinsText;
    private DiceRollCalculator _diceRollCalculator;
    private IWallet _wallet;
    private int _numberOfCoins;

    private void Start() {
      Init();
    }

    private void OnEnable() {
      AddListener();
    }

    private void OnDisable() {
      RemoveListener();
    }

    private void Init() {
      _wallet = ScribeDealer.Peek<WalletScribe>();
      _diceRollCalculator = new DiceRollCalculator();
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