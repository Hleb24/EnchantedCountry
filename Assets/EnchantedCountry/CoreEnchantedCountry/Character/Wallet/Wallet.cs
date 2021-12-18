using Core.EnchantedCountry.MonoBehaviourScripts.GameSaveSystem;
using Core.EnchantedCountry.SupportSystems.Data;

namespace Core.EnchantedCountry.CoreEnchantedCountry.Character.Wallet {
	public class Wallet {
		#region Fields
		private const int BottomBorder = 0;
		private int _maxAmountOfCoins = 0;
		private int _coins;
		private IWallet _wallet;
		#endregion
		#region Constructors
		public Wallet() { }
		public Wallet(int startCoins) {
			_wallet = ScribeDealer.Peek<WalletScribe>();
			coins = startCoins;
		}

		public Wallet(WalletDataSave walletDataSave) {
			_coins = walletDataSave.Coins;
			_maxAmountOfCoins = walletDataSave.MaxCoins;
		}
		#endregion
		#region Properties
		public int coins {
			get {
				return _coins;
			}
			set {
				if (value >= BottomBorder || value <= _maxAmountOfCoins) {
					_coins = value;
					
					_wallet.SetCoins(value);
					GSSSingleton.Instance.SaveInGame();
				} else {
					_coins = BottomBorder;
					_wallet.SetCoins(value);
					GSSSingleton.Instance.SaveInGame();
				}
			}
		}
		public int MaxAmountOfCoins {
			get {
				return _maxAmountOfCoins;
			}
			set {
				_maxAmountOfCoins = value;
				_wallet.SetMaxCoins(value);
				GSSSingleton.Instance.SaveInGame();
			}
		}
		#endregion
	}
}