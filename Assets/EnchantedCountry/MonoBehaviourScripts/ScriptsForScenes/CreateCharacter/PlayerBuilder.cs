using System;
using Core.EnchantedCountry.CoreEnchantedCountry.Character;
using Core.EnchantedCountry.CoreEnchantedCountry.Character.Equipment;
using Core.EnchantedCountry.CoreEnchantedCountry.Character.GamePoints;
using Core.EnchantedCountry.CoreEnchantedCountry.Character.Levels;
using Core.EnchantedCountry.CoreEnchantedCountry.Character.Qualities;
using Core.EnchantedCountry.CoreEnchantedCountry.Character.Wallet;
using Core.EnchantedCountry.CoreEnchantedCountry.GameRule.Armor;
using Core.EnchantedCountry.CoreEnchantedCountry.GameRule.RiskPoints;
using Core.EnchantedCountry.CoreEnchantedCountry.GameRule.Weapon;
using Core.EnchantedCountry.MonoBehaviourScripts.GameSaveSystem;
using Core.EnchantedCountry.ScriptableObject.ProductObject;
using Core.EnchantedCountry.ScriptableObject.Storage;
using Core.EnchantedCountry.SupportSystems.Data;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Core.EnchantedCountry.MonoBehaviourScripts.ScriptsForScenes.CreateCharacter {
  public class PlayerBuilder : MonoBehaviour {
    #region FIELDS
    [FormerlySerializedAs("_storageSO"),SerializeField]
    private StorageSO _storageSo;
    [SerializeField]
    private PlayerCharacter _playerCharacter;
    [SerializeField]
    private Button _createPlayer;
    // ReSharper disable once Unity.RedundantSerializeFieldAttribute
    [SerializeField]
    // ReSharper disable once NotAccessedField.Local
    private EquipmentsOfCharacterDataHandler _equipmentsOfCharacterDataHandler;
    [SerializeField]
    private bool _buildOnStart;
    #endregion
    
    #region MONOBEGAVIOR_METHDOS
    private void Start() {
      if (_buildOnStart) {
       Invoke(nameof (BuildPlayer), 0.5f);
      }
    }

    private void OnEnable() {
      AddListeners();
    }

    private void OnDisable() {
      RemoveListeners();
    }
    #endregion

    #region HANDLERS
    private void AddListeners() {
      _createPlayer.onClick.AddListener(BuildPlayer);
    }

    private void RemoveListeners() {
      _createPlayer.onClick.RemoveListener(BuildPlayer);
    }
    #endregion

    #region BUILDER
    public void BuildPlayer() {
      _equipmentsOfCharacterDataHandler = new EquipmentsOfCharacterDataHandler(GSSSingleton.Singleton);
      _playerCharacter = new PlayerCharacter(
        GetCharacterQualities(), 
        GetCharacterType(),
        GetLevels(),
        GetGamePoints(),
        GetRiskPoints(),
        GetWallet(),
        GetEquipmentsOfCharacter(),
        GetEquipmentsUsed(),
        GetArmor(),
        GetShield(),
        GetRangeWeapon(),
        GetMeleeWeapon(),
        GetProjectiles()
      );
    }

    private CharacterType GetCharacterType() {
      if (Enum.TryParse(GSSSingleton.Singleton.GetClassOfCharacterData().nameOfClass, out CharacterType characterType)) {
        return characterType;
      }
      return default;
    }

    private CharacterQualities GetCharacterQualities() {
       QualitiesData qualitiesData =  GSSSingleton.Singleton;
       CharacterQualities characterQualities = new CharacterQualities(Quality.QualityType.Strength, qualitiesData.strength, Quality.QualityType.Agility, qualitiesData.agility,
         Quality.QualityType.Constitution, qualitiesData.constitution, Quality.QualityType.Wisdom, qualitiesData.wisdom, Quality.QualityType.Courage, qualitiesData.courage);
       return characterQualities;
    }
    #endregion

    private GamePoints GetGamePoints() {
      GamePoints gamePoints = new GamePoints(GSSSingleton.Singleton.GetGamePointsData().Points);
      return gamePoints;
    }

    private Levels GetLevels() {
      Levels levels = new Levels(GSSSingleton.Singleton.GetGamePointsData().Points);
      return levels;
    }

    private RiskPoints GetRiskPoints() {
      RiskPoints riskPoints = new RiskPoints(GSSSingleton.Singleton.GetRiskPointsData().riskPoints);
      return riskPoints;
    }

    private Wallet GetWallet() {
      Wallet wallet = new Wallet(GSSSingleton.Singleton.GetWalletData().NumberOfCoins);
      return wallet;
    }

    private EquipmentsOfCharacter GetEquipmentsOfCharacter() {
      EquipmentsOfCharacter equipmentsOfCharacter = new EquipmentsOfCharacter(
        GSSSingleton.Singleton.GetEquipmentsOfCharacterData().EquipmentCards);
      return equipmentsOfCharacter;
    }

    private EquipmentsUsed GetEquipmentsUsed() {
      EquipmentUsedData equipmentUsedData = GSSSingleton.Singleton;
      EquipmentsUsed equipmentsUsed = new EquipmentsUsed(equipmentUsedData.armorId, equipmentUsedData.shieldId, equipmentUsedData.oneHandedId, equipmentUsedData.twoHandedId,
        equipmentUsedData.rangeId, equipmentUsedData.projectiliesId, equipmentUsedData.bagId, equipmentUsedData.animalId, equipmentUsedData.carriageId);
      return equipmentsUsed;
    }

    private Armor GetArmor() {
      EquipmentUsedData equipmentUsedData = GSSSingleton.Singleton;
      if (equipmentUsedData.armorId != 0) {
        ProductSO armorSo = _storageSo.GetArmorFromList(equipmentUsedData.armorId);
        Armor armor = armorSo.GetArmor();
        return armor;
      } 
      return null;
    }
    
    private Armor GetShield() {
      EquipmentUsedData equipmentUsedData = GSSSingleton.Singleton;
      if (equipmentUsedData.shieldId != 0) {
        ProductSO shieldSo = _storageSo.GetArmorFromList(equipmentUsedData.animalId);
        Armor shield = shieldSo.GetArmor();
        return shield;
      }
      return null;
    }
    
    private Weapon GetMeleeWeapon() {
      EquipmentUsedData equipmentUsedData = GSSSingleton.Singleton;
      if (equipmentUsedData.oneHandedId != 0) {
        ProductSO weaponSo = _storageSo.GetWeaponFromList(equipmentUsedData.oneHandedId);
        Weapon weapon = weaponSo.GetWeapon();
        return weapon;
      } 
      if (equipmentUsedData.twoHandedId != 0) {
        ProductSO weaponSo = _storageSo.GetWeaponFromList(equipmentUsedData.twoHandedId);
        Weapon weapon = weaponSo.GetWeapon();
        return weapon;
      }
      return null;
    }
    
    private Weapon GetRangeWeapon() {
      EquipmentUsedData equipmentUsedData = GSSSingleton.Singleton;
      if (equipmentUsedData.rangeId != 0) {
        ProductSO weaponSo = _storageSo.GetWeaponFromList(equipmentUsedData.rangeId);
        Weapon weapon = weaponSo.GetWeapon();
        return weapon;
      } 
      return null;
    }
    
    private Weapon GetProjectiles() {
      EquipmentUsedData equipmentUsedData = GSSSingleton.Singleton;
      if (equipmentUsedData.projectiliesId != 0) {
        ProductSO weaponSo = _storageSo.GetWeaponFromList(equipmentUsedData.projectiliesId);
        Weapon weapon = weaponSo.GetWeapon();
        return weapon;
      } 
      return null;
    }

    #region PROPERTIES
    public PlayerCharacter PlayerCharacter {
      get {
        return _playerCharacter;
      }
    }
    #endregion
  }

  public class Builder {
    #region FIELDS
    private const string STORAGE_PATH = "ForBuilder/Storage";
    private readonly StorageSO _storageSo;
    private PlayerCharacter _playerCharacter;
    #endregion

    #region CONSTRUCTOR
    public Builder() {
      _storageSo = Resources.Load<StorageSO>(STORAGE_PATH);
    }
    #endregion
    #region BUILDER
    public PlayerCharacter BuildPlayer() {
      return new PlayerCharacter(
        GetCharacterQualities(), 
        GetCharacterType(),
        GetLevels(),
        GetGamePoints(),
        GetRiskPoints(),
        GetWallet(),
        GetEquipmentsOfCharacter(),
        GetEquipmentsUsed(),
        GetArmor(),
        GetShield(),
        GetRangeWeapon(),
        GetMeleeWeapon(),
        GetProjectiles()
      );
    }

    private CharacterType GetCharacterType() {
      if (Enum.TryParse(GSSSingleton.Singleton.GetClassOfCharacterData().nameOfClass, out CharacterType characterType)) {
        return characterType;
      }
      return default;
    }

    private CharacterQualities GetCharacterQualities() {
       QualitiesData qualitiesData =  GSSSingleton.Singleton;
       CharacterQualities characterQualities = new CharacterQualities(Quality.QualityType.Strength, qualitiesData.strength, Quality.QualityType.Agility, qualitiesData.agility,
         Quality.QualityType.Constitution, qualitiesData.constitution, Quality.QualityType.Wisdom, qualitiesData.wisdom, Quality.QualityType.Courage, qualitiesData.courage);
       return characterQualities;
    }
    #endregion

    private GamePoints GetGamePoints() {
      GamePoints gamePoints = new GamePoints(GSSSingleton.Singleton.GetGamePointsData().Points);
      return gamePoints;
    }

    private Levels GetLevels() {
      Levels levels = new Levels(GSSSingleton.Singleton.GetGamePointsData().Points);
      return levels;
    }

    private RiskPoints GetRiskPoints() {
      RiskPoints riskPoints = new RiskPoints(GSSSingleton.Singleton.GetRiskPointsData().riskPoints);
      return riskPoints;
    }

    private Wallet GetWallet() {
      Wallet wallet = new Wallet(GSSSingleton.Singleton.GetWalletData().NumberOfCoins);
      return wallet;
    }

    private EquipmentsOfCharacter GetEquipmentsOfCharacter() {
      EquipmentsOfCharacter equipmentsOfCharacter = new EquipmentsOfCharacter(
        GSSSingleton.Singleton.GetEquipmentsOfCharacterData().EquipmentCards);
      return equipmentsOfCharacter;
    }

    private EquipmentsUsed GetEquipmentsUsed() {
      EquipmentUsedData equipmentUsedData = GSSSingleton.Singleton;
      EquipmentsUsed equipmentsUsed = new EquipmentsUsed(equipmentUsedData.armorId, equipmentUsedData.shieldId, equipmentUsedData.oneHandedId, equipmentUsedData.twoHandedId,
        equipmentUsedData.rangeId, equipmentUsedData.projectiliesId, equipmentUsedData.bagId, equipmentUsedData.animalId, equipmentUsedData.carriageId);
      return equipmentsUsed;
    }

    private Armor GetArmor() {
      EquipmentUsedData equipmentUsedData = GSSSingleton.Singleton;
      if (equipmentUsedData.armorId != 0) {
        ProductSO armorSo = _storageSo.GetArmorFromList(equipmentUsedData.armorId);
        Armor armor = armorSo.GetArmor();
        return armor;
      } 
      return null;
    }
    
    private Armor GetShield() {
      EquipmentUsedData equipmentUsedData = GSSSingleton.Singleton;
      if (equipmentUsedData.shieldId != 0) {
        ProductSO shieldSo = _storageSo.GetArmorFromList(equipmentUsedData.animalId);
        Armor shield = shieldSo.GetArmor();
        return shield;
      }
      return null;
    }
    
    private Weapon GetMeleeWeapon() {
      EquipmentUsedData equipmentUsedData = GSSSingleton.Singleton;
      if (equipmentUsedData.oneHandedId != 0) {
        ProductSO weaponSo = _storageSo.GetWeaponFromList(equipmentUsedData.oneHandedId);
        Weapon weapon = weaponSo.GetWeapon();
        return weapon;
      } 
      if (equipmentUsedData.twoHandedId != 0) {
        ProductSO weaponSo = _storageSo.GetWeaponFromList(equipmentUsedData.twoHandedId);
        Weapon weapon = weaponSo.GetWeapon();
        return weapon;
      }
      return null;
    }
    
    private Weapon GetRangeWeapon() {
      EquipmentUsedData equipmentUsedData = GSSSingleton.Singleton;
      if (equipmentUsedData.rangeId != 0) {
        ProductSO weaponSo = _storageSo.GetWeaponFromList(equipmentUsedData.rangeId);
        Weapon weapon = weaponSo.GetWeapon();
        return weapon;
      } 
      return null;
    }
    
    private Weapon GetProjectiles() {
      EquipmentUsedData equipmentUsedData = GSSSingleton.Singleton;
      if (equipmentUsedData.projectiliesId != 0) {
        ProductSO weaponSo = _storageSo.GetWeaponFromList(equipmentUsedData.projectiliesId);
        Weapon weapon = weaponSo.GetWeapon();
        return weapon;
      } 
      return null;
    }

    #region PROPERTIES
    public PlayerCharacter PlayerCharacter {
      get {
        return _playerCharacter;
      }
    }
    #endregion
  }
}