using System.Collections.Generic;
using Core.Mono.Scenes.QualityDiceRoll;
using Core.Rule.Character;
using Core.Rule.GameRule.NPC;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Mono.Scenes.Fight {
  public class CardPresenter : MonoBehaviour {
    [SerializeField]
    private GameObject _cardPrefab;
    [SerializeField]
    private Transform _anchorForPlayer;
    [SerializeField]
    private Transform _anchorForNpc;
    [SerializeField]
    private PlayerBuilder _playerBuilder;
    [SerializeField]
    private NpcBuilder _npcBuilder;
    [SerializeField]
    private Button _createCards;
    [SerializeField]
    private List<Sprite> _sprites;
    [SerializeField]
    private List<CardView> _cardViews;

    private List<PlayerCharacter> _playerCharacterList;
    private List<Npc> _npcList;
    private PlayerCharacter _playerCharacter;
    private Npc _npc;

    private void OnEnable() {
      AddListeners();
    }

    private void OnDisable() {
      RemoveListeners();
    }

    private void AddListeners() {
      _createCards.onClick.AddListener(SpawnCards);
    }

    private void RemoveListeners() {
      _createCards.onClick.RemoveListener(SpawnCards);
    }
    
    public void SpawnCards() {
      _playerCharacter = _playerBuilder.PlayerCharacter;
      _npc = _npcBuilder.Npc;
      _cardViews = new List<CardView>();
      GameObject cardPlayer = Instantiate(_cardPrefab, _anchorForPlayer);
      _cardViews.Add(cardPlayer.GetComponent<CardView>());
      _cardViews[0].SetFieldsInCard(_sprites[0], _playerCharacter.Name, _playerCharacter.RiskPoints.GetPoints(), _playerCharacter.Armor.ArmorClass.ClassOfArmor);
      GameObject cardNpc = Instantiate(_cardPrefab, _anchorForNpc);
      _cardViews.Add(cardNpc.GetComponent<CardView>());
      _cardViews[1].SetFieldsInCard(_sprites[1], _npc.Name, _npc.RiskPoints.GetPoints(), _npc.ArmorClass.ClassOfArmor);
    }
  }
}