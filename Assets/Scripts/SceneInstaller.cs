using Interfaces;
using Signals;
using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller<SceneInstaller>
{
    public Rigidbody _playerRB;
    public GameObject pulpitPrefab;
    public GameManager GM;
    
    public override void InstallBindings()
    {
        //For all intents and purposes, GM has become a singleton here
        Container.BindInstance<GameManager>(GM);
        Container.BindInterfacesAndSelfTo<PulpitPool>().AsSingle().WithArguments(pulpitPrefab);
        Container.Bind<PlatformSpawnLogic>().AsCached();
        Container.Bind<JsonLoader>().AsSingle();
        
        //these 2 bindings work on interface req and class req in PIC
        //one thing i can notice here is that I didnt bind the interface so this is simpler DI
        
        // Container.Bind<PlayerMovementController>().FromInstance(new PlayerMovementController(_playerRB));
        // Container.BindInterfacesAndSelfTo<PlayerMovementController>().FromInstance(new PlayerMovementController(_playerRB)).AsSingle();
        
        //this one works only when interface is required in the PIC
        Container.Bind<IPlayerMovementController>().To<PlayerMovementController>().FromInstance(new PlayerMovementController(_playerRB)).AsSingle();
        
        Container.BindFactory<Pulpit, Pulpit.Factory>().FromComponentInNewPrefab(pulpitPrefab);


        SignalBusInstaller.Install(Container);
        Container.DeclareSignal<PlayerDiedSignal>();
        // Container.DeclareSignal<PlayerPausedGameSignal>();
        Container.DeclareSignal<DataLoadedSignal>();

        Container.BindSignal<PlayerDiedSignal>().ToMethod<GameManager>(gm => gm.EndLevel).From(x => x.FromInstance(GM));
        Container.BindSignal<DataLoadedSignal>().ToMethod<GameManager>(gm => gm.SetDataFromApi).From(x => x.FromInstance(GM));
        // Container.BindSignal<PlayerPausedGameSignal>().ToMethod<PauseMenu>(x => x.TogglePause).FromNew();

        //since signal is only read by game manager
        // Container.BindSignal<DataLoadedSignal>().ToMethod<GameManager>(x => x.SetDataFromApi).FromResolve();
    }
}