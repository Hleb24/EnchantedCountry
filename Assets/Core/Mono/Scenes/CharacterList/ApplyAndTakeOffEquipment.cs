using System;
using Core.Rule.GameRule.EquipmentIdConstants;
using Core.ScriptableObject.Armor;
using Core.ScriptableObject.Products;
using Core.ScriptableObject.Storage;
using Core.ScriptableObject.Weapon;
using Core.SupportSystems.Data;
using Core.SupportSystems.SaveSystem.SaveManagers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Core.Rule.GameRule.Armor.Armor;
using static Core.Rule.GameRule.Weapon.Weapon;

namespace Core.Mono.Scenes.CharacterList {
  public class ApplyAndTakeOffEquipment : MonoBehaviour {
    public static event Action<int> ApplyButtonClicked;
    public static event Action<int> TakeOffEquipment;
    public static event Action EquipmentChanged;
    [SerializeField]
    private StorageObject _storage;
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
    [SerializeField]
    private bool _useGameSave;
    private IEquipmentUsed _equipmentUsed;
    private (ArmorType, TMP_Text) _armorTupleForText;
    private (ArmorType, TMP_Text) _shieldTupleForText;
    private (WeaponType, TMP_Text) _oneHandedTupleForText;
    private (WeaponType, TMP_Text) _twoHandedTupleForText;
    private (WeaponType, TMP_Text) _rangeTupleForText;
    private (WeaponType, TMP_Text) _projectiliesTupleForText;
    private (ArmorType, int) _armorTuple;
    private (ArmorType, int) _shieldTuple;
    private (WeaponType, int) _oneHandedTuple;
    private (WeaponType, int) _twoHandedTuple;
    private (WeaponType, int) _rangeTuple;
    private (WeaponType, int) _projectiliesTuple;
    private int _bagId;
    private int _animalId;
    private int _carriageId;
    private int _id;

    private void Start() {
      SetTuples();
      LoadUsedEquipmentDataWithInvoke();
    }

    private void OnEnable() {
      ProductsInCharacterListView.ProductSelected += OnProductSelected;
      ProductsSelectionInCharacterList.OpenArmorListOfProducts += OnOpenArmorListOfProducts;
      ProductsSelectionInCharacterList.OpenWeaponListOfProducts += OnWeaponListOfProducts;
      ProductsSelectionInCharacterList.OpenItemListOfProducts += OnOpenItemListOfProducts;
      _applyButton.onClick.AddListener(OnApplyButtonClicked);
      _takeOffButton.onClick.AddListener(OnTakeOffButtonClicked);
    }

    private void OnDisable() {
      ProductsInCharacterListView.ProductSelected -= OnProductSelected;
      ProductsSelectionInCharacterList.OpenArmorListOfProducts -= OnOpenArmorListOfProducts;
      ProductsSelectionInCharacterList.OpenWeaponListOfProducts -= OnWeaponListOfProducts;
      ProductsSelectionInCharacterList.OpenItemListOfProducts -= OnOpenItemListOfProducts;
      _applyButton.onClick.RemoveListener(OnApplyButtonClicked);
      _takeOffButton.onClick.RemoveListener(OnTakeOffButtonClicked);
    }

    private void SetTuples() {
      _armorTupleForText = (ArmorType.OnlyArmor, _armorText);
      _shieldTupleForText = (ArmorType.Shield, _shieldText);
      _oneHandedTupleForText = (WeaponType.OneHanded, _oneHandedText);
      _twoHandedTupleForText = (WeaponType.TwoHanded, _twoHandedText);
      _rangeTupleForText = (WeaponType.Range, _rangeText);
      _projectiliesTupleForText = (WeaponType.Projectilies, _projectilesText);
      _armorTuple = (ArmorType.OnlyArmor, EquipmentIdConstants.NoArmorId);
      _shieldTuple = (ArmorType.Shield, 0);
      _oneHandedTuple = (WeaponType.OneHanded, 0);
      _twoHandedTuple = (WeaponType.TwoHanded, 0);
      _rangeTuple = (WeaponType.Range, 0);
      _projectiliesTuple = (WeaponType.Projectilies, 0);
    }

    private void SetTupleByIdOnApplyButtonClicked() {
      if (_id.Equals(0)) {
        return;
      }

      ProductObject productObject = _storage.GetProductFromList(_id) ?? throw new ArgumentNullException("_storage.GetProductFromList(_id)");
      ProductObject.ProductType productType = productObject.productType;
      switch (productType) {
        case ProductObject.ProductType.Weapon:
          if (productObject.item is WeaponObject weapon) {
            if ((weapon.weaponType & _oneHandedTuple.Item1) != WeaponType.None) {
              TakeOffUsedEquipment(_oneHandedTuple.Item2);
              SetIdForOneHandedTuple();
              SetTextForOneHandedTuple(productObject.productName);
              SetUsedEquipmentDataForOneHandedWeapon();
              TakeOffUsedEquipment(_twoHandedTuple.Item2);
              SetIdForTwoHandedTuple(0);
              SetTextForTwoHandedTuple(string.Empty);
              SetUsedEquipmentDataForTwoHandedWeapon();
            }

            if ((weapon.weaponType & _twoHandedTuple.Item1) != WeaponType.None) {
              TakeOffUsedEquipment(_twoHandedTuple.Item2);
              SetIdForTwoHandedTuple();
              SetTextForTwoHandedTuple(productObject.productName);
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
              SetTextForRangeTuple(productObject.productName);
              SetUsedEquipmentDataForRangeWeapon();
            }

            if ((weapon.weaponType & _projectiliesTuple.Item1) != WeaponType.None) {
              CheckRangeSetForProjectiles(weapon);
              TakeOffUsedEquipment(_projectiliesTuple.Item2);
              SetIdForProjectiliesTuple();
              SetTextForProjectiliesTuple(productObject.productName);
              SetUsedEquipmentDataForProjectiles();
            }
          }

          break;
        case ProductObject.ProductType.Armor:
          if (productObject.item is ArmorObject armor) {
            if ((armor.armorType & _armorTuple.Item1) != ArmorType.None) {
              TakeOffUsedEquipment(_armorTuple.Item2);
              SetIdForArmorTuple();
              SetTextForArmorTuple(productObject.productName);
              SetUsedEquipmentDataForArmor();
            }

            if ((armor.armorType & _shieldTuple.Item1) != ArmorType.None) {
              TakeOffUsedEquipment(_shieldTuple.Item2);
              SetIdForShieldTuple();
              SetTextForShieldTuple(productObject.productName);
              SetUsedEquipmentDataForShield();
              TakeOffUsedEquipment(_twoHandedTuple.Item2);
              SetIdForTwoHandedTuple(0);
              SetTextForTwoHandedTuple(string.Empty);
              SetUsedEquipmentDataForTwoHandedWeapon();
            }
          }

          break;
        case ProductObject.ProductType.Item:
          if (EquipmentIdConstants.Bags.Contains(_id)) {
            TakeOffUsedEquipment(_bagId);
            SetIdForBag(_id);
            SetTextForBag(productObject.productName);
            SetUsedEquipmentDataForBag();
          }

          if (EquipmentIdConstants.Animals.Contains(_id)) {
            TakeOffUsedEquipment(_animalId);
            SetIdForAnimal(_id);
            SetTextForAnimal(productObject.productName);
            SetUsedEquipmentDataForAnimal();
          }

          if (EquipmentIdConstants.Carriages.Contains(_id)) {
            TakeOffUsedEquipment(_carriageId);
            SetIdForCarriage(_id);
            SetTextForCarriage(productObject.productName);
            SetUsedEquipmentDataForCarriage();
          }

          break;
      }

      SaveUsedEquipmentData();
      ApplyButtonClicked?.Invoke(_id);
    }

    private void CheckRangeSetForProjectiles(WeaponObject weapon) {
      if (_rangeTuple.Item2 != 0) {
        WeaponType rangeType = _storage.GetProductFromList(_rangeTuple.Item2).GetWeaponType();

        WeaponType rangeSet = default;
        if ((weapon.weaponType & WeaponType.SlingSet) != WeaponType.None) {
          rangeSet = WeaponType.SlingSet;
        }

        if ((weapon.weaponType & WeaponType.CrossbowSet) != WeaponType.None) {
          rangeSet = WeaponType.CrossbowSet;
        }

        if ((weapon.weaponType & WeaponType.ShotbowSet) != WeaponType.None) {
          rangeSet = WeaponType.ShotbowSet;
        }

        if ((weapon.weaponType & WeaponType.LongbowSet) != WeaponType.None) {
          rangeSet = WeaponType.LongbowSet;
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

    private void ChechRangeSetForRangeWeapon(WeaponObject weapon) {
      if (_projectiliesTuple.Item2 != 0) {
        WeaponType projetiliesType = _storage.GetProductFromList(_projectiliesTuple.Item2).GetWeaponType();

        WeaponType rangeSet = default;
        if ((weapon.weaponType & WeaponType.SlingSet) != WeaponType.None) {
          rangeSet = WeaponType.SlingSet;
        }

        if ((weapon.weaponType & WeaponType.CrossbowSet) != WeaponType.None) {
          rangeSet = WeaponType.CrossbowSet;
        }

        if ((weapon.weaponType & WeaponType.ShotbowSet) != WeaponType.None) {
          rangeSet = WeaponType.ShotbowSet;
        }

        if ((weapon.weaponType & WeaponType.LongbowSet) != WeaponType.None) {
          rangeSet = WeaponType.LongbowSet;
        }

        if ((weapon.weaponType & WeaponType.CatapultSet) != WeaponType.None) {
          rangeSet = WeaponType.CatapultSet;
        }

        if ((rangeSet & projetiliesType) == WeaponType.None) {
          TakeOffEquipment?.Invoke(_projectiliesTuple.Item2);
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

      ProductObject productObject = _storage.GetProductFromList(id);
      ProductObject.ProductType productType = productObject.productType;
      switch (productType) {
        case ProductObject.ProductType.Weapon:
          var weapon = productObject.item as WeaponObject;
          if ((weapon.weaponType & _oneHandedTuple.Item1) != WeaponType.None) {
            SetTextForOneHandedTuple(productObject.productName);
          }

          if ((weapon.weaponType & _twoHandedTuple.Item1) != WeaponType.None) {
            SetTextForTwoHandedTuple(productObject.productName);
          }

          if ((weapon.weaponType & _rangeTuple.Item1) != WeaponType.None) {
            SetTextForRangeTuple(productObject.productName);
          }

          if ((weapon.weaponType & _projectiliesTuple.Item1) != WeaponType.None) {
            SetTextForProjectiliesTuple(productObject.productName);
          }

          break;
        case ProductObject.ProductType.Armor:
          var armor = productObject.item as ArmorObject;
          if ((armor.armorType & _armorTuple.Item1) != ArmorType.None) {
            SetTextForArmorTuple(productObject.productName);
          }

          if ((armor.armorType & _shieldTuple.Item1) != ArmorType.None) {
            SetTextForShieldTuple(productObject.productName);
          }

          break;
        case ProductObject.ProductType.Item:
          if (EquipmentIdConstants.Bags.Contains(id)) {
            SetTextForBag(productObject.productName);
          }

          if (EquipmentIdConstants.Animals.Contains(id)) {
            SetTextForAnimal(productObject.productName);
          }

          if (EquipmentIdConstants.Carriages.Contains(id)) {
            SetTextForCarriage(productObject.productName);
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
        SetIdForArmorTuple(EquipmentIdConstants.NoArmorId);
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

      if (_id == _projectiliesTuple.Item2) {
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
      ApplyButtonClicked?.Invoke(_projectiliesTuple.Item2);
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
      _projectiliesTuple.Item2 = _id;
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
      _projectiliesTuple.Item2 = id;
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
      _projectiliesTupleForText.Item2.text = productName;
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
      _equipmentUsed.SetEquipment(EquipmentsUsedId.ProjectilesId, _projectiliesTuple.Item2);
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
      if (_useGameSave) {
        // GSSSingleton.Instance.SaveInGame();
        EquipmentChanged?.Invoke();
      }
    }

    private void LoadUsedEquipmentDataWithInvoke() {
      if (_testUsedEquipment) {
        return;
      }

      if (_useGameSave) {
        _equipmentUsed = ScribeDealer.Peek<EquipmentUsedScribe>();
        Invoke(nameof(SetTuplesAfterLoadUsedEquipmentData), 0.3f);
      }
    }

    private void SetTuplesAfterLoadUsedEquipmentData() {
      _armorTuple.Item2 = _equipmentUsed.GetEquipment(EquipmentsUsedId.ArmorId);
      _shieldTuple.Item2 = _equipmentUsed.GetEquipment(EquipmentsUsedId.ShieldId);
      _oneHandedTuple.Item2 = _equipmentUsed.GetEquipment(EquipmentsUsedId.OneHandedId);
      _twoHandedTuple.Item2 = _equipmentUsed.GetEquipment(EquipmentsUsedId.TwoHandedId);
      _rangeTuple.Item2 = _equipmentUsed.GetEquipment(EquipmentsUsedId.RangeId);
      _projectiliesTuple.Item2 = _equipmentUsed.GetEquipment(EquipmentsUsedId.ProjectilesId);
      _bagId = _equipmentUsed.GetEquipment(EquipmentsUsedId.BagId);
      _animalId = _equipmentUsed.GetEquipment(EquipmentsUsedId.AnimalId);
      _carriageId = _equipmentUsed.GetEquipment(EquipmentsUsedId.CarriageId);
      SetTextTuplesById(_armorTuple.Item2);
      SetTextTuplesById(_shieldTuple.Item2);
      SetTextTuplesById(_oneHandedTuple.Item2);
      SetTextTuplesById(_twoHandedTuple.Item2);
      SetTextTuplesById(_rangeTuple.Item2);
      SetTextTuplesById(_projectiliesTuple.Item2);
      SetTextTuplesById(_bagId);
      SetTextTuplesById(_animalId);
      SetTextTuplesById(_carriageId);
      EquipmentChanged?.Invoke();
    }
  }
}