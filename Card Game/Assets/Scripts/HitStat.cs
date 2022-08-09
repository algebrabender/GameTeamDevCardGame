using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitStat : MonoBehaviour
{
    public Animator animatorStat;

    // Start is called before the first frame update
    void Start()
    {
        animatorStat = GetComponent<Animator>();
    }

    public void PlayStatHitAnim()
    {
        animatorStat.Play("HitStats");
    }
}
