using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundPlay : MonoBehaviour {
	//音乐
	public enum MusicType:byte{
		None,
		start,		  //开始界面
		game,		  //游戏界面
		gameover,  //游戏失败
		win,       //游戏胜利 
		kaluli		//卡路里歌
	}
	//音效
	public enum SoundEffect:byte{
		None,
		button,		//按钮
		build,		//建造人物
		upgrade,	//升级
		select,		//鼠标选中
		hit,		//目标击中
		money,		//钱不够提示
		star		//星星闪烁音效
	}
	//背景音乐播放器
	AudioSource _audioSource;
	//音效播放器
	AudioSource[] _soundEffect;
	//开始界面背景音乐
	[SerializeField]
	public AudioClip _startBgm;
	//游戏界面背景音乐
	[SerializeField]
	public AudioClip _gameBgm;
	//游戏失败背景音乐
	[SerializeField]
	public AudioClip _gameFailureBgm;
	//游戏胜利背景音乐
	[SerializeField]
	public AudioClip _gameWinBgm;
	//卡路里背景音乐
	[SerializeField]
	public AudioClip _kaluliBgm;
	//游戏按键音效
	[SerializeField]
	public AudioClip _buttonEffect;
	//建造音效
	[SerializeField]
	public AudioClip _buildEffect;
	//升级音效
	[SerializeField]
	public AudioClip _upgradeEffect;
	//鼠标选中音效
	[SerializeField]
	public AudioClip _selectEffect;
	//击中目标音效
	[SerializeField]
	public AudioClip _hitEffect;
	//钱不够提示音效
	[SerializeField]
	public AudioClip _moneyEffect;
	//星星闪烁音效
	[SerializeField]
	public AudioClip _starEffect;
	//当前播放的音乐
	[SerializeField]
	MusicType _currentMusic;
	public static SoundPlay _soundPlay;
	void Awake(){
		//跳转场景，不销毁物体自身
		DontDestroyOnLoad(this.gameObject);
		SceneManager.LoadScene (1);
	}
	void Start(){
		_audioSource = this.gameObject.GetComponent<AudioSource> ();
		_soundEffect = this.gameObject.GetComponents<AudioSource> ();
		//音量大小
		_audioSource.volume = 0.5f;

		//循环播放
		_audioSource.loop=true;

		//脚本单例
		_soundPlay=this;
		/*
		//跳转场景，不销毁物体自身
		DontDestroyOnLoad(this.gameObject);
		_soundPlay = this;
		//在摄像机中添加AudioSource组件
		_audioSource=this.gameObject.AddComponent<AudioSource>();
		_audioSource.playOnAwake = true;
		//循环播放
		_audioSource.loop=true;
		//设置音量为最大，区间在0-1之间
		_audioSource.volume=1.0f;
		//设置audioclip
		//PlayBgmAudio(0);
		*/
		PlayMusic ("start");

	}

	/// <summary>
	/// 停止音乐播放
	/// </summary>
	public void StopMusic(){
		_audioSource.Stop ();
		_currentMusic = MusicType.None;
	}
	/*
	public void PlayBgmAudio(int audioIndex){

		print(bgmAudioSource==null);
 		_audioSource.clip = bgmAudioSource [audioIndex];
		_audioSource.Play ();
	
	}
	public void PlayAudioEffect(int audioIndex){
		_audioSource.clip = audioSourceEffect [audioIndex];
		_audioSource.Play ();

	}
	public void Button(){
		_audioSource.PlayOneShot (clickButton);
	}*/
	/// <summary>
	/// 播放背景音乐
	/// </summary>
	public void PlayMusic(string musicName){
		try{
			//转换为MusicType类型
			MusicType music=(MusicType)System.Enum.Parse(typeof(MusicType),musicName);
			//如果转换成功
			if(System.Enum.IsDefined(typeof(MusicType),music)){
				//播放成功，返回
				PlayMusic(music);
				return;
			}
			else 
				Debug.Log(musicName+"音乐不存在");
		}
		catch(System.Exception){
			//如果转换失败，退出
			Debug.Log(musicName+"音乐不存在");
		}
		//如果音乐不存在预读中，从资源中查找
		AudioClip musicClip=Resources.Load<AudioClip>("Audio/"+musicName);
		if (musicClip != null) {
			//如果找到音乐文件
			if (_audioSource.clip == musicClip)
				return;
			//播放音乐
			_audioSource.clip=musicClip;
			_audioSource.Play ();
			_currentMusic = MusicType.None;
		}
	}

	/// <summary>
	/// 播放背景音乐
	/// </summary>
	public void PlayMusic(MusicType music){
		//Debug.Log (music);
		//如果当前音乐与要播放的音乐一致，则退出
		if(_currentMusic==music) return;
		//播放并更改当前音乐
		_currentMusic=music;

		//Debug.LogFormat ("music {0}", _currentMusic);
		_audioSource.Stop();

		switch(music){
		case MusicType.start:
			_audioSource.clip = _startBgm;
			break;
		case MusicType.game:
			_audioSource.clip = _gameBgm;
			break;
		case MusicType.gameover:
			_audioSource.clip = _gameFailureBgm;
			break;
		case MusicType.win:
			_audioSource.clip = _gameWinBgm;
			break;
		case MusicType.kaluli:
			_audioSource.clip = _kaluliBgm;
			break;
		}
		_audioSource.Play ();
	}

	/// <summary>
	/// 根据音效名称播放音效
	/// </summary>
	public void PlaySound(string soundName){
		try{
			//转换为SoundEffect类型
			SoundEffect sound=(SoundEffect)System.Enum.Parse(typeof(SoundEffect),soundName);
			//如果转换成功
			if(System.Enum.IsDefined(typeof(SoundEffect),sound)){
				//播放成功，返回
				PlaySound(sound);
				return;
			}
			//else 
			//Debug.Log(soundName+"音效不存在");
		}
		catch(System.Exception){
			//如果转换失败，退出
			//Debug.Log(soundName+"音效不存在");
		}
		//如果音效不存在预读中，从资源中查找
		AudioClip soundClip=Resources.Load<AudioClip>("SoundEffect/"+soundName);
		if (soundClip != null) {
			//如果找到音效文件
			AudioSource audio=GetFreeSoundAudio();
			if (audio != null) {
				//如果有空闲播放组件，播放音效
				audio.clip=soundClip;
				audio.Play ();
			
			}
		}
	}

	/// <summary>
	/// 播放指定音效
	/// </summary>
	public void PlaySound(SoundEffect sound){
		//Debug.Log (music);
		//找到空闲音效播放组件
		AudioSource freeAudio=GetFreeSoundAudio();

		//如果所有播放组件繁忙，退出函数
		if(freeAudio==null){
			return;
		}
		//音效置为空
		freeAudio.clip=null;
		//播放指定音效
		switch(sound){
		case SoundEffect.button:
			freeAudio.clip = _buttonEffect;
			break;
		case SoundEffect.build:
			freeAudio.clip = _buildEffect;
			break;
		case SoundEffect.upgrade:
			freeAudio.clip = _upgradeEffect;
			break;
		case SoundEffect.select:
			freeAudio.clip = _selectEffect;
			break;
		case SoundEffect.hit:
			freeAudio.clip = _hitEffect;
			break;
		case SoundEffect.money:
			freeAudio.clip = _moneyEffect;
			break;
		case SoundEffect.star:
			freeAudio.clip = _starEffect;
			break;
		}
		//如果音效不为空，播放音效
		if (freeAudio.clip != null)
			freeAudio.Play ();
	}

	/// <summary>
	/// 获得空闲音效组件
	/// </summary>
	AudioSource GetFreeSoundAudio(){
		for (int i = 0; i < _soundEffect.Length; i++) {
			if(_soundEffect[i].isPlaying){
				continue;
			}
			return _soundEffect [i];
		}
		return null;
	}

}
