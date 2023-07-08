using SODefinitions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    [SerializeField] float tolerance;
    [SerializeField] CharacterSO stats;
    private Vector2 movement;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
    }
    public void SetMovement(Vector2 inputMovement)
    {
        movement = inputMovement;
        movement.Normalize();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * (stats.MoveSpeed * Time.fixedDeltaTime));

        if (movement.sqrMagnitude > tolerance)
        {
            float moveAngle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, moveAngle), 0.15f);
        }
    }
}
