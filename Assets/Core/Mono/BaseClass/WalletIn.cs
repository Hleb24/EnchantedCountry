using Core.Rule.Character.Wallet;
using Core.SupportSystems.Data;
using Core.SupportSystems.SaveSystem.SaveManagers;
using TMPro;
using UnityEngine;

namespace Core.Mono.BaseClass {
  public class WalletIn : MonoBehaviour {
    [SerializeField]
    protected TMP_Text _coins;
    [SerializeField]
    protected WalletSO _walletSo;
    [SerializeField]
    protected IWallet _wallet;
    [SerializeField]
    protected bool _useGameSave;

    private void Start() {
      Invoke(nameof(InitializeWallet), 0.1f);
    }

    protected virtual void OnEnable() {
      Invoke(nameof(SetWalletText), 0.2f);
    }

    protected virtual void OnDisable() { }

    protected void InitializeWallet() {
      _wallet = ScribeDealer.Peek<WalletScribe>();
    }

    protected void SetWalletText() {
      if (_useGameSave) {
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