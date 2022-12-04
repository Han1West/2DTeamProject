using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    private PlayerStat thePlayerStat;



    public int atk;
    public float attackDelay;


    // Start is called before the first frame update
    void Start()
    {

        thePlayerStat = FindObjectOfType<PlayerStat>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.name == "Player")
        {
            thePlayerStat.Hit(atk);
        }
    }


    // Update is called once per frame
    void Update()
    {

    }

}
