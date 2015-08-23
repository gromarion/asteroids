using UnityEngine;
using System.Collections;
using System.IO;

public class SoundManager : MonoBehaviour {

	public static SoundManager instance;

	public AudioClip shoot_sound, explosion1, explosion2, explosion3, thrust, enemy_fire;
	private AudioSource audio_source;
	private bool playing_sound;

	void Awake () {
		if (instance == null) {
			instance = this;
		}
		else if (instance != this) {
			Destroy(gameObject); 
		}
		DontDestroyOnLoad(gameObject);
	}

	void Start () {
		audio_source = (AudioSource)gameObject.AddComponent<AudioSource>();;
		playing_sound = false;
	}

	public void shoot() {
		if (shoot_sound) {
			audio_source.PlayOneShot(shoot_sound);
		}
	}

	public void enemy_shoot() {
		if (enemy_fire) {
			audio_source.PlayOneShot(enemy_fire);
		}
	}

	public void explode() {
		switch (Random.Range (1, 4)) {
		case 1:
			audio_source.PlayOneShot (explosion1);
			break;
		case 2:
			audio_source.PlayOneShot (explosion2);
			break;
		case 3:
			audio_source.PlayOneShot (explosion3);
			break;
		}
	}

	public void thrustOn() {
		if (!playing_sound) {
			audio_source.loop = playing_sound = true;
			audio_source.clip = thrust;
			audio_source.Play();
		}
	}

	public void thrustOff() {
		if (playing_sound) {
			playing_sound = false;
			audio_source.Stop();
		}
	}
}
