using UnityEngine;

public class GhostEyes : MonoBehaviour
{
    [SerializeField] Movement _movement;
    [SerializeField] Sprite[] _eyeSprites;
    [SerializeField] SpriteRenderer _eyeSpriteRenderer;
    private void OnEnable()
    {
        _movement.OnDirectionChanged += HandleOnDirectionChanged;
    }
    void HandleOnDirectionChanged()
    {
        if(_movement.CurrentDir == Vector2.left)
        {
            _eyeSpriteRenderer.sprite = _eyeSprites[0];
        }
        else if(_movement.CurrentDir == Vector2.right)
        {
            _eyeSpriteRenderer.sprite = _eyeSprites[1];
        }
        else if(_movement.CurrentDir == Vector2.down) 
        {
            _eyeSpriteRenderer.sprite = _eyeSprites[2];
        }
        else
        {
            _eyeSpriteRenderer.sprite = _eyeSprites[3];
        }
    }
}
