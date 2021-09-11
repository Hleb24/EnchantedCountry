using System;
using Core.EnchantedCountry.CoreEnchantedCountry.Character;
using Core.EnchantedCountry.CoreEnchantedCountry.Dice;
using Core.EnchantedCountry.CoreEnchantedCountry.GameRule.Initiative;
using Core.EnchantedCountry.CoreEnchantedCountry.GameRule.NPC;
using UnityEngine;
using UnityEngine.UI;

namespace Core.EnchantedCountry.MonoBehaviourScripts.ScriptsForScenes.FightScene {
  public class CombatController : MonoBehaviour {
    public static event Action<string, float> AttackButtonClicked;
    #region FIELDS
    [SerializeField]
    private Button _attack;
    [SerializeField]
    private InitiativeController _initiativeController;
    private PlayerCharacter _playerCharacter;
    private Npc _npc;
    private bool _isGetReference;
    #endregion

    #region MONOBEHAVIOR_METHODS
    private void OnEnable() {
      AddListeners();
    }

    private void OnDisable() {
      RemoveListeners();
    }
    #endregion

    #region HANDLERS
    private void AddListeners() {
      _initiativeController.InitiativeDiceRollComplete += OnInitiativeDiceRollComplete;
      _attack.onClick.AddListener(OnAttackButtonClicked);
    }

    private void OnInitiativeDiceRollComplete() {
      _playerCharacter = _initiativeController.PlayerCharacter;
      _npc = _initiativeController.Npc;
    }

    private void RemoveListeners() {
      _initiativeController.InitiativeDiceRollComplete -= OnInitiativeDiceRollComplete;
      _attack.onClick.RemoveListener(OnAttackButtonClicked);
    }
    #endregion

    private int GetDiceRollValue(int accuracy = 0) {
      return KitOfDice.diceKit[KitOfDice.SetWithOneTwelveSidedAndOneSixSidedDice].SumRollsOfDice() + accuracy;
    }

    private bool IsPlayerCharacter(IInitiative initiative) {
      return _playerCharacter == initiative;
    }

    private bool IsNpc(IInitiative initiative) {
      return _npc == initiative;
    }

    private void OnAttackButtonClicked() {
      if (IsPlayerCharacter(_initiativeController.GetIInitiative())) {
        if (_playerCharacter.MeleeWeapon != null) {
          int diceRollValue = GetDiceRollValue(_playerCharacter.GetMeleeAccuracy());
          float meleeDamage = _playerCharacter.GetMeleeDamage();
          Debug.Log($"<color=green>{_playerCharacter.CharacterType}</color>, dice roll value <color=green>{diceRollValue}</color> to melee <color=green>{meleeDamage}</color> damage, weapont type <color=green>{_playerCharacter.MeleeWeapon.weaponType}</color>");
          _npc.GetDamaged(diceRollValue, meleeDamage,
            _playerCharacter.MeleeWeapon.Id, _playerCharacter.MeleeWeapon.weaponType);
          AttackButtonClicked?.Invoke(_npc.Name, _npc.RiskPoints.Points);
        }
      }
      if (IsNpc(_initiativeController.GetIInitiative())) {
        if (_npc.NumberOfAttack !=0) {
          if (_npc.AttackEveryoneAtOnce) {
            Debug.Log("Everyone at once <color=red>Npc</color> attack");
            for (int i = 0; i < _npc.NumberOfAttack; i++) {
              int index = i;
              int diceRollValue = GetDiceRollValue(_npc.Accuracy());
              float damage = _npc.ToDamage(diceRollValue,_playerCharacter,index);
              Debug.Log($"<color=red>{_npc.Name}</color>, dice roll value <color=red>{diceRollValue}</color> to index <size=12>{index}</size> - <color=red>{damage}</color> damage");
              _playerCharacter.GetDamaged(diceRollValue, damage);
              AttackButtonClicked?.Invoke(_playerCharacter.Name, _playerCharacter.RiskPoints.Points);
            }
          }

          if (!_npc.AttackEveryoneAtOnce) {
            Debug.Log("One <color=red>Npc</color> attack");
            int weapon = UnityEngine.Random.Range(0, _npc.NumberOfWeapon);
            int diceRollValue = GetDiceRollValue(_npc.Accuracy());
            float damage = _npc.ToDamage(diceRollValue,_playerCharacter,weapon);
            Debug.Log($"<color=red>{_npc.Name}</color>, dice roll value <color=red>{diceRollValue}</color> to weapon <color=red>{damage}</color> damage");
            _playerCharacter.GetDamaged(diceRollValue, damage);
            AttackButtonClicked?.Invoke(_playerCharacter.Name, _playerCharacter.RiskPoints.Points);
          }
        }
      }
      _initiativeController.MoveIndexToBack();
     
    }
  }
}