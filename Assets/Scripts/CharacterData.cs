using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Character数据类
/// </summary>
[System.Serializable]
public class CharacterData{
	//Character预制
	public GameObject characterPrefab;
	//创建所需的钱
	public int characterCost;
	//Character升级预制
	public GameObject characterUpgradePrefab;
	//创建所需的钱
	public int characterUpgradeCost;
	public Animator animator;
	//类型
	public CharacterType type;
	public enum CharacterType{
		character1,
		character2,
		character3

	}

}
