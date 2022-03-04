using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace VehicleTracker.ViewModel.Common.Enums
{
    public enum ExpenseCategoryTypes
    {
        [Description("Electricity")]
        Electricity = 1,

        [Description("Telephone")]
        Telephone = 2,

        [Description("Water")]
        Water = 3,

        [Description("Rent")]
        Rent = 4,

        [Description("Vehicle Expenses")]
        VehicleExpenses = 5,

        [Description("Office supplies")]
        OfficeSupplies = 6,

        [Description("Entertainment")]
        Entertainment = 7,

        [Description("Business meals and travel expenses")]
        BusinessMealsAndTravelExpenses = 8,

        [Description("Payroll")]
        Payroll = 9,

        [Description("Employee benefits")]
        EmployeeBenefits = 10,

        [Description("Taxes")]
        Taxes = 11,

        [Description("Training and education")]
        TrainingAndEducation = 12,

        [Description("Professional fees and business services")]
        ProfessionalFeesAndBusinessServices = 13,

        [Description("Membership fees")]
        MembershipFees = 14,

        [Description("Interest payments and bank fees")]
        InterestPaymentsAndBankFees = 15,

        [Description("Business licenses and permits")]
        BusinessLicensesAndPermits = 16,

        [Description("Business insurance")]
        BusinessInsurance = 17,

        [Description("Website And Software Expenses")]
        WebsiteAndSoftwareExpenses = 18,

        [Description("Advertising and marketing")]
        AdvertisingAndMarketing = 20,

        [Description("Furniture, equipment, and machinery")]
        FurnitureEquipmentAndMachinery = 21,

        [Description("Other")]
        Other = 22,




    }
}
