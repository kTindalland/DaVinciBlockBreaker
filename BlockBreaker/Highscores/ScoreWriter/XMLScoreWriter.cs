using System.IO;
using System.Xml;
using BlockBreaker.Highscores.Interfaces;
using BlockBreaker.Highscores.Resources;

namespace BlockBreaker.Highscores.ScoreWriter
{
    /// <summary>
    /// A class that will take in a new score and append it to the specified high scores list.
    /// </summary>
    public class XMLScoreWriter : IScoreWriter
    {
        /// <summary>
        /// Add a score onto the end of the highscores file
        /// </summary>
        /// <param name="newScore">The score that is being appended.</param>
        /// <param name="path">The path to the highscore file.</param>
        public void AppendScore(Score newScore, string path)
        {
            var doc = new XmlDocument();
            XmlNode root; // A reference to the root xml node.

            // Check if the file exists.
            if (File.Exists(path)) // If it does exist
            {
                doc.Load(path); // Load in the file
                root = doc.SelectSingleNode("//scores"); // Find the root
            }
            else // If it doesn't exist
            {
                doc.CreateXmlDeclaration("1.0", "UTF-8", null); // Create the header
                root = doc.CreateElement("scores"); // Create the root
                doc.AppendChild(root);
            }

            var newScoreNode = doc.CreateElement("score"); // Create a new blank score node

            var nameAttribute = doc.CreateAttribute("name"); // Create a new name attribute
            nameAttribute.Value = newScore.Name; // Fill the name attribute
            newScoreNode.Attributes.Append(nameAttribute); // Attach the attribute to the node.

            newScoreNode.InnerText = newScore.HighScore.ToString(); // Put the score inside of the node
            root.AppendChild(newScoreNode); // Attach the node inside of the root node


            doc.Save(path); // Save the file.
        }
    }
}
