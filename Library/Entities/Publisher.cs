//using System;
//using System.Text.RegularExpressions;

//namespace Library.Entities
//{
//    public class Publisher
//    {
//        private string _name;
//        private string _address;

//        public string Name
//        {
//            get => _name;
//            set
//            {
//                if (string.IsNullOrWhiteSpace(value))
//                    throw new ArgumentException("Publisher name cannot be null or empty.");

//                if (!Regex.IsMatch(value, @"^[a-zA-Z\s.]+$"))
//                    throw new ArgumentException("Publisher name can only contain English letters, spaces, and periods.");

//                _name = value;
//            }
//        }

//        public string Address
//        {
//            get => _address;
//            set
//            {
//                if (string.IsNullOrWhiteSpace(value))
//                    throw new ArgumentException("Address cannot be null or empty.");

//                if (value.Length < 5)
//                    throw new ArgumentException("Address must be at least 5 characters long.");

//                if (!Regex.IsMatch(value, @"^[a-zA-Z0-9\s,.-]+$"))
//                    throw new ArgumentException("Address can only contain English letters, numbers, spaces, commas, periods, and hyphens.");

//                _address = value;
//            }
//        }

//        public Publisher(string name, string address)
//        {
//            Name = name;
//            Address = address;
//        }

//        public override string ToString()
//        {
//            return $"{Name} {Address}";
//        }
//    }
//}
