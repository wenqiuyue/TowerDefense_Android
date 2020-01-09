using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FloorGrass : MonoBehaviour {
	//当前FloorGrass上的人物
	[HideInInspector]
	public GameObject characterGo;
	//[System.Serializable]
	public GameObject buildEffect;
	[HideInInspector]
	public bool isUpgraded=false;
	[HideInInspector]
	public CharacterData characterData;
	//public Animator animator;
	private Renderer renderer;
	public static FloorGrass _floorGrass;
	void Start(){
		_floorGrass = this;
		renderer = GetComponent<Renderer>();

		//objFloor = GameObject.Find ("Floor_Grass");
		//renderer.material.shader = Shader.Find ("Standard");

		//floorTexture =objFloor.renderer.material.;
	}
	/// <summary>
	/// 建造character方法
	/// </summary>
	public void BuildCharacter(CharacterData characterData){
		this.characterData = characterData;
		isUpgraded = false;
		characterGo=GameObject.Instantiate (characterData.characterPrefab,transform.position,Quaternion.identity);
		//创建人物的动画效果
		GameObject gam=GameObject.Instantiate (buildEffect,transform.position,Quaternion.identity);
		SoundPlay._soundPlay.PlaySound ("build");
		//创建人物后 延迟一秒销毁动画
		Destroy (gam,1);
	}

	/// <summary>
	/// 建造Updatecharacter方法
	/// </summary>
	public void BuildUpdateCharacter(){
		if (isUpgraded == true) return;
		Destroy (characterGo);
		isUpgraded = true;
		characterGo=GameObject.Instantiate (characterData.characterUpgradePrefab,transform.position,Quaternion.identity);
		//创建人物的动画效果
		GameObject gam=GameObject.Instantiate (buildEffect,transform.position,Quaternion.identity);
		SoundPlay._soundPlay.PlaySound ("upgrade");
		//创建人物后 延迟一秒销毁动画
		Destroy (gam,1);
		//钱不够提示
		//animator.SetTrigger("upgrade");
	}

	/// <summary>
	/// 拆除人物
	/// </summary>
	public void DestroyCharacter(){
		Destroy (characterGo);
		isUpgraded = false;
		characterGo = null;
		characterData = null;
		GameObject gam=GameObject.Instantiate (buildEffect,transform.position,Quaternion.identity);
		Destroy (gam,1);
		SoundPlay._soundPlay.PlaySound ("build");
	}

	public void SelectEffect(){
		//
		if (characterGo == null && IPointerOverUI._Instance.IsPointerOverUIObjectAWithTag("circular")==false) {
		//Debug.Log("OnMouseEnter");
			//SoundPlay._soundPlay.PlaySound("select");

			renderer.material.color = Color.red;
		}
	}
	public void UnselectEffect(){
		renderer.material.color = Color.white;
		//renderer.material.SetTexture ("Floor_Grass",floorTexture);
	}
	}

