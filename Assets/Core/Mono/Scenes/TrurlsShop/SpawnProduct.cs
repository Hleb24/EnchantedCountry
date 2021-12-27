using System;
using System.Collections.Generic;
using Core.ScriptableObject.Products;
using UnityEngine;
using UnityEngine.Serialization;
using static Core.Rule.GameRule.Armor;
using static Core.Rule.GameRule.Weapon;

namespace Core.Mono.Scenes.TrurlsShop {
	/// <summary>
	///   Класс для создание товаров в стартовой лавке Трурля.
	/// </summary>
	public class SpawnProduct : MonoBehaviour {
    private const int TestNumberOfProduct = 5;
    private const int NumberOfProjectiles = 20;
    public static event Action SpawnCompleted;
    [SerializeField]
    private CharacterInTrurlsShop _characterInTrurlsShop;
    [SerializeField]
    private GameObject _productPrefab;
    [SerializeField]
    private List<ProductView> _productViewListForArmor;
    [SerializeField]
    private List<ProductView> _productViewListForWeapon;
    [FormerlySerializedAs("_productViewListForProjectilies"), SerializeField]
    private List<ProductView> _productViewListForProjectiles;
    [SerializeField]
    private List<ProductView> _productViewListForItems;
    [FormerlySerializedAs("_armorListSO"), SerializeField]
    private List<ProductObject> _armorListSo;
    [FormerlySerializedAs("_weaponListSO"), SerializeField]
    private List<ProductObject> _weaponListSo;
    [FormerlySerializedAs("_projectiliesListSO"), SerializeField]
    private List<ProductObject> _projectilesListSo;
    [FormerlySerializedAs("_itemsListSO"), SerializeField]
    private List<ProductObject> _itemsListSo;
    [SerializeField]
    private Transform _contentArmor;
    [SerializeField]
    private Transform _contentWeapon;
    [SerializeField]
    private Transform _contentItem;

    private void Awake() {
      _characterInTrurlsShop.GetCharacterType += Spawn;
    }

    private void OnDestroy() {
      _characterInTrurlsShop.GetCharacterType -= Spawn;
    }

    private void Spawn() {
      SpawnWeapon();
      SpawnArmor();
      SpawnProjectiles();
      SpawnItems();
      SpawnCompleted?.Invoke();
    }

    private void SpawnArmor() {
      _productViewListForArmor = new List<ProductView>();
      for (var i = 0; i < _armorListSo.Count; i++) {
        GameObject product = Instantiate(_productPrefab, _contentArmor);
        var productView = product.GetComponent<ProductView>();
        _productViewListForArmor.Add(productView);
      }

      for (var i = 0; i < _armorListSo.Count; i++) {
        InitializeProductFields(_productViewListForArmor[i], _armorListSo[i], TestNumberOfProduct);
      }
    }

    private void SpawnWeapon() {
      _productViewListForWeapon = new List<ProductView>();
      for (var i = 0; i < _weaponListSo.Count; i++) {
        GameObject product = Instantiate(_productPrefab, _contentWeapon);
        var productView = product.GetComponent<ProductView>();
        _productViewListForWeapon.Add(productView);
      }

      for (var i = 0; i < _weaponListSo.Count; i++) {
        InitializeProductFields(_productViewListForWeapon[i], _weaponListSo[i], TestNumberOfProduct);
      }
    }

    private void SpawnProjectiles() {
      _productViewListForProjectiles = new List<ProductView>();
      for (var i = 0; i < _projectilesListSo.Count; i++) {
        GameObject product = Instantiate(_productPrefab, _contentWeapon);
        var productView = product.GetComponent<ProductView>();
        _productViewListForProjectiles.Add(productView);
      }

      for (var i = 0; i < _projectilesListSo.Count; i++) {
        InitializeProductFields(_productViewListForProjectiles[i], _projectilesListSo[i], NumberOfProjectiles);
      }
    }

    private void SpawnItems() {
      _productViewListForItems = new List<ProductView>();
      for (var i = 0; i < _itemsListSo.Count; i++) {
        GameObject product = Instantiate(_productPrefab, _contentItem);
        var productView = product.GetComponent<ProductView>();
        _productViewListForItems.Add(productView);
      }

      for (var i = 0; i < _itemsListSo.Count; i++) {
        InitializeProductFields(_productViewListForItems[i], _itemsListSo[i], TestNumberOfProduct);
      }
    }

    private void InitializeProductFields(ProductView productView, ProductObject productObject, int amount) {
      SetProductIcon(productView, productObject);
      SetProductName(productView, productObject);
      SetProductNumberOfProduct(productView, amount);
      SetProductProperty(productView, productObject);
      SetProductPrice(productView, productObject);
      SetProductId(productView, productObject);
      KitForCharacterType(productView, productObject);
    }

    private void SetProductIcon(ProductView productView, ProductObject productObject) {
      productView.SetIcon(productObject.GetIcon());
    }

    private void SetProductName(ProductView productView, ProductObject productObject) {
      productView.SetName(productObject.GetProductName());
    }

    private void SetProductNumberOfProduct(ProductView productView, int numberOfProduct) {
      productView.SetNumberOfProduct(numberOfProduct.ToString());
    }

    private void SetProductProperty(ProductView productView, ProductObject productObject) {
      productView.SetProperty(productObject.GetProperty());
    }

    private void SetProductPrice(ProductView productView, ProductObject productObject) {
      productView.SetPrice(productObject.GetPrice().ToString());
    }

    private void SetProductId(ProductView productView, ProductObject productObject) {
      productView.SetId(productObject.GetId());
    }

    private void KitForCharacterType(ProductView productView, ProductObject productObject) {
      switch (productObject.GetProductType()) {
        case ProductObject.ProductType.None:
          break;
        case ProductObject.ProductType.Weapon:
          if ((productObject.GetWeaponType() & _characterInTrurlsShop.GetWeaponKit()) == WeaponType.None) {
            productView.ProductNotForCharacterType();
          }

          break;
        case ProductObject.ProductType.Armor:
          if ((productObject.GetArmorType() & _characterInTrurlsShop.GetArmorKit()) == ArmorType.None) {
            productView.ProductNotForCharacterType();
          }

          break;
        case ProductObject.ProductType.Item:
          break;
      }
    }

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
  }
}