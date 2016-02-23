using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class Title_Screen : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public	void LoadScene(){
		SceneManager.LoadScene (1);	}
}
