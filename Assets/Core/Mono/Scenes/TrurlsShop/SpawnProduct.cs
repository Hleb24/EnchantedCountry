using System;
using System.Collections.Generic;
using Core.Main.GameRule;
using Core.SO.ProductObjects;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core.Mono.Scenes.TrurlsShop {
  /// <summary>
  ///   Класс для создание товаров в стартовой лавке Трурля.
  /// </summary>
  public class SpawnProduct : MonoBehaviour {
    private const int TEST_NUMBER_OF_PRODUCT = 5;
    private const int NUMBER_OF_PROJECTILES = 20;
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
    private List<ProductSO> _armorListSo;
    [FormerlySerializedAs("_weaponListSO"), SerializeField]
    private List<ProductSO> _weaponListSo;
    [FormerlySerializedAs("_projectiliesListSO"), SerializeField]
    private List<ProductSO> _projectilesListSo;
    [FormerlySerializedAs("_itemsListSO"), SerializeField]
    private List<ProductSO> _itemsListSo;
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
        InitializeProductFields(_productViewListForArmor[i], _armorListSo[i], TEST_NUMBER_OF_PRODUCT);
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
        InitializeProductFields(_productViewListForWeapon[i], _weaponListSo[i], TEST_NUMBER_OF_PRODUCT);
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
        InitializeProductFields(_productViewListForProjectiles[i], _projectilesListSo[i], NUMBER_OF_PROJECTILES);
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
        InitializeProductFields(_productViewListForItems[i], _itemsListSo[i], TEST_NUMBER_OF_PRODUCT);
      }
    }

    private void InitializeProductFields(ProductView productView, ProductSO productSO, int amount) {
      SetProductIcon(productView, productSO);
      SetProductName(productView, productSO);
      SetProductNumberOfProduct(productView, amount);
      SetProductProperty(productView, productSO);
      SetProductPrice(productView, productSO);
      SetProductId(productView, productSO);
      KitForCharacterType(productView, productSO);
    }

    private void SetProductIcon(ProductView productView, ProductSO productSO) {
      productView.SetIcon(productSO.GetIcon());
    }

    private void SetProductName(ProductView productView, ProductSO productSO) {
      productView.SetName(productSO.GetProductName());
    }

    private void SetProductNumberOfProduct(ProductView productView, int numberOfProduct) {
      productView.SetNumberOfProduct(numberOfProduct.ToString());
    }

    private void SetProductProperty(ProductView productView, ProductSO productSO) {
      productView.SetProperty(productSO.GetProperty());
    }

    private void SetProductPrice(ProductView productView, ProductSO productSO) {
      productView.SetPrice(productSO.GetPrice().ToString());
    }

    private void SetProductId(ProductView productView, ProductSO productSO) {
      productView.SetId(productSO.GetId());
    }

    private void KitForCharacterType(ProductView productView, ProductSO productSO) {
      switch (productSO.GetProductType()) {
        case ProductSO.ProductType.None:
          break;
        case ProductSO.ProductType.Weapon:
          if ((productSO.GetWeaponType() & _characterInTrurlsShop.GetWeaponKit()) == WeaponType.None) {
            productView.ProductNotForCharacterType();
          }

          break;
        case ProductSO.ProductType.Armor:
          if ((productSO.GetArmorType() & _characterInTrurlsShop.GetArmorKit()) == ArmorType.None) {
            productView.ProductNotForCharacterType();
          }

          break;
        case ProductSO.ProductType.Item:
          break;
      }
    }

    public List<ProductView> ProductViewListForArmor {
      get {
        return _productViewListForArmor;
      }
    }

    public List<ProductView> ProductViewListForWeapon {
      get {
        return _productViewListForWeapon;
      }
    }

    public List<ProductView> ProductViewListForProjectiles {
      get {
        return _productViewListForProjectiles;
      }
    }

    public List<ProductView> ProductViewListForItems {
      get {
        return _productViewListForItems;
      }
    }
  }
}