using System;
using Core.Main.Character;
using Core.Main.Dice;
using Core.Main.GameRule.Initiative;
using Core.Main.NonPlayerCharacters;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Mono.Scenes.Fight {
  public class CombatController : MonoBehaviour {
    public static event Action<string, float> AttackButtonClicked;
    [SerializeField]
    private Button _attack;
    [SerializeField]
    private InitiativeController _initiativeController;
    private PlayerCharacter _playerCharacter;
    private NonPlayerCharacter _nonPlayerCharacter;
    private bool _isGetReference;

    private void OnEnable() {
      AddListeners();
    }

    private void OnDisable() {
      RemoveListeners();
    }

    private void AddListeners() {
      _initiativeController.InitiativeDiceRollComplete += OnInitiativeDiceRollComplete;
      _attack.onClick.AddListener(OnAttackButtonClicked);
    }

    private void OnInitiativeDiceRollComplete() {
      _playerCharacter = _initiativeController.PlayerCharacter;
      _nonPlayerCharacter = _initiativeController.NonPlayerCharacter;
    }

    private void RemoveListeners() {
      _initiativeController.InitiativeDiceRollComplete -= OnInitiativeDiceRollComplete;
      _attack.onClick.RemoveListener(OnAttackButtonClicked);
    }

    private int GetDiceRollValue(int accuracy = 0) {
      return KitOfDice.diceKit[KitOfDice.SetWithOneTwelveSidedAndOneSixSidedDice].SumRollsOfDice() + accuracy;
    }

    private bool IsPlayerCharacter(IInitiative initiative) {
      return _playerCharacter == initiative;
    }

    private bool IsNpc(IInitiative initiative) {
      return _nonPlayerCharacter == initiative;
    }

    private void OnAttackButtonClicked() {
      if (IsPlayerCharacter(_initiativeController.GetIInitiative())) {
        if (_playerCharacter.MeleeWeapon != null) {
          int diceRollValue = GetDiceRollValue(_playerCharacter.GetMeleeAccuracy());
          float meleeDamage = _playerCharacter.GetMeleeDamage();
          Debug.Log($"<color=green>{_playerCharacter.ClassType}</color>, dice roll value <color=green>{diceRollValue}</color> to melee <color=green>{meleeDamage}</color> damage, weapont type <color=green>{_playerCharacter.MeleeWeapon.weaponType}</color>");
          _nonPlayerCharacter.GetDamaged(diceRollValue, meleeDamage,
            _playerCharacter.MeleeWeapon.Id, _playerCharacter.MeleeWeapon.weaponType);
          AttackButtonClicked?.Invoke(_nonPlayerCharacter.Name, _nonPlayerCharacter.RiskPoints.GetPoints());
        }
      }
      if (IsNpc(_initiativeController.GetIInitiative())) {
        if (_nonPlayerCharacter.NumberOfAttack !=0) {
          if (_nonPlayerCharacter.AttackEveryoneAtOnce) {
            Debug.Log("Everyone at once <color=red>Npc</color> attack");
            for (int i = 0; i < _nonPlayerCharacter.NumberOfAttack; i++) {
              int index = i;
              int diceRollValue = GetDiceRollValue(_nonPlayerCharacter.Accuracy());
              float damage = _nonPlayerCharacter.ToDamage(diceRollValue,_playerCharacter,index);
              Debug.Log($"<color=red>{_nonPlayerCharacter.Name}</color>, dice roll value <color=red>{diceRollValue}</color> to index <size=12>{index}</size> - <color=red>{damage}</color> damage");
              _playerCharacter.GetDamaged(diceRollValue, damage);
              AttackButtonClicked?.Invoke(_playerCharacter.Name, _playerCharacter.RiskPoints.GetPoints());
            }
          }

          if (!_nonPlayerCharacter.AttackEveryoneAtOnce) {
            Debug.Log("One <color=red>Npc</color> attack");
            int weapon = UnityEngine.Random.Range(0, _nonPlayerCharacter.NumberOfWeapon);
            int diceRollValue = GetDiceRollValue(_nonPlayerCharacter.Accuracy());
            float damage = _nonPlayerCharacter.ToDamage(diceRollValue,_playerCharacter,weapon);
            Debug.Log($"<color=red>{_nonPlayerCharacter.Name}</color>, dice roll value <color=red>{diceRollValue}</color> to weapon <color=red>{damage}</color> damage");
            _playerCharacter.GetDamaged(diceRollValue, damage);
            AttackButtonClicked?.Invoke(_playerCharacter.Name, _playerCharacter.RiskPoints.GetPoints());
          }
        }
      }
      _initiativeController.MoveIndexToBack();
     
    }
  }
}