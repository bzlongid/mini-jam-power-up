using System.Collections;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    // for following the player
    public Transform playerObject;
    public Vector3 offsetFromPlayer;

    // for ammo
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireForce = 20f;

    void Start()
    {
        // stick to the player
        transform.SetParent(playerObject);

        // fire ammo
        StartCoroutine(RepeatFireAmmo());
    }

    IEnumerator RepeatFireAmmo()
    {
        while (true)
        {
            // will execute every 'fireAmmoInterval' seconds
            Fire();

            // wait 'fireAmmoInterval' seconds before next iteration
            yield return GameManager.Instance.FireAmmoInterval;
        }
    }

    public void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, GameManager.Instance.MousePosition, transform);
        bullet.GetComponent<Rigidbody>().AddForce(firePoint.forward * fireForce, ForceMode.Impulse);
    }
}
