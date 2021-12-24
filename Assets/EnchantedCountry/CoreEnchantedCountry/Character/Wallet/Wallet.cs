using Core.EnchantedCountry.MonoBehaviourScripts.GameSaveSystem;
using Core.EnchantedCountry.SupportSystems.Data;
using Core.EnchantedCountry.SupportSystems.SaveSystem.SaveManagers;

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

		public Wallet(WalletDataScroll walletDataScroll) {
			_coins = walletDataScroll.Coins;
			_maxAmountOfCoins = walletDataScroll.MaxCoins;
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
				} else {
					_coins = BottomBorder;
					_wallet.SetCoins(value);
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
			}
		}
		#endregion
	}
}