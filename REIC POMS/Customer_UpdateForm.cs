﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace REIC_POMS
{
    public partial class Customer_UpdateForm : Form
    {
        public Customer_UpdateForm()
        {
            InitializeComponent();
        }

        //---------------------
        //  SPECIAL VARIABLES |
        //---------------------
        private bool cancel; //Will be used by View Form to know if it will update its Customer data or not
        bool tinFilled;
        string fullTin; //Entire TIN Number (values from the 4 boxes combined)
        
        //-------------------
        //  GETTERS-SETTERS |
        //-------------------
        public string BNameToEdit {
            set { txtBusinessName.Text = value; }
            get { return txtBusinessName.Text; } }

        public bool TinFilled {
            set { tinFilled = value; }
            get { return tinFilled; } }

        public string FullTinNumber {
            set { fullTin = value; }
            get { return fullTin; } }

        public string FullTinToEdit {
            set {
                    txtTinNumber1.Text = value; //Need this if N/A
                    if (TinFilled == true) //Display the TIN, separated into parts
                    {
                        string[] tinPart = txtTinNumber1.Text.Split('-');
                        txtTinNumber1.Text = tinPart[0];
                        txtTinNumber2.Text = tinPart[1];
                        txtTinNumber3.Text = tinPart[2];
                        txtTinNumber4.Text = tinPart[3];
                    }
                    else { txtTinNumber1.Text = "N/A"; }
                }
             }

        public string TinToEdit1 {
            set { txtTinNumber1.Text = value; }
            get { return txtTinNumber2.Text; } }

        public string TinToEdit2 {
            set { txtTinNumber2.Text = value; }
            get { return txtTinNumber2.Text; } }

        public string TinToEdit3 {
            set { txtTinNumber3.Text = value; }
            get { return txtTinNumber3.Text; } }

        public string TinToEdit4 {
            set { txtTinNumber4.Text = value; }
            get { return txtTinNumber4.Text; } }

        public string NameToEdit {
            set { txtCustomerName.Text = value; }
            get { return txtCustomerName.Text; } }

        public string PersonToEdit {
            set { txtCustomerPerson.Text = value; }
            get { return txtCustomerPerson.Text; } }

        public string NumberToEdit {
            set { txtCustomerNumber.Text = value; }
            get { return txtCustomerNumber.Text; } }

        public string AcctNumberToEdit {
            set { txtAccountNumber.Text = value; }
            get { return txtAccountNumber.Text; } }

        public string EmailToEdit {
            set { txtCustomerEmail.Text = value; }
            get { return txtCustomerEmail.Text; } }

        public string AddressToEdit {
            set { txtCustomerAddress.Text = value; }
            get { return txtCustomerAddress.Text; } }

        public bool Cancel { get { return cancel; } }


        //-------------------
        //  BUTTON METHODS  |
        //-------------------
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //---VALIDATION
            //If any text field that can't be null is empty, display error message.
            if ((txtCustomerName.TextLength == 0) ||
                (txtCustomerPerson.TextLength == 0) ||
                (txtCustomerNumber.TextLength == 0) ||
                (txtCustomerEmail.TextLength == 0) ||
                (txtCustomerAddress.TextLength == 0))
            {
                MessageBox.Show("All fields, except Business Style/Name and TIN Number, are required to be filled out.", "Empty Fields", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; //Enables user to edit the form again
            }

            //If E-mail text field doesn't have the @ sign
            if (txtCustomerEmail.Text.Contains("@") == false)
            {
                MessageBox.Show("Please enter a valid e-mail.", "Error", MessageBoxButtons.OK);
                return; //Enables user to edit the form again
            }

            //If optional fields are empty, set values to N/A (Will be useful for View Form's sake.)
            if (txtBusinessName.TextLength == 0) { BNameToEdit = "N/A"; }
            //fullTin = txtTinNumber1.Text + "-" + txtTinNumber2.Text + "-" + txtTinNumber3.Text + "-" + txtTinNumber4.Text;
            fullTin = txtTinNumber1.Text + txtTinNumber2.Text + txtTinNumber3.Text + txtTinNumber4.Text; //For checking
                if ((fullTin == "")  || (fullTin.Contains("N/A") == true)) //If fullTin contains no numbers or "N/A"
                {
                    TinFilled = false;
                    FullTinNumber = "N/A";
                }
                else if (fullTin.Length != 12) //If TIN input is partially complete
                {
                    TinFilled = false;
                    MessageBox.Show("TIN number is incomplete.", "Error", MessageBoxButtons.OK);
                    return; //Enables user to edit the form again
                }
                else
                {
                    TinFilled = true;
                    fullTin = txtTinNumber1.Text + "-" + txtTinNumber2.Text + "-" + txtTinNumber3.Text + "-" + txtTinNumber4.Text;
                    FullTinNumber = fullTin;
                }
            if ((txtAccountNumber.TextLength == 0) || (txtAccountNumber.Text.Contains("N/A")))
                { AcctNumberToEdit = "N/A"; }

            //---CLOSE FORM
            cancel = false; //Will be used by Customer Main Screen
            this.Close(); //Only Customer_MainScreen.cs remains
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            cancel = true;
            this.Close();
        }

        //------------------------
        //  OTHER FORM ELEMENTS  |
        //------------------------
        //If User has input 3 characters in a TIN textbox part, text cursor jumps to the next textbox part.
        private void txtTinNumber1_TextChanged(object sender, EventArgs e)
        {
            if (txtTinNumber1.TextLength == 3)
            {
                txtTinNumber2.Select();
                txtTinNumber2.Focus();
            }
        }

        private void txtTinNumber2_TextChanged(object sender, EventArgs e)
        {
            if (txtTinNumber2.TextLength == 3)
            {
                txtTinNumber3.Select();
                txtTinNumber3.Focus();
            }
        }

        private void txtTinNumber3_TextChanged(object sender, EventArgs e)
        {
            if (txtTinNumber3.TextLength == 3)
            {
                txtTinNumber4.Select();
                txtTinNumber4.Focus();
            }
        }
    }
}
