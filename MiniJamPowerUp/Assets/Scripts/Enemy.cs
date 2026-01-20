using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.PlayerTransform != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, 
                GameManager.Instance.PlayerTransform.position,
                GameManager.Instance.EnemySpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(GameObjectTags.AMMO))
        {
            Destroy(gameObject);
            GameManager.Instance.OnEnemyEliminated();
        }
    }
}
