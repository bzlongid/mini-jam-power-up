#nullable enable

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/*
 *  Controlls the state of the following: 
 *      wave (enemy) information, 
 *      weapon information, 
 *      mouse position
 */

public class GameManager : MonoBehaviour
{
    public static GameManager? Instance { get; private set; } = null!;

    // constants
    public const int MAX_WAVES = 7;

    [Header("Object Refrences")]
    [SerializeField] Camera? mainCamera;
    [SerializeField] GameObject playerGameObject;

    [Header("Enemy")]
    [SerializeField] private int[] enemiesPerWave = new int[MAX_WAVES] { 5, 10, 15, 20, 25, 30, 50 };
    [SerializeField] private WaitForSeconds enemySpawnRate = new WaitForSeconds(1f);
    [SerializeField] private List<Transform> enemySpawnPoints = new List<Transform>();
    [SerializeField] private float enemySpeed = 3f;

    [Header("Weapon")]
    public Weapon StartingWeapon = Weapon.Single;

    [Header("Ammo")]
    [SerializeField] private List<AmmoMapping> ammoMappings = new List<AmmoMapping>();
    [Serializable]
    public class AmmoMapping
    {
        public AmmoPrefab AmmoType;
        public GameObject? AmmoPrefab;
    }
    private Dictionary<AmmoPrefab, GameObject> ammoDict = new Dictionary<AmmoPrefab, GameObject>();

    #region GLOBAL VARS
    public Quaternion MousePosition { get; private set; }

    public Transform? PlayerTransform { get; private set; }
    public Rigidbody? PlayerRB { get; private set; }

    public int CurWave { get; private set; } = 1;
    public int EnemiesInWave { get; private set; }
    public float EnemySpeed { get; private set; }
    public int EnemiesLeft { get; private set; }
    public int CurEnemySpawnPointIndex { get; private set; }

    public Weapon CurWeapon { get; private set; }
    public GameObject? CurAmmoPrefab { get; private set; }
    public AmmoSpread CurAmmoSpread { get; private set; }
    public WaitForSeconds FireAmmoInterval { get; private set; } = new WaitForSeconds(1f);
    #endregion

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;

        // build ammo prefab refs - can break this out into another manager
        ammoDict = new Dictionary<AmmoPrefab, GameObject>();
        foreach (var mapping in ammoMappings)
        {
            if(mapping.AmmoPrefab != null)
                ammoDict[mapping.AmmoType] = mapping.AmmoPrefab;
        }

        UpdateMosePosition();

        PlayerTransform = playerGameObject.transform;
        PlayerRB = playerGameObject.GetComponent<Rigidbody>();

        SetWeapon(StartingWeapon);

        EnemySpeed = enemySpeed;
        // start wave

        // Optional: Keep the object alive when loading new scenes
        //DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        UpdateMosePosition();
    }

    public void OnEnemyEliminated()
    {
        EnemiesLeft--;

        if (EnemiesLeft <= 0) OnWaveEnded();
    }

    private void OnWaveEnded()
    {
        // put anything to signal the wave end here i.e. sound, cutscene, animations, etc.

        // update the wave info
        CurWave++;
        if (CurWave > MAX_WAVES)
        {
            OnGameEnded();
            return;
        }

        EnemiesInWave = enemiesPerWave[CurWave];
        EnemiesLeft = EnemiesInWave;

        // update the player weapon
        var weaponTypes = Enum.GetValues(typeof(Weapon));
        if (weaponTypes.Length >= CurWave)
        {
            SetWeapon((Weapon)weaponTypes.GetValue(CurWave));
        }
        else
        {
            Debug.LogError($"GameManager - Could not find a weapon for wave {CurWave}! Defaulting to game end...");
            OnGameEnded();
            return;
        }
    }

    private void OnGameEnded()
    {

    }

    private void UpdateMosePosition()
    {
        if (mainCamera == null)
        {
            Debug.LogError("GameManager - main camera is missing!");
            return;
        }

        // create a plane at player's Y position (XZ plane)
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (playerPlane.Raycast(ray, out float distance))
        {
            Vector3 hitPoint = ray.GetPoint(distance);
            Vector3 direction = hitPoint - transform.position;
            direction.y = 0f; // keep only horizontal rotation

            if (direction.sqrMagnitude > 0.001f)
            {
                MousePosition = Quaternion.LookRotation(direction);
            }
        }
    }

    private void SetWeapon(Weapon weapon)
    {
        // update the state of the weapon
        CurWeapon = weapon;
        CurAmmoPrefab = ammoDict[CombatVariables.WeaponPrefabMap[weapon]];
        CurAmmoSpread = CombatVariables.WeaponSpreadMap[weapon];
        FireAmmoInterval = CombatVariables.WeaponReloadTimeMap[weapon];
    }
}
