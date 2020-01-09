using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 每一波食物的属性 
/// </summary>
[System.Serializable]
public class Wave {
	
	//食物预制体
	public GameObject foodPrefab;
	//每一波食物的个数
	public int foodCount;
	//生成的速率
	public float rate;

}
