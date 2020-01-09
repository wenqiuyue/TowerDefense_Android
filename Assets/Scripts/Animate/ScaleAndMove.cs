using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ScaleAndMove : MonoBehaviour {
	/// <summary>
	/// 缩放时间
	/// </summary>
	const float _SCALE_TIME = 0.5f;

	/// <summary>
	/// 旋转时间
	/// </summary>
	const float _ROTATE_TIME = 0.3f;
	/// <summary>
	/// 移动距离
	/// </summary>
	const float _MOVE_DIS = 60;

	/// <summary>
	/// 动画图片数组
	/// </summary>
	[SerializeField]
	Image[] _imageArray;

	// Use this for initialization
	void Start () {
		// 所有图片缩放置为0
		for(short i = 0; i < _imageArray.Length; i++){
			_imageArray [i].transform.localScale = Vector3.zero;
		}

		// 从中间开始，图片依次变大
		Sequence seq = DOTween.Sequence ();
		Image image1 = null, image2 = null;

		float time = 0;
		for(short i = 0; i < 3; i++){
			switch (i) {
			case 0:
				image1 = _imageArray [0];
				break;
			case 1:
				image1 = _imageArray [1];
				image2 = _imageArray [2];
				break;
			case 2:
				image1 = _imageArray [3];
				image2 = _imageArray [4];
				break;
			}

			seq.Append (image1.transform.DOScale(1, _SCALE_TIME)).SetEase(Ease.Linear);
			seq.Append (image1.transform.DOScale(0, _SCALE_TIME)).SetEase(Ease.Linear);
			seq.Append (image1.transform.DOScale(1, _SCALE_TIME)).SetEase(Ease.Linear);

			if (image2 == null)
				continue;

			time += _SCALE_TIME * 3;
			seq.Insert (time, image2.transform.DOScale(1, _SCALE_TIME)).SetEase(Ease.Linear);
			seq.Insert (time + _SCALE_TIME, image2.transform.DOScale(0, _SCALE_TIME)).SetEase(Ease.Linear);
			seq.Insert (time + _SCALE_TIME + _SCALE_TIME, image2.transform.DOScale(1, _SCALE_TIME)).SetEase(Ease.Linear);
		}

		seq.OnComplete (Rotate);
	}

	/// <summary>
	/// 旋转与移动动画
	/// </summary>
	void Rotate(){
		_imageArray [0].transform.DORotate (new Vector3 (0, -359, 0), _ROTATE_TIME * 2, RotateMode.LocalAxisAdd).SetEase(Ease.Linear).SetLoops(-1);
		_imageArray [1].transform.DOLocalMoveX (_MOVE_DIS, _ROTATE_TIME).SetRelative().SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
		_imageArray [3].transform.DOLocalMoveX (_MOVE_DIS, _ROTATE_TIME).SetRelative().SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);

		_imageArray [2].transform.DOLocalMoveX (-_MOVE_DIS, _ROTATE_TIME).SetRelative().SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
		_imageArray [4].transform.DOLocalMoveX (-_MOVE_DIS, _ROTATE_TIME).SetRelative().SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
	}
}
