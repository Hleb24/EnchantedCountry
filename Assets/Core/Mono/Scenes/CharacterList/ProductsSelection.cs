using System;
using Core.Mono.BaseClass;

namespace Core.Mono.Scenes.CharacterList {

	public class ProductsSelection : ProductSelection {
		#region FIELDS
		public static Action OpenArmorListOfProducts;
		public static Action OpenWeaponListOfProducts;
		public static Action OpenItemListOfProducts;

		#endregion
		#region HANDLERS
		protected override void AddListener() {
			base.AddListener();
			SpawnProducts.SpawnCompleted += OnSpawnCompleted;
		}

		protected override void RemoveListener() {
			base.RemoveListener();
			SpawnProducts.SpawnCompleted -= OnSpawnCompleted;
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