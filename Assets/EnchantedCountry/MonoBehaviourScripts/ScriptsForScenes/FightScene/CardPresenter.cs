using System.Collections.Generic;
using Core.EnchantedCountry.CoreEnchantedCountry.Character;
using Core.EnchantedCountry.CoreEnchantedCountry.GameRule.NPC;
using Core.EnchantedCountry.MonoBehaviourScripts.ScriptsForScenes.CreateCharacter;
using UnityEngine;
using UnityEngine.UI;

namespace Core.EnchantedCountry.MonoBehaviourScripts.ScriptsForScenes.FightScene {
  public class CardPresenter : MonoBehaviour {
    #region FIELDS
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
    #endregion

    #region MONBEHAVIOUR_METHODS
    private void OnEnable() {
      AddListeners();
    }

    private void OnDisable() {
      RemoveListeners();
    }
    #endregion

    #region HANDLERS
    private void AddListeners() {
      _createCards.onClick.AddListener(SpawnCards);
    }

    private void RemoveListeners() {
      _createCards.onClick.RemoveListener(SpawnCards);
    }
    #endregion
    
    #region SPAWN_CARDS
    public void SpawnCards() {
      _playerCharacter = _playerBuilder.PlayerCharacter;
      _npc = _npcBuilder.Npc;
      _cardViews = new List<CardView>();
      GameObject cardPlayer = Instantiate(_cardPrefab, _anchorForPlayer);
      _cardViews.Add(cardPlayer.GetComponent<CardView>());
      _cardViews[0].SetFieldsInCard(_sprites[0], _playerCharacter.Name, _playerCharacter.RiskPoints.Points, _playerCharacter.Armor.ArmorClass.ClassOfArmor);
      GameObject cardNpc = Instantiate(_cardPrefab, _anchorForNpc);
      _cardViews.Add(cardNpc.GetComponent<CardView>());
      _cardViews[1].SetFieldsInCard(_sprites[1], _npc.Name, _npc.RiskPoints.Points, _npc.ArmorClass.ClassOfArmor);
    }
    #endregion
  }
}