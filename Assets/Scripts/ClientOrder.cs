using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClientOrder : MonoBehaviour
{
    public Flavour[] flavoursAvailable;
    public List<IceCreamFlavour> desiredFlavours = new List<IceCreamFlavour>();
    int flavourNumber;
    public int maxFlavourNumber;
    public bool satisfied = false;
    public bool ready_ = false;
    public Transform space;

    public Transform[] flavourCircles;

    [SerializeField] TextMeshPro timerText;
    [SerializeField] public float clientTimer;
    public float timer;
    [SerializeField] int clientPoints;

    private void Start()
    {
        //flavourCircles = GetComponentsInChildren<Transform>();
        for (int i = 1; i < flavourCircles.Length; i++)
        {
            flavourCircles[i].gameObject.SetActive(false);
        }

        desiredFlavours.Clear();
        flavourNumber = Random.Range(1, maxFlavourNumber+1);
        switch(flavourNumber)
        {
            case 1:
                clientPoints = 2;
                break;
            case 2:
                clientPoints = 5;
                break;
            case 3:
                clientPoints = 10;
                break;
        }
        for (int i = 0; i < flavourNumber; i++)
        {
            //IceCreamFlavour flavour = (IceCreamFlavour)Random.Range(0, 3);
            Flavour flavour = flavoursAvailable[Random.Range(0,flavoursAvailable.Length)];
            //flavour.iceCreamFlavour = (IceCreamFlavour)Random.Range(0, 3);
            if (!desiredFlavours.Contains(flavour.iceCreamFlavour))
            {
                desiredFlavours.Add(flavour.iceCreamFlavour);
                flavourCircles[i].gameObject.SetActive(true);
                flavourCircles[i].GetComponent<SpriteRenderer>().color = flavour.iceCreamColor;
            }
            else
            {
                i--;
            }
        }

        timer = clientTimer;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        timerText.text = "0:" + timer.ToString("00");
        if(timer < 1)
        {
            ClientGoesAway();
        }
    }

    public void ActivateFlavourCircle()
    {
        for (int i = 1; i <= flavourNumber; i++)
        {
            flavourCircles[i].gameObject.SetActive(true);
            //flavourCircles[i].GetComponent<SpriteRenderer>().color = desiredFlavours[i].
        }
    }

    public void ClientSatisfied()
    {
        space.GetComponent<ClientSpace>().occupied = false;
        FindObjectOfType<ClientGenerator>().clients -= 1;
        FindObjectOfType<GameManager>().AddPoints(clientPoints);
        Destroy(gameObject);
    }

    public void ClientGoesAway()
    {
        space.GetComponent<ClientSpace>().occupied = false;
        FindObjectOfType<ClientGenerator>().clients -= 1;
        FindObjectOfType<GameManager>().LessPoints(5);
        Destroy(gameObject);
    }

    public void CheckIfReady(Bowl bowlOffered)
    {
        bool ready = true;
        ready_ = true;
        if (bowlOffered.GetComponent<IceCreamInside>().flavours.Count > 0)
        {
            Debug.Log("HOLA");
            if (bowlOffered.GetComponent<IceCreamInside>().flavours.Count == desiredFlavours.Count)
            {
                foreach (IceCreamFlavour desiredFlavour in desiredFlavours)
                {
                    if (!bowlOffered.GetComponent<IceCreamInside>().flavours_.Contains(desiredFlavour))
                    {
                        Debug.Log("WE NEED " + desiredFlavour);
                        ready = false;
                        ready_ = false;
                        break;
                    }
                }
                if (ready)
                {
                    //bowlOffered.space.GetComponent<WorkBase>().DeactivateFlavourCircle();
                    //bowlOffered.space.GetComponent<WorkBase>().occupied = false;
                    //bowlOffered.space = null;
                    //Destroy(bowlOffered.gameObject);
                    Debug.Log("READY!!");
                    //satisfied = true;
                }
                else
                {
                    Debug.Log("TRASH!!");
                    //satisfied = false;
                }
            }
            else if (bowlOffered.GetComponent<IceCreamInside>().flavours.Count > desiredFlavours.Count)
            {
                Debug.Log("TRASH!!");
                //satisfied = false;
            }
            else
            {
                Debug.Log("MORE!!");
                //satisfied = false;
            }
        }
    }
}
