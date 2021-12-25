using Core.Rule.Character;
using Core.Rule.GameRule.NPC;
using NUnit.Framework;
using Zenject;

namespace Editor.NTest.EditorTests.ImpactTest {
  [TestFixture, Author("Hleb Cheliakh", "4elyah@gmail.com"), Category("Combat"), TestOf("Impact")]
  public class ImpactTest : ZenjectUnitTestFixture {
    #region PREPARE_FOR_TEST
    private PlayerCharacter _player;
    private Npc _npc;
    #endregion

    [SetUp]
    public void InitializeFields() {
      Container.Bind<PlayerCharacter>().AsSingle();

    }

    [TearDown]
    public void DeleteFields() {
      
    }
    [Test]
    public void RunTest1() {
      
      // TODO
    }
  }
}