using System;
using System.Collections.Generic;       // List
using System.Text;                      // StringBuilder
using System.Text.RegularExpressions;   // Regex

namespace CSharp_081013
{
    /// <summary>
    /// This (public) class represent a word buffer.
    /// <summary>
    public class WordBuffer
    {
        #region Private Instance Fields

        /// <summary>
        /// The list of words buffered in the buffer.
        /// </summary>
        private readonly List<string> bufferedWords;

        /// <summary>
        /// The delimiting pattern according to which the line is to be split into words.
        /// </summary>
        private readonly string delimitingPattern;

        /// <summary>
        /// The width to which the block is to be formatted.
        /// </summary>
        private readonly int blockWidth;

        /// <summary>
        /// The width of words (separated by single space characters) currently contained in the buffer.
        /// </summary>
        private int containedWidth;

        #endregion // Private Instance Fields

        #region Internal Instance Properties

        /// <summary>
        /// Gets the block width.
        /// </summary>
        /// 
        /// <value>
        /// The block width.
        /// </value>
        internal int BlockWidth
        {
            get
            {
                return blockWidth;
            }
        }

        /// <summary>
        /// Gets the contained width.
        /// </summary>
        /// 
        /// <value>
        /// The contained width.
        /// </value>
        internal int ContainedWidth
        {
            get
            {
                return containedWidth;
            }
        }

        /// <summary>
        /// Determines whether the buffer can be flushed.
        /// </summary>
        /// 
        /// <value>
        /// <c>True</c> if a line can be flushed, <c>false</c> otherwise.
        /// </value>
        internal bool Flushable
        {
            get
            {
                return (ContainedWidth >= BlockWidth) ? true : false;
            }
        }

        /// <summary>
        /// Determines whether the buffer is empty (ergo contains no buffered words).
        /// </summary>
        /// 
        /// <value>
        /// <c>True</c> if the word buffer is empty, <c>false</c> otherwise.
        /// </value>
        internal bool Empty
        {
            get
            {
                return (bufferedWords.Count == 0) ? true : false;
            }
        }

        /// <summary>
        /// Gets the next words's lenght.
        /// </summary>
        /// 
        /// <value>
        /// The next word's length.
        /// </value>
        internal int NextWordLength
        {
            get
            {
                if (Empty)
                {
                    throw new Exception();
                }
                return (bufferedWords[0].Length);
            }
        }

        #endregion // Internal Instance Properties

        #region Internal Instance Constructors

        // TODO: Hide the default (non-parameter) constructor.

        /// <summary>
        /// Creates a new word buffer which formats the text to a given block width.
        /// </summary>
        /// 
        /// <param name="blockWidth">The block width.</param>
        /// 
        /// <exception cref="ArgumentException">
        /// Condition: <c>blockWidth</c> is less than or equal to zero.
        /// </exception>
        internal WordBuffer(int blockWidth)
        {
            // Validate the arguments.

            // Validate the block width.
            if (blockWidth <= 0)
            {
                throw new ArgumentException();
            }

            // Initialize the instance fields.

            bufferedWords = new List<string>();
            delimitingPattern = "[ \t]+";
            this.blockWidth = blockWidth;
            containedWidth = 0;
        }

        #endregion // Internal Instance Constructors

        #region Internal Instance Methods

        /// <summary>
        /// Buffers a given line into the word buffer, i.e. feeds the words comprising the line into the word buffer.
        /// </summary>
        /// 
        /// <param name="line">The line to be buffered.</param>
        internal void BufferLine(string line)
        {
            // Split the line into words according to the delimiters.
            string[] words = Regex.Split(line, delimitingPattern);

            // Add the words to the buffer.
            foreach (string word in words)
            {
                this.bufferedWords.Add(word);

                // The word buffer now contains the newly added word...
                containedWidth += word.Length;

                // ... and a space trailing it. 
                containedWidth += 1;
            }

            // Remove the space trailing the lastly added word.
            containedWidth -= 1;
        }

        /// <summary>
        /// Flushes a line from the word buffer.
        /// </summary>
        internal string FlushLine()
        {
            // Initially, the line contains no words, i.e. the line is empty.
            List<string> lineWords = new List<string>();
            int lineLength = 0;

            while (!Empty && ((lineLength + PeekWord().Length) <= blockWidth))
            {
                lineLength += (PeekWord().Length + 1);
                lineWords.Add(FlushWord());
            }

            // Return the formatted line.
            return FormatLine(lineWords);
        }

        #endregion // Internal Instance Methods

        #region Private Instance Methods

        /// <summary>
        /// Peeks at the next word contained in the word buffer.
        /// </summary>
        /// 
        /// <returns>
        /// The next word contained in the word buffer.
        /// </returns>
        private string PeekWord()
        {
            // Throw exception if the word buffer is empty (i.e. no word can be flushed).
            if (Empty)
            {
                throw new Exception();
            }

            return bufferedWords[0];
        }

        /// <summary>
        /// Flushes the next word contained in the word buffer.
        /// </summary>
        /// 
        /// <returns>
        /// The next word contained in the word buffer.
        /// </returns>
        private string FlushWord()
        {
            // Throw exception if the word buffer is empty (i.e. no word can be flushed).
            if (Empty)
            {
                throw new Exception();
            }

            // Get the word.
            string word = bufferedWords[0];

            // Remove the word from the word buffer ...
            bufferedWords.RemoveAt(0);

            // ... and update the contained width.
            containedWidth -= (word.Length + 1);

            // Return the word.
            return word;
        }

        /// <summary>
        /// Formats given words into a line of desired (block) width.
        /// </summary>
        /// 
        /// <param name="words"></param>
        /// 
        /// <returns>
        /// 
        /// </returns>
        private string FormatLine(List<string> words)
        {
            // Calculate the space width contained within the line.
            int wordsWidth = 0;
            foreach (string lineWord in words)
            {
                wordsWidth += lineWord.Length;
            }
            int spaceWidth = blockWidth - wordsWidth;

            // Calculate the narrow separator width.
            int narrowSeparatorWidth =
                (words.Count > 1) ?
                spaceWidth / (words.Count - 1) :
                spaceWidth;

            // Build the narrow separator.
            StringBuilder narrowSeparatorStringBuilder = new StringBuilder();
            for (int i = 0; i < narrowSeparatorWidth; i++)
            {
                narrowSeparatorStringBuilder.Append(' ');
            }
            string narrowSeparator = narrowSeparatorStringBuilder.ToString();

            // Calculate the number of wide separators.
            int wideSeparatorCount =
                (words.Count > 1) ?
                spaceWidth % (words.Count - 1) :
                0;

            // Build the formatted line.
            StringBuilder lineStringBuilder = new StringBuilder();

            // Append all the words (except for the last one) followed by a separator.
            for (int i = 0; i < (words.Count - 1); i++)
            {
                // Append the word and a narrow separator following it.
                lineStringBuilder.Append(words[i] + narrowSeparator);

                // Append an additional space if necessary.
                if (wideSeparatorCount > 0)
                {
                    lineStringBuilder.Append(' ');
                    wideSeparatorCount--;
                }
            }

            // Append the last word not followed by separator.
            lineStringBuilder.Append(words[words.Count - 1]);

            // Return the formatted string.
            return lineStringBuilder.ToString();
        }

        #endregion // Private Instance Methods
    }
}
