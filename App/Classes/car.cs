using System;
using System.Collections.Generic;
using OpenTK.Windowing.GraphicsLibraryFramework;
using KWEngine3.GameObjects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Mathematics;
using System.Security.AccessControl;
using KWEngine3;
namespace GruppeC.App.Classes
{
    public class car : GameObject
    {
        
        private float speed = 0.12f;
        private float rotation = 0f;
        private bool hasPlayerInItJustSwitched = false;
        
        public car()
        {
            SetModel("KWQuad");
            Name = "c";
            SetPosition(5.0f, 0.5f, -5.0f);
            SetScale(9f, 4.3f, 3.0f);
            IsCollisionObject = true;
            IsShadowCaster = true;
            HasTransparencyTexture = true;
            SetTexture("./App/imgs/Cars/car2.png");
            AddRotationX(-90);
        }

        public void SetPlayerInCar(bool inCar)
        {
            GameWorld01 world = CurrentWorld as GameWorld01;
            //if the Player is not yet inside the car but attempts to get into
            //it gives time for the player to hop into the car and not getting any poopoo textures of KWCube shown
            if (world.get_has_player_in_it() == false && inCar == true)
            {
                hasPlayerInItJustSwitched = true;
            }
            world.set_has_player_in_it(true);

        }
        public bool GetPlayerInCar()
        {
            GameWorld01 world = CurrentWorld as GameWorld01;
            return world.get_has_player_in_it();
        }


        public override void Act()
        {
            light_folow_car_if_player_init();
            GameWorld01 world = CurrentWorld as GameWorld01;
            if (world.get_has_player_in_it())
            {
                /*
                if (fuel <= 0)
                {
                    speed = 0;
                    if (Keyboard.IsKeyPressed(Keys.A) || Keyboard.IsKeyPressed(Keys.D)) 
                    {
                        AddRotationY(rotation);
                    }
                }
                */

                if (Keyboard.IsKeyPressed(Keys.D1))
                {
                    speed = 0.06f;
                    rotation = 1f;
                }
                else if (Keyboard.IsKeyPressed(Keys.D2))
                {
                    speed = 0.10f;
                    rotation = 0.85f;
                }
                else if (Keyboard.IsKeyPressed(Keys.D3))
                {
                    speed = 0.14f;
                    rotation = 0.7f;
                }
                else if (Keyboard.IsKeyPressed(Keys.D4))
                {
                    speed = 0.17f;
                    rotation = 0.55f;
                }
                else if (Keyboard.IsKeyPressed(Keys.D5))
                {
                    speed = 0.20f;
                    rotation = 0.40f;
                }

                GameWorld01 World = CurrentWorld as GameWorld01;
                if (Keyboard.IsKeyDown(Keys.W) == true && World.get_car_fuel() >= 0.1f)
                {
                    MoveAlongVector(this.LookAtVectorLocalRight, speed);
                    World.loose_car_fuel(speed);
                    //MoveAlongVector(-speed);
                    if (Keyboard.IsKeyDown(Keys.D) == true)
                    {
                        AddRotationY(-rotation, true);

                    }
                    if (Keyboard.IsKeyDown(Keys.A) == true)
                    {
                        AddRotationY(rotation, true);
                    }

                }
                if (Keyboard.IsKeyDown(Keys.S) == true && World.get_car_fuel() >= 0.1f)
                {
                    World.loose_car_fuel(speed);
                    if (Keyboard.IsKeyDown(Keys.G) && Keyboard.IsKeyDown(Keys.KeyPad1))
                    {
                        speed = 3f;
                    }
                    MoveAlongVector(this.LookAtVectorLocalRight, -speed);
                    if (Keyboard.IsKeyDown(Keys.D) == true)
                    {
                        AddRotationY(rotation, true);

                    }
                    if (Keyboard.IsKeyDown(Keys.A) == true)
                    {
                        AddRotationY(-rotation, true);
                    }
                }

                if (Keyboard.IsKeyPressed(Keys.E) == true && hasPlayerInItJustSwitched == false)
                {
                    
                    SetPlayerInCar(false);
                    Player p = new Player();
                    p.SetPosition(this.Position.X, p.Position.Y + 0.5f, this.Position.Z + 4);
                    p.SetTexture("./App/imgs/PLayer/p2.png");
                    world.set_has_player_in_it(false);
                    CurrentWorld.AddGameObject(p);
                }


                CurrentWorld.SetCameraTarget(Position);
                CurrentWorld.SetCameraPosition(Position.X, Position.Y + 20, Position.Z + 0.000000000000001f);
                hasPlayerInItJustSwitched = false;
            }
            else
            {
                speed = 0.06f;
                rotation = 1f;
            }
            
        }
        public void light_folow_car_if_player_init()
        {
            GameWorld01 world = CurrentWorld as GameWorld01;
            if(world.get_has_player_in_it())
            {
                world.set_lightx(this.Position.X);
                world.set_lighty(this.Position.Y +1f);
                world.set_lightz(this.Position.Z);
            }
        }
    }
}