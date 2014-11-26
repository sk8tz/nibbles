﻿using System;
using System.Collections.Generic;
using CocosSharp;
using Nibbles.Shared.Helpers;

namespace Nibbles.Shared.Layers
{
	public class GameScoresLayer : CCLayerColor
	{
		CCSprite logo;
		CCRepeatForever repeatedAction;
		string scoreMessage = string.Empty;
		bool highScore;

		public GameScoresLayer ()
		{

			var touchListener = new CCEventListenerTouchAllAtOnce ();
			touchListener.OnTouchesBegan = (touches, ccevent) => {
				var mainGame = GameStartLayer.CreateScene (Window);
				var transitionToGameOver = new CCTransitionMoveInL (0.3f, mainGame);

				Director.ReplaceScene (transitionToGameOver);
			};

			AddEventListener (touchListener, this);

			scoreMessage = String.Format ("Current High Score");

			Color = CCColor3B.White;

			Opacity = 255;

			var moveUp = new CCMoveBy (1.0f, new CCPoint (0.0f, 50.0f));
			var moveDown = moveUp.Reverse ();

			// A CCSequence action runs the list of actions in ... sequence!
			CCSequence moveSeq = new CCSequence (new CCEaseBackInOut (moveUp), new CCEaseBackInOut (moveDown));

			repeatedAction = new CCRepeatForever (moveSeq);

		}



		protected override void AddedToScene ()
		{
			base.AddedToScene ();

			Scene.SceneResolutionPolicy = CCSceneResolutionPolicy.ShowAll;

			var scoreLabel = new CCLabel (scoreMessage, "Roboto-Light", 48) {
				Position = new CCPoint (VisibleBoundsWorldspace.Size.Center.X + 190, VisibleBoundsWorldspace.Size.Center.Y + 125),
				Color = CCColor3B.Black,
				HorizontalAlignment = CCTextAlignment.Center,
				VerticalAlignment = CCVerticalTextAlignment.Center,
				AnchorPoint = CCPoint.AnchorMiddle
			};

			AddChild (scoreLabel);

			Scene.SceneResolutionPolicy = CCSceneResolutionPolicy.ShowAll;

			var scoreLabel2 = new CCLabel (scoreMessage, "Roboto-Light", 56) {
				Position = new CCPoint (VisibleBoundsWorldspace.Size.Center.X + 150, VisibleBoundsWorldspace.Size.Center.Y + 125),
				Color = new CCColor3B (52, 152, 219),
				HorizontalAlignment = CCTextAlignment.Center,
				VerticalAlignment = CCVerticalTextAlignment.Center,
				AnchorPoint = CCPoint.AnchorMiddle,
				Text = Settings.HighScore.ToString()
			};

			AddChild (scoreLabel2);

			var playAgainLabel = new CCLabel ("Tap to Return", "Roboto-Light", 36) {
				Position = new CCPoint (VisibleBoundsWorldspace.Size.Center.X + 110, 60),
				Color = CCColor3B.Black,
				HorizontalAlignment = CCTextAlignment.Center,
				VerticalAlignment = CCVerticalTextAlignment.Center,
				AnchorPoint = CCPoint.AnchorMiddle,
			};

			AddChild (playAgainLabel);


			CCRect visibleBounds = VisibleBoundsWorldspace;
			CCPoint centerBounds = visibleBounds.Center;

			logo = new CCSprite ("title");
			// Layout the positioning of sprites based on visibleBounds
			logo.AnchorPoint = CCPoint.AnchorMiddle;
			logo.Position = new CCPoint (centerBounds.X, centerBounds.Y - 175);

			// Run actions on sprite
			// Note: we can reuse the same action definition on multiple sprites!
			logo.RunAction (repeatedAction);

			AddChild (logo);

		}

		public static CCScene CreateScene (CCWindow mainWindow)
		{
			var scene = new CCScene (mainWindow);
			var layer = new GameScoresLayer ();

			scene.AddChild (layer);

			return scene;
		}

	}
}