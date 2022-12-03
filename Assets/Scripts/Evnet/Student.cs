using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Student : MonoBehaviour
{

    public Dialogue dialogue_1;
    public Dialogue dialogue_2;
    public Dialogue dialogue_3;
    public Dialogue dialogue_4;

    private PlayerStat thePlayerStat;
    private DialogueManager theDM;
    private OrderManager theOrder;
    private PlayerManager thePlayer;

    public int count = 0;

    private bool flag;

    // Start is called before the first frame update
    void Start()
    {
        thePlayerStat = FindObjectOfType<PlayerStat>();
        theDM = FindObjectOfType<DialogueManager>();
        theOrder = FindObjectOfType<OrderManager>();
        thePlayer = FindObjectOfType<PlayerManager>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!flag && Input.GetKey(KeyCode.F) && thePlayer.animator.GetFloat("DirY") == 1f)
        {
            count += 1;
            StartCoroutine(EventCoroutine());
        }
    }

    void Update()
    {
            
    }

    IEnumerator EventCoroutine()
    {
        theOrder.PreLoadCharacter();
        theOrder.NotMove(); //이벤트 시작시 이동불가


        switch (count)
        {
            case 0:
                theDM.ShowDialogue(dialogue_1);
                yield return new WaitUntil(() => !theDM.talking);
                break;
            case 1:
                theDM.ShowDialogue(dialogue_2);
                yield return new WaitUntil(() => !theDM.talking);
                break;
            case 2:
                theDM.ShowDialogue(dialogue_3);
                yield return new WaitUntil(() => !theDM.talking);
                break;
            case 3:
                theDM.ShowDialogue(dialogue_4);
                yield return new WaitUntil(() => !theDM.talking);
                thePlayerStat.Hit(thePlayerStat.hp);
                break;

        }

        theOrder.Move(); //이벤트 종료시 이동가능
    }
}
