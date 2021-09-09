using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.EnchantedCountry.MonoBehaviourScripts.ScriptsForScenes.CharacterList {
  public class ProgressViewInCharacterList : MonoBehaviour {
    #region FIELDS
    [SerializeField]
    private TMP_Text _level;
    [SerializeField]
    private TMP_Text _numberOfGamePoints;
    [SerializeField]
    private TMP_Text _spellLevel;
    [SerializeField]
    private Image _background;
    private bool _isCurrentLevel;
    #endregion
    #region SET_FIELDS
    public void SetTextFields(int level, int gamePoints, int spellLevel, bool isCurrentLevel = false) {
      SetLevelText(level);
      SetGamePointsText(gamePoints);
      SetSpellLevelText(spellLevel);
      SetColorForBackground(isCurrentLevel);
    }
    public void SetTextFields(int level, int gamePoints, bool isCurrentLevel = false) {
      SetLevelText(level);
      SetGamePointsText(gamePoints);
      SetColorForBackground(isCurrentLevel);
    }
    
    private void SetGamePointsText(int gamePoints) {
      _numberOfGamePoints.text = gamePoints.ToString();
    }

    private void SetLevelText(int level) {
      _level.text = level.ToString();
    }

    private void SetSpellLevelText(int spellLevel) {
      _spellLevel.text = spellLevel.ToString();
    }

    private void SetColorForBackground(bool isCurrentLevel) {
      _isCurrentLevel = isCurrentLevel;
      if (_isCurrentLevel) {
        SetGreenColorForBackground();
      }
    }

    private void SetGreenColorForBackground() {
      _background.color = Color.green;
    }
    #endregion    
  }
}