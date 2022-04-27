using Snake.Scripts.Data;

namespace Snake.Scripts.Abstract
{
    public interface IGameSystem
    {
        bool IsActive { get; }
        void StartSystem();
        void StopSystem();
        void ClearSystem();
    }
}