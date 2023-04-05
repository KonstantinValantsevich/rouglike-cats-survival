using UnityEngine;
using UnityEngine.Animations;

public class Entity : MonoBehaviour
{
    public float moveSpeed;

    protected void Move(Vector2 movementDirection)
    {
        var velocity = moveSpeed * movementDirection.normalized;
        transform.Translate(velocity * Time.deltaTime, Space.World);
    }

    protected void SetLookRotation(Vector3 pointToLook)
    {
        var dir = pointToLook - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}