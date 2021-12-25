using Core.Mono.BaseClass;

namespace Core.Mono.Scenes.TrurlsShop {

	public class WalletInTrurlsShop : WalletIn {
		protected override void OnEnable() {
			base.OnEnable();
			CashBox.BuyProductSuccess += OnBuyProductSuccess;
			OpenTrurlsShop.OpenTrurlsShopCanvas += OnOpenTrurlsShopCanvas;
		}

		protected override void OnDisable() {
			CashBox.BuyProductSuccess -= OnBuyProductSuccess;
			OpenTrurlsShop.OpenTrurlsShopCanvas -= OnOpenTrurlsShopCanvas;
		}
		private void OnBuyProductSuccess() {
			SetWalletText();
		}

		private void OnOpenTrurlsShopCanvas() {
			InitializeWallet();
			Invoke(nameof(SetWalletText), 0.1f);
		}
	}
}
