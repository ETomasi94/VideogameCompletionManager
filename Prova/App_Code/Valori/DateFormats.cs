using System.ComponentModel;

namespace GameCompletionManager
{
    public enum DateFormats
    {
        [Description("Data generica")]
        D,

        [Description("Data estesa")]
        T,

        [Description("Data in cifre")]
        d,

        [Description("Data corta")]
        t
    }
}