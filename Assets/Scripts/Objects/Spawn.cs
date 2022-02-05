using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject objectPrefab;
    public GameObject rabbitPrefab;
    public float direction;

    public float spawnTime;
    public float destroyObjectTime = 10f;
    public bool spawnRabbit;
    public Sprite[] stoneSprites;

    private SpriteRenderer spriteRenderer;
    private GameObject obstacle;
    private GameObject rabbit;

    private Vector2 cord;
    private float timer = 0f;
    private float hightSpawn = -1.46f;
    
    private float extraOutStone;
    private float extraOutRabbit;

    void Start()
    {
        if (direction == 1)
        {
            cord = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
            extraOutStone = 2.5f;
        } else
        {
            cord = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
            extraOutStone = -2.5f;
        }
    }

    void Update()
    {
        if (timer <= spawnTime)
        {
            timer += Time.deltaTime;
        }
        else
        {
            if(spawnRabbit)
            {
                rabbit = Instantiate(rabbitPrefab, new Vector2((cord.x + RabbitExtra()) + direction, hightSpawn), Quaternion.identity);
                rabbit.transform.rotation = Quaternion.Euler(0, 180, 0);
                StartCoroutine(DestroyGameObject(rabbit));
            }

            obstacle = Instantiate(objectPrefab, new Vector2((cord.x + extraOutStone) + direction, hightSpawn), Quaternion.identity);
            obstacle.GetComponent<ObjectMovement>().direction = direction;
            
            spriteRenderer = obstacle.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = stoneSprites[Random.Range(0, 3)];

            obstacle.AddComponent<PolygonCollider2D>();
            StartCoroutine(DestroyGameObject(obstacle));
            timer = 0;
        }
    }

    private float RabbitExtra()
    {
        if (direction == 1)
        {
            extraOutRabbit = Random.Range(3f, 7f);
        }
        else
        {
            extraOutRabbit = Random.Range(-3f, -7f);
        }
        return extraOutRabbit;
    }

    IEnumerator DestroyGameObject(GameObject gameObject)
    {
        yield return new WaitForSeconds(destroyObjectTime);

        Destroy(gameObject);
    }
}
