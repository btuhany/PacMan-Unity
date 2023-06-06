using UnityEngine;

public class Ghost : MonoBehaviour
{
    [SerializeField] int _point = 200;
    void GetEaten()
    {
        GameManager.Instance.IncreaseScore(_point);
    }
}
