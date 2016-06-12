using System;
using System.IO;    // Stream

namespace CSharp_081020
{
    /// <summary>
    /// This (public) class represents a bit stream.
    ///</summary>
    public class BitStream
    {
        #region Private Instance Fields

        /// <summary>
        /// The stream over which the bit stream abstraction is created.
        /// </summary>
        private Stream stream;

        /// <summary>
        /// The bit buffer is represented as byte that is written to the stream once filled.
        /// </summary>
        private byte bitBuffer;

        /// <summary>
        /// Keeps track of the number of bits buffered in the bit buffer.
        /// </summary>
        private int bitCount;

        #endregion // Private Instance Fields

        #region Public Instance Properties

        /// <summary>
        /// Gets the stream over which the bit stream abstraction is created.
        /// </summary>
        /// 
        /// <value>
        /// The stream over which the bit stream abstraction is created.
        /// </value>
        public Stream Stream
        {
            get
            {
                return stream;
            }
        }

        /// <summary>
        /// Determines whether the bit buffer is full, i.e whether one byte needs to be written to the stream.
        /// </summary>
        /// 
        /// <value>
        /// <c>True</c> if the bit buffer is full, <c>false</c> otherwise.
        /// </value>
        public bool BitBufferFull
        {
            get
            {
                return (bitCount == 8);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool BitBufferEmpty
        {
            get
            {
                return (bitCount == 0);
            }
        }

        #endregion // Public Instance Properties

        #region Public Instance Constructors

        /// <summary>
        /// Creates a new bit stream abstraction over a given stream.
        /// </summary>
        /// 
        /// <param name="stream">The stream over which the bit stream abstraction is to be created.</param>
        ///
        /// <expcetion cref="ArgumentNullException">
        /// Condition: <c>stream</c> is <c>null</c>.
        /// </expcetion>
        public BitStream(Stream stream)
        {
            // Validate the arguments.
            if (stream == null)
            {
                throw new ArgumentNullException();
            }

            // Initialize the instance fields.
            this.stream = stream;
        }

        #endregion // Public Instance Constructors

        #region Public Instance Methods

        /// <summary>
        /// Writes a bit to the stream.
        /// </summary>
        /// 
        /// <param name="bit">The bit.</param>
        /// 
        /// <exception cref="ArgumentException">
        /// Condition: <c>bit</c> is neither 0 nor 1.
        /// </exception>
        public void WriteBit(int bit)
        {
            // Validate the arguments.
            if (!((bit == 0) || (bit == 1)))
            {
                throw new ArgumentException();
            }

            // Buffer the bit.
            bitBuffer |= (byte)(bit << (7 - bitCount));
            bitCount++;

            // If the bit buffer is full, flush it by writing one byte to the stream.
            if (BitBufferFull)
            {
                stream.WriteByte(bitBuffer);
                bitBuffer = 0;
                bitCount = 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Finish()
        {
            stream.WriteByte(bitBuffer);
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <returns>
        /// </returns>
        public int ReadBit()
        {
            // If the bit buffer is empty, fill it by reading one byte from the stream.
            if (BitBufferEmpty)
            {
                int i = stream.ReadByte();
                if (i == -1)
                {
                    return -1;
                }
                else
                {
                    bitBuffer = (byte)i;
                    bitCount = 8;
                }
            }

            // Flush the bit.
            int bit = (bitBuffer >> (bitCount - 1)) & 1;
            bitCount--;

            // Return the bit.
            return bit;
        }



        #endregion // Public Instance Methods
    }
}
