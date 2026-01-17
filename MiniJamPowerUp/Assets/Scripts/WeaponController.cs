using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Transform playerObject;
    public Vector3 offsetFromPlayer;

    void Start()
    {
        // stick to the player
        transform.SetParent(playerObject);
    }
}
