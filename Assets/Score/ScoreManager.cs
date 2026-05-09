using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int score = 0;

    public void AddRuns(int runs)
    {
        score += runs;
        Debug.Log("Score: " + score);
    }
}