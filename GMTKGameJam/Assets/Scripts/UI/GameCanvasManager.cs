using System.Collections;
using DragoRyu.DevTools;
using Entities;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class GameCanvasManager : MonoBehaviour
    {
        [SerializeField] private GameObject PauseScreen;
        [SerializeField] private GameObject UpgradeScreen;
        [SerializeField] private GameObject GameEndScreen;
        [SerializeField] private Image HealthBar;
        [SerializeField] private Image BossHealthBar;
        [SerializeField] private Image FKey;
        private Trigger _playerCheck;

        private void Start()
        {
            Time.timeScale = 1f;
            HealthBar.fillAmount = 0f;
            BossHealthBar.fillAmount = 0f;
            GameManager.Instance.GameStart += StartGame;
            GameManager.Instance.BossPhase += BossStarted;
            _playerCheck = new Trigger(PlayerAvailable);
        }

        private void Update()
        {
            if (GameManager.Instance.PlayerTransform != null)
                _playerCheck.SetTrigger();
        }

        private void StartGame()
        {
            StartCoroutine(FillBar(HealthBar));
            GameManager.Instance.PlayerTransform.GetComponent<Player>().TookDamage += UpdateHealthBar;
            FKey.color = Color.white;
        }

        private void BossStarted()
        {
            StartCoroutine(FillBar(BossHealthBar));
            GameManager.Instance.BossTransform.GetComponent<EnemyBoss>().TookDamage += UpdateEnemyHealthBar;
        }

        private void PlayerAvailable()
        {
            GameManager.Instance.PlayerTransform.GetComponent<Player>().WeaponAvailable += WeaponAvailable;
            GameManager.Instance.PlayerDeath += ShowGameEnd;
        }

        private void WeaponAvailable(bool available)
        {
            FKey.gameObject.SetActive(available);
        }

        private void UpdateEnemyHealthBar(float percentage)
        {
            BossHealthBar.fillAmount = percentage;
        }

        private void UpdateHealthBar(float percentage)
        {
            Debug.Log("Health percentage is: " + percentage);
            HealthBar.fillAmount = percentage;
        }

        public void HomeClicked()
        {
            Time.timeScale = 1f;
            GameSceneManager.Instance.LoadMainMenu();
        }

        private IEnumerator FillBar(Image image)
        {
            float value = 0f;
            while (value < 1)
            {
                image.fillAmount = value;
                value += Time.deltaTime * 2f;
                yield return null;
            }

            image.fillAmount = 1f;
        }

        private void ShowGameEnd()
        {
            GameEndScreen.SetActive(true);
        }

        public void ShowMenu(bool open)
        {
            Time.timeScale = open ? 0f : 1f;
            PauseScreen.SetActive(open);
        }
    }
}