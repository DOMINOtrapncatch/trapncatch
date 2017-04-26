using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class SpeedUp : Spell
{
    public ParticleSystem particle;
    [Range(0, 100)]
    public int SpeedAmount = 20;

    public override void Activate()
    {
        StartCoroutine(speed());
        cat.Attack += 20;
        cat.Speed -= SpeedAmount;
    }

    IEnumerator speed()
    {
        particle.Stop();
        particle.transform.position = cat.transform.position;
        particle.Play();
        cat.Attack -= 20;
        cat.Speed += SpeedAmount;

        yield return new WaitForSeconds(20f);
    }
}