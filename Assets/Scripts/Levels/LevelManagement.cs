using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManagement : MonoBehaviour
{
    public GameObject player;
    public GameObject ChangeSpriteObject;
    public Sprite[] sprites;
    public Vector3 MoveObjectTo;
    public SpriteRenderer[] bg;

    private int currentLevel;
    private float levelTime = 60f;

    private SpriteRenderer spriteRenderer;
    private int spriteIndex;
    private int spritesAmount = 6;

    private float changeSpriteTime = 8f;
    private float timer = 0f;

    private string daySceneName = "Day";
    private string nightSceneName = "Night";
 
    private Color dayColor = new Color(0.78f, 0.78f, 0.78f, 1f);
    private Color nightColor = new Color(1f, 1f, 1f, 1f);
    private Color initialColor;

    void Start()
    {
        currentLevel = SceneManager.GetActiveScene().buildIndex;
        
        if (SceneManager.GetActiveScene().name == daySceneName)
        {
            StartCoroutine(MoveToPosition(transform, MoveObjectTo, levelTime));
            initialColor = new Color(1f, 1f, 1f, 1f);
            StartCoroutine(ChangeBackgroundColor(dayColor, levelTime));
        } 
        else
        {
            spriteRenderer = ChangeSpriteObject.GetComponent<SpriteRenderer>();
            initialColor = new Color(0.71f, 0.71f, 0.71f, 1f);
            StartCoroutine(ChangeBackgroundColor(nightColor, levelTime));
        }
    }

    void Update()
    {
        if (levelTime > 0)
        {
            levelTime -= Time.deltaTime;
        }
        else
        {
            startNextLevel();
        }

        if (SceneManager.GetActiveScene().name == nightSceneName)
        {
            if (timer <= changeSpriteTime)
            {
                timer += Time.deltaTime;
            }
            else if (spriteIndex < spritesAmount)
            {
                spriteIndex++;
                spriteRenderer.sprite = sprites[spriteIndex];
                timer = 0;
            }
        } 
    }

    private IEnumerator ChangeBackgroundColor(Color newColor, float timeToChange)
    {
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / timeToChange)
        {
            Color targetColor = newColor;
            Color newColor2 = Color.Lerp(initialColor, newColor, t);
            for (int i = 0; i < bg.Length; i++)
            {
                bg[i].GetComponent<SpriteRenderer>().color = newColor2;
            }
            yield return null;
        }
    }

    private IEnumerator MoveToPosition(Transform transform, Vector3 position, float timeToReachTarget)
    {
        Vector3 currentPos = transform.position;
        float t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / timeToReachTarget;
            transform.position = Vector3.Lerp(currentPos, position, t);
            yield return null;
        }
    }

    private void startNextLevel()
    {
        switch(currentLevel)
        {
            case 1:
                PlayerPrefs.SetInt("powerScore", player.GetComponent<Score>().scoreAmount);
                PlayerPrefs.SetInt("powerScore", player.GetComponent<PlayerControlls>().powerScore);
                SceneManager.LoadScene(currentLevel + 1);
                break;
            case 3:
                SceneManager.LoadScene(1);
                break;
        }
    }
}
