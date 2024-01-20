using KWEngine3.GameObjects;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GruppeC.App.Classes
{
    internal class Bullet : GameObject
    {
        private Vector3 _moveDirection;
        public Bullet(Vector3 moveDirection)
        {
            _moveDirection = moveDirection;
            // Bullitt Propertys
            SetModel("KWQuad");
            HasTransparencyTexture = true;
            SetTexture("./App/imgs/Bullitt/b.png");
            AddRotationX(180);
            SetColor(77f, 166f, 255f);
            IsCollisionObject = true;
            SetScale(0.5f, 0.5f, 0.2f);
        }
        public override void Act()
        {
            if (IsInsideScreenSpace == false)
            {
                CurrentWorld.RemoveGameObject(this);
                return;
            }
            //bullet moves in players looking direction
            MoveAlongVector(_moveDirection, 0.2f);

            bullet_hits_the_enemy();
            bullet_hits_the_car();

            //IMG UPDATE
        }

        public void bullet_hits_the_enemy()
        {
            List<Intersection> intersections = GetIntersections();
            foreach (Intersection i in intersections)
            {
                GameWorld01 world = CurrentWorld as GameWorld01;
                // if there is a Colision between an Object and Bullitt
                //then Remove the Bullitt
                GameObject collider = i.Object;
                if (collider is Enemy)
                {
                    //CurrentWorld.RemoveGameObject(i.Object);
                    //i.Object.SetTexture("./App/imgs/Enemy/death.png");
                    CurrentWorld.RemoveGameObject(this);
                    world.enemy_item_drop(i.Object.Position.X, i.Object.Position.Y, i.Object.Position.Z);
                    if(i.Object is Enemy enemy)
                    {
                        enemy.HP = enemy.HP - 10;
                        if(enemy.HP <= 0)
                        {
                            world.RemoveGameObject(enemy);
                        }
                    }
                        

                }
            }
        }
        public void bullet_hits_the_car()
        {
            List<Intersection> intersections = GetIntersections();
            foreach (Intersection i in intersections)
            {
                // if there is a Colision between an Object and Bullitt
                //then Remove the Bullitt
                GameObject collider = i.Object;
                if (collider is car)
                {

                    CurrentWorld.RemoveGameObject(this);

                }
            }
        }
    }
}
