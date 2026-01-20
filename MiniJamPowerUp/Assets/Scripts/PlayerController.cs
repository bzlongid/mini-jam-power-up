#nullable enable

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // tunable stuff
    public float MovementSpeed = 5f;
    public float TurnDamping = 15f;

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
        GameManager.Instance.PlayerRB.MovePosition(GameManager.Instance.PlayerRB.position + movement);
    }

    private void FaceMouse()
    {
        var playerRB = GameManager.Instance.PlayerRB;
        if (playerRB != null)
        {
            playerRB.MoveRotation(
            Quaternion.Slerp(playerRB.rotation, GameManager.Instance.MousePosition, TurnDamping * Time.fixedDeltaTime)
            );
        }
        else
        {
            Debug.LogError("PlayerController - Player Rigid Body is null! Ensure the player object is set in the game manager.");
        }
        
    }
}
