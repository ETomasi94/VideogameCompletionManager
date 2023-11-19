using System.ComponentModel;

enum TipiFile
{
    [Description(".accdb")]
    ACCDB,

    [Description(".mdb")]
    MDB,

    [Description(".xls")]
    XLS,

    [Description(".xlsx")]
    XLSX,

    [Description(".sqlite")]
    SQLITE,

    [Description(".sqlite3")]
    SQLITE3,

    [Description(".db")]
    DB,

    [Description(".db3")]
    DB3,

    [Description(".s3db")]
    S3DB,

    [Description(".sl3")]
    SL3
}