using System.Collections.Generic;
using Core.EnchantedCountry.ScriptableObject.ProductObject;
using UnityEngine;

namespace Core.EnchantedCountry.ScriptableObject.Storage {
	[CreateAssetMenu(menuName = "Storage", fileName = "Storage", order = 57)]
	public class StorageSO : UnityEngine.ScriptableObject {
		public List<ProductSO> productslist;
		public List<ProductSO> armorList;
		public List<ProductSO> weaponList;
		public List<ProductSO> projectiliesList;
		public List<ProductSO> itemList;
		public ProductSO GetProductFromList(int id) {
			for (int i = 0; i < productslist.Count; i++) {
				if (productslist[i].id == id)
					return productslist[i];
			}
			Debug.LogError("ProductSO not found!");
			return null;
		}
		public ProductSO GetArmorFromList(int id) {
			for (int i = 0; i < armorList.Count; i++) {
				if (armorList[i].id == id)
					return armorList[i];
			}
			Debug.LogError("ProductSO not found!");
			return null;
		}
		public  ProductSO GetWeaponFromList(int id) {
			for (int i = 0; i < weaponList.Count; i++) {
				if (weaponList[i].id == id)
					return weaponList[i];
			}
			Debug.LogError("ProductSO not found!");
			return null;
		}
		public ProductSO GetProjectiliesFromList(int id) {
			for (int i = 0; i < projectiliesList.Count; i++) {
				if (projectiliesList[i].id == id)
					return projectiliesList[i];
			}
			Debug.LogError("ProductSO not found!");
			return null;
		}
		public ProductSO GetItemFromList(int id) {
			for (int i = 0; i < itemList.Count; i++) {
				if (itemList[i].id == id)
					return itemList[i];
			}
			Debug.LogError("ProductSO not found!");
			return null;
		}
	}
}
