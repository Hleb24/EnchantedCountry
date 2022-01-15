using System.Threading.Tasks;
using Core.Mono.MainManagers;
using Core.Rule.Character;
using Core.Rule.Character.Equipment;
using Core.Rule.Character.Levels;
using Core.Rule.Character.Qualities;
using Core.Rule.GameRule;
using Core.Rule.GameRule.EquipmentIdConstants;
using Core.Rule.GameRule.RiskPoints;
using Core.ScriptableObject.Products;
using Core.ScriptableObject.Storage;
using Core.Support.Data;
using Core.Support.SaveSystem.SaveManagers;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

namespace Core.Mono.Scenes.QualityDiceRoll {
  public class PlayerBuilder : MonoBehaviour {
    [FormerlySerializedAs("_storageSo"), FormerlySerializedAs("_storageSO"), SerializeField]
    private StorageObject _storageObject;
    [SerializeField]
    private PlayerCharacter _playerCharacter;
    [SerializeField]
    private Button _createPlayer;
    // ReSharper disable once Unity.RedundantSerializeFieldAttribute
    [SerializeField]
    // ReSharper disable once NotAccessedField.Local
    private IEquipment _equipments;
    [SerializeField]
    private bool _buildOnStart;
    [Inject]
    private IStartGame _startGame;
    private IDealer _dealer;
    private IEquipmentUsed _equipmentUsed;
    private IGamePoints _gamePoints;
    private IRiskPoints _riskPoints;
    private IClassType _type;

    [Inject]
    private void InjectDealer(IDealer dealer) {
      _dealer = dealer;
    }
    private void Start() {
      WaitLoad();
    }

    private void OnEnable() {
      AddListeners();
    }

    private void OnDisable() {
      RemoveListeners();
    }

    public void BuildPlayer() {
      _equipments = _dealer.Peek<IEquipment>();
      _equipmentUsed = _dealer.Peek<IEquipmentUsed>();
      _playerCharacter = new PlayerCharacter(GetCharacterQualities(), GetCharacterType(), GetLevels(), GetGamePoints(), GetRiskPoints(), GetWallet(), GetEquipmentsOfCharacter(),
        GetEquipmentsUsed(), GetArmor(), GetShield(), GetRangeWeapon(), GetMeleeWeapon(), GetProjectiles());
    }

    private async void WaitLoad() {
      while (_startGame.StillInitializing()) {
        await Task.Yield();
      }

      _gamePoints = _dealer.Peek<IGamePoints>();
      _riskPoints = _dealer.Peek<IRiskPoints>();
      _type = _dealer.Peek<IClassType>();
      if (_buildOnStart) {
        Invoke(nameof(BuildPlayer), 0.5f);
      }
    }

    private IGamePoints GetGamePoints() {
      return _gamePoints;
    }

    private Levels GetLevels() {
      var levels = new Levels(_gamePoints.GetPoints());
      return levels;
    }

    private RiskPoints GetRiskPoints() {
      return new RiskPoints(_riskPoints);
    }

    private IWallet GetWallet() {
      return _dealer.Peek<IWallet>();
    }

    private EquipmentsOfCharacter GetEquipmentsOfCharacter() {
      var equipmentsOfCharacter = new EquipmentsOfCharacter(_equipments.GetEquipmentCards());
      return equipmentsOfCharacter;
    }

    private IEquipmentUsed GetEquipmentsUsed() {
      return _equipmentUsed;
    }

    private Armor GetArmor() {
      if (_equipmentUsed.GetEquipment(EquipmentsUsedId.ArmorId) != 0) {
        ProductObject armorObject = _storageObject.GetArmorFromList(_equipmentUsed.GetEquipment(EquipmentsUsedId.ArmorId));
        Armor armor = armorObject.GetArmor();
        return armor;
      }

      ProductObject noArmorProduct = _storageObject.GetArmorFromList(EquipmentIdConstants.NO_ARMOR_ID);
      Armor noArmor = noArmorProduct.GetArmor();
      return noArmor;
    }

    private Armor GetShield() {
      if (_equipmentUsed.GetEquipment(EquipmentsUsedId.ShieldId) != 0) {
        ProductObject shieldObject = _storageObject.GetArmorFromList(_equipmentUsed.GetEquipment(EquipmentsUsedId.ShieldId));
        Armor shield = shieldObject.GetArmor();
        return shield;
      }

      return null;
    }

    private Weapon GetMeleeWeapon() {
      if (_equipmentUsed.GetEquipment(EquipmentsUsedId.OneHandedId) != 0) {
        ProductObject weaponObject = _storageObject.GetWeaponFromList(_equipmentUsed.GetEquipment(EquipmentsUsedId.OneHandedId));
        Weapon weapon = weaponObject.GetWeapon();
        return weapon;
      }

      if (_equipmentUsed.GetEquipment(EquipmentsUsedId.TwoHandedId) != 0) {
        ProductObject weaponObject = _storageObject.GetWeaponFromList(_equipmentUsed.GetEquipment(EquipmentsUsedId.TwoHandedId));
        Weapon weapon = weaponObject.GetWeapon();
        return weapon;
      }

      return null;
    }

    private Weapon GetRangeWeapon() {
      if (_equipmentUsed.GetEquipment(EquipmentsUsedId.RangeId) != 0) {
        ProductObject weaponObject = _storageObject.GetWeaponFromList(_equipmentUsed.GetEquipment(EquipmentsUsedId.RangeId));
        Weapon weapon = weaponObject.GetWeapon();
        return weapon;
      }

      return null;
    }

    private Weapon GetProjectiles() {
      if (_equipmentUsed.GetEquipment(EquipmentsUsedId.ProjectilesId) != 0) {
        ProductObject weaponObject = _storageObject.GetProjectilesFromList(_equipmentUsed.GetEquipment(EquipmentsUsedId.ProjectilesId));
        Weapon weapon = weaponObject.GetWeapon();
        return weapon;
      }

      return null;
    }

    private void AddListeners() {
      _createPlayer.onClick.AddListener(BuildPlayer);
    }

    private void RemoveListeners() {
      _createPlayer.onClick.RemoveListener(BuildPlayer);
    }

    private ClassType GetCharacterType() {
      return _type.GetClassType();
    }

    private CharacterQualities GetCharacterQualities() {
      IQualityPoints qualityPoints = _dealer.Peek<IQualityPoints>();
      var characterQualities = new CharacterQualities(QualityType.Strength, qualityPoints.GetQualityPoints(QualityType.Strength), QualityType.Agility,
        qualityPoints.GetQualityPoints(QualityType.Agility), QualityType.Constitution, qualityPoints.GetQualityPoints(QualityType.Constitution), QualityType.Wisdom,
        qualityPoints.GetQualityPoints(QualityType.Wisdom), QualityType.Courage, qualityPoints.GetQualityPoints(QualityType.Courage));
      return characterQualities;
    }

    public PlayerCharacter PlayerCharacter {
      get {
        return _playerCharacter;
      }
    }
  }
}