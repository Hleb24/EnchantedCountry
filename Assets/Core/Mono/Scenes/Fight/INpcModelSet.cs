using JetBrains.Annotations;

namespace Core.Mono.Scenes.Fight {
  public interface INpcModelSet {
    [CanBeNull]
    public INpcModel GetNpcModel(int id);
  }
}