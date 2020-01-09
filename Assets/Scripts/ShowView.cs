using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowView : MonoBehaviour {
	//按键移动速度
	public float speed = 20;
	//鼠标滑轮速度
	public float mouseSpeed=60;
	//x轴边界最大值
	float xMax = 50;
	//x轴边界最小值
	float xMin = -60;
	//z轴边界最大值
	float zMax = 150;
	//z轴边界最小值
	float zMin = 7;
	//y轴边界最大值
	float yMax = 95;
	//y轴边界最小值
	float yMin = 15;
	/// <summary>
	/// 键盘和鼠标调节视野
	/// </summary>
	void Update () {
		//调节水平按键AD
		float h = Input.GetAxis ("Horizontal");
		//调节垂直按键WS
		float v = Input.GetAxis("Vertical");
		//获取鼠标滑轮
		float mouse=Input.GetAxis("Mouse ScrollWheel");
		//摄像机移动
		transform.Translate(new Vector3(-h*speed,mouse*mouseSpeed,-v*speed)*Time.deltaTime,Space.World);
		//如果相机x轴大于最大边界，则x等于最大边界
		if (transform.position.x > xMax) {
			transform.position = new Vector3(xMax,transform.position.y,transform.position.z);
		}
		//如果相机x轴小于最小边界，则x等于最小边界
		if (transform.position.x < xMin) {
			transform.position = new Vector3(xMin,transform.position.y,transform.position.z);
		}
		//如果相机z轴大于最大边界，则z等于最大边界
		if (transform.position.z > zMax) {
			transform.position = new Vector3(transform.position.x,transform.position.y,zMax);
		}
		//如果相机z轴小于最小边界，则z等于最小边界
		if (transform.position.z < zMin) {
			transform.position = new Vector3(transform.position.x,transform.position.y,zMin);
		}
		//如果相机z轴大于最大边界，则z等于最大边界
		if (transform.position.y > yMax) {
			transform.position = new Vector3(transform.position.x,yMax,transform.position.z);
		}
		//如果相机y轴小于最小边界，则y等于最小边界
		if (transform.position.y < yMin) {
			transform.position = new Vector3(transform.position.x,yMin,transform.position.z);
		}
			
	}
}
