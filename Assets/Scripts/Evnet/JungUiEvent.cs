using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JungUiEvent : MonoBehaviour
{

    public Dialogue dialogue_1;
    public Dialogue dialogue_2;
    public Dialogue dialogue_3;
    public Dialogue dialogue_4;

    public Dialogue dialogue_11;
    public Dialogue dialogue_22;
    public Dialogue dialogue_33;
    public Dialogue dialogue_44;
    public Dialogue dialogue_55;

    private DialogueManager theDM;
    private OrderManager theOrder;
    private PlayerManager thePlayer;

    public GameObject npc11;


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
        if (!flag)
        {
            flag = true;
            StartCoroutine(FirstEventCoroutine());
        }
    }

    IEnumerator FirstEventCoroutine()
    {
        theOrder.PreLoadCharacter();

        theOrder.NotMove(); //이벤트 시작시 이동불가


        theOrder.Turn("NPC3", "UP");
        theOrder.Turn("NPC4", "UP");
        theOrder.Turn("NPC5", "UP");
        theOrder.Move("Player", "UP");
        theOrder.Move("Player", "UP");
        theOrder.Move("Player", "UP");
        theOrder.Move("Player", "UP");
        theOrder.Move("Player", "UP");

        yield return new WaitUntil(() => thePlayer.queue.Count == 0);

        theDM.ShowDialogue(dialogue_1);
        yield return new WaitUntil(() => !theDM.talking);

        theDM.ShowDialogue(dialogue_2);
        yield return new WaitUntil(() => !theDM.talking);
        npc11.SetActive(true);
        theDM.ShowDialogue(dialogue_3);
        yield return new WaitUntil(() => !theDM.talking);

        theDM.ShowDialogue(dialogue_4);
        yield return new WaitUntil(() => !theDM.talking);

        theOrder.Move(); //이벤트 종료시 이동가능

        StartCoroutine(EndingEventCoroutine());
    }

    IEnumerator EndingEventCoroutine()
    {
        theOrder.PreLoadCharacter();
        theOrder.NotMove(); //이벤트 시작시 이동불가


        yield return new WaitUntil(() => thePlayer.queue.Count == 0);

        theDM.ShowDialogue(dialogue_11);
        yield return new WaitUntil(() => !theDM.talking);

        theDM.ShowDialogue(dialogue_22);
        yield return new WaitUntil(() => !theDM.talking);

        theDM.ShowDialogue(dialogue_33);
        yield return new WaitUntil(() => !theDM.talking);

        theDM.ShowDialogue(dialogue_44);
        yield return new WaitUntil(() => !theDM.talking);

        theDM.ShowDialogue(dialogue_55);
        yield return new WaitUntil(() => !theDM.talking);


        theOrder.Move(); //이벤트 종료시 이동가능
        //SceneManager.LoadScene("Start");
    }
}
