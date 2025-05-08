using UnityEngine;

public class Sounds : MonoBehaviour
{
    [Header ("Sounds Effects")]
    [SerializeField] private AudioClip[] _sounds;
    [SerializeField] private SoundArrays[] _randomSound;

    [SerializeField] private AudioSource _audioSource => GetComponent<AudioSource>();

    public void PlaySound(int index, float volume = 1f, bool random = false, bool destroyed = false, float mood1 = 0.85f, float mood2 = 1.2f)
    {
        AudioClip clip = random ? _randomSound[index].SoundArray[Random.Range(0, _randomSound[index].SoundArray.Length)] : _sounds[index];
        _audioSource.pitch = Random.Range(mood1, mood2);

        if (destroyed)
        {
            AudioSource.PlayClipAtPoint(clip, transform.position, volume);
        }
        else
        {
            _audioSource.PlayOneShot(clip, volume);
        }
    }
    public void StopSound()
    {
        _audioSource.Stop();
    }

}
