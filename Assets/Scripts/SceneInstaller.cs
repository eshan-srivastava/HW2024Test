using Interfaces;
using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller<SceneInstaller>
{
    public Rigidbody _playerRB;
    public GameObject pulpitPrefab;
    
    public override void InstallBindings()
    {
        // Container.Bind<GameManager>().AsSingle();
        Container.BindInterfacesAndSelfTo<PulpitPool>().AsSingle().WithArguments(pulpitPrefab);
        Container.Bind<PlatformSpawnLogic>().AsCached();
        Container.Bind<JsonLoader>().AsSingle();
        
        //these 2 bindings work on interface req and class req in PIC
        //one thing i can notice here is that I didnt bind the interface so this is simpler DI
        
        // Container.Bind<PlayerMovementController>().FromInstance(new PlayerMovementController(_playerRB));
        Container.BindInterfacesAndSelfTo<PlayerMovementController>().FromInstance(new PlayerMovementController(_playerRB)).AsSingle();
        
        //this one works only when interface is required in the PIC
        Container.Bind<IPlayerMovementController>().To<PlayerMovementController>().FromInstance(new PlayerMovementController(_playerRB)).AsSingle();
        
        Container.BindFactory<Pulpit, Pulpit.Factory>().FromComponentInNewPrefab(pulpitPrefab);

        
        
        // Container.DeclareSignal<DataLoadedSignal>();
        //since signal is only read by game manager
        // Container.BindSignal<DataLoadedSignal>().ToMethod<GameManager>(x => x.SetDataFromApi).FromResolve();
    }
}