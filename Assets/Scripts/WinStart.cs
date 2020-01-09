using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinStart : MonoBehaviour {
	//灰色的星星
	[SerializeField]
	Image[] grayStart;
	//增加金币提示界面
	[SerializeField]
	GameObject _addMoneyUI;
	float rate=0.2f;
	void Start(){
		StartCoroutine ("StartShow");
	}
	/// <summary>
	/// 星星显示
	/// </summary>
	private IEnumerator StartShow(){
		//遍历灰色星星图片数组
		//没有食物到达终点
		if(FoodIncubator._foodIncubator.destroyCount==0){
			for (int i = 0; i < grayStart.Length; i++) {
				grayStart [i].color = new Color (255,255,255,0);
				yield return new WaitForSeconds (rate);
			}

		}
		//到达终点的食物为1个
		if(FoodIncubator._foodIncubator.destroyCount==1){
			for (int i = 0; i < grayStart.Length-1; i++) {
				grayStart [i].color = new Color (255,255,255,0);
				yield return new WaitForSeconds (rate);
			}

		}
		//到达终点的食物为2个
		if(FoodIncubator._foodIncubator.destroyCount==2){
			for (int i = 0; i < grayStart.Length-2; i++) {
				grayStart [i].color = new Color (255,255,255,0);
				yield return new WaitForSeconds (rate);
			}

		}
		Debug.Log ("count"+FoodIncubator._foodIncubator.destroyCount);
	}

	/// <summary>
	/// 关闭增加金币提示界面按钮
	/// </summary>
	public void CloseButton(){
		_addMoneyUI.SetActive (false);
	}
}
