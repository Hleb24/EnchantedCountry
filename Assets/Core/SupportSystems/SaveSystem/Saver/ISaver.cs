using Core.SupportSystems.SaveSystem.SaveManagers;

namespace Core.SupportSystems.SaveSystem.Saver
{
    public interface ISaver {
        void Save(Scrolls scrolls);

        Scrolls Load(out bool isNewGame);
        void DeleteSave();
    }
}
