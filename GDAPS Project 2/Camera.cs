using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GDAPS_Project_2
{
    class Camera
    {
        public Vector2 position;
        GraphicsDevice range;


        public Rectangle right;
        public Rectangle left;
        public Rectangle up;

        public Matrix viewMatrix { get; set; }
        public float camX
        {
            get { return position.X; }
            set { position.X = camX; }
        }

        public float camY
        {
            get { return position.Y; }
            set { position.Y = camY; }
        }

        public Vector2 origin { get; set; }
        public Camera(Player p, GraphicsDevice screen)
        {
            position = Vector2.Zero;
            origin = Vector2.Zero;
            position.X = p.ObjRect.Center.X - 150;
            position.Y = p.ObjRect.Top - 100;
            right = new Rectangle(screen.Viewport.Width / 2, 0, screen.Viewport.Width / 2, screen.Viewport.Height);
            left = new Rectangle(0, 0, screen.Viewport.Width / 4, screen.Viewport.Height);
            up = new Rectangle(0, 0, screen.Viewport.Width, screen.Viewport.Height / 2);
            range = screen;
        }

        public void Move(Player p, int width, int height)
        {
            position.X = -p.ObjRectX + width/2;
            position.Y = -p.ObjRectY + height/2;
        }

        public Matrix GetTransform(Player p, int width, int height)
        {
            Move(p, width, height);
            var translationMatrix = Matrix.CreateTranslation(new Vector3(position.X, position.Y, 0));
            var originMatrix = Matrix.CreateTranslation(new Vector3(origin.X, origin.Y, 0));

            return translationMatrix * originMatrix;
        }
    }
}
