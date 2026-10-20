using UnityEngine;
using Fusion;
using UnityEngine.InputSystem;

public class PlayerColor : NetworkBehaviour
{
    public MeshRenderer MeshRenderer;
    private int healthPoints = 50;
    private int hit;

    [Networked, OnChangedRender(nameof(ColorChanged))] public Color NetworkedColor {get; set;}

    private void Update()
    {
        if(HasStateAuthority == false) return;
        hit  = Random.Range(1,5);

        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            NetworkedColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1f);
        }

        if (Keyboard.current.iKey.wasPressedThisFrame)
        {   
            healthPoints -= hit;
        }

        Debug.Log(healthPoints);

        switch (healthPoints)
        {
            case <= 10:
                NetworkedColor = new Color(1f,0f,0f, 1f);
            break;
        }
    }

    private void ColorChanged()
    {
        MeshRenderer.material.color = NetworkedColor;
    }

}
