using UnityEngine;

public class BowlerController : MonoBehaviour
{
    public Rigidbody ball;
    public Transform releasePoint;

    public Transform yorkerPoint;
    public Transform fullPoint;
    public Transform goodLengthPoint;
    public Transform shortPoint;

    public float minSpeed = 16f;
    public float maxSpeed = 22f;

    public float minSwing = -0.4f;
    public float maxSwing = 0.4f;

    bool released = false;

    void Update()
    {
        if (!released)
        {
            ball.transform.position = releasePoint.position;
        }
    }

    public void ReleaseBall()
    {
        released = true;

        // ✅ RESET PHYSICS
        ball.linearVelocity = Vector3.zero;
        ball.angularVelocity = Vector3.zero;

        // ✅ 🔥 RESET BALL STATE (IMPORTANT)
        BallBounce bb = ball.GetComponent<BallBounce>();
        if (bb != null)
        {
            bb.ResetBallState();
        }

        // 🎲 RANDOM DELIVERY
        int type = Random.Range(0, 4);
        Transform target = goodLengthPoint;

        if (type == 0)
        {
            target = yorkerPoint;
            Debug.Log("Yorker");
        }
        else if (type == 1)
        {
            target = fullPoint;
            Debug.Log("Full");
        }
        else if (type == 2)
        {
            target = goodLengthPoint;
            Debug.Log("Good Length");
        }
        else
        {
            target = shortPoint;
            Debug.Log("Short / Bouncer");
        }

        // 🎯 DIRECTION
        Vector3 dir = (target.position - releasePoint.position).normalized;

        // 🎯 SPEED
        float speed = Random.Range(minSpeed, maxSpeed);

        Vector3 velocity = dir * speed;

        // 🎯 ARC
        velocity.y += Random.Range(1.5f, 2.5f);
        velocity.y -= 0.8f;

        // 🎯 SWING
        float swing = Random.Range(minSwing, maxSwing);
        velocity += releasePoint.right * swing;

        // APPLY
        ball.linearVelocity = velocity;

        // 🎯 SPIN
        ball.angularVelocity = new Vector3(
            Random.Range(15f, 25f),
            0,
            0
        );

        Invoke(nameof(ResetBall), 4f);
    }

    void ResetBall()
    {
        released = false;

        ball.linearVelocity = Vector3.zero;
        ball.angularVelocity = Vector3.zero;

        ball.transform.position = releasePoint.position;
    }
}