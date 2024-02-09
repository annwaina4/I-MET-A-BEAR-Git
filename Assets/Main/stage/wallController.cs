using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallController : MonoBehaviour
{
    GameObject player;
    private float wallDistance;

    void Start()
    {
        this.player = GameObject.Find("player");
        this.wallDistance = player.transform.position.x - this.transform.position.x;
    }

    void Update()
    {
        Vector3 playerPos = this.player.transform.position;

        transform.position = new Vector3(playerPos.x - wallDistance, transform.position.y, transform.position.z);
    }
}
