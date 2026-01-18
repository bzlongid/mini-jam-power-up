#nullable enable

using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager? Instance { get; private set; }

    // refrences
    [SerializeField] Camera? mainCamera;

    // global vars
    public Quaternion MousePosition;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;

        UpdateMosePosition();

        // Optional: Keep the object alive when loading new scenes
        //DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        UpdateMosePosition();
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
            direction.y = -90f; // keep only horizontal rotation

            if (direction.sqrMagnitude > 0.001f)
            {
                MousePosition = Quaternion.LookRotation(direction);
            }
        }
    }
}
