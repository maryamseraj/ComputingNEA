using System;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
namespace COMPUTINGNEA.Models
{
    public class Investment : User
    {
        public string InvestmentName { get; set; }
        public string Industry { get; set; }
        public double IndustryMarketGrowth { get; set; }
        public string AmountInvested { get; set; }
        public string Revenue { get; set; }
        public string Profit { get; set; }
        public double InvestmentReturn { get; set; }

        public override int SaveDetails()
        {

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "connectiontestnea.database.windows.net";
            builder.UserID = "maryamseraj";
            builder.Password = "Thenextstep22!";
            builder.InitialCatalog = "connection";
            neuralnetwork nn = new neuralnetwork();
            // Connect to SQL database on Azure
            SqlConnection con = new SqlConnection(builder.ConnectionString);
            int num = 17;

            var industryGrowth = new Dictionary<string, double>(){
                {"Advertising", 23.35},
                {"Aerospace", 26.85},
                {"Air transport", 52.59},
                {"Apparel", 16.09},
                {"Auto & Truck", 160.60},
                {"Auto parts", 18.46},
                {"Bank (Money Center)", 1.47},
                {"Banks (regional)", 7.26},
                {"Beverage (Alcoholic)", 13.02},
                {"Beverage (Soft)", 22.31},
                {"Broadcasting", 14.56},
                {"Brokerage & Investment Banking", 10.72},
                {"Building Materials", 19.60},
                {"Business & Consumer Services", 15.60},
                {"Cable TV", 78.40 },
                {"Chemical (Basic)", 16.43},
                {"Chemical (Diversified)", 19.57},
                {"Chemical (Speciality)", 18.33},
                {"Coal & Related Energy", 219.21},
                {"Computer Services", 11.32},
                {"Computers/Peripherals", 13.78},
                {"Construction Supplies", 146.29},
                {"Diversified", 41.07},
                {"Drugs (Biotechnology)", 114.73},
                {"Drugs (Pharamceutical)", 149.80},
                {"Education", 11.54},
                {"Electrical Equipment", 103.37},
                {"Electronics (Consumer & Office", 17.21},
                {"Electronics (General", 50.18},
                {"Engineering/Construction", 23.36},
                {"Entertainment", 46.44},
                {"Environmental & Waste Services", 40.80},
                {"Farming/Agriculture", 35.48},
                {"Financial Services", 12.67},
                {"Food Processing", 15.26},
                {"Food Wholesalers", 17.92},
                {"Furn/Home Furnishings", 15.06},
                {"Green & Renewable Energy", 22.70},
                {"Healthcare Products", 83.72},
                {"Healthcare Support Services", 23.79},
                {"Healthcare Information and Technology", 35.55},
                {"Homebuilding", 24.31},
                {"Hospitals/Healthcare Facilities", 14.26},
                {"Hotel/Gaming", 55.38 },
                {"Household Products", 40.02},
                {"Information Services", 18.99},
                {"Insurance (General)", 18.97},
                {"Insurance (Life)", 15.87},
                {"Insurance (Props/Cas.)", 7.59},
                {"Investments & Asset Management", 14.07},
                {"Machinery", 16.57},
                {"Metals & Mining", 34.38},
                {"Office Equipment & Services", 13.78},
                {"Oil/Gas (Integrated)", 31.27},
                {"Oil/Gas (Production and Exploration)", 55.06},
                {"Oil/Gas Distribution", 25.34},
                {"Oilfield Svcs/Equip.", 29.75},
                {"Packaging & Container", 9.50},
                {"Paper/forest Products", 13.82},
                {"Power", 8.06},
                {"Precious Metals", 28.43},
                {"Publishing and Newspapers", 7.18},
                {"R.E.I.T", 16.19},
                {"Real Estate (Development)", 1140.37},
                {"Real Estate (General/Diversified)", 3.80},
                {"Real Estate (Operations & Services)", 39.87},
                {"Recreation", 29.15},
                {"Reinsurance", 5.14},
                {"Restaurant/Dining", 29.50},
                {"Retail (Automotive)", 39.25},
                {"Retail (Building Supply)", 16.93},
                {"Retail (Distributors)", 16.37},
                {"Retail (General)", 11.35},
                {"Retail (Grocery and Food)", 6.31},
                {"Retail (Online)", 19.03},
                {"Retail (Special Lines)", 14.98},
                {"Rubber & Tires", 28.20},
                {"Semiconductor", 22.25},
                {"Semiconductor Equip", 32.62},
                {"Shipbuilding & Marine", 26.88},
                {"Shoe", 27.51},
                {"Software (Entertainment)", 34.66},
                {"Software (Internet)", 25.85},
                {"Software (System & Application", 41.69},
                {"Steel", 26.60},
                {"Telecom (Wireless)", 8.07},
                {"Telecom. Equipment", 23.45},
                {"Telecom. Services", 19.05},
                {"Tobacco", 17.90},
                {"Transportation", 14.10},
                {"Transportaion (Railroads", 10.96},
                {"Trucking", 30.05},
                {"Utility (General)", 4.97},
                {"Utility (Water)", 6.29},
};
            IndustryMarketGrowth = industryGrowth[Industry];
            InvestmentReturn = Calculation();
            Userid = GetUserID();
            string insertDetails = "INSERT INTO [dbo].[Investment](UserID, Useridd, InvestmentName, Industry, AmountInvested, Revenue, Profit, IndustryMarketGrowth, InvestmentReturn) values ('" + Userid + "'" + "," + "'" + num + "'" + "," + "'" + InvestmentName + "'" + "," + "'" + Industry + "'" + "," + "'" + AmountInvested + "'" + "," + "'" + Revenue + "'" + "," + "'" + Profit + "'" + "," + "'" + IndustryMarketGrowth + "'" + "," + "'" + InvestmentReturn + "')";

            SqlCommand cmd1 = new SqlCommand(insertDetails, con);
            con.Open();
            int i = cmd1.ExecuteNonQuery();
            con.Close();
            return i;
        }

        public double Calculation()
        {
        

        InvestmentReturn = (Convert.ToDouble(Revenue) - Convert.ToDouble(AmountInvested)) * 100;

            return InvestmentReturn;
            
        }

        static void PrintMatrix(double[,] matrix)
        {
            int rowLength = matrix.GetLength(0);
            int colLength = matrix.GetLength(1);

            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < colLength; j++)
                {
                    Console.Write(string.Format("{0} ", matrix[i, j]));
                }
                Console.Write(Environment.NewLine);
            }
        }

        public List<string> SearchName(string searchInput)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "connectiontestnea.database.windows.net";
            builder.UserID = "maryamseraj";
            builder.Password = "Thenextstep22!";
            builder.InitialCatalog = "connection";

            // Connect to SQL database on Azure
        //    SqlConnection con = new SqlConnection(builder.ConnectionString);
          

            // Check if username and email already exist
            string SearchName = "SELECT InvestmentName FROM [dbo].[Investment] WHERE InvestmentName = @SearchTerm";
           

          //  string connectionstring = "Server=tcp:connectiontestnea.database.windows.net,1433;Initial Catalog=connection;Persist Security Info=False;User ID=maryamseraj;Password={your_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            

          

            var investments = new List<string>();

            using (var connection = new SqlConnection(builder.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(SearchName, connection))
                {
                    command.Parameters.Add(new SqlParameter("@SearchTerm", searchInput));

                    using (var readerr = command.ExecuteReader())
                    {
                        while (readerr.Read())
                        {
                            string investment1 = readerr.GetString(0);
                            investments.Add(investment1);
                        }
                    }
                }
            }


            
            return investments;
        }
            

            
       
        public void MainMerge<T>(T[] values, int left, int mid, int right) where T : IComparable<T>
        {
            T[] temp = new T[25];
            int i, eol, num, pos;

            eol = (mid - 1);
            pos = left;
            num = (right - left + 1);

            while ((left <= eol) && (mid <= right))
            {
                if (values[left].CompareTo(values[mid]) < 0)
                    temp[pos++] = values[left++];
                else
                    temp[pos++] = values[mid++];
            }

            while (left <= eol)
                temp[pos++] = values[left++];

            while (mid <= right)
                temp[pos++] = values[mid++];

            for (i = 0; i < num; i++)
            {
                values[right] = temp[right];
                right--;
            }
        }

        public void SortMerge<T>(T[] values, int left, int right) where T : IComparable<T>
        {
            int mid;

            if (right > left)
            {
                mid = (right + left) / 2;
                SortMerge(values, left, mid);
                SortMerge(values, (mid + 1), right);

                MainMerge(values, left, (mid + 1), right);
            }
        }

        public bool binarySearch(string[] arr, string x)
        {
            int l = 0, r = arr.Length - 1;
            while (l <= r)
            {
                int m = l + (r - l) / 2;

                int res = x.CompareTo(arr[m]);

                // Check if x is present at mid
                if (res == 0)
                    return true;

                // If x greater, ignore left half
                if (res > 0)
                    l = m + 1;

                // If x is smaller, ignore right half
                else
                    r = m - 1;
            }

            return false;
        }


    }

}
