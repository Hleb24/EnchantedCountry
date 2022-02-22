using Core.Main.Character;
using UnityEngine;
using UnityEngine.Assertions;

namespace Core.SO.Wallet {
  [CreateAssetMenu(menuName = "Wallet", fileName = "Wallet", order = 54)]
  public class WalletSO : ScriptableObject, IWallet {
    [SerializeField]
    private int _coins;
    [SerializeField]
    private int _maxCoins;

    public int GetCoins() {
      return _coins;
    }

    public void SetCoins(int coins) {
      if (coins < 0) {
        coins = 0;
      } else if (coins > _maxCoins) {
        coins = _maxCoins;
      }

      _coins = coins;
    }

    public void ChangeCoins(int coins) {
      if (IsCoinsEnough(coins)) {
        _coins += coins;
        if (_coins > _maxCoins) {
          _coins = _maxCoins;
        }

        return;
      }

      _coins = 0;
    }

    public int GetMaxCoins() {
      return _maxCoins;
    }

    public void SetMaxCoins(int maxCoins) {
      Assert.IsTrue(maxCoins >= 0, nameof(maxCoins));
      _maxCoins = maxCoins;
    }

    public bool IsCoinsEnough(int coins) {
      return _coins + coins >= 0;
    }
  }
}