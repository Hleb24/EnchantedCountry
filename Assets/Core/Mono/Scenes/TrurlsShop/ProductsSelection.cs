using Core.Mono.BaseClass;

namespace Core.Mono.Scenes.TrurlsShop {

public class ProductsSelection : ProductSelection {
		#region HANDLERS
		protected override void AddListener() {
			base.AddListener();
			SpawnProduct.SpawnCompleted += OnSpawnCompleted;
		}

		protected override void RemoveListener() {
			base.RemoveListener();
			SpawnProduct.SpawnCompleted -= OnSpawnCompleted;
		}
		#endregion
	}
}
