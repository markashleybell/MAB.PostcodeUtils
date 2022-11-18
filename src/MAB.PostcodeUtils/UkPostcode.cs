using System.Linq;
using System.Text.RegularExpressions;

namespace MAB.PostcodeUtils
{
    public struct UkPostcode
    {
        private const int _inwardLength = 3;
        private const int _minimumLength = 5;

        private static readonly Regex _spaces = new Regex(@"\s+", RegexOptions.Compiled);

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

        public string Formatted { get; }

        public string Outward { get; }

        public string Area { get; }

        public string District { get; }

        public string Inward { get; }

        public string Sector { get; }

        public string Unit { get; }

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
