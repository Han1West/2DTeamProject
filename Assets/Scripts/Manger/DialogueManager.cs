using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    #region Singleton
    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    #endregion Singleton
    public TMP_Text text;
    public SpriteRenderer rendererDialogueWindow;

    private List<string> listName;
    private List<string> listSentences;
    private List<Sprite> listDialogueWindows;

    private int count; //대화 진행 상황 카운트

    public Animator animDialogueWindow;

    public string keySound;
    public string enterSound;

    private AudioManger theAudio;

    public bool talking = false;
    private bool keyActivated = false;
    private bool onlytext = false;

    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        text.text = "";
        listName = new List<string>();
        listSentences = new List<string>();
        listDialogueWindows = new List<Sprite>();
        theAudio = FindObjectOfType<AudioManger>();
    }

    public void ShowText(string[] _sentences)
    {
        talking = true;
        onlytext = true;

        for (int i = 0; i < _sentences.Length; i++)
        {
            listSentences.Add(_sentences[i]);
        }

        StartCoroutine(StartTextCorutine());
    }

    public void ShowDialogue(Dialogue dialogue)
    {
        talking = true;
        onlytext = false;

        for(int i = 0; i < dialogue.sentences.Length; i++)
        {
            listSentences.Add(dialogue.sentences[i]);
            listDialogueWindows.Add(dialogue.dialougueWindows[i]);
        }

        animDialogueWindow.SetBool("Appear", true);
        StartCoroutine(StartDialogueCoroutine());
    }

    public void ExitDialogue()
    {
        text.text = "";
        count = 0;
        listName.Clear();
        listSentences.Clear();
        listDialogueWindows.Clear();
        animDialogueWindow.SetBool("Appear", false);
        talking = false;
    }

    IEnumerator StartTextCorutine()
    {
        keyActivated = true;
        for (int i = 0; i < listSentences[count].Length; i++)
        {
            text.text += listSentences[count][i]; //1번째 문장의 1번째글자부터 하나씩 출력
            if (i % 7 == 1)
            {
                theAudio.Play(keySound);
            }
            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator StartDialogueCoroutine()
    {
        if (count > 0)
        {
            if (listDialogueWindows[count] != listDialogueWindows[count - 1])
            {
                animDialogueWindow.SetBool("Apeear", false);
                yield return new WaitForSeconds(0.2f);
                rendererDialogueWindow.GetComponent<SpriteRenderer>().sprite = listDialogueWindows[count];
                animDialogueWindow.SetBool("Appear", true);
            }
            else
            {
                yield return new WaitForSeconds(0.05f);
            }
        }
        else 
        {
            yield return new WaitForSeconds(0.05f);
            rendererDialogueWindow.GetComponent<SpriteRenderer>().sprite = listDialogueWindows[count];
        }
        keyActivated = true;
        for (int i = 0; i < listSentences[count].Length; i++)
        {
            text.text += listSentences[count][i]; //1번째 문장의 1번째글자부터 하나씩 출력
            if(i % 7 == 1)
            {
                theAudio.Play(keySound);
            }
            yield return new WaitForSeconds(0.01f);
        }

    }


    // Update is called once per frame
    void Update()
    {
        if (talking && keyActivated)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                keyActivated = false;
                count++;
                text.text = "";
                theAudio.Play(enterSound);

                if (count == listSentences.Count)
                {
                    StopAllCoroutines();
                    ExitDialogue();
                }
                else
                {
                    StopAllCoroutines();
                    if (onlytext)
                        StartCoroutine(StartTextCorutine());
                    else
                        StartCoroutine(StartDialogueCoroutine());
                }
            }
        }
    }

}
