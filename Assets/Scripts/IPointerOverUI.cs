using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class IPointerOverUI {
	#region Field 字段
	List<RaycastResult> _results;

	/// <summary>
	/// 单例 20171128 ChiHai
	/// </summary>
	static IPointerOverUI _instance;
	#endregion

	#region Property 属性
	/// <summary>
	/// 单例 20171128 ChiHai
	/// </summary>
	public static IPointerOverUI _Instance{get{
			if (_instance == null)
				_instance = new IPointerOverUI ();
			return _instance;
		}}
	#endregion

	#region Function 方法

    public IPointerOverUI(){
        _results = new List<RaycastResult>();
    }

    public bool IsPointerOverUIObjectS(Canvas canvas, Vector2 screenPosition) {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = screenPosition;

        GraphicRaycaster uiRaycaster = canvas.gameObject.GetComponent<GraphicRaycaster>();
        _results.Clear();
        uiRaycaster.Raycast(eventDataCurrentPosition, _results);
        return _results.Count > 0;
    }

	public bool IsPointerOverUIObjectAWithTag(string tag) {
		PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
		eventDataCurrentPosition.position = Input.mousePosition;

		_results.Clear();
		if (EventSystem.current != null)
			EventSystem.current.RaycastAll(eventDataCurrentPosition, _results);

		/*if (_results.Count > 0) {
			System.Text.StringBuilder bld = new System.Text.StringBuilder ();
			for(int i = 0; i < _results.Count; i++){
				if (i != 0)
					bld.Append (" ");
				bld.Append (_results[i].gameObject.transform.GetScenePathStr());
			}
			Debug.Log (bld.ToString());
		}*/

		int count = 0;
		for (short i = 0; i < _results.Count; i++) {
			//Debug.LogFormat ("{0} {1}", _results[i].gameObject.name, _results[i].gameObject.tag);
			if (_results[i].gameObject.tag.Equals(tag))
				count++;
		}
		//Debug.Log (count);
		return count > 0;
	}

    public bool IsPointerOverUIObjectA() {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
		eventDataCurrentPosition.position = Input.mousePosition;

        _results.Clear();
		if (EventSystem.current != null)
        	EventSystem.current.RaycastAll(eventDataCurrentPosition, _results);

        /*if (_results.Count > 0) {
			System.Text.StringBuilder bld = new System.Text.StringBuilder ();
			for(int i = 0; i < _results.Count; i++){
				if (i != 0)
					bld.Append (" ");
				bld.Append (_results[i].gameObject.transform.GetScenePathStr());
			}
			Debug.Log (bld.ToString());
		}*/

        int count = 0;
        for (short i = 0; i < _results.Count; i++) {
			if (_results[i].gameObject.layer != LayerMask.GetMask("Empty"))
                count++;
        }

        return count > 0;
    }
	#endregion
}