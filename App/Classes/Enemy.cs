using KWEngine3.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Mathematics;

namespace GruppeC.App.Classes
{
    internal class Enemy : GameObject
    {
        public float speed;
        public Enemy()
        {
            IsCollisionObject = true;
            HasTransparencyTexture = true;
            SetScale(2.0f, 3.0f, 3.0f);
            IsCollisionObject = true;
            IsShadowCaster = true;
            AddRotationX(-90);
        }
        public override void Act()
        {
            if(this.Position.Y < 0.5)
            {
                CurrentWorld.RemoveGameObject(this);
            }
            get_enemy_speed_from_gameworld();
            follow_the_player();
            collision_between_enemys();
            collistion_between_car_and_zombie();
            collision_between_enemy_players_item();
        }
        public void get_enemy_speed_from_gameworld()
        {
            if (CurrentWorld is GameWorld01)
            {
                GameWorld01 World = CurrentWorld as GameWorld01;
                speed = World.set_enemy_speed();
            }
        }
        public void follow_the_player()
        {   
            if (IsInsideScreenSpace == true)
            {
                Player Player = CurrentWorld.GetGameObjectByName<Player>("P");
                car Car = CurrentWorld.GetGameObjectByName<car>("c");
                if (Player != null)
                {
                    TurnTowardsXZ(Player.Position);
                    AddRotationX(90);
                }
                if (Player == null && Car.GetPlayerInCar() == true)
                {
                    TurnTowardsXZ(Car.Position);
                    AddRotationX(90);
                }
                MoveAlongVector(this.LookAtVectorLocalUp, speed);
            }
        }
        public void collision_between_enemys()
        {
            //Colider
            List<Intersection> intersections = GetIntersections();
            foreach (Intersection i in intersections)
            {
                GameObject collider = i.Object;
                if (collider is Enemy)
                {
                    Vector3 mtv = i.MTV;
                    MoveOffset(mtv);
                }
            }
        }
        public void collision_between_enemy_players_item()
        {
            //Colider
            List<Intersection> intersections = GetIntersections();
            foreach (Intersection i in intersections)
            {
                GameObject collider = i.Object;
                if (collider is item)
                {
                    CurrentWorld.RemoveGameObject(i.Object);
                }
            }
        }
        public void collistion_between_car_and_zombie()
        {
            List<Intersection> intersections = GetIntersections();
            foreach (Intersection i in intersections)
            {
                GameObject collider = i.Object;
                if (collider is car)
                {
                    Vector3 mtv = i.MTV;
                    MoveOffset(mtv);
                }
            }
        }

    }
}
