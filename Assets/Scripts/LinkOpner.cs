using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkOpner : MonoBehaviour
{
	public void OpenLinks(string URL)
	{
		Application.OpenURL(URL);
	}
}
