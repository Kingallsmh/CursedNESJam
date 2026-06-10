using UnityEngine;

public class RandomVarianceSFX : MonoBehaviour
{
    [SerializeField] AudioSource source;
    [SerializeField] Vector2 volumeRange;
    [SerializeField] Vector2 pitchRange;

    public void SetRandomVolume()
    {
        source.volume = Random.Range(volumeRange.x, volumeRange.y);
    }

    public void SetRandomPitch()
    {
        source.pitch = Random.Range(pitchRange.x, pitchRange.y);
    }
}
