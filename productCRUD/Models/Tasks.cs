using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace productCRUD.Models
{
    [Table("tasks")]
    public class Task : BaseModel
    {
        [PrimaryKey("id", false)]
        public int Id { get; set; }

        [Column("title")]
        public string Title { get; set; }

        [Column("is_completed")]
        public bool IsCompleted { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
    }
}
