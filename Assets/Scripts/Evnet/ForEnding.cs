using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForEnding : MonoBehaviour
{

    private OrderManager theOrder;
    private NumberSystem theNumber;
    private DialogueManager theDM;
    private AudioManger theAudio;

    public string cancelSound;

    public Dialogue dialogue_1;
    public Dialogue dialogue_2;

    public bool flag;
    public int correctNumber;
    public bool result;

    public GameObject go;
    public GameObject npc3;
    public GameObject npc4;
    public GameObject npc5;
    // Start is called before the first frame update
    void Start()
    {
        theOrder = FindObjectOfType<OrderManager>();
        theNumber = FindObjectOfType<NumberSystem>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!flag && collision.gameObject.name == "Player")
        {
            StartCoroutine(ACoroutine());
            if(!result)
            {
                theAudio.Play(cancelSound);
            }
        }
    }


    void Update()
    {
        if(result)
        {
            flag = true;
            go.SetActive(true);
        }
    }

    IEnumerator ACoroutine()
    {
        theOrder.NotMove();
        theNumber.ShowNumber(correctNumber);
        yield return new WaitUntil(() => !theNumber.activated);
        result = theNumber.GetResult();
        theOrder.Move();
    }

}
