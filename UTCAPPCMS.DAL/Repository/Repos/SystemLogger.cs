using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UTCAPPCMS.DAL.Models;
using UTCAPPCMS.DAL.Repository.Interfaces;

namespace UTCAPPCMS.DAL.Repository.Repos
{
    public class SystemLogger : ISystemLogger
    {
        private readonly IUnitOfWork<AuditTrialid> _Logging;
        public SystemLogger(IUnitOfWork<AuditTrialid> _Logging)
        {
            this._Logging = _Logging;
        }

        public async Task TraceLogsAsync(int _sessionUserId, string _ControllerName, string _ActionName, string _Description, string _Remark)
        {
            await _Logging.Repository.Add(new AuditTrialid
            {
                CreatedDate = DateTime.Now,
                CreatedUserId = _sessionUserId,
                ModifiedUserId = 0,
                LastModifiedDate = DateTime.Now,
                IsEnable = true,
                IsDeleted = false,

                ControllerName = _ControllerName,
                ActionName = _ActionName,
                Description = _Description,
                Remark = _Remark
            });
            await _Logging.Commit();
        }
    }
}
