using Core.EnchantedCountry.MonoBehaviourScripts.ScriptsForScenes.CreateCharacter;
using NUnit.Framework;
using UnityEngine;

namespace Editor.NTest.PlayModeTests.MonoBehaviourTests.CreateCharacterScene {
  [Author("Hleb Cheliakh", "4elyah@gmail.com"), Category("CreateCharacter"), TestOf("DiceRoll")]
  public class DiceRollTest {
    #region Preparation_for_Tests
    private const string PATH_BY_DICE_ROLL = "ForTests/CreateCharacter/DiceRoll";
    private const string PATH_BY_BUTTON_DICE_ROLL = "ForTests/CreateCharacter/Button_DiceRoll";
    private DiceRoll _diceRoll;

    [SetUp]
    public void InitFields() {
      _diceRoll = Object.Instantiate(Resources.Load<DiceRoll>(PATH_BY_DICE_ROLL));
    }

    [TearDown]
    public void DeleteFields() {
      Object.Destroy(_diceRoll.gameObject);
    }
    #endregion

    #region Tests
    #endregion
  }
}