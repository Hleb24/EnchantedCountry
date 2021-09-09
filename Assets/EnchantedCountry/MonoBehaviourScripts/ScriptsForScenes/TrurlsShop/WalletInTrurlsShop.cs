using Core.EnchantedCountry.MonoBehaviourScripts.BaseClasses;

namespace Core.EnchantedCountry.MonoBehaviourScripts.ScriptsForScenes.TrurlsShop {

	public class WalletInTrurlsShop : WalletIn {
		#region MONOBEHAVIOUR_METHODS
		protected override void OnEnable() {
			base.OnEnable();
			CashBox.BuyProductSuccess += OnBuyProductSuccess;
			OpenTrurlsShop.OpenTrurlsShopCanvas += OnOpenTrurlsShopCanvas;
		}

		protected override void OnDisable() {
			CashBox.BuyProductSuccess -= OnBuyProductSuccess;
			OpenTrurlsShop.OpenTrurlsShopCanvas -= OnOpenTrurlsShopCanvas;
		}
		#endregion
		#region HANDLERS
		private void OnBuyProductSuccess() {
			SetWalletText();
		}

		private void OnOpenTrurlsShopCanvas() {
			InitializeWallet();
			Invoke(nameof(SetWalletText), 0.1f);
		}
		#endregion
	}
}
