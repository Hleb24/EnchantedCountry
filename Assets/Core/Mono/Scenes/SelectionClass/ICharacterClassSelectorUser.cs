namespace Core.Mono.Scenes.SelectionClass {
  public interface ICharacterClassSelectorUser {
    public CharacterClassSelector ClassSelector { get; }
    public void SetSelector(CharacterClassSelector characterClassSelector);
  }
}