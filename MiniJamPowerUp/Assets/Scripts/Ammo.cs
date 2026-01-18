using UnityEngine;
using UnityEngine.InputSystem;

public class Ammo : MonoBehaviour
{
    private void Start()
    {
        transform.rotation = Quaternion.Euler(180f, 90f, 0f); // Sets world rotation

    }

    private void OnCollisionEnter(Collision collision)
    {
        //Destroy(gameObject);

        // CHECK IF ENEMY WAS HIT
        // DAMAGE ENEMY
    }
}
