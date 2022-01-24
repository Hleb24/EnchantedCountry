using Core.Main.Character.Levels;
using NUnit.Framework;

namespace Editor.NTest.PlayModeTests.CharacterTests.LevelsTest {
  [Author("Hleb Cheliakh", "4elyah@gmail.com"), Category("Character"), TestOf("Levels")]
  public class LevelsTest {
    #region Tests
    [Test, Description("Level at least 0"), Repeat(1)]
    public void LevelAtLeastZero([Values(0, 1, 2, 3, 4, 5)] int level) {
      Assert.That(_levels.Level, Is.EqualTo(0));
      _levels.Level = level;
      Assert.That(_levels.Level, Is.AtLeast(0));
    }
    #endregion

    #region Preparation_for_Tests
    private Levels _levels;

    [SetUp]
    public void InitFields() {
      _levels = new Levels();
    }

    [TearDown]
    public void DeleteFields() {
      _levels = null;
    }
    #endregion
  }
}