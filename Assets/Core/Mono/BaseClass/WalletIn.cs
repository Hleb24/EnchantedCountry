using Core.Main.Character;
using Core.Mono.MainManagers;
using Core.SO.Wallet;
using Core.Support.Data;
using TMPro;
using UnityEngine;
using Zenject;

namespace Core.Mono.BaseClass {
  public class WalletIn : MonoBehaviour {
    [SerializeField]
    protected TMP_Text _coins;
    [SerializeField]
    protected WalletSO _walletSo;
    private IWallet _iWallet;
    private Wallet _wallet;
    private IStartGame _startGame;

    protected virtual void OnEnable() {
      SetWalletText();
    }

    protected virtual void OnDisable() { }

    [Inject]
    public void Constructor(IStartGame startGame, IWallet wallet) {
      _startGame = startGame;
      _iWallet = wallet;
      _wallet = _startGame.UseGameSave() ? new Wallet(_iWallet) : new Wallet(_walletSo);
    }

    public void SetMaxCoins(int coins) {
      _wallet.SetMaxCoins(coins);
    }

    public int GetCoins() {
      return _wallet.GetCoins();
    }

    public void SetCoins(int coins) {
      _wallet.SetCoins(coins);
    }

    protected void SetWalletText() {
      _coins.text = _wallet.GetCoins().ToString();
    }
  }
}