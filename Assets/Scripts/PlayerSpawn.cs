using UnityEngine;
using Fusion;

public class PlayerSpawn : SimulationBehaviour, IPlayerJoined
{
    public GameObject PlayerPrefab;
    
    // Si "MI" jugador entra en la partida, lo muestro

    void IPlayerJoined.PlayerJoined(PlayerRef player){

        if(player == Runner.LocalPlayer){
            Runner.Spawn(PlayerPrefab, new Vector3(Random.Range(-5,5),1,0), Quaternion.identity);
        }
    }

}
