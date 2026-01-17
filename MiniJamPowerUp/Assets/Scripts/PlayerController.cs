using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector2 _movementInput;
    public float movementSpeed = 5f;

    // This function is called automatically by the Player Input component when using "Send Messages"
    public void OnMove(InputValue value)
    {
        // Read the Vector2 value from the input
        //_movementInput = context.ReadValue<Vector2>();

        var v = value.Get<Vector2>();
        _movementInput = v;
    }

    void Update()
    {
        // Use the input value in the Update method for smooth movement
        Vector3 moveDirection = new Vector3(_movementInput.x, 0, _movementInput.y);
        transform.Translate(moveDirection * movementSpeed * Time.deltaTime);
    }
}
