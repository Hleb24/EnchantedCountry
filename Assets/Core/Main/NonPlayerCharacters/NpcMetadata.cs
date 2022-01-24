using System;
using JetBrains.Annotations;

namespace Core.Main.NonPlayerCharacters {
  public class NpcMetadata {
    private int _id;
    private string _name;
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

  public class NpcMetadataModel {
    public NpcMetadataModel(int id, [NotNull] string name, [NotNull] string description, [NotNull] string property, int experience, Alignment alignment, NpcType npcType) {
      Id = id >= 0 ? id : throw new ArgumentException($"{nameof(id)} должен быть больше нуля");
      Name = name ?? throw new ArgumentNullException(nameof(name));
      Description = description ?? throw new ArgumentNullException(nameof(description));
      Property = property ?? throw new ArgumentNullException(nameof(property));
      Experience = experience >= 0 ? id : throw new ArgumentException($"{nameof(experience)} должен быть больше нуля");
      Alignment = alignment;
      NpcType = npcType;
    }

    public int Id { get; }

    public string Name { get; }

    public string Description { get; }

    public string Property { get; }

    public int Experience { get; }

    public Alignment Alignment { get; }
    public NpcType NpcType { get; }
  }
}