using Application.Dto.Citas;

namespace Application.Core.Utils
{
    public static class ManagementOfAvailableHour
    {
        const int minimumAppointmentDuration = 30;
        const int maximumAppointmentDuration = 90;
        public static int getSpaceAvailableBetweenHours(CitaDto currentAppointment, CitaDto nextAppointment)
        {
            double result = 0;

            var timeUntilNextTime = TimeSpan.Parse(nextAppointment.Hour) - TimeSpan.Parse(currentAppointment.Hour);

            var timeUntilNextTimeInMinutes = timeUntilNextTime.TotalMinutes;

            var subtractionMinutesBetweenHours = timeUntilNextTimeInMinutes - int.Parse(currentAppointment.Duration);

            if (subtractionMinutesBetweenHours >= minimumAppointmentDuration && subtractionMinutesBetweenHours < maximumAppointmentDuration
                || subtractionMinutesBetweenHours >= maximumAppointmentDuration)
            {
                result = Math.Truncate(subtractionMinutesBetweenHours / minimumAppointmentDuration);
            }

            return (int)result;
        }

        public static int getSpaceAvailableBetweenFinalHours(TimeSpan finalTime, CitaDto cita)
        {
            double result = 0;

            var lastMinuteDifference = finalTime - TimeSpan.Parse(cita.Hour);

            var durationInMinutesLastHour = TimeSpan.FromMinutes(int.Parse(cita.Duration));

            var resultMinutesLastHour = lastMinuteDifference.Subtract(durationInMinutesLastHour);

            var convertToMinutesMinutesLastHour = resultMinutesLastHour.TotalMinutes;

            if (convertToMinutesMinutesLastHour >= minimumAppointmentDuration)
            {
                result = Math.Truncate(convertToMinutesMinutesLastHour / minimumAppointmentDuration);
            }

            return (int)result;
        }

        public static int getSpaceAvailableBetweenInitialHours(TimeSpan initialTime, CitaDto cita)
        {
            double result = 0;

            var differenceBetweenTheFirstHours = TimeSpan.Parse(cita.Hour) - initialTime;

            var convertToMinutesMinutesInitialHour = differenceBetweenTheFirstHours.TotalMinutes;

            if (convertToMinutesMinutesInitialHour >= minimumAppointmentDuration)
            {
                result = Math.Truncate(convertToMinutesMinutesInitialHour / minimumAppointmentDuration);
            }
            return (int)result;
        }
    }
}
