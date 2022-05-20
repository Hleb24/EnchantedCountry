using System;
using Aberrance.Extensions;
using Core.Main.Character.Item;
using Core.Support.Data.Equipment;
using Core.Support.SaveSystem.SaveManagers;
using Core.Support.SaveSystem.Scribe;
using UnityEngine.Assertions;

namespace Core.Support.Data.Wallet {
  /// <summary>
  ///   Класс для хранения данных о монетах персонажа.
  /// </summary>
  [Serializable]
  public class WalletScribe : IScribe, IWallet {
    private static WalletScribe _originWalletScribe;
    private const int START_MAX_COINS = 100;
    private WalletDataScroll _walletDataScroll;

    public T Clone<T>() {
      return (T)MemberwiseClone();
    }

    public T CloneWithTracking<T>() {
      IsTracking = true;
      return Clone<T>();
    }

    public void ReplaceOriginal<T>(T newOriginValue) {
      if (newOriginValue is WalletScribe walletScribe) {
        _originWalletScribe = walletScribe;
      }
    }

    public void ReplaceOriginal() {
      _originWalletScribe = this;
    }

    void IScribe.SaveOnQuit(Scrolls scrolls) {
      bool changeOrigin = ScribeHandler.ChangeOrigin(this, this, _originWalletScribe);
      if (changeOrigin) {
        _originWalletScribe = this;
      }

      IsTracking = false;
      scrolls.WalletDataScroll = _originWalletScribe._walletDataScroll;
    }

    void IScribe.Init(Scrolls scrolls) {
      _walletDataScroll = new WalletDataScroll(0, START_MAX_COINS);
      UpdateLastChanged();
      _originWalletScribe = this;
      if (scrolls.IsNull()) {
        return;
      }

      scrolls.WalletDataScroll = _originWalletScribe._walletDataScroll;
    }

    void IScribe.Save(Scrolls scrolls) {
      scrolls.WalletDataScroll = _originWalletScribe._walletDataScroll;
    }

    void IScribe.Loaded(Scrolls scrolls) {
      _walletDataScroll.Coins = scrolls.WalletDataScroll.Coins;
      _walletDataScroll.MaxCoins = scrolls.WalletDataScroll.MaxCoins;
      _originWalletScribe = this;
    }

    int IWallet.GetCoins() {
      return _walletDataScroll.Coins;
    }

    void IWallet.SetCoins(int coins) {
      UpdateLastChanged();
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
      UpdateLastChanged();
      _walletDataScroll.MaxCoins = maxCoins;
    }

    bool IWallet.IsCoinsEnough(int coins) {
      return _walletDataScroll.Coins + coins >= 0;
    }

    private void UpdateLastChanged() {
      LastChanged = DateTime.Now;
    }

    public bool IsTracking { get; private set; }

    public DateTime LastChanged { get; private set; }
  }
}