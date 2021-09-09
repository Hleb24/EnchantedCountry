using System;
using Core.EnchantedCountry.SupportSystems.SaveSystem;
using UnityEngine.Serialization;

namespace Core.EnchantedCountry.SupportSystems.Data {
  [Serializable]
  public class WalletData: ResetSave {
    [FormerlySerializedAs("numberOfCoins")]
    public int NumberOfCoins;
    [FormerlySerializedAs("TopBorder")]
    public int MaxAmountOfCoins;

    private WalletData(){}
    public WalletData(int coins = 0, int maxAmountOfCoins = 100) {
      NumberOfCoins = coins;
      MaxAmountOfCoins = maxAmountOfCoins;
    }
    public void Reset() {
      NumberOfCoins = default;
      MaxAmountOfCoins = 100;
    }
  }
}