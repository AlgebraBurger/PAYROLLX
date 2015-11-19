using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPAYROLLCONSOLE
{
    public class AppConfig
    {
        public string CompanyName { get; set; }
        public PaymentPeriod paymentPeriod { get; set; }
        public int HoursByDayCount { get; set; }
        public int TotalWorkingDays { get; set; }
        public DateTime StartDay { get; set; }
        public DateTime EndDay { get; set; }

    }

    public enum PaymentPeriod
    {
        DAILY, WEEKLY, SEMIMONTHLY, MONTHLY
    }

    public class TaxRateRow
    {
        public int rate { get; set; }
        public double amount { get; set; }
    }

    public static class TaxRateTable
    {
        public static List<TaxRateRow> getTable(AppConfig config)
        {
            List<TaxRateRow> table;
            switch (config.paymentPeriod)
            {
                case PaymentPeriod.DAILY:

                    table = new List<TaxRateRow>()
                                {
                                    new TaxRateRow() { rate = 5,  amount = 0.00 },
                                    new TaxRateRow() { rate = 10, amount = 1.65 },
                                    new TaxRateRow() { rate = 15, amount = 8.25 },
                                    new TaxRateRow() { rate = 20, amount = 28.05 },
                                    new TaxRateRow() { rate = 25, amount = 74.26 },
                                    new TaxRateRow() { rate = 30, amount = 165.02 },
                                    new TaxRateRow() { rate = 32, amount = 412.54 },

                                };

                    break;

                case PaymentPeriod.WEEKLY:

                    table = new List<TaxRateRow>()
                                {
                                    new TaxRateRow() { rate = 5,  amount = 0.00 },
                                    new TaxRateRow() { rate = 10, amount = 9.62 },
                                    new TaxRateRow() { rate = 15, amount = 48.08 },
                                    new TaxRateRow() { rate = 20, amount = 163.46 },
                                    new TaxRateRow() { rate = 25, amount = 432.69 },
                                    new TaxRateRow() { rate = 30, amount = 961.54 },
                                    new TaxRateRow() { rate = 32, amount = 2403.85 },

                                };

                    break;
                case PaymentPeriod.SEMIMONTHLY:

                    table = new List<TaxRateRow>()
                                {
                                    new TaxRateRow() { rate = 5,  amount = 0.00 },
                                    new TaxRateRow() { rate = 10, amount = 20.83 },
                                    new TaxRateRow() { rate = 15, amount = 104.17 },
                                    new TaxRateRow() { rate = 20, amount = 354.17 },
                                    new TaxRateRow() { rate = 25, amount = 937.50 },
                                    new TaxRateRow() { rate = 30, amount = 2083.33 },
                                    new TaxRateRow() { rate = 32, amount = 5208.33 },

                                };
                    break;
                case PaymentPeriod.MONTHLY:

                    table = new List<TaxRateRow>()
                                {
                                    new TaxRateRow() { rate = 5,  amount = 0.00 },
                                    new TaxRateRow() { rate = 10, amount = 41.67 },
                                    new TaxRateRow() { rate = 15, amount = 208.33 },
                                    new TaxRateRow() { rate = 20, amount = 708.33 },
                                    new TaxRateRow() { rate = 25, amount = 1875.00 },
                                    new TaxRateRow() { rate = 30, amount = 4166.67 },
                                    new TaxRateRow() { rate = 32, amount = 10416.67 },

                                };

                    break;

                default:

                    table = new List<TaxRateRow>()
                                {
                                    new TaxRateRow() { rate = 5,  amount = 0.00 },
                                    new TaxRateRow() { rate = 10, amount = 41.67 },
                                    new TaxRateRow() { rate = 15, amount = 208.33 },
                                    new TaxRateRow() { rate = 20, amount = 708.33 },
                                    new TaxRateRow() { rate = 25, amount = 1875.00 },
                                    new TaxRateRow() { rate = 30, amount = 4166.67 },
                                    new TaxRateRow() { rate = 32, amount = 10416.67 },

                                };

                    break;
            }
            return table;
        }
    }

    public class withHoldingTaxRow
    {
        public double baseSalary { get; set; }
        public double rangeSalary { get; set; }
        public double taxAmount { get; set; }
        public double excessRate { get; set; }
    }

    public class sssRow
    {
        public double FromSalaryCredit { get; set; }
        public double ToSalaryCredit { get; set; }
        public double m_salary_credit { get; set; }
        public double ss_er { get; set; }
        public double ss_ee { get; set; }
        public double ss_total { get; set; }
        public double ec_er { get; set; }
        public double tc_er { get; set; }
        public double tc_ee { get; set; }
        public double tc_total { get; set; }
        public double se_vm_ofw_tc { get; set; }

    }

    public class phRow
    {
        public double baseSalary { get; set; }
        public double rangeSalary { get; set; }
        public double totalPremium { get; set; }
        public double employeeShare { get; set; }
        public double employerShare { get; set; }
    }

    public class pagibigRow
    {
        public double baseSalary { get; set; }
        public double rangeSalary { get; set; }
        public double EmployeeShareRate { get; set; }
        public double EmployerShareRate { get; set; }
    }

    public interface IWithHoldingTaxTable
    {
        List<withHoldingTaxRow> getTable(AppConfig config);
    }

    public class zeroWithHoldingTaxTable : IWithHoldingTaxTable
    {
        public List<withHoldingTaxRow> getTable(AppConfig config)
        {
            var table = new List<withHoldingTaxRow>();
            switch (config.paymentPeriod)
            {
                case PaymentPeriod.DAILY:

                    table = new List<withHoldingTaxRow>()
                                {
                                     new withHoldingTaxRow() { baseSalary=0, rangeSalary=0,           excessRate=5, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==5).amount  },
                                     new withHoldingTaxRow() { baseSalary=33, rangeSalary=98.99,      excessRate=10, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==10).amount  },
                                     new withHoldingTaxRow() { baseSalary=99, rangeSalary=230.99,     excessRate=15, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==15).amount  },
                                     new withHoldingTaxRow() { baseSalary=231, rangeSalary=461.99,    excessRate=20, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==20).amount  },
                                     new withHoldingTaxRow() { baseSalary=462, rangeSalary=824.99,    excessRate=25, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==25).amount  },
                                     new withHoldingTaxRow() { baseSalary=825, rangeSalary=1649.99,   excessRate=30, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==30).amount  },
                                     new withHoldingTaxRow() { baseSalary=1650, rangeSalary=1000000,  excessRate=32, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==32).amount  },

                                };

                    break;
                case PaymentPeriod.WEEKLY:
                    table = new List<withHoldingTaxRow>()
                                {
                                     new withHoldingTaxRow() { baseSalary=0, rangeSalary=0,          excessRate=5, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==5).amount  },
                                     new withHoldingTaxRow() { baseSalary=192, rangeSalary=576.99,   excessRate=10, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==10).amount  },
                                     new withHoldingTaxRow() { baseSalary=577, rangeSalary=1345.99,  excessRate=15, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==15).amount  },
                                     new withHoldingTaxRow() { baseSalary=1346, rangeSalary=2691.99, excessRate=20, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==20).amount  },
                                     new withHoldingTaxRow() { baseSalary=2692, rangeSalary=4807.99, excessRate=25, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==25).amount  },
                                     new withHoldingTaxRow() { baseSalary=4808, rangeSalary=9614.99, excessRate=30, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==30).amount  },
                                     new withHoldingTaxRow() { baseSalary=9615, rangeSalary=1000000, excessRate=32, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==32).amount  },

                                };

                    break;
                case PaymentPeriod.SEMIMONTHLY:
                    table = new List<withHoldingTaxRow>()
                                {
                                     new withHoldingTaxRow() { baseSalary=0, rangeSalary=0,            excessRate=5, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==5).amount  },
                                     new withHoldingTaxRow() { baseSalary=417, rangeSalary=1249.99,    excessRate=10, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==10).amount  },
                                     new withHoldingTaxRow() { baseSalary=1250, rangeSalary=2916.99,   excessRate=15, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==15).amount  },
                                     new withHoldingTaxRow() { baseSalary=2917, rangeSalary=5832.99,   excessRate=20, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==20).amount  },
                                     new withHoldingTaxRow() { baseSalary=5833, rangeSalary=10416.99,  excessRate=25, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==25).amount  },
                                     new withHoldingTaxRow() { baseSalary=10417, rangeSalary=20832.99, excessRate=30, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==30).amount  },
                                     new withHoldingTaxRow() { baseSalary=20833, rangeSalary=1000000,  excessRate=32, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==32).amount  },

                                };

                    break;
                case PaymentPeriod.MONTHLY:
                    table = new List<withHoldingTaxRow>()
                                {
                                     new withHoldingTaxRow() { baseSalary=0, rangeSalary=0,      excessRate=5, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==5).amount  },
                                     new withHoldingTaxRow() { baseSalary=833, rangeSalary=2499.99, excessRate=10, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==10).amount  },
                                     new withHoldingTaxRow() { baseSalary=2500, rangeSalary=5832.99, excessRate=15, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==15).amount  },
                                     new withHoldingTaxRow() { baseSalary=5833, rangeSalary=11666.99, excessRate=20, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==20).amount  },
                                     new withHoldingTaxRow() { baseSalary=11667, rangeSalary=20832.99, excessRate=25, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==25).amount  },
                                     new withHoldingTaxRow() { baseSalary=20833, rangeSalary=41666.99, excessRate=30, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==30).amount  },
                                     new withHoldingTaxRow() { baseSalary=41667, rangeSalary=1000000, excessRate=32, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==32).amount  },

                                };

                    break;

            }

            return table;

        }
    }

    public class smeWithHoldingTaxTable : IWithHoldingTaxTable
    {
        public List<withHoldingTaxRow> getTable(AppConfig config)
        {

            var table = new List<withHoldingTaxRow>();
            switch (config.paymentPeriod)
            {
                case PaymentPeriod.DAILY:
                    table = new List<withHoldingTaxRow>()
                                {
                                    new withHoldingTaxRow() { baseSalary=165, rangeSalary=197.99, excessRate=5, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==5).amount  },
                                    new withHoldingTaxRow() { baseSalary=198, rangeSalary=263.99, excessRate=10, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==10).amount  },
                                    new withHoldingTaxRow() { baseSalary=264, rangeSalary=395.99, excessRate=15, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==15).amount  },
                                    new withHoldingTaxRow() { baseSalary=396, rangeSalary=626.99, excessRate=20, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==20).amount  },
                                    new withHoldingTaxRow() { baseSalary=627, rangeSalary=989.99, excessRate=25, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==25).amount  },
                                    new withHoldingTaxRow() { baseSalary=990, rangeSalary=1814.99, excessRate=30, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==30).amount  },
                                    new withHoldingTaxRow() { baseSalary=1815, rangeSalary=1000000, excessRate=32, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==32).amount  },

                                };

                    break;
                case PaymentPeriod.WEEKLY:
                    table = new List<withHoldingTaxRow>()
                                {
                                    new withHoldingTaxRow() { baseSalary=962,    rangeSalary=1153.99, excessRate=5, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==5).amount  },
                                    new withHoldingTaxRow() { baseSalary=1154,  rangeSalary=1537.99, excessRate=10, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==10).amount  },
                                    new withHoldingTaxRow() { baseSalary=1538,  rangeSalary=2307.99, excessRate=15, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==15).amount  },
                                    new withHoldingTaxRow() { baseSalary=2308,  rangeSalary=3653.99, excessRate=20, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==20).amount  },
                                    new withHoldingTaxRow() { baseSalary=3654,  rangeSalary=5768.99, excessRate=25, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==25).amount  },
                                    new withHoldingTaxRow() { baseSalary=5769,  rangeSalary=10576.99, excessRate=30, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==30).amount  },
                                    new withHoldingTaxRow() { baseSalary=10577, rangeSalary=1000000, excessRate=32, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==32).amount  },
                                };

                    break;
                case PaymentPeriod.SEMIMONTHLY:
                    table = new List<withHoldingTaxRow>()
                                {
                                    new withHoldingTaxRow() { baseSalary=962,    rangeSalary=1153.99, excessRate=5, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==5).amount  },
                                    new withHoldingTaxRow() { baseSalary=1154,  rangeSalary=1537.99, excessRate=10, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==10).amount  },
                                    new withHoldingTaxRow() { baseSalary=1538,  rangeSalary=2307.99, excessRate=15, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==15).amount  },
                                    new withHoldingTaxRow() { baseSalary=2308,  rangeSalary=3653.99, excessRate=20, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==20).amount  },
                                    new withHoldingTaxRow() { baseSalary=3654,  rangeSalary=5768.99, excessRate=25, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==25).amount  },
                                    new withHoldingTaxRow() { baseSalary=5769,  rangeSalary=10576.99, excessRate=30, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==30).amount  },
                                    new withHoldingTaxRow() { baseSalary=10577, rangeSalary=1000000, excessRate=32, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==32).amount  },
                                };

                    break;
                case PaymentPeriod.MONTHLY:
                    table = new List<withHoldingTaxRow>()
                                {
                                    new withHoldingTaxRow() { baseSalary=4167, rangeSalary=4999.99, excessRate=5, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==5).amount  },
                                    new withHoldingTaxRow() { baseSalary=5000, rangeSalary=6666.99, excessRate=10, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==10).amount  },
                                    new withHoldingTaxRow() { baseSalary=6667, rangeSalary=9999.99, excessRate=15, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==15).amount  },
                                    new withHoldingTaxRow() { baseSalary=10000, rangeSalary=15832.99, excessRate=20, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==20).amount  },
                                    new withHoldingTaxRow() { baseSalary=15833, rangeSalary=24999.99, excessRate=25, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==25).amount  },
                                    new withHoldingTaxRow() { baseSalary=25000, rangeSalary=45832.99, excessRate=30, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==30).amount  },
                                    new withHoldingTaxRow() { baseSalary=45833, rangeSalary=1000000, excessRate=32, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==32).amount  },

                                };

                    break;

            }

            return table;
        }
    }

    public class me1s1WithHoldingTaxTable : IWithHoldingTaxTable
    {
        public List<withHoldingTaxRow> getTable(AppConfig config)
        {



            var table = new List<withHoldingTaxRow>();
            switch (config.paymentPeriod)
            {
                case PaymentPeriod.DAILY:
                    table = new List<withHoldingTaxRow>()
                                {
                                    new withHoldingTaxRow() { baseSalary=248, rangeSalary=280.99, excessRate=5, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==5).amount  },
                                    new withHoldingTaxRow() { baseSalary=281, rangeSalary=346.99, excessRate=10, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==10).amount  },
                                    new withHoldingTaxRow() { baseSalary=347, rangeSalary=478.99, excessRate=15, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==15).amount  },
                                    new withHoldingTaxRow() { baseSalary=479, rangeSalary=709.99, excessRate=20, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==20).amount  },
                                    new withHoldingTaxRow() { baseSalary=710, rangeSalary=1072.99, excessRate=25, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==25).amount  },
                                    new withHoldingTaxRow() { baseSalary=1073, rangeSalary=1897.99, excessRate=30, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==30).amount  },
                                    new withHoldingTaxRow() { baseSalary=1898, rangeSalary=1000000, excessRate=32, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==32).amount  },

                                };

                    break;
                case PaymentPeriod.WEEKLY:
                    table = new List<withHoldingTaxRow>()
                        {

                                    new withHoldingTaxRow() { baseSalary=1442, rangeSalary=1634.99, excessRate=5, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==5).amount  },
                                    new withHoldingTaxRow() { baseSalary=1635, rangeSalary=2018.99, excessRate=10, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==10).amount  },
                                    new withHoldingTaxRow() { baseSalary=2019, rangeSalary=2787.99, excessRate=15, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==15).amount  },
                                    new withHoldingTaxRow() { baseSalary=2788, rangeSalary=4134.99, excessRate=20, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==20).amount  },
                                    new withHoldingTaxRow() { baseSalary=4135, rangeSalary=6249.99, excessRate=25, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==25).amount  },
                                    new withHoldingTaxRow() { baseSalary=6250, rangeSalary=11057.99, excessRate=30, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==30).amount  },
                                    new withHoldingTaxRow() { baseSalary=11058, rangeSalary=1000000, excessRate=32, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==32).amount  },
                        };

                    break;
                case PaymentPeriod.SEMIMONTHLY:
                    table = new List<withHoldingTaxRow>()
                        {

                                    new withHoldingTaxRow() { baseSalary=3125, rangeSalary=3541.99, excessRate=5, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==5).amount  },
                                    new withHoldingTaxRow() { baseSalary=3542, rangeSalary=4374.99, excessRate=10, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==10).amount  },
                                    new withHoldingTaxRow() { baseSalary=4375, rangeSalary=6041.99, excessRate=15, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==15).amount  },
                                    new withHoldingTaxRow() { baseSalary=6042, rangeSalary=8957.99, excessRate=20, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==20).amount  },
                                    new withHoldingTaxRow() { baseSalary=8958, rangeSalary=13541.99, excessRate=25, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==25).amount  },
                                    new withHoldingTaxRow() { baseSalary=13542, rangeSalary=23957.99, excessRate=30, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==30).amount  },
                                    new withHoldingTaxRow() { baseSalary=23958, rangeSalary=1000000, excessRate=32, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==32).amount  },
                        };

                    break;
                case PaymentPeriod.MONTHLY:
                    table = new List<withHoldingTaxRow>()
                                {
                                    new withHoldingTaxRow() { baseSalary=6250, rangeSalary=7082.99, excessRate=5, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==5).amount  },
                                    new withHoldingTaxRow() { baseSalary=7083, rangeSalary=8749.99, excessRate=10, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==10).amount  },
                                    new withHoldingTaxRow() { baseSalary=8750, rangeSalary=12082.99, excessRate=15, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==15).amount  },
                                    new withHoldingTaxRow() { baseSalary=12083, rangeSalary=17916.99, excessRate=20, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==20).amount  },
                                    new withHoldingTaxRow() { baseSalary=17917, rangeSalary=27082.99, excessRate=25, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==25).amount  },
                                    new withHoldingTaxRow() { baseSalary=27083, rangeSalary=47916.99, excessRate=30, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==30).amount  },
                                    new withHoldingTaxRow() { baseSalary=47917, rangeSalary=1000000, excessRate=32, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==32).amount  },

                                };

                    break;

            }

            return table;

        }
    }

    public class me2s2WithHoldingTaxTable : IWithHoldingTaxTable
    {
        public List<withHoldingTaxRow> getTable(AppConfig config)
        {


            var table = new List<withHoldingTaxRow>();
            switch (config.paymentPeriod)
            {
                case PaymentPeriod.DAILY:
                    table = new List<withHoldingTaxRow>()
                        {
                              new withHoldingTaxRow() { baseSalary=330, rangeSalary=362.99, excessRate=5, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==5).amount  },
                              new withHoldingTaxRow() { baseSalary=363, rangeSalary=428.99, excessRate=10, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==10).amount  },
                              new withHoldingTaxRow() { baseSalary=429, rangeSalary=560.99, excessRate=15, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==15).amount  },
                              new withHoldingTaxRow() { baseSalary=561, rangeSalary=791.99, excessRate=20, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==20).amount  },
                              new withHoldingTaxRow() { baseSalary=792, rangeSalary=1154.99, excessRate=25, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==25).amount  },
                              new withHoldingTaxRow() { baseSalary=1155, rangeSalary=1979.99, excessRate=30, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==30).amount  },
                              new withHoldingTaxRow() { baseSalary=1980, rangeSalary=1000000, excessRate=32, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==32).amount  },
                        };

                    break;
                case PaymentPeriod.WEEKLY:
                    table = new List<withHoldingTaxRow>()
                        {

                              new withHoldingTaxRow() { baseSalary=1923, rangeSalary=2114.99, excessRate=5, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==5).amount  },
                              new withHoldingTaxRow() { baseSalary=2115, rangeSalary=2499.99, excessRate=10, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==10).amount  },
                              new withHoldingTaxRow() { baseSalary=2500, rangeSalary=3268.99, excessRate=15, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==15).amount  },
                              new withHoldingTaxRow() { baseSalary=3269, rangeSalary=4314.99, excessRate=20, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==20).amount  },
                              new withHoldingTaxRow() { baseSalary=4315, rangeSalary=6730.99, excessRate=25, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==25).amount  },
                              new withHoldingTaxRow() { baseSalary=6731, rangeSalary=11537.99, excessRate=30, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==30).amount  },
                              new withHoldingTaxRow() { baseSalary=11538, rangeSalary=1000000, excessRate=32, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==32).amount  },
                        };

                    break;
                case PaymentPeriod.SEMIMONTHLY:
                    table = new List<withHoldingTaxRow>()
                        {

                            new withHoldingTaxRow() { baseSalary=4167, rangeSalary=4582.99, excessRate=5, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==5).amount  },
                              new withHoldingTaxRow() { baseSalary=4583, rangeSalary=5416.99, excessRate=10, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==10).amount  },
                              new withHoldingTaxRow() { baseSalary=5417, rangeSalary=7082.99, excessRate=15, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==15).amount  },
                              new withHoldingTaxRow() { baseSalary=7083, rangeSalary=9999.99, excessRate=20, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==20).amount  },
                              new withHoldingTaxRow() { baseSalary=10000, rangeSalary=14582.99, excessRate=25, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==25).amount  },
                              new withHoldingTaxRow() { baseSalary=14583, rangeSalary=24999.99, excessRate=30, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==30).amount  },
                              new withHoldingTaxRow() { baseSalary=25000, rangeSalary=1000000, excessRate=32, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==32).amount  },
                        };

                    break;
                case PaymentPeriod.MONTHLY:
                    table = new List<withHoldingTaxRow>()
                                {
                                    new withHoldingTaxRow() { baseSalary=8333, rangeSalary=9166.99, excessRate=5, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==5).amount  },
                                    new withHoldingTaxRow() { baseSalary=9167, rangeSalary=10832.99, excessRate=10, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==10).amount  },
                                    new withHoldingTaxRow() { baseSalary=10833, rangeSalary=14166.99, excessRate=15, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==15).amount  },
                                    new withHoldingTaxRow() { baseSalary=14167, rangeSalary=19999.99, excessRate=20, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==20).amount  },
                                    new withHoldingTaxRow() { baseSalary=20000, rangeSalary=29166.99, excessRate=25, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==25).amount  },
                                    new withHoldingTaxRow() { baseSalary=29167, rangeSalary=49999.99, excessRate=30, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==30).amount  },
                                    new withHoldingTaxRow() { baseSalary=50000, rangeSalary=1000000, excessRate=32, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==32).amount  },

                                };

                    break;

            }

            return table;

        }
    }

    public class me3s3WithHoldingTaxTable : IWithHoldingTaxTable
    {
        public List<withHoldingTaxRow> getTable(AppConfig config)
        {

            var table = new List<withHoldingTaxRow>();
            switch (config.paymentPeriod)
            {
                case PaymentPeriod.DAILY:
                    table = new List<withHoldingTaxRow>()
                        {
                             new withHoldingTaxRow() { baseSalary=413, rangeSalary=445.99, excessRate=5, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==5).amount  },
                             new withHoldingTaxRow() { baseSalary=446, rangeSalary=511.99, excessRate=10, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==10).amount  },
                             new withHoldingTaxRow() { baseSalary=512, rangeSalary=643.99, excessRate=15, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==15).amount  },
                             new withHoldingTaxRow() { baseSalary=644, rangeSalary=874.99, excessRate=20, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==20).amount  },
                             new withHoldingTaxRow() { baseSalary=875, rangeSalary=1237.99, excessRate=25, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==25).amount  },
                             new withHoldingTaxRow() { baseSalary=1238, rangeSalary=2062.99, excessRate=30, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==30).amount  },
                             new withHoldingTaxRow() { baseSalary=2063, rangeSalary=1000000, excessRate=32, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==32).amount  },

                        };

                    break;
                case PaymentPeriod.WEEKLY:
                    table = new List<withHoldingTaxRow>()
                        {
                            new withHoldingTaxRow() { baseSalary=2404, rangeSalary=2595.99, excessRate=5, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==5).amount  },
                            new withHoldingTaxRow() { baseSalary=2596, rangeSalary=2980.99, excessRate=10, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==10).amount  },
                            new withHoldingTaxRow() { baseSalary=2981, rangeSalary=3749.99, excessRate=15, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==15).amount  },
                            new withHoldingTaxRow() { baseSalary=3750, rangeSalary=5095.99, excessRate=20, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==20).amount  },
                            new withHoldingTaxRow() { baseSalary=5096, rangeSalary=7211.99, excessRate=25, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==25).amount  },
                            new withHoldingTaxRow() { baseSalary=7212, rangeSalary=12018.99, excessRate=30, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==30).amount  },
                            new withHoldingTaxRow() { baseSalary=12019, rangeSalary=1000000, excessRate=32, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==32).amount  },

                        };

                    break;
                case PaymentPeriod.SEMIMONTHLY:
                    table = new List<withHoldingTaxRow>()
                        {
                            new withHoldingTaxRow() { baseSalary=5208, rangeSalary=5624.99, excessRate=5, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==5).amount  },
                            new withHoldingTaxRow() { baseSalary=5625, rangeSalary=6457.99, excessRate=10, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==10).amount  },
                            new withHoldingTaxRow() { baseSalary=6458, rangeSalary=8124.99, excessRate=15, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==15).amount  },
                            new withHoldingTaxRow() { baseSalary=8125, rangeSalary=11041.99, excessRate=20, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==20).amount  },
                            new withHoldingTaxRow() { baseSalary=11042, rangeSalary=15624.99, excessRate=25, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==25).amount  },
                            new withHoldingTaxRow() { baseSalary=15625, rangeSalary=26041.99, excessRate=30, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==30).amount  },
                            new withHoldingTaxRow() { baseSalary=26042, rangeSalary=1000000, excessRate=32, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==32).amount  },
                        };

                    break;
                case PaymentPeriod.MONTHLY:
                    table = new List<withHoldingTaxRow>()
                                {
                                    new withHoldingTaxRow() { baseSalary=10417, rangeSalary=11249.99, excessRate=5, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==5).amount  },
                                    new withHoldingTaxRow() { baseSalary=11250, rangeSalary=12916.99, excessRate=10, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==10).amount  },
                                    new withHoldingTaxRow() { baseSalary=12917, rangeSalary=16249.99, excessRate=15, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==15).amount  },
                                    new withHoldingTaxRow() { baseSalary=16250, rangeSalary=22082.99, excessRate=20, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==20).amount  },
                                    new withHoldingTaxRow() { baseSalary=22083, rangeSalary=31249.99, excessRate=25, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==25).amount  },
                                    new withHoldingTaxRow() { baseSalary=31250, rangeSalary=52082.99, excessRate=30, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==30).amount  },
                                    new withHoldingTaxRow() { baseSalary=52083, rangeSalary=1000000, excessRate=32, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==32).amount  },

                                };

                    break;

            }

            return table;

        }
    }

    public class me4s4WithHoldingTaxTable : IWithHoldingTaxTable
    {

        public List<withHoldingTaxRow> getTable(AppConfig config)
        {


            var table = new List<withHoldingTaxRow>();
            switch (config.paymentPeriod)
            {
                case PaymentPeriod.DAILY:
                    table = new List<withHoldingTaxRow>()
                        {
                             new withHoldingTaxRow() { baseSalary=495, rangeSalary=527.99, excessRate=5, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==5).amount  },
                             new withHoldingTaxRow() { baseSalary=528, rangeSalary=593.99, excessRate=10, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==10).amount  },
                             new withHoldingTaxRow() { baseSalary=594, rangeSalary=725.99, excessRate=15, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==15).amount  },
                             new withHoldingTaxRow() { baseSalary=726, rangeSalary=956.99, excessRate=20, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==20).amount  },
                             new withHoldingTaxRow() { baseSalary=957, rangeSalary=1319.99, excessRate=25, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==25).amount  },
                             new withHoldingTaxRow() { baseSalary=1320, rangeSalary=2144.99, excessRate=30, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==30).amount  },
                             new withHoldingTaxRow() { baseSalary=2145, rangeSalary=1000000, excessRate=32, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==32).amount  },

                        };

                    break;
                case PaymentPeriod.WEEKLY:
                    table = new List<withHoldingTaxRow>()
                        {
                             new withHoldingTaxRow() { baseSalary=2885, rangeSalary=3076.99, excessRate=5, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==5).amount  },
                             new withHoldingTaxRow() { baseSalary=3077, rangeSalary=3461.99, excessRate=10, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==10).amount  },
                             new withHoldingTaxRow() { baseSalary=3462, rangeSalary=4230.99, excessRate=15, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==15).amount  },
                             new withHoldingTaxRow() { baseSalary=4231, rangeSalary=5576.99, excessRate=20, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==20).amount  },
                             new withHoldingTaxRow() { baseSalary=5577, rangeSalary=7691.99, excessRate=25, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==25).amount  },
                             new withHoldingTaxRow() { baseSalary=7692, rangeSalary=12499.99, excessRate=30, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==30).amount  },
                             new withHoldingTaxRow() { baseSalary=12500, rangeSalary=1000000, excessRate=32, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==32).amount  },

                        };

                    break;
                case PaymentPeriod.SEMIMONTHLY:
                    table = new List<withHoldingTaxRow>()
                        {
                            new withHoldingTaxRow() { baseSalary=6250, rangeSalary=6666.99, excessRate=5, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==5).amount  },
                             new withHoldingTaxRow() { baseSalary=6667, rangeSalary=7499.99, excessRate=10, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==10).amount  },
                             new withHoldingTaxRow() { baseSalary=7500, rangeSalary=9166.99, excessRate=15, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==15).amount  },
                             new withHoldingTaxRow() { baseSalary=9167, rangeSalary=12082.99, excessRate=20, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==20).amount  },
                             new withHoldingTaxRow() { baseSalary=12083, rangeSalary=16666.99, excessRate=25, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==25).amount  },
                             new withHoldingTaxRow() { baseSalary=16667, rangeSalary=27082.99, excessRate=30, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==30).amount  },
                             new withHoldingTaxRow() { baseSalary=27083, rangeSalary=1000000, excessRate=32, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==32).amount  },

                        };

                    break;
                case PaymentPeriod.MONTHLY:
                    table = new List<withHoldingTaxRow>()
                                {
                                    new withHoldingTaxRow() { baseSalary=12500, rangeSalary=13332.99, excessRate=5, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==5).amount  },
                                    new withHoldingTaxRow() { baseSalary=13333, rangeSalary=14999.99, excessRate=10, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==10).amount  },
                                    new withHoldingTaxRow() { baseSalary=15000, rangeSalary=18332.99, excessRate=15, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==15).amount  },
                                    new withHoldingTaxRow() { baseSalary=18333, rangeSalary=24166.99, excessRate=20, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==20).amount  },
                                    new withHoldingTaxRow() { baseSalary=24167, rangeSalary=33332.99, excessRate=25, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==25).amount  },
                                    new withHoldingTaxRow() { baseSalary=33333, rangeSalary=54166.99, excessRate=30, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==30).amount  },
                                    new withHoldingTaxRow() { baseSalary=54167, rangeSalary=1000000, excessRate=32, taxAmount=TaxRateTable.getTable(config).Find(x => x.rate==32).amount  },

                                };

                    break;

            }

            return table;

        }
    }

    public class PagibigTable
    {
        public List<pagibigRow> getTable()
        {
            return new List<pagibigRow>()
                {
                     new pagibigRow() { baseSalary=0, rangeSalary=1499.99 , EmployeeShareRate = 0.1, EmployerShareRate = 0.2  },
                     new pagibigRow() { baseSalary=1500, rangeSalary=100000000, EmployeeShareRate= 0.2, EmployerShareRate = 0.2 }
                };
        }
    }

    public class PHTable
    {
        public List<phRow> getTable()
        {
            return new List<phRow>()
                {
                    new phRow() { baseSalary=0,     rangeSalary=8999.99,    totalPremium=200.00, employeeShare=100    ,employerShare = 100.00 },
                    new phRow() { baseSalary=9000,  rangeSalary=9999.99,    totalPremium=225.00, employeeShare=112.50 ,employerShare = 112.50 },
                    new phRow() { baseSalary=10000, rangeSalary=10999.99,   totalPremium=250.00, employeeShare=125.00 ,employerShare = 125.00 },
                    new phRow() { baseSalary=11000, rangeSalary=11999.99,   totalPremium=275.00, employeeShare=137.50 ,employerShare = 137.50},
                    new phRow() { baseSalary=12000, rangeSalary=12999.99,   totalPremium=300.00, employeeShare=150.00 ,employerShare = 150.00},
                    new phRow() { baseSalary=13000, rangeSalary=13999.99,   totalPremium=325.00, employeeShare=162.50 ,employerShare = 162.50},
                    new phRow() { baseSalary=14000, rangeSalary=14999.99,   totalPremium=350.00, employeeShare=175.00 ,employerShare = 175.00},
                    new phRow() { baseSalary=15000, rangeSalary=15999.99,   totalPremium=375.00, employeeShare=187.50 ,employerShare = 187.50},
                    new phRow() { baseSalary=16000, rangeSalary=16999.99,   totalPremium=400.00, employeeShare=200.00 ,employerShare = 200.00},
                    new phRow() { baseSalary=17000, rangeSalary=17999.99,   totalPremium=425.00, employeeShare=212.50 ,employerShare = 212.50},
                    new phRow() { baseSalary=18000, rangeSalary=18999.99,   totalPremium=450.00, employeeShare=225.00 ,employerShare = 225.00},
                    new phRow() { baseSalary=19000, rangeSalary=19999.99,   totalPremium=475.00, employeeShare=237.50 ,employerShare = 237.50},
                    new phRow() { baseSalary=20000, rangeSalary=20999.99,   totalPremium=500.00, employeeShare=250.00 ,employerShare = 250.00},
                    new phRow() { baseSalary=21000, rangeSalary=21999.99,   totalPremium=525.00, employeeShare=262.50 ,employerShare = 262.50},
                    new phRow() { baseSalary=22000, rangeSalary=22999.99,   totalPremium=550.00, employeeShare=275.00 ,employerShare = 275.00},
                    new phRow() { baseSalary=23000, rangeSalary=23999.99,   totalPremium=575.00, employeeShare=287.50 ,employerShare = 287.50},
                    new phRow() { baseSalary=24000, rangeSalary=24999.99,   totalPremium=600.00, employeeShare=300.00 ,employerShare = 300.00},
                    new phRow() { baseSalary=25000, rangeSalary=25999.99,   totalPremium=625.00, employeeShare=312.50 ,employerShare = 312.50},
                    new phRow() { baseSalary=26000, rangeSalary=26999.99,   totalPremium=650.00, employeeShare=325.00 ,employerShare = 325.00},
                    new phRow() { baseSalary=27000, rangeSalary=27999.99,   totalPremium=675.00, employeeShare=337.50 ,employerShare = 337.50},
                    new phRow() { baseSalary=28000, rangeSalary=28999.99,   totalPremium=700.00, employeeShare=350.00 ,employerShare = 350.00},
                    new phRow() { baseSalary=29000, rangeSalary=29999.99,   totalPremium=725.00, employeeShare=362.50 ,employerShare = 362.50},
                    new phRow() { baseSalary=30000, rangeSalary=30999.99,   totalPremium=750.00, employeeShare=375.00 ,employerShare = 375.00},
                    new phRow() { baseSalary=31000, rangeSalary=31999.99,   totalPremium=775.00, employeeShare=387.50 ,employerShare = 387.50},
                    new phRow() { baseSalary=32000, rangeSalary=32999.99,   totalPremium=800.00, employeeShare=400.00 ,employerShare = 400.00},
                    new phRow() { baseSalary=33000, rangeSalary=33999.99,   totalPremium=825.00, employeeShare=412.50 ,employerShare = 412.50},
                    new phRow() { baseSalary=34000, rangeSalary=34999.99,   totalPremium=850.00, employeeShare=425.00 ,employerShare = 425.00},
                    new phRow() { baseSalary=35000, rangeSalary=1000000000, totalPremium=875.00, employeeShare=437.50 ,employerShare = 437.50},

                };
        }
    }

    public class SssTable
    {

        public List<sssRow> getTable()
        {
            return new List<sssRow>()
                {
                   new sssRow() { FromSalaryCredit=1000, ToSalaryCredit=1249.99, m_salary_credit = 1000,      ss_er=73.70,  ss_ee=36.30,   ss_total=110,     ec_er = 10, tc_er=83.70,   tc_ee=36.30,  tc_total = 120.00, se_vm_ofw_tc = 110.00 },
                   new sssRow() { FromSalaryCredit=1250, ToSalaryCredit=1749.99, m_salary_credit = 1500,      ss_er=110.50, ss_ee=54.50,   ss_total=165.00,  ec_er = 10, tc_er=120.50,  tc_ee=54.50,  tc_total = 175.00, se_vm_ofw_tc = 165.00 },
                   new sssRow() { FromSalaryCredit=1750, ToSalaryCredit=2249.99, m_salary_credit = 2000,      ss_er=147.30, ss_ee=72.70,   ss_total=220.00,  ec_er = 10, tc_er=157.30,  tc_ee=72.70,  tc_total = 230.00, se_vm_ofw_tc = 220.00 },
                   new sssRow() { FromSalaryCredit=2250, ToSalaryCredit=2749.99, m_salary_credit = 2500,      ss_er=184.20, ss_ee=90.80,   ss_total=275.00,  ec_er = 10, tc_er=194.20,  tc_ee=90.80,  tc_total = 285.00, se_vm_ofw_tc = 275.00 },
                   new sssRow() { FromSalaryCredit=2750, ToSalaryCredit=3249.99, m_salary_credit = 3000,      ss_er=221.00, ss_ee=109.00,  ss_total=330.00,  ec_er = 10, tc_er=231.00,  tc_ee=109.00, tc_total = 340.00, se_vm_ofw_tc = 330.00 },
                   new sssRow() { FromSalaryCredit=3250, ToSalaryCredit=3749.99, m_salary_credit = 3500,      ss_er=257.80, ss_ee=127.20,  ss_total=385.00,  ec_er = 10, tc_er=267.80,  tc_ee=127.20, tc_total = 395.00, se_vm_ofw_tc = 385.00 },
                   new sssRow() { FromSalaryCredit=3750, ToSalaryCredit=4249.99, m_salary_credit = 4000,      ss_er=294.70, ss_ee=145.30,  ss_total=440.00,  ec_er = 10, tc_er=304.70,  tc_ee=145.30, tc_total = 450.00, se_vm_ofw_tc = 440.00 },
                   new sssRow() { FromSalaryCredit=4250, ToSalaryCredit=4749.99, m_salary_credit = 4500,      ss_er=331.50, ss_ee=163.50,  ss_total=495.00,  ec_er = 10, tc_er=341.50,  tc_ee=163.50, tc_total = 505.00, se_vm_ofw_tc = 495.00 },
                   new sssRow() { FromSalaryCredit=4750, ToSalaryCredit=5249.99, m_salary_credit = 5000,      ss_er=368.30, ss_ee=181.70,  ss_total=550.00,  ec_er = 10, tc_er=378.30,  tc_ee=181.70, tc_total = 560.00, se_vm_ofw_tc = 550.00 },
                   new sssRow() { FromSalaryCredit=5250, ToSalaryCredit=5749.99, m_salary_credit = 5500,      ss_er=405.20, ss_ee=199.80,  ss_total=605.00,  ec_er = 10, tc_er=415.20,  tc_ee=199.80, tc_total = 615.00, se_vm_ofw_tc = 605.00 },
                   new sssRow() { FromSalaryCredit=5750, ToSalaryCredit=6249.99, m_salary_credit = 6000,      ss_er=442.00, ss_ee=218.00,  ss_total=660.00,  ec_er = 10, tc_er=452.00,  tc_ee=218.00, tc_total = 670.00, se_vm_ofw_tc = 660.00 },
                   new sssRow() { FromSalaryCredit=6250, ToSalaryCredit=6749.99, m_salary_credit = 6500,      ss_er=478.80, ss_ee=236.20,  ss_total=715.00,  ec_er = 10, tc_er=488.80,  tc_ee=236.20, tc_total = 725.00, se_vm_ofw_tc = 715.00 },
                   new sssRow() { FromSalaryCredit=6750, ToSalaryCredit=7249.99, m_salary_credit = 7000,      ss_er=515.70, ss_ee=254.30,  ss_total=770.00,  ec_er = 10, tc_er=525.70,  tc_ee=254.30, tc_total = 780.00, se_vm_ofw_tc = 770.00 },
                   new sssRow() { FromSalaryCredit=7250, ToSalaryCredit=7749.99, m_salary_credit = 7500,      ss_er=552.50, ss_ee=272.50,  ss_total=825.00,  ec_er = 10, tc_er=562.50,  tc_ee=272.50, tc_total = 835.00, se_vm_ofw_tc = 825.00 },
                   new sssRow() { FromSalaryCredit=7750, ToSalaryCredit=8249.99, m_salary_credit = 8000,      ss_er=589.30, ss_ee=290.70,  ss_total=880.00,  ec_er = 10, tc_er=599.30,  tc_ee=290.70, tc_total = 890.00, se_vm_ofw_tc = 880.00 },
                   new sssRow() { FromSalaryCredit=8250, ToSalaryCredit=8749.99, m_salary_credit = 8500,      ss_er=626.20, ss_ee=308.80,  ss_total=935.00,  ec_er = 10, tc_er=636.20,  tc_ee=308.80, tc_total = 945.00, se_vm_ofw_tc = 935.00 },
                   new sssRow() { FromSalaryCredit=8750, ToSalaryCredit=9249.99, m_salary_credit = 9000,      ss_er=663.00, ss_ee=327.00,  ss_total=990.00,  ec_er = 10, tc_er=673.00,  tc_ee=327.50, tc_total = 1000.00, se_vm_ofw_tc = 990.00 },
                   new sssRow() { FromSalaryCredit=9250, ToSalaryCredit=9749.99, m_salary_credit = 9500,      ss_er=699.80, ss_ee=345.20,  ss_total=1045.00, ec_er = 10, tc_er=709.80,  tc_ee=345.20, tc_total = 1055.00, se_vm_ofw_tc = 1045.00 },
                   new sssRow() { FromSalaryCredit=9750, ToSalaryCredit=10249.99, m_salary_credit = 10000,    ss_er=736.70, ss_ee=363.30,  ss_total=1100.00, ec_er = 10, tc_er=746.70,  tc_ee=363.30, tc_total = 1110.00, se_vm_ofw_tc = 1100.00 },
                   new sssRow() { FromSalaryCredit=10250, ToSalaryCredit=10749.99, m_salary_credit = 10500,   ss_er=773.70, ss_ee=381.50,  ss_total=1155.00, ec_er = 10, tc_er=783.50,  tc_ee=381.50, tc_total = 1165.00, se_vm_ofw_tc = 1155.00 },
                   new sssRow() { FromSalaryCredit=10750, ToSalaryCredit=11249.99, m_salary_credit = 11000,   ss_er=810.30, ss_ee=399.70,  ss_total=1210.00, ec_er = 10, tc_er=820.30,  tc_ee=399.70, tc_total = 1220.00, se_vm_ofw_tc = 1210.00 },
                   new sssRow() { FromSalaryCredit=11250, ToSalaryCredit=11749.99, m_salary_credit = 11500,   ss_er=847.20, ss_ee=417.80,  ss_total=1265.00, ec_er = 10, tc_er=857.20,  tc_ee=417.80, tc_total = 1275.00, se_vm_ofw_tc = 1265.00 },
                   new sssRow() { FromSalaryCredit=11750, ToSalaryCredit=12249.99, m_salary_credit = 12000,   ss_er=884.00, ss_ee=436.00,  ss_total=1320.00, ec_er = 10, tc_er=894.00,  tc_ee=436.00, tc_total = 1330.00, se_vm_ofw_tc = 1320.00 },
                   new sssRow() { FromSalaryCredit=12250, ToSalaryCredit=12749.99, m_salary_credit = 12500,   ss_er=920.80, ss_ee=454.20,  ss_total=1375.00, ec_er = 10, tc_er=930.80,  tc_ee=454.20, tc_total = 1385.00, se_vm_ofw_tc = 1375.00 },
                   new sssRow() { FromSalaryCredit=12750, ToSalaryCredit=13249.99, m_salary_credit = 13000,   ss_er=957.70, ss_ee=472.30,  ss_total=1430.00, ec_er = 10, tc_er=967.70,  tc_ee=472.30, tc_total = 1440.00, se_vm_ofw_tc = 1430.00 },
                   new sssRow() { FromSalaryCredit=13250, ToSalaryCredit=13749.99, m_salary_credit = 13500,   ss_er=994.50, ss_ee=490.50,  ss_total=1485.00, ec_er = 10, tc_er=1004.50, tc_ee=490.50, tc_total = 1495.00, se_vm_ofw_tc = 1485.00 },
                   new sssRow() { FromSalaryCredit=13750, ToSalaryCredit=14249.99, m_salary_credit = 14000,   ss_er=1031.30, ss_ee=508.70, ss_total=1540.00, ec_er = 10, tc_er=1041.30, tc_ee=508.70, tc_total = 1550.00, se_vm_ofw_tc = 1540.00 },
                   new sssRow() { FromSalaryCredit=14250, ToSalaryCredit=14749.99, m_salary_credit = 14500,   ss_er=1068.20, ss_ee=526.80, ss_total=1595.00, ec_er = 10, tc_er=1078.20, tc_ee=526.80, tc_total = 1605.00, se_vm_ofw_tc = 1595.00},
                   new sssRow() { FromSalaryCredit=14750, ToSalaryCredit=15249.99, m_salary_credit = 15000,   ss_er=1105.00, ss_ee=545.00, ss_total=1650.00, ec_er = 30, tc_er=1135.00, tc_ee=545.00, tc_total = 1680.00, se_vm_ofw_tc = 1650.00 },
                   new sssRow() { FromSalaryCredit=15250, ToSalaryCredit=15749.99, m_salary_credit = 15500,   ss_er=1141.80, ss_ee=563.20, ss_total=1705.00, ec_er = 30, tc_er=1171.80, tc_ee=563.20, tc_total = 1735.00, se_vm_ofw_tc = 1705.00 },
                   new sssRow() { FromSalaryCredit=15750, ToSalaryCredit=1000000000, m_salary_credit = 16000, ss_er=1178.70, ss_ee=581.30, ss_total=1760.00, ec_er = 30, tc_er=1208.70, tc_ee=581.30, tc_total = 1790.00, se_vm_ofw_tc = 1760.00 },
                };

        }
    }

    public enum TypeOfWorkingDay
    {
        OrdinaryDay, RestDay, SpecialDay, SpecialDayRestDay, RegularDay, RegularDayRestDay, DoubleHoliday, DoubleHolidayRestDay
    }


    public static class OverTimeTable
    {
        public static double getOTPremium(TypeOfWorkingDay typeOfWorkingDay, double hourlyRate, double hoursGenerated)
        {
            double ndPremium = 0;

            switch (typeOfWorkingDay)
            {
                case TypeOfWorkingDay.OrdinaryDay:
                    ndPremium = hourlyRate * hoursGenerated;
                    break;

                case TypeOfWorkingDay.RestDay:
                    ndPremium = hourlyRate * 1.3 * hoursGenerated;
                    break;
                case TypeOfWorkingDay.SpecialDay:
                    ndPremium = hourlyRate * 1.3 * hoursGenerated;
                    break;

                case TypeOfWorkingDay.SpecialDayRestDay:
                    ndPremium = hourlyRate * 1.5 * hoursGenerated;
                    break;

                case TypeOfWorkingDay.RegularDay:
                    ndPremium = hourlyRate * 2 * hoursGenerated;
                    break;
                case TypeOfWorkingDay.RegularDayRestDay:
                    ndPremium = hourlyRate * 2.6 * hoursGenerated;
                    break;
                case TypeOfWorkingDay.DoubleHoliday:
                    ndPremium = hourlyRate * 3.3 * hoursGenerated;
                    break;
                case TypeOfWorkingDay.DoubleHolidayRestDay:
                    ndPremium = hourlyRate * 3.9 * hoursGenerated;
                    break;

            }
            return ndPremium;
        }
    }

    public enum latePolicyType
    {
        TypeA, TypeB, TypeC
    }
    public class latePolicyRow
    {
        public latePolicyType latePolicyType { get; set; }
        public double deductionAmount { get; set; }
    }

    public static class latePolicyTable
    {
        public static List<latePolicyRow> getTable()
        {
            return new List<latePolicyRow>()
            {
                new latePolicyRow() { latePolicyType = latePolicyType.TypeA, deductionAmount = 10 },
                new latePolicyRow() { latePolicyType = latePolicyType.TypeB, deductionAmount = 20 },
                new latePolicyRow() { latePolicyType = latePolicyType.TypeC, deductionAmount = 30 }
            };
        }
    }

    public class overTimePolicyRow
    {
        public double Rate { get; set; }
        public DateTime StartTime { get; set; }
    }

    public static class overTimeTable
    {
        public static List<overTimePolicyRow> getTable()
        {
            return new List<overTimePolicyRow>()
            {
                new overTimePolicyRow() { Rate = 1, StartTime = DateTime.Parse("20:00:00") },
                new overTimePolicyRow() { Rate = 2, StartTime = DateTime.Parse("22:00:00") },
            };
        }
    }

    public static class NightDiffTable
    {
        public static double getNDPremium(TypeOfWorkingDay typeOfWorkingDay, double hourlyRate, double hoursGenerated)
        {
            double ndPremium = 0;

            switch (typeOfWorkingDay)
            {
                case TypeOfWorkingDay.OrdinaryDay:
                    ndPremium = hourlyRate * 0.10 * hoursGenerated;
                    break;
                case TypeOfWorkingDay.RestDay:
                    ndPremium = hourlyRate * 1.3 * 0.10 * hoursGenerated;
                    break;
                case TypeOfWorkingDay.SpecialDay:
                    ndPremium = hourlyRate * 1.3 * 0.10 * hoursGenerated;
                    break;
                case TypeOfWorkingDay.SpecialDayRestDay:
                    ndPremium = hourlyRate * 1.5 * 0.10 * hoursGenerated;
                    break;
                case TypeOfWorkingDay.RegularDay:
                    ndPremium = hourlyRate * 2 * 0.10 * hoursGenerated;
                    break;
                case TypeOfWorkingDay.RegularDayRestDay:
                    ndPremium = hourlyRate * 2.6 * 0.10 * hoursGenerated;
                    break;
                case TypeOfWorkingDay.DoubleHoliday:
                    ndPremium = hourlyRate * 3.3 * 0.10 * hoursGenerated;
                    break;
                case TypeOfWorkingDay.DoubleHolidayRestDay:
                    ndPremium = hourlyRate * 3.9 * 0.10 * hoursGenerated;
                    break;

            }
            return ndPremium;
        }
    }

}
