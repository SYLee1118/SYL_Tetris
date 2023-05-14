using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI uiText;

	public void SetText(string _text)
	{
		uiText.text = _text;
	}
}
