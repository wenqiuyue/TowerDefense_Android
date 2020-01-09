using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AddTotalMoney : MonoBehaviour {
	//所拥有的钱
	public int totalMoney=500;
	public static AddTotalMoney _addTotalMoney;
	//当前场景编号
	public int index;

	// Use this for initialization
	void Awake () {
		_addTotalMoney = this;
		//print (SceneManager.GetActiveScene ().buildIndex);
		//获取当前场景
		index=SceneManager.GetActiveScene ().buildIndex;
		//如果在第二关时
		if (index == 3) {
			//如果第一关没有食物到达终点，则金币初始值为800
			if (CheckPointManager._checkPointManager.starCountList [0] == 0) {
				totalMoney = 800;
			}
			//如果达到终点的食物有1个，则金币初始值为700
			if (CheckPointManager._checkPointManager.starCountList [0] == 1) {
				totalMoney = 700;
			}
			//如果达到终点的食物有2个，则金币初始值为600
			if (CheckPointManager._checkPointManager.starCountList [0] == 2) {
				totalMoney = 600;
			}
		}
		//如果在第三关时
		if (index == 4) {
			//如果第二关没有食物到达终点，则金币初始值为800
			if (CheckPointManager._checkPointManager.starCountList [1] == 0) {
				totalMoney = 800;
			}
			if (CheckPointManager._checkPointManager.starCountList [1] == 1) {
				totalMoney = 700;
			}
			if (CheckPointManager._checkPointManager.starCountList [1] == 2) {
				totalMoney = 600;
			}
		}
		Debug.Log (CheckPointManager._checkPointManager.starCountList [0]);
		Debug.Log (CheckPointManager._checkPointManager.starCountList [1]);
	}

}
