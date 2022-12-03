using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieEvent : MonoBehaviour
{
    private OrderManager theOrder;
    private DialogueManager theDM;

    public Dialogue dialogue_1;

    public GameObject monster;
    public int count;

    public bool flag;
    // Start is called before the first frame update
    void Start()
    {
        theDM = FindObjectOfType<DialogueManager>();
        theOrder = FindObjectOfType<OrderManager>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!flag && collision.gameObject.name == "Player")
        {
            flag = true;
            StartCoroutine(DiaCoroutine());
            theOrder.Move();
            var clone = Instantiate(monster, PlayerManager.instance.transform.position, Quaternion.Euler(Vector3.zero));
        }
    }


    IEnumerator DiaCoroutine()
    {
        theOrder.NotMove();
        theDM.ShowDialogue(dialogue_1);
        yield return new WaitUntil(() => !theDM.talking);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
