using GaHipHop_Repository.Repository;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Quartz
{
    public class QuartzJob : IJob
    {
        private readonly IUnitOfWork _unitOfWork;

        public QuartzJob(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task Execute(IJobExecutionContext context)
        {
            try
            {
                var currentTime = DateTime.Now;
                var discounts = _unitOfWork.DiscountRepository
                    .Get(c => c.Status)
                    .ToList();

                foreach (var discount in discounts)
                {
                    var endTimeWithExpiredDate = discount.ExpiredDate;
                    if (endTimeWithExpiredDate <= currentTime)
                    {
                        discount.Status = false;
                        _unitOfWork.DiscountRepository.Update(discount);
                    }
                }

                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            return Task.CompletedTask;
        }
    
    }
}
