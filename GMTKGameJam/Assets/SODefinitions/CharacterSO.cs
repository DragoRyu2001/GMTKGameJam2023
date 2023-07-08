using UnityEngine;

namespace SODefinitions
{
    [CreateAssetMenu(fileName = "CharacterType", menuName = "CharacterSO")]
    // ReSharper disable once InconsistentNaming
    public class CharacterSO : ScriptableObject
    {
        public float BaseHealth;
        public float MoveSpeed;
        public float CharDamageMultiplier; //1 on normal enemies
        public float EngagementDistance; //0 when player
        public float ExitDistance; //0 when player
    }
}