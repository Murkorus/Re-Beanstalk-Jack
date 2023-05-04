using UnityEngine;

public class LaunchToHitTarget : MonoBehaviour
{
    public float angle;
    public float speed;
    public Transform target;

    private Rigidbody2D rb;


    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.Find("Player").transform;
        ThrowObject();  
    }

    Vector2 calcBallisticVelocityVector(Vector2 source, Vector2 target, float angle)
    {
        Vector2 direction = target - source;
        float h = direction.y;
        direction.y = 0;
        float distance = direction.magnitude;
        float a = angle * Mathf.Deg2Rad;
        direction.y = distance * Mathf.Tan(a);
        distance += h / Mathf.Tan(a);

        // calculate velocity
        float velocity = Mathf.Sqrt(distance * Physics2D.gravity.magnitude / Mathf.Sin(2 * a));
        return velocity * direction.normalized;
    }

    void ThrowObject()
    {
        // calculate the velocity needed to throw the object to the target
        Vector2 velocityVector = calcBallisticVelocityVector(transform.position, target.transform.position, angle);

        // add the calculated velocity to the rigidbody
        rb.AddForce(velocityVector, ForceMode2D.Impulse);
    }
}
