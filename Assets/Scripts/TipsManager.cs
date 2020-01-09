using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TipsManager : MonoBehaviour {
	//手指出现的位置的数组
	public Transform[] tips;
	//手指预制
	public Image finger;
	//单例脚本
	public static TipsManager _tipsManager;
	//手指动画
	public Animator tipsAnimator; 
	void Statrt(){
		_tipsManager = this;
	}

	/// <summary>
	/// 手指提示出现的位置
	/// </summary>
	public void Tips(){
		//遍历手指出现的位置的数组
		for (int i = 0; i < tips.Length; i++) {
					Image tipsPre = Image.Instantiate (finger, tips [i].transform.position, Quaternion.identity);
		}
		tipsAnimator.SetTrigger ("tips");

	}

	/// <summary>
	/// 游戏场景出现的位置 
	/// </summary>
	//void GameTips(){
		//如果点击了下一步，则手指将出现在下一个位置上
		//if (Input.GetMouseButtonDown (0)) {
			//if (EventSystem.current.IsPointerOverGameObject ()) {

				//Image tipsPre = Image.Instantiate (finger, tips [i].transform.position, Quaternion.identity);
			//}
		//}
	//}

}
