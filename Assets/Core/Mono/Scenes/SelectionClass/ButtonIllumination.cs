using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Core.Mono.Scenes.SelectionClass {
  public class ButtonIllumination : MonoBehaviour {
    private void ChangeColorForPressedButton() { }
    [Header("Set in Inspector"), SerializeField]
    private List<Button> _buttons;
    [FormerlySerializedAs("_colorOfChoise"),SerializeField]
    private Color _colorOfChoice;
    [SerializeField]
    private Color _defaultColor;
  }
}