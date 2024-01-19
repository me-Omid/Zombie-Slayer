using KWEngine3;
using KWEngine3.GameObjects;
using KWEngine3.Helper;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using static OpenTK.Graphics.OpenGL.GL;
using System.Runtime.ConstrainedExecution;

namespace GruppeC.App.Classes
{
    public class Player : GameObject
    {
        public bool rotation = false;
        public bool cheat = false;
        public bool isInCar = false;
        public Player()
        {
            Name = "P";
            SetColor(0.0f, 255.0f, 0.0f);
            IsCollisionObject = true;
            IsShadowCaster = true;
            SetModel("KWQuad");
            SetScale(2.0f, 3.0f, 3.0f);
            HasTransparencyTexture = true;
            AddRotationX(-90);
            AddRotationY(180, true);
        }
        public override void Act()
        {
            //Player_Functions
            Player_Movement();
            Camera_Follow_the_Player();
            light_object_follow_player();
            player_rotation();
            Playershoots();
            player_takes_the_item();
            light_object_follow_player();
            Car_Movement_Apearence();
            //Collistions
            collistion_between_player_and_zombie();
            collistion_between_player_and_car();
            player_cheats();
            speed_up_with_shift();
        }
        public void speed_up_with_shift()
        {
            GameWorld01 world = CurrentWorld as GameWorld01;
            if (Keyboard.IsKeyDown(Keys.LeftShift) == true)
            {
                world.set_player_speed(0.04f);
            }
            else
            {
                world.set_player_speed(0.02f);
            }
        }
        public void player_takes_the_item()
        {
            GameWorld01 world = CurrentWorld as GameWorld01;
            List<Intersection> intersections = GetIntersections();
            foreach (Intersection i in intersections)
            {
                GameObject collider = i.Object;
                if (collider is item)
                {
                    if(i.Object.Name == "G")
                    {
                        CurrentWorld.RemoveGameObject(i.Object);
                        world.item_gold();
                    }
                    else if(i.Object.Name == "S")
                    {
                        CurrentWorld.RemoveGameObject(i.Object);
                        world.item_shild();
                    }
                    else if (i.Object.Name == "A")
                    {
                        CurrentWorld.RemoveGameObject(i.Object);
                        world.item_amunition();
                    }
                }
            }
        }
        public void light_object_follow_player()
        {
            GameWorld01 world = CurrentWorld as GameWorld01;
            if(world.get_has_player_in_it() == false)
            {
                world.set_lightx(Position.X);
                world.set_lightz(Position.Z);
                world.set_lighty(Position.Y + 1f);
            }
        }
        public void Camera_Follow_the_Player()
        {
            GameWorld01 gameworld = CurrentWorld as GameWorld01;

            CurrentWorld.SetCameraTarget(Position);
            CurrentWorld.SetCameraPosition(Position.X, Position.Y + 20, Position.Z + 0.00000000000000001f);



        }
        public void collistion_between_player_and_zombie()
        {
            bool collision = false;
            GameWorld01 world = CurrentWorld as GameWorld01;
            List<Intersection> intersections = GetIntersections();
            foreach (Intersection i in intersections)
            {
                
                GameObject collider = i.Object;
                if (collider is Enemy)
                {
                    Vector3 mtv = i.MTV;
                    MoveOffset(mtv);

                    if (CurrentWorld is GameWorld01)
                    {
  
                        world.enemy_damages_the_player();
                        world.GetLightObjectByName("#1").SetColor(2, 0, 0, 1);
                        world.GetLightObjectByName("#1").SetNearFar(1, 10);
                        collision = true;
                    }
                }
            }
            if(collision == false)
            {
                world.GetLightObjectByName("#1").SetNearFar(1, 100);
                world.GetLightObjectByName("#1").SetColor(1, 1, 0, 1);
            }
            collision = false;
            
            
          
        }
        public void collistion_between_player_and_car()
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
        public void Player_Movement()
        {
            GameWorld01 world = CurrentWorld as GameWorld01;
            if (Keyboard.IsKeyDown(Keys.W) == true)
            {
                MoveOffset(0.0f, 0.0f, -world.get_player_speed());
            }
            if (Keyboard.IsKeyDown(Keys.S) == true)
            {
                MoveOffset(0.0f, 0.0f, world.get_player_speed());
            }
            if (Keyboard.IsKeyDown(Keys.D) == true)
            {
                MoveOffset(world.get_player_speed(), 0.0f, 0.0f);
            }
            if (Keyboard.IsKeyDown(Keys.A) == true)
            {
                MoveOffset(-world.get_player_speed(), 0.0f, 0.0f);
            }
        }
        public void player_cheats()
        {
            if (Keyboard.IsKeyDown(Keys.Space) == true)
            {
                MoveOffset(0.0f, 0.03f, 0.00f);

            }
        }
        public void Car_Movement_Apearence()
        {
            List<car> cars = CurrentWorld.GetGameObjectsByType<car>();
            foreach (car x in cars)
            {
                Vector3 delta = x.Position - this.Position;
                float distance = delta.Length;
                if (distance < 4)
                {
                    if (Keyboard.IsKeyPressed(Keys.E) == true)
                    {
                        CurrentWorld.RemoveGameObject(this);
                        isInCar = true;
                        x.SetPlayerInCar(true);
                        break;
                    }
                    GameWorld01 World = CurrentWorld as GameWorld01;
                    if (Keyboard.IsKeyDown(Keys.F) == true)
                    {
                        if (World.get_car_fuel() < 50)
                        {
                            World.set_car_fuel();
                        }
                    }
                }
            }

        }
        public void player_rotation()
        {
            GameWorld01 world = CurrentWorld as GameWorld01;
            if (Keyboard.IsKeyDown(Keys.Left) == true)
            {
                AddRotationY(1, true);
            }
            if (Keyboard.IsKeyDown(Keys.Right) == true)
            {
                AddRotationY(-1, true);
            }
            if (Keyboard.IsKeyDown(Keys.Left) == true || Keyboard.IsKeyDown(Keys.Right) == true)
            {
                rotation = true;
            }
            if (Keyboard.IsKeyDown(Keys.Left) == false && Keyboard.IsKeyDown(Keys.Right) == false)
            {
                rotation = false;
            }
        }
        public void Playershoots()
        {
            GameWorld01 gameworld = CurrentWorld as GameWorld01;
            //Player Shoots
            if (Keyboard.IsKeyPressed(Keys.Up) == true && rotation == false && gameworld.get_amunition() > 0)
            {
                // Bullet bekommt jetzt über den Konstruktor gleich die Flugbahn mit, weil bei
                // Fake-2D-Umgebungen der LookAtVector nicht gleichwertig verwendet werden kann:
                Bullet b = new Bullet(-this.LookAtVectorLocalUp);

                // Wenn Bullet ein KWQuad ist, guckt es in die Standardrichtung +Z (wie alle 3D-Modelle).
                // Weil die Kamera aber von oben auf die Szene blickt, wird Bullet
                // dann mit AddRotationX(-90, true) so gedreht, dass es nach oben 
                // zeigt, weil es sonst nicht sichtbar wäre (weil es nur hauchdünn ist).
                // Dadurch dreht sich aber auch der LookAtVector mit, so dass
                // 2D-Objekte eigentlich keinen brauchbaren LookAtVector haben, weil diese 
                // dann auch zur Kamera zeigen (statt z.B. nach rechts/inks/vorne/hinten).

                // Lösung:
                // Jedes GameObject hat auch noch LookAtVectorLocalRight und LocalUp.
                // Einen der beiden kann man dann definitiv für die eigenen Zwecke
                // nutzen. In diesem Fall ist es der lokale Up-Vector.
                b.SetPosition(this.Center - this.LookAtVectorLocalUp * 1f);
                b.SetRotation(this.Rotation); // damit das Bullet-Objekt auch nach oben schaut
                CurrentWorld.AddGameObject(b);

                gameworld.set_amunition(1);
            }
        }
    }
}
