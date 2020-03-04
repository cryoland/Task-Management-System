using System;
using TMS.Application.Common.Interfaces;

namespace TMS.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
