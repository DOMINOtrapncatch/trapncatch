using UnityEngine;
using UnityEngine.UI;

abstract public class Character : MonoBehaviour
{
    public int x, y, z;
    public double width, height, depth;
    public float life, maxLife;

    public int attack, speed, defense;
    public int velo;

}