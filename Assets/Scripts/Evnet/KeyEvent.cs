using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyEvent : MonoBehaviour
{
    public string dropSound;

    public Dialogue dialogue_1;
    public Dialogue dialogue_2;

    private DialogueManager theDM;
    private OrderManager theOrder;
    private PlayerManager thePlayer;
    private AudioManger theAudio;

    public GameObject npc3;
    public GameObject npc4;
    public GameObject npc5;
    public GameObject go;

    private bool flag;

    // Start is called before the first frame update
    void Start()
    {
        theAudio = FindObjectOfType<AudioManger>();
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
            StartCoroutine(OpenCoroutine());
        }
    }

    IEnumerator OpenCoroutine()
    {
        theOrder.NotMove();

        npc3.SetActive(true);
        npc4.SetActive(true);
        npc5.SetActive(true);

        theOrder.Turn("NPC3", "DOWN");
        theOrder.Turn("NPC4", "DOWN");
        theOrder.Turn("NPC5", "DOWN");

        theAudio.Play(dropSound);
        go.SetActive(true);
        yield return new WaitForSeconds(0.5f);

        theOrder.Turn("NPC3", "UP");
        theOrder.Turn("NPC4", "UP");
        theOrder.Turn("NPC5", "UP");

        yield return new WaitForSeconds(0.5f);
        theDM.ShowDialogue(dialogue_1);
        yield return new WaitUntil(() => !theDM.talking);

        theDM.ShowDialogue(dialogue_2);
        yield return new WaitUntil(() => !theDM.talking);


        npc3.SetActive(false);
        npc4.SetActive(false);
        npc5.SetActive(false);

        theOrder.Move();
    }
}
