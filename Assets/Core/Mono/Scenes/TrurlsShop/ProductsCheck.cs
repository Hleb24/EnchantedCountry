using System.Collections.Generic;
using Core.Rule.Character.Equipment;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core.Mono.Scenes.TrurlsShop {
	public class ProductsCheck : MonoBehaviour {
		[SerializeField]
		private CashBox _cashBox;
		[FormerlySerializedAs("_spawnProductInTrulsShop"),SerializeField]
		private SpawnProduct _spawnProduct;
		private void Start() {
			SpawnProduct.SpawnCompleted += OnSpawnCompleted;
		}

		private void OnDestroy() {
			SpawnProduct.SpawnCompleted -= OnSpawnCompleted;
		}
		private void OnSpawnCompleted() {
			IfCharacterHasEquipmentSetCharacterHasTrueAndGreenBackground();
		}
		private void IfCharacterHasEquipmentSetCharacterHasTrueAndGreenBackground() {
			IfCharacterHasArmorSetChatacterHasTrueAndGreenBackground(_spawnProduct.ProductViewListForArmor
																															, _cashBox.Equipments.GetEquipmentCards());
			IfCharacterHasWeaponSetChatacterHasTrueAndGreenBackground(_spawnProduct.ProductViewListForWeapon
																															, _cashBox.Equipments.GetEquipmentCards());
			IfCharacterHasProjectiliesSetChatacterHasTrueAndGreenBackground(_spawnProduct.ProductViewListForProjectiles
																															, _cashBox.Equipments.GetEquipmentCards());
			IfCharacterHasItemSetChatacterHasTrueAndGreenBackground(_spawnProduct.ProductViewListForItems
																															, _cashBox.Equipments.GetEquipmentCards());
		}

		private void IfCharacterHasArmorSetChatacterHasTrueAndGreenBackground(List<ProductView> productsViewList, List<EquipmentCard> equipmentsCardList) {
			if (productsViewList.Equals(null)
							|| equipmentsCardList.Equals(null))
				return;
			for (int i = 0; i < productsViewList.Count; i++) {
				for (int j = 0; j < equipmentsCardList.Count; j++) {
					if (productsViewList[i].Id == equipmentsCardList[j].ID) {
						productsViewList[i].SetCharacterHasTrueAndBackgroundGreen();
					}
				}
			}
		}
		private void IfCharacterHasWeaponSetChatacterHasTrueAndGreenBackground(List<ProductView> productsViewList, List<EquipmentCard> equipmentsCardList ) {
			if (productsViewList.Equals(null)
							|| equipmentsCardList.Equals(null))
				return;
			for (int i = 0; i < productsViewList.Count; i++) {
				for (int j = 0; j < equipmentsCardList.Count; j++) {
					if (productsViewList[i].Id == equipmentsCardList[j].ID) {
						productsViewList[i].SetCharacterHasTrueAndBackgroundGreen();
					}
				}
			}
		}

		private void IfCharacterHasProjectiliesSetChatacterHasTrueAndGreenBackground(List<ProductView> productsViewList, List<EquipmentCard> equipmentsCardList ) {
			if (productsViewList.Equals(null)
							|| equipmentsCardList.Equals(null))
				return;
			for (int i = 0; i < productsViewList.Count; i++) {
				for (int j = 0; j < equipmentsCardList.Count; j++) {
					if (productsViewList[i].Id == equipmentsCardList[j].ID) {
						productsViewList[i].SetCharacterHasTrueAndBackgroundGreen();
					}
				}
			}
		}
		private void IfCharacterHasItemSetChatacterHasTrueAndGreenBackground(List<ProductView> productsViewList, List<EquipmentCard> equipmentsCardList) {
			if (productsViewList.Equals(null)
							|| equipmentsCardList.Equals(null))
				return;
			for (int i = 0; i < productsViewList.Count; i++) {
				for (int j = 0; j < equipmentsCardList.Count; j++) {
					if (productsViewList[i].Id == equipmentsCardList[j].ID) {
						productsViewList[i].SetCharacterHasTrueAndBackgroundGreen();
					}
				}
			}
		}
	}
}
