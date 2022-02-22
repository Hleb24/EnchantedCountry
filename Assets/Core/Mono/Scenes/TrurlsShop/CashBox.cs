using System;
using Aberrance.Extensions;
using Core.Main.Character;
using Core.Main.GameRule;
using Core.Mono.BaseClass;
using Core.Mono.MainManagers;
using Core.SO.Equipment;
using Core.SO.ProductObjects;
using Core.SO.Storage;
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
      _startGame = startGame;
      Equipments = _startGame.UseGameSave() ? equipment : _equipment;
    }

    private void OnProductSelected(int id) {
      if (id == EquipmentIdConstants.NO_ARMOR_ID) {
        DisableInteractableForBuyProductButton();
        return;
      }

      _selectedId = id;
      if (CanBuyProduct(_walletIn.GetCoins(), GetPrice(_selectedId)).False()) {
        DisableInteractableForBuyProductButton();
        return;
      }

      if (_buyProduct.interactable.False()) {
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
      int coins = _walletIn.GetCoins();
      if (CanBuyProduct(coins, price)) {
        int leftCoins = coins - price;
        _walletIn.SetCoins(leftCoins);
        AddProductToEquipmentCard(_selectedId);
        BuyProductSuccess?.Invoke();
        BuyProductSuccessAndCheckPrice?.Invoke(_selectedId);
      } else {
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
      Equipments.ChangeQuantity(id, amount);
    }

    private void EnableInteractableForBuyProductButton() {
      _buyProduct.interactable = true;
    }

    private void DisableInteractableForBuyProductButton() {
      _buyProduct.interactable = false;
    }

    public IEquipment Equipments { get; private set; }
  }
}