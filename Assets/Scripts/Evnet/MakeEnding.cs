using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeEnding : MonoBehaviour
{
    private DatabaseManager theDatabase;

    public GameObject go;

    private bool flag;
    // Start is called before the first frame update
    void Start()
    {
        theDatabase = FindObjectOfType<DatabaseManager>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (theDatabase.switches[1] && !flag)
        {
            flag = true;
            go.SetActive(true);
        }
            
    }
}
