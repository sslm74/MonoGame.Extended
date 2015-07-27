using System;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Graphics;

namespace MonoGame.Extended.BitmapFonts
{
    public static class BitmapFontExtensions
    {
        public static void DrawString(this SpriteBatch spriteBatch, BitmapFont bitmapFont, string text, Vector2 position, Color color)
        {
            var dx = position.X;
            var dy = position.Y;

            foreach (char character in text)
            {
                var fontRegion = bitmapFont.GetCharacterRegion(character);

                if (fontRegion != null)
                {
                    var fontChar = fontRegion.FontCharacter;
                    var charPosition = new Vector2(dx + fontChar.XOffset, dy + fontChar.YOffset);

                    spriteBatch.Draw(fontRegion.TextureRegion, charPosition, color);
                    dx += fontChar.XAdvance;
                }

                if (character == '\n')
                {
                    dy += bitmapFont.LineHeight;
                    dx = position.X;
                }
            }
        }

        public static void DrawString(this SpriteBatch spriteBatch, BitmapFont bitmapFont, string text, Vector2 position, Color color, int wrapWidth)
        {
            var dx = position.X;
            var dy = position.Y;
            var sentences = text.Split(new[] {'\n'}, StringSplitOptions.None);

            foreach (var sentence in sentences)
            {
                var words = sentence.Split(new[] { ' ' }, StringSplitOptions.None);

                for (var i = 0; i < words.Length; i++)
                {
                    var word = words[i];
                    var size = bitmapFont.GetStringRectangle(word, Vector2.Zero);

                    if (i != 0 && dx + size.Width >= wrapWidth)
                    {
                        dy += bitmapFont.LineHeight;
                        dx = position.X;
                    }

                    DrawString(spriteBatch, bitmapFont, word, new Vector2(dx, dy), color);
                    dx += size.Width + bitmapFont.GetCharacterRegion(' ').FontCharacter.XAdvance;
                }

                dx = position.X;
                dy += bitmapFont.LineHeight;
            }
        }
    }
}