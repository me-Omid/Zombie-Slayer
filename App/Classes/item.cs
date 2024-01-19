using KWEngine3;
using KWEngine3.GameObjects;
using KWEngine3.Helper;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using static OpenTK.Graphics.OpenGL.GL;
namespace GruppeC.App.Classes
{
    internal class item : GameObject
    {
        public item()
        {
            SetModel("KWQuad");
            SetScale(1f, 1f, 0.1f);
            AddRotationX(-90);
            AddRotationY(180, true);
            IsCollisionObject = true;
            HasTransparencyTexture = true;
        }
        public override void Act()
        {
            remvoe_item_if_outside_the_screen();
        }
        
        public void add_light_object()
        {
            GameWorld01 world = CurrentWorld as GameWorld01;
            world.item_light(Position.X, Position.Y, Position.Z);
        }

        public void remvoe_item_if_outside_the_screen()
        {
            GameWorld01 world = CurrentWorld as GameWorld01;
            if (IsInsideScreenSpace == false)
            {
                CurrentWorld.RemoveGameObject(this);
            }
        }
    }
}
