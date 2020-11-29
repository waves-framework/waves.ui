//
// Author: Ryan Seghers
//
// Copyright (C) 2013-2014 Ryan Seghers
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the irrevocable, perpetual, worldwide, and royalty-free
// rights to use, copy, modify, merge, publish, distribute, sublicense, 
// display, perform, create derivative works from and/or sell copies of 
// the Software, both in source and object code form, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

using System;

namespace Waves.UI.Drawing.Charting.Utils
{
	/// <summary>
	///     Cubic spline interpolation.
	///     Call Fit (or use the corrector constructor) to compute spline coefficients, then Eval to evaluate the spline at
	///     other X coordinates.
	/// </summary>
	/// <remarks>
	///     <para>
	///         This is implemented based on the wikipedia article:
	///         http://en.wikipedia.org/wiki/Spline_interpolation
	///         I'm not sure I have the right to include a copy of the article so the equation numbers referenced in
	///         comments will end up being wrong at some point.
	///     </para>
	///     <para>
	///         This is not optimized, and is not MT safe.
	///         This can extrapolate off the ends of the splines.
	///         You must provide points in X sort order.
	///     </para>
	/// </remarks>
	public class CubicSpline
    {
        #region Fields

        // N-1 spline coefficients for N points
        private float[] _a;
        private float[] _b;

        // Save the original x and y for Eval
        private float[] _xOrig;
        private float[] _yOrig;

        #endregion

        #region Ctor

        /// <summary>
        ///     Default ctor.
        /// </summary>
        public CubicSpline()
        {
        }

        /// <summary>
        ///     Construct and call Fit.
        /// </summary>
        /// <param name="x">Input. X coordinates to fit.</param>
        /// <param name="y">Input. Y coordinates to fit.</param>
        /// <param name="startSlope">Optional slope constraint for the first point. Single.NaN means no constraint.</param>
        /// <param name="endSlope">Optional slope constraint for the final point. Single.NaN means no constraint.</param>
        /// <param name="debug">Turn on console output. Default is false.</param>
        public CubicSpline(float[] x, float[] y, float startSlope = float.NaN, float endSlope = float.NaN,
            bool debug = false)
        {
            Fit(x, y, startSlope, endSlope, debug);
        }

        #endregion

        #region Private Methods

        /// <summary>
        ///     Throws if Fit has not been called.
        /// </summary>
        private void CheckAlreadyFitted()
        {
            if (_a == null) throw new Exception("Fit must be called before you can evaluate.");
        }

        private int _lastIndex;

        /// <summary>
        ///     Find where in xOrig the specified x falls, by simultaneous traverse.
        ///     This allows xs to be less than x[0] and/or greater than x[n-1]. So allows extrapolation.
        ///     This keeps state, so requires that x be sorted and xs called in ascending order, and is not multi-thread safe.
        /// </summary>
        private int GetNextXIndex(float x)
        {
            if (x < _xOrig[_lastIndex]) throw new ArgumentException("The X values to evaluate must be sorted.");

            while (_lastIndex < _xOrig.Length - 2 && x > _xOrig[_lastIndex + 1]) _lastIndex++;

            return _lastIndex;
        }

        /// <summary>
        ///     Evaluate the specified x value using the specified spline.
        /// </summary>
        /// <param name="x">The x value.</param>
        /// <param name="j">Which spline to use.</param>
        /// <param name="debug">Turn on console output. Default is false.</param>
        /// <returns>The y value.</returns>
        private float EvalSpline(float x, int j, bool debug = false)
        {
            var dx = _xOrig[j + 1] - _xOrig[j];
            var t = (x - _xOrig[j]) / dx;
            var y = (1 - t) * _yOrig[j] + t * _yOrig[j + 1] + t * (1 - t) * (_a[j] * (1 - t) + _b[j] * t); // equation 9
            if (debug) Console.WriteLine("xs = {0}, j = {1}, t = {2}", x, j, t);
            return y;
        }

        #endregion

        #region Fit*

        /// <summary>
        ///     Fit x,y and then eval at points xs and return the corresponding y's.
        ///     This does the "natural spline" style for ends.
        ///     This can extrapolate off the ends of the splines.
        ///     You must provide points in X sort order.
        /// </summary>
        /// <param name="x">Input. X coordinates to fit.</param>
        /// <param name="y">Input. Y coordinates to fit.</param>
        /// <param name="xs">Input. X coordinates to evaluate the fitted curve at.</param>
        /// <param name="startSlope">Optional slope constraint for the first point. Single.NaN means no constraint.</param>
        /// <param name="endSlope">Optional slope constraint for the final point. Single.NaN means no constraint.</param>
        /// <param name="debug">Turn on console output. Default is false.</param>
        /// <returns>The computed y values for each xs.</returns>
        public float[] FitAndEval(float[] x, float[] y, float[] xs, float startSlope = float.NaN,
            float endSlope = float.NaN, bool debug = false)
        {
            Fit(x, y, startSlope, endSlope, debug);
            return Eval(xs, debug);
        }

        /// <summary>
        ///     Compute spline coefficients for the specified x,y points.
        ///     This does the "natural spline" style for ends.
        ///     This can extrapolate off the ends of the splines.
        ///     You must provide points in X sort order.
        /// </summary>
        /// <param name="x">Input. X coordinates to fit.</param>
        /// <param name="y">Input. Y coordinates to fit.</param>
        /// <param name="startSlope">Optional slope constraint for the first point. Single.NaN means no constraint.</param>
        /// <param name="endSlope">Optional slope constraint for the final point. Single.NaN means no constraint.</param>
        /// <param name="debug">Turn on console output. Default is false.</param>
        public void Fit(float[] x, float[] y, float startSlope = float.NaN, float endSlope = float.NaN,
            bool debug = false)
        {
            if (float.IsInfinity(startSlope) || float.IsInfinity(endSlope))
                throw new Exception("startSlope and endSlope cannot be infinity.");

            // Save x and y for eval
            _xOrig = x;
            _yOrig = y;

            var n = x.Length;
            var r = new float[n]; // the right hand side numbers: wikipedia page overloads b

            var m = new TriDiagonalMatrixF(n);
            float dx1, dx2, dy1, dy2;

            // First row is different (equation 16 from the article)
            if (float.IsNaN(startSlope))
            {
                dx1 = x[1] - x[0];
                m.C[0] = 1.0f / dx1;
                m.B[0] = 2.0f * m.C[0];
                r[0] = 3 * (y[1] - y[0]) / (dx1 * dx1);
            }
            else
            {
                m.B[0] = 1;
                r[0] = startSlope;
            }

            // Body rows (equation 15 from the article)
            for (var i = 1; i < n - 1; i++)
            {
                dx1 = x[i] - x[i - 1];
                dx2 = x[i + 1] - x[i];

                m.A[i] = 1.0f / dx1;
                m.C[i] = 1.0f / dx2;
                m.B[i] = 2.0f * (m.A[i] + m.C[i]);

                dy1 = y[i] - y[i - 1];
                dy2 = y[i + 1] - y[i];
                r[i] = 3 * (dy1 / (dx1 * dx1) + dy2 / (dx2 * dx2));
            }

            // Last row also different (equation 17 from the article)
            if (float.IsNaN(endSlope))
            {
                dx1 = x[n - 1] - x[n - 2];
                dy1 = y[n - 1] - y[n - 2];
                m.A[n - 1] = 1.0f / dx1;
                m.B[n - 1] = 2.0f * m.A[n - 1];
                r[n - 1] = 3 * (dy1 / (dx1 * dx1));
            }
            else
            {
                m.B[n - 1] = 1;
                r[n - 1] = endSlope;
            }

            if (debug) Console.WriteLine("Tri-diagonal matrix:\n{0}", m.ToDisplayString(":0.0000", "  "));
            if (debug) Console.WriteLine("r: {0}", ArrayUtil.ToString(r));

            // k is the solution to the matrix
            var k = m.Solve(r);
            if (debug) Console.WriteLine("k = {0}", ArrayUtil.ToString(k));

            // a and b are each spline's coefficients
            _a = new float[n - 1];
            _b = new float[n - 1];

            for (var i = 1; i < n; i++)
            {
                dx1 = x[i] - x[i - 1];
                dy1 = y[i] - y[i - 1];
                _a[i - 1] = k[i - 1] * dx1 - dy1; // equation 10 from the article
                _b[i - 1] = -k[i] * dx1 + dy1; // equation 11 from the article
            }

            if (debug) Console.WriteLine("a: {0}", ArrayUtil.ToString(_a));
            if (debug) Console.WriteLine("b: {0}", ArrayUtil.ToString(_b));
        }

        #endregion

        #region Eval*

        /// <summary>
        ///     Evaluate the spline at the specified x coordinates.
        ///     This can extrapolate off the ends of the splines.
        ///     You must provide X's in ascending order.
        ///     The spline must already be computed before calling this, meaning you must have already called Fit() or
        ///     FitAndEval().
        /// </summary>
        /// <param name="x">Input. X coordinates to evaluate the fitted curve at.</param>
        /// <param name="debug">Turn on console output. Default is false.</param>
        /// <returns>The computed y values for each x.</returns>
        public float[] Eval(float[] x, bool debug = false)
        {
            CheckAlreadyFitted();

            var n = x.Length;
            var y = new float[n];
            _lastIndex = 0; // Reset simultaneous traversal in case there are multiple calls

            for (var i = 0; i < n; i++)
            {
                // Find which spline can be used to compute this x (by simultaneous traverse)
                var j = GetNextXIndex(x[i]);

                // Evaluate using j'th spline
                y[i] = EvalSpline(x[i], j, debug);
            }

            return y;
        }

        /// <summary>
        ///     Evaluate (compute) the slope of the spline at the specified x coordinates.
        ///     This can extrapolate off the ends of the splines.
        ///     You must provide X's in ascending order.
        ///     The spline must already be computed before calling this, meaning you must have already called Fit() or
        ///     FitAndEval().
        /// </summary>
        /// <param name="x">Input. X coordinates to evaluate the fitted curve at.</param>
        /// <param name="debug">Turn on console output. Default is false.</param>
        /// <returns>The computed y values for each x.</returns>
        public float[] EvalSlope(float[] x, bool debug = false)
        {
            CheckAlreadyFitted();

            var n = x.Length;
            var qPrime = new float[n];
            _lastIndex = 0; // Reset simultaneous traversal in case there are multiple calls

            for (var i = 0; i < n; i++)
            {
                // Find which spline can be used to compute this x (by simultaneous traverse)
                var j = GetNextXIndex(x[i]);

                // Evaluate using j'th spline
                var dx = _xOrig[j + 1] - _xOrig[j];
                var dy = _yOrig[j + 1] - _yOrig[j];
                var t = (x[i] - _xOrig[j]) / dx;

                // From equation 5 we could also compute q' (qp) which is the slope at this x
                qPrime[i] = dy / dx
                            + (1 - 2 * t) * (_a[j] * (1 - t) + _b[j] * t) / dx
                            + t * (1 - t) * (_b[j] - _a[j]) / dx;

                if (debug) Console.WriteLine("[{0}]: xs = {1}, j = {2}, t = {3}", i, x[i], j, t);
            }

            return qPrime;
        }

        #endregion

        #region Static Methods

        /// <summary>
        ///     Static all-in-one method to fit the splines and evaluate at X coordinates.
        /// </summary>
        /// <param name="x">Input. X coordinates to fit.</param>
        /// <param name="y">Input. Y coordinates to fit.</param>
        /// <param name="xs">Input. X coordinates to evaluate the fitted curve at.</param>
        /// <param name="startSlope">Optional slope constraint for the first point. Single.NaN means no constraint.</param>
        /// <param name="endSlope">Optional slope constraint for the final point. Single.NaN means no constraint.</param>
        /// <param name="debug">Turn on console output. Default is false.</param>
        /// <returns>The computed y values for each xs.</returns>
        public static float[] Compute(float[] x, float[] y, float[] xs, float startSlope = float.NaN,
            float endSlope = float.NaN, bool debug = false)
        {
            var spline = new CubicSpline();
            return spline.FitAndEval(x, y, xs, startSlope, endSlope, debug);
        }

        /// <summary>
        ///     Fit the input x,y points using the parametric approach, so that y does not have to be an explicit
        ///     function of x, meaning there does not need to be a single value of y for each x.
        /// </summary>
        /// <param name="x">Input x coordinates.</param>
        /// <param name="y">Input y coordinates.</param>
        /// <param name="nOutputPoints">How many output points to create.</param>
        /// <param name="xs">Output (interpolated) x values.</param>
        /// <param name="ys">Output (interpolated) y values.</param>
        /// <param name="firstDx">
        ///     Optionally specifies the first point's slope in combination with firstDy. Together they
        ///     are a vector describing the direction of the parametric spline of the starting point. The vector does
        ///     not need to be normalized. If either is NaN then neither is used.
        /// </param>
        /// <param name="firstDy">See description of dx0.</param>
        /// <param name="lastDx">
        ///     Optionally specifies the last point's slope in combination with lastDy. Together they
        ///     are a vector describing the direction of the parametric spline of the last point. The vector does
        ///     not need to be normalized. If either is NaN then neither is used.
        /// </param>
        /// <param name="lastDy">See description of dxN.</param>
        public static void FitParametric(float[] x, float[] y, int nOutputPoints, out float[] xs, out float[] ys,
            float firstDx = float.NaN, float firstDy = float.NaN, float lastDx = float.NaN, float lastDy = float.NaN)
        {
            // Compute distances
            var n = x.Length;
            var dists = new float[n]; // cumulative distance
            dists[0] = 0;
            float totalDist = 0;

            for (var i = 1; i < n; i++)
            {
                var dx = x[i] - x[i - 1];
                var dy = y[i] - y[i - 1];
                var dist = (float) Math.Sqrt(dx * dx + dy * dy);
                totalDist += dist;
                dists[i] = totalDist;
            }

            // Create 'times' to interpolate to
            var dt = totalDist / (nOutputPoints - 1);
            var times = new float[nOutputPoints];
            times[0] = 0;

            for (var i = 1; i < nOutputPoints; i++) times[i] = times[i - 1] + dt;

            // Normalize the slopes, if specified
            NormalizeVector(ref firstDx, ref firstDy);
            NormalizeVector(ref lastDx, ref lastDy);

            // Spline fit both x and y to times
            var xSpline = new CubicSpline();
            xs = xSpline.FitAndEval(dists, x, times, firstDx / dt, lastDx / dt);

            var ySpline = new CubicSpline();
            ys = ySpline.FitAndEval(dists, y, times, firstDy / dt, lastDy / dt);
        }

        private static void NormalizeVector(ref float dx, ref float dy)
        {
            if (!float.IsNaN(dx) && !float.IsNaN(dy))
            {
                var d = (float) Math.Sqrt(dx * dx + dy * dy);

                if (d > float.Epsilon) // probably not conservative enough, but catches the (0,0) case at least
                {
                    dx = dx / d;
                    dy = dy / d;
                }
                else
                {
                    throw new ArgumentException("The input vector is too small to be normalized.");
                }
            }
            else
            {
                // In case one is NaN and not the other
                dx = dy = float.NaN;
            }
        }

        #endregion
    }
}