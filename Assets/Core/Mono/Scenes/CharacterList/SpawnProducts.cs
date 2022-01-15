using System;
using System.Collections.Generic;
using Core.Rule.Character.Equipment;
using Core.Rule.GameRule.EquipmentIdConstants;
using Core.ScriptableObject.Equipment;
using Core.ScriptableObject.Products;
using Core.ScriptableObject.Storage;
using Core.Support.Data;
using Core.Support.SaveSystem.SaveManagers;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;
using static Core.Rule.GameRule.Armor;
using static Core.Rule.GameRule.Weapon;

namespace Core.Mono.Scenes.CharacterList {
  public class SpawnProducts : MonoBehaviour {
    private const int TestNumberOfProduct = 5;
    private const int NumberOfProjectiles = 20;

    public static event Action SpawnCompleted;
    [SerializeField]
    private CharacterInCharacterList _characterInCharacterList;
    [SerializeField]
    private GameObject _productPrefab;
    [SerializeField]
    // ReSharper disable once NotAccessedField.Local
    private EquipmentObject _equipment;
    [SerializeField]
    private StorageObject _storage;
    [SerializeField]
    private List<ProductsView> _productViewListForArmor;
    [SerializeField]
    private List<ProductsView> _productViewListForWeapon;
    [SerializeField]
    private List<ProductsView> _productViewListForProjectilies;
    [SerializeField]
    private List<ProductsView> _productViewListForItems;
    [SerializeField]
    private List<ProductObject> _armorListSo;
    [SerializeField]
    private List<ProductObject> _weaponListSo;
    [FormerlySerializedAs("_projectiliesListSo"), SerializeField]
    private List<ProductObject> _projectilesListSo;
    [SerializeField]
    private List<ProductObject> _itemsListSo;
    [SerializeField]
    private Transform _contentArmor;
    [SerializeField]
    private Transform _contentWeapon;
    [SerializeField]
    private Transform _contentItem;
    [SerializeField]
    private List<int> _itemWhatCanUsed;
    private IEquipment _equipments;
    private IDealer _dealer;

    [Inject]
    private void InjectDealer(IDealer dealer) {
      _dealer = dealer;
    }

    private void Awake() {
      _characterInCharacterList.GetCharacterType += SetListsSoAndSpawnProducts;
      SetItemWhatCanUsedList();
    }

    private void Start() {
      _equipments = _dealer.Peek<IEquipment>();
    }

    private void OnDestroy() {
      _characterInCharacterList.GetCharacterType -= SetListsSoAndSpawnProducts;
    }

    private void SetItemWhatCanUsedList() {
      _itemWhatCanUsed = new List<int>();
      _itemWhatCanUsed.AddRange(EquipmentIdConstants.Bags);
      _itemWhatCanUsed.AddRange(EquipmentIdConstants.Animals);
      _itemWhatCanUsed.AddRange(EquipmentIdConstants.Carriages);
    }

    private void SetListsSoAndSpawnProducts() {
      SetArmorListSo(_equipments.GetEquipmentCards(), _storage.armorList);
      SetWeaponListSo(_equipments.GetEquipmentCards(), _storage.weaponList);
      SetProjectilesListSo(_equipments.GetEquipmentCards(), _storage.projectiliesList);
      SetItemListSo(_equipments.GetEquipmentCards(), _storage.itemList);
      Spawn();
    }

    private void SetArmorListSo(List<EquipmentCard> equipmentCards, List<ProductObject> armorList) {
      _armorListSo = new List<ProductObject>();
      for (var i = 0; i < equipmentCards.Count; i++) {
        if (equipmentCards[i].ID == EquipmentIdConstants.NO_ARMOR_ID) {
          continue;
        }

        for (var j = 0; j < armorList.Count; j++) {
          if (equipmentCards[i].ID == armorList[j].GetId()) {
            _armorListSo.Add(armorList[j]);
          }
        }
      }
    }

    private void SetWeaponListSo(List<EquipmentCard> equipmentCards, List<ProductObject> weaponList) {
      _weaponListSo = new List<ProductObject>();
      for (var i = 0; i < equipmentCards.Count; i++) {
        for (var j = 0; j < weaponList.Count; j++) {
          if (equipmentCards[i].ID == weaponList[j].GetId()) {
            _weaponListSo.Add(weaponList[j]);
          }
        }
      }
    }

    private void SetProjectilesListSo(List<EquipmentCard> equipmentCards, List<ProductObject> projectiliesList) {
      _projectilesListSo = new List<ProductObject>();
      for (var i = 0; i < equipmentCards.Count; i++) {
        for (var j = 0; j < projectiliesList.Count; j++) {
          if (equipmentCards[i].ID == projectiliesList[j].GetId()) {
            _projectilesListSo.Add(projectiliesList[j]);
          }
        }
      }
    }

    private void SetItemListSo(List<EquipmentCard> equipmentCards, List<ProductObject> itemList) {
      _itemsListSo = new List<ProductObject>();
      for (var i = 0; i < equipmentCards.Count; i++) {
        for (var j = 0; j < itemList.Count; j++) {
          if (equipmentCards[i].ID == itemList[j].GetId()) {
            _itemsListSo.Add(itemList[j]);
          }
        }
      }
    }

    private void Spawn() {
      SpawnWeapon();
      SpawnArmor();
      SpawnProjectiles();
      SpawnItems();
      SpawnCompleted?.Invoke();
    }

    private void SpawnArmor() {
      _productViewListForArmor = new List<ProductsView>();
      for (var i = 0; i < _armorListSo.Count; i++) {
        GameObject product = Instantiate(_productPrefab, _contentArmor);
        var productView = product.GetComponent<ProductsView>();
        _productViewListForArmor.Add(productView);
      }

      for (var i = 0; i < _armorListSo.Count; i++) {
        int index = i;
        InitializeProductFields(_productViewListForArmor[index], _armorListSo[index], TestNumberOfProduct);
      }
    }

    private void SpawnWeapon() {
      _productViewListForWeapon = new List<ProductsView>();
      for (var i = 0; i < _weaponListSo.Count; i++) {
        GameObject product = Instantiate(_productPrefab, _contentWeapon);
        var productView = product.GetComponent<ProductsView>();
        _productViewListForWeapon.Add(productView);
      }

      for (var i = 0; i < _weaponListSo.Count; i++) {
        int index = i;
        InitializeProductFields(_productViewListForWeapon[index], _weaponListSo[index], TestNumberOfProduct);
      }
    }

    private void SpawnProjectiles() {
      _productViewListForProjectilies = new List<ProductsView>();
      for (var i = 0; i < _projectilesListSo.Count; i++) {
        GameObject product = Instantiate(_productPrefab, _contentWeapon);
        var productView = product.GetComponent<ProductsView>();
        _productViewListForProjectilies.Add(productView);
      }

      for (var i = 0; i < _projectilesListSo.Count; i++) {
        int index = i;
        InitializeProductFields(_productViewListForProjectilies[index], _projectilesListSo[index], NumberOfProjectiles);
      }
    }

    private void SpawnItems() {
      _productViewListForItems = new List<ProductsView>();
      for (var i = 0; i < _itemsListSo.Count; i++) {
        GameObject product = Instantiate(_productPrefab, _contentItem);
        var productView = product.GetComponent<ProductsView>();
        _productViewListForItems.Add(productView);
      }

      for (var i = 0; i < _itemsListSo.Count; i++) {
        int index = i;
        InitializeProductFields(_productViewListForItems[index], _itemsListSo[index], TestNumberOfProduct);
        SetCanNotUsedForItem(_productViewListForItems[index], _itemsListSo[index]);
      }
    }

    private void SetCanNotUsedForItem(ProductsView productView, ProductObject productObject) {
      if (!_itemWhatCanUsed.Contains(productObject.GetId())) {
        productView.ProductWhatCanNotUsed();
      }
    }

    private void InitializeProductFields(ProductsView productView, ProductObject productObject, int amount) {
      SetProductName(productView, productObject);
      SetProductNumberOfProduct(productView, amount);
      SetProductId(productView, productObject);
      KitForCharacterType(productView, productObject);
    }

    private void SetProductName(ProductsView productView, ProductObject productObject) {
      productView.SetProductName(productObject.GetProductName());
    }

    private void SetProductNumberOfProduct(ProductsView productView, int numberOfProduct) {
      productView.SetNumberOfProduct(numberOfProduct.ToString());
    }

    private void SetProductId(ProductsView productView, ProductObject productObject) {
      productView.SetId(productObject.GetId());
    }

    private void KitForCharacterType(ProductsView productView, ProductObject productObject) {
      switch (productObject.GetProductType()) {
        case ProductObject.ProductType.None:
          break;
        case ProductObject.ProductType.Weapon:
          if ((productObject.GetWeaponType() & _characterInCharacterList.GetWeaponKit()) == WeaponType.None) {
            productView.ProductWhatCanNotUsed();
          }

          break;
        case ProductObject.ProductType.Armor:
          if ((productObject.GetArmorType() & _characterInCharacterList.GetArmorKit()) == ArmorType.None) {
            productView.ProductWhatCanNotUsed();
          }

          break;
        case ProductObject.ProductType.Item:
          break;
      }
    }

    public StorageObject StorageObject {
      get {
        return _storage;
      }
    }
  }
}