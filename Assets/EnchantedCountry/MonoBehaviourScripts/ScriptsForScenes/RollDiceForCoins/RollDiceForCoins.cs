using Core.EnchantedCountry.CoreEnchantedCountry.Character.CharacterCreation;
using Core.EnchantedCountry.MonoBehaviourScripts.GameSaveSystem;
using Core.EnchantedCountry.SupportSystems;
using Core.EnchantedCountry.SupportSystems.Data;
using Core.EnchantedCountry.SupportSystems.SaveSystem.SaveManagers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Core.EnchantedCountry.MonoBehaviourScripts.ScriptsForScenes.RollDiceForCoins {
  public class RollDiceForCoins : MonoBehaviour {
    [SerializeField]
    private Button _diceRollButton;
    [SerializeField]
    private TMP_Text _numberOfCoinsText;
    [SerializeField]
    private bool _useGameSave;
    [Inject]
    private CharacterCreation _characterCreation;
    private IWallet _wallet;
    private int _numberOfCoins;

    private void Start() {
      _wallet = ScribeDealer.Peek<WalletScribe>();
      // if (_useGameSave) {
      // 	_walletData = GSSSingleton.Instance;
      // }
    }

    private void OnEnable() {
      AddListener();
    }

    private void OnDisable() {
      RemoveListener();
    }

    private void AddListener() {
      _diceRollButton.onClick.AddListener(RollDiceForCoinsSaveDataSetUITextForNumberOfCoinsAndDeactivatedButton);
    }

    private void RemoveListener() {
      _diceRollButton.onClick.RemoveListener(RollDiceForCoinsSaveDataSetUITextForNumberOfCoinsAndDeactivatedButton);
    }

    private void RollDiceForCoinsSaveDataSetUITextForNumberOfCoinsAndDeactivatedButton() {
      _numberOfCoins = GetNumberOfCoins();
      SetNumberOfCoinsInWalletDataAndReturnValue();
      SetUITextForNumberOfCoins();
      DeactivateDiceRollButton();
    }

    private int GetNumberOfCoins() {
      return _characterCreation.GetSumDiceRollForCoins();
    }

    // ReSharper disable once UnusedMethodReturnValue.Local
    private int SetNumberOfCoinsInWalletDataAndReturnValue() {
      _wallet.SetCoins(_numberOfCoins);
      return _wallet.GetCoins();
    }

    private void SetUITextForNumberOfCoins() {
      _numberOfCoinsText.text = _wallet.GetCoins().ToString();
    }

    private void DeactivateDiceRollButton() {
      _diceRollButton.gameObject.SetActive(false);
    }
  }
}