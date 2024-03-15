using System.ComponentModel.DataAnnotations;

namespace WebApp1.Models
{
    public interface ICipher
    {
        string Encrypted { get; set; }

        int Id { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        string Text { get; set; }

        string Encrypt();
    }

    public enum CipherType
    {
        AtoN = 0,
        ACEDB = 1
    }
}