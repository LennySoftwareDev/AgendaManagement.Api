using Application.Dto.Utils;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Dto.Citas.Queries;

public class GetCitaByDayRequestDto : IRequest<string>
{
    [EnumDataType(typeof(WeekDay))]
    [Required]
    public WeekDay Day { get; set; }
}
