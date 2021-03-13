using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Text;
using VehicleTracker.Data;
using System.Linq;
using System.Threading.Tasks;
using VehicleTracker.ViewModel.Common;
using VehicleTracker.Model.Enums;

namespace VehicleTracker.Business.Factories
{
    public class MainCodeManager  
    {
        public MainCodeManager(VMDBContext dbContext, Codes selectedCodeType)
        {
            SelectedCodeType = selectedCodeType;
            DbContext = dbContext;
        }


        protected VMDBContext DbContext { get; private set; }
        public Codes SelectedCodeType { get; private set; }



        public virtual async Task<ResponseViewModel> Save(CodeViewModel code)
        {
            throw new NotImplementedException("Method has not implemented");
        }

        public virtual async Task<ResponseViewModel> Delete(int id)
        {
            throw new NotImplementedException("Method has not implemented");
        }

        public virtual async Task<ResponseViewModel> Delete(CodeViewModel code)
        {
            throw new NotImplementedException("Method has not implemented");
        }

        public virtual List<CodeViewModel> GetAll()
        {
            throw new NotImplementedException("Method has not implemented");
        }

        public virtual async Task<CodeViewModel> GetById(int id)
        {
            throw new NotImplementedException("Method has not implemented");
        }


    }



}
