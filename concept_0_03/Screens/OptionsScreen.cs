﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace concept_0_03
{
    class OptionsScreen : IGameScreen
    {
        private bool m_exitGame;
        private readonly IGameScreenManager m_ScreenManager;
        private bool isMusicOn = true;

        private List<Component> m_components;
        private SoundEffect click;

        private SoundEffect bgSong;
        private SoundEffectInstance bgMusic;

        public bool IsPaused { get; private set; }

        public OptionsScreen(IGameScreenManager gameScreenManager)
        {
            m_ScreenManager = gameScreenManager;
        }

        public void ChangeBetweenScreens()
        {
            if (m_exitGame)
            {
                m_ScreenManager.Exit();
            }
        }

        public void Init(ContentManager content)
        {
            click = content.Load<SoundEffect>("SFX/Select_Click");

            #region Resume Button
            var resumeButton = new Button(content.Load<Texture2D>("Menu/Grey/grey_button00"), content.Load<SpriteFont>("Fonts/Font"))
            {
                Position = new Vector2(300, 200),
                Text = "Resume"
            };

            resumeButton.Click += ResumeButton_Click;

            #endregion
            #region Save Button
            var saveButton = new Button(content.Load<Texture2D>("Menu/Grey/grey_button00"), content.Load<SpriteFont>("Fonts/Font"))
            {
                Position = new Vector2(300, 250),
                Text = "Save"
            };

            saveButton.Click += SaveButton_Click;
            #endregion
            #region Music Button
            var musicButton = new Button(content.Load<Texture2D>("Menu/Grey/grey_button00"), content.Load<SpriteFont>("Fonts/Font"))
            {
                Position = new Vector2(300, 300),
                Text = "Music",
            };

            musicButton.Click += MusicButton_Click;
            #endregion
            #region Quit Button
            var quitButton = new Button(content.Load<Texture2D>("Menu/Red/red_button00"), content.Load<SpriteFont>("Fonts/Font"))
            {
                Position = new Vector2(300, 350),
                Text = "Quit"
            };

            quitButton.Click += QuitButton_Click;
            #endregion

            m_components = new List<Component>()
            {
                resumeButton,
                saveButton,
                musicButton,
                quitButton,
            };
        }

        private void ResumeButton_Click(object sender, EventArgs e)
        {
            click.Play();
            m_ScreenManager.PopScreen();
        }

        private void QuitButton_Click(object sender, EventArgs e)
        {
            m_exitGame = true;
        }

        private void MusicButton_Click(object sender, EventArgs e)
        {
            click.Play();

            // Music Toggle
            if (isMusicOn)
            {
                Console.WriteLine("Music Off");
                isMusicOn = false;
            }
            else if (!isMusicOn)
            {
                Console.WriteLine("Music On");
                isMusicOn = true;
            }
            Console.WriteLine("Music Toggle");
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            click.Play();
            Console.WriteLine("Save Game");
        }

        public void Pause()
        {
            IsPaused = true;
        }

        public void Resume()
        {
            IsPaused = false;
        }

        public void Update(GameTime gameTime)
        {
            foreach (var component in m_components)
                component.Update(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            foreach (var component in m_components)
                component.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }

        public void HandleInput(GameTime gameTime)
        {
            var keyboard = Keyboard.GetState();
            var oldState = keyboard;

            if (keyboard.IsKeyDown(Keys.Escape))
            {
                m_exitGame = true;
            }

            /* -- Trying to get Back
            if (oldState.IsKeyUp(Keys.Back) && keyboard.IsKeyDown(Keys.Back))
            {
                m_ScreenManager.PopScreen();
            }
            */
        }

        public void Dispose()
        {
            
        }
    }
}