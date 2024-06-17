using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPoint : MonoBehaviour
{
    [SerializeField] TeleportPoint destination;

    private Player player;
    private bool justTeleported;

    private void Start() {
        player = Player.Instance;
    }

    private void Update() {
        Vector3 playerPos = player.transform.position;
        if (justTeleported) {
            if (Math.Abs(playerPos.x - transform.position.x) > 2.0f) {
                justTeleported = false;
            } else if(Math.Abs(playerPos.z - transform.position.z) > 2.0f) {
                justTeleported = false;
            }
        }

        if(Math.Abs(playerPos.x - transform.position.x) < 0.5f) {
            if(Math.Abs(playerPos.z - transform.position.z) < 0.5f) {
                if(!justTeleported) {
                    player.transform.position = new Vector3(destination.transform.position.x, 0.0f, destination.transform.position.z);
                    destination.justTeleported = true;
                }
            }
        }
    }
}
