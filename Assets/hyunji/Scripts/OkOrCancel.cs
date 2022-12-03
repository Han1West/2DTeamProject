using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OkOrCancel : MonoBehaviour
{
    private AudioManger theAudio;
    public string keySound;
    public string enterSound;
    public string cancelSound;

    public GameObject up_Panel;
    public GameObject down_Panel;

    public TMP_Text up_Text;
    public TMP_Text down_Text;

    public bool activated;
    public bool keyInput;
    private bool result = true;

    // Start is called before the first frame update
    void Start()
    {
        theAudio = FindObjectOfType<AudioManger>();
    }

    public void Selected()
    {
        theAudio.Play(keySound);
        result = !result;

        if(result)
        {
            up_Panel.gameObject.SetActive(false);
            down_Panel.gameObject.SetActive(true);
        }
        else
        {
            up_Panel.gameObject.SetActive(true);
            down_Panel.gameObject.SetActive(false);
        }
    }

    public void ShowTwoChoice(string _upText, string _donwText)
    {
        activated = true;
        result = true;
        up_Text.text = _upText;
        down_Text.text = _donwText;

        up_Panel.gameObject.SetActive(false);
        down_Panel.gameObject.SetActive(true);

        StartCoroutine(ShowTwoChoiceCoroutine());
    }

    public bool GetResult()
    {
        return result;
    }

    IEnumerator ShowTwoChoiceCoroutine()
    {
        yield return new WaitForSeconds(0.01f);
        keyInput = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(keyInput)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                Selected();
            }
            else if(Input.GetKeyDown(KeyCode.UpArrow))
            {
                Selected();
            }
            else if(Input.GetKeyDown(KeyCode.F))
            {
                theAudio.Play(enterSound);
                keyInput = false;
                activated = false;
            }
            else if(Input.GetKeyDown(KeyCode.Escape))
            {
                theAudio.Play(cancelSound);
                keyInput = false;
                activated = false;
                result = false;
            }
        }
    }
}
