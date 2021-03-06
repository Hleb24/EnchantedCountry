using JetBrains.Annotations;

namespace Core.Main.NonPlayerCharacters.Model {
  public class NpcMetadata {
    private readonly string _name;
    private int _id;
    private string _description;
    private string _property;
    private Alignment _alignment;
    private NpcType _npcType;
    private int _experience;

    public NpcMetadata([NotNull] NpcMetadataModel model) {
      _id = model.Id;
      _name = model.Name;
      _description = model.Description;
      _property = model.Property;
      _alignment = model.Alignment;
      _npcType = model.NpcType;
      _experience = model.Experience;
    }

    public string GetName() {
      return _name;
    }
  }
}