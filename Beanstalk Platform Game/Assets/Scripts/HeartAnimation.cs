using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartAnimation : MonoBehaviour
{



    public void playDamageAnimation()
    {
        GetComponent<Animator>().Play("Heart_Damage");
    }

    public void DestroyHeart()
    {
        transform.parent.GetComponent<HealthDisplay>().removeHealthToList();
    }
}
