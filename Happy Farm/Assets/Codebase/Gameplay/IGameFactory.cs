using System.Threading.Tasks;
using UnityEngine;

namespace Codebase.Gameplay
{
    public interface IGameFactory
    {
        void RegisterSavable(GameObject entity);
        Task CreateUI();
    }
}