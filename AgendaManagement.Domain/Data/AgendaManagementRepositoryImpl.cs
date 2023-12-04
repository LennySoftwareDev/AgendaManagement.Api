using Application.Dto.Citas;
using Newtonsoft.Json;

namespace AgendaManagement.Domain.Data
{
    public class AgendaManagementRepositoryImpl : AgendaManagementRepository
    {
        public List<CitaDto> GetCitaByDay()
        {
            string dataJson = string.Empty;

            try
            {
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "agendamanagement.json");

                dataJson = File.ReadAllText(filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al leer el archivo: {ex.Message}");
            }

            return JsonConvert.DeserializeObject<List<CitaDto>>(dataJson);
        }
    }
}
