using UnityEngine;

public class Boundary : MonoBehaviour
{
    private bool hasBounced = false;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            hasBounced = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            if (GameManager.instance.isOut) return;

            if (hasBounced)
            {
                Debug.Log("FOUR!");
                GameManager.instance.AddRuns(4);
            }
            else
            {
                Debug.Log("SIX!");
                GameManager.instance.AddRuns(6);
            }

            // prevent multiple scoring
            hasBounced = false;
        }
    }
}