using UnityEngine;
using UnityEngine.Assertions;
using Zenject;

namespace Cube.Spawner.DI1
{
    public class CubeSpawnerInstaller : MonoInstaller
    {
        [SerializeField] GameObject cubePrefab;

        private void Awake() => Assert.IsNotNull(cubePrefab);

        public override void InstallBindings()
        {
            Container.BindFactory<_Cube, FactoryDI1>().FromComponentInNewPrefab(cubePrefab);
        }
    }
}