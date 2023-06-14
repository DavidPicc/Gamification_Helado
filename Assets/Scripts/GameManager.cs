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
    float timer;
    public int goodClients = 0;

    [SerializeField] GameObject menuObject;
    public bool isPaused = false;

    void Start()
    {
        menuObject.SetActive(false);
    }
    void Update()
    {
        timer += Time.deltaTime;
        // Create a TimeSpan object from the current time
        TimeSpan timeSpan = TimeSpan.FromSeconds(timer);

        // Format the time with hours, minutes, and seconds
        timeText.text = "Time: " + $"{timeSpan.Hours:00}:{timeSpan.Minutes:00}:{timeSpan.Seconds:00}";

        Difficulty();
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

    public void UpdatePointText()
    {
        if(points < 0)
        {
            points = 0;
            pointsText.text = "Points: " + points.ToString("000");
        }
        else if (points < 1000)
        {
            pointsText.text = "Points: " + points.ToString("000");
        }
        else
        {
            pointsText.text = "Points: " + (points / 1000).ToString("F1") + "K";
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
        SceneManager.LoadScene("MainMenu");
    }
}
