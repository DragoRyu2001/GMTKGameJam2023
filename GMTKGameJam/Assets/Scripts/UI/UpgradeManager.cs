using SODefinitions;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable InconsistentNaming

namespace UI
{
    public class UpgradeManager : MonoBehaviour
    {
        [SerializeField] private Slider AssaultDamageProgression;
        [SerializeField] private Slider AssaultDurabilityProgression;
        [SerializeField] private Slider DMRDamageProgression;
        [SerializeField] private Slider DMRDurabilityProgression;
        [SerializeField] private Slider SMGDamageProgression;
        [SerializeField] private Slider SMGDurabilityProgression;
        [SerializeField] private Slider PlayerProgression;

        private PlayerStats stats;

        private void OnEnable()
        {
            stats = PlayerStats.Instance;
            SetAllSliders();
        }

        private void SetAllSliders()
        {
            SetPlayerSliders();

            SetAssaultSliders();

            SetDMRSliders();

            SetSMGSliders();
        }

        private void SetSMGSliders()
        {
            ProgressionSO SMGStats = stats.GetProgression(typeof(SMG));
            SetSlider(SMGDamageProgression, SMGStats.DamageProgression.Count, PlayerPrefsManager.SMG.GetDamageLevel());
            SetSlider(SMGDurabilityProgression, SMGStats.DurabilityProgression.Count,
                PlayerPrefsManager.SMG.GetDurabilityLevel());
        }

        private void SetDMRSliders()
        {
            ProgressionSO DMRStats = stats.GetProgression(typeof(DMR));
            SetSlider(DMRDamageProgression, DMRStats.DamageProgression.Count,
                PlayerPrefsManager.DMR.GetDamageLevel());
            SetSlider(DMRDurabilityProgression, DMRStats.DurabilityProgression.Count,
                PlayerPrefsManager.DMR.GetDurabilityLevel());
        }

        private void SetAssaultSliders()
        {
            ProgressionSO AssaultStats = stats.GetProgression(typeof(Assault));
            SetSlider(AssaultDamageProgression, AssaultStats.DamageProgression.Count,
                PlayerPrefsManager.Assault.GetDamageLevel());
            SetSlider(AssaultDurabilityProgression,
                AssaultStats.DurabilityProgression.Count,
                PlayerPrefsManager.Assault.GetDurabilityLevel());
        }

        private void SetPlayerSliders()
        {
            ProgressionSO PlayerStats = stats.GetProgression();
            SetSlider(PlayerProgression, PlayerStats.DurabilityProgression.Count,
                PlayerPrefsManager.Player.GetDurabilityLevel());
        }

        public void UpgradeAssaultDamage()
        {
            PlayerPrefsManager.DecreaseCoins(10);
            PlayerPrefsManager.Assault.UpgradeDamage();
            SetAssaultSliders();
        }

        public void UpgradeAssaultDurability()
        {
            PlayerPrefsManager.DecreaseCoins(10);
            PlayerPrefsManager.Assault.UpgradeDurability();
            SetAssaultSliders();
        }

        public void UpgradeDMRDamage()
        {
            PlayerPrefsManager.DecreaseCoins(10);
            PlayerPrefsManager.DMR.UpgradeDamage();
            SetDMRSliders();
        }

        public void UpgradeDMRDurabilty()
        {
            PlayerPrefsManager.DecreaseCoins(10);
            PlayerPrefsManager.DMR.UpgradeDurability();
            SetDMRSliders();
        }

        public void UpgradeSMGDamage()
        {
            PlayerPrefsManager.DecreaseCoins(10);
            PlayerPrefsManager.SMG.UpgradeDamage();
            SetSMGSliders();
        }

        public void UpgradeSMGDurability()
        {
            PlayerPrefsManager.DecreaseCoins(10);
            PlayerPrefsManager.SMG.UpgradeDurability();
            SetSMGSliders();
        }

        public void UpgradePlayerHealth()
        {
            PlayerPrefsManager.DecreaseCoins(10);
            PlayerPrefsManager.Player.UpgradeDurability();
            SetPlayerSliders();
        }

        private void SetSlider(Slider slider, int total, int level)
        {
            slider.value = (float)level / (float)total;
        }
    }
}