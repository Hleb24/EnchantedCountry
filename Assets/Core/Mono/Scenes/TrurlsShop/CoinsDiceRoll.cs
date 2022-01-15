using Core.Rule.Dice;
using Core.Support.Data;
using Core.Support.SaveSystem.SaveManagers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Core.Mono.Scenes.TrurlsShop {
  /// <summary>
  /// Класс отвечает за бросок костей для стартовых монет.
  /// </summary>
  public class CoinsDiceRoll : MonoBehaviour {
    [SerializeField]
    private Button _diceRollButton;
    [SerializeField]
    private TMP_Text _numberOfCoinsText;
    private DiceRollCalculator _diceRollCalculator;
    private IWallet _wallet;
    private IDealer _dealer;
    private int _numberOfCoins;

    [Inject]
    private void InjectDealer(IDealer dealer) {
      _dealer = dealer;
    }

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
      _wallet = _dealer.Peek<IWallet>();
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