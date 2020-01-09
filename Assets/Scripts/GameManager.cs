using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour {
	//根据路径数来表示场景的编号
	public int _scenes=1;
	//普通场景编号
	public int _ordinaryScenes=1;
	//结束文本
	public Text _endText;
	//显示文本的速率
	public float _endTextRate = 0.6f;
	private string _word;
	//失败UI
	public GameObject _endUI;
	//胜利UI
	public GameObject _winUI;
	//选择关卡UI
	public GameObject _selectUI;
	//退出游戏弹窗
	public GameObject _tanChuangUI;
	//灰色的星星
	public Image[] grayStart;
	//灰色星星消失的时间间隔
	float rate=1f;
	//单例GameManager，方便调用
	public static GameManager Instance;
	//马桶出现动画
	public Animator animator1;
	//第二排人出现动画
	public Animator animator2;
	//第三排人出现动画
	public Animator animator3;
	//光的闪烁动画
	public Animator _guangAnimator;
	private FoodIncubator foodIncubator; 
	//暂停键的当前时间速率
	float nowTime;
	//退出游戏弹窗的当前时间速率
	float tanChuangNowTime;
	int _checkpoint;
	/*
	/// <summary>
	/// null关卡未解锁，false未通过，true已经通过
	/// </summary>
	List<bool?> isLevelPassList = new List<bool?>(){false, null, null};
	//已解锁图片数组
	[SerializeField]
	Sprite[] unLocked;
	//未解锁图片数组
	[SerializeField]
	Sprite[] noUnLocked;
	*/
	//关卡UI
	[SerializeField]
	Button[] cardArray;
	//按钮
	[SerializeField]
	Button[] button;
	//快进图片数组
	[SerializeField]
	Sprite[] fast;
	//暂停播放图片数组
	[SerializeField]
	Sprite[] stop;
	//增加金币图片数组
	[SerializeField]
	Sprite[] addMoney;
	//增加金币提示界面
	[SerializeField]
	GameObject _addMoneyUI;
	//增加金币文字图片提示UI
	[SerializeField]
	GameObject _addMoneyTextUI;
	public bool _isPause = false;

	void Awake(){
		
		Instance = this;
		foodIncubator = GetComponent<FoodIncubator> ();
		//获取当前场景
		_checkpoint=SceneManager.GetActiveScene ().buildIndex;
	}
	void Start(){
		//cardArray = this.gameObject.GetComponents<Button> ();
		if(_scenes == 2){
		_word = _endText.text;
		_endText.text="";
		//StartCoroutine (EndText());
		}
		//Win ();
		return;
	}

	private IEnumerator EndText(){
		foreach (char endText in _word.ToCharArray()) {
			_endText.text += endText;
			yield return new WaitForSeconds (_endTextRate);
		}

	}
	/// <summary>
	/// 控制失败
	/// </summary>
	public void Failed(){
		_isPause = true;

		//播放失败音乐
		SoundPlay._soundPlay.PlayMusic("gameover");
		foodIncubator.Stop ();
		_endUI.SetActive (true);
		CheckPointManager._checkPointManager.CheckPointUI(_checkpoint,cardArray,false);
		//第一关时到达终点的食物个数
		if(AddTotalMoney._addTotalMoney.index==2){
			CheckPointManager._checkPointManager.starCountList [0] = FoodIncubator._foodIncubator.destroyCount;

		}
		//第二关时到达终点的食物个数
		if(AddTotalMoney._addTotalMoney.index==3){
			CheckPointManager._checkPointManager.starCountList [1] = FoodIncubator._foodIncubator.destroyCount;
		}
		Debug.LogFormat ("{0}{1}",AddTotalMoney._addTotalMoney.index,FoodIncubator._foodIncubator.destroyCount);
		//_endText.text = "失败";
		//_word = _endText.text;
		//_endText.text="";
		//StartCoroutine (EndText());

	}
	/// <summary>
	/// 控制胜利
	/// </summary>
	public void Win(){
		_isPause = true;
		_winUI.SetActive (true);	
		//如果场景为1时
		if (_scenes == 1) {
			//播放胜利音乐
			SoundPlay._soundPlay.PlayMusic ("win");
			StartCoroutine ("StartShow");


		}
		/*for(int i=0;i<CheckPointManager._checkPointManager.isLevelPassList.Count;i++){
			if (CheckPointManager._checkPointManager.isLevelPassList [i] == true) {
				cardArray[i].onClick.AddListener (delegate {
					ClickCard(i);
				});
				Debug.Log ("i"+i);
			}
		}*/
		if (_scenes == 2) {
			//播放卡路里音乐
			SoundPlay._soundPlay.PlayMusic ("kaluli");
			//animator1.SetTrigger ("rotate");
			//animator2.SetTrigger ("Image3");
			//animator3.SetTrigger ("Image2");
			_endText.text = "不是我们不想减肥，只是敌人太过强大！";
			_word = _endText.text;
			_endText.text = "";
			StartCoroutine (EndText ());
		}
		CheckPointManager._checkPointManager.CheckPointUI(_checkpoint,cardArray,true);
		//第一关时到达终点的食物个数
		if(AddTotalMoney._addTotalMoney.index==2){
			CheckPointManager._checkPointManager.starCountList [0] = FoodIncubator._foodIncubator.destroyCount;

		}
		//第二关时到达终点的食物个数
		if(AddTotalMoney._addTotalMoney.index==3){
			CheckPointManager._checkPointManager.starCountList [1] = FoodIncubator._foodIncubator.destroyCount;
		}
		Debug.LogFormat ("{0}{1}",AddTotalMoney._addTotalMoney.index,FoodIncubator._foodIncubator.destroyCount);
	

	}
	/// <summary>
	/// 返回游戏按钮
	/// </summary>
	public void ButtonReturn(){
		SoundPlay._soundPlay.PlaySound ("button");
		FoodIncubator.FoodCountAlive = 0;
		Time.timeScale = 1;
		SceneManager.LoadScene (1);
		SoundPlay._soundPlay.PlayMusic("start");
	}

	/// <summary>
	/// 退出游戏弹窗
	/// </summary>
	public void ButtonReturnGame(){
		SoundPlay._soundPlay.PlaySound ("button");
		//记录当前游戏的速率
		tanChuangNowTime = Time.timeScale;
		//暂停
		Time.timeScale = 0;
		_tanChuangUI.SetActive (true);
	}

	/// <summary>
	/// 继续游戏按钮
	/// </summary>
	public void ContinueGameButton(){
		SoundPlay._soundPlay.PlaySound ("button");
		_tanChuangUI.SetActive (false);
		//将游戏速率变为暂停前的速率
		Time.timeScale=tanChuangNowTime;
	}
	/// <summary>
	/// 回到当前关卡
	/// </summary>
	public void NowCard(){
		SoundPlay._soundPlay.PlaySound ("button");
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
		SoundPlay._soundPlay.StopMusic ();
		SoundPlay._soundPlay.PlayMusic("game");
		AddTotalMoney._addTotalMoney.totalMoney = 500;
		if (Time.timeScale == 2) {
			Time.timeScale = 1;
		}
		
	}
	/// <summary>
	/// 回到上一关卡
	/// </summary>
	public void lastCard(){
		SoundPlay._soundPlay.PlaySound ("button");
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex-1);
		SoundPlay._soundPlay.StopMusic ();
		SoundPlay._soundPlay.PlayMusic("game");

	}
	/// <summary>
	/// 进入下一关按钮
	/// </summary>
	public void ButtonNext(){
		//将进入下一个游戏界面的初始初率为1 
		Time.timeScale = 1;
		SoundPlay._soundPlay.PlaySound ("button");
		//获取当前场景
		//_checkpoint=SceneManager.GetActiveScene ().buildIndex;
		//如果场景没有在最后一个场景
		if (_checkpoint <= 3) {
			//跳转到当前场景的后一个场景
			SceneManager.LoadScene (_checkpoint + 1);
		} else {
			//如果当前场景是最后一个场景，则直接跳转到第二个场景
			SceneManager.LoadScene (2);
		}
		Debug.Log ("_checkpoint"+_checkpoint);
		SoundPlay._soundPlay.PlayMusic("game");
	}
	/// <summary>
	/// 关卡
	/// </summary>
	public void ClickCard(int i){
		SoundPlay._soundPlay.PlaySound ("button");
		SceneManager.LoadScene (i+2);
		SoundPlay._soundPlay.StopMusic ();
		SoundPlay._soundPlay.PlayMusic("game");
		if (Time.timeScale == 2) {
			Time.timeScale = 1;
		}

	}

	/// <summary>
	/// 快进按钮
	/// </summary>
	public void FastButton(){
		SoundPlay._soundPlay.PlaySound ("button");
		//如果在正常速度下点击按钮
		if(Time.timeScale==1){
			//将速度变为2倍速
			Time.timeScale=2;
			//将按钮图片变为慢速图片
			button [0].GetComponent<Image> ().sprite = fast [1];
		}else if(Time.timeScale==2){
			//如果在2倍速状态下按下按钮，则将速度变为正常速度
			Time.timeScale=1;
			//将按钮图片变为加速图片
			button [0].GetComponent<Image> ().sprite = fast [0];
		}
	}

	/// <summary>
	/// 暂停播放按钮
	/// </summary>
	public void StopButton(){
		SoundPlay._soundPlay.PlaySound ("button");

		//如果在运行状态点击暂停按钮
		if(Time.timeScale != 0){
			nowTime = Time.timeScale;
			//暂停
			Time.timeScale = 0;
			//将按钮图片由播放图片变为暂停图片
			button [1].GetComponent<Image> ().sprite = stop [0];
		}else if(Time.timeScale==0){
			//如果在暂停状态点击播放按钮,则返回到暂停前的播放速度进行播放
			Time.timeScale=nowTime;
			//将按钮图片由暂停图片变为播放图片
			button [1].GetComponent<Image> ().sprite = stop [1];
		}
	}

	/// <summary>
	/// 回到选择关卡的界面
	/// </summary>
	public void MeneButton(){
		SoundPlay._soundPlay.PlaySound ("button");
		_winUI.SetActive (false);
		_selectUI.SetActive (true);
		//CheckPointManager._checkPointManager.CheckPointUI(_checkpoint,cardArray,true);

	}
	/// <summary>
	///星星显示 
	/// </summary>
	private IEnumerator StartShow(){
		//遍历灰色星星图片数组
		//没有食物到达终点
		if(FoodIncubator._foodIncubator.destroyCount==0){
			for (int i = 0; i < grayStart.Length; i++) {
				SoundPlay._soundPlay.PlaySound ("star");
				grayStart [i].color = new Color (255,255,255,0);
				yield return new WaitForSeconds (rate);
			}
			AddMoney (3);
		}
		//到达终点的食物为1个
		if(FoodIncubator._foodIncubator.destroyCount==1){
			for (int i = 0; i < grayStart.Length-1; i++) {
				SoundPlay._soundPlay.PlaySound ("star");
				grayStart [i].color = new Color (255,255,255,0);
				yield return new WaitForSeconds (rate);
			}
			AddMoney (2);
		}
		//到达终点的食物为2个
		if(FoodIncubator._foodIncubator.destroyCount==2){
			for (int i = 0; i < grayStart.Length-2; i++) {
				SoundPlay._soundPlay.PlaySound ("star");
				grayStart [i].color = new Color (255,255,255,0);
				yield return new WaitForSeconds (rate);
			}
			AddMoney (1);
		}
		Debug.Log ("count"+FoodIncubator._foodIncubator.destroyCount);
	}
	/// <summary>
	/// 关闭增加金币提示界面按钮
	/// </summary>
	public void CloseButton(){
		SoundPlay._soundPlay.PlaySound ("button");
		_addMoneyUI.SetActive (false);
	}

	/// <summary>
	/// 根据得到的星星增加金币
	/// </summary>
	public void AddMoney(int count){
		_addMoneyUI.SetActive (true);
		_guangAnimator.SetTrigger ("guang");
		//如果得到1颗星星
		if (count == 1) {
			//则显示增加100金币图片
			_addMoneyTextUI.GetComponent<Image> ().sprite = addMoney [0];
			AddTotalMoney._addTotalMoney.totalMoney = 600;
				
		}
		//如果得到2颗星星
		if (count == 2) {
			//则显示增加200金币图片
			_addMoneyTextUI.GetComponent<Image> ().sprite = addMoney [1];
			AddTotalMoney._addTotalMoney.totalMoney = 700;
				
		} else if (count == 3) {
			//如果得到3颗星星，则显示增加300金币图片
			_addMoneyTextUI.GetComponent<Image> ().sprite = addMoney [2];
			AddTotalMoney._addTotalMoney.totalMoney = 800;
				
		}

	}
	
	/*public void GameGuidance(){
		_tipsUI.SetActive (true);
		TipsManager._tipsManager.Tips ();

	}*/
}
	/*
	void CheckPointUI(){
		
			for (int i = 0; i < isLevelPassList.Count; i++) {
				if (isLevelPassList [i] == false || isLevelPassList [i] == null) {
					cardArray [i].sprite = noUnLocked [i];
				} else if (isLevelPassList [i] == true) {
					cardArray [i].sprite = unLocked [i];
				}
			}
	}
*/