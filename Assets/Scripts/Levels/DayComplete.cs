using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DayComplete : MonoBehaviour
{
    public Animator an;
    private float loadBeforeStart = 4f;
    private string nightSceneName = "Night";

    void Start()
    {
        an.GetComponent<Animator>().Play("Fox-Win", - 1, 0f);
        StartCoroutine(LoadNextLevel());
    }

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(loadBeforeStart);
        SceneManager.LoadScene(nightSceneName);
    }
}
