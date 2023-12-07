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

        var displayNameDay = DisplayNameDay.GetEnumDisplayName(request.Day);

        try
        {
            TimeSpan initialTime = TimeSpan.FromHours(9);
            TimeSpan finalTime = TimeSpan.FromHours(17);

            var requestDay = _agendaManagementRepository.GetCitaByDay()
                .Where(x => x.Day == displayNameDay
            && TimeSpan.Parse(x.Hour) <= finalTime
            && TimeSpan.Parse(x.Hour) >= initialTime);

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

                    spaceAvailable += ManagementOfAvailableHour.getSpaceAvailableBetweenHours(currentAppointment, nextAppointment);
                }

                count++;

                if (count == totalAppointment)
                {
                    var resultBetweenFinalHours = ManagementOfAvailableHour.getSpaceAvailableBetweenFinalHours(finalTime, lastHour);
                    var resultBetweenInitialHours = ManagementOfAvailableHour.getSpaceAvailableBetweenInitialHours(initialTime, firstHour);

                    spaceAvailable += resultBetweenInitialHours + resultBetweenFinalHours;
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