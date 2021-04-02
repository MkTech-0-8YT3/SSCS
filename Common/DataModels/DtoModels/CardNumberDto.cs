using System.ComponentModel.DataAnnotations;

namespace Common.DataModels.DtoModels
{
    public class CardNumberDto
    {
        public CardNumberDto(string cardIdNumber)
        {
            CardIdNumber = cardIdNumber;
        }
        [Required]
        public string CardIdNumber { get; set; }
    }
}
