using UnityEngine;
using System.Collections;

public class WicketHit : MonoBehaviour
{
    private Rigidbody rb;
    private bool isHit = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball") && !isHit)
        {
            isHit = true;

            // 🟢 Enable physics → wickets fall
            rb.isKinematic = false;
            rb.useGravity = true;

            // 🔥 Delay OUT (IMPORTANT)
            StartCoroutine(OutAfterDelay());
        }
    }

    IEnumerator OutAfterDelay()
    {
        yield return new WaitForSeconds(2f); // delay 1 second

        GameManager.instance.Out();
    }
}