using System.Collections.Generic;
using System.IO;
using Aberrance.UnityEngine.Attributes;
using Aberrance.UnityEngine.Saver;
using Core.SO.ProductObjects;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core.SO.Storage {
  [CreateAssetMenu(menuName = "Storage", fileName = "Storage", order = 57)]
  public class StorageSO : ScriptableObject {
    [FormerlySerializedAs("_productslist"), FormerlySerializedAs("productslist"), SerializeField]
    private List<ProductSO> _productsList;
    [FormerlySerializedAs("armorList"), SerializeField]
    private List<ProductSO> _armorList;
    [FormerlySerializedAs("weaponList"), SerializeField]
    private List<ProductSO> _weaponList;
    [FormerlySerializedAs("_projectliesList"), FormerlySerializedAs("_projectiliesList"), FormerlySerializedAs("projectiliesList"), SerializeField]
    private List<ProductSO> _projectilesList;
    [FormerlySerializedAs("itemList"), SerializeField]
    private List<ProductSO> _itemList;

    [NotNull]
    public List<ProductSO> GetArmors() {
      return _armorList;
    }

    [NotNull]
    public List<ProductSO> GetWeapons() {
      return _weaponList;
    }

    [NotNull]
    public List<ProductSO> GetProjectiles() {
      return _projectilesList;
    }

    [NotNull]
    public List<ProductSO> GetItems() {
      return _itemList;
    }

    [NotNull]
    public List<ProductSO> GetProducts() {
      return _productsList;
    }

    [CanBeNull]
    public ProductSO GetProductFromList(int id) {
      for (var i = 0; i < _productsList.Count; i++) {
        if (_productsList[i].GetId() == id) {
          return _productsList[i];
        }
      }

      Debug.LogError($"Товар с id {id} не найден!");
      return null;
    }

    [CanBeNull]
    public ProductSO GetArmorFromList(int id) {
      for (var i = 0; i < _armorList.Count; i++) {
        if (_armorList[i].GetId() == id) {
          return _armorList[i];
        }
      }

      Debug.LogError($"Товар с id {id} не найден!");
      return null;
    }

    [CanBeNull]
    public ProductSO GetWeaponFromList(int id) {
      for (var i = 0; i < _weaponList.Count; i++) {
        if (_weaponList[i].GetId() == id) {
          return _weaponList[i];
        }
      }

      Debug.LogError($"Товар с id {id} не найден!");
      return null;
    }

    [CanBeNull]
    public ProductSO GetProjectilesFromList(int id) {
      for (var i = 0; i < _projectilesList.Count; i++) {
        if (_projectilesList[i].GetId() == id) {
          return _projectilesList[i];
        }
      }

      Debug.LogError($"Товар с id {id} не найден!");
      return null;
    }

    [CanBeNull]
    public ProductSO GetItemFromList(int id) {
      for (var i = 0; i < _itemList.Count; i++) {
        if (_itemList[i].GetId() == id) {
          return _itemList[i];
        }
      }

      Debug.LogError($"Товар с id {id} не найден!");
      return null;
    }

    #region SAVE_TO_JSON
    [Button]
    public void SaveArmorToJson() {
      var saver = new JsonSaver();
      string pathToFolder = Path.Combine(Application.persistentDataPath, "Storage/Armor");
      for (var i = 0; i < _armorList.Count; i++) {
        string pathToFile = Path.Combine(pathToFolder, $"{_armorList[i].GetProductName()}.json");
        saver.Save(_armorList[i], pathToFolder, pathToFile);
      }
    }

    [Button]
    public void SaveWeaponToJson() {
      var saver = new JsonSaver();
      string pathToFolder = Path.Combine(Application.persistentDataPath, "Storage/Weapon");
      for (var i = 0; i < _weaponList.Count; i++) {
        string pathToFile = Path.Combine(pathToFolder, $"{_weaponList[i].GetProductName()}.json");
        saver.Save(_weaponList[i], pathToFolder, pathToFile);
      }
    }

    [Button]
    public void SaveRangeToJson() {
      var saver = new JsonSaver();
      string pathToFolder = Path.Combine(Application.persistentDataPath, "Storage/Projectlies");
      for (var i = 0; i < _projectilesList.Count; i++) {
        string pathToFile = Path.Combine(pathToFolder, $"{_projectilesList[i].GetProductName()}.json");
        saver.Save(_projectilesList[i], pathToFolder, pathToFile);
      }
    }

    [Button]
    public void SaveItemToJson() {
      var saver = new JsonSaver();
      string pathToFolder = Path.Combine(Application.persistentDataPath, "Storage/Item");
      for (var i = 0; i < _itemList.Count; i++) {
        string pathToFile = Path.Combine(pathToFolder, $"{_itemList[i].GetProductName()}.json");
        saver.Save(_itemList[i], pathToFolder, pathToFile);
      }
    }
    #endregion
  }
}