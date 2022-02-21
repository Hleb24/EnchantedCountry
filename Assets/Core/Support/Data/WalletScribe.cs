﻿using System;
using Aberrance.Extensions;
using Core.Support.SaveSystem.SaveManagers;
using Core.Support.SaveSystem.Scribe;
using UnityEngine.Assertions;

namespace Core.Support.Data {
  /// <summary>
  ///   Интерфейс для работы с монетами в кошельке.
  /// </summary>
  public interface IWallet {
    /// <summary>
    ///   Получить монеты, которые в кошельке.
    /// </summary>
    /// <returns>Монеты персонажа.</returns>
    public int GetCoins();

    /// <summary>
    ///   Присвоить количество монет в кошельке.
    /// </summary>
    /// <param name="coins">Новое значение монет.</param>
    public void SetCoins(int coins);

    /// <summary>
    ///   Изменить количество монет в кошельке.
    /// </summary>
    /// <param name="coins">Количество монет, на которое изменятеся значение монет в кошельке.</param>
    public void ChangeCoins(int coins);

    /// <summary>
    ///   Получить максимальное количество монет, которое может носить персонаж.
    /// </summary>
    /// <returns>Максимальное количество монет</returns>
    public int GetMaxCoins();

    /// <summary>
    ///   Установить новое максимальное количество для персонажа.
    /// </summary>
    /// <param name="maxCoins">Новое максимальное количество монет.</param>
    public void SetMaxCoins(int maxCoins);

    /// <summary>
    ///   Хватает ли монет у персонажа.
    /// </summary>
    /// <param name="coins">Нужное количество монет.</param>
    /// <returns>Истина - монет хватает, ложь - монет не хватает.</returns>
    public bool IsCoinsEnough(int coins);
  }

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