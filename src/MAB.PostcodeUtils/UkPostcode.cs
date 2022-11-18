using System.Linq;
using System.Text.RegularExpressions;

namespace MAB.PostcodeUtils
{
    /// <summary>
    /// Represents a UK Postcode.
    /// </summary>
    public struct UkPostcode
    {
        private const int _inwardLength = 3;
        private const int _minimumLength = 5;

        private static readonly Regex _spaces = new Regex(@"\s+", RegexOptions.Compiled);

        /// <summary>
        /// Creates a new UkPostcode instance.
        /// </summary>
        /// <param name="outward">The outward postcode.</param>
        /// <param name="area">The outward postcode area.</param>
        /// <param name="district">The outward postcode district.</param>
        /// <param name="inward">The inward postcode.</param>
        /// <param name="sector">The inward postcode sector.</param>
        /// <param name="unit">The inward postcode unit.</param>
        public UkPostcode(
            string outward,
            string area,
            string district,
            string inward,
            string sector,
            string unit)
        {
            Outward = outward;
            Area = area;
            District = district;

            Inward = inward;
            Sector = sector;
            Unit = unit;

            Formatted = Outward + " " + Inward;
        }

        /// <summary>
        /// The full postcode, formatted with a space.
        /// </summary>
        public string Formatted { get; }

        /// <summary>
        /// The outward postcode.
        /// </summary>
        public string Outward { get; }

        /// <summary>
        /// The outward postcode area.
        /// </summary>
        public string Area { get; }

        /// <summary>
        /// The outward postcode district.
        /// </summary>
        public string District { get; }

        /// <summary>
        /// The inward postcode.
        /// </summary>
        public string Inward { get; }

        /// <summary>
        /// The inward postcode sector.
        /// </summary>
        public string Sector { get; }

        /// <summary>
        /// The inward postcode unit.
        /// </summary>
        public string Unit { get; }

        /// <summary>
        /// Converts the string representation of a UK postcode to a <see cref="UkPostcode"/> value.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="postcode"></param>
        /// <returns></returns>
        public static bool TryParse(string value, out UkPostcode postcode)
        {
            postcode = default;

            if (string.IsNullOrWhiteSpace(value))
            {
                return false;
            }

            var s = _spaces
                .Replace(value, string.Empty)
                .ToUpperInvariant()
                .Trim();

            if (s.Length < _minimumLength)
            {
                return false;
            }

            if (char.IsDigit(s[0]))
            {
                return false;
            }

            var outward = s.Substring(0, s.Length - _inwardLength);
            var inward = s.Substring(s.Length - _inwardLength);

            if (!char.IsDigit(inward[0]))
            {
                return false;
            }

            var area = new string(outward.TakeWhile(c => char.IsLetter(c)).ToArray());
            var district = outward.Substring(area.Length);

            var sector = inward[0].ToString();
            var unit = inward.Substring(1);

            postcode = new UkPostcode(outward, area, district, inward, sector, unit);

            return true;
        }
    }
}
