using System;
using JetBrains.Annotations;

namespace Core.Main.NonPlayerCharacters {
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