using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControlls : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool inAir;
    public float jumpForce;
    
    private Animator an;
    private Vector2 outOfCamera;
    private float extraOut = 2f;
    private string nightSceneName = "Night";
    private string gameoverScene = "Gameover";
    private string koScene = "DayKO";
    // private string daySceneName = "Day";

    public Sprite[] staminaSprites;
    public SpriteRenderer spriteRenderer;

    private float timer = 0f;
    private float decreaseScoreTime = 4f;

    [HideInInspector] public int powerScore;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        an = GetComponent<Animator>();

        outOfCamera = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        if(SceneManager.GetActiveScene().name == nightSceneName)
        {
            powerScore = PlayerPrefs.GetInt("powerScore");
        }
    }

    void Update()
    {
        switch(powerScore)
        {
            case 0:
            case 1:
                spriteRenderer.sprite = staminaSprites[0];
                break;
            case 2:
            case 3:
                spriteRenderer.sprite = staminaSprites[1];
                break;
            case 4:
            case 5:
                spriteRenderer.sprite = staminaSprites[2];
                break;
            case 6:
            case 7:
                spriteRenderer.sprite = staminaSprites[3];
                break;
            case 8:
            case 9:
                spriteRenderer.sprite = staminaSprites[4];
                break;
            case 10:
            case 11:
                spriteRenderer.sprite = staminaSprites[5];
                break;
            case 12:
            case 13:
                spriteRenderer.sprite = staminaSprites[6];
                break;
            case 14:
            case 15:
                spriteRenderer.sprite = staminaSprites[7];
                break;
            case 16:
            case 17:
                spriteRenderer.sprite = staminaSprites[8];
                break;
            case 18:
            case 19:
            case 20:
                spriteRenderer.sprite = staminaSprites[9];
                break;
        }


        if (SceneManager.GetActiveScene().name == nightSceneName)
        {
            if (timer <= decreaseScoreTime)
            {
                timer += Time.deltaTime;
            }
            else if(powerScore != 0)
            {
                powerScore--;
                timer = 0;
                Debug.Log(powerScore);
            }
            if (powerScore == 0) SlowDownPlayer();
        }

        if (gameObject.transform.position.x > outOfCamera.x + extraOut)
        {
            SceneManager.LoadScene(koScene);
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && !inAir)
        {
            an.Play("Fox-Jump", -1, 0f);
            rb.velocity = new Vector2(0, jumpForce);
            inAir = true;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Object")
        {
            SlowDownPlayer();
        }

        if (col.gameObject.tag == "Enemy")
        {
            SceneManager.LoadScene(gameoverScene);
        }

        if (col.gameObject.tag == "Ground" && inAir)
        {
            inAir = false;
            an.Play("Fox-shadow", -1, 0f);
        }

        if (col.gameObject.tag == "Rabbit")
        {
            powerScore++;
            Destroy(col.gameObject);
        }
    }

    private void SlowDownPlayer()
    {
        inAir = true;
        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}
