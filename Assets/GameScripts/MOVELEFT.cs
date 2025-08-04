using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MOVELEFT : MonoBehaviour
{
    private float speedx = 30;
    private PlayerController playerControllerScript;
    // Start is called before the first frame update

    public SpawnManager spawnManager;
    void Start()
    {
        playerControllerScript= GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControllerScript.gameOver == false)
        {
            transform.Translate(Vector3.left* Time.deltaTime*speedx);
        }
        
    }
}
