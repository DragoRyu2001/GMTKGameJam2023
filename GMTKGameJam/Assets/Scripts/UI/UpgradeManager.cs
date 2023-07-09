using System.Collections.Generic;
using System.Linq;
using SODefinitions;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

// ReSharper disable InconsistentNaming

namespace UI
{
    public class UpgradeManager : MonoBehaviour
    {
        [Header("Pages")] 
        [SerializeField] 
        private List<GameObject> PageList;
        [Header("Sliders")]
        [SerializeField] private Slider AssaultFireRateProgression;
        [SerializeField] private Slider AssaultDurabilityProgression;
        [SerializeField] private Slider SniperFireRateProgression;
        [SerializeField] private Slider SniperDurabilityProgression;
        [SerializeField] private Slider SMGFireRateProgression;
        [SerializeField] private Slider SMGDurabilityProgression;
        [SerializeField] private Slider PlayerHealthProgression;
        [SerializeField] private Slider PlayerDashProgression;

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
            SetSlider(SMGFireRateProgression, SMGStats.DamageProgression.Count, PlayerPrefsManager.SMG.GetDamageLevel());
            SetSlider(SMGDurabilityProgression, SMGStats.DurabilityProgression.Count,
                PlayerPrefsManager.SMG.GetDurabilityLevel());
        }

        private void SetDMRSliders()
        {
            ProgressionSO DMRStats = stats.GetProgression(typeof(Sniper));
            SetSlider(SniperFireRateProgression, DMRStats.DamageProgression.Count,
                PlayerPrefsManager.DMR.GetDamageLevel());
            SetSlider(SniperDurabilityProgression, DMRStats.DurabilityProgression.Count,
                PlayerPrefsManager.DMR.GetDurabilityLevel());
        }

        private void SetAssaultSliders()
        {
            ProgressionSO AssaultStats = stats.GetProgression(typeof(Assault));
            SetSlider(AssaultFireRateProgression, AssaultStats.DamageProgression.Count,
                PlayerPrefsManager.Assault.GetDamageLevel());
            SetSlider(AssaultDurabilityProgression,
                AssaultStats.DurabilityProgression.Count,
                PlayerPrefsManager.Assault.GetDurabilityLevel());
        }

        private void SetPlayerSliders()
        {
            ProgressionSO PlayerStats = stats.GetProgression();
            SetSlider(PlayerHealthProgression, PlayerStats.DurabilityProgression.Count,
                PlayerPrefsManager.Player.GetDurabilityLevel());
        }
        #region Upgrade Regions
        public void UpgradeAssaultDamage()
        {
            if (PlayerPrefsManager.GetCoins() < stats.GetProgression(typeof(Assault))
                    .DamageCost[PlayerPrefsManager.DMR.GetDurabilityLevel() + 1]) return;
            PlayerPrefsManager.DecreaseCoins(10);
            PlayerPrefsManager.Assault.UpgradeDamage();
            SetAssaultSliders();
        }

        public void UpgradeAssaultDurability()
        {
            if (PlayerPrefsManager.GetCoins() < stats.GetProgression(typeof(Assault))
                    .DurabilityCost[PlayerPrefsManager.DMR.GetDurabilityLevel() + 1]) return;
            PlayerPrefsManager.DecreaseCoins(10);
            PlayerPrefsManager.Assault.UpgradeDurability();
            SetAssaultSliders();
        }

        public void UpgradeDMRDamage()
        {
            if (PlayerPrefsManager.GetCoins() < stats.GetProgression(typeof(Sniper))
                    .DamageCost[PlayerPrefsManager.DMR.GetDamageLevel() + 1]) return;
            PlayerPrefsManager.DecreaseCoins(10);
            PlayerPrefsManager.DMR.UpgradeDamage();
            SetDMRSliders();
        }

        public void UpgradeDMRDurabilty()
        {
            if (PlayerPrefsManager.GetCoins() < stats.GetProgression(typeof(Sniper))
                    .DurabilityCost[PlayerPrefsManager.DMR.GetDurabilityLevel() + 1]) return;
            PlayerPrefsManager.DecreaseCoins(10);
            PlayerPrefsManager.DMR.UpgradeDurability();
            SetDMRSliders();
        }

        public void UpgradeSMGDamage()
        {
            if (PlayerPrefsManager.GetCoins() < stats.GetProgression(typeof(SMG))
                    .DamageCost[PlayerPrefsManager.SMG.GetDamageLevel() + 1]) return;
            PlayerPrefsManager.DecreaseCoins(10);
            PlayerPrefsManager.SMG.UpgradeDamage();
            SetSMGSliders();
        }

        public void UpgradeSMGDurability()
        {
            if (PlayerPrefsManager.GetCoins() < stats.GetProgression(typeof(SMG))
                    .DurabilityCost[PlayerPrefsManager.SMG.GetDurabilityLevel() + 1]) return;
            PlayerPrefsManager.DecreaseCoins(10);
            PlayerPrefsManager.SMG.UpgradeDurability();
            SetSMGSliders();
        }

        public void UpgradePlayerHealth()
        {
            if (PlayerPrefsManager.GetCoins() < stats.GetProgression()
                    .DurabilityCost[PlayerPrefsManager.Player.GetDurabilityLevel() + 1]) return;
            PlayerPrefsManager.DecreaseCoins(10);
            PlayerPrefsManager.Player.UpgradeDurability();
            SetPlayerSliders();
        }

        public void UpgradePlayerDash()
        {
            if (PlayerPrefsManager.GetCoins() < stats.GetProgression()
                    .DamageCost[PlayerPrefsManager.Player.GetDamageLevel() + 1]) return;
            PlayerPrefsManager.DecreaseCoins(10);
            PlayerPrefsManager.Player.UpgradeDamage();
            SetPlayerSliders();
        }

        private bool CanUpgrade()
        {
            //return PlayerPrefsManager.GetCoins();
            return true;
        }
        #endregion
        
        private void SetSlider(Slider slider, int total, int level)
        {
            slider.value = (float)level / (float)total;
        }
    }
}