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

	#region IBeginDragHandler implementation
	
	public void OnBeginDrag (PointerEventData eventData)
	{
		itemBeingDragged = gameObject;
		startPosition = transform.position;
		startParent = transform.parent;
		GetComponent<CanvasGroup>().blocksRaycasts = false;

		//if(Input.GetMouseButton(0))
//		GUI.Label(new Rect(5,5,200,200), "This is " + this.name);
	}


	#endregion
	
	#region IDragHandler implementation
	
	public void OnDrag (PointerEventData eventData)
	{
		transform.position = eventData.position;
	}
	
	#endregion
	
	#region IEndDragHandler implementation
	
	public void OnEndDrag (PointerEventData eventData)
	{

		itemBeingDragged = null;
		GetComponent<CanvasGroup>().blocksRaycasts = true;
				
		if (transform.localPosition.x <= -318 && transform.localPosition.x >= -481) {
			if (transform.localPosition.y <= 274 && transform.localPosition.y >= 194) {
				transform.localPosition = forzePos;
			}
		}
		else
		{
			Debug.Log(transform.position.x.ToString());
		}
//		if(transform.parent == startParent){
//			transform.position = startPosition;
//		}

	}
	
	#endregion

//	public string name = "Some name";
//	
//	public void Start()
//	{
//		if(gameObject.GetComponent(Collider) == null)
//			gameObject.AddComponent(typeof(BoxCollider));
//	}
//	
//	public void OnMouseDown()
//	{
//		isClicked = true;
//	}
//	
//	public void OnMouseUp()
//	{
//		isClicked = false;
//	}
//	
//	public void OnGUI()
//	{
//		if(isClicked)
//			GUI.Label(new Rect(5,5,400,100), "This is " + this.name);
//	} 



//onClick
//
//	////////////////////
//	var isClicked : boolean;
//	
//	function Start () 
//	{
//		isClicked = false;
//		if (gameObject.GetComponent(Collider) == null)
//		{
//			gameObject.AddComponent(typeof(BoxCollider));
//		}
//	}
//	
//	function OnInputDown()
//	{
//		isClicked = true;
//	}
//	
//	function OnInputUp()
//	{
//		isClicked = false;
//	}
//	
//	function OnGUI()
//	{
//		if (isClicked == true)
//		{
//			GUI.Label(new Rect(5,5,400,100), "This is " + this.name);
//		}
//	}
//	
	
}
