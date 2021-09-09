using Core.EnchantedCountry.CoreEnchantedCountry.Character.CharacterCreation;
using Core.EnchantedCountry.MonoBehaviourScripts.GameSaveSystem;
using Core.EnchantedCountry.SupportSystems;
using Core.EnchantedCountry.SupportSystems.Data;
using Core.EnchantedCountry.SupportSystems.SaveSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Core.EnchantedCountry.MonoBehaviourScripts.ScriptsForScenes.RollDiceForCoins {
	public class RollDiceForCoins : MonoBehaviour {
		#region FIELDS
		[SerializeField]
		private Button _diceRollButton;
		[SerializeField]
		private TMP_Text _numberOfCoinsText;
		[SerializeField]
		private bool _useGameSave;
		private WalletData _walletData;
		[Inject]
		private CharacterCreation _characterCreation;
		private int _numberOfCoins;
		
		#endregion
		#region MONOBEHAVIOUR_METHODS
		private void Start() {
			if (_useGameSave) {
				_walletData = GSSSingleton.Singleton;
			}
		}

		private void OnEnable() {
			AddListener();
		}

		private void OnDisable() {
			RemoveListener();
		}
		#endregion
		#region HANDLERS
		private void AddListener() {
			_diceRollButton.onClick.AddListener(RollDiceForCoinsSaveDataSetUITextForNumberOfCoinsAndDeactivatedButton);
		}
		private void RemoveListener() {
			_diceRollButton.onClick.RemoveListener(RollDiceForCoinsSaveDataSetUITextForNumberOfCoinsAndDeactivatedButton);
		}
		#endregion
		#region ROLL_DICE_FOR_COINS
		private void RollDiceForCoinsSaveDataSetUITextForNumberOfCoinsAndDeactivatedButton() {
			_numberOfCoins = GetNumberOfCoins();
			SetNumberOfCoinsInWalletDataAndReturnValue();
			SaveNumberOfCoinsInWallet();
			SetUITextForNumberOfCoins();
			DeactivateDiceRollButton();
		}

		private int GetNumberOfCoins() {
			return _characterCreation.GetSumDiceRollForCoins();
		}

		// ReSharper disable once UnusedMethodReturnValue.Local
		private int SetNumberOfCoinsInWalletDataAndReturnValue() {
			_walletData.NumberOfCoins = _numberOfCoins;
			GSSSingleton.Singleton.SaveInGame();
			return _walletData.NumberOfCoins;
		}

		private void SaveNumberOfCoinsInWallet() {
			SaveSystem.Save(_walletData, SaveSystem.Constants.WALLET);
		}

		private void SetUITextForNumberOfCoins() {
			GenericTools.SetUIText(_numberOfCoinsText, _walletData.NumberOfCoins.ToString());
		}

		private void DeactivateDiceRollButton() {
			_diceRollButton.gameObject.SetActive(false);
		}
		#endregion
	}
	
}
