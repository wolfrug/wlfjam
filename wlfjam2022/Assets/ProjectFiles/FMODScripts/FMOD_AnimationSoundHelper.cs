using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class FMOD_AnimationSoundHelper : MonoBehaviour {
   public string soundToPlay1 = "";
   public string soundToPlay2 = "";
   public string soundToPlay3 = "";
   public string soundToPlay4 = "";
   public string soundToPlay5 = "";
   public string soundToPlay6 = "";
   // Start is called before the first frame update

   public void PlaySound1 () {
      if (AudioManager.instance != null) {
         AudioManager.instance.PlaySFX (soundToPlay1);
      }
   }
   public void PlaySound2 () {
      if (AudioManager.instance != null) {
         AudioManager.instance.PlaySFX (soundToPlay2);
      }
   }
   public void PlaySound3 () {
      if (AudioManager.instance != null) {
         AudioManager.instance.PlaySFX (soundToPlay3);
      }
   }

   public void PlaySound4 () {
      if (AudioManager.instance != null) {
         AudioManager.instance.PlaySFX (soundToPlay4);
      }
   }

   public void PlaySound5 () {
      if (AudioManager.instance != null) {
         AudioManager.instance.PlaySFX (soundToPlay5);
      }
   }

   public void PlaySound6 () {
      if (AudioManager.instance != null) {
         AudioManager.instance.PlaySFX (soundToPlay6);
      }
   }
}