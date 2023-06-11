using UnityEngine;

public class Pellet : MonoBehaviour
{
    [SerializeField] int _point = 50;
    protected virtual void GetEaten()
    {
        GameManager.Instance.IncreaseScore(_point);
        this.gameObject.SetActive(false);
        GameManager.Instance.IsGameEnded();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pacman"))
            GetEaten();
    }
}
