using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class AudioManager : MonoBehaviour {

    [SerializeField] private AudioClip click;
    [SerializeField] private AudioClip placeUnit;
    private AudioSource audio;
    private static AudioManager instance;
    public static event UnityAction playClick;
    //[SerializeField] private AudioClip
    // Start is called before the first frame update
    public static AudioManager GetAudio() => instance;
    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
        }
        else {
            instance = this;
        }
        audio=GetComponent<AudioSource>();
        
    }
    void Start() {
        UIManager.click += Click;
        GameManager.click += Click;
    }

    // Update is called once per frame
    void Update() {

    }
    private void Click(){
        audio.PlayOneShot(click);
    }
}
