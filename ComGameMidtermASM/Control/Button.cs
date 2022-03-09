using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ComGameMidtermASM.Control
{
    public class Button : Component
    {
        #region Fields

        private MouseState _currentMouseState;

        //private SpriteFont _font;

        private bool _isHovering;

        private MouseState _previousMouseState;

        private Texture2D _texture;


        #endregion

        #region Properties

        public event EventHandler Click;

        public string Text { get; set; }
        public bool Clicked{ get; private set; }

        public Color PenColor { get; set; }

        public Vector2 Position { get; set; }

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);
            }
        }


        #endregion

        #region Methods

        public Button(Texture2D texture)
        {
            _texture = texture;
            //_font = font;
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //declare idle button state
            var colour = Color.White;
            if (_isHovering)
            {
                _texture = start_hover_ori;
            }

            spriteBatch.Draw(_texture, Rectangle, colour);

            if (!string.IsNullOrEmpty(Text))
            {
                var x = (Rectangle.X + (Rectangle.Width / 2));
                var y = (Rectangle.X + (Rectangle.Height / 2));

                //spriteBatch.DrawString(_font, Text, new Vector2(x, y), PenColor);
            }
        }

        public override void Update(GameTime gameTime)
        {
            _previousMouseState = _currentMouseState;
            _currentMouseState = Mouse.GetState();

            var mouseRectangle = new Rectangle(_currentMouseState.X, _currentMouseState.Y, 1, 1);

            _isHovering = false;

            if (mouseRectangle.Intersects(Rectangle))
            {
                _isHovering = true;

                if (_currentMouseState.LeftButton == ButtonState.Released && _previousMouseState.LeftButton == ButtonState.Pressed)
                {
                    Click?.Invoke(this, new EventArgs());

                }
            }
        }

        #endregion
    }
}
