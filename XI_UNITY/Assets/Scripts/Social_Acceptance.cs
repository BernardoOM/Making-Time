using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//script for display inviatation window, ask player accept or refuse.  binded with "Window_Accept_Social" prefab 

public class Social_Acceptance : MonoBehaviour {

	private int num;
	private Commitment temp_com;
	private string NPCPath = "Sprites/Characters/Char_01_Full"; 
	private int rand_num;
	public Image NPCImage;
	// Use this for initialization
	void Start () {
		transform.SetParent (GameObject.Find ("Calendar").transform, false);
		transform.localPosition= new Vector3(0, 0, 0);
		if (GameManager.UI.CuSceneBool == true) {
			transform.SetParent (GameObject.Find ("Canvas").transform, false);
			transform.localPosition = new Vector3(0, 0, 0);

		}
		rand_num=Random.Range (1, 10);
		NPCPath= "Sprites/Characters/Char_0"+rand_num+ "_Full";
		NPCImage.GetComponent<Image>().sprite =(Sprite) Resources.Load(NPCPath, typeof(Sprite));
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Acept_Window(Commitment com){
        temp_com = com;
		transform.FindChild("Invitation_Text").gameObject.GetComponent<Text>().text
		="A new invitation to "+ com.name;

		//pause game
		GameManager.Instance.PauseGame ();
	} 

    // being called by button onclick(), in prefab - acept button.
	public void Acept_Event(){
		Destroy (gameObject);
		//if clicked, resume game
		GameManager.Instance.StartGame();

	}
    //being called like above, refuse button.
	public void Refuse_Event(){
		GameManager.Calendar.refuse_social (temp_com);
		Destroy (gameObject);
		//if clicked, resume game
		GameManager.Instance.StartGame();

	}
}
