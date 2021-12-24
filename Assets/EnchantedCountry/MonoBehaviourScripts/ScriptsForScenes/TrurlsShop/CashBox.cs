using System;
using Core.EnchantedCountry.CoreEnchantedCountry.Character.Wallet;
using Core.EnchantedCountry.CoreEnchantedCountry.GameRule.EquipmentIdConstants;
using Core.EnchantedCountry.MonoBehaviourScripts.BaseClasses;
using Core.EnchantedCountry.MonoBehaviourScripts.GameSaveSystem;
using Core.EnchantedCountry.ScriptableObject.EquipmentSO;
using Core.EnchantedCountry.ScriptableObject.ProductObject;
using Core.EnchantedCountry.ScriptableObject.Storage;
using Core.EnchantedCountry.SupportSystems.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Core.EnchantedCountry.MonoBehaviourScripts.ScriptsForScenes.TrurlsShop {
	public class CashBox : MonoBehaviour {
		#region FIELDS
		[SerializeField]
		private EquipmentSO _equipment;
		[SerializeField]
		private WalletInTrurlsShop _walletIn;
		[SerializeField]
		private StorageSO _storage;
		[SerializeField]
		private Button _buyProduct;
		private IEquipment _equipments;
		// ReSharper disable once NotAccessedField.Local
		private IWallet _wallet;
		private int _selectedId;


		public static event Action BuyProductSuccess;
		public static event Action<int> BuyProductSuccessAndCheckPrice;
		#endregion
		#region MONOBEHAVIOUR_METHODS
		private void Awake() {
			RemoveAllEquipmentCardsForFirstTrurlsShopOpening();
			DisableInteractableForBuyProductButton();
		}

		private void Start() {
			_wallet = ScribeDealer.Peek<WalletScribe>();
			_equipments = ScribeDealer.Peek<EquipmentsScribe>();
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

	
		#endregion
		#region HANDLERS
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
			if (_selectedId == EquipmentIdConstants.NoArmorId)
				return;
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
		#endregion
		#region FIRST_OPENING
		private void RemoveAllEquipmentCardsForFirstTrurlsShopOpening() {
			if (Leviathan.Instance.IsNewGame) {
				_equipment.RemoveAllEquipmentCards();
			}
		}
		#endregion
		#region BUY_PRODUCT
		private int GetPrice(int id) {
			ProductSO product = _storage.GetProductFromList(id);
			return product.price;
		}

		private bool CanBuyProduct(int coins, int price) {
			return coins >= price;
		}

		private void AddProductToEquipmentCard(int id, int amount = 1) {
			_equipment.IncreaseQuantityOfProduct(id, amount);
			_equipments.ChangeQuantity(id, amount);
		}
		#endregion
		#region TOGGLE_BUTTON
		private void EnableInteractableForBuyProductButton() {
			_buyProduct.interactable = true;
		}
		private void DisableInteractableForBuyProductButton() {
			_buyProduct.interactable = false;
		}
		#endregion
		#region PROPERTIES
		public EquipmentSO Equipment {
			get {
				return _equipment;
			}
		}

		public IEquipment Equipments {
			get {
				return _equipments;
			}
		}
		#endregion
	}
}
