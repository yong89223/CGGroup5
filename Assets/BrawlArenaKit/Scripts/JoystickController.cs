
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class JoystickController : MonoBehaviour
{
    public Image leftJoystickBackgroundImage;
    public Image rightJoystickBackgroundImage; 
    public bool leftJoystickAlwaysVisible = false; 
    public bool rightJoystickAlwaysVisible = false; 

    private Image leftJoystickHandleImage; 
    private Image rightJoystickHandleImage; 
    private LeftJoystick leftJoystick; 
    private RightJoystick rightJoystick; 
    private int leftSideFingerID = 0; 
    private int rightSideFingerID = 0;

    void Start()
    {



            leftJoystick = leftJoystickBackgroundImage.GetComponent<LeftJoystick>(); 
            leftJoystickHandleImage = leftJoystick.transform.GetChild(0).GetComponent<Image>(); 
            rightJoystick = rightJoystickBackgroundImage.GetComponent<RightJoystick>(); 
            rightJoystickHandleImage = rightJoystick.transform.GetChild(0).GetComponent<Image>(); 
    }



    void FixedUpdate()
    {
        if (Input.touchCount > 0)
        {
            Touch[] myTouches = Input.touches;

            for (int i = 0; i < Input.touchCount; i++)
            {
                if (myTouches[i].phase == TouchPhase.Began)
                {
                        if (myTouches[i].position.x < Screen.width / 2)
                        {
                            leftSideFingerID = myTouches[i].fingerId;

                            if (leftJoystick.joystickStaysInFixedPosition == false)
                            {
                                var currentPosition = leftJoystickBackgroundImage.rectTransform.position;
                                currentPosition.x = myTouches[i].position.x + leftJoystickBackgroundImage.rectTransform.sizeDelta.x / 2; 
                                currentPosition.y = myTouches[i].position.y - leftJoystickBackgroundImage.rectTransform.sizeDelta.y / 2;

                                currentPosition.x = Mathf.Clamp(currentPosition.x, 0 + leftJoystickBackgroundImage.rectTransform.sizeDelta.x, Screen.width / 2);
                                currentPosition.y = Mathf.Clamp(currentPosition.y, 0, Screen.height - leftJoystickBackgroundImage.rectTransform.sizeDelta.y);

                                leftJoystickBackgroundImage.rectTransform.position = currentPosition;
                                leftJoystickBackgroundImage.enabled = true;
                                leftJoystickBackgroundImage.rectTransform.GetChild(0).GetComponent<Image>().enabled = true;
                            }
                            else
                            {
                                if ((myTouches[i].position.x <= leftJoystickBackgroundImage.rectTransform.position.x) && (myTouches[i].position.x >= (leftJoystickBackgroundImage.rectTransform.position.x - leftJoystickBackgroundImage.rectTransform.sizeDelta.x)))
                                {
                                    if ((myTouches[i].position.y >= leftJoystickBackgroundImage.rectTransform.position.y) && (myTouches[i].position.y <= (leftJoystickBackgroundImage.rectTransform.position.y + leftJoystickBackgroundImage.rectTransform.sizeDelta.y)))
                                    {
                                        leftJoystickBackgroundImage.enabled = true;
                                        leftJoystickBackgroundImage.rectTransform.GetChild(0).GetComponent<Image>().enabled = true;
                                    }
                                }
                            }
                        }

                    if (myTouches[i].position.x > Screen.width / 2)
                    {
                        rightSideFingerID = myTouches[i].fingerId; 

                        if (rightJoystick.joystickStaysInFixedPosition == false)
                        {
                            var currentPosition = rightJoystickBackgroundImage.rectTransform.position; 
                            currentPosition.x = myTouches[i].position.x + rightJoystickBackgroundImage.rectTransform.sizeDelta.x / 2; 
                            currentPosition.y = myTouches[i].position.y - rightJoystickBackgroundImage.rectTransform.sizeDelta.y / 2; 

                            // keep the right joystick on the right-side half of the screen
                            currentPosition.x = Mathf.Clamp(currentPosition.x, Screen.width / 2 + rightJoystickBackgroundImage.rectTransform.sizeDelta.x, Screen.width);
                            currentPosition.y = Mathf.Clamp(currentPosition.y, 0, Screen.height - rightJoystickBackgroundImage.rectTransform.sizeDelta.y);

                            rightJoystickBackgroundImage.rectTransform.position = currentPosition;

                            rightJoystickBackgroundImage.enabled = true;
                            rightJoystickBackgroundImage.rectTransform.GetChild(0).GetComponent<Image>().enabled = true;
                        }
                        else
                        {

                            if ((myTouches[i].position.x <= rightJoystickBackgroundImage.rectTransform.position.x) && (myTouches[i].position.x >= (rightJoystickBackgroundImage.rectTransform.position.x - rightJoystickBackgroundImage.rectTransform.sizeDelta.x)))
                            {
                                if ((myTouches[i].position.y >= rightJoystickBackgroundImage.rectTransform.position.y) && (myTouches[i].position.y <= (rightJoystickBackgroundImage.rectTransform.position.y + rightJoystickBackgroundImage.rectTransform.sizeDelta.y)))
                                {
                                    rightJoystickBackgroundImage.enabled = true;
                                    rightJoystickBackgroundImage.rectTransform.GetChild(0).GetComponent<Image>().enabled = true;
                                }
                            }
                        }
                    }
                }

                if (myTouches[i].phase == TouchPhase.Ended)
                {
                    if (myTouches[i].fingerId == leftSideFingerID)
                    {
                        leftJoystickBackgroundImage.enabled = leftJoystickAlwaysVisible;
                        leftJoystickHandleImage.enabled = leftJoystickAlwaysVisible;
                    }

                    if (myTouches[i].fingerId == rightSideFingerID)
                    {
                        rightJoystickBackgroundImage.enabled = rightJoystickAlwaysVisible;
                        rightJoystickHandleImage.enabled = rightJoystickAlwaysVisible;
                    }
                }
            }
        }
    }
}
