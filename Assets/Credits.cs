using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    public Sprite[] animalSprites;

    public RuntimeAnimatorController bearAnim;
    public RuntimeAnimatorController rabbitAnim;

    private string menuSceneName = "Menu";

    void Start()
    {
        int animalIndex = Random.Range(0, 3);

        switch (animalIndex)
        {
            case 0:
                GetComponent<Animator>().Play("Fox-shadow");
                break;
            case 1:
                GetComponent<Animator>().runtimeAnimatorController = bearAnim as RuntimeAnimatorController;
                
                GetComponent<Animator>().Play("Bear");
                transform.rotation = Quaternion.Euler(0, 180, 0);
                GetComponent<SpriteRenderer>().sprite = animalSprites[animalIndex];
                break;
            case 2:
                GetComponent<Animator>().runtimeAnimatorController = rabbitAnim as RuntimeAnimatorController;

                GetComponent<Animator>().Play("Rabbit");
                GetComponent<SpriteRenderer>().sprite = animalSprites[animalIndex];
                break;
        }
        gameObject.AddComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(menuSceneName);
        }
    }
}
