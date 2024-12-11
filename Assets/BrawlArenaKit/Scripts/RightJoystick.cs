using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RightJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
	public delegate void OnDragStarted();
	public delegate void OnDragEnded();
	public OnDragStarted ondragstarted;
	public OnDragEnded ondragended;
    public bool joystickStaysInFixedPosition = false;
    public int joystickHandleDistance = 4;

    private Image bgImage;
    private Image joystickKnobImage;
    private Vector3 inputVector;
    private Vector3 unNormalizedInput;
    private Vector3[] fourCornersArray = new Vector3[4];
    private Vector2 bgImageStartPosition;

    private void Start()
    {

        if (GetComponent<Image>() != null && transform.GetChild(0).GetComponent<Image>() != null)
        {
            bgImage = GetComponent<Image>();
            joystickKnobImage = transform.GetChild(0).GetComponent<Image>();
            bgImage.rectTransform.GetWorldCorners(fourCornersArray);
            bgImageStartPosition = fourCornersArray[3];
            bgImage.rectTransform.pivot = new Vector2(1, 0);
            bgImage.rectTransform.anchorMin = new Vector2(1, 0);
            bgImage.rectTransform.anchorMax = new Vector2(1, 0);
            bgImage.rectTransform.position = bgImageStartPosition;
        }
    }

    public virtual void OnDrag(PointerEventData ped)
    {
        Vector2 localPoint = Vector2.zero;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(bgImage.rectTransform, ped.position, ped.pressEventCamera, out localPoint))
        {
  
            localPoint.x = (localPoint.x / bgImage.rectTransform.sizeDelta.x); 
            localPoint.y = (localPoint.y / bgImage.rectTransform.sizeDelta.y);


            inputVector = new Vector3(localPoint.x * 2 + 1, localPoint.y * 2 - 1, 0);

            unNormalizedInput = inputVector;

            inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

            joystickKnobImage.rectTransform.anchoredPosition =
             new Vector3(inputVector.x * (bgImage.rectTransform.sizeDelta.x / joystickHandleDistance),
                         inputVector.y * (bgImage.rectTransform.sizeDelta.y / joystickHandleDistance));

            if (joystickStaysInFixedPosition == false)
            {
                if (unNormalizedInput.magnitude > inputVector.magnitude)
                {
                    var currentPosition = bgImage.rectTransform.position;
                    currentPosition.x += ped.delta.x;
                    currentPosition.y += ped.delta.y;

                    currentPosition.x = Mathf.Clamp(currentPosition.x, Screen.width / 2 + bgImage.rectTransform.sizeDelta.x, Screen.width);
                    currentPosition.y = Mathf.Clamp(currentPosition.y, 0, Screen.height - bgImage.rectTransform.sizeDelta.y);

                    bgImage.rectTransform.position = currentPosition;
                }
            }
        }
    }

    public virtual void OnPointerDown(PointerEventData ped)
    {
        OnDrag(ped); 
		ondragstarted();

    }

    public virtual void OnPointerUp(PointerEventData ped)
    {
		ondragended ();
        inputVector = Vector3.zero; 
        joystickKnobImage.rectTransform.anchoredPosition = Vector3.zero;
    }

    public Vector3 GetInputDirection()
    {
        return new Vector3(inputVector.x, inputVector.y, 0); 
    }
}