using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class screenShake : MonoBehaviour
{
    public CinemachineVirtualCamera Vcam;
    private CinemachineBasicMultiChannelPerlin noise;

    public void Start() {
        Vcam = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();
        noise = Vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin> ();
    }
    public void shake(float time, float amplitude, float frequency) {
        StartCoroutine(shakeScreen(time, amplitude, frequency));
    }

    public void Noise(float amplitudeGain, float frequencyGain) {
        noise.m_AmplitudeGain = amplitudeGain;
        noise.m_FrequencyGain = frequencyGain;    
    }

    IEnumerator shakeScreen(float time, float amplitude, float frequency) {
        Noise(amplitude, frequency);
        yield return new WaitForSeconds(time);
        Noise(0, 0);
    }
}
