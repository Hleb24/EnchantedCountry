﻿using Core.EnchantedCountry.MonoBehaviourScripts.GameSaveSystem;
using Core.EnchantedCountry.SupportSystems.Data;

namespace Core.EnchantedCountry.CoreEnchantedCountry.Character.Wallet {
	public class Wallet {
		#region Fields
		private const int BottomBorder = 0;
		private int _maxAmountOfCoins = 0;
		private int _coins;
		#endregion
		#region Constructors
		public Wallet() { }
		public Wallet(int startCoins) {
			coins = startCoins;
		}

		public Wallet(WalletData walletData) {
			_coins = walletData.NumberOfCoins;
			_maxAmountOfCoins = walletData.MaxAmountOfCoins;
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
					GSSSingleton.Instance.GetWalletData().NumberOfCoins = value;
					GSSSingleton.Instance.SaveInGame();
				} else {
					_coins = BottomBorder;
					GSSSingleton.Instance.GetWalletData().NumberOfCoins = value;
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
				GSSSingleton.Instance.GetWalletData().MaxAmountOfCoins = value;
				GSSSingleton.Instance.SaveInGame();
			}
		}
		#endregion
	}
}