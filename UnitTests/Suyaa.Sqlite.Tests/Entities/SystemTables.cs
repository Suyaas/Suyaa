using System.ComponentModel.DataAnnotations.Schema;

namespace SqlServerDemo.Entities
{
    [Table("SystemTables", Schema = "dbo")]
    public class SystemTables
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
        /// 显示名称
        /// </summary>
        public string Text { get; set; } = string.Empty;
        /// <summary>
        /// 对象Id
        /// </summary>
        public decimal ObjectID { get; set; }
    }
}
