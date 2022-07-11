using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectClicker : MonoBehaviour
{
    public bool selected;
    private GameObject selectedPlate;
    private void Update()
    {
        if (!Input.GetMouseButtonDown(0))
        {
            return;
        }
        else 
        { 
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                if (hit.transform.gameObject.CompareTag("Plate"))
                {
                    if (!selected)
                    {
                        Debug.Log("NOT");
                        selected = true;
                        selectedPlate = hit.transform.gameObject;
                        selectedPlate.transform.position = selectedPlate.transform.position + new Vector3(0, 0.5f, 0);
                    }
                    else if (selected)
                    {
                        Debug.Log("YES");

                        selected = false;
                        selectedPlate.transform.position = selectedPlate.transform.position + new Vector3(0, -0.5f, 0);
                    }
                }
            }
        }
    }
}
