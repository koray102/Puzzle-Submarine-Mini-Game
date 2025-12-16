using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<GameManager>();

                if (_instance == null)
                {
                    GameObject obj = new GameObject("GameManager");
                    _instance = obj.AddComponent<GameManager>();
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }


    public void LoadScene(string sceneName, float delayTime)
    {
        StartCoroutine(LoadSceneDelayed(sceneName, delayTime));
    }

    
    public void LoadSceneInstantly(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }


    private IEnumerator LoadSceneDelayed(string sceneName, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        SceneManager.LoadScene(sceneName);
    }


    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
