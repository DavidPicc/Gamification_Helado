using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public bool finishedTutorial = false;
    int tutorialStep = 0;
    [SerializeField] GameObject[] tutorialStepsGO;
    [SerializeField] GameObject workBase1, iceCreamCups, iceCreamLids, tutorialParent;
    [SerializeField] GameObject[] iceCream;
    [SerializeField] GameObject[] workBases;
    void Start()
    {
        iceCreamCups.GetComponent<Collider2D>().enabled = true;
        iceCreamLids.GetComponent<Collider2D>().enabled = false;
        DeactivateIceCream();
        DeactivateWorkBase();
    }

    void Update()
    {
        switch(tutorialStep)
        {
            case 0:
                ResetTutorialGO(0);

                if(workBase1.GetComponent<WorkBase>().occupied)
                {
                    reseted = false;
                    iceCream[0].GetComponent<Collider2D>().enabled = true;
                    iceCreamCups.GetComponent<Collider2D>().enabled = false;
                    NextTutorialStep();
                }
                break;
            case 1:
                ResetTutorialGO(1);
                GetComponent<ClientGenerator>().SpawnClient(2);

                if (FindObjectOfType<IceCreamInside>().flavours_.Count >= 1)
                {
                    reseted = false;
                    iceCreamLids.GetComponent<Collider2D>().enabled = true;
                    DeactivateIceCream();
                    NextTutorialStep();
                }
                break;
            case 2:
                ResetTutorialGO(2);

                if (FindObjectOfType<Bowl>().hasLid)
                {
                    reseted = false;
                    NextTutorialStep();
                }
                break;
            case 3:
                ResetTutorialGO(3);
                finishedTutorial = true;
                ActivateIceCream();
                ActivateWorkBase();
                iceCreamCups.GetComponent<Collider2D>().enabled = true;
                iceCreamLids.GetComponent<Collider2D>().enabled = true;
                tutorialParent.SetActive(false);
                this.enabled = false;
                break;
            default:
                tutorialStep = 3;
                break;

        }
    }

    public void NextTutorialStep()
    {
        tutorialStep += 1;
    }

    bool reseted = false;
    public void ResetTutorialGO(int step)
    {
        if(!reseted)
        {
            for (int i = 0; i < tutorialStepsGO.Length; i++)
            {
                tutorialStepsGO[i].SetActive(false);
            }
            if (step <= tutorialStepsGO.Length - 1)
            {
                tutorialStepsGO[step].SetActive(true);
            }
            reseted = true;
        }
        
    }

    public void DeactivateIceCream()
    {
        for (int i = 0; i < iceCream.Length; i++)
        {
            iceCream[i].GetComponent<Collider2D>().enabled = false;
        }
    }

    public void ActivateIceCream()
    {
        for (int i = 0; i < iceCream.Length; i++)
        {
            iceCream[i].GetComponent<Collider2D>().enabled = true;
        }
    }

    public void DeactivateWorkBase()
    {
        for (int i = 0; i < workBases.Length; i++)
        {
            workBases[i].GetComponent<Collider2D>().enabled = false;
        }
    }

    public void ActivateWorkBase()
    {
        for (int i = 0; i < workBases.Length; i++)
        {
            workBases[i].GetComponent<Collider2D>().enabled = true;
        }
    }

    public void SkipTutorial()
    {
        tutorialStep = 3;
    }
}
