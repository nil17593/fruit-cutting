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
                if (GameManager.Instance.selectPlates)
                {
                    if (hit.transform.gameObject.CompareTag("Plate"))
                    {
                        if (!selected)
                        {
                            selected = true;
                            hit.transform.gameObject.GetComponent<PlateController>().thisPlate = true;
                            selectedPlate = hit.transform.gameObject;
                            selectedPlate.transform.position = selectedPlate.transform.position + new Vector3(0, 0.5f, 0);
                        }
                        else if (selected)
                        {
                            selectedPlate.transform.gameObject.GetComponent<PlateController>().thisPlate = false;
                            selected = false;
                            selectedPlate.transform.position = selectedPlate.transform.position + new Vector3(0, -0.5f, 0);
                        }
                    }
                }
                if (hit.transform.gameObject.CompareTag("BottleCap"))
                {
                        //GameManager.Instance.presentGameState = GameManager.GameState.Pour;
                        GameManager.Instance.bottleSleceted = true;
                }
            }
        }
    }
}
