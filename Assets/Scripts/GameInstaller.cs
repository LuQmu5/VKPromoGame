using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private ProgressDisplay2 _scoreDisplay;
    [SerializeField] private UnityConnector _connector;

    public override void InstallBindings()
    {
        Container.BindInstance(_playerController).AsSingle();
        Container.BindInstance(_scoreDisplay).AsSingle();
        Container.BindInstance(_connector).AsSingle();

        Container.Bind<ObjectFactory>().AsSingle();
        Container.BindInterfacesAndSelfTo<GameManager>().AsSingle();
    }
}