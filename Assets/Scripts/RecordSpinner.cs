using UnityEngine;

// Spins the record player's platter (and the record sitting on it) like a real turntable.
public class RecordSpinner : MonoBehaviour
{
    // A real record turns 33 1/3 times a minute = 200 degrees every second
    public float degreesPerSecond = 200f;

    void Update()
    {
        transform.Rotate(0f, degreesPerSecond * Time.deltaTime, 0f);
    }
}
