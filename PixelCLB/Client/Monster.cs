using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PixelCLB
{
    public class Monster : LoadedLife
    {
        public int HP;

        public int ServerHP;

        public MonsterStats Stats;

        public bool serverDead;

        public bool famTest;

        public Monster(int id, MonsterStats stats)
        {
            this.ID = id;
            this.Stats = stats;
            this.HP = this.Stats.HP;
            this.ServerHP = 100;
        }

        public Monster(int id)
        {
            this.ID = id;
        }

        public void Damage(int dmg)
        {
            Monster hP = this;
            hP.HP = hP.HP - dmg;
        }

        public void famSwitch()
        {
            this.famTest = !this.famTest;
        }
    }
}
