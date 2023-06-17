namespace Codebase.Utils.Raycast
{
    public interface IClickable
    {
        void Construct(params IComponent[] states);
        void Interact();
        void Update();
    }
}