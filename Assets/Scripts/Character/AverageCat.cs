using UnityEngine;
using System.Collections;

public class AverageCat : Cat {

    public AverageCat(int x, int y) : base(x, y)
    {
        base.maxLife = 100;
        base.defense = 75;
        base.attack = 75;
        base.speed = 75;
        base.mana = 60;
    }

    public override int Spell1()
    {
        //cat food     *distractifs* : croquettes -> la victime est trop occupe a bouffer pour faire autre chose  : bloque pendant 45secs 
        return 0;
    }

    public override int Spell2()
    {
        //wool ball    *immobilisant*: boule de laine -> la victime s'est emmele -> bloquee pendant 1min30 ou en cliquant 15 fois sur x 
        return 0;
    }

    public override int Spell3()
    {
        //cat bite     *hurt*        : morsure -> Att.chat_classique = 75
        return 0;
    }

    public override int Spell4()
    {
        //night vision *status*      : peut voir dans le noir pendant 45secondes quand les lumieres sont eteintes.
        return 0;
    }
}
