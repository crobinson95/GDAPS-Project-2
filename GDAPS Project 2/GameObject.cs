using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GDAPS_Project_2
{
   public abstract class GameObject
    {
        public bool isDangerous;

        Rectangle objRect;
        public Rectangle ObjRect
        {
            get { return objRect; }
            set { objRect = value; }
        }
        public int ObjRectX
        {
            get { return objRect.X; }
            set { objRect.X = value; }
        }
        public int ObjRectY
        {
            get { return objRect.Y; }
            set { objRect.Y = value; }
        }

        Texture2D objImage;
        public Texture2D ObjImage
        {
            get { return objImage; }
            set { objImage = value; }
        }

        public virtual void spriteDraw(SpriteBatch s)
        {
            s.Draw(objImage, objRect, Color.White);
        }

        public GameObject(int x, int y, int width ,int height)
        {
            objRect = new Rectangle(x, y, width, height);
        }
    }
}
