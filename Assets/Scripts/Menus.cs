using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour
{
    //класс для управления элементов ui внутри уровня
    // публичные методы для кнопок
    [SerializeField] private GameObject pauseMenuUi; // меню паузы
    private bool isPaused = false; // состояние паузы

    // Update is called once per frame
    void Update()
    {
        //при нажатие на esc открываю меню паузы
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            editStateMenu(); 
        }
    }
    
    public void Resume()
    {
        editStateMenu();
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("menu");
    }
    
    public void Retry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("level");
    }

    private void editStateMenu()
    {
        isPaused = !isPaused;
        pauseMenuUi.SetActive(isPaused);
        Time.timeScale = isPaused ? 0f : 1f;
    }
}
