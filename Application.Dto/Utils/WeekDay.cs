using System.ComponentModel.DataAnnotations;

namespace Application.Dto.Utils;

public enum WeekDay
{
    [Display(Name = "lunes")]
    Lunes,

    [Display(Name = "martes")]
    Martes,

    [Display(Name = "miércoles")]
    Miércoles,

    [Display(Name = "jueves")]
    Jueves,

    [Display(Name = "viernes")]
    Viernes
}
