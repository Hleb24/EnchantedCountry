using System;
using Core.Main.GameRule.EquipmentIdConstants;
using Core.Mono.BaseClass;
using Core.Mono.MainManagers;
using Core.SO.Equipment;
using Core.SO.Product;
using Core.SO.Storage;
using Core.Support.Data;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Core.Mono.Scenes.TrurlsShop {
  public class CashBox : MonoBehaviour {
    public static event Action BuyProductSuccess;
    public static event Action<int> BuyProductSuccessAndCheckPrice;
    [SerializeField]
    private EquipmentSO _equipment;
    [SerializeField]
    private WalletInTrurlsShop _walletIn;
    [SerializeField]
    private StorageSO _storage;
    [SerializeField]
    private Button _buyProduct;
    private IStartGame _startGame;
    // ReSharper disable once NotAccessedField.Local
    private IWallet _wallet;
    private int _selectedId;

    private void Awake() {
      RemoveAllEquipmentCardsForFirstTrurlsShopOpening();
      DisableInteractableForBuyProductButton();
    }

    private void OnEnable() {
      ProductView.ProductSelected += OnProductSelected;
      ProductSelection.OpenNewListOfProducts += OnOpenNewListOfProducts;
      _buyProduct.onClick.AddListener(BuyButtonClicked);
    }

    private void OnDisable() {
      ProductView.ProductSelected -= OnProductSelected;
      ProductSelection.OpenNewListOfProducts -= OnOpenNewListOfProducts;
      _buyProduct.onClick.RemoveListener(BuyButtonClicked);
    }

    [Inject]
    public void Constructor(IEquipment equipment, IStartGame startGame, IWallet wallet) {
      Equipments = equipment;
      _startGame = startGame;
      _wallet = wallet;
    }

    private void OnProductSelected(int id) {
      if (id == EquipmentIdConstants.NO_ARMOR_ID) {
        DisableInteractableForBuyProductButton();
        return;
      }

      _selectedId = id;
      if (!CanBuyProduct(_walletIn.Wallet.GetCoins(), GetPrice(_selectedId))) {
        DisableInteractableForBuyProductButton();
        return;
      }

      if (!_buyProduct.interactable) {
        EnableInteractableForBuyProductButton();
      }
    }

    private void OnOpenNewListOfProducts() {
      DisableInteractableForBuyProductButton();
    }

    private void BuyButtonClicked() {
      if (_selectedId == EquipmentIdConstants.NO_ARMOR_ID) {
        return;
      }

      int price = GetPrice(_selectedId);
      if (CanBuyProduct(_walletIn.Wallet.GetCoins(), price)) {
        _walletIn.Wallet.SetCoins(_walletIn.Wallet.GetCoins() - price);
        AddProductToEquipmentCard(_selectedId);
        BuyProductSuccess?.Invoke();
        BuyProductSuccessAndCheckPrice?.Invoke(_selectedId);
      }

      if (!CanBuyProduct(_walletIn.Wallet.GetCoins(), GetPrice(_selectedId))) {
        DisableInteractableForBuyProductButton();
      }
    }

    private void RemoveAllEquipmentCardsForFirstTrurlsShopOpening() {
      if (_startGame.StartNewGame()) {
        _equipment.RemoveAllEquipmentCards();
      }
    }

    private int GetPrice(int id) {
      ProductSO product = _storage.GetProductFromList(id);
      return product.GetPrice();
    }

    private bool CanBuyProduct(int coins, int price) {
      return coins >= price;
    }

    private void AddProductToEquipmentCard(int id, int amount = 1) {
      _equipment.IncreaseQuantityOfProduct(id, amount);
      Equipments.ChangeQuantity(id, amount);
    }

    private void EnableInteractableForBuyProductButton() {
      _buyProduct.interactable = true;
    }

    private void DisableInteractableForBuyProductButton() {
      _buyProduct.interactable = false;
    }

    public EquipmentSO Equipment {
      get {
        return _equipment;
      }
    }

    public IEquipment Equipments { get; private set; }
  }
}