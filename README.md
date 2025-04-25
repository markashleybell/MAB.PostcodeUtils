# MAB.PostcodeUtils

Utilities for working with postal codes (with a focus on the UK).

## Specification

[UK Postcode Specification (PDF)](docs/ILRSpecification2013_14Appendix_C_Dec2012_v1.pdf)

<table style="text-align: center;">
<tbody><tr>
<th style="background:#87cefa; color: black; border: solid 1px #000;" colspan="4">POSTCODE
</th></tr>
<tr style="background:#ab6d00; color: white;">
<td colspan="2" style="border: solid 1px #000;">Outward code
</td>
<td colspan="2" style="border: solid 1px #000;">Inward code
</td></tr>
<tr style="background:#1f7242; color: white;">
<td style="border: solid 1px #000;">Area</td>
<td style="border: solid 1px #000;">District</td>
<td style="border: solid 1px #000;">Sector</td>
<td style="border: solid 1px #000;">Unit
</td></tr>
<tr style="background:#655edf; color: white;">
<td style="border: solid 1px #000;">SW</td>
<td style="border: solid 1px #000;">1W</td>
<td style="border: solid 1px #000;">0</td>
<td style="border: solid 1px #000;">NY
</td></tr></tbody></table>

## Example Usage

```csharp
if (UkPostcode.TryParse("  T q95 HA ", out var postcode))
{
    /*
    postcode.Formatted  ->  "TQ9 5HA";

    postcode.Outward    ->  "TQ9"

    postcode.Area       ->  "TQ"
    postcode.District   ->  "9"

    postcode.Inward     ->  "5HA"

    postcode.Sector     ->  "5"
    postcode.Unit       ->  "HA"
    */
}
```

## Useful Links

### General reference

https://en.wikipedia.org/wiki/Postcodes_in_the_United_Kingdom

### Areas not included in Code Point Open datasets

https://en.wikipedia.org/wiki/BT_postcode_area
https://en.wikipedia.org/wiki/GY_postcode_area
https://en.wikipedia.org/wiki/IM_postcode_area
https://en.wikipedia.org/wiki/JE_postcode_area

### Postcode Datasets

https://geoportal.statistics.gov.uk/datasets/ons::ons-postcode-directory-may-2022-1/about
https://osdatahub.os.uk/downloads/open/CodePointOpen

## Attribution

Package icon: https://www.iconfinder.com/icons/8684894/options_setting_email_mail_envelope_message_letter_icon
