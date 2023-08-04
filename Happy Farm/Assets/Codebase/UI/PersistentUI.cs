using Codebase.UI.GameOver;
using UnityEngine;

namespace Codebase.UI
{
    public class PersistentUI : MonoBehaviour
    {
        [field: SerializeField] public GameOverUI GameOverUI { get; private set; }
    }
}