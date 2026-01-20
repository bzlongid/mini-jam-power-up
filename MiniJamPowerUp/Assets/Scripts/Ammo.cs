#nullable enable

using UnityEngine;

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
