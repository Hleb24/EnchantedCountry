using System;
using System.Collections.Generic;
using Core.EnchantedCountry.CoreEnchantedCountry.Character.Equipment;
using Core.EnchantedCountry.CoreEnchantedCountry.GameRule.EquipmentIdConstants;
using Core.EnchantedCountry.ScriptableObject.EquipmentSO;
using Core.EnchantedCountry.ScriptableObject.ProductObject;
using Core.EnchantedCountry.ScriptableObject.Storage;
using Core.EnchantedCountry.SupportSystems.Data;
using UnityEngine;
using UnityEngine.Serialization;
using static Core.EnchantedCountry.CoreEnchantedCountry.GameRule.Armor.Armor;
using static Core.EnchantedCountry.CoreEnchantedCountry.GameRule.Weapon.Weapon;

namespace Core.EnchantedCountry.MonoBehaviourScripts.ScriptsForScenes.CharacterList {
  public class SpawnProductsInCharacterList : MonoBehaviour {
    #region FIELDS
    private const int TEST_NUMBER_OF_PRODUCT = 5;
    private const int NUMBER_OF_PROJECTILIES = 20;
    [SerializeField]
    private CharacterInCharacterList _characterInCharacterList;
    [SerializeField]
    private GameObject _productPrefab;
    [SerializeField]
    // ReSharper disable once NotAccessedField.Local
    private EquipmentSO _equipment;
    [SerializeField]
    private StorageSO _storage;
    [SerializeField]
    private List<ProductsInCharacterListView> _productViewListForArmor;
    [SerializeField]
    private List<ProductsInCharacterListView> _productViewListForWeapon;
    [SerializeField]
    private List<ProductsInCharacterListView> _productViewListForProjectilies;
    [SerializeField]
    private List<ProductsInCharacterListView> _productViewListForItems;
    [SerializeField]
    private List<ProductSO> _armorListSo;
    [SerializeField]
    private List<ProductSO> _weaponListSo;
    [FormerlySerializedAs("_projectiliesListSo"), SerializeField]
    private List<ProductSO> _projectilesListSo;
    [SerializeField]
    private List<ProductSO> _itemsListSo;
    [SerializeField]
    private Transform _contentArmor;
    [SerializeField]
    private Transform _contentWeapon;
    [SerializeField]
    private Transform _contentItem;
    [SerializeField]
    private List<int> _itemWhatCanUsed;
    private IEquipment _equipments;

    public static event Action SpawnCompleted;
    #endregion

    #region GET_PRODUCT_LIST
    public List<ProductSO> GetArmorList() {
      return _armorListSo;
    }

    public List<ProductSO> GetWeaponList() {
      return _weaponListSo;
    }

    public List<ProductSO> GetProjectiliesList() {
      return _projectilesListSo;
    }

    public List<ProductSO> GetItemList() {
      return _itemsListSo;
    }
    #endregion

    #region MONOBEHAVIOUR_METHODS
    private void Awake() {
      _characterInCharacterList.GetCharacterType += SetListsSoAndSpawnProducts;
      SetItemWhatCanUsedList();
    }

    private void OnDestroy() {
      _characterInCharacterList.GetCharacterType -= SetListsSoAndSpawnProducts;
    }

    private void Start() {
      _equipments = ScribeDealer.Peek<EquipmentsScribe>();
    }
    #endregion

    #region SET_LISTS
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

    private void SetArmorListSo(List<EquipmentCard> equipmentCards, List<ProductSO> armorList) {
      _armorListSo = new List<ProductSO>();
      for (var i = 0; i < equipmentCards.Count; i++) {
        if (equipmentCards[i].ID == EquipmentIdConstants.NoArmorId) {
          continue;
        }

        for (var j = 0; j < armorList.Count; j++) {
          if (equipmentCards[i].ID == armorList[j].id) {
            _armorListSo.Add(armorList[j]);
          }
        }
      }
    }

    private void SetWeaponListSo(List<EquipmentCard> equipmentCards, List<ProductSO> weaponList) {
      _weaponListSo = new List<ProductSO>();
      for (var i = 0; i < equipmentCards.Count; i++) {
        for (var j = 0; j < weaponList.Count; j++) {
          if (equipmentCards[i].ID == weaponList[j].id) {
            _weaponListSo.Add(weaponList[j]);
          }
        }
      }
    }

    private void SetProjectilesListSo(List<EquipmentCard> equipmentCards, List<ProductSO> projectiliesList) {
      _projectilesListSo = new List<ProductSO>();
      for (var i = 0; i < equipmentCards.Count; i++) {
        for (var j = 0; j < projectiliesList.Count; j++) {
          if (equipmentCards[i].ID == projectiliesList[j].id) {
            _projectilesListSo.Add(projectiliesList[j]);
          }
        }
      }
    }

    private void SetItemListSo(List<EquipmentCard> equipmentCards, List<ProductSO> itemList) {
      _itemsListSo = new List<ProductSO>();
      for (var i = 0; i < equipmentCards.Count; i++) {
        for (var j = 0; j < itemList.Count; j++) {
          if (equipmentCards[i].ID == itemList[j].id) {
            _itemsListSo.Add(itemList[j]);
          }
        }
      }
    }
    #endregion

    #region SPAWN_PRODUCT
    private void Spawn() {
      SpawnWeapon();
      SpawnArmor();
      SpawnProjectilies();
      SpawnItems();
      SpawnCompleted?.Invoke();
    }

    private void SpawnArmor() {
      _productViewListForArmor = new List<ProductsInCharacterListView>();
      for (var i = 0; i < _armorListSo.Count; i++) {
        GameObject product = Instantiate(_productPrefab, _contentArmor);
        var productView = product.GetComponent<ProductsInCharacterListView>();
        _productViewListForArmor.Add(productView);
      }

      for (var i = 0; i < _armorListSo.Count; i++) {
        int index = i;
        InitializeProductFields(_productViewListForArmor[index], _armorListSo[index], TEST_NUMBER_OF_PRODUCT);
      }
    }

    private void SpawnWeapon() {
      _productViewListForWeapon = new List<ProductsInCharacterListView>();
      for (var i = 0; i < _weaponListSo.Count; i++) {
        GameObject product = Instantiate(_productPrefab, _contentWeapon);
        var productView = product.GetComponent<ProductsInCharacterListView>();
        _productViewListForWeapon.Add(productView);
      }

      for (var i = 0; i < _weaponListSo.Count; i++) {
        int index = i;
        InitializeProductFields(_productViewListForWeapon[index], _weaponListSo[index], TEST_NUMBER_OF_PRODUCT);
      }
    }

    private void SpawnProjectilies() {
      _productViewListForProjectilies = new List<ProductsInCharacterListView>();
      for (var i = 0; i < _projectilesListSo.Count; i++) {
        GameObject product = Instantiate(_productPrefab, _contentWeapon);
        var productView = product.GetComponent<ProductsInCharacterListView>();
        _productViewListForProjectilies.Add(productView);
      }

      for (var i = 0; i < _projectilesListSo.Count; i++) {
        int index = i;
        InitializeProductFields(_productViewListForProjectilies[index], _projectilesListSo[index], NUMBER_OF_PROJECTILIES);
      }
    }

    private void SpawnItems() {
      _productViewListForItems = new List<ProductsInCharacterListView>();
      for (var i = 0; i < _itemsListSo.Count; i++) {
        GameObject product = Instantiate(_productPrefab, _contentItem);
        var productView = product.GetComponent<ProductsInCharacterListView>();
        _productViewListForItems.Add(productView);
      }

      for (var i = 0; i < _itemsListSo.Count; i++) {
        int index = i;
        InitializeProductFields(_productViewListForItems[index], _itemsListSo[index], TEST_NUMBER_OF_PRODUCT);
        SetCanNotUsedForItem(_productViewListForItems[index], _itemsListSo[index]);
      }
    }

    private void SetCanNotUsedForItem(ProductsInCharacterListView productView, ProductSO productSo) {
      if (!_itemWhatCanUsed.Contains(productSo.id)) {
        productView.ProductWhatCanNotUsed();
      }
    }
    #endregion

    #region INITIALIZE_PRODUCT_FIELDS
    private void InitializeProductFields(ProductsInCharacterListView productView, ProductSO productSo, int amount) {
      SetProductName(productView, productSo);
      SetProductNumberOfProduct(productView, amount);
      SetProductId(productView, productSo);
      KitForCharacterType(productView, productSo);
    }

    private void SetProductName(ProductsInCharacterListView productView, ProductSO productSo) {
      productView.Name.text = productSo.productName;
    }

    private void SetProductNumberOfProduct(ProductsInCharacterListView productView, int numberOfProduct) {
      productView.NumberOfProduct.text = numberOfProduct.ToString();
    }

    private void SetProductId(ProductsInCharacterListView productView, ProductSO productSo) {
      productView.Id = productSo.id;
    }

    private void KitForCharacterType(ProductsInCharacterListView productView, ProductSO productSo) {
      switch (productSo.productType) {
        case ProductSO.ProductType.None:
          break;
        case ProductSO.ProductType.Weapon:
          if ((productSo.GetWeaponType() & _characterInCharacterList.GetWeaponKit()) == WeaponType.None) {
            productView.ProductWhatCanNotUsed();
          }

          break;
        case ProductSO.ProductType.Armor:
          if ((productSo.GetArmorType() & _characterInCharacterList.GetArmorKit()) == ArmorType.None) {
            productView.ProductWhatCanNotUsed();
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
    public List<ProductsInCharacterListView> ProductViewListForArmor {
      get {
        return _productViewListForArmor;
      }
      set {
        _productViewListForArmor = value;
      }
    }

    public List<ProductsInCharacterListView> ProductViewListForWeapon {
      get {
        return _productViewListForWeapon;
      }
      set {
        _productViewListForWeapon = value;
      }
    }

    public List<ProductsInCharacterListView> ProductViewListForProjectilies {
      get {
        return _productViewListForProjectilies;
      }
      set {
        _productViewListForProjectilies = value;
      }
    }

    public List<ProductsInCharacterListView> ProductViewListForItems {
      get {
        return _productViewListForItems;
      }
      set {
        _productViewListForItems = value;
      }
    }

    public StorageSO StorageSo {
      get {
        return _storage;
      }
    }
    #endregion
  }
}