using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnIngredient : MonoBehaviour
{
    [SerializeField] GameObject iceCreamPrefab;
    GameObject instantiatedIceCream;

    Vector3 MouseWorldPos()
    {
        Vector3 mouseScreenPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseScreenPos.z = 0;
        return mouseScreenPos;
    }

    private void OnMouseDown()
    {
        if(!FindObjectOfType<GameManager>().isPaused)
        {
            instantiatedIceCream = Instantiate(iceCreamPrefab, transform.position, Quaternion.identity);
        }
    }

    private void OnMouseDrag()
    {
        if(instantiatedIceCream != null)
            instantiatedIceCream.transform.position = MouseWorldPos();
    }

    private void OnMouseUp()
    {
        if (instantiatedIceCream != null)
            instantiatedIceCream.GetComponent<IngredientMove>().isBeingDragged = false;
    }
}
