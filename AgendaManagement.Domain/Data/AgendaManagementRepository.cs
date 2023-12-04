using Application.Dto.Citas;

namespace AgendaManagement.Domain.Data
{
    public interface AgendaManagementRepository
    {
        List<CitaDto> GetCitaByDay();
    }
}
