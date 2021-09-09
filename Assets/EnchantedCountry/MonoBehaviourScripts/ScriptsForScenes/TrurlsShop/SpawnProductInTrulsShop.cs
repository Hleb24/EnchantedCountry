using System;
using System.Collections.Generic;
using Core.EnchantedCountry.ScriptableObject.ProductObject;
using UnityEngine;
using UnityEngine.Serialization;
using static Core.EnchantedCountry.CoreEnchantedCountry.GameRule.Armor.Armor;
using static Core.EnchantedCountry.CoreEnchantedCountry.GameRule.Weapon.Weapon;

namespace Core.EnchantedCountry.MonoBehaviourScripts.ScriptsForScenes.TrurlsShop {

	public class SpawnProductInTrulsShop : MonoBehaviour {
		#region FIELDS
		private const int TEST_NUMBER_OF_PRODUCT = 5;
		private const int NUMBER_OF_PROJECTILIES = 20;
		[SerializeField]
		private CharacterInTrurlsShop _characterInTrurlsShop;
		[SerializeField]
		private GameObject _productPrefab;
		[SerializeField]
		private List<ProductView> _productViewListForArmor;
		[SerializeField]
		private List<ProductView> _productViewListForWeapon;
		[FormerlySerializedAs("_productViewListForProjectilies"),SerializeField]
		private List<ProductView> _productViewListForProjectiles;
		[SerializeField]
		private List<ProductView> _productViewListForItems;
		[FormerlySerializedAs("_armorListSO"),SerializeField]
		private List<ProductSO> _armorListSo;
		[FormerlySerializedAs("_weaponListSO"),SerializeField]
		private List<ProductSO> _weaponListSo;
		[FormerlySerializedAs("_projectiliesListSO"),SerializeField]
		private List<ProductSO> _projectilesListSo;
		[FormerlySerializedAs("_itemsListSO"),SerializeField]
		private List<ProductSO> _itemsListSo;
		[SerializeField]
		private Transform _contentArmor;
		[SerializeField]
		private Transform _contentWeapon;
		[SerializeField]
		private Transform _contentItem;
		public static event Action SpawnCompleted;
		#endregion
		#region GET_PRODUCT_LIST
		public List<ProductSO> GetArmorList() {
			return _armorListSo;
		}

		public List<ProductSO> GetWeaponList() {
			return _weaponListSo;
		}
		public List<ProductSO> GetProjectilesList() {
			return _projectilesListSo;
		}

		public List<ProductSO> GetItemList() {
			return _itemsListSo;
		}
		#endregion
		#region MONOBEHAVIOUR_METHODS
		private void Awake() {
			_characterInTrurlsShop.GetCharacterType += Spawn;
		}

		private void OnDestroy() {
			_characterInTrurlsShop.GetCharacterType -= Spawn;
		}
		#endregion
		#region SPAWN_PRODUCT
		private void Spawn() {
			SpawnWeapon();
			SpawnArmor();
			SpawnProjectiles();
			SpawnItems();
			SpawnCompleted?.Invoke();
		}
		private void SpawnArmor() {
			_productViewListForArmor = new List<ProductView>();
			for (int i = 0; i < _armorListSo.Count; i++) {
				GameObject product = Instantiate(_productPrefab, _contentArmor);
				ProductView productView = product.GetComponent<ProductView>();
				_productViewListForArmor.Add(productView);
			}
			for (int i = 0; i < _armorListSo.Count; i++) {
				int index = i;
				InitializeProductFields(_productViewListForArmor[index], _armorListSo[index], TEST_NUMBER_OF_PRODUCT);
			}
		}

		private void SpawnWeapon() {
			_productViewListForWeapon = new List<ProductView>();
			for (int i = 0; i < _weaponListSo.Count; i++) {
				GameObject product = Instantiate(_productPrefab, _contentWeapon);
				ProductView productView = product.GetComponent<ProductView>();
				_productViewListForWeapon.Add(productView);
			}
			for (int i = 0; i < _weaponListSo.Count; i++) {
				int index = i;
				InitializeProductFields(_productViewListForWeapon[index], _weaponListSo[index], TEST_NUMBER_OF_PRODUCT);
			}
		}

		private void SpawnProjectiles() {
			_productViewListForProjectiles = new List<ProductView>();
			for (int i = 0; i < _projectilesListSo.Count; i++) {
				GameObject product = Instantiate(_productPrefab, _contentWeapon);
				ProductView productView = product.GetComponent<ProductView>();
				_productViewListForProjectiles.Add(productView);
			}
			for (int i = 0; i < _projectilesListSo.Count; i++) {
				int index = i;
				InitializeProductFields(_productViewListForProjectiles[index], _projectilesListSo[index], NUMBER_OF_PROJECTILIES);
			}
		}
		private void SpawnItems() {
			_productViewListForItems = new List<ProductView>();
			for (int i = 0; i < _itemsListSo.Count; i++) {
				GameObject product = Instantiate(_productPrefab, _contentItem);
				ProductView productView = product.GetComponent<ProductView>();
				_productViewListForItems.Add(productView);
			}
			for (int i = 0; i < _itemsListSo.Count; i++) {
				int index = i;
				InitializeProductFields(_productViewListForItems[index], _itemsListSo[index], TEST_NUMBER_OF_PRODUCT);
			}
		}
		#endregion
		#region INITIALIZE_PRODUCT_FIELDS 
		private void InitializeProductFields(ProductView productView, ProductSO productSo, int amount) {
			SetProductIcon(productView, productSo);
			SetProductName(productView, productSo);
			SetProductNumberOfProduct(productView, amount);
			SetProductProperty(productView, productSo);
			SetProductPrice(productView, productSo);
			SetProductId(productView, productSo);
			KitForCharacterType(productView, productSo);
		}
		private void SetProductIcon(ProductView productView, ProductSO productSo) {
			productView.Icon.sprite = productSo.icon;
		}
		private void SetProductName(ProductView productView, ProductSO productSo) {
			productView.Name.text = productSo.productName;
		}
		private void SetProductNumberOfProduct(ProductView productView, int numberOfProduct) {
			productView.NumberOfProduct.text = numberOfProduct.ToString();
		}
		private void SetProductProperty(ProductView productView, ProductSO productSo) {
			productView.Property.text = productSo.Property;
		}
		private void SetProductPrice(ProductView productView, ProductSO productSo) {
			productView.Price.text = productSo.price.ToString();
		}

		private void SetProductId(ProductView productView, ProductSO productSo) {
			productView.Id = productSo.id;
		}

		private void KitForCharacterType(ProductView productView, ProductSO productSo) {
			switch (productSo.productType) {
				case ProductSO.ProductType.None:
					break;
				case ProductSO.ProductType.Weapon:
						if((productSo.GetWeaponType() & _characterInTrurlsShop.GetWeaponKit()) == WeaponType.None) {
							productView.ProductNotForCharacterType();
						}
						break;
				case ProductSO.ProductType.Armor:
						if((productSo.GetArmorType() & _characterInTrurlsShop.GetArmorKit()) == ArmorType.None) {
							productView.ProductNotForCharacterType();
						}
						break;
				case ProductSO.ProductType.Item:
					break;
			}
		}
		#endregion
		#region ADD_TO_PRODUCT_VIEW_LIST

		#endregion
		#region PROPERTIES
		public List<ProductView> ProductViewListForArmor {
			get {
				return _productViewListForArmor;
			}
			set {
				_productViewListForArmor = value;
			}
		}

		public List<ProductView> ProductViewListForWeapon {
			get {
				return _productViewListForWeapon;
			}
			set {
				_productViewListForWeapon = value;
			}
		}

		public List<ProductView> ProductViewListForProjectiles {
			get {
				return _productViewListForProjectiles;
			}
			set {
				_productViewListForProjectiles = value;
			}
		}

		public List<ProductView> ProductViewListForItems {
			get {
				return _productViewListForItems;
			}
			set {
				_productViewListForItems = value;
			}
		}
		#endregion
	}
}
