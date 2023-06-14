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
    //bool floating = false;
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
            //if(floating)
            //{
            //    if (!GetComponent<IngredientMove>().isBeingDragged)
            //    {
            //        CheckClient();
            //    }
            //}
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
                    // Drop the bowl inside the space
                    transform.SetParent(spaceCollider.transform);
                    transform.position = spaceCollider.transform.position;
                    space = spaceCollider.gameObject;
                    spaceCollider.gameObject.GetComponent<WorkBase>().occupied = true;
                    droppedInBowl = true;
                    bowlPlaced = true;
                    originalTransform = spaceCollider.transform;
                    break;
                }
            }
        }
        if (!droppedInBowl)
        {
            // Destroy the bowl
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
                    // Drop the bowl inside the client and check if client satisfied.
                    clientOrder.CheckIfReady(this);
                    if (clientOrder.ready_)
                    {
                        Debug.Log("CLIENTE SATISFECHO");
                        goodClients.Add(clientOrder);
                        //originalTransform.gameObject.GetComponent<WorkBase>().occupied = false;
                        //clientOrder.ClientSatisfied();
                        //droppedInClient = true;
                        //Destroy(gameObject);
                        //break;
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
            FindObjectOfType<GameManager>().LessPoints(1);
            originalTransform.gameObject.GetComponent<WorkBase>().occupied = false;
            space.GetComponent<WorkBase>().DeactivateFlavourCircle();
            Destroy(gameObject);
        }
    }
    //private void CheckClient()
    //{
    //    ClientArray();
    //    bool droppedInClient = false;
    //    foreach (Collider2D clientCollider in clientColliders)
    //    {
    //        if (clientCollider != null && clientCollider.bounds.Contains(transform.position))
    //        {
    //            if (!clientCollider.gameObject.GetComponent<ClientOrder>().satisfied)
    //            {
    //                Debug.Log("CLIENTE");
    //                // Drop the bowl inside the client and check if client satisfied.
    //                clientCollider.gameObject.GetComponent<ClientOrder>().CheckIfReady(this);
    //                if(clientCollider.gameObject.GetComponent<ClientOrder>().satisfied)
    //                {
    //                    Debug.Log("CLIENTE SATISFECHO");
    //                    droppedInClient = true;
    //                    originalTransform.gameObject.GetComponent<WorkBase>().occupied = false;
    //                    clientCollider.gameObject.GetComponent<ClientOrder>().ClientSatisfied();
    //                    Destroy(gameObject);
    //                    break;
    //                }
    //            }
    //        }
    //    }
    //    if (!droppedInClient)
    //    {
    //        Collider2D trashCollider = FindObjectOfType<Trash>().GetComponent<Collider2D>();
    //        if (trashCollider != null && trashCollider.bounds.Contains(transform.position))
    //        {
    //            originalTransform.gameObject.GetComponent<WorkBase>().occupied = false;
    //            floating = false;
    //            Destroy(gameObject);
    //        }
    //        else
    //        {
    //            // Returns the bowl.
    //            transform.position = originalTransform.position;
    //        }
    //    }
    //    floating = false;
    //}

    //Vector3 MouseWorldPos()
    //{
    //    Vector3 mouseScreenPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //    mouseScreenPos.z = 0;
    //    return mouseScreenPos;
    //}

    //private void OnMouseDown()
    //{
    //    if(hasLid)
    //    {
    //        GetComponent<CircleCollider2D>().isTrigger = false;
    //        GetComponent<CircleCollider2D>().isTrigger = true;
    //        GetComponent<IngredientMove>().isBeingDragged = true;
    //        floating = true;
    //    }
    //}

    //private void OnMouseDrag()
    //{
    //    if (hasLid)
    //        transform.position = MouseWorldPos();
    //}

    //private void OnMouseUp()
    //{
    //    if(hasLid)
    //        GetComponent<IngredientMove>().isBeingDragged = false;
    //}
}
