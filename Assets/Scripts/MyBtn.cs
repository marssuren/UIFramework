using UnityEngine;
using System.Collections;

public class MyBtn : MonoBehaviour 
{

	
	void Start ()
	{
		GetComponent<Animation>().Play("TestAnim");
	}
	
	
	void Update () 
	{
	
	}
}
