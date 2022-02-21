using System;
using Core.Support.PrefsTools;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Core.Mono.Scenes.TrurlsShop {
  public class ProductView : MonoBehaviour, IPointerClickHandler {
    private static bool IsNotEnoughCoinForBuyProduct(int coinsInWallet, int price) {
      return coinsInWallet < price;
    }

    public static event Action<ProductView> PointerClicked;
    public static event Action<int> ProductSelected;
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
    private bool _isNotForCharacterType;
    private bool _characterHas;
    private bool _thisProductSelected;
    private int _id;

    private void OnEnable() {
      PointerClicked += OnPointerClicked;
      CashBox.BuyProductSuccessAndCheckPrice += OnBuyProductSuccessAndCheckPrice;
    }

    private void OnDisable() {
      PointerClicked -= OnPointerClicked;
      CashBox.BuyProductSuccessAndCheckPrice -= OnBuyProductSuccessAndCheckPrice;
      SetWhiteColorForBackgroundOnDisable();
      SetGreenColorForBackgroundOnDisable();
      SetFalseForThisProductSelected();
    }

    public void OnPointerClick(PointerEventData eventData) {
      _thisProductSelected = true;
      SetColorForBackground();
      PointerClicked?.Invoke(this);
      if (_isNotForCharacterType) {
        return;
      }

      ProductSelected?.Invoke(_id);
    }

    public void ProductNotForCharacterType() {
      SetGreyColorForBackground();
      DisableRaycastTargetForBackground();
      _isNotForCharacterType = true;
    }

    public void SetCharacterHasTrueAndBackgroundGreen() {
      _characterHas = true;
      SetGreenColorForBackground();
    }

    public void SetIcon(Sprite sprite) {
      _icon.sprite = sprite;
    }

    public void SetName(string productName) {
      _name.text = productName;
    }

    public void SetNumberOfProduct(string numberOfProduct) {
      _numberOfProduct.text = numberOfProduct;
    }

    public void SetProperty(string property) {
      _property.text = property;
    }

    public void SetPrice(string price) {
      _price.text = price;
    }

    public void SetId(int id) {
      _id = id;
    }

    public int GetId() {
      return _id;
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

        if (_isNotForCharacterType || _characterHas) {
          return;
        }

        SetWhiteColorForBackground();
      }
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
      if (_isNotForCharacterType) {
        return;
      }

      if (_characterHas && _thisProductSelected) {
        SetCyanColorForBackground();
        return;
      }

      if (_characterHas && !_thisProductSelected) {
        SetGreenColorForBackground();
        return;
      }

      Limbo.GetOff(PrefsConstants.COINS_IN_WALLET, out int coinsInWallet);
      if (TryGetPrice(out int price)) {
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

    private bool TryGetPrice(out int price) {
      return int.TryParse(_price.text, out price);
    }
  }
}