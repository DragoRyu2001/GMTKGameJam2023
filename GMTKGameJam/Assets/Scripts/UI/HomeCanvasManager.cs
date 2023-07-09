using UnityEngine;

namespace UI
{
    public class HomeCanvasManager : MonoBehaviour
    {
        public void PlayButtonClicked()
        {
            GameSceneManager.Instance.LoadGamePlay();
        }
    }
}
