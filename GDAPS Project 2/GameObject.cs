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
        public bool isDangerous = false;

        public string itemType;

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
        public int ObjRectWidth
        {
            get { return objRect.Width; }
            set { objRect.Width = value; }
        }
        public int ObjRectHeight
        {
            get { return objRect.Height; }
            set { objRect.Height = value; }
        }

        Texture2D objImage;

        public Texture2D ObjImage
        {
            get { return objImage; }
            set { objImage = value; }
        }

        public virtual void spriteDraw(SpriteBatch s)
        {
            if (itemType == "panel" || itemType == "paneldark")
            {
                s.Draw(objImage, objRect, null, Color.White, 0.0f, new Vector2(0, 0), SpriteEffects.None, layerDepth: 0.4f);
            }
            else 
            {
                s.Draw(objImage, objRect, null, Color.White, 0, new Vector2(0,0), SpriteEffects.None, layerDepth: 0.3f);
            }
        }

        public GameObject(int x, int y, int width ,int height)
        {
            objRect = new Rectangle(x, y, width, height);
            itemType = null;
        }

        public GameObject(int x, int y, int width, int height, string ItemType)
        {
            objRect = new Rectangle(x, y, width, height);
            itemType = ItemType;
        }
    }
}
