using UnityEngine;
using UnityEngine.InputSystem;

public class SayHello : MonoBehaviour
{
    public string msg = "¡Hola a todos!";

    private float finalTime = 0f;

    public void ShowMessage(string message)
    {
        msg = message;
        finalTime = Time.time + 3f;
    }

    void OnGUI()
    {
        if (Time.time < finalTime)
        {
            GUI.Box(new Rect(20, 20, 350, 50),msg);
        }
    }
}