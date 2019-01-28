using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockBreaker.Resources;
using DaVinci_Framework.DefaultRenderables;
using DaVinci_Framework.Renderer.Resources;

namespace BlockBreaker.Renderables
{
    public class DoublePointsBlock : Block
    {
        private Text _scoreMultiplierLabel;

        public DoublePointsBlock(int difficulty, double[] initialPosition, int[] dimensions, Text scoreMultLabel) : base(difficulty, initialPosition, dimensions)
        {
            _scoreMultiplierLabel = scoreMultLabel;
        }

        
        public override Pixel[,] ItemPixels(bool isBlank = false)
        {
            var pixels = new Pixel[_dimensions[0], _dimensions[1]];

            for (int i = 0; i < _dimensions[1]; i++) // The Y axis
            {
                for (int j = 0; j < _dimensions[0]; j++) // The X axis
                {
                    if (!isBlank)
                    {
                        pixels[j, i] = new Pixel('£', ConsoleColor.Black ,_difficultyColours[_difficulty]);
                    }
                    else
                    {
                        pixels[j, i] = new Pixel(ConsoleColor.Black);
                    }

                }
            }

            return pixels;
        }

        public override void Break()
        {
            base.Break();

            ResourceManager.PointMultiplier *= 2;
            _scoreMultiplierLabel.ChangeText("Current Score Multipler : " + ResourceManager.PointMultiplier);
            SetTimer();
        }

        public void SetTimer()
        {
            var multTimer = new System.Timers.Timer(1000);
            multTimer.Enabled = true;
            multTimer.AutoReset = false;
            multTimer.Elapsed += OnTimedEvent;
            multTimer.Start();
        }

        public void OnTimedEvent(object source, EventArgs args)
        {
            ResourceManager.PointMultiplier /= 2;
            _scoreMultiplierLabel.ChangeText("Current Score Multipler : " + ResourceManager.PointMultiplier);
        }
    }
}
