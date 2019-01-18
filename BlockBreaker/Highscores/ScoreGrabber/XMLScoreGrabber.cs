using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Linq;
using BlockBreaker.Highscores.Interfaces;
using BlockBreaker.Highscores.Resources;


namespace BlockBreaker.Highscores.ScoreGrabber
{
    /// <summary>
    /// A class to grab the scores from and xml file
    /// </summary>
    public class XMLScoreGrabber : IScoreGrabber
    {
        /// <summary>
        /// Get and return the scores from an xml file
        /// </summary>
        /// <param name="path">The filepath of the xml file</param>
        /// /// <param name="amount">The amount of scores you want</param>
        /// <returns>A list of scores</returns>
        public List<Score> ReadScores(string path, int amount = -1)
        {
            // Check if the file exists.
            if (!File.Exists(path))
                return new List<Score>(); // Return empty list of scores.


            var doc = new XmlDocument(); // Create the document object

            doc.Load(path); // Load the xml document into the doc instance

            var scores = new List<Score>(); // The list of scores that will be returned


            var scoreNodes = doc.SelectNodes("//scores/score"); // Gets all the score nodes

            foreach (XmlNode scoreNode in scoreNodes) // Add each score to the list
            {
                var name = scoreNode.Attributes["name"].Value; // Get the name from the name attribute
                var score = int.Parse(scoreNode.InnerText); // Get the score from the inner text

                var newScore = new Score(name, score); // Create the new score struct

                scores.Add(newScore); // Add the new score to the list
            }

            var sortedScores = scores.OrderByDescending(score => score.HighScore).ToList<Score>(); // Sort the scores

            if (amount > 0 && amount <= sortedScores.Count) // If count has been given a valid state
            {
                var firstScores = sortedScores.Take(amount); // Get first scores
                var firstScoresList = new List<Score>(); // new list to store scores in

                foreach (var score in firstScores) // Transfer scores over into the list
                {
                    firstScoresList.Add(score);
                }

                return firstScoresList; // Return the scores list
            }
            else
            {
                return scores; // Return the scores
            }

        }
    }
}
