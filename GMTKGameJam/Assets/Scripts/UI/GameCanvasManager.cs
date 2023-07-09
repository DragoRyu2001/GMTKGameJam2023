using System.Collections;
using Entities;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class GameCanvasManager : MonoBehaviour
    {
        [SerializeField] private GameObject UpgradeScreen;
        [SerializeField] private GameObject GameEndScreen;
        [SerializeField] private Image HealthBar; 
        [SerializeField] private Image BossHealthBar; 
        private void Start()
        {
            HealthBar.fillAmount = 0f;
            BossHealthBar.fillAmount = 0f;
            GameManager.Instance.GameStart += StartGame;
            GameManager.Instance.BossPhase += BossStarted;
        }

        private void StartGame()
        {
            StartCoroutine(FillBar(HealthBar));
            GameManager.Instance.PlayerTransform.GetComponent<Player>().TookDamage += UpdateHealthBar;
        }

        private void BossStarted()
        {
            StartCoroutine(FillBar(BossHealthBar));
            GameManager.Instance.BossTransform.GetComponent<EnemyBoss>().TookDamage += UpdateEnemyHealthBar;
        }

        private void UpdateEnemyHealthBar(float percentage)
        {
            BossHealthBar.fillAmount = percentage;
        }
        private void UpdateHealthBar(float percentage)
        {
            Debug.Log("Health percentage is: "+percentage);
            HealthBar.fillAmount = percentage;
        }

        public void HomeClicked()
        {
            GameSceneManager.Instance.LoadMainMenu();
        }

        private IEnumerator FillBar(Image image)
        {
            float value = 0f;
            while (value < 1)
            {
                image.fillAmount = value;
                value += Time.deltaTime*2f;
                yield return null;
            }

            image.fillAmount = 1f;
        }
    }
}
