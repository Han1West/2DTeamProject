using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    private OrderManager theOrder;
    private PlayerStat thePlayerStat;
    private Inventory theInven;

    private const int WEAPON = 0;
    // Start is called before the first frame update
    void Start()
    {
        theOrder = FindObjectOfType<OrderManager>();
        thePlayerStat = FindObjectOfType<PlayerStat>();
        theInven = FindObjectOfType<Inventory>();
    }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
