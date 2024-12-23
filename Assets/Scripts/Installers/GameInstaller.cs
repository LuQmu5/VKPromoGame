using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private ProgressDisplay _progressDisplay;
    [SerializeField] private TutorialManager _tutorialManager;
    [SerializeField] private ObjectSpawner _objectSpawner;
    [SerializeField] private GameOverDisplay _gameOverDisplay;

    public override void InstallBindings()
    {
        Container.BindInstance(_playerController).AsSingle();
        Container.BindInstance(_progressDisplay).AsSingle();
        Container.BindInstance(_tutorialManager).AsSingle();
        Container.BindInstance(_objectSpawner).AsSingle();
        Container.BindInstance(_gameOverDisplay).AsSingle();

        Container.Bind<ObjectFactory>().AsSingle();
        Container.BindInterfacesAndSelfTo<GameManager>().AsSingle();
    }
}