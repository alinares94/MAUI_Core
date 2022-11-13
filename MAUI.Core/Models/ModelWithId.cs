using SQLite;

namespace MAUI.Core.Models;
public class ModelWithId
{
    [PrimaryKey, AutoIncrement]
    public int Id {get; set;}
}
