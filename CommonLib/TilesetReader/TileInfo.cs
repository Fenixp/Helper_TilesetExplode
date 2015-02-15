using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonLib.Common
{
    public class TileInfo
    {
        int _tileSize;
        int _xCoord;
        int _yCoord;
        Texture2D _texture;
        Rectangle _rectangle;
        string _name;

        public TileInfo(string name, int size, int xCoord, int yCoord, Texture2D texture)
        {
            _tileSize = size;
            _xCoord = xCoord;
            _yCoord = yCoord;
            _texture = texture;
            _name = name;

            _rectangle = new Rectangle(_xCoord * _tileSize, _yCoord * _tileSize, _tileSize, _tileSize);
            
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public int XCoord
        {
            get
            {
                return _xCoord;
            }
        }

        public int YCoord
        {
            get
            {
                return _yCoord;
            }
        }

        public Texture2D Texture
        {
            get 
            {
                return _texture;
            }
        }

        public Rectangle Rectangle
        {
            get
            {
                return _rectangle;
            }
        }

        public int TileSize
        {
            get
            {
                return _tileSize;
            }
        }
    }
}
