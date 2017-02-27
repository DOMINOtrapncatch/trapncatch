using UnityEngine;
using UnityEngine.UI;

abstract public class Character
{
    public int x, y, z;
    public double width, height, depth;
    public int life, maxLife;

    public int attack, speed, defense;
    public int velo;

    //sourisvschat et chatvssouris
    //public Sprite icon;
    public Character(int x, int y, int z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
        this.life = maxLife;
    }

}