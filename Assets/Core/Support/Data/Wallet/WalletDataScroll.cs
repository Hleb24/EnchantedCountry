using System;

namespace Core.Support.Data.Wallet {
  [Serializable]
  public struct WalletDataScroll {
    public int Coins;
    public int MaxCoins;

    public WalletDataScroll(int coins, int maxCoins) {
      Coins = coins;
      MaxCoins = maxCoins;
    }
  }
}