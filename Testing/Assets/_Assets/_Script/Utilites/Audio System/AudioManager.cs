using System;
using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;

namespace Abhishek.Utils {
    public class AudioManager : Singleton<AudioManager> {
        [SerializeField] private Sounds[] sounds;
        [SerializeField] private GameDataSO gameDataSO;
        private List<AudioSource> sfxSourceList;
        protected override void Awake()
        {
            base.Awake();
        }
        private void Start(){
            sfxSourceList = new List<AudioSource>();
            foreach(Sounds s in sounds){
                if(s.playAtPoint) continue;
                s.source = gameObject.AddComponent<AudioSource>();
                if(s.isSfx){
                    sfxSourceList.Add(s.source);
                }
                
                s.source.loop = s.isLooping;
                s.source.pitch = s.pitchSlider;
                s.source.volume = s.volumeSlider;
                s.source.playOnAwake = s.playOnAwake;
                s.source.clip = s.audioClip;
                s.source.mute = s.isMute;
                
            }
        }
        private void Update()
        {
            if (gameDataSO != null)
            {
                MuteMusic(!gameDataSO.GetSoundState());
                MuteSFX(!gameDataSO.GetSoundState());
                
            }
        }

        public void MuteMusic(bool Mute){
            
            for (int i = 0; i < sounds.Length; i++){
                if(sounds[i].soundType == Sounds.SoundType.BGM){
                }
                sounds[i].source.mute = Mute;
                
            }
            
        }
        public void MuteSFX(bool mute){
            for (int i = 0; i < sfxSourceList.Count; i++){
                sfxSourceList[i].mute = mute;
            }
        }
        
        
        public void PlayMusic(Sounds.SoundType soundType){
            Sounds s = Array.Find(sounds ,s => s.soundType == soundType);
            s.source.Play();
        }
        public void PauseMusic(Sounds.SoundType soundType){
            Sounds s = Array.Find(sounds ,s => s.soundType == soundType);
            if(s != null){
                if(s.source.clip != null){
                    s.source.Pause();
                }
            }
        }
        public void PlayOneShotMusic(Sounds.SoundType soundType)
		{
			if (AudioManager.Instance != null)
			{
				Sounds s = Array.Find(sounds, s => s.soundType == soundType);
				if (s != null)
				{
					if (s.source.clip != null)
					{
						s.source.PlayOneShot(s.audioClip);
					}
				}
			}
		}
        public void StopAudio(Sounds.SoundType soundType){
            Sounds s = Array.Find(sounds ,s => s.soundType == soundType);
            s.source.Stop();
        }
        public void StopAllAudios(){
            foreach(Sounds s in sounds){
                if(!s.playAtPoint){
                    StopAudio(s.soundType);
                }
            }
        }
        public void PlayAudioFromPos(AudioSource source,Sounds.SoundType soundType){
            if(gameDataSO.GetSoundState()){
                Sounds s = Array.Find(sounds ,s => s.soundType == soundType);
                source.clip = s.audioClip;
                if(source.isPlaying) return;
                source.mute = s.isMute;
                source.volume = s.volumeSlider;
                source.pitch = s.pitchSlider;
                source.playOnAwake = s.playOnAwake;
                source.loop = s.isLooping;
                source.Play();
            }else{
                source.Stop();
            }
        }
        public void StopAudioFromPos(AudioSource source){
            if(gameDataSO.GetSoundState()){
                source.Stop();
            }
        }
        
    }

}