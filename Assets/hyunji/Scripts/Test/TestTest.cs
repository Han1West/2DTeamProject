using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTest : MonoBehaviour
{

    private OrderManager theOrder;
    private DialogueManager theDM;

    public bool flag;
    public string[] texts;

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
            StartCoroutine(ACoroutine());
        }
    }

    IEnumerator ACoroutine()
    {
        flag = true;
        theOrder.NotMove();
        theDM.ShowText(texts);
        yield return new WaitUntil(() => !theDM.talking);
        theOrder.Move();
    }
}
