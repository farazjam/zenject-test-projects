using UnityEngine;
using UnityEngine.Assertions;
using Zenject;

namespace Cube.Spawner.DI2
{
    public class SpawnerInstallerDI2 : MonoInstaller
    {
        [SerializeField] GameObject cubePrefab;

        private void Awake() => Assert.IsNotNull(cubePrefab);

        public override void InstallBindings()
        {
            Container.BindMemoryPool<Cube, Cube.CubePool>().WithInitialSize(10).
                FromComponentInNewPrefab(cubePrefab).UnderTransformGroup("Cubes").NonLazy();
        }
    }
}