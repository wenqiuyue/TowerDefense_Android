using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	//子弹伤害值
	public int damage=50;
	//子弹飞行速度
	public float bulletSpeed=20;
	//攻击目标位置
	private Transform target;
	//子弹爆炸效果
	public GameObject explosionEffectPrefab;
	//目标位置到子弹位置的距离
	private float distance=0.2f;

	/// <summary>
	/// 获取目标位置方法
	/// </summary>
	/// <param name="_target">Target.</param>
	public void setTarget(Transform _target){
		this.target = _target;
	}

	// Update is called once per frame
	void Update () {
		
		//如果目标位置不存在
		if(target==null){
			//执行销毁自身方法，并退出
			Die ();
			return;
		}
		//子弹面朝目标位置方向
		transform.LookAt (target.position);
		//子弹移动
		transform.Translate (Vector3.forward*bulletSpeed*Time.deltaTime);
		//实际目标到子弹的距离
		Vector3 distanceVec = transform.position - target.position;
		//如果实际目标到子弹的距离小于定义的距离
		if(distanceVec.magnitude<distance){
			//播放目标击中音效
			SoundPlay._soundPlay.PlaySound ("hit");
			//食物减血
			target.GetComponent<Food>().TakeDamage(damage);
			Die ();
		}


	}

	void Die(){
		//播放爆炸效果
		GameObject bulletEffect=GameObject.Instantiate(explosionEffectPrefab,transform.position,transform.rotation);
		//销毁子弹爆炸效果
		Destroy(bulletEffect,0.5f);
		//销毁子弹自身
		Destroy(this.gameObject);

	}

	/*
	/// <summary>
	/// 检测子弹是否和食物发生碰撞
	/// </summary>
	void  OnTriggerEnter(Collider col){
		if (col.tag == "food") {
			//食物减血
			col.GetComponent<Food>().TakeDamage(damage);
			//播放爆炸效果
			GameObject.Instantiate(explosionEffectPrefab,transform.position,transform.rotation);
			//销毁子弹自身
			Destroy(this.gameObject);
		}
	}
	*/
}
