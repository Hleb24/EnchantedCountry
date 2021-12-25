using System;
using Core.Mono.BaseClass;
using Core.Mono.MainManagers;
using Core.Rule.GameRule.EquipmentIdConstants;
using Core.ScriptableObject.Equipment;
using Core.ScriptableObject.Products;
using Core.ScriptableObject.Storage;
using Core.SupportSystems.Data;
using Core.SupportSystems.SaveSystem.SaveManagers;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Core.Mono.Scenes.TrurlsShop {
  public class CashBox : MonoBehaviour {
    public static event Action BuyProductSuccess;
    public static event Action<int> BuyProductSuccessAndCheckPrice;
    [SerializeField]
    private EquipmentObject _equipment;
    [SerializeField]
    private WalletInTrurlsShop _walletIn;
    [SerializeField]
    private StorageObject _storage;
    [SerializeField]
    private Button _buyProduct;
    // ReSharper disable once NotAccessedField.Local
    private IWallet _wallet;
    [Inject]
    private IStartGame _startGame;
    private int _selectedId;

    private void Awake() {
      RemoveAllEquipmentCardsForFirstTrurlsShopOpening();
      DisableInteractableForBuyProductButton();
    }

    private void Start() {
      _wallet = ScribeDealer.Peek<WalletScribe>();
      Equipments = ScribeDealer.Peek<EquipmentsScribe>();
    }

    private void OnEnable() {
      ProductView.ProductSelected += OnProductSelected;
      ProductSelection.OpenNewListOfProducts += OnOpenNewListOfProducts;
      OpenTrurlsShop.OpenTrurlsShopCanvas += OnOpenTrurlsShopCanvas;
      _buyProduct.onClick.AddListener(BuyButtonClicked);
    }

    private void OnDisable() {
      ProductView.ProductSelected -= OnProductSelected;
      ProductSelection.OpenNewListOfProducts -= OnOpenNewListOfProducts;
      OpenTrurlsShop.OpenTrurlsShopCanvas -= OnOpenTrurlsShopCanvas;
      _buyProduct.onClick.RemoveListener(BuyButtonClicked);
    }

    private void OnProductSelected(int id) {
      if (id == EquipmentIdConstants.NoArmorId) {
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
      if (_selectedId == EquipmentIdConstants.NoArmorId) {
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

    private void OnOpenTrurlsShopCanvas() {
      _wallet = ScribeDealer.Peek<WalletScribe>();
    }

    private void RemoveAllEquipmentCardsForFirstTrurlsShopOpening() {
      if (_startGame.StartNewGame()) {
        _equipment.RemoveAllEquipmentCards();
      }
    }

    private int GetPrice(int id) {
      ProductObject product = _storage.GetProductFromList(id);
      return product.price;
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

    public EquipmentObject Equipment {
      get {
        return _equipment;
      }
    }

    public IEquipment Equipments { get; private set; }
  }
}