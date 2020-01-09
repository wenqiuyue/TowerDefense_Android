using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class FoodIncubator : MonoBehaviour {
	//到达终点的食物个数
	public int destroyCount=0;
	//食物还存在的个数
	public static int FoodCountAlive=0;
	//食物波数
	public Wave[] waves;
	//生成食物的起始点
	public Transform START;
	//等待时间
	//public float rateTime=3;
	//将生成的食物加入列表
	public static List<GameObject> _foodListPre=new List<GameObject>();
	public float _waveRate=0.2f;
	//食物个数显示文本
	[SerializeField]
	Text foodCountShow;
	//脚本单例
	public static FoodIncubator _foodIncubator;
	//private Coroutine _coroutine;
	// Use this for initialization
	void Start () {
		_foodIncubator = this;
		StartCoroutine ("IncubatorFood");
	}

	public void UpdateFoodCount(int changefood=0){
		// UI显示食物个数
		foodCountShow.text = ""+changefood;
	}

	/// <summary>
	/// 当游戏失败时，食物不在生成
	/// </summary>
	public void Stop(){
		StopCoroutine ("IncubatorFood");
	}

	/// <summary>
	/// 使用协程创建食物孵化器
	/// </summary>
	/// <returns>The food.</returns>
	IEnumerator IncubatorFood(){
		foreach (Wave wave in waves) {
			for (int i = 0; i < wave.foodCount; i++) {
				GameObject foodPre=GameObject.Instantiate (wave.foodPrefab, START.position, Quaternion.identity);
				//食物还存在的个数加1
				FoodCountAlive++;
				//Debug.Log ("增加"+FoodCountAlive);
				_foodListPre.Add (foodPre);
				UpdateFoodCount (FoodCountAlive);
				if (i != wave.foodCount-1);
				yield return new WaitForSeconds (wave.rate);
			
			}
			if (GameManager.Instance._scenes == 1) {
				while (FoodCountAlive > 0) {
					yield return 0;
				}
				yield return new WaitForSeconds (_waveRate);
			} else if (GameManager.Instance._scenes == 2) {
				yield return new WaitForSeconds (_waveRate);
			} 
		}
		//食物生成完，且场景中还存有食物
		while(FoodCountAlive>0){
			yield return 0;
		}
		//当所有食物都被消灭，则胜利
		GameManager.Instance.Win();


	}
}
