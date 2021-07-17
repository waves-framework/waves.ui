using System;

namespace MTL.UI.Media.Drawing
{
    /// <summary>
    ///     Point base structure.
    /// </summary>
    [Serializable]
    public struct MtlPoint
    {
        /// <summary>
        ///     Main Constructor.
        /// </summary>
        /// <param name="xValue">The x value of the vector. </param>
        /// <param name="yValue">The y value of the vector. </param>
        public MtlPoint(float xValue, float yValue)
            : this()
        {
            X = xValue;
            Y = yValue;
        }

        /// <summary>
        ///     X coordinate.
        /// </summary>
        public float X { get; set; }

        /// <summary>
        ///     Y coordinate.
        /// </summary>
        public float Y { get; set; }

        /// <summary>
        ///     The length of the vector.
        /// </summary>
        public float Length => (float)Math.Sqrt(SquaredLength);

        /// <summary>
        ///     The squared length of the vector. Useful for optimi.
        /// </summary>
        public float SquaredLength => X * X + Y * Y;

        /// <summary>
        ///     The absolute angle of the vector.
        /// </summary>
        public float Angle => (float)Math.Atan2(Y, X);

        /// <summary>
        ///     ToString method overriden for easy printing/debugging.
        /// </summary>
        /// <returns>The string representation of the vector.</returns>
        public override string ToString()
        {
            return $"({X}, {Y})";
        }
    }
}
