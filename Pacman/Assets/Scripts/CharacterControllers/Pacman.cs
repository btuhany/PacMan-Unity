using System.Collections;
using UnityEngine;

public class Pacman : MonoBehaviour
{
    [SerializeField] Movement _movement;
    [SerializeField] Flip _flip;
    [SerializeField] Animator _anim;
    bool _canReadInput = true;

    public Movement Movement { get => _movement; }

    private void OnEnable()
    {
        _movement.OnDirectionChanged += HandleOnDirectionChanged;
    }
    private void Update()
    {
        if (!_canReadInput) return;
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


        if (_movement.IsStopeed)
            _anim.SetBool("Stop", true);
        else
            _anim.SetBool("Stop", false);


    }

    public void ResetState()
    {
        _anim.SetBool("Reset", true);
        _movement.Rb.simulated = true;
        _movement.ResetState();
        _flip.RotateSprite(Vector2.right);
        _canReadInput = true;
    }
    void HandleOnDirectionChanged()
    {  
        _flip.RotateSprite(_movement.CurrentDir);
    }
    public void Eaten()
    {
        _canReadInput = false;
        _movement.StopMovement();
        _movement.Rb.simulated = false;
        _movement.IsActive = false;
        _anim.SetTrigger("Eaten");
    }
    public void GameFinished()
    {
        _movement.StopMovement();
        _movement.IsActive = false;
        _anim.SetTrigger("Finished");
    }
    public void EatenAnimationEvent()
    {
        this.gameObject.SetActive(false);
    }
}
