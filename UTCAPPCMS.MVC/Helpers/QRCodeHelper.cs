using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UTCAPPCMS.DAL.Models;
using UTCAPPCMS.DAL.Repository.Interfaces;
using UTCAPPCMS.DAL.ViewModel;

namespace UTCAPPCMS.MVC.Helpers
{
    public class QRCodeHelper
    {
        private readonly IUnitOfWork<TableTariff> _unitOfWorkTariff;

        public QRCodeHelper(IUnitOfWork<TableTariff> _unitOfWorkTariff)
        {
            this._unitOfWorkTariff = _unitOfWorkTariff;
        }

        public CalculateDuartion calculateduartion (DateTime? timein ,int siteId)
        {
            try
            {
                var timeout = DateTime.Now;

                if (timein.ToString() != "" && timeout.ToString() != "")
                {
                    TimeSpan span = (timeout - timein.Value);
                    int duration = span.Hours;
                    string durationval = "";

                    if ((span.Seconds > 0) || (span.Minutes > 0) || (span.Minutes == 0 && span.Hours == 0))
                    {
                        duration = duration + 1;
                    }

                    if (span.Days > 0)
                    {
                        duration = duration + (span.Days * 24);
                    }

                    durationval = (span.Hours + (span.Days * 24)) + ":" + span.Minutes + ":" + span.Seconds;

                    double totalprice = 0;

                    var tariff = _unitOfWorkTariff.Repository.where(x => x.ParkingLocationId == siteId).ToList();

                    for (int x = 0; x < duration; x++)
                    {
                        if (x <= tariff.Count - 1)
                        {
                            totalprice += Convert.ToDouble(tariff[x].HourPrice);
                        }
                        else
                        {
                            totalprice += Convert.ToDouble(tariff[tariff.Count() - 1].HourPrice);
                        }
                    }

                    return new CalculateDuartion() { Duration = durationval, Price = totalprice.ToString() };
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}