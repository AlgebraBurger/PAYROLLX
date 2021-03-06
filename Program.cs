﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HRPAYROLLCONSOLE
{
    class Program
    {
     
        static void Main(string[] args)
        {


            //-----------------------------------------------------------------
            // get every end of the month

            DateTime today = DateTime.Today;
            DateTime endOfMonth = new DateTime(today.Year,
                                               today.Month,
                                               DateTime.DaysInMonth(today.Year,
                                                                    today.Month));



            Console.WriteLine("Today is : " + today + ": which is " + today.DayOfWeek);

            Console.WriteLine(" -------------------- \t");
            Console.WriteLine("Last Day of the Month" + endOfMonth);



            //-------------------------------------------------------------------


            var holidays = new List<DateTime> {/* list of observed holidays */};
            DateTime lastBusinessDay = new DateTime();
            var s = DateTime.DaysInMonth(2015, 11);
            while (s > 0)
            {
                var dtCurrent = new DateTime(2015, 11, s);
                if (dtCurrent.DayOfWeek < DayOfWeek.Saturday && dtCurrent.DayOfWeek > DayOfWeek.Sunday &&
                 !holidays.Contains(dtCurrent))
                {
                    lastBusinessDay = dtCurrent;
                    s = 0;
                }
                else
                {
                    s = s - 1;
                }
            }
            Console.WriteLine(" -------------------- \t");
            Console.WriteLine("Last Business Day" + lastBusinessDay + "Which is " + lastBusinessDay.DayOfWeek);
            //--------------------------------------------------------------------




            AppConfig config = new AppConfig()
            {
                CompanyName = "HR PAYROLL SYSTEM",
                paymentPeriod = PaymentPeriod.MONTHLY,
                HoursByDayCount = 8,
                TotalWorkingDays = 20,
                StartDay = DateTime.Parse("9:00:00"),
                EndDay = DateTime.Parse("18:00:00")
            };

            
        

            List<Employee> employees = new List<Employee>()
            {
                new Employee()
                {
                    AccountID = "106",
                    FirstName = "Julius",
                    LastName = "Bacosa",
                    taxStatus = TaxStatus.Single,
                    MonthlySalary = 50000,
                    NumberOfDependents = 0,
                    attendace = new Attendance(){}

                },
            };


            //get attendance log        
            List<AttLogRow> attendace = GetAttendance();


            //get working calendar
            var calendar = Calendar.getCalendar();

            //calculator 
            foreach (var employee in employees)
            {
                Console.WriteLine(employee.AccountID);
                var totalDaysWorked = 0;
                double RegularDayHoursCount = 0;
                double DoubleHolidayHoursCount = 0;
                double DoubleHolidayRestDayHoursCount = 0;
                double OrdinaryDayHoursCount = 0;
                double RegularDayRestDayHoursCount = 0;
                double RestDayHoursCount = 0;
                double SpecialDayHoursCount = 0;
                double SpecialDayRestDayHoursCount = 0;

                double totalHoursLate = 0;
                double totalHoursUnderTime = 0;
           


                //get attendace
                List<DateTime> dlog = new List<DateTime>();
                List<DailyAttendanceRow> PunchCards = new List<DailyAttendanceRow>();

                var eAttn = attendace.Where(x => x.AccountID == employee.AccountID);

                if (eAttn.Count()>0)
                {
                    //has attendance
                    foreach (var e in eAttn)
                    {

                        if (e.checkType == CheckType.I)
                        {
                            PunchCards.Add(new DailyAttendanceRow()
                            {
                                AccountID = employee.AccountID,
                                IN = e.DateLog
                            });
                        }
                        if (e.checkType == CheckType.O)
                        {
                            foreach (var i in PunchCards)
                            {
                                if (i.IN.Date == e.DateLog.Date)
                                {
                                    i.OUT = e.DateLog;
                                }                                
                            }
                            
                        }

                    }

                    var pCards = PunchCards.Where(x => x.IN.Year != 0001 && x.OUT.Year != 0001).Distinct();
                    //var pCards = PunchCards;
                    pCards = pCards.GroupBy(x => x.IN.Date).Select(g => g.First()).ToList();

                    foreach (var e in pCards)
                    {
                        TimeSpan duration;
                        if (e.IN.Year != 0001 && e.OUT.Year!=0001)
                        {
                            duration = e.OUT - e.IN;
                            totalDaysWorked++;
                        }
                        else
                        {
                            //not valid date
                            duration = DateTime.Now - DateTime.Now;
                        }

                        // *** calculate attendance here!!!!

                        // 1. calculate late
                        // 2. calculate overtime , is this overtime approved? (check overtime request table)
                        // 3. Calculate undertime
                        // 4. what is the type of day? holiday? special? regular?

                        TimeSpan late = e.IN.TimeOfDay - config.StartDay.TimeOfDay;
                        TimeSpan OT = e.OUT.TimeOfDay - config.EndDay.TimeOfDay;
                        TimeSpan UT = config.EndDay.TimeOfDay - e.OUT.TimeOfDay;

                        Console.WriteLine(e.IN + " = " + e.OUT + " = " + duration.TotalHours + " : L?" + late.TotalHours + ": OT?" + OT.TotalHours + " : UT? " + UT.TotalHours);

                        if (late.TotalHours > 0)
                        {
                            totalHoursLate += late.TotalHours;
                        }

                        if (OT.TotalHours > 0)
                        {
                            //is this OT approved?
                            // check OT Request Table

                            //if Approved, what is the Type of Day to know the Value of OT.                           
                            var calendarList = calendar.Where(x => x.EventDate.Date == e.IN.Date);
                            if (calendarList.Count() > 0)
                            {
                                foreach (var cal in calendarList)
                                {
                                    switch (cal.typeOfWorkingDay)
                                    {
                                        case TypeOfWorkingDay.OrdinaryDay: OrdinaryDayHoursCount += OT.TotalHours; break;
                                        case TypeOfWorkingDay.DoubleHoliday: DoubleHolidayHoursCount += OT.TotalHours; break;
                                        case TypeOfWorkingDay.DoubleHolidayRestDay: DoubleHolidayRestDayHoursCount += OT.TotalHours; break;
                                        case TypeOfWorkingDay.RegularDay: RestDayHoursCount += OT.TotalHours; break;
                                        case TypeOfWorkingDay.RegularDayRestDay: RegularDayRestDayHoursCount += OT.TotalHours; break;
                                        case TypeOfWorkingDay.RestDay: RestDayHoursCount += OT.TotalHours; break;
                                        case TypeOfWorkingDay.SpecialDay: SpecialDayHoursCount += OT.TotalHours; break;
                                        case TypeOfWorkingDay.SpecialDayRestDay: SpecialDayRestDayHoursCount += OT.TotalHours; break;
                                        default: OrdinaryDayHoursCount += OT.TotalHours; break;

                                    }
                                }
                            }
                            else
                            {
                                //ordinary day
                                OrdinaryDayHoursCount += OT.TotalHours;
                            }

                        }

                        if (UT.TotalHours > 0)
                        {
                            totalHoursUnderTime += UT.TotalHours;
                        }                        
                    }                    
                }
                else
                {

                }

                Console.WriteLine(OrdinaryDayHoursCount);

                Attendance att = new Attendance()
                {
                    NumberOfDaysWorked = totalDaysWorked,
                    TotalWorkingDays = config.TotalWorkingDays,
                    TotalLateHours = totalHoursLate,
                    TotalHoursUnderTime  = totalHoursUnderTime,
                    RegularDayHoursCount = RegularDayHoursCount,
                    DoubleHolidayHoursCount = DoubleHolidayHoursCount,
                    DoubleHolidayRestDayHoursCount = DoubleHolidayRestDayHoursCount,
                    OrdinaryDayHoursCount = OrdinaryDayHoursCount,
                    RegularDayRestDayHoursCount = RegularDayRestDayHoursCount,
                    RestDayHoursCount = RestDayHoursCount,
                    SpecialDayHoursCount = SpecialDayHoursCount,
                    SpecialDayRestDayHoursCount = SpecialDayRestDayHoursCount,
                };

                employee.attendace = att;


                SalaryCalculator salary = new SalaryCalculator(employee, config);
                Pay pay = salary.Compute();

                Display(employee, pay);

            }



            Console.ReadLine();
        }

        public enum CheckType
        {
            I, O
        }

        public class AttLogRow
        {
            public string AccountID { get; set; }
            public DateTime DateLog { get; set; }
            public CheckType checkType { get; set; }
            public int sensorID { get; set; }
        }

        public class DailyAttendanceRow
        {
            public string AccountID { get; set; }
            public DateTime IN { get; set; }
            public DateTime OUT { get; set; }         
        }

        public static List<AttLogRow> GetAttendance()
        {
            List<AttLogRow> attendanceLogList = new List<AttLogRow>();

            using (StreamReader sr = new StreamReader(@"C:\WOLFX\C#\HRPAYROLLCONSOLE\HRPAYROLLCONSOLE\HRPAYROLLCONSOLE\test-logs.csv"))
            {
                string currentLine;
                // currentLine will be null when the StreamReader reaches the end of file
                while ((currentLine = sr.ReadLine()) != null)
                {
                    if (currentLine == "Ac-No,sTime,checktype,sensorid") { }
                    else
                    {
                        string[] str = currentLine.Split(',');
                        AttLogRow attLogRow = new AttLogRow()
                        {
                            AccountID = str[0],
                            DateLog = DateTime.Parse(str[1]),
                            checkType = str[2] == "I" ? CheckType.I : CheckType.O,
                            sensorID = Int32.Parse(str[3]),
                        };

                        attendanceLogList.Add(attLogRow);

                    }
                }
            }

            return attendanceLogList;
        }

        public static void Display(Employee employee, Pay pay)
        {
            Console.WriteLine(" --------------------------------------------------------- \t");
            Console.WriteLine("                                                          \t");
            Console.WriteLine("NAME: " + employee.FirstName + " " + employee.LastName + "\t");
            Console.WriteLine("Tax Status: " + employee.taxStatus + "\t");
            Console.WriteLine("-----------------------------------------------------------\t");
            Console.WriteLine("Salary: " + employee.MonthlySalary + "\t");
            Console.WriteLine("-----------------------------------------------------------\t");


            var TotalWorkingDays = employee.attendace.TotalWorkingDays;
            var daysWorked = employee.attendace.NumberOfDaysWorked;
            var totalDaysAbsent = TotalWorkingDays - daysWorked;

            Console.WriteLine("-----------------------------------------------------------\t");
            Console.WriteLine("Total Working Days: " + TotalWorkingDays + "\t");
            Console.WriteLine("Total Days Worked: " + daysWorked + "\t");
            Console.WriteLine("Number of Days Absent: " + totalDaysAbsent + "\t");
            Console.WriteLine("-----------------------------------------------------------\t");
            
            Console.WriteLine("-----------------------------------------------------------\t");
            Console.WriteLine("Basic Pay: " + pay.BasicPay + "\t");
            Console.WriteLine("OT Pay: +" + pay.TotalOTAmount + "\t");
            Console.WriteLine("Total Late Deduction: -" + pay.TotalLateDeduction + "\t");
            Console.WriteLine("-----------------------------------------------------------\t");
            Console.WriteLine("Gross Pay: " + pay.GrossPay + "\t");
            Console.WriteLine("-----------------------------------------------------------\t");

           

            Console.WriteLine("                                                           \t");
            Console.WriteLine("-----------------------------------------------------------\t");
            Console.WriteLine("WithHolding Tax: " + pay.WithHoldingTax + "\t");
            Console.WriteLine("SSS Contribution: " + pay.SSSContribution + "\t");
            Console.WriteLine("PhilHeal Contribution: " + pay.PhilHealthContribution + "\t");
            Console.WriteLine("Pagibig Contribution: " + pay.PagIbigContribution + "\t");
            Console.WriteLine("-----------------------------------------------------------\t");
            Console.WriteLine("Total Deduction: " + pay.TotalDeduction + "\t");

            Console.WriteLine("Net Pay: " + pay.NetPay + "\t");
            Console.WriteLine("                                                          \t");
            Console.WriteLine(" --------------------------------------------------------- \t");
        }

       

        public class SalaryCalculator
        {
            private Employee employee;
            private Attendance attendance;
            private AppConfig config;


            public SalaryCalculator(Employee _employee,  AppConfig _config)
            {
                employee = _employee;
                attendance = employee.attendace;
                config = _config;

            }

            public double computeWithHoldingTax<T>(T TaxTable) where T : IWithHoldingTaxTable
            {

                double withHoldingTax = 0;
                double taxAmount = 0;
                double excess = 0;
                double taxRate = 0;
                
                var taxRows = TaxTable.getTable(config);
                foreach (withHoldingTaxRow row in taxRows)
                {
                    //if (row.baseSalary < employee.MonthlySalary && row.rangeSalary >= employee.MonthlySalary)
                    //{
                    //    taxAmount = row.taxAmount;
                    //    excess = employee.MonthlySalary - row.baseSalary;
                    //    taxRate = row.excessRate / 100;

                    //    withHoldingTax = taxAmount + (excess * taxRate);
                    //}
                    if (row.baseSalary < employee.attendace.GrossPay && row.rangeSalary >= employee.attendace.GrossPay)
                    {
                        taxAmount = row.taxAmount;
                        excess = employee.attendace.GrossPay - row.baseSalary;
                        taxRate = row.excessRate / 100;

                        withHoldingTax = taxAmount + (excess * taxRate);
                    }
                }

                return withHoldingTax;
            }

            public Pay Compute()
            {

                var taxStatus = employee.taxStatus;

                var totalWorkingDays = attendance.TotalWorkingDays;
                var totalDaysWorked = attendance.NumberOfDaysWorked;
                var Salary = employee.MonthlySalary;

                double basicPay = (Salary / totalWorkingDays) * totalDaysWorked;

                employee.attendace.BasicPay = basicPay;

                double hourlyRate = (employee.MonthlySalary / employee.attendace.TotalWorkingDays) / config.HoursByDayCount;

                //---------------- + nd / ot ----------------------
                
                double OTAmount = 0;

                if (employee.attendace.OrdinaryDayHoursCount > 0)
                {
                    OTAmount += OverTimeTable.getOTPremium(TypeOfWorkingDay.OrdinaryDay, hourlyRate, employee.attendace.OrdinaryDayHoursCount);
                }
                if (employee.attendace.RestDayHoursCount > 0)
                {
                    OTAmount += OverTimeTable.getOTPremium(TypeOfWorkingDay.OrdinaryDay, hourlyRate, employee.attendace.RestDayHoursCount);
                }
                if (employee.attendace.SpecialDayHoursCount > 0)
                {
                    OTAmount += OverTimeTable.getOTPremium(TypeOfWorkingDay.OrdinaryDay, hourlyRate, employee.attendace.SpecialDayHoursCount);
                }
                if (employee.attendace.SpecialDayRestDayHoursCount > 0)
                {
                    OTAmount += OverTimeTable.getOTPremium(TypeOfWorkingDay.OrdinaryDay, hourlyRate, employee.attendace.SpecialDayRestDayHoursCount);
                }
                if (employee.attendace.RegularDayHoursCount > 0)
                {
                    OTAmount += OverTimeTable.getOTPremium(TypeOfWorkingDay.OrdinaryDay, hourlyRate, employee.attendace.RegularDayHoursCount);
                }
                if (employee.attendace.RegularDayRestDayHoursCount > 0)
                {
                    OTAmount += OverTimeTable.getOTPremium(TypeOfWorkingDay.OrdinaryDay, hourlyRate, employee.attendace.RegularDayRestDayHoursCount);
                }
                if (employee.attendace.DoubleHolidayHoursCount > 0)
                {
                    OTAmount += OverTimeTable.getOTPremium(TypeOfWorkingDay.OrdinaryDay, hourlyRate, employee.attendace.DoubleHolidayHoursCount);
                }
                if (employee.attendace.DoubleHolidayRestDayHoursCount > 0)
                {
                    OTAmount += OverTimeTable.getOTPremium(TypeOfWorkingDay.OrdinaryDay, hourlyRate, employee.attendace.DoubleHolidayRestDayHoursCount);
                }



            //---------------- - nd / ot ----------------------

                double taxableAllowance = attendance.TaxableAllowance;
                double nonTaxableAllowance = attendance.NonTaxableAllowance;


                var  latePolicy = latePolicyTable.getTable();


                var lPolicy = latePolicy.Find(x => x.latePolicyType == latePolicyType.TypeA);

                var totalLateDeduction = employee.attendace.TotalLateHours * lPolicy.deductionAmount;
                var totalUnderTimeDeduction = employee.attendace.TotalHoursUnderTime * lPolicy.deductionAmount;

                double grossPay = (basicPay + taxableAllowance + nonTaxableAllowance +  OTAmount) - totalLateDeduction - totalUnderTimeDeduction;

                employee.attendace.GrossPay = grossPay;

                // --- deductions ---

                double withHoldingTax = 0; // how to compute withholding


                if (employee.NumberOfDependents > 0)
                {


                    if (employee.NumberOfDependents == 1)
                    {
                        me1s1WithHoldingTaxTable taxTable = new me1s1WithHoldingTaxTable();
                        withHoldingTax = computeWithHoldingTax<me1s1WithHoldingTaxTable>(taxTable);
                    }
                    if (employee.NumberOfDependents == 2)
                    {
                        me2s2WithHoldingTaxTable taxTable = new me2s2WithHoldingTaxTable();
                        withHoldingTax = computeWithHoldingTax<me2s2WithHoldingTaxTable>(taxTable);
                    }
                    if (employee.NumberOfDependents == 3)
                    {
                        me3s3WithHoldingTaxTable taxTable = new me3s3WithHoldingTaxTable();
                        withHoldingTax = computeWithHoldingTax<me3s3WithHoldingTaxTable>(taxTable);
                    }
                    if (employee.NumberOfDependents >= 4)
                    {
                        me4s4WithHoldingTaxTable taxTable = new me4s4WithHoldingTaxTable();
                        withHoldingTax = computeWithHoldingTax<me4s4WithHoldingTaxTable>(taxTable);
                    }

                }
                else
                {
                    withHoldingTax = 0;
                    if (employee.taxStatus == TaxStatus.Zero)
                    {

                        zeroWithHoldingTaxTable taxTable = new zeroWithHoldingTaxTable();
                        withHoldingTax = computeWithHoldingTax<zeroWithHoldingTaxTable>(taxTable);

                    }
                    else
                    {
                        smeWithHoldingTaxTable taxTable = new smeWithHoldingTaxTable();
                        withHoldingTax = computeWithHoldingTax<smeWithHoldingTaxTable>(taxTable);

                    }

                }


                var sssTable = new SssTable();
                var sssRows = sssTable.getTable();

                double sssContribution = 0; //how to compute sss
                foreach (sssRow row in sssRows)
                {
                    if (row.FromSalaryCredit < employee.MonthlySalary && row.ToSalaryCredit >= employee.MonthlySalary)
                    {                        
                        sssContribution = row.ss_ee; //employee sss
                    }
                }


                var phTable = new PHTable();
                var phRows = phTable.getTable();

                double PhilHealthContribution = 0; //how to compute philhealth
                foreach (phRow row in phRows)
                {
                    if (row.baseSalary <= employee.MonthlySalary && row.rangeSalary > employee.MonthlySalary)
                    {
                        PhilHealthContribution = row.employeeShare;
                    }
                }

                double pagIbigCOntribution = 0; //how to compute pagibig
                var pagibigTable = new PagibigTable();
                var pagibigRow = pagibigTable.getTable();

                double pagIbigShareRate = 1;
                foreach (pagibigRow row in pagibigRow)
                {
                    if (row.baseSalary < employee.MonthlySalary && row.rangeSalary >= employee.MonthlySalary)
                    {
                        pagIbigShareRate = row.EmployeeShareRate;
                    }
                }

                pagIbigCOntribution = employee.MonthlySalary * (pagIbigShareRate / 100);


                var totalDeduction = withHoldingTax + sssContribution + PhilHealthContribution + pagIbigCOntribution;

                var netPay = grossPay - totalDeduction;

                Pay pay = new Pay()
                {
                    BasicPay = basicPay,
                    TotalOTAmount = OTAmount,
                    TotalLateDeduction = totalLateDeduction,
                    HourlyRate = hourlyRate,
                    EmployeeName = employee.FirstName + " " + employee.LastName,
                    TaxableAllowance = taxableAllowance,
                    NonTaxableAllowance = nonTaxableAllowance,                            
                    GrossPay = grossPay,
                    WithHoldingTax = withHoldingTax,
                    SSSContribution = sssContribution,
                    PhilHealthContribution = PhilHealthContribution,
                    PagIbigContribution = pagIbigCOntribution,
                    TotalDeduction = totalDeduction,
                    NetPay = netPay,

                };

                return pay;
            }
        }

        public class Attendance
        {
            public int ID { get; set; }
            public double NumberOfHoursWorked { get; set; }
            public int NumberOfDaysWorked { get; set; }
          
            public double BasicPay { get; set; }
            public double GrossPay { get; set; }

            public double TotalLateHours { get; set; }
            public double TotalHoursUnderTime { get; set; }
            public double TaxableAllowance { get; set; }
            public double NonTaxableAllowance { get; set; }


            public double OrdinaryDayHoursCount { get; set; }
            public double RestDayHoursCount { get; set; }
            public double SpecialDayHoursCount { get; set; }
            public double SpecialDayRestDayHoursCount { get; set; }
            public double RegularDayHoursCount { get; set; }
            public double RegularDayRestDayHoursCount { get; set; }
            public double DoubleHolidayHoursCount { get; set; }
            public double DoubleHolidayRestDayHoursCount { get; set; }

            public int TotalWorkingDays { get; set; }

            
        }

        public enum TaxStatus
        {
            Zero, Single, Married
        }

        public class Employee
        {
            public int ID { get; set; }
            public string AccountID { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            
            public int NumberOfDependents { get; set; }
            public TaxStatus taxStatus { get; set; }
            public double MonthlySalary { get; set; }
            public double SemiMonthlySalary { get { return MonthlySalary / 2; } }
         
            public Attendance attendace { get; set; }

        }

        public class Pay
        {

            public int ID { get; set; }
            public string EmployeeName { get; set; }
            public double TotalLateDeduction { get; set; }
            public double TotalOTAmount { get; set; }
            public double HourlyRate { get; set; }
            public double BasicPay { get; set; }
            public double TaxableAllowance { get; set; }
            public double NonTaxableAllowance { get; set; }
            public double NightDifferential { get; set; }
            public double OverTimePay { get; set; }
            public double GrossPay { get; set; }
            public double WithHoldingTax { get; set; }
            public double SSSContribution { get; set; }
            public double PhilHealthContribution { get; set; }
            public double PagIbigContribution { get; set; }
            public double TotalDeduction { get; set; }
            public double NetPay { get; set; }
            public DateTime Created { get; set; }
        }

       
    }

   
}
