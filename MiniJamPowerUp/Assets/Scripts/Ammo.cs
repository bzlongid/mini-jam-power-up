using UnityEngine;
using UnityEngine.InputSystem;

public class Ammo : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(GameObjectTags.ENEMY))
        {
            Destroy(gameObject);
        }
    }
}
