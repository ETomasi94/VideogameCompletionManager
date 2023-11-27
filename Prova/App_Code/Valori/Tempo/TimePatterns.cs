using System.ComponentModel;

public enum TimePatterns
{
    [Description("yyyy-MM-dd HH:mm:ss")]
    TIMESTAMP_COMPLETE,

    [Description("yyyy/MM/dd HH:mm:ss")]
    TIMESTAMP_COMPLETE_S,

    [Description("dd/MM/yyyy HH:mm:ss")]
    EUROPEAN_TIMESTAMP_S,

    [Description("dd MMMM yyyy HH:mm:ss")]
    EUROPEAN_TIMESTAMP,

    [Description("dddd, dd MMMM yyyy HH:mm:ss")]
    EUROPEAN_TIMESTAMP_CW,

    [Description("dddd dd MMMM yyyy HH:mm:ss")]
    EUROPEAN_TIMESTAMP_W,

    [Description("dddd dd/MM/yyyy HH:mm:ss")]
    EUROPEAN_TIMESTAMP_WS,

    [Description("dddd, dd/MM/yyyy HH:mm:ss")]
    EUROPEAN_TIMESTAMP_CWS,

    [Description("dd/MM/yyyy HH:mm")]
    EUROPEAN_DATETIME_S,

    [Description("dd MMMM yyyy HH:mm")]
    EUROPEAN_DATETIME,

    [Description("dddd dd MMMM yyyy HH:mm")]
    EUROPEAN_DATETIME_W,

    [Description("dddd, dd MMMM yyyy HH:mm")]
    EUROPEAN_DATETIME_CW,

    [Description("dddd dd/MM/yyyy HH:mm")]
    EUROPEAN_DATETIME_WS,

    [Description("dddd, dd/MM/yyyy HH:mm")]
    EUROPEAN_DATETIME_CWS,

    [Description("dd/MM/yyyy")]
    EUROPEAN_DATE_S,

    [Description("dd MMMM yyyy")]
    EUROPEAN_DATE,

    [Description("dddd dd MMMM yyyy")]
    EUROPEAN_DATE_W,

    [Description("dddd, dd MMMM yyyy")]
    EUROPEAN_DATE_CW,

    [Description("dddd dd/MM/yyyy")]
    EUROPEAN_DATE_WS,

    [Description("dddd, dd/MM/yyyy")]
    EUROPEAN_DATE_CWS,

    [Description("MM/dd/yyyy")]
    AMERICAN_DATE_S,

    [Description("MM/dd/yyyy HH:mm")]
    AMERICAN_DATETIME_S,

    [Description("MM/dd/yyyy HH:mm:ss")]
    AMERICAN_TIMESTAMP_S,

    [Description("MMMM dd yyyy")]
    AMERICAN_DATE,

    [Description("MMMM dd yyyy HH:mm")]
    AMERICAN_DATETIME,

    [Description("MMMM dd yyyy HH:mm:ss")]
    AMERICAN_TIMESTAMP,

    [Description("dddd MMMM dd yyyy")]
    AMERICAN_DATE_W,

    [Description("dddd, MMMM dd yyyy")]
    AMERICAN_DATE_CW,

    [Description("dddd, MM/dd/yyyy")]
    AMERICAN_DATE_CSW,

    [Description("dddd MM/dd/yyyy")]
    AMERICAN_DATE_WS,

    [Description("dddd, MM/dd/yyyy")]
    AMERICAN_DATE_CWS,

    [Description("dddd MMMM dd yyyy HH:mm")]
    AMERICAN_DATETIME_W,

    [Description("dddd, MMMM dd yyyy HH:mm")]
    AMERICAN_DATETIME_CW,

    [Description("dddd MM/dd/yyyy HH:mm")]
    AMERICAN_DATETIME_WS,

    [Description("dddd, MM/dd/yyyy HH:mm")]
    AMERICAN_DATETIME_CWS,

    [Description("dddd MMMM dd yyyy HH:mm:ss")]
    AMERICAN_TIMESTAMP_W,

    [Description("dddd, MMMM dd yyyy HH:mm:ss")]
    AMERICAN_TIMESTAMP_CW,

    [Description("dddd MM/dd/yyyy HH:mm:ss")]
    AMERICAN_TIMESTAMP_WS,

    [Description("dddd, MM/dd/yyyy HH:mm:ss")]
    AMERICAN_TIMESTAMP_CWS,
}