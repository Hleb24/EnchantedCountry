using Core.Main.Character;
using Core.Main.GameRule;
using Core.SO.ProductObjects;
using Core.SO.Storage;
using TMPro;
using UnityEngine;
using Zenject;

namespace Core.Mono.Scenes.CharacterList {
  public class ViewParameters : MonoBehaviour {
    [SerializeField]
    private SpawnProducts _spawnProducts;
    [SerializeField]
    private WalletInCharacterList _walletIn;
    [SerializeField]
    private TMP_Text _armorClassText;
    [SerializeField]
    private TMP_Text _meleeAttackText;
    [SerializeField]
    private TMP_Text _meleeDamageText;
    [SerializeField]
    private TMP_Text _rangeAttackText;
    [SerializeField]
    private TMP_Text _rangeDamageText;
    [SerializeField]
    private TMP_Text _maxAmountOfCoinsText;
    private IEquipmentUsed _equipmentUsed;
    private Qualities _qualities;
    private int _classOfArmor;
    private int _meleeAttack;
    private float _meleeMinDamage;
    private float _meleeMaxDamage;
    private int _rangeAttack;
    private float _rangeMinDamage;
    private float _rangeMaxDamage;
    private int _maxAmountOfCoins;

    private void OnEnable() {
      EquipmentsChoice.EquipmentChanged += OnEquipmentChanged;
    }

    private void OnDisable() {
      EquipmentsChoice.EquipmentChanged -= OnEquipmentChanged;
    }

    [Inject]
    public void Constructor(IEquipmentUsed equipmentUsed, Qualities qualities) {
      _equipmentUsed = equipmentUsed;
      _qualities = qualities;
    }

    private void OnEquipmentChanged() {
      ArmorClassParameters();
      MeleeParameters();
      RangeParameters();
      MaxAmountOfCoinsParameters();
    }

    private void GetArmorClass(StorageSO storageSO, int id) {
      if (id.Equals(0)) {
        return;
      }

      ProductSO product = storageSO.GetArmorFromList(id);
      Armor armor = product;
      _classOfArmor = armor.GetArmorClass() + _qualities.GetModifierOf(QualityType.Agility);
      SetTextForParameters(_armorClassText, _classOfArmor);
    }

    private void GetAttackForMeleeWeapon(StorageSO storageSO, int oneHandedId, int twoHandedId) {
      if (IsTwoWeapons(oneHandedId, twoHandedId)) {
        Debug.Log("Two weapon");
        return;
      }

      if (IsNoWeapon(oneHandedId, twoHandedId)) {
        Debug.Log("No weapon");
        return;
      }

      var id = 0;
      if (!oneHandedId.Equals(0)) {
        id = oneHandedId;
      }

      if (!twoHandedId.Equals(0)) {
        id = twoHandedId;
      }

      _meleeAttack = GetAttack(storageSO, id) + _qualities.GetModifierOf(QualityType.Strength);
      Debug.Log($"Сила {_qualities.GetPointsOf(QualityType.Strength)}, модификатор {_qualities.GetModifierOf(QualityType.Strength)}");
    }

    private void GetAttackForRangeWeapon(StorageSO storageSO, int rangeId) {
      if (rangeId.Equals(0)) {
        Debug.Log("Don't have range weapon");
        return;
      }

      _rangeAttack = GetAttack(storageSO, rangeId) + _qualities.GetModifierOf(QualityType.Agility);
    }

    private int GetAttack(StorageSO storageSO, int id) {
      ProductSO product = storageSO.GetWeaponFromList(id);
      Weapon weapon = product;
      int attack = weapon.GetAccuracy();
      return attack;
    }

    private void GetDamageForMeleeWeapon(StorageSO storageSO, int oneHandedId, int twoHandedId) {
      if (IsTwoWeapons(oneHandedId, twoHandedId)) {
        Debug.Log("Two weapon");
        return;
      }

      if (IsNoWeapon(oneHandedId, twoHandedId)) {
        Debug.Log("No weapon");
        _meleeMinDamage = 0;
        _meleeMaxDamage = 0;
        return;
      }

      var id = 0;
      if (!oneHandedId.Equals(0)) {
        id = oneHandedId;
      }

      if (!twoHandedId.Equals(0)) {
        id = twoHandedId;
      }

      GetMeleeDamage(storageSO, id);
    }

    private bool IsNoWeapon(int oneHandedId, int twoHandedId) {
      return oneHandedId.Equals(0) && twoHandedId.Equals(0);
    }

    private bool IsTwoWeapons(int oneHandedId, int twoHandedId) {
      return !oneHandedId.Equals(0) && !twoHandedId.Equals(0);
    }

    private void GetMeleeDamage(StorageSO storageSO, int id) {
      ProductSO product = storageSO.GetWeaponFromList(id);
      Weapon weapon = product;
      _meleeMinDamage = weapon.GetMinDamage();
      _meleeMaxDamage = weapon.GetMaxDamage();
    }

    private void GetDamageForRangeWeapon(StorageSO storageSO, int rangeId, int projectiliesId = 0) {
      if (rangeId.Equals(0)) {
        _rangeMinDamage = 0;
        _rangeMaxDamage = 0;
        Debug.Log("Don't have range weapon");
        return;
      }

      GetRangeDamage(storageSO, rangeId, projectiliesId);
    }

    private void GetRangeDamage(StorageSO storageSO, int rangeId, int projectiliesId = 0) {
      ProductSO product = storageSO.GetWeaponFromList(rangeId);
      Weapon weapon = product;
      _rangeMinDamage = weapon.GetMinDamage();
      _rangeMaxDamage = weapon.GetMaxDamage();
      if (projectiliesId == 0) {
        return;
      }

      ProductSO projectiliesProduct = storageSO.GetProjectilesFromList(projectiliesId);
      Weapon projectilies = projectiliesProduct;
      _rangeMinDamage += projectilies.GetMinDamage();
      _rangeMaxDamage += projectilies.GetMaxDamage();
    }

    private void MaxAmountOfCoins(StorageSO storageSO) {
      var maxCoins = 0;
      if (_equipmentUsed.GetEquipment(EquipmentsUsedId.BagId) != 0) {
        ProductSO productSO = storageSO.GetProductFromList(_equipmentUsed.GetEquipment(EquipmentsUsedId.BagId));
        maxCoins += int.Parse(productSO.GetProperty());
      }

      if (_equipmentUsed.GetEquipment(EquipmentsUsedId.AnimalId) != 0) {
        ProductSO productSO = storageSO.GetProductFromList(_equipmentUsed.GetEquipment(EquipmentsUsedId.AnimalId));
        maxCoins += int.Parse(productSO.GetProperty());
      }

      if (_equipmentUsed.GetEquipment(EquipmentsUsedId.CarriageId) != 0) {
        ProductSO productSO = storageSO.GetProductFromList(_equipmentUsed.GetEquipment(EquipmentsUsedId.CarriageId));
        maxCoins += int.Parse(productSO.GetProperty());
      }

      maxCoins += int.Parse(storageSO.GetProductFromList(EquipmentIdConstants.POCKETS).GetProperty());
      _maxAmountOfCoins = maxCoins;
      _walletIn.SetMaxCoins(_maxAmountOfCoins);
    }

    private void ArmorClassParameters() {
      GetArmorClass(_spawnProducts.StorageSO, _equipmentUsed.GetEquipment(EquipmentsUsedId.ArmorId));
    }

    private void MeleeParameters() {
      GetAttackForMeleeWeapon(_spawnProducts.StorageSO, _equipmentUsed.GetEquipment(EquipmentsUsedId.OneHandedId), _equipmentUsed.GetEquipment(EquipmentsUsedId.TwoHandedId));
      SetTextForParameters(_meleeAttackText, _meleeAttack);
      GetDamageForMeleeWeapon(_spawnProducts.StorageSO, _equipmentUsed.GetEquipment(EquipmentsUsedId.OneHandedId), _equipmentUsed.GetEquipment(EquipmentsUsedId.TwoHandedId));
      SetTextForParameters(_meleeDamageText, _meleeMinDamage, _meleeMaxDamage);
    }

    private void RangeParameters() {
      GetAttackForRangeWeapon(_spawnProducts.StorageSO, _equipmentUsed.GetEquipment(EquipmentsUsedId.RangeId));
      SetTextForParameters(_rangeAttackText, _rangeAttack);
      GetDamageForRangeWeapon(_spawnProducts.StorageSO, _equipmentUsed.GetEquipment(EquipmentsUsedId.RangeId), _equipmentUsed.GetEquipment(EquipmentsUsedId.ProjectilesId));
      SetTextForParameters(_rangeDamageText, _rangeMinDamage, _rangeMaxDamage);
    }

    private void MaxAmountOfCoinsParameters() {
      MaxAmountOfCoins(_spawnProducts.StorageSO);
      SetTextForParameters(_maxAmountOfCoinsText, _maxAmountOfCoins);
    }

    private void SetTextForParameters(TMP_Text text, int parameter) {
      text.text = parameter.ToString();
    }

    private void SetTextForParameters(TMP_Text text, float min, float max) {
      text.text = $"{min}-{max}";
    }
  }
}