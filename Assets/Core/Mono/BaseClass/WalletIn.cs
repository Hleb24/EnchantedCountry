using Core.Main.Character.Wallet;
using Core.Mono.MainManagers;
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
    protected IWallet _wallet;
    protected IStartGame _startGame;

    protected virtual void OnEnable() {
      SetWalletText();
    }

    protected virtual void OnDisable() { }

    [Inject]
    public void Constructor(IStartGame startGame, IWallet wallet) {
      _startGame = startGame;
      _wallet = wallet;
    }

    protected void SetWalletText() {
      if (_startGame.UseGameSave()) {
        _coins.text = _wallet.GetCoins().ToString();
      } else {
        _coins.text = _walletSo.numberOfCoins.ToString();
      }
    }

    public IWallet Wallet {
      get {
        return _wallet;
      }
    }
  }
}