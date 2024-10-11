using System;
using Unity.VisualScripting;

// Testing system for making sure data-based stuff has good functionality.

// Created by Leif Larson
// Last updated 8 Oct 2024

#nullable enable

namespace Testing
{

    enum Verbosity
    {
        NONE=0, FAILS_ONLY=1, ALL=2
    }

    /// <summary>
    /// <b>Instructions for use:</b>
    /// <br></br>1.) Use Test() (or TestDelta() for float values) to verify the values of tests.
    /// <br></br>- Set the expected value to the result the functionality should produce.
    /// <br></br>- Set the experimental value to the result the functionality actually produces.
    /// <br></br>- Note: You may choose to print them to the console with Debug.Log(Test(...)), since Test() does return a string.
    /// <br></br>2.) Print the output using either <see cref="GetSummary"/> to get the overall result, or <see cref="GetRecord"/> to get a closer look at which tests did what.
    /// </summary>
    class TestBattery
    {
        internal int identicals, passes, fails;
        private string name, abbr;
        private string record;
        private Verbosity verbose;

        /// <summary>
        /// Creates a test battery object.
        /// </summary>
        /// <param name="name">The title of the test battery given at the top of <see cref="GetRecord"/>.</param>
        /// <param name="abbr">The abbreviation of the name. Keep it very short.</param>
        /// <param name="verbose">Whether to store details of each tested value, or just the test result in the full report.</param>
        public TestBattery(string name, string? abbr, Verbosity? verbose=Verbosity.FAILS_ONLY)
        {
            this.name = name;
            if (abbr == null)
            {
                this.abbr = name.Substring(0, Math.Min(name.Length, 3));
            }
            else
            {
                this.abbr = abbr;
            }
            this.record = "====[ " + name + " ]====\n";
            if (verbose == null)
            {
                this.verbose = Verbosity.FAILS_ONLY;
            }
            else
            {
                this.verbose = (Verbosity) verbose;
            }
            identicals = 0;
            passes = 0;
            fails = 0;
        }

        public string GetName() { return name; }

        /// <summary>
        /// Set when extra data should be displayed in the tests.
        /// </summary>
        /// <param name="newVerbose">the new verbose value.</param>
        public void SetVerbose(Verbosity newVerbose) 
        { 
            verbose = newVerbose; 
        }

        /// <summary>
        /// Saves a test that compares an <paramref name="expected"/> and <paramref name="experimental"/> value, and tolerates miniscule errors. The test result is tallied and saved.
        /// <br></br>For values that are not floats, use Test() instead.
        /// 
        /// <br></br><b>Result Types:</b>
        /// <br></br><b>Passes:</b> The expected/experimental parameters are not further apart than <paramref name="delta"/>.
        /// <br></br><b>Fails:</b> The expected/experimental parameters do *not* match.
        /// </summary>
        /// <param name="label">The label for the test.</param>
        /// <param name="expected">The expected value.</param>
        /// <param name="experimental">The value being tested.</param>
        /// <param name="delta">The allowed tolerance for error. Should be small.</param>
        /// <returns>returns a string representation of the test.</returns>
        public string TestDelta(string label, float expected, float experimental, float delta)
        {
            string result;
            // value equality
            if (experimental + delta >= expected && experimental - delta <= expected)
            {
                passes++;
                result = "[PASS] " + GetLabel(label) + "\n";
                if (verbose == Verbosity.ALL) { result += GetComparisonText(expected, experimental); }
            }
            // test failure
            else
            {
                fails++;
                result = "[FAIL] " + GetLabel(label) + "\n";
                if (verbose == Verbosity.FAILS_ONLY) { result += GetComparisonText(expected, experimental); }
            }
            record += result;
            return result;
        }

        /// <summary>
        /// Saves a test that compares an <paramref name="expected"/> and <paramref name="experimental"/> value. The test result is tallied and saved.
        /// <br></br>Notice: For floats, use TestFloat() instead.
        /// 
        /// <br></br><b>Result Types:</b>
        /// <br></br><b>Identicals:</b> The expected/experimental parameters share the same reference. (This only works for reference types.)
        /// <br></br><b>Passes:</b> The expected/experimental parameters match.
        /// <br></br><b>Fails:</b> The expected/experimental parameters do *not* match.
        /// </summary>
        /// <param name="label">The label for the test.</param>
        /// <param name="expected">The expected value.</param>
        /// <param name="experimental">The value being tested.</param>
        /// <returns>returns a string representation of the test.</returns>
        public string Test(string label, object? expected, object? experimental)
        {
            return Test(label, expected, experimental, true);
        }

        /// <summary>
        /// Saves a test that compares an <paramref name="expected"/> and <paramref name="experimental"/> value. The test result is tallied and saved.
        /// <br></br>Notice: For floats, use TestFloat() instead.
        /// 
        /// <br></br><b>Result Types:</b>
        /// <br></br><b>Identicals:</b> The expected/experimental parameters share the same reference. (This only works for reference types.)
        /// <br></br><b>Passes:</b> The expected/experimental parameters match.
        /// <br></br><b>Fails:</b> The expected/experimental parameters do *not* match.
        /// </summary>
        /// <param name="label">The label for the test.</param>
        /// <param name="expected">The expected value.</param>
        /// <param name="experimental">The value being tested.</param>
        /// <param name="allowedToBeIdentical">Whether the expected and experimental are allowed to point to the same reference.</param>
        /// <returns>returns a string representation of the test.</returns>
        public string Test(string label, object? expected, object? experimental, bool allowedToBeIdentical)
        {
            string result;
            // disallowed identical reference
            if (object.ReferenceEquals(expected, experimental) && !allowedToBeIdentical)
            {
                fails++;
                result = "[IDEN = FAIL] " + GetLabel(label) + "\n";
                if (verbose == Verbosity.FAILS_ONLY || verbose == Verbosity.ALL) { result += GetComparisonText(expected, experimental); }
            }
            // identical reference
            if (object.ReferenceEquals(expected, experimental) && allowedToBeIdentical)
            {
                identicals++;
                result = "[IDEN] " + GetLabel(label) + "\n";
                if (verbose == Verbosity.ALL) { result += GetComparisonText(expected, experimental, "i| "); }
            }
            // value equality
            else if (object.Equals(expected, experimental)) 
            {
                passes++;
                result = "[PASS] " + GetLabel(label) + "\n";
                if (verbose == Verbosity.ALL) { result += GetComparisonText(expected, experimental, " | "); }
            }
            // test failure
            else
            {
                fails++;
                result = "[FAIL] " + GetLabel(label) + "\n";
                if (verbose == Verbosity.FAILS_ONLY || verbose == Verbosity.ALL) { result += GetComparisonText(expected, experimental); }
            }
            record += result;
            return result;
        }

        /// <summary>
        /// Returns the full test record.
        /// </summary>
        /// <returns>The full test record for this battery.</returns>
        public string GetRecord()
        {
            return record;
        }

        /// <summary>
        /// Returns the summary of the tests results.
        /// <br></br><b>Result Types:</b>
        /// <br></br><b>Identicals:</b> The expected/experimental parameters share the same reference. (This only works for reference types.)
        /// <br></br><b>Passes:</b> The expected/experimental parameters match.
        /// <br></br><b>Fails:</b> The expected/experimental parameters do *not* match.
        /// </summary>
        /// <returns>the test summary.</returns>
        public string GetSummary()
        {
            string summary = string.Empty;
            if (identicals > 0) {
                summary += "Identicals: " + identicals + "\n";
            }
            summary += "Passes: " + passes + "\n";
            summary += "Fails: " + fails + "\n";
            return summary;
        }

        private string GetLabel(string label)
        {
            return "(" + abbr + ") " + label;
        }

        private string GetComparisonText(object? expected, object? experimental, string indenter="-> ")
        {
            string s = indenter + (expected!=null ? expected.GetType() : "null") + " = " + (expected!=null ? expected.ToString() : "null") + "\n";
            s += indenter + (experimental!=null ? experimental.GetType() : "null") + " = " + (experimental!=null ? experimental.ToString() : "null") + "\n";
            return s;
        }
    }
}


