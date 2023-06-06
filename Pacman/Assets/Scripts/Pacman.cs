using UnityEngine;

public class Pacman : MonoBehaviour
{
    [SerializeField] Movement _movement;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            _movement.SetDirection(Vector2.up);
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            _movement.SetDirection(Vector2.down);
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            _movement.SetDirection(Vector2.left);
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            _movement.SetDirection(Vector2.right);
        }

        if (Input.GetKeyDown(KeyCode.Space))
            GameManager.Instance.DebugPellet();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Node"))
            _movement.TryNextDirection();
    }
}
