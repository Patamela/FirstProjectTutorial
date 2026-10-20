using Fusion;
using UnityEngine;

public class CharacterHealth : NetworkBehaviour
{
    private PlayerColor playerColor;
    [Networked] public int Health { get; set; } = 100;

    public override void Spawned()
    {
        playerColor = GetComponent<PlayerColor>();
    }


    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RPC_TakeDamage(int damage)
    {

        string idPlayer = Runner.LocalPlayer.ToString();
        Health = Mathf.Max(0, Health-damage);

        playerColor?.UpdateColor();

        SayHello ui = FindAnyObjectByType<SayHello>();

        if (ui != null && Health==0)
        {
            ui.ShowMessage("HAS MUERTO "+idPlayer);
        }
    }

}
