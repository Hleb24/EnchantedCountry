using System;
using Core.SupportSystems.PrefsTools;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Core.Mono.Scenes.TrurlsShop {

	public class ProductView : MonoBehaviour, IPointerClickHandler {
		[SerializeField]
		private Image _icon;
		[SerializeField]
		private TMP_Text _name;
		[SerializeField]
		private TMP_Text _numberOfProduct;
		[SerializeField]
		private TMP_Text _property;
		[SerializeField]
		private TMP_Text _price;
		[SerializeField]
		private Image _background;
		private int _id;
		private bool _isNotForCharacterType;
		private bool _characterHas;
		private bool _thisProductSelected;
		public static event Action<ProductView> PointerClicked;
		public static event Action<int> ProductSelected;
		public void ProductNotForCharacterType() {
			SetGreyColorForBackground();
			DisableRaycastTargetForBackground();
			_isNotForCharacterType = true;
		}
		public void SetCharacterHasTrueAndBackgroundGreen() {
			_characterHas = true;
			SetGreenColorForBackground();
		}
		private void OnEnable() {
			ProductView.PointerClicked += OnPointerClicked;
			CashBox.BuyProductSuccessAndCheckPrice += OnBuyProductSuccessAndCheckPrice;
		}

		private void OnDisable() {
			ProductView.PointerClicked -= OnPointerClicked;
			CashBox.BuyProductSuccessAndCheckPrice -= OnBuyProductSuccessAndCheckPrice;
			SetWhiteColorForBackgroundOnDisable();
			SetGreenColorForBackgroundOnDisable();
			SetFalseForThisProductSelected();

		}
		private void SetGreenColorForBackgroundOnDisable() {
			if (_characterHas) {
				SetGreenColorForBackground();
			}
		}

		private void SetWhiteColorForBackgroundOnDisable() {
			if (!_isNotForCharacterType && !_characterHas) {
				SetWhiteColorForBackground();
			}
		}
		private void OnPointerClicked(ProductView productView) {
			if (productView != this) {
				_thisProductSelected = false;
				if (_characterHas && !_thisProductSelected) {
					SetGreenColorForBackground();
					return;
				}
				if (_isNotForCharacterType || _characterHas)
					return;
				SetWhiteColorForBackground();
			}
		}

		public void OnPointerClick(PointerEventData eventData) {
			_thisProductSelected = true;
			SetColorForBackground();
			PointerClicked?.Invoke(this);
			if (_isNotForCharacterType)
				return;
			ProductSelected?.Invoke(_id);
		}
		private void OnBuyProductSuccessAndCheckPrice(int id) {
			if (ThisIsThisId(id)) {
				_characterHas = true;
				SetColorForBackground();
			}
		}

		private bool ThisIsThisId(int id) {
			return _id == id;
		}
		private void SetFalseForThisProductSelected() {
			_thisProductSelected = false;
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
			if (_isNotForCharacterType )
				return;
			if (_characterHas && _thisProductSelected) {
				SetCyanColorForBackground();
				return;
			}
			if (_characterHas && !_thisProductSelected) {
				SetGreenColorForBackground();
				return;
			}
			Limbo.GetOff(PrefsConstants.COINS_IN_WALLET, out int coinsInWallet);
			int price;
			if (TryGetPrice(out price)) {
				if (IsNotEnoughCoinForBuyProduct(coinsInWallet, price)) {
					_background.color = Color.red;
				return;
				} 
			}
			SetOrangeColorForBackground();
		}

		private void SetOrangeColorForBackground() {
			_background.color = new Color(0.9245283f, 0.5744624f, 0.3523674f, 1f);
		}

		private static bool IsNotEnoughCoinForBuyProduct(int coinsInWallet, int price) {
			return coinsInWallet < price;
		}

		private bool TryGetPrice(out int price) {
			return int.TryParse(_price.text, out price);
		}
		public Image Icon {
			get {
				return _icon;
			}
			set {
				_icon = value;
			}
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

		public TMP_Text Property {
			get {
				return _property;
			}
			set {
				_property = value;
			}
		}

		public TMP_Text Price {
			get {
				return _price;
			}
			set {
				_price = value;
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

		public bool isNotForCharacterType {
			get {
				return _isNotForCharacterType;
			}
		}

		public bool CharacterHas {
			get {
				return _characterHas;
			}
			set {
				_characterHas = value;
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