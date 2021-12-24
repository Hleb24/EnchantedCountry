using Core.EnchantedCountry.SupportSystems.SaveSystem.SaveManagers;

namespace Core.EnchantedCountry.SupportSystems.SaveSystem.Saver
{
    public interface ISaver {
        void Save(Scrolls scrolls);

        Scrolls Load(out bool isNewGame);
    }
}
