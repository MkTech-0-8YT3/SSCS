using System.ComponentModel.DataAnnotations;

namespace Common.DataModels.DtoModels
{
    public class CardNumber
    {
        public CardNumber(string cardIdNumber)
        {
            CardIdNumber = cardIdNumber;
        }
        [Required]
        public string CardIdNumber { get; set; }
    }
}
