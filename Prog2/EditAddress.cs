/* Grading ID: D2575
 * Program number: 3
 * Due Date: 11/15/16
 * Description: This class creates and Edit Address form, that has properties for Name, address1, address2, city, state, zip, selectedindex,
 * and addressform. It loads an address list from the main form to a listbox, and upon a user choosing an address to edit and hitting OK,
 * another dialog box is launched. This dialog box then has its data pulled back to this form, which stores it so the main form can pull
 * the edited address back to the original address list. 
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UPVApp
{
    public partial class EditAddress : Form
    {
        private string _name;//Backing field for name
        private string _address1; //backing field for address1
        private string _address2;//backing field for address2
        private string _city;//backing field for city
        private string _state;//backing field for state
        private int _zip;//backing field for zip
        private int _index;//backing field for selected index of list box.
        private List<Address> addressList;//backing field for the address list passed from main form
        private AddressForm _addressForm = new AddressForm();//backing field for the address form pulled later on.

        //preocndition: must have address list from main form
        //postcondition: creates EditAddress form, has address list from main form
        public EditAddress(List<Address> addresses)
        {
            addressList = addresses; //set backing field to list provided by the main form

            InitializeComponent();
        }

        //preconditions: value must not be empty or null
        //postconditions returns address name, also sets if value is not null or empty/
        public string AddressName //name of the address
        {
            //preconditions: none
            //postconditions: returns the value of addressname
            get { return _name; }

            //preconditions: value not null or empty
            //postconditions: sets _name equal to the value entered by user
            set { _name = value; }
        }

        //preconditions: value must not be null
        //postconditions: returns value of address 1 or sets to what user entered.
        public string Address1
        {
            //preconditions: none
            //postconditions: returns the value of address1
            get { return _address1; }

            //preconditions: value must not be empty or null
            //postconditions: sets _address1 to value entered by user
            set { _address1 = value; }
        }

        //preconditions: none
        //postconditions: returns _address2 or sets _address2 to value entered by user
        public string Address2
        {
            //preconditions: none
            //postconditions: returns the value of _address2
            get { return _address2; }

            //preconditions: none
            //postconditions: sets _address to value entered by user
            set { _address2 = value; }
        }

        //preconditions: value cannot be empty string or null
        //postconditions: returns _city or sets _city to value entered by user
        public string City
        {
            //preconditions: none
            //postconditions: returns the value of 
            get { return _city; }

            //preconditions
            //postconditions
            set { _city = value; }
        }

        //preconditions: value cannot be  empty string or null
        //postconditions: returns _state or sets _state to value entered by user
        public string State
        {
            //preconditions: none
            //postconditions: returns the value of 
            get { return _state; }

            //preconditions
            //postconditions
            set { _state = value; }
        }

        //preconditions: value must be between >=0 and <=99999
        //postconditions: returns _zip or sets to what was entered by the user
        public int Zip
        {

            //preconditions: none
            //postconditions: returns the value of _zip
            get { return _zip; }

            //preconditions: value must be between >= 0 and >= 99999
            //postconditions: sets _zip to value entered by the user
            set { _zip = value; }
        }

        //preconditions: value greater than 1
        //postconditions: sets property to the selected index of the list box, returns selected index of what user selected.
        public int SelectedIndex
        {

            //preconditions: none
            //postconditions: returns the value of _index
            get { return _index; }

            //preconditions: value must be greater than or equal to 0
            //postconditions: sets the _index to value entered by user
            set { _index = value; }
        }

        //Saved the form as a property of the Edit Address form. Mostly just to see if it wold work.
        //This was unneccessary, and I realize I could have just set the above properties to what I pulled from the address form, and
        //pulled that back to the main form.

        //preconditions: addressForm must not be null/
        //postconditions: Succesfully store address form to property in edit address form, return that value as well
        public AddressForm addressForm
        {

            //preconditions: none
            //postconditions: returns _addressform backing field
            get { return _addressForm; }

            //preconditions: value not null
            //postconditions: sets _addressform backing field to what was entered by the user.
            set { _addressForm = value; }
        }

        //preconditions: address list successfully passed from main form
        //postconditions: fills the address combo box with a list of addresses.
        private void EditAddress_Load(object sender, EventArgs e)
        {
            foreach (Address x in addressList)
            {
                addrsListBox.Items.Add(x.Name);
            }
        }

        //preconditions: 
        //Address list has sucessfully passed from main form
        //postconditions: dialog box opened by this button will have succesfully closed and the data will have been pulled back to the 
        //addressForm property.
        private void okBttn_Click(object sender, EventArgs e)
        {
            AddressForm editAdd = new AddressForm(); //New address form, used for editing selected address
            Address x = addressList[addrsListBox.SelectedIndex]; //set address from list to be used, located by the selected item's index
            SelectedIndex = addrsListBox.SelectedIndex; // set Property Selected index the the chosen address index


            //This sets all the properties of the selected address form object to the values stored by the temporary address object x
            editAdd.AddressName = x.Name;
            editAdd.Address1 = x.Address1;
            editAdd.Address2 = x.Address2;
            editAdd.City = x.City;
            editAdd.State = x.State;
            editAdd.ZipText = x.Zip.ToString();

            editAdd.ShowDialog();//Show the dialog of the address form created previously
            if (editAdd.DialogResult == DialogResult.OK)//Check to see if the DialogResult is OK, if so:
            {
                //I'm leaving this here to show I understood how else I could have done it. If I were to do this another way,
                //These properties would be what I sent back to the main form.

               // int newZip; //temporary variable to hold the zip entered on the edit form
                /*AddressName = editAdd.AddressName;
                Address1 = editAdd.Address1;
                Address2 = editAdd.Address2;
                City = editAdd.City;
                State = editAdd.State;
                int.TryParse(editAdd.ZipText, out newZip);
                Zip = newZip;*/

                //Set the addressForm property to the address form created earlier. This will be pulled by main form for editing the selected
                //address object.
                if(addressForm != null)
                addressForm = editAdd;
                DialogResult = DialogResult.OK;
            }
        }

        //preconditions: None
        //postconditions: closes the form, does not open dialog to edit address.
        private void cnclBttn_Click(object sender, EventArgs e)
        {
            this.Close();//close this form when the user hits the cancel button.
        }
    }
}
