using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Sidebar : MonoBehaviour
{
	public Text	energy;
	public Text happiness;

	public void ChangeValues(int newEnergy, int newHappiness)
	{
		energy.text = (newEnergy + 5).ToString();
		happiness.text = (newHappiness + 5).ToString();
	}
}
