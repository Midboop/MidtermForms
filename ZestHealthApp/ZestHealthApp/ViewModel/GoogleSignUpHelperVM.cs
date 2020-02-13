using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ZestHealthApp.ViewModel
{
     public class GoogleSignUpHelperVM : INotifyPropertyChanged
    {
		private string email;
		public string Email
		{
			get { return email; }
			set
			{
				// sets the email value in the database
				email = value;
				PropertyChanged(this, new PropertyChangedEventArgs("EmailAddress"));
			}
		}

		private string picture;

		public event PropertyChangedEventHandler PropertyChanged;

		public string Picture
		{
			get { return picture; }
			set
			{
				// and the password value
				picture = value;
				PropertyChanged(this, new PropertyChangedEventArgs("ProfilePicture"));
			}
		}

		private string name;

		public string Name
		{
			get { return name; }
			set
			{
				// and the name
				name = value;
				PropertyChanged(this, new PropertyChangedEventArgs("DisplayName"));
			}
		}
	}
}
