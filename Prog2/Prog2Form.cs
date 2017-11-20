// Program 2
// CIS 200
// Fall 2016
// Due: 11/1/2016
// By: Andrew L. Wright (Students use Grading ID)

// File: Prog2Form.cs
// This class creates the main GUI for Program 2. It provides a
// File menu with About and Exit items, an Insert menu with Address and
// Letter items, and a Report menu with List Addresses and List Parcels
// items.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace UPVApp
{
    public partial class Prog2Form : Form
    {
        private UserParcelView upv; // The UserParcelView
        private BinaryFormatter formatter = new BinaryFormatter();

        // Precondition:  None
        // Postcondition: The form's GUI is prepared for display. A few test addresses are
        //                added to the list of addresses
        public Prog2Form()
        {
            InitializeComponent();
            upv = new UserParcelView();

        }

        // Precondition:  File, About menu item activated
        // Postcondition: Information about author displayed in dialog box
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string NL = Environment.NewLine; // Newline shorthand

            MessageBox.Show($"Program 2{NL}By: Andrew L. Wright{NL}CIS 200{NL}Fall 2016",
                "About Program 2");
        }

        // Precondition:  File, Exit menu item activated
        // Postcondition: The application is exited
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Precondition:  Insert, Address menu item activated
        // Postcondition: The Address dialog box is displayed. If data entered
        //                are OK, an Address is created and added to the list
        //                of addresses
        private void addressToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddressForm addressForm = new AddressForm();    // The address dialog box form
            DialogResult result = addressForm.ShowDialog(); // Show form as dialog and store result

            if (result == DialogResult.OK) // Only add if OK
            {
                try
                {
                    upv.AddAddress(addressForm.AddressName, addressForm.Address1,
                        addressForm.Address2, addressForm.City, addressForm.State,
                        int.Parse(addressForm.ZipText)); // Use form's properties to create address
                }
                catch (FormatException) // This should never happen if form validation works!
                {
                    MessageBox.Show("Problem with Address Validation!", "Validation Error");
                }
            }

            addressForm.Dispose(); // Best practice for dialog boxes
        }

        // Precondition:  Report, List Addresses menu item activated
        // Postcondition: The list of addresses is displayed in the addressResultsTxt
        //                text box
        private void listAddressesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder result = new StringBuilder(); // Holds text as report being built
                                                        // StringBuilder more efficient than String
            string NL = Environment.NewLine;            // Newline shorthand

            result.Append("Addresses:");
            result.Append(NL); // Remember, \n doesn't always work in GUIs
            result.Append(NL);

            foreach (Address a in upv.AddressList)
            {
                result.Append(a.ToString());
                result.Append(NL);
                result.Append("------------------------------");
                result.Append(NL);
            }

            reportTxt.Text = result.ToString();

            // Put cursor at start of report
            reportTxt.Focus();
            reportTxt.SelectionStart = 0;
            reportTxt.SelectionLength = 0;
        }

        // Precondition:  Insert, Letter menu item activated
        // Postcondition: The Letter dialog box is displayed. If data entered
        //                are OK, a Letter is created and added to the list
        //                of parcels
        private void letterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LetterForm letterForm; // The letter dialog box form
            DialogResult result;   // The result of showing form as dialog

            if (upv.AddressCount < LetterForm.MIN_ADDRESSES) // Make sure we have enough addresses
            {
                MessageBox.Show("Need " + LetterForm.MIN_ADDRESSES + " addresses to create letter!",
                    "Addresses Error");
                return;
            }

            letterForm = new LetterForm(upv.AddressList); // Send list of addresses
            result = letterForm.ShowDialog();

            if (result == DialogResult.OK) // Only add if OK
            {
                try
                {
                    // For this to work, LetterForm's combo boxes need to be in same
                    // order as upv's AddressList
                    upv.AddLetter(upv.AddressAt(letterForm.OriginAddressIndex),
                        upv.AddressAt(letterForm.DestinationAddressIndex),
                        decimal.Parse(letterForm.FixedCostText)); // Letter to be inserted
                }
                catch (FormatException) // This should never happen if form validation works!
                {
                    MessageBox.Show("Problem with Letter Validation!", "Validation Error");
                }
            }

            letterForm.Dispose(); // Best practice for dialog boxes
        }

        // Precondition:  Report, List Parcels menu item activated
        // Postcondition: The list of parcels is displayed in the parcelResultsTxt
        //                text box
        private void listParcelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder result = new StringBuilder(); // Holds text as report being built
                                                        // StringBuilder more efficient than String
            decimal totalCost = 0;                      // Running total of parcel shipping costs
            string NL = Environment.NewLine;            // Newline shorthand

            result.Append("Parcels:");
            result.Append(NL); // Remember, \n doesn't always work in GUIs
            result.Append(NL);

            foreach (Parcel p in upv.ParcelList)
            {
                result.Append(p.ToString());
                result.Append(NL);
                result.Append("------------------------------");
                result.Append(NL);
                totalCost += p.CalcCost();
            }

            result.Append(NL);
            result.Append($"Total Cost: {totalCost:C}");

            reportTxt.Text = result.ToString();

            // Put cursor at start of report
            reportTxt.Focus();
            reportTxt.SelectionStart = 0;
            reportTxt.SelectionLength = 0;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {           
            DialogResult result; //used to indicate when user hits OK or cancel
            string fileName; //Temporary variable to hold the file name

            using (SaveFileDialog saveFile = new SaveFileDialog()) // Uses a new save dialog to navigate local file structure and save.
            {
                saveFile.CheckFileExists = false;//Don't look to see if the file already exists
                result = saveFile.ShowDialog();//Show the save dialog
                fileName = saveFile.FileName;
            }

            if (result == DialogResult.OK)
            {
                if (fileName == string.Empty) //Check to see if string is empty
                {
                    MessageBox.Show("Enter a valid file name", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error); // throw error if the file name is empty.
                }
                else
                {
                    try
                    {
                        FileStream saveAs = new FileStream(fileName,
                            FileMode.OpenOrCreate, FileAccess.Write);//+++++++++++++++++++++++++++++++++++++++++++===================
                        formatter.Serialize(saveAs, upv);//open or create the file with write access, save file via file stream
                        saveAs.Close();//close the saveAs box when the write operation is complete.
                    }
                    catch(IOException)//++++++++++++++++++++++++++++++++==============================================================
                    {
                        MessageBox.Show("Couldn't open file", "Error", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);//Throw this error if the user picks a file the program can't save as.
                    }
                }
            }
        }

        private void openButton_Click(object sender, EventArgs e)
        {
            DialogResult result; //temporary variable used to hold the dialog result of the open dialog
            string fileName;//temporary variable, holds file name.

            using (OpenFileDialog openMe = new OpenFileDialog())//Use an open dialog to open the file
            {
                result = openMe.ShowDialog();//show the open dialog
                fileName = openMe.FileName;//set variable to open dialog filename
                openMe.CheckFileExists = false;//don't check to see if the file exists.
            }
            if (fileName == string.Empty)//checks to see if you actually picked a file
            {
                MessageBox.Show("Please pick a file and try again");//Show this if use didn't pick a file
            }
            else
            {
                try
                {
                    FileStream openFile = new FileStream(fileName,
                         FileMode.Open, FileAccess.ReadWrite);//Filestream for opening the file with read/write access.
                    upv = (UserParcelView)formatter.Deserialize(openFile);//Set the upv object of the program to the upv of the file.
                }
                catch (IOException)
                {
                    MessageBox.Show("Couldn't open file", "Error",
                     MessageBoxButtons.OK, MessageBoxIcon.Error);//throw this if the user picks a file that cannot be opened by program.
                }
                catch (SerializationException)
                {
                    MessageBox.Show("Wrong kind of file, try again.");//Show if user picks a file of the wrong type of file to open
                }
            }
        }

        private void addressToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DialogResult result;//Hold dialog result of the edit form.
            EditAddress editForm = new EditAddress(upv.AddressList);    // The address dialog box form
            result = editForm.ShowDialog();//show the edit form.

            if (result == DialogResult.OK)
            {     
                int newZip;//temporary variable to hold the edited zip from the address form
                int.TryParse(editForm.addressForm.ZipText, out newZip);//Tries to parse the ziptext, does nothing if fails.
                upv.AddressList[editForm.SelectedIndex].Name = editForm.addressForm.AddressName;//set these to the selected address object from the upv list, get data from the editForm
                upv.AddressList[editForm.SelectedIndex].Address1 = editForm.addressForm.Address1;
                upv.AddressList[editForm.SelectedIndex].Address2 = editForm.addressForm.Address2;
                upv.AddressList[editForm.SelectedIndex].City = editForm.addressForm.City;
                upv.AddressList[editForm.SelectedIndex].State = editForm.addressForm.State;
                upv.AddressList[editForm.SelectedIndex].Zip = newZip;
            }
        }
    }
}