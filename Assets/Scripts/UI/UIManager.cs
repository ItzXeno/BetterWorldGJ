using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject button1, button2, button3, tutImg1,TutImg2,TutImg3, title, backButton, next;
    void Start()
    {
        button1.SetActive(true);
        button2.SetActive(true);
        button3.SetActive(true);
        title.SetActive(true);
        tutImg1.SetActive(false);
        TutImg2.SetActive(false);
        TutImg3.SetActive(false);
        backButton.SetActive(false);
        next.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startGame()
    {
        SceneManager.LoadScene(1);
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void tutorial()
    {
        button1.SetActive(false);
        button2.SetActive(false);
        button3.SetActive(false);
        title.SetActive(false);
        tutImg1.SetActive(true);
        backButton.SetActive(true);
        next.SetActive(true);
    }

    public void back()
    {
        button1.SetActive(true);
        button2.SetActive(true);
        button3.SetActive(true);
        title.SetActive(true);
        tutImg1.SetActive(false);
        TutImg2.SetActive(false);
        TutImg3.SetActive(false);
        backButton.SetActive(false);
        next.SetActive(false);
    }

    public void nextTut()
    {
        if (tutImg1.activeSelf)
        {
            tutImg1.SetActive(false);
            TutImg2.SetActive(true);
        }
        else if (TutImg2.activeSelf)
        {
            TutImg2.SetActive(false);
            TutImg3.SetActive(true);
        }
        else if (TutImg3.activeSelf)
        {
            next.SetActive(false);
        }
    }
}
