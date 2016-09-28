//Just decided to do it in GameState
//If need to be more then the class is here

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plaper {
	class Scoring {
		private int score;
		Vector2 scorePos;
		Game1 game;
		const string SCTEXT = "Score: ";
		Color textColor = Color.Black;

		//constructor
		public Scoring(Game1 game) {
			score = 0;
			scorePos = new Vector2(5.0f, 5.0f);
			this.game = game;
		}

		public void Update() {
			++score;
		}

		public void Draw(SpriteBatch spritebatch) {
			spritebatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullCounterClockwise);
			spritebatch.DrawString(Output.font24, SCTEXT + score, scorePos, textColor, 0, Vector2.Zero, Plaper.playHeight / 1000f, SpriteEffects.None, 0.5f);
			spritebatch.End();
		}

		public int GetScore() {
			return score;
		}
	}
}