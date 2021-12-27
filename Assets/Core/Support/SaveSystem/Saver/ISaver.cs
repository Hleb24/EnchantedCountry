using Core.Support.SaveSystem.SaveManagers;

namespace Core.Support.SaveSystem.Saver
{
    public interface ISaver {
        void Save(Scrolls scrolls);

        Scrolls Load(out bool isNewGame);
        void DeleteSave();
    }
}
