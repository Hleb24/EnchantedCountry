using System;
using Core.EnchantedCountry.MonoBehaviourScripts.GameSaveSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Core.EnchantedCountry.MonoBehaviourScripts.ScriptsForScenes.TrurlsShop {
	public class OpenTrurlsShop : MonoBehaviour {
		#region FIELDS
		[SerializeField]
		private GameObject _trurlsShopCanvas;
		[SerializeField]
		private GameObject _diceRollForCoinsCanvas;
		[SerializeField]
		private Button _openTrurlsShop;

		public static event Action OpenTrurlsShopCanvas;
		#endregion
		#region MONOBEHAVIOUR_METHODS
		private void Start() {
			FirstTimeOpenTrurlsShop();
		}
		
		private void OnEnable() {
			_openTrurlsShop.onClick.AddListener(OnDiceRollButtonClicked);
		}

		private void OnDisable() {
			_openTrurlsShop.onClick.RemoveListener(OnDiceRollButtonClicked);
		}
		#endregion
		#region HANDLERS
		private void OnDiceRollButtonClicked() {
			OpenShopOfTrurl();
			OpenTrurlsShopCanvas?.Invoke();
		}
		#endregion
		#region FIRST_TIME_TRURLS_SHOP_OPENING
		private void FirstTimeOpenTrurlsShop() {
			if (GSSSingleton.Singleton.IsNewGame) {
				OpenDiceRollForCoinsCanvas();
			}else {
				OpenShopOfTrurl();
			}
		}
		#endregion
		#region OPEN_CANVAS
		private void OpenShopOfTrurl() {
			_trurlsShopCanvas.SetActive(true);
			_diceRollForCoinsCanvas.SetActive(false);
		}

		private void OpenDiceRollForCoinsCanvas() {
			_diceRollForCoinsCanvas.SetActive(true);
			_trurlsShopCanvas.SetActive(false);
		}
		#endregion
	}
}
