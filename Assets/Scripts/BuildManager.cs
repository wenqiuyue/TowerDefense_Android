using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class BuildManager : MonoBehaviour {
	public CharacterData character1Data;
	public CharacterData character2Data;
	public CharacterData character3Data;
	//所拥有的钱
	//public int totalMoney=500;
	//当前所选的人物（要建造的人物）
	private CharacterData selectCharacterType;
	//当前所选择的人物（场景中的游戏物体）
	FloorGrass selectFloorGrass;
	[SerializeField]
	Text moneyText;
	//钱不够提示动画
	public Animator animator;
	//减速范围闪烁动画
	public Animator circularAnimator;
	private GameObject character;
	FloorGrass selectFloor;
	FloorGrass select;
	//升级UI
	public GameObject upgradeUI;
	//升级按钮
	public Button upgradeButton;
	bool isShow=false;
	//人物是否升级
	//bool isUpgrade=false;
	public static BuildManager _buildManager;
	void Start(){
		_buildManager = this;
		moneyText.text = "" + AddTotalMoney._addTotalMoney.totalMoney;
	}
	/// <summary>
	/// 钱的改变
	/// </summary>
	public void UpdateMoney(int change=0){
		
		AddTotalMoney._addTotalMoney.totalMoney += change;
		moneyText.text = "" + AddTotalMoney._addTotalMoney.totalMoney;
	
	}
	void Update(){
		//return;
		//if(selectFloor != null) selectFloor.UnselectEffect ();

		//如果点击了鼠标

			//鼠标没有在UI上
		if(/*EventSystem.current.IsPointerOverGameObject()==false*/IPointerOverUI._Instance.IsPointerOverUIObjectAWithTag("circular")==false&&IPointerOverUI._Instance.IsPointerOverUIObjectAWithTag("upgrade")==false&&IPointerOverUI._Instance.IsPointerOverUIObjectAWithTag("UI")==false&&IPointerOverUI._Instance.IsPointerOverUIObjectAWithTag("finger")==false){
				//用射线检测进行人物建造
				Ray ray=Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;
				bool isCollider=Physics.Raycast (ray,out hit,Mathf.Infinity,LayerMask.GetMask("Floor_Grass"));

				if (isCollider) {
				//Debug.Log (hit.collider.gameObject.name);
					//检测当前选择的FloorGrass
				selectFloor = hit.collider.GetComponent<FloorGrass>();
				if (select != selectFloor) {
					SoundPlay._soundPlay.PlaySound ("select");
				}
				if(isShow==false){

					//selectFloor.SelectEffect ();
				}
				if(Input.GetMouseButtonDown(0)){
					
					//如果FloorGrass没有构建人物
					if (selectCharacterType!=null && selectFloor.characterGo == null) {
						if (isShow == false) {
							//如果钱足够，则进行建造人物
							if (AddTotalMoney._addTotalMoney.totalMoney >= selectCharacterType.characterCost) {
								//钱的改变
								UpdateMoney (-selectCharacterType.characterCost);

								//建人物
								selectFloor.BuildCharacter (selectCharacterType);
								selectFloor.isUpgraded = false;
								if (selectCharacterType == character3Data) {
									circularAnimator.Play ("circular");
									HideUpgradeUI ();
								}
								/*if(Character._character.vertigo==true){
								circularAnimator.SetTrigger("circular");
								
							}*/
								//isUpgrade = false;
							} else {
								//钱不够提示音效
								SoundPlay._soundPlay.PlaySound ("money");
								//钱不够提示
								animator.SetTrigger ("twinkle");
						
							}
						}
					}else if(selectFloor.characterGo != null){
						selectFloorGrass = selectFloor;
						//如果点击的人物和选择的人物相同，并且升级UI已显示
						if (selectFloor == selectFloorGrass && upgradeUI.activeInHierarchy) {
							//升级处理
							//OnUpgradeCharacter();
							//ShowUpgradeUI(selectFloor.transform.position,isUpgrade);
							//isUpgrade = true;
							//隐藏
							HideUpgradeUI ();
						} else{
							Debug.LogFormat ("{0}", selectFloor.characterData.type);
							if (selectFloor == selectFloorGrass&&selectFloor.characterData.type != CharacterData.CharacterType.character3) {
								ShowUpgradeUI (selectFloor.transform.position, selectFloor.isUpgraded);
								if (selectFloorGrass.isUpgraded == false) {
									upgradeButton.GetComponent<Image> ().color = Color.white;
								}
								if (selectFloorGrass.isUpgraded == true) {
									upgradeButton.GetComponent<Image> ().color = Color.gray;
								}
							} else {
								HideUpgradeUI ();
							}
						}
						selectFloorGrass = selectFloor;

					}

					//Debug.Log ("1");
				}

			} 

		}
		select = selectFloor;
	
	}
	public void Oncharacter1Select(bool isOn){
		if (isOn) {
			selectCharacterType = character1Data;
		}
	}
	public void Oncharacter2Select(bool isOn){
		if (isOn) {
			selectCharacterType = character2Data;
		}
	}
	public void Oncharacter3Select(bool isOn){
		if (isOn) {
			selectCharacterType = character3Data;

		}
	}

	/// <summary>
	/// 升级UI显示
	/// </summary>
	public void ShowUpgradeUI(Vector3 pos, bool isDisabled=false){
		
			upgradeUI.SetActive (true);
			upgradeUI.transform.position = pos;
			upgradeButton.interactable = !isDisabled;
			isShow = true;
		return;
	}

	/// <summary>
	/// 升级UI隐藏
	/// </summary>
	public void HideUpgradeUI(){
		upgradeUI.SetActive (false);
		isShow = false;
	}

	/// <summary>
	/// 升级处理
	/// </summary>
	public void OnUpgradeCharacter(){
		
		SoundPlay._soundPlay.PlaySound ("button");
		//isUpgrade = false;
		//升级处理,如果钱大于升级人物所需要的钱 
		if (AddTotalMoney._addTotalMoney.totalMoney >= selectFloorGrass.characterData.characterUpgradeCost) {
			//钱的改变
			UpdateMoney (-selectFloorGrass.characterData.characterUpgradeCost);
			//建人物
			selectFloorGrass.BuildUpdateCharacter ();
			//selectFloor.isUpgraded = true;
			//isUpgrade = true;
			selectFloorGrass.characterData.animator.SetTrigger ("upgrade");
		} else {
			//钱不够提示音效
			SoundPlay._soundPlay.PlaySound ("money");
			//钱不够提示
			animator.SetTrigger ("twinkle");

		}
		HideUpgradeUI ();

	}

			
	/// <summary>
	/// 拆除人物
	/// </summary>
	public void DestroyButton(){
		SoundPlay._soundPlay.PlaySound ("button");
		Debug.Log (selectFloorGrass.isUpgraded);
		//如果升级后拆除人物，则总的钱加上建造的钱+升级的钱，否则只加上建造的钱
		if (selectFloorGrass.isUpgraded==true) {
			UpdateMoney ((selectFloorGrass.characterData.characterUpgradeCost+selectFloorGrass.characterData.characterCost)/2);
			Debug.LogFormat ("{0} {1}","isUpgraded == true",(selectFloorGrass.characterData.characterUpgradeCost+selectFloorGrass.characterData.characterCost)/2);
		} else if(selectFloorGrass.isUpgraded==false){
			UpdateMoney (selectFloorGrass.characterData.characterCost/2);
			Debug.LogFormat ("{0} {1}","isUpgraded == false",selectFloorGrass.characterData.characterCost/2);
		}
		selectFloorGrass.DestroyCharacter ();
		HideUpgradeUI ();
	
	}

}
