using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoints : MonoBehaviour {

	//路点数组
	public  Transform[] positions;
	//奇数路点列表
	public  List<Transform> oddPositionsList=new List<Transform>();
	//偶数路点列表
	public  List<Transform> evenPositionsList=new List<Transform>();
	public static WayPoints _wayPoints;
	void Start(){
		_wayPoints = this;
	}
	/// <summary>
	/// 将每个路点放进路点数组
	/// </summary> 
	void Awake(){
		positions=new Transform[transform.childCount];
		for (int i = 0; i < positions.Length; i++) {
			positions [i] = transform.GetChild (i);
			//Debug.Log (positions.Length);
		
		}
		//Debug.Log (positions.Length);
		//将第0个路点放入奇数列表
		oddPositionsList.Add(positions[0]);
		//遍历数组中的路点，如果是奇数路点，则将奇数路点放入奇数路点列表，否则加入偶数路点列表
		for(int positionsIndex=0;positionsIndex<positions.Length-4;positionsIndex++){
			if (positionsIndex % 2 != 0) {
				oddPositionsList.Add (positions [positionsIndex]);
			} else {
				evenPositionsList.Add (positions[positionsIndex]);
			
			}
		}
		//第14、16、17路点放入奇数路点列表和偶数路点列表,第15路点放入奇数路点列表
		oddPositionsList.Add(positions[14]);
		oddPositionsList.Add(positions[15]);
		oddPositionsList.Add(positions[16]);
		oddPositionsList.Add(positions[17]);
		evenPositionsList.Add (positions[15]);
		evenPositionsList.Add (positions[16]);
		evenPositionsList.Add (positions[17]);
		Debug.Log (oddPositionsList.Count);
		for (int a = 0; a < oddPositionsList.Count; a++) {
			Debug.Log (oddPositionsList[a]);
		}
	}


}
