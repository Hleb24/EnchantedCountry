using System;
using Core.EnchantedCountry.MonoBehaviourScripts.BaseClasses;

namespace Core.EnchantedCountry.MonoBehaviourScripts.ScriptsForScenes.CharacterList {

	public class ProductsSelectionInCharacterList : ProductSelection {
		#region FIELDS
		public static Action OpenArmorListOfProducts;
		public static Action OpenWeaponListOfProducts;
		public static Action OpenItemListOfProducts;

		#endregion
		#region HANDLERS
		protected override void AddListener() {
			base.AddListener();
			SpawnProductsInCharacterList.SpawnCompleted += OnSpawnCompleted;
		}

		protected override void RemoveListener() {
			base.RemoveListener();
			SpawnProductsInCharacterList.SpawnCompleted -= OnSpawnCompleted;
		}

		protected override void HandlerForArmorButton() {
			base.HandlerForArmorButton();
			OpenArmorListOfProducts?.Invoke();
		}

		protected override void HandlerForWeaponButton() {
			base.HandlerForWeaponButton();
			OpenWeaponListOfProducts?.Invoke();
		}

		protected override void HandlerForItemButton() {
			base.HandlerForItemButton();
			OpenItemListOfProducts?.Invoke();
		}
		#endregion
	}
}
