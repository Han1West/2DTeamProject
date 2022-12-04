using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStat : MonoBehaviour
{
    public static PlayerStat instance;

    public int hp;
    public int currentHp;

    public int atk;
    public int added_atk;


    public string dmgSound;

    private FadeManager theFade;
    private Menu theMenu;

    public GameObject prefabs_Floating_text;
    public GameObject parent;

    // Start is called before the first frame update
    void Start()
    {
        theMenu = FindObjectOfType<Menu>();
        theFade = FindObjectOfType<FadeManager>();
        instance = this;
    }

    public void Hit(int _enemyAtk)
    {
        int dmg;

        dmg = _enemyAtk;

        currentHp = currentHp - dmg;

        if (currentHp <= 0)
        {
            SceneManager.LoadScene("Title");
            StartCoroutine(GameOverCoroutine());
            Debug.Log("게임 오버");
        }

        AudioManger.instance.Play(dmgSound);

        Vector3 vector = this.transform.position;
        vector.y += 60;

        GameObject clone = Instantiate(prefabs_Floating_text, vector, Quaternion.Euler(Vector3.zero));
        clone.GetComponent<FloatingText>().text.text = dmg.ToString();
        clone.GetComponent<FloatingText>().text.color = Color.red;
        clone.GetComponent<FloatingText>().text.fontSize = 25;
        clone.transform.SetParent(parent.transform);
        StopAllCoroutines();
        StartCoroutine(HitCoroutine());
    }

    IEnumerator HitCoroutine()
    {
        Color color = GetComponent<SpriteRenderer>().color;
        color.a = 0;
        GetComponent<SpriteRenderer>().color = color;
        yield return new WaitForSeconds(0.1f);
        color.a = 1f;
        GetComponent<SpriteRenderer>().color = color;
        yield return new WaitForSeconds(0.1f);
        color.a = 0f;
        GetComponent<SpriteRenderer>().color = color;
        yield return new WaitForSeconds(0.1f);
        color.a = 1f;
        GetComponent<SpriteRenderer>().color = color;
        yield return new WaitForSeconds(0.1f);
        color.a = 0f;
        GetComponent<SpriteRenderer>().color = color;
        yield return new WaitForSeconds(0.1f);
        color.a = 1f;
        GetComponent<SpriteRenderer>().color = color;
    }

    IEnumerator GameOverCoroutine()
    {
        theFade.GameOver();

        yield return new WaitForSeconds(2f);

        theMenu.ToTitle();

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
