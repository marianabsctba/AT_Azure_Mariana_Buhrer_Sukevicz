using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Model.Models
{
    public class Donation
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Nome da doação")]
        public string DonationName { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Descrição")]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Data de Registro")]
        public DateTime DateOfRegister { get; set; }

        [Required]
        [Display(Name = "Valor do frete")]
        public double Courier { get; set; }

        [Required]
        [Range(0, 100)]
        [Display(Name = "Quantidade")]
        public int Quantity { get; set; }

        [Required]
        [Display(Name = "O item é novo?")]
        public bool NewOrOld { get; set; }

        [Display(Name = "Imagem")]
        public string ImageUri { get; set; }

        [Display(Name = "Visualizações")]
        public int QuantityView { get; set; }


    }
}
