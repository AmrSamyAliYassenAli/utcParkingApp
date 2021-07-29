using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace UTCAPPCMS.DAL.Repository.Interfaces
{
    public interface ISystemLogger
    {
        Task TraceLogsAsync(int _sessionUserId, string _ControllerName, string _ActionName, string _Description, string _Remark);
    }
}
