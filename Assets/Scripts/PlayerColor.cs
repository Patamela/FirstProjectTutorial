using UnityEngine;
using Fusion;
using UnityEngine.InputSystem;

public class PlayerColor : NetworkBehaviour
{
    public  MeshRenderer MeshRenderer;
    private int healthPoints = 50;
    private int hit;

    [Networked, OnChangedRender(nameof(ColorChanged))] public Color NetworkedColor {get; set;}

    public override void Spawned()
    {
        NetworkedColor = new Color(0f, 1f, 0f, 1f);
        ColorChanged();
    }

    private void Update()
    {
        if(HasStateAuthority == false) return;
        hit  = Random.Range(1,5);

        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            NetworkedColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1f);
        }

        if (Keyboard.current.iKey.wasPressedThisFrame && healthPoints >= 0)
        {   
            healthPoints -= hit;
            Color colorActual = NetworkedColor;

            colorActual.r = Mathf.Clamp01(colorActual.r+0.05f);
            colorActual.g = Mathf.Clamp01(colorActual.g - 0.05f);

            NetworkedColor = colorActual;
        }
    }

    private void ColorChanged()
    {
        MeshRenderer.material.color = NetworkedColor;
    }

}
