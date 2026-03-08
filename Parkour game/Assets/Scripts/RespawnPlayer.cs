using UnityEngine;

public class RespawnPlayer : MonoBehaviour
{
    GameObject player;
    public Transform spawnPos;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            player.transform.position = spawnPos.position;
        }
    }
}
