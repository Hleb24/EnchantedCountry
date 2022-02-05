using Core.Main.NonPlayerCharacters;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Core.Mono.Scenes.Fight {
  public class NpcHolder : MonoBehaviour {
    [SerializeField]
    private Button _createNpc;
    [SerializeField]
    private int _npcId;
    [SerializeField]
    private bool _buildOnStart;
    private NpcBuilder _npcBuilder;
    
    [Inject]
    public void Constructor(NpcBuilder builder) {
      _npcBuilder = builder;
    }

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

    private void AddListeners() {
      _createNpc.onClick.AddListener(BuildNpc);
    }

    private void RemoveListeners() {
      _createNpc.onClick.RemoveListener(BuildNpc);
    }

    private void BuildNpc() {
      NonPlayerCharacter = _npcBuilder.Build(_npcId);
      
    }

   

    public NonPlayerCharacter NonPlayerCharacter { get; private set; }
  }
}