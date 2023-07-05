using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bowl : MonoBehaviour
{
    private Collider2D[] workSpaceColliders;
    public ClientOrder[] clientColliders;
    public Collider2D trashCollider;
    public GameObject space;
    bool bowlPlaced = false;
    public bool hasLid = false;
    [SerializeField] Transform originalTransform;
    ClientOrder selectedClient;
    private void Start()
    {
        workSpaceColliders = GameObject.FindObjectsOfType<WorkBase>().Select(space => space.GetComponent<Collider2D>()).ToArray();
    }
    private void Update()
    {
        if (!bowlPlaced)
        {
            if (!GetComponent<IngredientMove>().isBeingDragged)
            {
                CheckSpace();
            }
        }
        else
        {
            if (hasLid)
            {
                CheckClient();
            }
        }
    }

    private void CheckSpace()
    {
        bool droppedInBowl = false;
        foreach (Collider2D spaceCollider in workSpaceColliders)
        {
            if (spaceCollider != null && spaceCollider.bounds.Contains(transform.position))
            {

                if (!spaceCollider.gameObject.GetComponent<WorkBase>().occupied)
                {
                    transform.SetParent(spaceCollider.transform);
                    transform.position = spaceCollider.transform.position;
                    space = spaceCollider.gameObject;
                    spaceCollider.gameObject.GetComponent<WorkBase>().occupied = true;
                    spaceCollider.gameObject.GetComponent<WorkBase>().bowlShadow.SetActive(false);
                    droppedInBowl = true;
                    bowlPlaced = true;
                    originalTransform = spaceCollider.transform;
                    break;
                }
            }
        }
        if (!droppedInBowl)
        {
            Destroy(gameObject);
        }
    }

    public void ClientArray()
    {
        clientColliders = GameObject.FindObjectsOfType<ClientOrder>().Select(client => client.GetComponent<ClientOrder>()).ToArray();
    }

    private void CheckClient()
    {
        bool droppedInClient = false;
        ClientArray();
        List<ClientOrder> goodClients = new List<ClientOrder>();
        foreach (ClientOrder clientOrder in clientColliders)
        {
            if (clientOrder != null && clientOrder.desiredFlavours.Count == GetComponent<IceCreamInside>().flavours_.Count)
            {
                if (!clientOrder.satisfied)
                {
                    Debug.Log("CLIENTE");
                    clientOrder.CheckIfReady(this);
                    if (clientOrder.ready_)
                    {
                        Debug.Log("CLIENTE SATISFECHO");
                        goodClients.Add(clientOrder);
                    }
                }
            }
        }
        float closestTimer = 1000f;
        foreach(ClientOrder client in goodClients)
        {
            if (client.timer < closestTimer)
            {
                selectedClient = client;
                closestTimer = selectedClient.timer;
            }
        }
        if(selectedClient != null)
        {
            space.GetComponent<WorkBase>().DeactivateFlavourCircle();
            space.GetComponent<WorkBase>().occupied = false;
            space = null;
            originalTransform.gameObject.GetComponent<WorkBase>().occupied = false;
            selectedClient.ClientSatisfied();
            droppedInClient = true;
            Destroy(gameObject);
        }
        if (!droppedInClient)
        {
            //FindObjectOfType<GameManager>().LessPoints(1);
            originalTransform.gameObject.GetComponent<WorkBase>().occupied = false;
            space.GetComponent<WorkBase>().DeactivateFlavourCircle();
            Destroy(gameObject);
        }
    }
}
