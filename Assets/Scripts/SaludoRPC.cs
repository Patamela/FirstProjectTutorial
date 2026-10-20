using UnityEngine;
using Fusion;
using UnityEngine.InputSystem;

public class SaludoRPC : NetworkBehaviour
{
    // Update para tecla H
    void Update()
    {
        if (Keyboard.current != null &&
            Keyboard.current.hKey.wasPressedThisFrame)
        {
            string idPlayer = Runner.LocalPlayer.ToString();

            RPC_Greet("Hola, soy "+idPlayer);
        }
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.Proxies)]
    public void RPC_Greet(string message)
    {
        SayHello ui = FindAnyObjectByType<SayHello>();

        if(ui != null)
        {
           ui.ShowMessage(message); 
        }

    }
}
