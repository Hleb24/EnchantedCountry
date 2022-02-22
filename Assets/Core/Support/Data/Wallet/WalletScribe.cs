using System;
using Aberrance.Extensions;
using Core.Main.Character;
using Core.Support.SaveSystem.SaveManagers;
using Core.Support.SaveSystem.Scribe;
using UnityEngine.Assertions;

namespace Core.Support.Data.Wallet {
  /// <summary>
  ///   Класс для хранения данных о монетах персонажа.
  /// </summary>
  [Serializable]
  public class WalletScribe : IScribe, IWallet {
    private const int StartMaxCoins = 100;
    private WalletDataScroll _walletDataScroll;

    void IScribe.Init(Scrolls scrolls) {
      _walletDataScroll = new WalletDataScroll(0, StartMaxCoins);
      if (scrolls.Null()) {
        return;
      }

      scrolls.WalletDataScroll = _walletDataScroll;
    }

    void IScribe.Save(Scrolls scrolls) {
      scrolls.WalletDataScroll = _walletDataScroll;
    }

    void IScribe.Loaded(Scrolls scrolls) {
      _walletDataScroll.Coins = scrolls.WalletDataScroll.Coins;
      _walletDataScroll.MaxCoins = scrolls.WalletDataScroll.MaxCoins;
    }

    int IWallet.GetCoins() {
      return _walletDataScroll.Coins;
    }

    void IWallet.SetCoins(int coins) {
      if (coins < 0) {
        coins = 0;
      } else if (coins > _walletDataScroll.MaxCoins) {
        coins = _walletDataScroll.MaxCoins;
      }

      _walletDataScroll.Coins = coins;
    }

    void IWallet.ChangeCoins(int coins) {
      _walletDataScroll.Coins += coins;
      if (_walletDataScroll.Coins < 0) {
        _walletDataScroll.Coins = 0;
      } else if (_walletDataScroll.Coins > _walletDataScroll.MaxCoins) {
        _walletDataScroll.Coins = _walletDataScroll.MaxCoins;
      }
    }

    int IWallet.GetMaxCoins() {
      return _walletDataScroll.MaxCoins;
    }

    void IWallet.SetMaxCoins(int maxCoins) {
      Assert.IsTrue(maxCoins >= 0);
      _walletDataScroll.MaxCoins = maxCoins;
    }

    bool IWallet.IsCoinsEnough(int coins) {
      return _walletDataScroll.Coins + coins >= 0;
    }
  }
}