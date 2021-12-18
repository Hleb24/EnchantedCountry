using System;
using UnityEngine.Assertions;

namespace Core.EnchantedCountry.SupportSystems.Data {
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
    public bool CoinsEnough(int coins);
  }

  /// <summary>
  ///   Класс для хранения данных о монетах персонажа.
  /// </summary>
  [Serializable]
  public class WalletScribe : IScribe, IWallet {
    private const int StartMaxCoins = 100;
    private WalletDataSave _walletDataSave;

    void IScribe.Init(SaveGame saveGame) {
      _walletDataSave = new WalletDataSave(0, StartMaxCoins);
      if (saveGame is null) {
        return;
      }

      saveGame.WalletDataSave = _walletDataSave;
    }

    void IScribe.Save(SaveGame saveGame) {
      saveGame.WalletDataSave = _walletDataSave;
    }

    void IScribe.Loaded(SaveGame saveGame) {
      _walletDataSave.Coins = saveGame.WalletDataSave.Coins;
      _walletDataSave.MaxCoins = saveGame.WalletDataSave.MaxCoins;
    }

    int IWallet.GetCoins() {
      return _walletDataSave.Coins;
    }

    void IWallet.SetCoins(int coins) {
      _walletDataSave.Coins = coins;
    }

    void IWallet.ChangeCoins(int coins) {
      _walletDataSave.Coins += coins;
      if (_walletDataSave.Coins < 0) {
        _walletDataSave.Coins = 0;
      }
    }

    int IWallet.GetMaxCoins() {
      return _walletDataSave.MaxCoins;
    }

    void IWallet.SetMaxCoins(int maxCoins) {
      Assert.IsTrue(maxCoins >= 0);
      _walletDataSave.MaxCoins = maxCoins;
    }

    bool IWallet.CoinsEnough(int coins) {
      _walletDataSave.Coins -= coins;
      return _walletDataSave.Coins >= 0;
    }
  }

  [Serializable]
  public struct WalletDataSave {
    public int Coins;
    public int MaxCoins;

    public WalletDataSave(int coins, int maxCoins) {
      Coins = coins;
      MaxCoins = maxCoins;
    }
  }
}