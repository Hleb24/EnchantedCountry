using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Core.Animations {
  public class LevelLoaderAnimation : MonoBehaviour {
    [SerializeField]
    private Animator _transition;
    [SerializeField]
    private int _transitionTime = 1000;

    public async UniTask StartTransitionAnimation() {
      _transition.SetTrigger("Start");
      await UniTask.Delay(_transitionTime);
    }
  }
}