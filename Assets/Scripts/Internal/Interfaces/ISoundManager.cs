using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISoundManager
{
    void Mute();
    void UnMute();
    void SetVolume(float volume);
}
