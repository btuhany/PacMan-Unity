using UnityEngine;

public class Pacman : MonoBehaviour
{
    [SerializeField] Movement _movement;
    [SerializeField] Flip _flip;
    private void OnEnable()
    {
        _movement.OnDirectionChanged += HandleOnDirectionChanged;
    }
    private void Update()
    {
        if (!_movement.IsActive && Input.anyKeyDown)
        {
            _movement.IsActive = true;
        }
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
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Node"))
            _movement.TryNextDirection();
    }
    public void ResetState()
    {
        _movement.ResetState();
        _flip.RotateSprite(Vector2.right);


    }
    void HandleOnDirectionChanged()
    {
        _flip.RotateSprite(_movement.CurrentDir);
    }
}
