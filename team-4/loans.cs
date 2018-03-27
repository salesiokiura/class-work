using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WakenyapamojaSaccoSystem
{
    class loans
    {
        static public DataTable ListofMembersLoans(string IDNumber)
        {
            OleDbCommand cmd = new OleDbCommand();
            cmd.CommandText = "SELECT [id], [LoanAmount], [Period] FROM [loans] WHERE [MemberID] = @ID AND [Status] = @Disbursed";
            cmd.Parameters.AddWithValue("ID", Program.GetMemberSystemID(IDNumber));
            cmd.Parameters.AddWithValue("Disbursed", Properties.Settings.Default.DisbursedLoanStatus);
            DataTable dt = db.ReadFromDB(cmd);
            return dt;
        }

        static public double CompoundInterest(double principal, double interestRate, int timesPerYear, double years)
        {
            // (1 + r/n)
            double body = 1 + (interestRate / timesPerYear);

            // nt
            double exponent = timesPerYear * years;

            // P(1 + r/n)^nt
            return principal * Math.Pow(body, exponent);
        }

        static public bool ChangeLoanStatus(string LoanStatus, string LoanApplicationID)
        {
            try
            {
                string sql = "UPDATE [loans] SET [Status] = @status WHERE [id] = @id";
                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("status", LoanStatus);
                cmd.Parameters.AddWithValue("id", LoanApplicationID);
                db.CreateRecord(cmd);
                return true;

            } catch (Exception E)
            {
                MessageBox.Show("Error: " + E.Message, "Wakenya pamoja Sacco", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}
