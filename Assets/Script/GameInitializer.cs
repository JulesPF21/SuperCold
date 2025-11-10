using UnityEngine;
using UnityEngine.InputSystem;

public class GameInitializer : MonoBehaviour
{
    public PlayerInputManager playerInputManager;

    void Start()
    {
        Debug.Log("Nombre d’écrans détectés : " + Display.displays.Length);
        // Si la première manette est détectée
        if (Gamepad.all.Count > 0)
        {
            playerInputManager.JoinPlayer(0, -1, null, Gamepad.all[0]);
        }

        // Si la deuxième manette est détectée
        if (Gamepad.all.Count > 1)
        {
            playerInputManager.JoinPlayer(1, -1, null, Gamepad.all[1]);
        }
    }
}