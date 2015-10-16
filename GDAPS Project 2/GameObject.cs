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
        Vector2 objImage;
        Rectangle objRect;
        Texture objTexture;

        public abstract bool isColliding();
        public void spriteDraw(SpriteBatch s)
        {
            this.spriteDraw(s);
        }
    }
}
