using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    private int score = 0;
    Rigidbody2D rb_player;
    SpriteRenderer spriteRenderer;
    public Color blue, pink, purple, yellow;
    //private List<Color> colorList;
    private string currentColor;
    private bool isOver = false;
    public Text gameOverText, scoreText;
    private AudioSource audioSource;
    public AudioClip tapAudio, boostAudio, gameOverAudio, scoreUpAudio;
    private bool boostAudioPlaying = false;
    private CameraControl cameraControl;
    private bool shakeControl = true, isOverAudioPlayed = false;
    public GameObject[] obstaclePrefabs, skillPrefabs;
    private int skillCreateCounter = 0;

    void Start()
    {
        cameraControl = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraControl>();
        Time.timeScale = 1;
        rb_player = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        //colorList = new List<Color>() { blue, pink, purple, yellow };
        rb_player.gravityScale = 0; //to avoid fall down at the beginning
        audioSource = GetComponent<AudioSource>();

        ChooseRandomColor();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isOver)
        {
            if (rb_player.gravityScale == 0) rb_player.gravityScale = 2;
            rb_player.velocity = new Vector2(0, 7f);
            /*audioSource.clip = tapAudio;
            audioSource.Play();*/
            if (!boostAudioPlaying)
            {
                StartCoroutine(PlayAudio(tapAudio));
            }
        }
        else if(Input.GetMouseButtonDown(0) && isOver)
        {
            SceneManager.LoadScene("GameScene");
        }
        GameOver();
    }

    private void ChooseRandomColor()
    {
        int temp = Random.Range(0, 4);
        //spriteRenderer.color = colorList.ElementAt(temp);
        switch (temp)
        {
            case 0:
                spriteRenderer.color = blue;
                currentColor = "BlueTag";
                break;
            case 1:
                spriteRenderer.color = pink;
                currentColor = "PinkTag";
                break;
            case 2:
                spriteRenderer.color = purple;
                currentColor = "PurpleTag";
                break;
            case 3:
                spriteRenderer.color = yellow;
                currentColor = "YellowTag";
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(currentColor == collision.tag)
        {
            //Debug.Log("score");
        }
        else if(collision.tag == "RandomColorChangerTag")
        {
            ChooseRandomColor();
            Destroy(collision.gameObject);
        }
        else if(collision.tag == "Finish")
        {
            isOver = true;
            StartCoroutine(PlayAudio(gameOverAudio));
        }
        else if(collision.tag == "ScoreUpTag")
        {
            StartCoroutine(PlayAudio(scoreUpAudio));
            StartCoroutine(CrashCircle(collision.gameObject));
            if(!isOver) score++;
            Creator();
            scoreText.text = score.ToString();
        }
        else if(collision.tag == "BoosterTag")
        {
            //rb_player.AddForce(new Vector2(0,500));
            if (rb_player.velocity.y >= 0)
            {
                rb_player.velocity = new Vector2(0, 15f);
                if(!boostAudioPlaying) StartCoroutine(PlayAudio(boostAudio));
            }
        }
        else if(collision.tag == "ScoreUpTagBlock")
        {
            StartCoroutine(PlayAudio(scoreUpAudio));
            StartCoroutine(CrashBlock(collision.gameObject));
            if(!isOver) score++;
            Creator();
            scoreText.text = score.ToString();
        }
        else
        {
            isOver = true;
            StartCoroutine(PlayAudio(gameOverAudio));
        }
    }

    private void GameOver()
    {
        if (isOver)
        {
            RemoveBoosters(); //remove all boosters when game is over
            gameOverText.text = "Tap to play again";
            if (shakeControl)
            {
                shakeControl = false;
                StartCoroutine(cameraControl.Shake(1.5f, 0.5f));
            }
            //Time.timeScale = 0;
            rb_player.bodyType = RigidbodyType2D.Kinematic;
        }
    }

    IEnumerator PlayAudio(AudioClip clip)
    {
        if (audioSource.enabled && !isOverAudioPlayed)
        {
            audioSource.clip = clip;
            if (clip.GetInstanceID() == boostAudio.GetInstanceID()) boostAudioPlaying = true;
            if (clip.GetInstanceID() == gameOverAudio.GetInstanceID()) isOverAudioPlayed = true;
            audioSource.Play();
            yield return new WaitForSeconds(clip.length);

            if (clip.GetInstanceID() == boostAudio.GetInstanceID()) boostAudioPlaying = false;
            if (clip.GetInstanceID() == gameOverAudio.GetInstanceID()) audioSource.enabled = false;
        }
    }

    private IEnumerator CrashCircle(GameObject circle)
    {
        circle.GetComponent<BoxCollider2D>().enabled = false;
        foreach (Transform child in circle.transform)
        {
            child.GetComponent<CircleControl1>().enabled = false;
            foreach (Transform c in child)
            {
                Rigidbody2D rb = c.GetComponent<Rigidbody2D>();
                rb.bodyType = RigidbodyType2D.Dynamic;
                rb.AddForce(new Vector2(Random.Range(-100, 100), Random.Range(-25, -100)));
            }
        }
        yield return new WaitForSeconds(5f);
        Destroy(circle);
    }

    private IEnumerator CrashBlock(GameObject block)
    {
        block.GetComponent<BoxCollider2D>().enabled = false;
        foreach (Transform child in block.transform)
        {
            child.GetComponent<BlockControl1>().enabled = false;
            Rigidbody2D rb = child.GetComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.AddForce(new Vector2(Random.Range(-100, 100), Random.Range(-25, -100)));
        }
        yield return new WaitForSeconds(5f);
        Destroy(block);
    }

    private void RemoveBoosters()
    {
        GameObject[] boosters = GameObject.FindGameObjectsWithTag("BoosterTag");
        for (int i = 0; i < boosters.Length; i++)
        {
            Destroy(boosters[i]);
        }
    }

    private void Creator()
    {
        skillCreateCounter++;
        Instantiate(obstaclePrefabs[Random.Range(0, 5)], new Vector3(0, transform.position.y + 83, 0), Quaternion.identity);
        if(skillCreateCounter % 3 == 0)
        {
            Instantiate(skillPrefabs[Random.Range(0, 2)], new Vector3(0, transform.position.y + 78, 0), Quaternion.identity);
        }
    }
}
