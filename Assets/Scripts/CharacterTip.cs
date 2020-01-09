using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CharacterTip : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler {
	public GameObject characterUI;
	/// <summary>
	/// 鼠标进入事件
	/// </summary>
	public void OnPointerEnter(PointerEventData eventData){
		characterUI.SetActive (true);
	}

	/// <summary>
	/// 鼠标离开事件
	/// </summary>
	public void OnPointerExit(PointerEventData eventData){
		characterUI.SetActive (false);
	}
}
