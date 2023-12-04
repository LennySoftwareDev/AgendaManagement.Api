using AgendaManagement.Domain.Data;
using Application.Core.Utils;
using Application.Dto.Citas.Queries;
using MediatR;

namespace Syp.Pi.Aplication.Core.Archivos.Queries;

public class GetCitaByDay : IRequestHandler<GetCitaByDayRequestDto, string>
{
    private readonly AgendaManagementRepository _agendaManagementRepository;
    public GetCitaByDay(AgendaManagementRepository agendaManagementRepository)
    {
        _agendaManagementRepository = agendaManagementRepository;
    }
    public async Task<string> Handle(GetCitaByDayRequestDto request, CancellationToken cancellationToken)
    {
        double spaceAvailable = 0;

        const int minimumAppointmentDuration = 30;

        var displayNameDay = DisplayNameDay.GetEnumDisplayName(request.Day);

        try
        {
            var requestDay = _agendaManagementRepository.GetCitaByDay().Where(x => x.Day == displayNameDay);

            var appointmentDuration = 0;

            int totalMinutesBetweenHours = 0;

            TimeSpan initialtime = TimeSpan.FromHours(9);
            TimeSpan finalTime = TimeSpan.FromHours(17);

            double differenceMinutesBetweenHours = (finalTime - initialtime).TotalMinutes;

            foreach (var day in requestDay.ToList())
            {
                appointmentDuration += int.Parse(day.Duration);

                TimeSpan time = TimeSpan.Parse(day.Hour);

                if (time > finalTime)
                {
                    break;
                }

                time = time < initialtime ? initialtime : time;
                time = time > finalTime ? finalTime : time;

                totalMinutesBetweenHours = (int)(time - initialtime).TotalMinutes;
            }

            if (totalMinutesBetweenHours < differenceMinutesBetweenHours)
            {
                double totalMinutes = differenceMinutesBetweenHours - totalMinutesBetweenHours;
                spaceAvailable = ((totalMinutesBetweenHours + totalMinutes) - appointmentDuration) / minimumAppointmentDuration;
            }
        }
        catch (Exception ex)
        {
            return $"Error: {ex.Message}";
        }

        return $"La cantidad de espacios disponibles para acceder a una cita el día '{displayNameDay}' son: {Math.Truncate(spaceAvailable)}";
    }
}