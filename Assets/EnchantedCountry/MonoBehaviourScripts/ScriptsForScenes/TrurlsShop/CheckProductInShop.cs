using System.Collections.Generic;
using Core.EnchantedCountry.CoreEnchantedCountry.Character.Equipment;
using UnityEngine;

namespace Core.EnchantedCountry.MonoBehaviourScripts.ScriptsForScenes.TrurlsShop {
	public class CheckProductInShop : MonoBehaviour {
		#region FIELDS
		[SerializeField]
		private CashBox _cashBox;
		[SerializeField]
		private SpawnProductInTrulsShop _spawnProductInTrulsShop;
		#endregion
		#region MONOBEHAVIOUR_METHODS
		private void Start() {
			SpawnProductInTrulsShop.SpawnCompleted += OnSpawnCompleted;
		}

		private void OnDestroy() {
			SpawnProductInTrulsShop.SpawnCompleted -= OnSpawnCompleted;
		}
		#region HANDLERS
		private void OnSpawnCompleted() {
			IfCharacterHasEquipmentSetCharacterHasTrueAndGreenBackground();
		}
		#endregion
		#endregion
		#region IF_CHARACTER_HAS_EQUIPMENT
		private void IfCharacterHasEquipmentSetCharacterHasTrueAndGreenBackground() {
			IfCharacterHasArmorSetChatacterHasTrueAndGreenBackground(_spawnProductInTrulsShop.ProductViewListForArmor
																															, _cashBox.EquipmentsOfCharacterDataHandler.EquipmentsOfCharacter.EquipmentCards);
			IfCharacterHasWeaponSetChatacterHasTrueAndGreenBackground(_spawnProductInTrulsShop.ProductViewListForWeapon
																															, _cashBox.EquipmentsOfCharacterDataHandler.EquipmentsOfCharacter.EquipmentCards);
			IfCharacterHasProjectiliesSetChatacterHasTrueAndGreenBackground(_spawnProductInTrulsShop.ProductViewListForProjectiles
																															, _cashBox.EquipmentsOfCharacterDataHandler.EquipmentsOfCharacter.EquipmentCards);
			IfCharacterHasItemSetChatacterHasTrueAndGreenBackground(_spawnProductInTrulsShop.ProductViewListForItems
																															, _cashBox.EquipmentsOfCharacterDataHandler.EquipmentsOfCharacter.EquipmentCards);
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
		#endregion
	}
}
