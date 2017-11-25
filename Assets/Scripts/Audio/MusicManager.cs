using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class MusicManager : AbstractMonoSingleton<MusicManager>, ISoundManager {	
        public List<AudioClip> Tracks;
	
        private AudioSource audioSource;
        private int trackIndex;
        private Coroutine playNextTrackOrderCoroutine;

        public float Volume
        {
            get { return audioSource.volume; }
        }

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            // TODO Написать вменяемую обертку над проигрывателем
            DontDestroyOnLoad(gameObject);
            PurgeSoundtracks();
            Play();
        }

        private void PurgeSoundtracks()
        {
            for (int s = Tracks.Count - 1; s >= 0; s--)
            {
                if (Tracks[s] == null || Tracks[s].length <= 0) Tracks.RemoveAt(s);
            }
        }
	
        private void Play()
        {
            if(playNextTrackOrderCoroutine != null)
                StopCoroutine(playNextTrackOrderCoroutine);

            if (Tracks.Count > 0)
            {
                trackIndex = 0;
                audioSource.clip = Tracks[trackIndex];
                audioSource.Play();
                playNextTrackOrderCoroutine = StartCoroutine(PlayNextTrackOrder());
            }
        }

        private IEnumerator PlayNextTrackOrder()
        {
            yield return new WaitForSeconds(Tracks[trackIndex].length);
            trackIndex = trackIndex + 1 < Tracks.Count ? trackIndex + 1 : 0;
            audioSource.clip = Tracks[trackIndex];
            audioSource.Play();
            if (playNextTrackOrderCoroutine != null)
            {
                StopCoroutine(playNextTrackOrderCoroutine);
                playNextTrackOrderCoroutine = StartCoroutine(PlayNextTrackOrder());
            }
        }

        public void Mute()
        {
            audioSource.mute = true;
            audioSource.enabled = false;
        }

        public void UnMute()
        {
            audioSource.mute = false;
            audioSource.enabled = true;
        }

        public void SetVolume(float volume)
        {
            audioSource.volume = volume;
        }
    }
}