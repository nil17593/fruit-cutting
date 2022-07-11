using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectClicker : MonoBehaviour
{
    public bool selected;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                if (hit.transform != null)
                {

                    if (hit.transform.gameObject.CompareTag("Plate") && !selected)
                    {
                        selected = true;
                        hit.transform.position = hit.transform.position + new Vector3(0, 0.5f, 0);
                    }
                    else if (selected)
                    {
                        selected = false;
                        hit.transform.position = hit.transform.position + new Vector3(0, -0.5f, 0);
                    }
                }
            }
        }
    }
}
