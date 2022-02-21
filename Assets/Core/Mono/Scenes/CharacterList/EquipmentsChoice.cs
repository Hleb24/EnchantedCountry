using System;
using Core.Main.GameRule;
using Core.Mono.MainManagers;
using Core.SO.Armor;
using Core.SO.Product;
using Core.SO.Storage;
using Core.SO.WeaponObjects;
using Core.Support.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Core.Mono.Scenes.CharacterList {
  public class EquipmentsChoice : MonoBehaviour {
    public static event Action<int> ApplyButtonClicked;
    public static event Action<int> TakeOffEquipment;
    public static event Action EquipmentChanged;
    [SerializeField]
    private StorageSO _storage;
    [SerializeField]
    private Button _applyButton;
    [SerializeField]
    private Button _takeOffButton;
    [SerializeField]
    private TMP_Text _armorText;
    [SerializeField]
    private TMP_Text _shieldText;
    [SerializeField]
    private TMP_Text _oneHandedText;
    [SerializeField]
    private TMP_Text _twoHandedText;
    [SerializeField]
    private TMP_Text _rangeText;
    [SerializeField]
    private TMP_Text _projectilesText;
    [SerializeField]
    private TMP_Text _bag;
    [SerializeField]
    private TMP_Text _animal;
    [SerializeField]
    private TMP_Text _carriage;
    [SerializeField]
    private bool _testUsedEquipment;
    private IStartGame _startGame;
    private IEquipmentUsed _equipmentUsed;
    private (ArmorType, TMP_Text) _armorTupleForText;
    private (ArmorType, TMP_Text) _shieldTupleForText;
    private (WeaponType, TMP_Text) _oneHandedTupleForText;
    private (WeaponType, TMP_Text) _twoHandedTupleForText;
    private (WeaponType, TMP_Text) _rangeTupleForText;
    private (WeaponType, TMP_Text) _projectilesTupleForText;
    private (ArmorType, int) _armorTuple;
    private (ArmorType, int) _shieldTuple;
    private (WeaponType, int) _oneHandedTuple;
    private (WeaponType, int) _twoHandedTuple;
    private (WeaponType, int) _rangeTuple;
    private (WeaponType, int) _projectilesTuple;
    private int _bagId;
    private int _animalId;
    private int _carriageId;
    private int _id;

    private void Start() {
      SetTuples();
      LoadUsedEquipmentDataWithInvoke();
    }

    private void OnEnable() {
      ProductsView.ProductSelected += OnProductSelected;
      ProductsSelection.OpenArmorListOfProducts += OnOpenArmorListOfProducts;
      ProductsSelection.OpenWeaponListOfProducts += OnWeaponListOfProducts;
      ProductsSelection.OpenItemListOfProducts += OnOpenItemListOfProducts;
      _applyButton.onClick.AddListener(OnApplyButtonClicked);
      _takeOffButton.onClick.AddListener(OnTakeOffButtonClicked);
    }

    private void OnDisable() {
      ProductsView.ProductSelected -= OnProductSelected;
      ProductsSelection.OpenArmorListOfProducts -= OnOpenArmorListOfProducts;
      ProductsSelection.OpenWeaponListOfProducts -= OnWeaponListOfProducts;
      ProductsSelection.OpenItemListOfProducts -= OnOpenItemListOfProducts;
      _applyButton.onClick.RemoveListener(OnApplyButtonClicked);
      _takeOffButton.onClick.RemoveListener(OnTakeOffButtonClicked);
    }

    [Inject]
    public void Constructor(IStartGame startGame, IEquipmentUsed equipmentUsed) {
      _startGame = startGame;
      _equipmentUsed = equipmentUsed;
    }

    private void SetTuples() {
      _armorTupleForText = (ArmorType.OnlyArmor, _armorText);
      _shieldTupleForText = (ArmorType.Shield, _shieldText);
      _oneHandedTupleForText = (WeaponType.OneHanded, _oneHandedText);
      _twoHandedTupleForText = (WeaponType.TwoHanded, _twoHandedText);
      _rangeTupleForText = (WeaponType.Range, _rangeText);
      _projectilesTupleForText = (WeaponType.Projectiles, _projectilesText);
      _armorTuple = (ArmorType.OnlyArmor, EquipmentIdConstants.NO_ARMOR_ID);
      _shieldTuple = (ArmorType.Shield, 0);
      _oneHandedTuple = (WeaponType.OneHanded, 0);
      _twoHandedTuple = (WeaponType.TwoHanded, 0);
      _rangeTuple = (WeaponType.Range, 0);
      _projectilesTuple = (WeaponType.Projectiles, 0);
    }

    private void SetTupleByIdOnApplyButtonClicked() {
      if (_id.Equals(0)) {
        return;
      }

      ProductSO productSO = _storage.GetProductFromList(_id) ?? throw new ArgumentNullException("_storage.GetProductFromList(_id)");
      ProductSO.ProductType productType = productSO.GetProductType();
      switch (productType) {
        case ProductSO.ProductType.Weapon:
          if (productSO.GetItem() is WeaponSO weapon) {
            if ((weapon.weaponType & _oneHandedTuple.Item1) != WeaponType.None) {
              TakeOffUsedEquipment(_oneHandedTuple.Item2);
              SetIdForOneHandedTuple();
              SetTextForOneHandedTuple(productSO.GetProductName());
              SetUsedEquipmentDataForOneHandedWeapon();
              TakeOffUsedEquipment(_twoHandedTuple.Item2);
              SetIdForTwoHandedTuple(0);
              SetTextForTwoHandedTuple(string.Empty);
              SetUsedEquipmentDataForTwoHandedWeapon();
            }

            if ((weapon.weaponType & _twoHandedTuple.Item1) != WeaponType.None) {
              TakeOffUsedEquipment(_twoHandedTuple.Item2);
              SetIdForTwoHandedTuple();
              SetTextForTwoHandedTuple(productSO.GetProductName());
              SetUsedEquipmentDataForTwoHandedWeapon();
              TakeOffUsedEquipment(_shieldTuple.Item2);
              TakeOffUsedEquipment(_oneHandedTuple.Item2);
              SetIdForShieldTuple(0);
              SetIdForOneHandedTuple(0);
              SetTextForShieldTuple(string.Empty);
              SetTextForOneHandedTuple(string.Empty);
              SetUsedEquipmentDataForShield();
              SetUsedEquipmentDataForOneHandedWeapon();
            }

            if ((weapon.weaponType & _rangeTuple.Item1) != WeaponType.None) {
              ChechRangeSetForRangeWeapon(weapon);
              TakeOffUsedEquipment(_rangeTuple.Item2);
              SetIdForRangeTuple();
              SetTextForRangeTuple(productSO.GetProductName());
              SetUsedEquipmentDataForRangeWeapon();
            }

            if ((weapon.weaponType & _projectilesTuple.Item1) != WeaponType.None) {
              CheckRangeSetForProjectiles(weapon);
              TakeOffUsedEquipment(_projectilesTuple.Item2);
              SetIdForProjectiliesTuple();
              SetTextForProjectiliesTuple(productSO.GetProductName());
              SetUsedEquipmentDataForProjectiles();
            }
          }

          break;
        case ProductSO.ProductType.Armor:
          if (productSO.GetItem() is ArmorSO armor) {
            if ((armor.armorType & _armorTuple.Item1) != ArmorType.None) {
              TakeOffUsedEquipment(_armorTuple.Item2);
              SetIdForArmorTuple();
              SetTextForArmorTuple(productSO.GetProductName());
              SetUsedEquipmentDataForArmor();
            }

            if ((armor.armorType & _shieldTuple.Item1) != ArmorType.None) {
              TakeOffUsedEquipment(_shieldTuple.Item2);
              SetIdForShieldTuple();
              SetTextForShieldTuple(productSO.GetProductName());
              SetUsedEquipmentDataForShield();
              TakeOffUsedEquipment(_twoHandedTuple.Item2);
              SetIdForTwoHandedTuple(0);
              SetTextForTwoHandedTuple(string.Empty);
              SetUsedEquipmentDataForTwoHandedWeapon();
            }
          }

          break;
        case ProductSO.ProductType.Item:
          if (EquipmentIdConstants.Bags.Contains(_id)) {
            TakeOffUsedEquipment(_bagId);
            SetIdForBag(_id);
            SetTextForBag(productSO.GetProductName());
            SetUsedEquipmentDataForBag();
          }

          if (EquipmentIdConstants.Animals.Contains(_id)) {
            TakeOffUsedEquipment(_animalId);
            SetIdForAnimal(_id);
            SetTextForAnimal(productSO.GetProductName());
            SetUsedEquipmentDataForAnimal();
          }

          if (EquipmentIdConstants.Carriages.Contains(_id)) {
            TakeOffUsedEquipment(_carriageId);
            SetIdForCarriage(_id);
            SetTextForCarriage(productSO.GetProductName());
            SetUsedEquipmentDataForCarriage();
          }

          break;
      }

      SaveUsedEquipmentData();
      ApplyButtonClicked?.Invoke(_id);
    }

    private void CheckRangeSetForProjectiles(WeaponSO weapon) {
      if (_rangeTuple.Item2 != 0) {
        WeaponType rangeType = _storage.GetProductFromList(_rangeTuple.Item2).GetWeaponType();

        WeaponType rangeSet = default;
        if ((weapon.weaponType & WeaponType.SlingSet) != WeaponType.None) {
          rangeSet = WeaponType.SlingSet;
        }

        if ((weapon.weaponType & WeaponType.CrossbowSet) != WeaponType.None) {
          rangeSet = WeaponType.CrossbowSet;
        }

        if ((weapon.weaponType & WeaponType.ShortBowSet) != WeaponType.None) {
          rangeSet = WeaponType.ShortBowSet;
        }

        if ((weapon.weaponType & WeaponType.LongBowSet) != WeaponType.None) {
          rangeSet = WeaponType.LongBowSet;
        }

        if ((weapon.weaponType & WeaponType.CatapultSet) != WeaponType.None) {
          rangeSet = WeaponType.CatapultSet;
        }

        if ((rangeSet & rangeType) == WeaponType.None) {
          TakeOffEquipment?.Invoke(_rangeTuple.Item2);
          SetIdForRangeTuple(0);
          SetTextForRangeTuple(string.Empty);
          SetUsedEquipmentDataForRangeWeapon();
          SaveUsedEquipmentData();
        }
      }
    }

    private void ChechRangeSetForRangeWeapon(WeaponSO weapon) {
      if (_projectilesTuple.Item2 != 0) {
        WeaponType projetiliesType = _storage.GetProductFromList(_projectilesTuple.Item2).GetWeaponType();

        WeaponType rangeSet = default;
        if ((weapon.weaponType & WeaponType.SlingSet) != WeaponType.None) {
          rangeSet = WeaponType.SlingSet;
        }

        if ((weapon.weaponType & WeaponType.CrossbowSet) != WeaponType.None) {
          rangeSet = WeaponType.CrossbowSet;
        }

        if ((weapon.weaponType & WeaponType.ShortBowSet) != WeaponType.None) {
          rangeSet = WeaponType.ShortBowSet;
        }

        if ((weapon.weaponType & WeaponType.LongBowSet) != WeaponType.None) {
          rangeSet = WeaponType.LongBowSet;
        }

        if ((weapon.weaponType & WeaponType.CatapultSet) != WeaponType.None) {
          rangeSet = WeaponType.CatapultSet;
        }

        if ((rangeSet & projetiliesType) == WeaponType.None) {
          TakeOffEquipment?.Invoke(_projectilesTuple.Item2);
          SetIdForProjectiliesTuple(0);
          SetTextForProjectiliesTuple(string.Empty);
          SetUsedEquipmentDataForProjectiles();
          SaveUsedEquipmentData();
        }
      }
    }

    private void TakeOffUsedEquipment(int tupleId) {
      if (tupleId != 0) {
        TakeOffEquipment?.Invoke(tupleId);
      }
    }

    private void SetTextTuplesById(int id) {
      if (id.Equals(0)) {
        return;
      }

      ProductSO productSO = _storage.GetProductFromList(id);
      ProductSO.ProductType productType = productSO.GetProductType();
      switch (productType) {
        case ProductSO.ProductType.Weapon:
          var weapon = productSO.GetItem() as WeaponSO;
          if ((weapon.weaponType & _oneHandedTuple.Item1) != WeaponType.None) {
            SetTextForOneHandedTuple(productSO.GetProductName());
          }

          if ((weapon.weaponType & _twoHandedTuple.Item1) != WeaponType.None) {
            SetTextForTwoHandedTuple(productSO.GetProductName());
          }

          if ((weapon.weaponType & _rangeTuple.Item1) != WeaponType.None) {
            SetTextForRangeTuple(productSO.GetProductName());
          }

          if ((weapon.weaponType & _projectilesTuple.Item1) != WeaponType.None) {
            SetTextForProjectiliesTuple(productSO.GetProductName());
          }

          break;
        case ProductSO.ProductType.Armor:
          var armor = productSO.GetItem() as ArmorSO;
          if ((armor.armorType & _armorTuple.Item1) != ArmorType.None) {
            SetTextForArmorTuple(productSO.GetProductName());
          }

          if ((armor.armorType & _shieldTuple.Item1) != ArmorType.None) {
            SetTextForShieldTuple(productSO.GetProductName());
          }

          break;
        case ProductSO.ProductType.Item:
          if (EquipmentIdConstants.Bags.Contains(id)) {
            SetTextForBag(productSO.GetProductName());
          }

          if (EquipmentIdConstants.Animals.Contains(id)) {
            SetTextForAnimal(productSO.GetProductName());
          }

          if (EquipmentIdConstants.Carriages.Contains(id)) {
            SetTextForCarriage(productSO.GetProductName());
          }

          break;
      }

      ApplyButtonClicked?.Invoke(id);
    }

    private void OnProductSelected(int id) {
      _id = id;
    }

    private void OnApplyButtonClicked() {
      SetTupleByIdOnApplyButtonClicked();
    }

    private void OnTakeOffButtonClicked() {
      if (_id.Equals(0)) {
        return;
      }

      if (_id == _armorTuple.Item2) {
        SetIdForArmorTuple(EquipmentIdConstants.NO_ARMOR_ID);
        SetTextForArmorTuple("No");
        TakeOffEquipment?.Invoke(_id);
        SetUsedEquipmentDataForArmor();
        SaveUsedEquipmentData();
        return;
      }

      if (_id == _shieldTuple.Item2) {
        SetIdForShieldTuple(0);
        SetTextForShieldTuple(string.Empty);
        TakeOffEquipment?.Invoke(_id);
        SetUsedEquipmentDataForShield();
        SaveUsedEquipmentData();
        return;
      }

      if (_id == _oneHandedTuple.Item2) {
        SetIdForOneHandedTuple(0);
        SetTextForOneHandedTuple(string.Empty);
        TakeOffEquipment?.Invoke(_id);
        SetUsedEquipmentDataForOneHandedWeapon();
        SaveUsedEquipmentData();
        return;
      }

      if (_id == _twoHandedTuple.Item2) {
        SetIdForTwoHandedTuple(0);
        SetTextForTwoHandedTuple(string.Empty);
        TakeOffEquipment?.Invoke(_id);
        SetUsedEquipmentDataForTwoHandedWeapon();
        SaveUsedEquipmentData();
        return;
      }

      if (_id == _rangeTuple.Item2) {
        SetIdForRangeTuple(0);
        SetTextForRangeTuple(string.Empty);
        TakeOffEquipment?.Invoke(_id);
        SetUsedEquipmentDataForRangeWeapon();
        SaveUsedEquipmentData();
        return;
      }

      if (_id == _projectilesTuple.Item2) {
        SetIdForProjectiliesTuple(0);
        SetTextForProjectiliesTuple(string.Empty);
        TakeOffEquipment?.Invoke(_id);
        SetUsedEquipmentDataForProjectiles();
        SaveUsedEquipmentData();
        return;
      }

      if (_id == _bagId) {
        SetIdForBag(0);
        SetTextForBag(string.Empty);
        TakeOffEquipment?.Invoke(_id);
        SetUsedEquipmentDataForBag();
        SaveUsedEquipmentData();
        return;
      }

      if (_id == _animalId) {
        SetIdForAnimal(0);
        SetTextForAnimal(string.Empty);
        TakeOffEquipment?.Invoke(_id);
        SetUsedEquipmentDataForAnimal();
        SaveUsedEquipmentData();
        return;
      }

      if (_id == _carriageId) {
        SetIdForCarriage(0);
        SetTextForCarriage(string.Empty);
        TakeOffEquipment?.Invoke(_id);
        SetUsedEquipmentDataForCarriage();
        SaveUsedEquipmentData();
      }
    }

    private void OnOpenArmorListOfProducts() {
      ApplyButtonClicked?.Invoke(_armorTuple.Item2);
      ApplyButtonClicked?.Invoke(_shieldTuple.Item2);
    }

    private void OnWeaponListOfProducts() {
      ApplyButtonClicked?.Invoke(_oneHandedTuple.Item2);
      ApplyButtonClicked?.Invoke(_twoHandedTuple.Item2);
      ApplyButtonClicked?.Invoke(_rangeTuple.Item2);
      ApplyButtonClicked?.Invoke(_projectilesTuple.Item2);
    }

    private void OnOpenItemListOfProducts() {
      ApplyButtonClicked?.Invoke(_bagId);
      ApplyButtonClicked?.Invoke(_animalId);
      ApplyButtonClicked?.Invoke(_carriageId);
    }

    private void SetIdForArmorTuple() {
      _armorTuple.Item2 = _id;
    }

    private void SetIdForShieldTuple() {
      _shieldTuple.Item2 = _id;
    }

    private void SetIdForOneHandedTuple() {
      _oneHandedTuple.Item2 = _id;
    }

    private void SetIdForTwoHandedTuple() {
      _twoHandedTuple.Item2 = _id;
    }

    private void SetIdForRangeTuple() {
      _rangeTuple.Item2 = _id;
    }

    private void SetIdForProjectiliesTuple() {
      _projectilesTuple.Item2 = _id;
    }

    private void SetIdForArmorTuple(int id) {
      _armorTuple.Item2 = id;
    }

    private void SetIdForShieldTuple(int id) {
      _shieldTuple.Item2 = id;
    }

    private void SetIdForOneHandedTuple(int id) {
      _oneHandedTuple.Item2 = id;
    }

    private void SetIdForTwoHandedTuple(int id) {
      _twoHandedTuple.Item2 = id;
    }

    private void SetIdForRangeTuple(int id) {
      _rangeTuple.Item2 = id;
    }

    private void SetIdForProjectiliesTuple(int id) {
      _projectilesTuple.Item2 = id;
    }

    private void SetIdForBag(int id) {
      _bagId = id;
    }

    private void SetIdForAnimal(int id) {
      _animalId = id;
    }

    private void SetIdForCarriage(int id) {
      _carriageId = id;
    }

    private void SetTextForArmorTuple(string productName) {
      _armorTupleForText.Item2.text = productName;
    }

    private void SetTextForShieldTuple(string productName) {
      _shieldTupleForText.Item2.text = productName;
    }

    private void SetTextForOneHandedTuple(string productName) {
      _oneHandedTupleForText.Item2.text = productName;
    }

    private void SetTextForTwoHandedTuple(string productName) {
      _twoHandedTupleForText.Item2.text = productName;
    }

    private void SetTextForRangeTuple(string productName) {
      _rangeTupleForText.Item2.text = productName;
    }

    private void SetTextForProjectiliesTuple(string productName) {
      _projectilesTupleForText.Item2.text = productName;
    }

    private void SetTextForBag(string productName) {
      _bag.text = productName;
    }

    private void SetTextForAnimal(string productName) {
      _animal.text = productName;
    }

    private void SetTextForCarriage(string productName) {
      _carriage.text = productName;
    }

    private void SetUsedEquipmentDataForArmor() {
      _equipmentUsed.SetEquipment(EquipmentsUsedId.ArmorId, _armorTuple.Item2);
    }

    private void SetUsedEquipmentDataForShield() {
      _equipmentUsed.SetEquipment(EquipmentsUsedId.ShieldId, _shieldTuple.Item2);
    }

    private void SetUsedEquipmentDataForOneHandedWeapon() {
      _equipmentUsed.SetEquipment(EquipmentsUsedId.OneHandedId, _oneHandedTuple.Item2);
    }

    private void SetUsedEquipmentDataForTwoHandedWeapon() {
      _equipmentUsed.SetEquipment(EquipmentsUsedId.TwoHandedId, _twoHandedTuple.Item2);
    }

    private void SetUsedEquipmentDataForRangeWeapon() {
      _equipmentUsed.SetEquipment(EquipmentsUsedId.RangeId, _rangeTuple.Item2);
    }

    private void SetUsedEquipmentDataForProjectiles() {
      _equipmentUsed.SetEquipment(EquipmentsUsedId.ProjectilesId, _projectilesTuple.Item2);
    }

    private void SetUsedEquipmentDataForBag() {
      _equipmentUsed.SetEquipment(EquipmentsUsedId.BagId, _bagId);
    }

    private void SetUsedEquipmentDataForAnimal() {
      _equipmentUsed.SetEquipment(EquipmentsUsedId.AnimalId, _animalId);
    }

    private void SetUsedEquipmentDataForCarriage() {
      _equipmentUsed.SetEquipment(EquipmentsUsedId.CarriageId, _carriageId);
    }

    private void SaveUsedEquipmentData() {
      if (_startGame.UseGameSave()) {
        EquipmentChanged?.Invoke();
      }
    }

    private void LoadUsedEquipmentDataWithInvoke() {
      if (_testUsedEquipment) {
        return;
      }

      if (_startGame.UseGameSave()) {
        SetTuplesAfterLoadUsedEquipmentData();
      }
    }

    private void SetTuplesAfterLoadUsedEquipmentData() {
      _armorTuple.Item2 = _equipmentUsed.GetEquipment(EquipmentsUsedId.ArmorId);
      _shieldTuple.Item2 = _equipmentUsed.GetEquipment(EquipmentsUsedId.ShieldId);
      _oneHandedTuple.Item2 = _equipmentUsed.GetEquipment(EquipmentsUsedId.OneHandedId);
      _twoHandedTuple.Item2 = _equipmentUsed.GetEquipment(EquipmentsUsedId.TwoHandedId);
      _rangeTuple.Item2 = _equipmentUsed.GetEquipment(EquipmentsUsedId.RangeId);
      _projectilesTuple.Item2 = _equipmentUsed.GetEquipment(EquipmentsUsedId.ProjectilesId);
      _bagId = _equipmentUsed.GetEquipment(EquipmentsUsedId.BagId);
      _animalId = _equipmentUsed.GetEquipment(EquipmentsUsedId.AnimalId);
      _carriageId = _equipmentUsed.GetEquipment(EquipmentsUsedId.CarriageId);
      SetTextTuplesById(_armorTuple.Item2);
      SetTextTuplesById(_shieldTuple.Item2);
      SetTextTuplesById(_oneHandedTuple.Item2);
      SetTextTuplesById(_twoHandedTuple.Item2);
      SetTextTuplesById(_rangeTuple.Item2);
      SetTextTuplesById(_projectilesTuple.Item2);
      SetTextTuplesById(_bagId);
      SetTextTuplesById(_animalId);
      SetTextTuplesById(_carriageId);
      EquipmentChanged?.Invoke();
    }
  }
}