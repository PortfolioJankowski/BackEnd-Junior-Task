using Task.Enums;
using Task.Interfaces;
using Task.Models;
using Task.Services.DateConverter;

namespace Task.Services.Participants
{
    public class ParticipantMapper : IParticipantMapper
    {
        public (Participant? participant, string? Error) TryPopulateParticipantFromLine(string[] properties)
        {
            if (properties.Length == 0)
                return (null, "No properties found in the input.");

            try
            {
                var participant = new Participant()
                {
                    Id = ParseInt(properties[(int)ParticipantCsvReportHeaders.Id]),
                    Age = ParseInt(properties[(int)ParticipantCsvReportHeaders.Age]),
                    Name = ParseString(properties[(int)ParticipantCsvReportHeaders.Name]),
                    Email = ParseString(properties[(int)ParticipantCsvReportHeaders.Email]),
                    WorkStart = ZuluDateConverter.DateOffsetToDateTime(properties[(int)ParticipantCsvReportHeaders.WorkStart]),
                    WorkEnd = ZuluDateConverter.DateOffsetToDateTime(properties[(int)ParticipantCsvReportHeaders.WorkEnd])
                };
                return (participant, null);
            }
            catch (Exception ex)
            {
                return (null, $"Error parsing participantL: {ex.Message}");
            }
        }

        private int ParseInt(string value)
        {
            if (int.TryParse(value.Trim(), out int result))
                return result;

            throw new ArgumentException($"Invalid integer value: {value}");
        }
        private string ParseString(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("String value cannot be null or empty.");

            return value.Trim();
        }

        private DateTime ParseDateTime(string value)
        {
            if (value.Trim().EndsWith("Z"))
            {
                value = value.Trim().Replace("+00Z", "");
            }

            if (DateTimeOffset.TryParse(value.Trim(), out DateTimeOffset parsedDate))
                return parsedDate.DateTime;

            throw new ArgumentException($"Invalid DateTime value: {value}");
        }
    }
}
