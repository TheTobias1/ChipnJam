using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static PlayerInput inputBuffer;

    public static void SetPlayerInput(PlayerInput input)
    {
        inputBuffer = input;
    }

    public static PlayerInput GetPlayerInput()
    {
        return InputManager.inputBuffer;
    }

    private void Update()
    {
        InputManager.inputBuffer = PollInput();
    }

    protected virtual PlayerInput PollInput()
    {
        PlayerInput input = new PlayerInput();

        input.moveInput =  new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        input.jump = Input.GetButton("Jump");
        input.attack = Input.GetButton("Fire1");
        input.ability = Input.GetButton("Fire2");
        input.aim = Input.GetButton("Fire3");

        Vector2 mouseInput = new Vector2();

        Vector3 mousePosition = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            mousePosition = hit.point;
        }
        else
        {
            mousePosition = ray.origin + ray.direction * 25;
        }

        mouseInput = new Vector2(mousePosition.x, mousePosition.z) - new Vector2(transform.position.x, transform.position.z);
        input.mouseInput = mouseInput;

        return input;
    }


}


public struct PlayerInput
{
    public Vector2 moveInput;
    public bool jump;
    public bool attack;
    public bool ability;
    public bool aim;
    public Vector2 mouseInput;
}