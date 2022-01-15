using Core.Rule.Character.Wallet;
using Core.Support.Data;
using Core.Support.SaveSystem.SaveManagers;
using TMPro;
using UnityEngine;
using Zenject;

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
    private IDealer _dealer;

    [Inject]
    private void InjectDealer(IDealer dealer) {
      _dealer = dealer;
    }

    private void Start() {
      Invoke(nameof(InitializeWallet), 0.1f);
    }

    protected virtual void OnEnable() {
      Invoke(nameof(SetWalletText), 0.2f);
    }

    protected virtual void OnDisable() { }

    protected void InitializeWallet() {
      _wallet = _dealer.Peek<IWallet>();
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