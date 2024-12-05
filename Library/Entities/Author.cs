using System;
using System.Text.RegularExpressions;

namespace Library.Entities
{
    public class Author
    {
        private string _firstName;
        private string _lastName;
        private string _biography;

        public string FirstName
        {
            get => _firstName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("First name cannot be null or empty.");

                if (value.Length > 25)
                    throw new ArgumentException("First name cannot be longer than 25 characters.");

                if (!Regex.IsMatch(value, @"^[a-zA-Z]+$"))
                    throw new ArgumentException("First name can only contain English letters.");

                _firstName = value;
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Last name cannot be null or empty.");

                if (value.Length > 25)
                    throw new ArgumentException("Last name cannot be longer than 25 characters.");

                if (!Regex.IsMatch(value, @"^[a-zA-Z]+$"))
                    throw new ArgumentException("Last name can only contain English letters.");

                _lastName = value;
            }
        }


        public string Biography
        {
            get => _biography;
            set
            {
                if (value != null && value.Length > 150)
                    throw new ArgumentException("Biography cannot be longer than 150 characters.");

                _biography = value;
            }
        }

        public string FullName => $"{FirstName} {LastName}";

        public Author(string firstName, string lastName, string biography)
        {
            FirstName = firstName;
            LastName = lastName;
            Biography = biography;
        }
    }
}
