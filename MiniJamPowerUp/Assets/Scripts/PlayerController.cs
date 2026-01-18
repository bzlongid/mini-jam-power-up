using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // tunable stuff
    public float MovementSpeed = 5f;

    // refrences
    [SerializeField] Rigidbody playerRB;
    [SerializeField] Camera mainCamera;

    // private vars
    private Vector2 _movementInput;

    // used by the new Player Input component when using "Send Messages"
    public void OnMove(InputValue value)
    {
        _movementInput = value.Get<Vector2>();
    }

    void FixedUpdate()
    {
        Move();
        RotateTowardsMouse();
    }

    private void Move()
    {
        Vector3 movement = new Vector3(_movementInput.x, 0f, _movementInput.y) * MovementSpeed * Time.fixedDeltaTime;
        playerRB.MovePosition(playerRB.position + movement);
    }

    private void RotateTowardsMouse()
    {
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
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                playerRB.MoveRotation(targetRotation);
            }
        }
    }
}
