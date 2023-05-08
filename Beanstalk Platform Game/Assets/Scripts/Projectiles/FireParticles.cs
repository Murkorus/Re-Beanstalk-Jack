using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FireParticles : MonoBehaviour
{

    public bool fadeOut;

    public ParticleSystem PS;
    public Light2D light;
    public AudioSource AS;
    void Start()
    {
        StartCoroutine(DestroyWaitTime(6));
    }

    IEnumerator DestroyWaitTime(float time) {
        PS.Play();

        yield return new WaitForSeconds(time - 0.1f);

        PS.Stop();
        GetComponentInChildren<DistantAudio>().fadeOut = true;
        GetComponentInChildren<lightFade>().fadeOut = true;

        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
    }
}
