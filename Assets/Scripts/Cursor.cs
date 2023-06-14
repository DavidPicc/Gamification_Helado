using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    public GameObject bowl;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Check for left mouse button click
        {
            Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(clickPosition, Vector2.zero);

            if (hit.collider != null)
            {
                if (hit.collider.gameObject.GetComponent<Bowl>() != null)
                {
                    bowl = hit.collider.gameObject;
                    Debug.Log("Clicked on GameObject: " + bowl);
                    // Perform further checks or actions based on the clicked GameObject
                }
            }

            //Bowl[] bowlColliders = FindObjectsOfType<Bowl>().Select(space => space.GetComponent<Bowl>()).ToArray();
            //foreach (Bowl bowl in bowlColliders)
            //{
            //    bowl.gameObject.GetComponent<Collider2D>().isTrigger = false;
            //    bowl.gameObject.GetComponent<Collider2D>().isTrigger = true;
            //    Debug.Log("triggers reset");
            //}
        }
    }
}
