using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace AxisAndAlliesEurope
{
    public static class Victory
    {

        public static void drawVictory(Game1 game, ref Player player, SpriteBatch spriteBatch, ContentManager theContent)
        {
            spriteBatch.Begin(SpriteBlendMode.AlphaBlend,
            SpriteSortMode.Immediate, SaveStateMode.None);

            SpriteFont font;
            SpriteFont fontHighLighted;

            font = theContent.Load<SpriteFont>("Fonts\\GameFont");
            fontHighLighted = theContent.Load<SpriteFont>("Fonts\\GameFontHighLighted");

            spriteBatch.DrawString(font,
            player.getWhosTurn() + ": Wins" + "     click on Esc to exit", (
            new Vector2(0, 0)),
            Color.Black);

            spriteBatch.End();
        }
    }
}
