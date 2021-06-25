﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.ViewModel;
using VehicleTracker.ViewModel.Common;

namespace VehicleTracker.Business.Interfaces
{
    public interface ICustomerService
    {
        List<CustomerViewModel> GetAllCustomers();
        CustomerViewModel GetCustomerById(long id);
        Task<ResponseViewModel> AddNewCustomer(CustomerViewModel vm, string userName);
        Task<ResponseViewModel> UpdateCustomer(CustomerViewModel vm, string userName);
        Task<ResponseViewModel> DeleteCustomer(int id, string userName);
    }
}
