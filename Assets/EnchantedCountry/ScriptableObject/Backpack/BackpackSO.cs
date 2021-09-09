using Core.EnchantedCountry.ScriptableObject.Storage;
using UnityEngine;

namespace Core.EnchantedCountry.ScriptableObject.Backpack {
	[CreateAssetMenu(fileName = "Backpack", menuName = "Backpack", order = 57)]
	public class Backpack : UnityEngine.ScriptableObject {
		#region FIELDS
		public StorageSO storage;
		public bool isInitiated;
		#endregion
		#region MONOBEHAVIOUR_METHODS
		private void OnValidate() {
			Init();
		}

		private void OnEnable() {
			Init();
		}
		#endregion
		#region INIT
		private void Init() {
		}
		#endregion
		//#region GET_NUMBER_OF_PRODUCT
		//public int GetNumberOfProductInBackPack(int id) {
		//	return _productInBackPack[id];
		//}
		//#endregion
		//#region BOOLEAN
		//public bool ProductIdExists(int id) {
		//	return _productInBackPack.ContainsKey(id);
		//}

		//public bool NumberOfProductInBackpackAtMostZero(int id) {
		//	int numberOfProductInBackpack = GetNumberOfProductInBackPack(id);
		//	return numberOfProductInBackpack > 0;
		//}

		//public bool NumberOfProductInBackpackEqualZero(int id) {
		//	int numberOfProductInBackpack = GetNumberOfProductInBackPack(id);
		//	return numberOfProductInBackpack == 0;
		//}

		//public bool NumberOfProductMoreThan(int id, int amount) {
		//	return _productInBackPack[id] >= amount;
		//}
		//#endregion
		//#region INCREASE_AND_DEACREASE_NUMBER_OF_PRODUCT
		//public void IncreaseNumberOfProduct(int id, int amount) {
		//	int totalAmount = GetNumberOfProductInBackPack(id) + amount;
		//	SetNewNumberOfProduct(id, totalAmount);

		//}
		//public void DecreaseNumberOFProudct(int id, int amount) {
		//	if (NumberOfProductMoreThan(id, amount)){
		//		int totalAmount = GetNumberOfProductInBackPack(id) - amount;
		//		SetNewNumberOfProduct(id, totalAmount);
		//	}
		//}
		//public void SetNewNumberOfProduct(int id, int newNumberOfProduct) {
		//	_productInBackPack[id] = newNumberOfProduct;
		//}
		//#endregion
		//#region GET_PRODUCT_WITH_BACKPACK
		//public ProductSO GetProductWithBackpack(int id) {
		//	if (NumberOfProductInBackpackAtMostZero(id)) {
		//		for (int i = 0; i < storage.Count; i++) {
		//			if (storage[i].id == id)
		//				return storage[i];
		//		}
		//	}
		//	Debug.LogError("Get product with backpack returned null!");
		//	return null;
		//}
		//#endregion
		//#region ADD_PRODUCT_IN_BACKPACK
		//public void AddProductInBackpack(int id) {
		//	_productInBackPack[id] = _productInBackPack[id] + 1;
		//	Debug.LogError($"ProductInBackpack: Id {id}, Amount {_productInBackPack[id]}");
		//}

		//[ContextMenu("Check_product_in_backpack_from_console")]
		//public void CheckProductInBackpackFromConsole() {
		//	foreach (KeyValuePair<int, int> product in _productInBackPack) {
		//		Debug.LogError($"ProductInBackpack: Id {product.Key}, Amount {_productInBackPack.Values}");
		//	}
		//}
		//#endregion
	}
}
