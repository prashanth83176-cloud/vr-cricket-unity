using UnityEngine;

public class BallBounce : MonoBehaviour
{
    public bool hasBounced = false;
    public bool alreadyScored = false;
    public bool wasHitByBat = false;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            hasBounced = true;
            Debug.Log("BOUNCED!");
        }

        if (collision.gameObject.CompareTag("Bat"))
        {
            wasHitByBat = true;
        }
    }

    void OnCollisionStay(Collision collision)
    {
        // backup bounce detection
        if (collision.gameObject.CompareTag("Ground"))
        {
            hasBounced = true;
        }
    }

    public void ResetBallState()
    {
        hasBounced = false;
        alreadyScored = false;
        wasHitByBat = false;
    }
}