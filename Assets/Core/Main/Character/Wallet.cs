using Core.Support.Data;
using JetBrains.Annotations;
using UnityEngine.Assertions;

namespace Core.Main.Character {
  public class Wallet {
    private readonly IWallet _wallet;

    public Wallet([NotNull] IWallet wallet) {
      Assert.IsNotNull(wallet, nameof(wallet));
      _wallet = wallet;
    }

    public int GetCoins() {
      return _wallet.GetCoins();
    }

    public void SetCoins(int coins) {
      _wallet.SetCoins(coins);
    }

    public void SetMaxCoins(int coins) {
      _wallet.SetMaxCoins(coins);
    }

    public void ChangeCoins(int coins) {
      _wallet.ChangeCoins(coins);
    }

    public bool IsCoinsEnough(int coins) {
      return _wallet.IsCoinsEnough(coins);
    }
  }
}