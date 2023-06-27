using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadScript : MonoBehaviour
{
    [SerializeField] private Slider Scale;
    [SerializeField] private string NameScene;
    [SerializeField] private GameObject Dead;
    // Start is called before the first frame update

    private void Start()
    {
        if (PlayerPrefs.GetInt("Dead") == 1)
        {
            Dead?.SetActive(true);
            PlayerPrefs.SetInt("Dead", 0);
        }
        if (PlayerPrefs.GetInt("GoReclama") == 1)
            ShowReclama();
        else
            Loading(NameScene);
    }

    void ShowReclama()
    {
        ShowAdd.Show();
        Debug.Log("Реклама Показана!!!");
        PlayerPrefs.SetInt("GoReclama", 0);
        Loading(NameScene);
    }
    public void Loading(string nameScene)
    {
        StartCoroutine(LoadAsync(nameScene));
    }

    IEnumerator LoadAsync(string nameScene)
    {
        AsyncOperation loadAsync = SceneManager.LoadSceneAsync(nameScene);
        loadAsync.allowSceneActivation = false;
        while (!loadAsync.isDone)
        {
            Scale.value = loadAsync.progress;
            if (loadAsync.progress <= .99f && !loadAsync.allowSceneActivation)
            {
                yield return new WaitForSeconds(0);
                loadAsync.allowSceneActivation = true;
            }
            yield return null;
        }    
    }


    //public void Loading(int numberScene)
    //{
    //    StartCoroutine(LoadAsync(numberScene));
    //}

    //IEnumerator LoadAsync(int numberScene)
    //{
    //    AsyncOperation loadAsync = SceneManager.LoadSceneAsync(numberScene);
    //    loadAsync.allowSceneActivation = false;
    //    while (!loadAsync.isDone)
    //    {
    //        Scale.value = loadAsync.progress;
    //        Debug.Log(loadAsync.progress);
    //        if (loadAsync.progress <= .99f && !loadAsync.allowSceneActivation)
    //        {
    //            yield return new WaitForSeconds(0);
    //            loadAsync.allowSceneActivation = true;
    //        }
    //        yield return null;
    //    }
    //}


}
