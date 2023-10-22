using System.ComponentModel.DataAnnotations.Schema;

namespace SqlServerDemo.Entities
{
    [Table("SystemObjects", Schema = "dbo")]
    public class SystemObjects
    {

        /// <summary>
        /// Id
        /// </summary>
        public decimal Id { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// Version
        /// </summary>
        public string? Version { get; set; }
    }
}
