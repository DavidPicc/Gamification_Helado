using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientGenerator : MonoBehaviour
{
    [SerializeField] public GameObject clientPrefab;
    [SerializeField] Color[] clientColors;
    [SerializeField] Transform[] clientPos;
    [SerializeField] public float clientTimer;
    [SerializeField] int maxClients = 5;
    public int clients = 0;
    float timer;
    void Start()
    {
        timer = clientTimer;
    }

    void Update()
    {
        if (GetComponent<TutorialManager>().finishedTutorial && clients < maxClients)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {

                SpawnClient();
                timer = clientTimer;
            }
        }
    }

    void SpawnClient()
    {
        int randomPos = Random.Range(0, clientPos.Length);
        if (clientPos[randomPos].gameObject.GetComponent<ClientSpace>().occupied)
        {
            SpawnClient();
            return;
        }
        var client = Instantiate(clientPrefab, clientPos[randomPos].position, Quaternion.identity);
        clientPos[randomPos].gameObject.GetComponent<ClientSpace>().occupied = true;
        //client.GetComponent<SpriteRenderer>().color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        client.GetComponent<ClientOrder>().clientRenderer.color = clientColors[Random.Range(0, clientColors.Length)];
        client.GetComponent<ClientOrder>().space = clientPos[randomPos];
        clients += 1;
    }

    bool spawned = false;
    public void SpawnClient(int clientPosInt)
    {
        if(!spawned)
        {
            if (clientPos[clientPosInt].gameObject.GetComponent<ClientSpace>().occupied)
            {
                SpawnClient();
                return;
            }
            var client = Instantiate(clientPrefab, clientPos[clientPosInt].position, Quaternion.identity);
            clientPos[clientPosInt].gameObject.GetComponent<ClientSpace>().occupied = true;
            //client.GetComponent<SpriteRenderer>().color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
            client.GetComponent<ClientOrder>().manualClient = true;
            client.GetComponent<ClientOrder>().clientRenderer.color = clientColors[Random.Range(0, clientColors.Length)];
            client.GetComponent<ClientOrder>().space = clientPos[clientPosInt];
            clients += 1;
            spawned = true;
        }
    }
}
