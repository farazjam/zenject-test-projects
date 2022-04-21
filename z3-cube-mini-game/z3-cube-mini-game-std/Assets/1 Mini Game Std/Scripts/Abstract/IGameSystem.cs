//using Snake.Scripts.Data;

namespace Cube.MiniGame.Systems
{
    public interface IGameSystem
    {
        bool IsActive { get; }
        void StartSystem();
        void StopSystem();
        void ClearSystem();
    }
}