using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOfWarBehavior : MonoBehaviour
{
    public Transform player;
    public float radius = 5f;

    private void Update()
    {
        transform.position = new Vector3(player.position.x, player.position.y, player.position.z);

        transform.localScale = new Vector3(radius, radius, 1);
    }
}
