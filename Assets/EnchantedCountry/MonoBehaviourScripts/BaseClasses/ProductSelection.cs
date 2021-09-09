using System;
using UnityEngine;
using UnityEngine.UI;

namespace Core.EnchantedCountry.MonoBehaviourScripts.BaseClasses {
	public class ProductSelection : MonoBehaviour {
		#region FIELDS
		[SerializeField]
		protected GameObject _armorScrollView;
		[SerializeField]
		protected GameObject _weaponScrollView;
		[SerializeField]
		protected GameObject _itemScrollView;
		[SerializeField]
		protected Button _armorButton;
		[SerializeField]
		protected Button _weaponButton;
		[SerializeField]
		protected Button _itemButton;
		public static Action OpenNewListOfProducts;
		#endregion
		#region MONOBEHAVIOUR_METHODS
		protected virtual void OnEnable() {
			AddListener();
		}

		protected virtual void OnDisable() {
			RemoveListener();
		}
		#endregion
		#region HANDLERS
		protected virtual void AddListener() {
			_armorButton.onClick.AddListener(HandlerForArmorButton);
			_weaponButton.onClick.AddListener(HandlerForWeaponButton);
			_itemButton.onClick.AddListener(HandlerForItemButton);
			//SpawnProductInTrulsShop.SpawnCompleted += OnSpawnCompleted;
			//SpawnProductsInCharacterList.SpawnCompleted += OnSpawnCompleted;
		}

		protected virtual void RemoveListener() {
			_armorButton.onClick.RemoveListener(HandlerForArmorButton);
			_weaponButton.onClick.RemoveListener(HandlerForWeaponButton);
			_itemButton.onClick.RemoveListener(HandlerForItemButton);
			//SpawnProductInTrulsShop.SpawnCompleted -= OnSpawnCompleted;
			//SpawnProductsInCharacterList.SpawnCompleted -= OnSpawnCompleted;
		}

		protected virtual void OnSpawnCompleted() {
			ActivateArmorScrollView();
			DeactivateWeaponScrollView();
			DeactivateItemScrollView();
		}

		protected virtual void HandlerForArmorButton() {
			ActivateArmorScrollView();
			DeactivateWeaponScrollView();
			DeactivateItemScrollView();
			OpenNewListOfProducts?.Invoke();
			//OpenArmorListOfProducts?.Invoke();
		}

		protected virtual void HandlerForWeaponButton() {
			ActivateWeaponScrollView();
			DeactivateArmorScrollView();
			DeactivateItemScrollView();
			OpenNewListOfProducts?.Invoke();
			//OpenWeaponListOfProducts?.Invoke();
		}

		protected virtual void HandlerForItemButton() {
			ActivateItemScrollView();
			DeactivateArmorScrollView();
			DeactivateWeaponScrollView();
			OpenNewListOfProducts?.Invoke();
			//OpenItemListOfProducts?.Invoke();
		}
		#endregion
		#region TOGGLE_GAME_OBJECT
		private void ActivateArmorScrollView() {
			_armorScrollView.SetActive(true);
		}

		private void DeactivateArmorScrollView() {
			_armorScrollView.SetActive(false);

		}

		private void ActivateWeaponScrollView() {
			_weaponScrollView.SetActive(true);

		}

		private void DeactivateWeaponScrollView() {
			_weaponScrollView.SetActive(false);
		}

		private void ActivateItemScrollView() {
			_itemScrollView.SetActive(true);

		}
		private void DeactivateItemScrollView() {
			_itemScrollView.SetActive(false);
		}
		#endregion
	}
}
