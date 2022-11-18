using NUnit.Framework;

namespace MAB.PostcodeUtils.Test
{
    [TestFixture]
    public class UkPostcodeTests
    {
        [Test]
        public void Parse_General()
        {
            var validPatterns = new[] {
                "AA9A 9AA",
                "A9A 9AA",
                "A9 9AA",
                "A99 9AA",
                "AA9 9AA",
                "AA99 9AA",
            };

            foreach (var pattern in validPatterns)
            {
                var result = UkPostcode.TryParse(pattern, out var postcode);

                Assert.Multiple(() => {
                    Assert.That(result, Is.EqualTo(true));
                    Assert.That(postcode.Formatted, Is.EqualTo(pattern));
                });
            }
        }

        [Test]
        public void Parse_BadInput()
        {
            var validPatterns = new[] {
                null,
                string.Empty,
                "            ",
                "9A9A 9AA",
                "A9A AAA",
                "A9 9A",
                "A9999 9AAAA",
            };

            foreach (var pattern in validPatterns)
            {
                var result = UkPostcode.TryParse(pattern, out var _);

                Assert.That(result, Is.EqualTo(false));
            }
        }

        [Test]
        public void Parse_OneCharArea_OneDigitDistrict()
        {
            var result = UkPostcode.TryParse("A12BC", out var postcode);

            Assert.Multiple(() => {
                Assert.That(result, Is.EqualTo(true));
                Assert.That(postcode.Formatted, Is.EqualTo("A1 2BC"));
                Assert.That(postcode.Outward, Is.EqualTo("A1"));
                Assert.That(postcode.Area, Is.EqualTo("A"));
                Assert.That(postcode.District, Is.EqualTo("1"));
                Assert.That(postcode.Inward, Is.EqualTo("2BC"));
                Assert.That(postcode.Sector, Is.EqualTo("2"));
                Assert.That(postcode.Unit, Is.EqualTo("BC"));
            });
        }

        [Test]
        public void Parse_OneCharArea_TwoDigitDistrict()
        {
            var result = UkPostcode.TryParse("A123BC", out var postcode);

            Assert.Multiple(() => {
                Assert.That(result, Is.EqualTo(true));
                Assert.That(postcode.Formatted, Is.EqualTo("A12 3BC"));
                Assert.That(postcode.Outward, Is.EqualTo("A12"));
                Assert.That(postcode.Area, Is.EqualTo("A"));
                Assert.That(postcode.District, Is.EqualTo("12"));
                Assert.That(postcode.Inward, Is.EqualTo("3BC"));
                Assert.That(postcode.Sector, Is.EqualTo("3"));
                Assert.That(postcode.Unit, Is.EqualTo("BC"));
            });
        }

        [Test]
        public void Parse_OneCharArea_SubDistrict()
        {
            var result = UkPostcode.TryParse("A1B2CD", out var postcode);

            Assert.Multiple(() => {
                Assert.That(result, Is.EqualTo(true));
                Assert.That(postcode.Formatted, Is.EqualTo("A1B 2CD"));
                Assert.That(postcode.Outward, Is.EqualTo("A1B"));
                Assert.That(postcode.Area, Is.EqualTo("A"));
                Assert.That(postcode.District, Is.EqualTo("1B"));
                Assert.That(postcode.Inward, Is.EqualTo("2CD"));
                Assert.That(postcode.Sector, Is.EqualTo("2"));
                Assert.That(postcode.Unit, Is.EqualTo("CD"));
            });
        }

        [Test]
        public void Parse_TwoCharArea_OneDigitDistrict()
        {
            var result = UkPostcode.TryParse("AB12CD", out var postcode);

            Assert.Multiple(() => {
                Assert.That(result, Is.EqualTo(true));
                Assert.That(postcode.Formatted, Is.EqualTo("AB1 2CD"));
                Assert.That(postcode.Outward, Is.EqualTo("AB1"));
                Assert.That(postcode.Area, Is.EqualTo("AB"));
                Assert.That(postcode.District, Is.EqualTo("1"));
                Assert.That(postcode.Inward, Is.EqualTo("2CD"));
                Assert.That(postcode.Sector, Is.EqualTo("2"));
                Assert.That(postcode.Unit, Is.EqualTo("CD"));
            });
        }

        [Test]
        public void Parse_TwoCharArea_TwoDigitDistrict()
        {
            var result = UkPostcode.TryParse("AB123CD", out var postcode);

            Assert.Multiple(() => {
                Assert.That(result, Is.EqualTo(true));
                Assert.That(postcode.Formatted, Is.EqualTo("AB12 3CD"));
                Assert.That(postcode.Outward, Is.EqualTo("AB12"));
                Assert.That(postcode.Area, Is.EqualTo("AB"));
                Assert.That(postcode.District, Is.EqualTo("12"));
                Assert.That(postcode.Inward, Is.EqualTo("3CD"));
                Assert.That(postcode.Sector, Is.EqualTo("3"));
                Assert.That(postcode.Unit, Is.EqualTo("CD"));
            });
        }

        [Test]
        public void Parse_TwoCharArea_SubDistrict()
        {
            var result = UkPostcode.TryParse("AB1C2DE", out var postcode);

            Assert.Multiple(() => {
                Assert.That(result, Is.EqualTo(true));
                Assert.That(postcode.Formatted, Is.EqualTo("AB1C 2DE"));
                Assert.That(postcode.Outward, Is.EqualTo("AB1C"));
                Assert.That(postcode.Area, Is.EqualTo("AB"));
                Assert.That(postcode.District, Is.EqualTo("1C"));
                Assert.That(postcode.Inward, Is.EqualTo("2DE"));
                Assert.That(postcode.Sector, Is.EqualTo("2"));
                Assert.That(postcode.Unit, Is.EqualTo("DE"));
            });
        }
    }
}
