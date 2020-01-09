using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {
	[HideInInspector]
	//进入触发范围的食物
	public List<GameObject> foodList = new List<GameObject> ();
	[HideInInspector]
	//离开触发范围的食物
	public List<GameObject> recovereyFoodList = new List<GameObject> ();
	/// <summary>
	/// food进入触发范围
	/// </summary>
	/// <param name="col">Col.</param>
	void OnTriggerEnter(Collider col){
		if (col.tag == "food") {
			//将进入触发器范围的food加入列表
			foodList.Add (col.gameObject);
		/*	Food food = col.GetComponent<Food> ();
			if(food != null)
				food.SpdInit();*/
		}
		//Debug.Log ("trigger");
	}
	/// <summary>
	/// food离开触发范围
	/// </summary>
	/// <param name="col">Col.</param>
	void OnTriggerExit(Collider col){
		if (col.tag == "food") {
			//将离开触发器范围的food移除列表
			foodList.Remove (col.gameObject);
			recovereyFoodList.Add (col.gameObject);

			Food food = col.GetComponent<Food> ();
			if(food != null && vertigo)
				food.SpdInit();
		}
	}
	//每几秒攻击一次
	public float attackRate=1;
	//时间
	private float timer=0;
	//子弹物体
	public GameObject bulletPre;
	//子弹初始位置
	public Transform bulletPosition;
	public Transform characterPosition;
	public bool userLaser = false;
	//是否减速
	public bool vertigo=false;
	//减速
	float _decelerationSpeed=0.2f;
	public LineRenderer laserRenderer;
	public GameObject laserEffect;
	//激光伤害值
	float laserDamage=40;
	public static Character _character;
	void Start(){
		_character = this;
		timer = attackRate;
	}
	void Update(){
		if (vertigo == true) {
			
			UpdateList (foodList);
			if (foodList.Count > 0) {
				//UpdateList (foodList);
					//遍历进入攻击范围的食物，将速度减慢
					for(int i=0;i<foodList.Count;i++){
						foodList [i].GetComponent<Food> ().DecelerationSpeed (_decelerationSpeed);
					}
			}

			UpdateList (recovereyFoodList);
			//如果离开触发范围的食物的列表元素大于0
			/*if(recovereyFoodList.Count>0){

				//将该列表中的食物速度恢复
				for(int i=0;i<recovereyFoodList.Count;i++){
					recovereyFoodList [i].GetComponent<Food> ().SpdInit ();
				}
			}*/

		} else if (vertigo == false) {
			//如果列表有元素，并且第一个元素不为空
			if (foodList.Count > 0 && foodList [0] != null) {
				//将人物方向转要攻击的向目标方向
				Vector3 targetPosition = foodList [0].transform.position;
				targetPosition.y = characterPosition.position.y;
				characterPosition.LookAt (targetPosition);
			}
			if (userLaser == false) {
				timer += Time.deltaTime;
				//如果列表里有食物 并且时间到达攻击时间
				if (foodList.Count > 0 && timer >= attackRate && vertigo == false) {
					timer = 0;
					Attack ();

				}
			} else if (foodList.Count > 0) {
				//使用激光
				if (laserRenderer.enabled == false)
					laserRenderer.enabled = true;
				laserEffect.SetActive (true);
				//如果食物列表第一个元素为空，则更新列表
				if (foodList [0] == null) {
					UpdateList (foodList);
				}
				//如果食物列表有元素，则使用激光
				if (foodList.Count > 0) {
					laserRenderer.SetPositions (new Vector3[]{ bulletPosition.position, foodList [0].transform.position });
					//SoundPlay._soundPlay.PlaySound ("laser");
					//激光伤害
					foodList [0].GetComponent<Food> ().TakeDamage (laserDamage * Time.deltaTime);
					//激光特效显示位置
					laserEffect.transform.position = foodList [0].transform.position;
					//特效朝向
					Vector3 pos = transform.position;
					pos.y = foodList [0].transform.position.y;
					laserEffect.transform.LookAt (pos);
			
				}
			} else {
				laserEffect.SetActive (false);
				laserRenderer.enabled = false;
			}
			/*if (vertigo == true && foodList.Count > 0) {
			//如果食物列表第一个元素为空，则更新列表
			if (foodList [0] == null) {
				UpdateList ();
			}
			//如果食物列表里有元素
			if (foodList.Count > 0) {
				foodList [0].GetComponent<Food> ().speed = foodList [0].GetComponent<Food> ().speed * _decelerationSpeed;
			}
		}
		*/
		}
	}
	/// <summary>
	/// 攻击方法
	/// </summary>
	void Attack (){
		//如果食物列表里的第一个元素为空，则更新列表
		if(foodList[0]==null){
			UpdateList (foodList);
		}

		//如果列表元素个数大于0，则进行创建子弹
		if (foodList.Count > 0) {
			GameObject bullet = GameObject.Instantiate (bulletPre, bulletPosition.position, bulletPosition.rotation);
			bullet.GetComponent<Bullet> ().setTarget (foodList [0].transform);
		} else {
			
			//否则将时间设置为攻击时间，等待食物进入攻击范围
			timer = attackRate;
		
		}


	}
	/// <summary>
	/// 更新列表
	/// </summary>
	void UpdateList(List<GameObject> list){
		//空元素下标列表
		List<int> emptyIndexList = new List<int> ();
		//遍历整个食物列表，找出空元素，并将空元素的下标加入空元素下标列表
		for (int i = 0; i < list.Count; i++) {
			if (list [i] == null) {
				emptyIndexList.Add (i);
			}
		}
		for (int k = 0; k < emptyIndexList.Count; k++) {
			list.RemoveAt (emptyIndexList[k]-k);
		}
	}
}
	

