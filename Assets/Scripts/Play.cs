using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Play : MonoBehaviour
{

    private bool isStarted = false;
    [SerializeField] private GameObject planer;
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject blackScreen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isStarted)
        {
            planer.transform.Translate((new Vector3(0, 0, 1)) * -10f * Time.deltaTime);
        }
    }

    public void StartGame()
    {
        isStarted = !isStarted;
        menuPanel.SetActive(!isStarted);
        blackScreen.SetActive(isStarted);
        StartCoroutine(darkenScreen());
        Invoke("executeScene", 1f);
    }

    private IEnumerator darkenScreen()
    {
        Color color = blackScreen.GetComponent<Image>().color;
        color.a += .06f;
        blackScreen.GetComponent<Image>().color = color;
        yield return new WaitForSeconds(0.01f);
        if (blackScreen.GetComponent<Image>().color.a != 1f) StartCoroutine(darkenScreen());
    }

    private void executeScene()
    {
        StopAllCoroutines();
        SceneManager.LoadScene("level");
    }
}
