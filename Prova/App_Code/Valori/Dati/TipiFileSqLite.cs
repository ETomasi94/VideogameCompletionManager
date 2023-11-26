using System.ComponentModel;

enum TipiFileSqlite
{
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