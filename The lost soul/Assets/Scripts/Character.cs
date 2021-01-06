using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class Character : MonoBehaviour
{
    [SerializeField] DescriptionColor[] descriptionColors;
    [SerializeField] AnimationClip[] animationClip;
    [Space(5)]
    [SerializeField] Sprite[] heartSprite;
    [SerializeField] Image[] heart;

    private SpriteRenderer _spriteRenderer => transform.GetChild(0).GetComponent<SpriteRenderer>();
    private Animation _animation => GetComponent<Animation>();
    public int score;

    public GameObject Colors;
    public GameObject Lose;

    public static List<string> descriptionName = new List<string>();
    public static List<Color> descriptionColor = new List<Color>();

    public static bool isAlpha = true;
    public static string nameColor;
    public static Color color;

    [Space(5)] public float ReturnTime = 1f;

    private Random rand = new Random();

    private void Awake()
    {
        Time.timeScale = 1f;

        for (int i = 0; i < descriptionColors.Length; i++)
        {
            descriptionName.Add(descriptionColors[i].name);
            descriptionColor.Add(descriptionColors[i].color);

            int index = rand.Next(0, descriptionColors.Length);

            if (nameColor == null)
            {
                _spriteRenderer.color = descriptionColors[index].color;
                nameColor = descriptionColors[index].name;
                color = descriptionColors[index].color;
            }
        }

        for (int i = 0; i < heart.Length; i++)
        {
            heart[i].sprite = heartSprite[0];
        }

        Debug.Log(nameColor);

        score = heart.Length;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Arrow"))
        {
            if (Arrow.isColor == false)
            {
                Arrow.isColor = true;
                Bow.isArrow = true;

                isAlpha = false;

                collision.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

                switch (nameColor)
                {
                    case "Green":
                        PlayAnimation(1);
                        break;
                    case "Purple":
                        PlayAnimation(2);
                        break;
                    case "Blue":
                        PlayAnimation(3);
                        break;
                }

                StartCoroutine(PlayDie(collision, ReturnTime));
            }
            else
            {
                if (score >= 0)
                {
                    MinusOneHeart(--score);
                }
            }
        }
    }

    private void Update()
    {
        if(score == 0) Invoke(nameof(ViewLose), 2f);
    }

    private void ViewLose()
    {
        Time.timeScale = 0;
        Colors.SetActive(false);
        Lose.SetActive(true);
    }

    private void MinusOneHeart(int index)
    {
        for (int i = index; i > 0; i--)
        {
            heart[i].sprite = heartSprite[0];
        }

        heart[index].sprite = heartSprite[1];
    }

    private void PlayAnimation(int index)
    {
        _animation.clip = animationClip[index];
        _animation.Play();
    }

    private IEnumerator PlayDie(Collision2D collision, float returnTime)
    {
        yield return new WaitForSeconds(returnTime);
        PlayAnimation(0);
        collision.gameObject.GetComponent<Animation>().Play();
    }
}
