using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    GameObject player;
    private float cameraDistance;
    void Start()
    {
        this.player=GameObject.Find("player");

        this.cameraDistance = player.transform.position.x - this.transform.position.x;
    }

    
    void Update()
    {
        this.transform.position
      +=new Vector3(((player.transform.position.x-cameraDistance)-this.transform.position.x)*Time.deltaTime*3,0,0);
    }
}
