using AgendaManagement.Domain.Data;
using Application.Core.Utils;
using Application.Dto.Citas;
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
        const int maximumAppointmentDuration = 90;

        var displayNameDay = DisplayNameDay.GetEnumDisplayName(request.Day);

        try
        {
            TimeSpan initialtime = TimeSpan.FromHours(9);
            TimeSpan finalTime = TimeSpan.FromHours(17);

            var requestDay = _agendaManagementRepository.GetCitaByDay()
                .Where(x => x.Day == displayNameDay
            && TimeSpan.Parse(x.Hour) <= finalTime
            && TimeSpan.Parse(x.Hour) >= initialtime);

            var lastHour = requestDay.LastOrDefault();
            var firstHour = requestDay.FirstOrDefault();

            var totalAppointment = requestDay.Count();

            var count = 0;

            for (int i = 0; i < requestDay.Count(); i++)
            {
                CitaDto currentAppointment = requestDay.ElementAt(i);

                if (i < requestDay.Count() - 1)
                {
                    CitaDto nextAppointment = requestDay.ElementAt(i + 1);

                    var timeUntilNextTime = TimeSpan.Parse(nextAppointment.Hour) - TimeSpan.Parse(currentAppointment.Hour);

                    var timeUntilNextTimeInMinutes = timeUntilNextTime.TotalMinutes;

                    var subtractionMinutesBetweenHours = timeUntilNextTimeInMinutes - int.Parse(currentAppointment.Duration);

                    if (subtractionMinutesBetweenHours >= minimumAppointmentDuration && subtractionMinutesBetweenHours < maximumAppointmentDuration
                        || subtractionMinutesBetweenHours >= maximumAppointmentDuration)
                    {
                        spaceAvailable += Math.Truncate(subtractionMinutesBetweenHours / minimumAppointmentDuration);
                    }
                }

                count++;

                if (count == totalAppointment)
                {
                    var lastMinuteDifference = finalTime - TimeSpan.Parse(lastHour.Hour);

                    var durationInMinutesLastHour = TimeSpan.FromMinutes(int.Parse(lastHour.Duration));

                    var resultMinutesLastHour = lastMinuteDifference.Subtract(durationInMinutesLastHour);

                    var convertToMinutesMinutesLastHour = resultMinutesLastHour.TotalMinutes;

                    if (convertToMinutesMinutesLastHour >= minimumAppointmentDuration)
                    {
                        spaceAvailable += Math.Truncate(convertToMinutesMinutesLastHour / minimumAppointmentDuration);
                    }

                    var differenceBetweenTheFirstHours = TimeSpan.Parse(firstHour.Hour) - initialtime;

                    var convertToMinutesMinutesInitialHour = differenceBetweenTheFirstHours.TotalMinutes;

                    if (convertToMinutesMinutesInitialHour >= minimumAppointmentDuration)
                    {
                        spaceAvailable += Math.Truncate(convertToMinutesMinutesInitialHour / minimumAppointmentDuration);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            return $"Error: {ex.Message}";
        }

        return $"La cantidad de espacios disponibles para acceder a una cita el día '{displayNameDay}' son: {spaceAvailable}";
    }
}