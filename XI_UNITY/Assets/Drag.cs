using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Drag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
	public static GameObject itemBeingDragged;
	Vector3 startPosition;
	Transform startParent;
	public bool isClicked=false;
	Vector3 forzePos = new Vector3 (-399, 234, 0);

	public float anchor_x=-397f;
	public float anchor_y=237;
	private float gap_x=171.7f;
	private float gap_y= 83f;
	private Color old_color = new Color32 (176,131,97,255);
	private string text;
   
	void start()
	{
		old_color=GetComponent<Image> ().color;
		Debug.Log ("color set");
		text = GetComponent<Text> ().text;
		Debug.Log (text);
		Debug.Log ("start");


	}



	#region IBeginDragHandler implementation
	
	public void OnBeginDrag (PointerEventData eventData)
	{
		itemBeingDragged = gameObject;
		startPosition = transform.position;
		startParent = transform.parent;
		GetComponent<CanvasGroup>().blocksRaycasts = false;

	}


	#endregion

	#region IDragHandler implementation
	
	public void OnDrag (PointerEventData eventData)
	{
		transform.position = eventData.position;
		GetComponent<Image> ().color= old_color;

		//Debug.Log (GetComponent<Image> ().color);
		//Debug.Log (old_color);

		text = GetComponentInChildren<Text> ().text;

		//Debug.Log (text);

	}
	
	#endregion
	
	#region IEndDragHandler implementation
	
	public void OnEndDrag (PointerEventData eventData)
	{

		itemBeingDragged = null;
		GetComponent<CanvasGroup>().blocksRaycasts = true;
		transform.localPosition=Auto_absorp (transform.localPosition.x, transform.localPosition.y);		


	}
	
	#endregion

	public Vector3 Auto_absorp(float x, float y)
	{
		for (float i = anchor_x; i < 4f * gap_x; i += gap_x)
			for (float j = anchor_y; j > -3f * gap_y; j -= gap_y)
				
			if (x < i + 120f && x > i - 120f)
				if (y < j + 50f && y > j - 50f) 
					{
				x = i;
				y = j;
//				Debug.Log (x);
//				Debug.Log (y);
				GetComponent<Image> ().color = Color.red;

			}
		
		
		return new Vector3 (x, y, 0);
	}

	void update()
	{
		Debug.Log ("start");

	}

	}





