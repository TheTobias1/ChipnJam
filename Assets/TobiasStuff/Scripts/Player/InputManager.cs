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
}