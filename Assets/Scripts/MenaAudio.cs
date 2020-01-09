using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenaAudio : MonoBehaviour {
	//当前场景下标
	int index;
	void Update () {
		//获取当前场景
		index = SceneManager.GetActiveScene().buildIndex;
		//Debug.Log (index);
		//首页
		if (index == 0) {
			SoundPlay._soundPlay.PlayMusic ("start");
			return;
		}else if(index == 1){
			//SoundPlay._soundPlay.PlayMusic ("game");
			return;
		}
	}
}
