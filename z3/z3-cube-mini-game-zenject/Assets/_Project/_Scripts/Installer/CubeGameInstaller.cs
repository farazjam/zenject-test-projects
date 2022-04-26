using UnityEngine;
using Cube.MiniGame.Systems;
using Cube.MiniGame.Data;
using Cube.MiniGame.Blocks;
using Cube.MiniGame.Views;
using Cube.MiniGame.Utils;
using Zenject;

namespace Cube.MiniGame.Installers
{
    public class CubeGameInstaller : MonoInstaller
    {
        public PlayerBlock playerBlockPrefab;
        public CoinBlock coinBlockPrefab;
        public HurdleBlock hurdleBlockPrefab;
        public Despawner despawnerPrefab;
        public CubeGameData data;

        public override void InstallBindings()
        {
            // Signals
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<SystemStateChangedSignal>();
            Container.DeclareSignal<LevelConclusionSignal>();
            Container.DeclareSignal<BlockTouchedSignal>();

            // ScriptableObject Data
            Container.Bind<CubeGameData>().FromInstance(data).AsSingle().NonLazy();

            // Managers
            Container.BindInterfacesTo<GameManager>().AsSingle().NonLazy();
            Container.BindInterfacesTo<DataManager>().AsSingle().NonLazy();

            // Systems
            Container.BindInterfacesTo<GameStateHandler>().AsSingle().NonLazy();
            Container.BindInterfacesTo<InputManager>().AsSingle().NonLazy();
            Container.BindInterfacesTo<PlayerController>().AsSingle().NonLazy();
            Container.BindInterfacesTo<SwerveMovement>().AsSingle().NonLazy();
            Container.BindInterfacesTo<CoinSpawner>().AsSingle().NonLazy();
            Container.BindInterfacesTo<HurdleSpawner>().AsSingle().NonLazy();

            //Utils
            Container.BindInterfacesTo<Util>().AsSingle().NonLazy();

            // Prefabs
            Container.BindInterfacesTo<PlayerBlock>().FromComponentInNewPrefab(playerBlockPrefab).AsSingle().NonLazy();
            Container.BindMemoryPool<CoinBlock, CoinBlock.CoinBlockPool>().WithFixedSize(10).FromComponentInNewPrefab(coinBlockPrefab).
                UnderTransformGroup(BlockType.Coin.ToString()).NonLazy();
            Container.BindMemoryPool<HurdleBlock, HurdleBlock.HurdleBlockPool>().WithFixedSize(10).FromComponentInNewPrefab(hurdleBlockPrefab).
                UnderTransformGroup(BlockType.Hurdle.ToString()).NonLazy();
            Container.Bind<Despawner>().FromComponentInNewPrefab(despawnerPrefab).AsSingle().NonLazy();

        }
    }
}