using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI pointsText;
    [SerializeField] TextMeshProUGUI timeText;
    int points = 0;
    [SerializeField] float startingTimer;
    float timer;
    public int goodClients = 0;
    public int badClients = 0;

    [SerializeField] GameObject menuObject;
    [SerializeField] GameObject defeatObject;
    [SerializeField] TextMeshProUGUI pointTextDefeat, goodClientsDefeat, badClientsDefeat;
    public bool isPaused = false;

    void Start()
    {
        menuObject.SetActive(false);
        defeatObject.SetActive(false);
        timer = startingTimer;
    }
    void Update()
    {
        if(GetComponent<TutorialManager>().finishedTutorial)
        {
            timer -= Time.deltaTime;
            // Create a TimeSpan object from the current time
            TimeSpan timeSpan = TimeSpan.FromSeconds(timer);

            // Format the time with hours, minutes, and seconds
            timeText.text = "Tiempo: " + $"{timeSpan.Minutes:00}:{timeSpan.Seconds:00}";

            Difficulty();

            if (timer <= 0.3f)
            {
                Defeat();
            }
        }
    }

    public void Defeat()
    {
        defeatObject.SetActive(true);
        pointTextDefeat.text = "--- " + points.ToString() + " ---";
        goodClientsDefeat.text = goodClients.ToString();
        badClientsDefeat.text = badClients.ToString();
        //PlayerPrefs.SetInt("lastScore", points);
        isPaused = true;
        Time.timeScale = 0f;
    }

    public void ReloadScene()
    {
        isPaused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Difficulty()
    {        
        if (goodClients >= 0 && goodClients < 10)
        {
            Debug.Log("facil");
            // El tiempo que pasa entre cliente y cliente.
            FindObjectOfType<ClientGenerator>().clientTimer = 5f;
            // Numero de sabores que los clientes pueden pedir.
            ChangeFlavourNumber(1);
            // Paciencia del cliente para esperar.
            FindObjectOfType<ClientGenerator>().clientPrefab.GetComponent<ClientOrder>().clientTimer = 29f;

        }
        else if (goodClients >= 10 && goodClients < 16)
        {
            Debug.Log("medio");
            FindObjectOfType<ClientGenerator>().clientTimer = 3f;
            ChangeFlavourNumber(2);
            FindObjectOfType<ClientGenerator>().clientPrefab.GetComponent<ClientOrder>().clientTimer = 19f;
        }
        else if (goodClients >= 16)
        {
            Debug.Log("dificil");
            FindObjectOfType<ClientGenerator>().clientTimer = 2f;
            ChangeFlavourNumber(3);
            FindObjectOfType<ClientGenerator>().clientPrefab.GetComponent<ClientOrder>().clientTimer = 14f;
        }
    }
    void ChangeFlavourNumber(int newNumber)
    {
        FindObjectOfType<ClientGenerator>().clientPrefab.GetComponent<ClientOrder>().maxFlavourNumber = newNumber;
    }
    public void AddPoints(int amount)
    {
        points += amount;
        goodClients += 1;
        UpdatePointText();
    }

    public void LessPoints(int amount)
    {
        points -= amount;
        UpdatePointText();
    }

    public void AddTime(float extraTime)
    {
        timer += extraTime;
    }

    public void UpdatePointText()
    {
        if (points < 1000)
        {
            pointsText.text = "Puntos: " + points.ToString("000");
        }
        else
        {
            pointsText.text = "Puntos: " + (points / 1000).ToString("F1") + "K";
        }
    }

    public void PauseGame()
    {
        menuObject.SetActive(true);
        isPaused = true;
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        menuObject.SetActive(false);
        isPaused = false;
        Time.timeScale = 1f;
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1f;
        isPaused = false;
        PlayerPrefs.SetInt("lastScore", points);
        SceneManager.LoadScene("MainMenu");
    }
}
