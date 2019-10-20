// Inherited from https://github.com/Pquah/Beowulf/blob/master/Assets/SimpleDragAndDrop/Scripts/DragAndDropItem.cs

// Developed / Adapted by Ahmet Gungor

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

/// <summary>
/// Every "drag and drop" item must contain this script
/// </summary>
[RequireComponent(typeof(Image))]
public class DragAndDropItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    static public DragAndDropItem draggedItem;                                      // Item that is dragged now
    static public GameObject icon;                                                  // Icon of dragged item
    static public DragAndDropCell sourceCell;                                       // From this cell dragged item is

    public delegate void DragEvent(DragAndDropItem item);
    static public event DragEvent OnItemDragStartEvent;                             // Drag start event
    static public event DragEvent OnItemDragEndEvent;                               // Drag end event

    /// <summary>
    /// This item is dragged
    /// </summary>
    /// <param name="eventData"></param>
    
	void Start()
	{
		
	}
	public void OnBeginDrag(PointerEventData eventData)
    {
		//StartCoroutine (SoundBombGrab ());
		//-----------------------------Debug.Log("BIGBUG-ONGBEGINDRAG-START");
        sourceCell = GetComponentInParent<DragAndDropCell>();                       // Remember source cell
        draggedItem = this;                                                         // Set as dragged item
        icon = new GameObject("Icon");                                              // Create object for item's icon
		icon.tag = "Player";
        Image image = icon.AddComponent<Image>();
		//image.color = GetComponent<Image>().color;
		image.sprite = GetComponentInChildren<Image>().sprite;
        //image.sprite = GetComponent<Image>().sprite;
        image.raycastTarget = false;                                                // Disable icon's raycast for correct drop handling
        RectTransform iconRect = icon.GetComponent<RectTransform>();
        // Set icon's dimensions

		//iconRect.sizeDelta = new Vector2( 10,10);
		iconRect.sizeDelta = new Vector2( Mathf.Abs(GetComponent<RectTransform>().sizeDelta.x),
										  Mathf.Abs(GetComponent<RectTransform>().sizeDelta.y));
		//Debug.Log(GetComponent<RectTransform>().sizeDelta.x + " ---- " + GetComponent<RectTransform>().sizeDelta.y);
        Canvas canvas = GetComponentInParent<Canvas>();                             // Get parent canvas
        if (canvas != null)
        {
            // Display on top of all GUI (in parent canvas)
            icon.transform.SetParent(canvas.transform, true);                       // Set canvas as parent
            icon.transform.SetAsLastSibling();                                      // Set as last child in canvas transform
        }

        if (OnItemDragStartEvent != null)
        {
            OnItemDragStartEvent(this);                                             // Notify all about item drag start
        }
		//-----------------------------Debug.Log("BIGBUG-ONGBEGINDRAG-END");
    }

    /// <summary>
    /// Every frame on this item drag
    /// </summary>
    /// <param name="data"></param>
    public void OnDrag(PointerEventData data)
    {
        if (icon != null)
        {
			//-----------------------------Debug.Log("BIGBUG-ONDRAG-START");
			#if UNITY_EDITOR
            icon.transform.position = Input.mousePosition;                          // Item's icon follows to cursor
			#endif

			#if UNITY_ANDROID
			/*
			if (Input.touchCount > 0) 
			{
				Touch touch = Input.GetTouch(0); // get first touch since touch count is greater than zero

				if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved) 
				{
					// get the touch position from the screen touch to world point
					icon.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10));
					// lerp and set the position of the current object to that of the touch, but smoothly over time.
					//transform.position = Vector3.Lerp(transform.position, touchedPos, Time.deltaTime);
				}
			}*/
            
            // Ahmet Gungor
			if (Input.touchCount > 0)
			{
				foreach (Touch touch in Input.touches)
				{
					if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved) 
					{
						icon.GetComponent<RectTransform>().sizeDelta = new Vector2( 100,
																					100);
						icon.transform.position = touch.position;
						//-----------------------------Debug.Log("BIGBUG-ONDRAG-PROCESS");
					}
				}
			}


			#endif
			//-----------------------------Debug.Log("BIGBUG-ONDRAG-END");
			//Debug.DrawRay(icon.transform.position,new Vector3 (40,190,0),Color.black);
        }
    }



    /// <summary>
    /// This item is dropped
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData)
    {
		//-----------------------------Debug.Log("BIGBUG-ONENDDRAG-START");
		StartCoroutine (SoundBombGrab ());

        if (icon != null)
        {
            Destroy(icon);                                                          // Destroy icon on item drop
        }
        //MakeVisible(true);                                                          // Make item visible in cell
        if (OnItemDragEndEvent != null)
        {
            OnItemDragEndEvent(this);                                               // Notify all cells about item drag end
        }
        draggedItem = null;
        icon = null;
        sourceCell = null;
		//-----------------------------Debug.Log("BIGBUG-ONENDDRAG-END");
    }

    /// <summary>
    /// Enable item's raycast
    /// </summary>
    /// <param name="condition"> true - enable, false - disable </param>
    public void MakeRaycast(bool condition)
    {
        Image image = GetComponent<Image>();
        if (image != null)
        {
            image.raycastTarget = condition;
        }
    }

    /// <summary>
    /// Enable item's visibility
    /// </summary>
    /// <param name="condition"> true - enable, false - disable </param>
    public void MakeVisible(bool condition)
    {
        GetComponent<Image>().enabled = condition;
    }

    // Ahmet Gungor
    
	IEnumerator SoundBombGrab()
	{
		if(SoundPersistence.soundIsOpen == true)
			FindObjectOfType<gameSounds>().Play("bombGrab");

		yield return null;
	}

	IEnumerator SoundBombDrop()
	{
		if(SoundPersistence.soundIsOpen == true)
			FindObjectOfType<gameSounds>().Play("bombDrop");
		
		yield return null;
	}
}
