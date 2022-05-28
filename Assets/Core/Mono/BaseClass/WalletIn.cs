using Core.Main.Character.Item;
using Core.Mono.MainManagers;
using Core.SO.Wallet;
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
    private ILauncher _launcher;

    protected virtual void OnEnable() {
      SetWalletText();
    }

    protected virtual void OnDisable() { }

    [Inject]
    public void Constructor(ILauncher launcher, IWallet wallet) {
      _launcher = launcher;
      _iWallet = wallet;
      _wallet = _launcher.UseGameSave() ? new Wallet(_iWallet) : new Wallet(_walletSo);
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