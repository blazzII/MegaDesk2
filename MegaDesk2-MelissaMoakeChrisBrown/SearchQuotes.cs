﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MegaDesk2_MelissaMoakeChrisBrown
{
    public partial class DeskQuoteView : Form
    {
        public DeskQuoteView()
        {
            InitializeComponent();
            List<MaterialType> MaterialTypeList = Enum.GetValues(typeof(MaterialType))
                .Cast<MaterialType>().ToList();
            materialSearchBox.DataSource = MaterialTypeList;

            searchResults.View = View.Details;
            searchResults.Columns.Add("Name", -2, HorizontalAlignment.Left);
            searchResults.Columns.Add("Quote Date", -2, HorizontalAlignment.Left);
            searchResults.Columns.Add("Width", -2, HorizontalAlignment.Left);
            searchResults.Columns.Add("Depth", -2, HorizontalAlignment.Left);
            searchResults.Columns.Add("# of Drawers", -2, HorizontalAlignment.Left);
            searchResults.Columns.Add("Material", -2, HorizontalAlignment.Left);
            searchResults.Columns.Add("Rush Days", -2, HorizontalAlignment.Left);
            searchResults.Columns.Add("Price", -2, HorizontalAlignment.Left);
        }

        private void cancelSearchQuotesButton_Click(object sender, MouseEventArgs e)
        {
            var mainMenu = (MainMenu)Tag;
            mainMenu.Show();
            Close();
        }

        private void searchByMaterial(object sender, EventArgs e)
        {
            searchResults.Items.Clear();
            MaterialType MaterialType;
            string searchInput = materialSearchBox.SelectedItem.ToString();
            Enum.TryParse(searchInput, out MaterialType);

            try
            {
                string cFile = @"quotes.json";
                using (StreamReader sr = new StreamReader(cFile))
                {
                    string line;
                    //string display;

                    while ((line = sr.ReadLine()) != null)
                    {
                        DeskQuote fromFile = JsonConvert.DeserializeObject<DeskQuote>(line);
                        if (fromFile.Desk.MaterialName == MaterialType.ToString())
                        {
                            //display = fromFile.CustomerName + ", " + fromFile.QuoteDate.ToString("dd MMMMM yyyy") + ", "
                            // + fromFile.Desk.Width + ", " + fromFile.Desk.Depth + ", " + fromFile.Desk.NumOfDrawers + ", "
                            // + fromFile.Desk.MaterialName + ", " + fromFile.RushDays + ", " + fromFile.QuotePrice;

                            searchResults.Items.Add(new ListViewItem(new[]
                            {
                                fromFile.CustomerName, fromFile.QuoteDate.ToString("dd MMM yyyy"), fromFile.Desk.Width.ToString(),
                                fromFile.Desk.Depth.ToString(), fromFile.Desk.NumOfDrawers.ToString(), fromFile.Desk.MaterialName, fromFile.RushDays.ToString(),
                                fromFile.QuotePrice.ToString()
                             }
                            ));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "There is a problem");
            }
        }
    }
}
