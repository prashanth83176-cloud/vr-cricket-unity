using UnityEngine;

public class VRButton : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Controller"))
        {
            GameManager.instance.RestartGame();
        }
    }
}