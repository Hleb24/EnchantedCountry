using Core.EnchantedCountry.MonoBehaviourScripts.BaseClasses;

namespace Core.EnchantedCountry.MonoBehaviourScripts.ScriptsForScenes.TrurlsShop {

public class ProductsSelectionInTrurlsShop : ProductSelection {
		#region HANDLERS
		protected override void AddListener() {
			base.AddListener();
			SpawnProductInTrulsShop.SpawnCompleted += OnSpawnCompleted;
		}

		protected override void RemoveListener() {
			base.RemoveListener();
			SpawnProductInTrulsShop.SpawnCompleted -= OnSpawnCompleted;
		}
		#endregion
	}
}
