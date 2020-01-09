using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckPointManager : MonoBehaviour {
	//单例脚本
	public static CheckPointManager _checkPointManager;
	/// <summary>
	/// null关卡未解锁，false未通过，true已经通过
	/// </summary>
	[HideInInspector]
	public List<bool?> isLevelPassList = new List<bool?>(){true, null, null};
	//已解锁图片数组
	[SerializeField]
	Sprite[] unLocked;
	//未解锁图片数组
	[SerializeField]
	Sprite[] noUnLocked;

	/// <summary>
	/// 第1关和第2关分别获得的星星个数所增加的金币
	/// </summary>

	public List<int> starCountList = new List<int>(){0, 0};

	//关卡UI
	//[SerializeField]
	//Image[] cardArray;
	// Use this for initialization
	void Awake(){
		//跳转场景，不销毁物体自身
		DontDestroyOnLoad(this.gameObject);
	}
	void Start () {
		//单列脚本
		_checkPointManager = this;

	}

	/// <summary>
	/// 解锁
	/// </summary>
	/// <param name="_checkpoint">当前关卡</param>
	/// <param name="cardArray">三个关卡UI</param>
	/// <param name="isAdopt">是否通过关卡，通过为true，未通过为false</param>
	public void CheckPointUI(int _checkpoint,Button[] cardArray,bool isAdopt=false){
		//如果关卡通过
		if(isAdopt==true){
			if (_checkpoint >= 2 && _checkpoint <= 4) {
				// 当前关卡通过
				isLevelPassList [_checkpoint-2] = true;
				// 下一关卡解锁
				if(_checkpoint <= 3 && isLevelPassList [_checkpoint-1] == null)
					isLevelPassList [_checkpoint-1] = false;

			}
		}

		UpdateLevelSprite (cardArray);

		//for (int a = 0; a < isLevelPassList.Count; a++) {
		//	Debug.Log(isLevelPassList[a]);
		//}
	}

	/// <summary>
	/// 更新关卡图标
	/// </summary>
	/// <param name="cardArray">Card array.</param>
	public void UpdateLevelSprite(Button[] cardArray){
		//遍历关卡解锁列表
		for (int i = 0; i < isLevelPassList.Count; i++) {
			if (i >= cardArray.Length)
				break;

			//如果列表元素为false或null，则解锁UI图片为未解锁图片，否则为解锁图片
			if (isLevelPassList [i] == null) {
				//cardArray [i].GetComponent<Button>().image.sprite = noUnLocked [i];
				cardArray [i].enabled = false;
				cardArray [i].GetComponent<Image> ().sprite = noUnLocked [i];

			} else {
				//cardArray [i].GetComponent<Button>().image.sprite = unLocked [i];
				cardArray [i].enabled = true;
				cardArray [i].GetComponent<Image> ().sprite = unLocked [i];
			}
		}
	}
}
