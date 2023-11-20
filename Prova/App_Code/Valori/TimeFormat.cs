using System.ComponentModel;

enum TimeFormat
{
    [Description("YEAR")]
    yyyy,

    [Description("MONTH")]
    MM,

    [Description("DAY")]
    dd,

    [Description("WEEKDAY")]
    dddd,

    [Description("HOUR")]
    HH,

    [Description("MINUTES")]
    mm,

    [Description("SECONDS")]
    ss,

    [Description("AM|PM")]
    tt
}