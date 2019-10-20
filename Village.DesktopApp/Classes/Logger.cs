using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Village.Core;

namespace Village.DesktopApp.Classes
{
    public class Logger : Village.Core.ILogger
    {
        private List<string> _errors;

        public Logger()
        {
            _errors = new List<string>();
        }

        public void LogError(string message)
        {
            _errors.Add(message);
        }

        public void LogError(string message, Exception e)
        {
            _errors.Add(message);
        }

        public void DrawLog(SpriteBatch spriteBatch, GraphicsDevice graphics, SpriteFont font)
        {
            var box = new Texture2D(graphics, 640, 64);
            Color[] data = new Color[640 * 64];
            for (int i = 0; i < data.Length; ++i) data[i] = Color.Black;
            box.SetData(data);
            spriteBatch.Draw(box, new Vector2(0, 640 - 64), Color.White);

            List<string> messages;

            if (_errors.Count <= 4)
                messages = _errors.ToList();
            else
                messages = _errors.GetRange(_errors.Count() - 5, 4);

            var y = 640;
                foreach(var message in messages)
                    spriteBatch.DrawString(font, message
                    , new Vector2(0, (y = y - 15)), Color.White);

        }
    }
}
