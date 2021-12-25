using System.Collections.Generic;
using Core.ScriptableObject.Products;
using UnityEngine;

namespace Core.ScriptableObject.Storage {
	[CreateAssetMenu(menuName = "Storage", fileName = "Storage", order = 57)]
	public class StorageObject : UnityEngine.ScriptableObject {
		public List<ProductObject> productslist;
		public List<ProductObject> armorList;
		public List<ProductObject> weaponList;
		public List<ProductObject> projectiliesList;
		public List<ProductObject> itemList;
		public ProductObject GetProductFromList(int id) {
			for (int i = 0; i < productslist.Count; i++) {
				if (productslist[i].id == id)
					return productslist[i];
			}
			Debug.LogError("ProductSO not found!");
			return null;
		}
		public ProductObject GetArmorFromList(int id) {
			for (int i = 0; i < armorList.Count; i++) {
				if (armorList[i].id == id)
					return armorList[i];
			}
			Debug.LogError("ProductSO not found!");
			return null;
		}
		public  ProductObject GetWeaponFromList(int id) {
			for (int i = 0; i < weaponList.Count; i++) {
				if (weaponList[i].id == id)
					return weaponList[i];
			}
			Debug.LogError("ProductSO not found!");
			return null;
		}
		public ProductObject GetProjectiliesFromList(int id) {
			for (int i = 0; i < projectiliesList.Count; i++) {
				if (projectiliesList[i].id == id)
					return projectiliesList[i];
			}
			Debug.LogError("ProductSO not found!");
			return null;
		}
		public ProductObject GetItemFromList(int id) {
			for (int i = 0; i < itemList.Count; i++) {
				if (itemList[i].id == id)
					return itemList[i];
			}
			Debug.LogError("ProductSO not found!");
			return null;
		}
	}
}
