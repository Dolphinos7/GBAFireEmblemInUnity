using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats
{
    public bool canMove;
    public int level;
    public int health;
    public int strength;
    public int skill;
    public int speed;
    public int luck;
    public int defense;
    public int resistance;
    public int constitution;
    public int movement;
    public int experience;
    public double healthGrowth;
    public double strengthGrowth;
    public double skillGrowth;
    public double speedGrowth;
    public double luckGrowth;
    public double defenseGrowth;
    public double resistanceGrowth;


    public CharacterStats(int lev, int hp, int str, int skl, int spd, int lck, int def, int res, int con, int mov, double healthGrowth, double strengthGrowth, double skillGrowth, double speedGrowth, double luckGrowth, double defenseGrowth, double resistanceGrowth)
    {
        level = lev;
        health = hp;
        strength = str;
        skill = skl;
        speed = spd;
        luck = lck;
        defense = def;
        resistance = res;
        constitution = con;
        movement = mov;
        this.healthGrowth = healthGrowth;
        this.strengthGrowth = strengthGrowth;
        this.skillGrowth = skillGrowth;
        this.speedGrowth = speedGrowth;
        this.luckGrowth = luckGrowth;
        this.defenseGrowth = defenseGrowth;
        this.resistanceGrowth = resistanceGrowth;
        experience = 0;
        canMove = true;
    }

    public int getMovement(){
        return movement;
    }

    public bool getCanMove(){
        return canMove;
    }

    public void setCanMove(bool state){
        canMove = state;
    }
}
