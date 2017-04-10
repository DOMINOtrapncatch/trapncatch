using UnityEngine;
using UnityEngine.UI;

abstract public class Character : MonoBehaviour
{
    public float x, y, z;
    public float width, height, depth;
    public float life, maxLife;

    public float Attack, Speed, Defense;
	private float maxAttack = 20, maxSpeed = 8, maxDefense = 20;
	public float attack { get { return Attack * maxAttack / 100; } }
	public float speed { get { return Speed * maxSpeed / 100; } }
	public float defense { get { return Defense * maxDefense / 100; } }

    public float velo;

}