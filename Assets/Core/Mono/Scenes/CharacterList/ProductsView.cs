using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Core.Mono.Scenes.CharacterList {
  public class ProductsView : MonoBehaviour, IPointerClickHandler {
    private static readonly List<int> ThisProductUnusedList = new List<int>();

    public static event Action<ProductsView> PointerClicked;
    public static event Action<int> ProductSelected;
    [SerializeField]
    private Image _background;
    [SerializeField]
    private TMP_Text _name;
    [SerializeField]
    private TMP_Text _numberOfProduct;
    private int _id;
    private bool _productWhatCanNotUsed;
    private bool _thisProductUsed;

    private void OnEnable() {
      CheckUnusedList();
      if (_thisProductUsed) {
        SetGreenColorForBackground();
      }

      PointerClicked += OnPointerClicked;
      EquipmentsChoice.ApplyButtonClicked += OnApplyButtonClicked;
      EquipmentsChoice.TakeOffEquipment += OnTakeOffEquipment;
    }

    private void OnDisable() {
      PointerClicked -= OnPointerClicked;
      EquipmentsChoice.ApplyButtonClicked -= OnApplyButtonClicked;
      EquipmentsChoice.TakeOffEquipment -= OnTakeOffEquipment;
      SetWhiteColorForBackgroundOnDisable();
    }

    public void OnPointerClick(PointerEventData eventData) {
      SetColorForBackground();
      PointerClicked?.Invoke(this);
      if (_productWhatCanNotUsed) {
        ProductSelected?.Invoke(0);
        return;
      }

      ProductSelected?.Invoke(_id);
    }

    public void ProductWhatCanNotUsed() {
      SetGreyColorForBackground();
      DisableRaycastTargetForBackground();
      _productWhatCanNotUsed = true;
    }

    public void SetProductName(string productName) {
      _name.text = productName;
    }

    public void SetNumberOfProduct(string numberOfProduct) {
      _numberOfProduct.text = numberOfProduct;
    }

    public void SetId(int id) {
      _id = id;
    }

    private void OnApplyButtonClicked(int id) {
      if (ThisIsThisId(id)) {
        _thisProductUsed = true;
        SetGreenColorForBackground();
      }
    }

    private void SetWhiteColorForBackgroundOnDisable() {
      if (!_productWhatCanNotUsed && !_thisProductUsed) {
        SetWhiteColorForBackground();
      }
    }

    private void OnPointerClicked(ProductsView productView) {
      if (productView != this) {
        if (_productWhatCanNotUsed) {
          return;
        }

        if (_thisProductUsed) {
          SetGreenColorForBackground();
          return;
        }

        SetWhiteColorForBackground();
      }
    }

    private void OnTakeOffEquipment(int id) {
      if (ThisIsThisId(id)) {
        _thisProductUsed = false;
        SetWhiteColorForBackground();
      } else {
        ThisProductUnusedList.Add(id);
      }
    }

    private bool ThisIsThisId(int id) {
      return _id == id;
    }

    private void CheckUnusedList() {
      if (ThisProductUnusedList.Count.Equals(0)) {
        return;
      }

      for (var i = 0; i < ThisProductUnusedList.Count; i++) {
        if (ThisIsThisId(ThisProductUnusedList[i])) {
          _thisProductUsed = false;
          SetWhiteColorForBackground();
          ThisProductUnusedList.RemoveAt(i);
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
      if (_productWhatCanNotUsed) {
        return;
      }

      if (_thisProductUsed) {
        SetCyanColorForBackground();
        return;
      }

      SetOrangeColorForBackground();
    }

    private void SetOrangeColorForBackground() {
      _background.color = new Color(0.9245283f, 0.5744624f, 0.3523674f, 1f);
    }
  }
}