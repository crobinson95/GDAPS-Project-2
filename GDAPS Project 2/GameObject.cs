using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GDAPS_Project_2
{
    abstract class GameObject
    {
        Rectangle objRect;
        Rectangle ObjRect
        {
            get { return objRect; }
            set { objRect = value; }
        }
        int ObjRectX
        {
            get { return objRect.X; }
            set { objRect.X = value; }
        }
        int ObjRectY
        {
            get { return objRect.Y; }
            set { objRect.Y = value; }
        }

        Texture2D objImage;
        Texture2D ObjImage
        {
            get { return objImage; }
            set { objImage = value; }
        }

        public abstract bool isColliding();
        public void spriteDraw(SpriteBatch s)
        {
            s.Draw(objImage, objRect, Color.White);
        }
    }
}
