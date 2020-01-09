using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameButton : MonoBehaviour {
	public GameObject _checkPointUI;
	public GameObject _bgUI;
	public static GameButton _gameButton;
	//private TipsManager tipsManager;
	void Start(){
		_gameButton = this;
		//tipsManager = GetComponent<TipsManager> ();
	}
	/// <summary>
	/// 开始游戏按钮
	/// </summary>
	public void OnBeginButton(){
		SoundPlay._soundPlay.PlaySound ("button");
		_bgUI.SetActive (false);
		_checkPointUI.SetActive (true);
	}
	/// <summary>
	/// 开始游戏按钮
	/// </summary>
	public void NextCard(){
		SoundPlay._soundPlay.PlaySound ("button");
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex+1);
		SoundPlay._soundPlay.StopMusic ();
		SoundPlay._soundPlay.PlayMusic("game");
	}
	/// <summary>
	/// 回到游戏菜单界面
	/// </summary>
	public void MenuCard(){
		_checkPointUI.SetActive (false);
		_bgUI.SetActive (true);
	}
	/// <summary>
	/// 退出游戏按钮
	/// </summary>
	public void OnReturnButton(){
		
			#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
			#else
			Application.Quit();
			#endif
	}

	/*public void ClickGuidance(){
		OnBeginButton ();
		_tipsUI.SetActive (true);
		tipsManager.Tips ();

	}*/
}
