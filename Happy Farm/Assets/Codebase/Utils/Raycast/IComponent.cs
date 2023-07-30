using System.Threading.Tasks;

namespace Codebase.Utils.Raycast
{
    public interface IComponent
    {
        void Interact(UnityEngine.Transform transform);
        void Update();
    }
}