using UnityEngine;

public class HomeNode : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Ghost"))
        {
            collision.GetComponent<Ghost>().Movement.ChangeDirection(Vector2.up);
            //if(collision.TryGetComponent<PinkyGhost>(out PinkyGhost ghost))
        }
    }
}
