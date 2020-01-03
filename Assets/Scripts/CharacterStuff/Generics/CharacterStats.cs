using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats
{
    private bool canMove;
    private int level;
    private int health;
    private int strength;
    private int skill;
    private int speed;
    private int luck;
    private int defense;
    private int resistance;
    private int constitution;
    private int movement;
    private int experience;
    private double healthGrowth;
    private double strengthGrowth;
    private double skillGrowth;
    private double speedGrowth;
    private double luckGrowth;
    private double defenseGrowth;
    private double resistanceGrowth;


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
