using Core.SO.Storage;
using UnityEngine;

namespace Core.SO.Backpack {
	[CreateAssetMenu(fileName = "Backpack", menuName = "Backpack", order = 57)]
	public class BackpackSO : UnityEngine.ScriptableObject {
		public StorageSO storage;
		public bool isInitiated;
		private void OnValidate() {
			Init();
		}

		private void OnEnable() {
			Init();
		}
		private void Init() {
		}
		//public int GetNumberOfProductInBackPack(int id) {
		//	return _productInBackPack[id];
		//}
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
	}
}
