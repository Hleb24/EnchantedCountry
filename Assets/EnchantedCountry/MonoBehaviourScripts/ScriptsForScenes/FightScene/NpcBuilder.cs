using Core.EnchantedCountry.CoreEnchantedCountry.GameRule.NPC;
using Core.EnchantedCountry.ScriptableObject.SetOfNpc;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Core.EnchantedCountry.MonoBehaviourScripts.ScriptsForScenes.FightScene {
  public class NpcBuilder : MonoBehaviour {
    #region FIELDS
    [FormerlySerializedAs("_setOfNpcSO"),SerializeField]
    private SetOfNpcSO _setOfNpcSo;
    [SerializeField]
    private Button _createNpc;
    [SerializeField]
    private int _npcId;
    [SerializeField]
    private bool _buildOnStart;
    private Npc _npc;
    #endregion

    #region MONOBEHAVIOR_METHODS
    private void Start() {
      if (_buildOnStart) {
        BuildNpc();
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
      _createNpc.onClick.AddListener(BuildNpc);
    }

    private void RemoveListeners() {
      _createNpc.onClick.RemoveListener(BuildNpc);
    }
    #endregion

    #region GET_NPC
    private void BuildNpc() {
      _npc = _setOfNpcSo.GetNpcFromList(_npcId);
    }

    private void BuildNpc(int id) {
      _npc = _setOfNpcSo.GetNpcFromList(id);
    }
    #endregion
    
    #region PROPERTIES
    public Npc Npc {
      get {
        return _npc;
      }
    }
    #endregion
  }
}