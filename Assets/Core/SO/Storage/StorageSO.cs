using System.Collections.Generic;
using System.IO;
using Core.SO.Product;
using Core.Support.Attributes;
using Core.Support.SaveSystem.Saver;
using UnityEngine;

namespace Core.SO.Storage {
	[CreateAssetMenu(menuName = "Storage", fileName = "Storage", order = 57)]
	public class StorageSO : UnityEngine.ScriptableObject {
		public List<ProductSO> productslist;
		public List<ProductSO> armorList;
		public List<ProductSO> weaponList;
		public List<ProductSO> projectiliesList;
		public List<ProductSO> itemList;
		public ProductSO GetProductFromList(int id) {
			for (int i = 0; i < productslist.Count; i++) {
				if (productslist[i].GetId() == id)
					return productslist[i];
			}
			Debug.LogError("ProductSO not found!");
			return null;
		}
		public ProductSO GetArmorFromList(int id) {
			for (int i = 0; i < armorList.Count; i++) {
				if (armorList[i].GetId() == id)
					return armorList[i];
			}
			Debug.LogError("ProductSO not found!");
			return null;
		}
		public  ProductSO GetWeaponFromList(int id) {
			for (int i = 0; i < weaponList.Count; i++) {
				if (weaponList[i].GetId() == id)
					return weaponList[i];
			}
			Debug.LogError("ProductSO not found!");
			return null;
		}
		public ProductSO GetProjectilesFromList(int id) {
			for (int i = 0; i < projectiliesList.Count; i++) {
				if (projectiliesList[i].GetId() == id)
					return projectiliesList[i];
			}
			Debug.LogError("ProductSO not found!");
			return null;
		}
		public ProductSO GetItemFromList(int id) {
			for (int i = 0; i < itemList.Count; i++) {
				if (itemList[i].GetId() == id)
					return itemList[i];
			}
			Debug.LogError("ProductSO not found!");
			return null;
		}

		
		[Button]
		public void SaveArmorToJson() {
			var saver = new JsonSaver();
			string _pathToFolder = Path.Combine(Application.persistentDataPath, "Storage/Armor");
			string _pathToFile;
			for (var i = 0; i < armorList.Count; i++) {
				_pathToFile = Path.Combine(_pathToFolder, $"{armorList[i]._productName}.json");
				saver.Save(armorList[i], _pathToFolder, _pathToFile);
			}
		}
		
		[Button]
		public void SaveWeaponToJson() {
			var saver = new JsonSaver();
			string _pathToFolder = Path.Combine(Application.persistentDataPath, "Storage/Weapon");
			string _pathToFile = Path.Combine(Path.Combine(Application.persistentDataPath, "NpcWeapon"), "Save.json");
			for (var i = 0; i < weaponList.Count; i++) {
				_pathToFile = Path.Combine(_pathToFolder, $"{weaponList[i]._productName}.json");
				saver.Save(weaponList[i], _pathToFolder, _pathToFile);
			}
		}
		
		[Button]
		public void SaveRangeToJson() {
			var saver = new JsonSaver();
			string _pathToFolder = Path.Combine(Application.persistentDataPath, "Storage/Projectlies");
			string _pathToFile = Path.Combine(Path.Combine(Application.persistentDataPath, "NpcWeapon"), "Save.json");
			for (var i = 0; i < projectiliesList.Count; i++) {
				_pathToFile = Path.Combine(_pathToFolder, $"{projectiliesList[i]._productName}.json");
				saver.Save(projectiliesList[i], _pathToFolder, _pathToFile);
			}
		}
		
		[Button]
		public void SaveItemToJson() {
			var saver = new JsonSaver();
			string _pathToFolder = Path.Combine(Application.persistentDataPath, "Storage/Item");
			string _pathToFile = Path.Combine(Path.Combine(Application.persistentDataPath, "NpcWeapon"), "Save.json");
			for (var i = 0; i < itemList.Count; i++) {
				_pathToFile = Path.Combine(_pathToFolder, $"{itemList[i]._productName}.json");
				saver.Save(itemList[i], _pathToFolder, _pathToFile);
			}
		}
	}
}
