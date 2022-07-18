using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BottleController : MonoBehaviour
{
    private Touch touch;
    public float moveSpeedTouch;
    public float moveSpeedMouse;
    private Vector3 prevPos;

    private Vector3 initPosition;
    private Quaternion initRotation;

    private bool touchInput = false;
    private bool keyInput = false;

    public float rotateParameter;

    private float rotationZ;

    private void OnEnable()
    {
        initPosition = transform.position;
        initRotation = transform.rotation;
    }


    void Update()
    {
        if (!GameManager.Instance.canShake)
        {
            return;
        }
        else
        {

            if (Input.touchCount > 0)
            {
                touchInput = true;
                touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    //transform.eulerAngles = new Vector3(0, 55, 0);
                }

                if (touch.phase == TouchPhase.Moved)
                {
                    transform.position = new Vector3(transform.position.x + (-touch.deltaPosition.x * moveSpeedTouch), transform.position.y + (touch.deltaPosition.y * moveSpeedTouch), transform.position.z);
                    if (touch.deltaPosition.x > 0)
                    {
                        transform.Rotate(new Vector3(0, 0, rotateParameter));
                        GameManager.Instance.ProgressFillingBar.fillAmount += 0.02f;
                    }
                    else
                    {
                        transform.Rotate(new Vector3(0, 0, -rotateParameter));
                        GameManager.Instance.ProgressFillingBar.fillAmount += 0.02f;
                    }
                }
            }
            else
            {
                touchInput = false;
            }

            if (Input.GetMouseButtonDown(0))
            {
                prevPos = Input.mousePosition;
                //transform.eulerAngles = new Vector3(0, 55, 0);
            }

            if (Input.GetMouseButton(0))
            {
                keyInput = true;
                transform.position = new Vector3(transform.position.x + (Input.mousePosition.x - prevPos.x) * (-moveSpeedMouse), transform.position.y + (Input.mousePosition.y - prevPos.y) * moveSpeedMouse, transform.position.z);


                if (Input.mousePosition.x - prevPos.x > 0)
                {
                    transform.Rotate(new Vector3(0, 0, rotateParameter));
                    GameManager.Instance.ProgressFillingBar.fillAmount += 0.02f;
                }
                else if (Input.mousePosition.x - prevPos.x < 0)
                {
                    transform.Rotate(new Vector3(0, 0, -rotateParameter));
                    GameManager.Instance.ProgressFillingBar.fillAmount += 0.02f;
                }
                prevPos = Input.mousePosition;
            }
            else
            {
                keyInput = false;
            }

            if (keyInput == false && touchInput == false)
            {
                transform.position = initPosition;
                transform.rotation = initRotation;
            }
        }
    }
}