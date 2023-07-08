using DragoRyu.Utilities;
using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform destination;
    public float acceleration;
    public float velocity;

    private float maxVelocity;

    public void Init(float maxV, Transform dest)
    {
        maxVelocity = maxV;
        destination = dest;
    }

    public void StartMove()
    {
        StartCoroutine(Accelerate());
    }

    private IEnumerator Accelerate()
    {
        while (velocity < maxVelocity)
        {
            velocity += acceleration;
            yield return null;
        }
    }
    private IEnumerator Decelerate()
    {
        while (velocity > 0)
        {
            velocity -= acceleration;
            yield return null;
        }
    }

    public void EndMove()
    {
        StartCoroutine(Decelerate());
    }

    public void KillMovement()
    {
        StopAllCoroutines();
        Destroy(this);
    }

    private void Update()
    {
        if(destination != null)
        {
            Vector2 direction = destination.position.XY() - transform.position.XY();
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.Euler(0, 0, angle);
            if (velocity > 0)
            {
                transform.position += Time.deltaTime * velocity * transform.up;
            }
        }
    }


}
