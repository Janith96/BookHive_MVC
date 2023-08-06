using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookHive.Models
{
    public class Category
    {
        [Key] //not required here since by default the item with modelnameID(CategoryID) or Id will be taken as the primary key.
        public int Id { get; set; }
        
        [Required]
        [MaxLength(30)]
        [DisplayName("Category Name")]
        public string? Name { get; set; }
        
        [DisplayName("Display Order")]
        [Range(1,50,ErrorMessage ="Display order must be between 1-50")]
        public int DisplayOrder { get; set; }
    }
}
