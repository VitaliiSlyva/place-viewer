using System.ComponentModel.DataAnnotations;

namespace PlaceViewer.ViewModels;

public class SearchPlaceModel
{
    [Required]
    [Display(Name = "Place Name")]
    public string Name { get; set; }
}