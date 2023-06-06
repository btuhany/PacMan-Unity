using UnityEngine;

public class Ghost : MonoBehaviour
{
    public int Point = 200;
    void GetEaten()
    {
        GameManager.Instance.IncreaseScore(Point);
    }
}
