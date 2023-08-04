using UnityEngine;
using UnityEngine.UI;

namespace Codebase.UI.GameOver
{
    public class GameOverUI : MonoBehaviour
    {
        [field: SerializeField] public Button RestartButton { get; private set; }
        
        public void Show()
        {
            gameObject.SetActive(true);
        }
        
        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}