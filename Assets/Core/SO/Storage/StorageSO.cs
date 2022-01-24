using System.Collections.Generic;
using Core.SO.Product;
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
	}
}
