using UnityEngine;

namespace Core.Main.Character.Wallet {
	[CreateAssetMenu(menuName = "Wallet", fileName = "Wallet", order = 54)]
	public class WalletSO : UnityEngine.ScriptableObject {
		[SerializeField]
		private int _numberOfCoins;
		private Wallet _wallet;

		public void InitiatedWalletNumberOfCoins(int numberOfCoins) {
			_wallet = new Wallet(numberOfCoins);
			IsInitiatedTrue();
		}
		private void InitiatedWalletNumberOfCoins() {
			_wallet = new Wallet(_numberOfCoins);
			IsInitiatedTrue();
		}

		private void IsInitiatedTrue() {
			_numberOfCoins = _wallet.coins;
		}
		public int numberOfCoins {
			get {
				return _numberOfCoins;
			}
			set {
				_wallet.coins = value;
				_numberOfCoins = _wallet.coins;
			}
		}
	}
}
