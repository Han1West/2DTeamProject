using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeManager : MonoBehaviour
{

    public SpriteRenderer white;
    public SpriteRenderer black;
    public SpriteRenderer over;
    private Color color;

    public string flashSound;

    private Menu theMenu;
    private AudioManger theAudio;

    private WaitForSeconds waitTime = new WaitForSeconds(0.01f); //계산의 과부하 방지

    // Start is called before the first frame update
    void Start()
    {
        theMenu = FindObjectOfType<Menu>();
        theAudio = FindObjectOfType<AudioManger>();
    }
    public void FadeOut(float _speed = 0.02f)
    {
        StopAllCoroutines();
        StartCoroutine(FadeOutCoroutine(_speed));
    }

    IEnumerator FadeOutCoroutine(float _speed)
    {

        color = black.color;

        while (color.a < 1f)
        {
            color.a += _speed;
            black.color = color;
            yield return waitTime;
        }
    }

    public void FadeIn(float _speed = 0.02f)
    {
        StopAllCoroutines();
        StartCoroutine(FadeInCoroutine(_speed));
    }

    public void GameOver(float _speed = 0.02f)
    {
        StopAllCoroutines();
        StartCoroutine(GameOverCoroutine(_speed));
    }

    IEnumerator GameOverCoroutine(float _speed)
    {
        color = over.color;

        while (color.a < 1f)
        {
            color.a += _speed;
            over.color = color;
            yield return waitTime;
        }

        yield return new WaitForSeconds(2f);
        
        while (color.a > 0f)
        {
            color.a -= _speed;
            over.color = color;
            yield return waitTime;
        }

        theMenu.ToTitle();
    }

    IEnumerator FadeInCoroutine(float _speed)
    {

        color = black.color;

        while (color.a > 0f)
        {
            color.a -= _speed;
            black.color = color;
            yield return waitTime;
        }
    }


    public void Flash(float _speed = 0.1f)
    {
        StopAllCoroutines();
        StartCoroutine(FlashCoroutine(_speed));

    }

    IEnumerator FlashCoroutine(float _speed)
    {
        color = white.color;

        theAudio.Play(flashSound);
        while (color.a < 1f)
        {
            color.a += _speed;
            white.color = color;
            yield return waitTime;
        }
        while (color.a > 0f)
        {
            color.a -= _speed;
            white.color = color;
            yield return waitTime;
        }
    }

    public void FlashOut(float _speed = 0.02f)
    {
        StopAllCoroutines();
        StartCoroutine(FlashOutCoroutine(_speed));
    }

    IEnumerator FlashOutCoroutine(float _speed)
    {

        color = white.color;

        while (color.a < 1f)
        {
            color.a += _speed;
            white.color = color;
            yield return waitTime;
        }
    }

    public void FlashIn(float _speed = 0.02f)
    {
        StopAllCoroutines();
        StartCoroutine(FlashInCoroutine(_speed));
    }

    IEnumerator FlashInCoroutine(float _speed)
    {

        color = white.color;

        while (color.a > 0f)
        {
            color.a -= _speed;
            white.color = color;
            yield return waitTime;
        }
    }
}