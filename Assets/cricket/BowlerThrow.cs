 using UnityEngine;

public class BowlerThrow : MonoBehaviour
{
    public Rigidbody ball;
    public Transform releasePoint;
    public float bowlingForce = 600f;

    void Start()
    {
        InvokeRepeating("BowlBall", 2f, 6f);
    }

    void BowlBall()
    {
        ball.transform.position = releasePoint.position;
        ball.linearVelocity = Vector3.zero;
        ball.angularVelocity = Vector3.zero;

        ball.AddForce(releasePoint.forward * bowlingForce);
    }
}