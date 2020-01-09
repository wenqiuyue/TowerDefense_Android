using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Guidance : MonoBehaviour {
	//新手引导UI
	public GameObject _keyDescription;
	//游戏说明UI
	public GameObject _gameDescription;
	public GameObject[] gameDescriptionGroup;
	//下一步按钮图片
	public Sprite next;
	//退出按钮图片
	public Sprite signOut;
	//右下角按钮
	public Button button;
	//左下角按钮
	public GameObject leftButton;
	//游戏说明图片下标
	int index = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	/// <summary>
	/// 点击新手引导，进入按键说明界面
	/// </summary>
	public void KeyDescription(){
		SoundPlay._soundPlay.PlaySound ("button");
		_keyDescription.SetActive (true);
	}
	/// <summary>
	/// 在新手引导中，进入游戏说明
	/// </summary>
	public void GameDescription(){
		SoundPlay._soundPlay.PlaySound ("button");
		_keyDescription.SetActive (false);
		_gameDescription.SetActive (true);
		if(index==0)
		leftButton.SetActive (false);
	}
	/// <summary>
	/// 新手引导返回游戏按钮
	/// </summary>
	public void MenuCard(){
		SoundPlay._soundPlay.PlaySound ("button");
		_keyDescription.SetActive (false);
		GameButton._gameButton._bgUI.SetActive (true);
	}
		


	/// <summary>
	/// 游戏说明的下一步按钮
	/// </summary>
	public void GameDescriptionNext(){
		SoundPlay._soundPlay.PlaySound ("button");
		leftButton.SetActive (true);
		//if (index == 6)
			//return;
		if (index == 6) {
			//button.GetComponent<Image> ().sprite = signOut;
			_gameDescription.SetActive (false);
			GameButton._gameButton._bgUI.SetActive (true);
			return;
		}
		//if (index == 6)
			//return;
			button.GetComponent<Image> ().sprite = next;
			gameDescriptionGroup [index].SetActive (false);
			index = (index + 1);
		if (index == 6) {
			button.GetComponent<Image> ().sprite = signOut;

		}
			//for (i = 0; i < gameDescriptionGroup.Length;) {
			//gameDescriptionGroup [i-1].SetActive (false);
			gameDescriptionGroup [index].SetActive (true);
		//}

	}
	/// <summary>
	/// 游戏说明的上一步按钮
	/// </summary>
	public void GameDescriptionLastStep(){
		SoundPlay._soundPlay.PlaySound ("button");

			
		
		//if (index == 0) 
			//return;
		
		button.GetComponent<Image> ().sprite = next;
		gameDescriptionGroup [index].SetActive (false);
		index = (index - 1);

		if(index == 0){
			leftButton.SetActive (false);

		}
		//for (i = 0; i < gameDescriptionGroup.Length;) {
		//gameDescriptionGroup [i-1].SetActive (false);

		gameDescriptionGroup [index].SetActive (true);
		//}

	}


}
