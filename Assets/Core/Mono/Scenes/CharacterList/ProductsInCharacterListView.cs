using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Core.Mono.Scenes.CharacterList {
	public class ProductsInCharacterListView : MonoBehaviour, IPointerClickHandler {
		[SerializeField]
		private Image _background;
		[SerializeField]
		private TMP_Text _name;
		[SerializeField]
		private TMP_Text _numberOfProduct;
		private int _id;
		private bool _productWhatCanNotUsed;
		private bool _thisProductSelected;
		private bool _thisProductUsed;
		private static List<int> _thisProductUnusedList = new List<int>();
		public static event Action<ProductsInCharacterListView> PointerClicked;
		public static event Action<int> ProductSelected;
		public void ProductWhatCanNotUsed() {
			SetGreyColorForBackground();
			DisableRaycastTargetForBackground();
			_productWhatCanNotUsed = true;
		}
		
		private void OnEnable() {
			CheckUnusedList();
			if (_thisProductUsed) {
				SetGreenColorForBackground();
			}
			ProductsInCharacterListView.PointerClicked += OnPointerClicked;
			ApplyAndTakeOffEquipment.ApplyButtonClicked += OnApplyButtonClicked;
			ApplyAndTakeOffEquipment.TakeOffEquipment += OnTakeOffEquipment;
		}

		private void OnDisable() {
			ProductsInCharacterListView.PointerClicked -= OnPointerClicked;
			ApplyAndTakeOffEquipment.ApplyButtonClicked -= OnApplyButtonClicked;
			ApplyAndTakeOffEquipment.TakeOffEquipment -= OnTakeOffEquipment;
			SetWhiteColorForBackgroundOnDisable();
			SetFalseForThisProductSelected();
		}


		private void SetWhiteColorForBackgroundOnDisable() {
			if (!_productWhatCanNotUsed && !_thisProductUsed) {
				SetWhiteColorForBackground();
			}
		}
		private void OnPointerClicked(ProductsInCharacterListView productView) {
			if (productView != this) {
				_thisProductSelected = false;
				if (_productWhatCanNotUsed)
					return;
				if (_thisProductUsed) {
					SetGreenColorForBackground();
					return;
				}
				SetWhiteColorForBackground();
			}
		}

		public void OnPointerClick(PointerEventData eventData) {
			_thisProductSelected = true;
			SetColorForBackground();
			PointerClicked?.Invoke(this);
			if (_productWhatCanNotUsed) {
				ProductSelected?.Invoke(0);
				return;
			}
			ProductSelected?.Invoke(_id);
		}

		public void OnApplyButtonClicked(int id) {
			if (ThisIsThisId(id)) {
				_thisProductUsed = true;
				SetGreenColorForBackground();
			}
		}

		private void OnTakeOffEquipment(int id) {
			if (ThisIsThisId(id)) {
				_thisProductUsed = false;
				SetWhiteColorForBackground();
			} else {
				_thisProductUnusedList.Add(id);
			}
		}

		private bool ThisIsThisId(int id) {
			return _id == id;
		}
		private void SetFalseForThisProductSelected() {
			_thisProductSelected = false;
		}
		private void CheckUnusedList() {
			if(_thisProductUnusedList.Count.Equals(0))
				return;
			for (int i = 0; i < _thisProductUnusedList.Count; i++) {
				if (ThisIsThisId(_thisProductUnusedList[i])) {
					_thisProductUsed = false;
					SetWhiteColorForBackground();
					_thisProductUnusedList.RemoveAt(i);
				}
			}
		}
		
		private void DisableRaycastTargetForBackground() {
			_background.raycastTarget = false;
		}
		// ReSharper disable once UnusedMember.Local
		private void EnableRaycastTargetForBackground() {
			_background.raycastTarget = true;
		}
		private void SetWhiteColorForBackground() {
			_background.color = Color.white;
		}

		private void SetGreenColorForBackground() {
			_background.color = Color.green;
		}
		private void SetCyanColorForBackground() {
			_background.color = Color.cyan;
		}

		private void SetGreyColorForBackground() {
			_background.color = Color.grey;
		}

		private void SetColorForBackground() {
			if (_productWhatCanNotUsed)
				return;
			if (_thisProductUsed) {
				SetCyanColorForBackground();
				return;
			}
			SetOrangeColorForBackground();
		}

		private void SetOrangeColorForBackground() {
			_background.color = new Color(0.9245283f, 0.5744624f, 0.3523674f, 1f);
		}

		// ReSharper disable once UnusedMember.Local
		private static bool IsNotEnoughCoinForBuyProduct(int coinsInWallet, int price) {
			return coinsInWallet < price;
		}
		public TMP_Text Name {
			get {
				return _name;
			}
			set {
				_name = value;
			}
		}

		public TMP_Text NumberOfProduct {
			get {
				return _numberOfProduct;
			}
			set {
				_numberOfProduct = value;
			}
		}

		public int Id {
			get {
				return _id;
			}
			set {
				_id = value;
			}
		}

		public bool ThisProductSelected {
			get {
				return _thisProductSelected;
			}
			set {
				_thisProductSelected = value;
			}
		}
	}
}

