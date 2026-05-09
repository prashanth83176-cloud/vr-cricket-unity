using UnityEngine;
using System.Collections;

public class BoundaryTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Ball")) return;
        if (GameManager.instance.isOut) return;

        BallBounce ball = other.GetComponent<BallBounce>();
        if (ball == null) return;

        if (!ball.wasHitByBat) return;
        if (ball.alreadyScored) return;

        ball.alreadyScored = true;

        // ✅ DELAY FIX
        StartCoroutine(CheckScore(ball));
    }

    IEnumerator CheckScore(BallBounce ball)
    {
        yield return new WaitForSeconds(0.2f); // 🔥 IMPORTANT

        Debug.Log("Bounce status: " + ball.hasBounced);

        if (ball.hasBounced)
        {
            Debug.Log("FOUR! ✅");
            GameManager.instance.AddRuns(4);
        }
        else
        {
            Debug.Log("SIX! ✅");
            GameManager.instance.AddRuns(6);
        }
    }
}