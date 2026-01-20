#nullable enable

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    // for following the player
    public Vector3 offsetFromPlayer;

    // for ammo
    public float fireForce = 10f;

    [SerializeField] public List<Transform> SingleFP = new List<Transform>();
    [SerializeField] public List<Transform> TripleStraightFP = new List<Transform>();
    [SerializeField] public List<Transform> TripleAngledFP = new List<Transform>();
    [SerializeField] public List<Transform> TripleAngledDoubleLayerFP = new List<Transform>();
    [SerializeField] public List<Transform> TwinSnakesFP = new List<Transform>();

    private List<Transform> CurFirePoints = new List<Transform>();

    void Start()
    {
        // stick to the player
        transform.SetParent(GameManager.Instance?.PlayerTransform);

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
            yield return GameManager.Instance?.FireAmmoInterval ?? new WaitForSeconds(1f);
        }
    }

    public void Fire()
    {
        CurFirePoints = GetFirePoints();

        Debug.Log($"WeaponController - firing from {CurFirePoints.Count} fire points");

        // for each fire point
        for (var i = 0; i < CurFirePoints.Count; i++)
        {
            var bulletPrefab = GameManager.Instance.CurAmmoPrefab;
            if (bulletPrefab == null) return;

            var firePoint = CurFirePoints[i];

            // create an ammo prefab
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, GameManager.Instance.MousePosition, transform);

            // fire the ammo prefab
            bullet.GetComponent<Rigidbody>().AddForce(firePoint.forward * fireForce, ForceMode.Impulse);
        }
        
    }

    private List<Transform> GetFirePoints()
    {
        switch (GameManager.Instance.CurAmmoSpread)
        {
            default:
            case AmmoSpread.Single:
                return SingleFP;

            case AmmoSpread.TripleStraight:
                return TripleStraightFP;

            case AmmoSpread.TripeAngled:
                return TripleAngledFP;

            case AmmoSpread.TripeAngledDoubleLayer:
                return TripleAngledDoubleLayerFP;

            case AmmoSpread.TwinSnakes:
                return TwinSnakesFP;
        }
    }
}
