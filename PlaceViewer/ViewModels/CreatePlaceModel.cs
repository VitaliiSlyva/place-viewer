using System.ComponentModel.DataAnnotations;

namespace PlaceViewer.ViewModels;

public class CreatePlaceModel
{
    [Required]
    [Display(Name = "Place Name")]
    public string Name { get; set; }

    [Required]
    [Display(Name = "Place Image")]
    public IFormFile Image { get; set; }
}