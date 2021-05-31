using System.ComponentModel.DataAnnotations;

namespace word_count_app.common.Models
{
    public class RequestWords
    {
        [Required(ErrorMessage = "El texto es obligatorio")]
        public string OriginalText { get; set; }
        public string Filter { get; set; }
    }
}
