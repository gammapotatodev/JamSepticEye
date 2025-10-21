using JetBrains.Annotations;
using UnityEngine;

public static class DynamicInstancer
{
    /// <summary>
    /// Create a positional AudioSource GameObject at runtime.
    /// </summary>
    /// <param name="clip"></param>
    /// <param name="position"></param>
    /// <param name="volume"></param>
    /// <param name="pitch"></param>
    /// <param name="pan"></param>
    /// <param name="blend"></param>
    /// <returns></returns>
    public static AudioSource InstanceAudioSource([CanBeNull]AudioClip clip, Vector3 position, [CanBeNull]float volume, [CanBeNull]float pitch,[CanBeNull]float pan,[CanBeNull]float blend)
    {
        #region Defaults
        if (volume <= 0) volume = 1f;
        #endregion
        
        var obj = new GameObject();
        obj.transform.position = position;
        
        var source = obj.AddComponent<AudioSource>();

        source.volume = volume;
        source.pitch = pitch;
        // pan only reaches max -1f, 1f so, me, myself and I will clamp it for you
        pan = Mathf.Clamp(pan, -1f, 1f);
        source.panStereo = pan;
        source.spatialBlend = blend;
        
        if (clip != null) source.PlayOneShot(clip);
        
        return source;
    }
}
