using UnityEngine;

public class BallBowler : MonoBehaviour
{
    public Rigidbody ball;
    public Transform bowlPoint;
    public float force = 500f;

    void Start()
    {
        InvokeRepeating("BowlBall", 2f, 5f);
    }

    void BowlBall()
    {
        ball.transform.position = bowlPoint.position;
        ball.linearVelocity = Vector3.zero;
        ball.angularVelocity = Vector3.zero;

        ball.AddForce(Vector3.forward * force);
    }
}