using Core.Mono.Scenes.Fight;
using Core.SO.Impacts;
using Core.SO.NpcSet;
using Core.Support.Data;
using Core.Support.SaveSystem.SaveManagers;
using UnityEngine;
using Zenject;

namespace Aberrance.Installers {
  public class FightInstallers : MonoInstaller {
    [SerializeField]
    private NpcSetSO _npcSetSO;
    [SerializeField]
    private NpcWeaponSet _npcWeaponSet;
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
      Container.Bind<INpcWeaponSet>().To<NpcWeaponSet>().FromScriptableObject(_npcWeaponSet).AsSingle();
      Container.Bind<IImpactsSet>().To<ImpactsSet>().FromScriptableObject(_impactsSet).AsSingle();
      Container.Bind<NpcBuilder>().AsSingle();
    }
  }
}