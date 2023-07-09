using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HealthMatManager : MonoBehaviour
{
    [SerializeField] private SpriteRenderer fill;

    private void Awake()
    {
        fill = transform.GetChild(0).GetComponent<SpriteRenderer>();    
    }
    public void UpdateHealthShader(float health)
    {
        fill.material.SetFloat("_Health", health);
    }
}
