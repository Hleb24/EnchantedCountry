using Core.EnchantedCountry.CoreEnchantedCountry.Character.Wallet;
using Core.EnchantedCountry.MonoBehaviourScripts.GameSaveSystem;
using Core.EnchantedCountry.SupportSystems.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.EnchantedCountry.MonoBehaviourScripts.BaseClasses {

	public class WalletIn : MonoBehaviour {
		#region FIELDS
		[SerializeField]
		protected TMP_Text _coins;
		[SerializeField]
		protected WalletSO _walletSo;
		[SerializeField]
		protected WalletData _walletData;
		protected Wallet _wallet;
		[SerializeField]
		protected bool _useGameSave;
		#endregion
		#region MONOBEHAVIOUR_METHODS
		private void Start() {
			_walletData = GSSSingleton.Instance;
			Invoke(nameof(InitializeWallet), 0.1f);
		}

		protected void InitializeWallet() {
			_wallet = new Wallet(_walletData);
		}

		protected virtual void OnEnable() {
			Invoke(nameof(SetWalletText), 0.2f);
		}

		protected virtual void OnDisable() {
			_walletData.NumberOfCoins = _wallet.coins;
			GSSSingleton.Instance.SaveInGame();
		}
		#endregion
		#region SET_WALLET_TEXT
		protected void SetWalletText() {
			if (_useGameSave) {
				_coins.text = _wallet.coins.ToString();
			} else {
				_coins.text = _walletSo.numberOfCoins.ToString();
			}
		}
		#endregion
		#region PROPERTIES
		public Wallet Wallet{
			get {
				return _wallet;
			}
		}
		#endregion
	}
}
