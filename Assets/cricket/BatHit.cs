using UnityEngine;

public class BatHit : MonoBehaviour
{
    public float minHitForce = 5f;
    public float maxHitForce = 15f;

    // 🎯 TIMING RANGES
    public float perfectRange = 0.2f;
    public float goodRange = 0.5f;

    private Vector3 lastPosition;
    private Vector3 batVelocity;

    void Update()
    {
        batVelocity = (transform.position - lastPosition) / Time.deltaTime;
        lastPosition = transform.position;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Ball")) return;

        Rigidbody ballRb = collision.gameObject.GetComponent<Rigidbody>();
        BallBounce ball = collision.gameObject.GetComponent<BallBounce>();

        if (ball != null)
        {
            ball.wasHitByBat = true;
        }

        // 🎯 Bat speed
        float batSpeed = batVelocity.magnitude;

        // 🎯 Base force from speed
        float baseForce = Mathf.Clamp(batSpeed * 2f, minHitForce, maxHitForce);

        // 🎯 Distance for timing
        float distance = Vector3.Distance(transform.position, collision.transform.position);

        float timingMultiplier = 1f;

        // 🔥 TIMING LOGIC
        if (distance < perfectRange)
        {
            timingMultiplier = 1.3f; // strong hit
            Debug.Log("PERFECT SHOT 🔥");
        }
        else if (distance < goodRange)
        {
            timingMultiplier = 1.0f; // normal hit
            Debug.Log("GOOD SHOT 👍");
        }
        else
        {
            timingMultiplier = 0.6f; // weak hit
            Debug.Log("BAD SHOT ❌");
        }

        float finalForce = baseForce * timingMultiplier;

        // 🎯 Direction
        Vector3 direction = (collision.transform.position - transform.position).normalized;

        Vector3 force = direction * finalForce;

        // 🔥 CONTROL HEIGHT (reduce easy sixes)
        force.y = Mathf.Clamp(force.y, 0.8f, 3f);

        // 🔥 FORWARD BOOST
        force += transform.forward * 2f;

        // APPLY FORCE
        ballRb.linearVelocity = Vector3.zero;
        ballRb.AddForce(force, ForceMode.Impulse);

        Debug.Log("Force: " + finalForce);
    }
}