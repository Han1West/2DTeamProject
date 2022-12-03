using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangjoFirst: MonoBehaviour
{

    public Dialogue dialogue_1;
    public Dialogue dialogue_2;
    public Dialogue dialogue_3;

    private DialogueManager theDM;
    private OrderManager theOrder;
    private PlayerManager thePlayer;

    public GameObject npc3;
    public GameObject npc4;
    public GameObject npc5;
    public GameObject next;

    private bool flag;

    // Start is called before the first frame update
    void Start()
    {
        theDM = FindObjectOfType<DialogueManager>();
        theOrder = FindObjectOfType<OrderManager>();
        thePlayer = FindObjectOfType<PlayerManager>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!flag )
        {
            flag = true;            
            StartCoroutine(FirstEventCoroutine());
        }
    }

    IEnumerator FirstEventCoroutine()
    {
        theOrder.PreLoadCharacter();
       
        npc3.SetActive(true);
        npc4.SetActive(true);
        npc5.SetActive(true);
        theOrder.NotMove(); //이벤트 시작시 이동불가
        

        theOrder.Turn("NPC3", "UP");
        theOrder.Turn("NPC4", "UP");
        theOrder.Turn("NPC5", "UP");
        theOrder.Move("Player", "UP");
        theOrder.Move("Player", "UP");
        theOrder.Move("Player", "UP");
        yield return new WaitUntil(() => thePlayer.queue.Count == 0);

        theDM.ShowDialogue(dialogue_1);
        yield return new WaitUntil(() => !theDM.talking);

        theDM.ShowDialogue(dialogue_2);
        yield return new WaitUntil(() => !theDM.talking);

        theDM.ShowDialogue(dialogue_3);
        yield return new WaitUntil(() => !theDM.talking);

        npc3.SetActive(false);
        npc4.SetActive(false);
        npc5.SetActive(false);
        next.SetActive(true);

        theOrder.Move(); //이벤트 종료시 이동가능
    }
}
