namespace Codebase.Utils.Raycast
{
    public interface IClickable
    {
        void Construct(params IComponent[] components);
        void Interact();
        void Update();
    }
}