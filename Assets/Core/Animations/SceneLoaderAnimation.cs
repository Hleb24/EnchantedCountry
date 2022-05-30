using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Core.Animations {
  public class SceneLoaderAnimation : MonoBehaviour {
    private const string TRIGGER_START = "Start";
    [SerializeField]
    private Animator _transition;
    [SerializeField, ReadOnly]
    private int _transitionTime = 1000;
    private static readonly int Start = Animator.StringToHash(TRIGGER_START);

    public async UniTask StartTransitionAnimation() {
      _transition.SetTrigger(Start);
      await UniTask.Delay(_transitionTime);
    }
  }
}