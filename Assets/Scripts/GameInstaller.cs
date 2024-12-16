using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private ScoreDisplay _scoreDisplay;

    public override void InstallBindings()
    {
        Container.BindInstance(_playerController).AsSingle();
        Container.BindInstance(_scoreDisplay).AsSingle();

        Container.Bind<ObjectFactory>().AsSingle();
        Container.BindInterfacesAndSelfTo<GameManager>().AsSingle();
    }
}