using System;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Mono.BaseClass {
  public class ProductSelection : MonoBehaviour {
    public static Action OpenNewListOfProducts;
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

    protected virtual void OnEnable() {
      AddListener();
    }

    protected virtual void OnDisable() {
      RemoveListener();
    }

    protected virtual void AddListener() {
      _armorButton.onClick.AddListener(HandlerForArmorButton);
      _weaponButton.onClick.AddListener(HandlerForWeaponButton);
      _itemButton.onClick.AddListener(HandlerForItemButton);
    }

    protected virtual void RemoveListener() {
      _armorButton.onClick.RemoveListener(HandlerForArmorButton);
      _weaponButton.onClick.RemoveListener(HandlerForWeaponButton);
      _itemButton.onClick.RemoveListener(HandlerForItemButton);
    }

    protected void OnSpawnCompleted() {
      ActivateArmorScrollView();
      DeactivateWeaponScrollView();
      DeactivateItemScrollView();
    }

    protected virtual void HandlerForArmorButton() {
      ActivateArmorScrollView();
      DeactivateWeaponScrollView();
      DeactivateItemScrollView();
      OpenNewListOfProducts?.Invoke();
    }

    protected virtual void HandlerForWeaponButton() {
      ActivateWeaponScrollView();
      DeactivateArmorScrollView();
      DeactivateItemScrollView();
      OpenNewListOfProducts?.Invoke();
    }

    protected virtual void HandlerForItemButton() {
      ActivateItemScrollView();
      DeactivateArmorScrollView();
      DeactivateWeaponScrollView();
      OpenNewListOfProducts?.Invoke();
    }

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
  }
}