using Core.Main.Character.Class;
using Core.Main.Character.Item;
using Core.Main.Character.Quality;
using Core.Main.GameRule.Equipment;
using Core.Main.GameRule.Point;
using Core.Main.NonPlayerCharacters.Item;
using Core.Mono.Scenes.Fight;
using Core.SO.ImpactObjects;
using Core.SO.NpcObjects.NpcSet;
using Core.Support.SaveSystem.SaveManagers;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace EnchantedCountry.Installers {
  public class FightInstallers : MonoInstaller {
    [SerializeField]
    private NpcSetSO _npcSetSO;
    [FormerlySerializedAs("_npcWeaponSet"), SerializeField]
    private NpcWeaponSetSO _npcWeaponSetSO;
    [SerializeField]
    private ImpactsSet _impactsSet;

    public override void InstallBindings() {
      Container.Bind<IClassType>().FromResolveGetter(Dealers.Resolve<IClassType>()).AsSingle();
      Container.Bind<IRiskPoints>().FromResolveGetter(Dealers.Resolve<IRiskPoints>()).AsSingle();
      Container.Bind<IEquipment>().FromResolveGetter(Dealers.Resolve<IEquipment>()).AsSingle();
      Container.Bind<IEquipmentUsed>().FromResolveGetter(Dealers.Resolve<IEquipmentUsed>()).AsSingle();
      Container.Bind<IGamePoints>().FromResolveGetter(Dealers.Resolve<IGamePoints>()).AsSingle();
      Container.Bind<IWallet>().FromResolveGetter(Dealers.Resolve<IWallet>()).AsSingle();
      Container.Bind<IQualityPoints>().FromResolveGetter(Dealers.Resolve<IQualityPoints>()).AsSingle();
      Container.Bind<INpcModelSet>().To<NpcSetSO>().FromScriptableObject(_npcSetSO).AsSingle();
      Container.Bind<INpcWeaponSet>().To<NpcWeaponSetSO>().FromScriptableObject(_npcWeaponSetSO).AsSingle();
      Container.Bind<IImpactsSet>().To<ImpactsSet>().FromScriptableObject(_impactsSet).AsSingle();
      Container.Bind<NpcBuilder>().AsSingle();
    }
  }
}