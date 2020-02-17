using System;

namespace TMS.Services
{
    public abstract class HandlerCore
    {
        protected readonly ITMSRepository repository;
        protected HandlerCore(ITMSRepository repository)
        {
            this.repository = repository;
        }
    }
}