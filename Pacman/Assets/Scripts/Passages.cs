using UnityEngine;

public class Passages : MonoBehaviour
{
    [SerializeField] Transform _connectionTransform;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.transform.position = new Vector3(_connectionTransform.position.x, _connectionTransform.position.y, collision.transform.position.z);
    }

}
