using SODefinitions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    [Header("Dash Variables")]
    [SerializeField] private float dashImpulse;
    [SerializeField] private float dashDecay;


    [SerializeField] private float tolerance;
    [SerializeField] private CharacterSO stats;
    private Vector2 movement;
    private Rigidbody2D rb;
    [SerializeField]private float moveSpeed;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
        moveSpeed = stats.MoveSpeed;
    }
    public void SetMovement(Vector2 inputMovement)
    {
        movement = inputMovement;
        movement.Normalize();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));

        if (movement.sqrMagnitude > tolerance)
        {
            float moveAngle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, moveAngle), 0.15f);
        }
    }

    public void Dash()
    {
        Debug.Log("Dashed");
        moveSpeed += dashImpulse;
        StartCoroutine(MoveSpeedDecay());
    }

    private IEnumerator MoveSpeedDecay()
    {
        while(Mathf.Abs(moveSpeed - stats.MoveSpeed) > tolerance)
        {
            moveSpeed -= dashDecay;
            yield return null;
        }
    }
}
