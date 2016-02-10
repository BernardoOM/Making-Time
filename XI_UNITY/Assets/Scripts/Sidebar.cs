using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Sidebar : MonoBehaviour
{
	public Text	energy;
	public Text happiness;

	public void ChangeValues(int newEnergy, int newHappiness)
	{
		energy.text = energy.text.Remove(energy.text.Length - 1) + (newEnergy + 5);
		happiness.text = happiness.text.Remove(happiness.text.Length - 1) + (newHappiness + 5);
	}
}
