﻿using System.Collections.Generic;
using Nez.Textures;
using Microsoft.Xna.Framework;


namespace Nez.Sprites
{
	public enum AnimationCompletionBehavior
	{
		RemainOnFinalFrame,
		RevertToFirstFrame,
		HideSprite
	}


	/// <summary>
	/// houses the information that a SpriteT requires for animation
	/// </summary>
	public class SpriteAnimation
	{
		/// <summary>
		/// frames per second for the animations
		/// </summary>
		/// <value>The fps.</value>
		public float Fps
		{
			get => _fps;
			set => SetFps(value);
		}

		/// <summary>
		/// controls whether the animation should loop
		/// </summary>
		/// <value>The loop.</value>
		public bool Loop
		{
			get => _loop;
			set => SetLoop(value);
		}

		/// <summary>
		/// if loop is true, this controls if an animation loops sequentially or back and forth
		/// </summary>
		/// <value>The ping pong.</value>
		public bool PingPong
		{
			get => _pingPong;
			set => SetPingPong(value);
		}

		public float Delay = 0f;
		public float TotalDuration;
		public AnimationCompletionBehavior CompletionBehavior;
		public List<Sprite> Frames = new List<Sprite>();

		// calculated values used by SpriteT
		public float SecondsPerFrame;
		public float IterationDuration;

		float _fps = 10;
		bool _loop = true;
		bool _pingPong = false;
		bool _isDirty = true;


		public SpriteAnimation()
		{
		}


		public SpriteAnimation(Sprite frame)
		{
			AddFrame(frame);
		}


		public SpriteAnimation(List<Sprite> frames)
		{
			AddFrames(frames);
		}


		/// <summary>
		/// called by SpriteT to calculate the secondsPerFrame and totalDuration based on the loop details and frame count
		/// </summary>
		/// <returns>The for use.</returns>
		public void PrepareForUse()
		{
			if (!_isDirty)
				return;

			SecondsPerFrame = 1f / Fps;
			IterationDuration = SecondsPerFrame * (float) Frames.Count;

			if (Loop)
				TotalDuration = float.PositiveInfinity;
			else if (PingPong)
				TotalDuration = IterationDuration * 2f;
			else
				TotalDuration = IterationDuration;

			_isDirty = false;
		}


		/// <summary>
		/// sets the origin for all frames in this animation
		/// </summary>
		/// <param name="origin"></param>
		public SpriteAnimation SetOrigin(Vector2 origin)
		{
			for (var i = 0; i < Frames.Count; i++)
				Frames[i].Origin = origin;
			return this;
		}


		public SpriteAnimation SetFps(float fps)
		{
			_fps = fps;
			_isDirty = true;
			return this;
		}


		public SpriteAnimation SetLoop(bool loop)
		{
			_loop = loop;
			_isDirty = true;
			return this;
		}


		public SpriteAnimation SetPingPong(bool pingPong)
		{
			_pingPong = pingPong;
			_isDirty = true;
			return this;
		}


		/// <summary>
		/// adds a frame to this animation
		/// </summary>
		/// <param name="sprite">Sprite.</param>
		public SpriteAnimation AddFrame(Sprite sprite)
		{
			Frames.Add(sprite);
			return this;
		}


		/// <summary>
		/// adds multiple frames to this animation
		/// </summary>
		public SpriteAnimation AddFrames(List<Sprite> sprites)
		{
			for (var i = 0; i < sprites.Count; i++)
				AddFrame(sprites[i]);
			return this;
		}
	}
}