using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // tunable stuff
    public float MovementSpeed = 5f;

    // refrences
    [SerializeField] Rigidbody playerRB;

    // private vars
    private Vector2 movementInput;

    // signature used by the new Player Input component when using "Send Messages", do not change
    public void OnMove(InputValue value)
    {
        movementInput = value.Get<Vector2>();
    }

    void FixedUpdate()
    {
        Move();
        FaceMouse();
    }

    private void Move()
    {
        Vector3 movement = new Vector3(movementInput.x, 0f, movementInput.y) * MovementSpeed * Time.fixedDeltaTime;
        playerRB.MovePosition(playerRB.position + movement);
    }

    private void FaceMouse()
    {
        playerRB.MoveRotation(GameManager.Instance.MousePosition);
    }
}
