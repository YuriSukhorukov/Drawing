using UnityEngine;
using Utils;

namespace Audio
{
    [System.Serializable]
    public class SFXContainer
    {
        public AudioClip PressButton;
        public AudioClip PressCategoryItem;
        public AudioClip ScrollPictures;
        public AudioClip PressPencil;
    }

    [RequireComponent(typeof(AudioSource))]
    public class SFXManager : AbstractMonoSingleton<SFXManager>, ISoundManager
    {
        public SFXContainer SfxContainer = new SFXContainer();
        private AudioSource audioSource;

        public float Volume
        {
            get { return audioSource.volume; }
        }
	
        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            DontDestroyOnLoad(this);
        }
	
        public void SetVolume(float volume)
        {
            audioSource.volume = volume;
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

        public void OnPressButton()
        {
            if (SfxContainer.PressButton != null)
            {
                audioSource.PlayOneShot(SfxContainer.PressButton);
            }
        }

        public void OnPressCategoryItem()
        {
            if (SfxContainer.PressCategoryItem != null)
            {
                audioSource.PlayOneShot(SfxContainer.PressCategoryItem);
            }
        }

        public void OnPicturesScroll()
        {
            if (SfxContainer.ScrollPictures != null)
            {
                audioSource.PlayOneShot(SfxContainer.ScrollPictures);
            }
        }

        public void OnPressPencil()
        {
            if (SfxContainer.PressPencil != null)
            {
                audioSource.PlayOneShot(SfxContainer.PressPencil);
            }
        }
    }
}