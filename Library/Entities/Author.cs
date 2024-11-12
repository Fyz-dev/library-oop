using System;

namespace Library.Entities
{
    internal class Author
    {
        public string FirstName
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public string LastName
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public string Biography
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public string FullName => throw new NotImplementedException();

        public Author(string firstName, string lastName)
        {
            throw new NotImplementedException();
        }

        public Author(string firstName, string lastName, string biography)
            : this(firstName, lastName)
        {
            throw new NotImplementedException();
        }
    }
}
