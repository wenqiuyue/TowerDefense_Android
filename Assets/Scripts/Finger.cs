using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finger : MonoBehaviour {
	//x轴边界最大值
	float xMax = 50;
	//x轴边界最小值
	float xMin = -60;
	//z轴边界最大值
	float zMax = 130;
	//z轴边界最小值
	float zMin = 38;
	//y轴边界最大值
	float yMax = 70;
	//y轴边界最小值
	float yMin = 20;
	//按键移动速度
	float speed = 13;
	//标记摄像机的移动方向
	//private int isforward;
	//记录两个手指的旧位置
	//private Vector2 oposition1=new Vector2();
	//private Vector2 oposition2=new Vector2();
	//记录手指碰触的位置
	Vector2 m_screePos=new Vector2();
	//是否缩放
	private bool IsZoom = false;
	//当前双指触控间距
	private float DoubleTouchCurrDis;
	//过去双指触控间距
	private float DoubleTouchLasdis;
	Touch touch1;
	Touch touch2;
	Touch oldTouch1;
	Touch oldTouch2;

	/// <summary>
	/// 用于判断手指是否进行画面放大操作
	/// </summary>
	/*bool isEnlarge(Vector2 op1,Vector2 op2,Vector2 np1,Vector2 np2){
		//将函数传入上一次触摸两点的位置与本次触摸两点的位置计算出玩家的手势
		float leng1=Mathf.Sqrt((op1.x-op2.x)*(op1.x-op2.x)+(op1.y-op2.y)*(op1.y-op2.y));
		float leng2=Mathf.Sqrt((np1.x-np2.x)*(np1.x-np2.x)+(np1.y-np2.y)*(np1.y-np2.y));
		if (leng1 < leng2) {
			//放大手势
			return true;
		
		} else {
			//缩小手势
			return false;
		}
	
	}*/

	void Start () {
		//开启多点碰触
		Input.multiTouchEnabled = true;
	}
	void Update () {
		if (Input.touchCount <= 0)
			return;
		//单点碰触移动摄像机
		if (Input.touchCount == 1) {
			if (IPointerOverUI._Instance.IsPointerOverUIObjectAWithTag ("finger") == true) {
				if (Input.touches [0].phase == TouchPhase.Began) {
					//记录手指刚碰触的位置
					m_screePos = Input.touches [0].position;
				}
				//如果手指在屏幕上移动，移动摄像机
				if (Input.touches [0].phase == TouchPhase.Moved) { 
					transform.Translate (new Vector3 (-Input.touches [0].deltaPosition.x * Time.deltaTime * speed,
						0, -Input.touches [0].deltaPosition.y * Time.deltaTime * speed), Space.World);
					//如果x轴小于最小值，则x等于最小值
					if (transform.position.x < xMin) {
						transform.position = new Vector3 (xMin, transform.position.y, transform.position.z);
					}
					//如果x轴大于最大值，则x等于最大值
					if (transform.position.x > xMax) {
						transform.position = new Vector3 (xMax, transform.position.y, transform.position.z);
					}
					//如果z轴小于最小值，则z等于最小值
					if (transform.position.z < zMin) {
						transform.position = new Vector3 (transform.position.x, transform.position.y, zMin);
					}
					//如果z轴大于最大值，则z等于最大值
					if (transform.position.z > zMax) {
						transform.position = new Vector3 (transform.position.x, transform.position.y, zMax);
					}
				}
			}
		} else if ((Input.touchCount > 1) )//&& (Input.GetTouch (0).phase == TouchPhase.Moved || Input.GetTouch (1).phase == TouchPhase.Moved)) { 

			touch1 = Input.GetTouch (0);
			touch2 = Input.GetTouch (1);
			
		if (Input.GetTouch(1).phase==TouchPhase.Began) {

			oldTouch1 = touch1;
			oldTouch2 = touch2;
			return;
			}
			//获取当前手指间距
			DoubleTouchCurrDis = Vector2.Distance (touch1.position, touch2.position);
			//获取前手指间距
			DoubleTouchLasdis = Vector2.Distance (oldTouch1.position, oldTouch2.position);
			/*if (!IsZoom) {
				DoubleTouchLasdis = DoubleTouchCurrDis;
				IsZoom = true;
			}*/
			float distance = DoubleTouchCurrDis - DoubleTouchLasdis;
			transform.Translate (new Vector3 (0, -distance * Time.deltaTime * speed, 0), Space.World);
			//如果y轴小于最小值，则y等于最小值
			if (transform.position.y < yMin) {
				transform.position = new Vector3 (transform.position.x, yMin, transform.position.z);
			}
			//如果y轴大于最大值，则y等于最大值
			if (transform.position.y > yMax) {
				transform.position = new Vector3 (transform.position.x, yMax, transform.position.z);
			}
			oldTouch1 = touch1;
			oldTouch2 = touch2;
			/*
			//记录两个手指的位置
			Vector2 nposition1=new Vector2();
			Vector2 nposition2=new Vector2();

			//记录手指的每帧移动的距离
			Vector2 deltaDis1=new Vector2();
			Vector2 deltaDis2=new Vector2();

			for(int i=0;i<2;i++){
				Touch touch = Input.touches [i];
				if (touch.phase == TouchPhase.Ended)
					break;
				//如果手指在移动
				if(touch.phase == TouchPhase.Moved){
					if (i == 0) {
						nposition1 = touch.position;
						deltaDis1 = touch.deltaPosition;
					} else {
						nposition2 = touch.position;
						deltaDis2 = touch.deltaPosition;
						//判断手势伸缩从而计算摄像机前后移动参数缩放效果
						if(isEnlarge(oposition1,oposition2,nposition1,nposition2))
							isforward = 1;
							else
							isforward=-1;
						}
					//记录旧的触摸位置
					oposition1=nposition1;
					oposition2 = nposition2;

					}
				//移动摄像机
				Camera.main.transform.Translate(isforward*Vector3.forward*Time.deltaTime*speed*(Mathf.Abs(deltaDis2.x+deltaDis1.x)+Mathf.Abs(deltaDis1.y+deltaDis2.y)));
				}

			}
		*/
			return;
		}
	}

