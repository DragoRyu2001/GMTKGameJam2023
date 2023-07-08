using UnityEngine;

[CreateAssetMenu(fileName = "CharacterType", menuName = "CharacterSO")]
public class CharacterSO : ScriptableObject
{
    public float baseHealth;
    public float moveSpeed;
    public float charDamageMultiplier;  //1 on normal enemies

    public float engagementDistance; //0 when player
    public float exitDistance;       //0 when player
}
