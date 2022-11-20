using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour{
    public static AudioClip moveSound, lowTimeSound, startGameSound, berserkSound, captureSound;
    static AudioSource audioSrc;

    void Awake(){
        audioSrc = GetComponent<AudioSource>();
        string dir = "Sounds/";
        moveSound = (AudioClip) Resources.Load($"{dir}Move");
        lowTimeSound = (AudioClip) Resources.Load($"{dir}LowTime");
        berserkSound = (AudioClip) Resources.Load($"{dir}Berserk");
        startGameSound = (AudioClip) Resources.Load($"{dir}GenericNotify");
        captureSound = (AudioClip) Resources.Load($"{dir}Capture");
    }    
    public static void PlaySound(string sound){
        switch (sound){
            case "move": audioSrc.PlayOneShot(moveSound); break;
            case "lowTime": audioSrc.PlayOneShot(lowTimeSound); break;
            case "berserk": audioSrc.PlayOneShot(berserkSound); break;
            case "notification": audioSrc.PlayOneShot(startGameSound); break;
            case "capture": audioSrc.PlayOneShot(captureSound); break;
        }
    }
}
