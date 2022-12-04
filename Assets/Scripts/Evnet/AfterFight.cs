using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AfterFight : MonoBehaviour
{

    public Dialogue dialogue_1;
    public Dialogue dialogue_2;
    public Dialogue dialogue_3;
    public Dialogue dialogue_4;
    public Dialogue dialogue_5;
    public Dialogue dialogue_6;
    public Dialogue dialogue_7;
    public Dialogue dialogue_8;
    public Dialogue dialogue_9;

    private FadeManager theFade;
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
        theFade = FindObjectOfType<FadeManager>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!flag)
        {
            npc3.SetActive(true);
            npc4.SetActive(true);
            npc5.SetActive(true);
            flag = true;
            StartCoroutine(FirstEventCoroutine());
        }
    }

    IEnumerator FirstEventCoroutine()
    {
        theOrder.PreLoadCharacter();
        theOrder.NotMove(); //이벤트 시작시 이동불가


        theOrder.Turn("NPC5", "RIGHT");
        theOrder.Turn("NPC3", "RIGHT");
        theOrder.Turn("NPC4", "RIGHT");
        theOrder.Turn("Player", "UP");
        theOrder.Turn("Player", "LEFT");
        theOrder.Turn("Player", "RIGHT");

        yield return new WaitForSeconds(1f);

        theDM.ShowDialogue(dialogue_1);
        yield return new WaitUntil(() => !theDM.talking);

        theDM.ShowDialogue(dialogue_2);
        yield return new WaitUntil(() => !theDM.talking);

        theOrder.Turn("NPC4", "UP");
        theDM.ShowDialogue(dialogue_3);
        yield return new WaitUntil(() => !theDM.talking);
        theOrder.Turn("NPC4", "RIGHT");

        theOrder.Move("NPC5", "RIGHT");
        theDM.ShowDialogue(dialogue_4);
        yield return new WaitUntil(() => !theDM.talking);

        theDM.ShowDialogue(dialogue_5);
        yield return new WaitUntil(() => !theDM.talking);

        theOrder.Move("NPC4", "RIGHT");
        theDM.ShowDialogue(dialogue_6);
        yield return new WaitUntil(() => !theDM.talking);
 

        theDM.ShowDialogue(dialogue_7);
        yield return new WaitUntil(() => !theDM.talking);

        theDM.ShowDialogue(dialogue_8);
        yield return new WaitUntil(() => !theDM.talking);

        theDM.ShowDialogue(dialogue_9);
        yield return new WaitUntil(() => !theDM.talking);


        npc3.SetActive(false);
        npc4.SetActive(false);
        npc5.SetActive(false);

        theOrder.Move(); //이벤트 종료시 이동가능
        next.SetActive(true);
    }
}
