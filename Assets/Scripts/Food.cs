using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Food : MonoBehaviour {
	//奇数路线列表
	private List<Transform> oddPositions = new List<Transform> ();
	//偶数路线列表
	private List<Transform> evenPositions = new List<Transform> ();
	//定义食物行走路线类型1为奇数路线，2为偶数路线
	public int type=1;
	//路点数组下标
	private int index=0;
	//食物移动速度
	public float speed=20;
	// 初始速度
	float initSpd = 20;
	// 是否已经减速
	bool isMinusSpd = false;
	//初始血量
	public float hp=100;
	//总血量
	private float totalHp;
	private Slider hpSlider;
	//爆炸效果
	public GameObject foodExplosionEffectPre;
	//private FoodIncubator foodIncubator;

	private Transform[] positions;
	void Start () {
		initSpd = speed;
		//如果场景为类型1
		if (GameManager.Instance._scenes == 1) {
			positions = OneWayPoints.positions;
		} else if (GameManager.Instance._scenes == 2) {
			oddPositions = WayPoints._wayPoints.oddPositionsList;
			evenPositions = WayPoints._wayPoints.evenPositionsList;
		}
		totalHp = hp;
		hpSlider = GetComponentInChildren<Slider> ();
		//foodIncubator = gameObject.GetComponent<FoodIncubator> ();
		//获取当前场景编号
		AddTotalMoney._addTotalMoney.index=SceneManager.GetActiveScene ().buildIndex;
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.Instance._isPause)
			return;

		Move ();
	}
	/// <summary>
	/// 移动方法
	/// </summary>
	void Move(){
		//if (transform == null)
		//	return;
		if (GameManager.Instance._scenes == 1) {
			//if (index > positions.Length - 1)
				//return;
			
				transform.Translate ((positions [index].position - transform.position).normalized * Time.deltaTime * speed, Space.World);

				
			if (Vector3.Distance (positions [index].position, transform.position) < 0.2f) {
				index++;
			}
			//食物到达终点
			if (index > positions.Length - 1) {
				EndFood ();
			}
		} else if (GameManager.Instance._scenes == 2) {
			//如果食物到奇数路径达终点，则退出
			//if (index > oddPositions.Count - 1)
				//return;
			
			//Debug.Log ( index);
			//Debug.Log (oddPositions [index]);

			if (type == 1) {
				transform.Translate ((oddPositions [index].position - transform.position).normalized * Time.deltaTime * speed, Space.World);
				//如果目标位置与自身位置距离小于0.2f（到达目标位置），index+1
				if (Vector3.Distance (oddPositions [index].position, transform.position) < 0.2f) {
					index++;
				}
				//食物到达终点
				if (index > oddPositions.Count - 1) {
					EndFood ();
				}
			
			}
			//如果食物到达偶数路径终点，则退出
			//if (index > evenPositions.Count - 1)
				//return;
			if (type == 2) {
				transform.Translate ((evenPositions [index].position - transform.position).normalized * Time.deltaTime * speed, Space.World);
				if (Vector3.Distance (evenPositions [index].position, transform.position) < 0.2f) {
					index++;
				}
				//食物到达终点
				if (index > evenPositions.Count - 1) {
					EndFood ();
				}

			}

		}
	}
	/// <summary>
	/// 食物到达终点，销毁自身
	/// </summary>
	void FoodDestroy(){
		
		GameManager.Instance.Failed();
		for (int i = 0; i < FoodIncubator._foodListPre.Count; i++) {
			GameObject.Destroy (FoodIncubator._foodListPre[i]);
		}
		FoodIncubator.FoodCountAlive = 0;

	}
	/// <summary>
	/// 敌人个数减1
	/// </summary>
	/*void OnDestroy(){
		FoodIncubator.FoodCountAlive--;
		FoodIncubator._foodIncubator.UpdateFoodCount (FoodIncubator.FoodCountAlive);
		Debug.Log("减少"+FoodIncubator.FoodCountAlive);
	}*/
	/// <summary>
	/// 食物被子弹击中后减血方法
	/// </summary>
	public void TakeDamage(float _damage){
		//如果hp小于0，则直接退出
		if (hp < 0)
			return;
		hp -= _damage;
		hpSlider.value =(float) hp / totalHp;
		if(hp < 0){
			Die ();

		}

	}

	/// <summary>
	/// 到达终点的食物
	/// </summary>
	void EndFood(){
		Destroy (this.gameObject);
		//存在的食物个数减1 
		FoodIncubator.FoodCountAlive--;
		FoodIncubator._foodIncubator.UpdateFoodCount (FoodIncubator.FoodCountAlive);
		//到达终点的食物个数加1
		FoodIncubator._foodIncubator.destroyCount++;
		//Debug.Log (FoodIncubator._foodIncubator.destroyCount);
		//如果到达终点的食物个数大于等于3
		if (FoodIncubator._foodIncubator.destroyCount >= 3) {
			FoodDestroy ();
		}
		/*
		//第一关时到达终点的食物个数
		if(AddTotalMoney._addTotalMoney.index==2){
			CheckPointManager._checkPointManager.starCountList [0] = FoodIncubator._foodIncubator.destroyCount;

		}
		//第二关时到达终点的食物个数
		if(AddTotalMoney._addTotalMoney.index==3){
			CheckPointManager._checkPointManager.starCountList [1] = FoodIncubator._foodIncubator.destroyCount;
		}
		Debug.LogFormat ("{0}{1}",AddTotalMoney._addTotalMoney.index,FoodIncubator._foodIncubator.destroyCount);
		*/
		}
	/// <summary>
	/// 当食物hp值减为0时，死亡方法
	/// </summary>
	void Die(){
		GameObject effect= GameObject.Instantiate (foodExplosionEffectPre,transform.position,transform.rotation);
		Destroy (effect,1.5f);
		Destroy (this.gameObject);
		FoodIncubator.FoodCountAlive--;
		FoodIncubator._foodIncubator.UpdateFoodCount (FoodIncubator.FoodCountAlive);
		//OnDestroy ();
		FoodIncubator._foodListPre.Remove (this.gameObject);

	}
	/// <summary>
	/// 减速
	/// </summary>
	public void DecelerationSpeed(float _decelerationSpeed){
		if (isMinusSpd)
			return;

		speed = initSpd * _decelerationSpeed;
		isMinusSpd = true;
		return;

	}

	public void SpdInit(){
		
		speed = initSpd;
		isMinusSpd = false;
	}
}

